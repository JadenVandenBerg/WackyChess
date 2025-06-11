using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class Functionss : MonoBehaviour
{
    public static void movePiece(Piece p, GameObject toAppend)
    {
        GameObject go = p.go;
        go.GetComponent<RectTransform>().SetParent(toAppend.GetComponent<RectTransform>());
        go.transform.position = Vector2.zero;
        go.transform.localPosition = new Vector2((go.transform.position.x + toAppend.GetComponent<RectTransform>().sizeDelta.x / 2), (go.transform.position.y + toAppend.GetComponent<RectTransform>().sizeDelta.y / 2));
        if (!p.hasMoved)
        {
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(toAppend.GetComponent<RectTransform>().sizeDelta.x * 0.8f, toAppend.GetComponent<RectTransform>().sizeDelta.y * 0.8f);
            go.AddComponent<PhotonView>();
        }
    }
    public static void movePieceNoImage(Piece p, GameObject toAppend)
    {
        GameObject go = p.go;

        if (go == null)
        {
            //Debug.Log("GO IS NULL");
            return;
        }

        go.GetComponent<RectTransform>().SetParent(toAppend.GetComponent<RectTransform>());
        go.transform.position = Vector2.zero;
        go.transform.localPosition = new Vector2((go.transform.position.x + toAppend.GetComponent<RectTransform>().sizeDelta.x / 2), (go.transform.position.y + toAppend.GetComponent<RectTransform>().sizeDelta.y / 2));
    }
    public static GameObject clicked(BaseEventData e)
    {
        PointerEventData pointerEventData = (PointerEventData)e;
        if (pointerEventData.eligibleForClick && gameData.selected != pointerEventData.pointerPress)
        {
            //Debug.Log("Clicked: " + pointerEventData.pointerPress);
            gameData.selectedToMove = gameData.selected;
            gameData.selected = pointerEventData.pointerPress;
            return pointerEventData.pointerPress;
        }
        else if (gameData.selected == pointerEventData.pointerPress)
        {
            gameData.readyToMove = false;
            gameData.isSelected = false;
            resetBoardColours();
            gameData.selectedToMove = null;
            gameData.selected = null;
            //gameData.isSelected = false;
        }
        return null;
    }

    public static void resetBoardColours()
    {

        gameData.isSelected = false;
        //gameData.selected = null;
        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                Image s = findSquare(i, j).GetComponent<Image>();
                if ((i + j) % 2 == 0)
                {
                    s.color = (Color)(new Color32(14, 115, 34, 255));
                }
                else
                {
                    s.color = (Color)(new Color32(131, 199, 145, 255));
                }
            }
        }
    }

    public static Piece getPieceOnSquare(GameObject square)
    {
        if (square != null && square.transform != null)
        {
            if (square.transform.childCount == 0)
            {
                return null;
            }

            if (gameData.piecesDict.ContainsKey(square.transform.GetChild(0).gameObject))
            {
                return gameData.piecesDict[square.transform.GetChild(0).gameObject];
            }
        }

        return null;
    }

    public static Piece getPieceOnSquareDebug(GameObject square)
    {
        if (square != null && square.transform != null)
        {
            if (square.transform.childCount == 0)
            {
                return null;
            }

            if (gameData.piecesDict.ContainsKey(square.transform.GetChild(0).gameObject))
            {
                return gameData.piecesDict[square.transform.GetChild(0).gameObject];
            }
            else
            {
                int[] coords = findCoords(square);
                foreach (Piece piece in gameData.piecesDict.Values)
                {
                    if (piece.position[0] == coords[0] && piece.position[1] == coords[1])
                    {
                        return piece;
                    }
                }
            }
        }

        return null;
    }

    public static List<int[]> addMovesToCurrentMoveableCoords(Piece piece)
    {
        clearCurrentMoveableCoords();
        if (piece == null)
        {
            return null;
        }

        List<int[]> allMoves = new List<int[]>();

        int color = piece.color;

        bool check = false;
        if (color == 1 && gameData.isInCheck[0] == 1 || color == -1 && gameData.isInCheck[1] == 1)
        {
            check = true;
        }

        if (gameData.turn == gameData.forceStayTurn)
        {
            //Force Stay Turn Moves
            iterateThroughPieceMoves(moveComparator, piece, piece.forceStayTurnMoves, null, Color.red, check, false, false, allMoves, color, true, false);
        }
        else
        {
            iterateThroughPieceMoves(moveComparator, piece, piece.moves, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false);
            iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.moveAndAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false);
            iterateThroughPieceMoves(attacksComparator, piece, piece.attacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false);
            iterateThroughPieceMoves(oneTimeMovesComparator, piece, piece.oneTimeMoves, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false);
            iterateThroughPieceMoves(oneTimeMoveAndAttacksComparator, piece, piece.oneTimeMoveAndAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false);
            iterateThroughPieceMoves(murderousAttacksComparator, piece, piece.murderousAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false);
            iterateThroughPieceMoves(conditionalAttacksComparator, piece, piece.conditionalAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false);
            iterateThroughPieceMoves(jumpAttacksComparator, piece, piece.jumpAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false);

            //Dependent Attacks
            piece.dependentMovesSet();
            iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.dependentAttacks, null, Color.red, check, false, false, allMoves, color, true, false);

            //Interactive Moves
            piece.interactiveMovesSet();
            iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.interactiveAttacks, null, Color.red, check, false, false, allMoves, color, true, false);

            //Flag Moves
            if (piece.flag == 1)
            {
                iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.flagMove1, null, Color.red, check, false, false, allMoves, color, true, false);
            }
            else if (piece.flag == 2)
            {
                iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.flagMove2, null, Color.red, check, false, false, allMoves, color, false, false);
            }
        }

        foreach (int[] move in gameData.currentMoveableCoords)
        {
            allMoves.Add(move);
        }

        return allMoves;
    }

    public static void clearCurrentMoveableCoords()
    {
        gameData.currentMoveableCoords.Clear();
    }

    public static GameObject findSquare(int x, int y)
    {

        if (x < 0 || y < 0) return null;
        if (x > 8 || y > 8) return null;

        string newX = x.ToString();
        string newY = y.ToString();

        for (int i = 0; i < gameData.board.transform.childCount; i++)
        {
            GameObject child = gameData.board.transform.GetChild(i).gameObject;
            if (child.gameObject.name[0].ToString() == newX && child.gameObject.name[1].ToString() == newY)
            {
                return child.gameObject;
            }
        }

        return null;
    }

    public static int[] findCoords(GameObject square)
    {
        int[] sq;
        sq = square.name.ToIntArray();

        sq[0] = sq[0] - 48;
        sq[1] = sq[1] - 48;

        return sq;
    }

    public static bool isJump(Piece piece, int[] from, int[] to) //For diagonal, file, column jumps
    {

        //Debug.Log("JUMP INFO -");
        //Debug.Log("FROM: " + from[0] + "," + from[1]);
        //Debug.Log("TO: " + to[0] + "," + to[1]);
        int dirX, dirY;

        if (from[0] > to[0])
        {
            dirX = -1;
        }
        else if (from[0] == to[0])
        {
            dirX = 0;
        }
        else
        {
            dirX = 1;
        }

        if (from[1] > to[1])
        {
            dirY = -1;
        }
        else if (from[1] == to[1])
        {
            dirY = 0;
        }
        else
        {
            dirY = 1;
        }

        int diff = Mathf.Abs(from[0] - to[0]);
        if (Mathf.Abs(from[1] - to[1]) > diff)
        {
            diff = Mathf.Abs(from[1] - to[1]);
        }

        //Debug.Log("INFO: " + dirX + "," + dirY + "," + diff);

        for (int i = 1; i <= diff - 1; i++)
        {
            //Debug.Log("SEARCH: " + from[0] + (i * dirX) + "," + from[1] + (i * dirY));
            GameObject square = findSquare(from[0] + (i * dirX), from[1] + (i * dirY));
            //if (square != null) Debug.Log("IN BETWEEN: " + square.name);
            if (square != null && getPieceOnSquare(square) != null)
            {
                Piece onSquare = getPieceOnSquare(square);
                if (piece.state == "Ghost" && onSquare.color == piece.color
                    || onSquare.state == "Ghoul" && piece.color == onSquare.color)
                {
                    continue;
                }
                //Debug.Log("ITS A JUMP");
                return true;
            }
        }
        return false;
    }

    public static bool moveComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour)
    {
        return !jump && pieceIsNull;
    }
    public static bool moveAndAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour)
    {
        return !jump && (pieceIsNull || pieceIsDiffColour);
    }
    public static bool attacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour)
    {
        return !jump && !pieceIsNull && pieceIsDiffColour;
    }
    public static bool oneTimeMovesComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour)
    {
        return !jump && !piece.hasMoved && pieceIsNull;
    }
    public static bool oneTimeMoveAndAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour)
    {
        return !jump && !piece.hasMoved && (pieceIsNull || pieceIsDiffColour);
    }
    public static bool murderousAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour)
    {
        return !jump;
    }
    public static bool conditionalAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour)
    {
        return !jump && piece.condition && pieceIsNull;
    }
    public static bool jumpAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour)
    {
        return pieceIsNull || pieceIsDiffColour;
    }

    public static void iterateThroughPieceMoves(Func<Piece, bool, bool, bool, bool> comparator, Piece piece, int[,] moveType, Piece highlightPiece, Color highlightColor, bool check, bool highlight, bool changeValue, List<int[]> allMoves, int color, bool execDummyMove, bool ignoreDisabled)
    {
        for (int i = 0; i < moveType.GetLength(0); i++)
        {
            GameObject goHighlight = findSquare(moveType[i, 0] + piece.position[0], moveType[i, 1] + piece.position[1]);
            if (goHighlight != null)
            {
                int[] newPos = new int[] { moveType[i, 0] + piece.position[0], moveType[i, 1] + piece.position[1] };

                bool pieceIsNull = getPieceOnSquare(goHighlight) == null;
                bool pieceIsDiffColour = false;
                if (!pieceIsNull)
                {
                    pieceIsDiffColour = getPieceOnSquare(goHighlight).color != color;
                    if (ignoreDisabled && getPieceOnSquare(goHighlight).disabled)
                    {
                        //Debug.Log("THE PIECE ON " + newPos[0] + "," + newPos[1] + " IS DISABLED!");
                        pieceIsNull = true;
                    }
                }

                if (comparator(piece, isJump(piece, piece.position, newPos), pieceIsNull, pieceIsDiffColour))
                {

                    //if (piece.color == -1) Debug.Log("CONSIDERING MOVE: " + piece.position[0] + "," + piece.position[1] + " -> " + newPos[0] + "," + newPos[1]);
                    if (execDummyMove)
                    {
                        bool stillInCheck = dummyMove(piece, newPos);

                        if (!stillInCheck)
                        {
                            //Debug.Log("MOVE ACCEPTED");
                            if (highlight && piece == highlightPiece) goHighlight.GetComponent<Image>().color = highlightColor;
                            if (changeValue) gameData.currentMoveableCoordsAllPieces.Add(newPos);
                            allMoves.Add(newPos);
                        }
                    }
                    else
                    {
                        if (!check) allMoves.Add(newPos);
                    }
                }
            }
        }
    }

    public static List<int[]> addToCurrentMoveableCoordsTotal(int color, bool changeValue, bool highlight, Piece highlightPiece, bool execDummyMove, bool ignoreDisabled)
    {
        gameData.currentMoveableCoordsAllPieces.Clear();

        if (highlightPiece != null)
        {
            clearCurrentMoveableCoords();
            resetBoardColours();
            gameData.isSelected = true;

            addMovesToCurrentMoveableCoords(highlightPiece);
        }

        bool check = false;
        if (color == 1 && gameData.isInCheck[0] == 1 || color == -1 && gameData.isInCheck[1] == 1)
        {
            check = true;
        }

        List<int[]> allMoves = new List<int[]>();
        Color highlightColor = Color.red;

        foreach (Piece piece in gameData.piecesDict.Values)
        {
            if (piece == null || piece.color != color || piece.disabled)
            {
                continue;
            }

            //if (color == -1) Debug.Log("LOOKING AT " + piece.name + "! Color: " + piece.color);

            //Force Stay Turn Moves
            if (gameData.turn == gameData.forceStayTurn)
            {
                //Force Stay Turn Moves
                iterateThroughPieceMoves(moveComparator, piece, piece.forceStayTurnMoves, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);
            }
            else
            {
                //Moves
                iterateThroughPieceMoves(moveComparator, piece, piece.moves, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //Moves and Attacks
                iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.moveAndAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //Attacks
                iterateThroughPieceMoves(attacksComparator, piece, piece.attacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //One Time Moves
                iterateThroughPieceMoves(oneTimeMovesComparator, piece, piece.oneTimeMoves, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //One Time Move and Attacks
                iterateThroughPieceMoves(oneTimeMoveAndAttacksComparator, piece, piece.oneTimeMoveAndAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //Murderous Attacks
                iterateThroughPieceMoves(murderousAttacksComparator, piece, piece.murderousAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //Conditional Attacks
                iterateThroughPieceMoves(conditionalAttacksComparator, piece, piece.conditionalAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //Jump Attacks
                iterateThroughPieceMoves(jumpAttacksComparator, piece, piece.jumpAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //Dependent Attacks
                piece.dependentMovesSet();
                iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.dependentAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //Interactive Moves
                piece.interactiveMovesSet();
                iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.interactiveAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);

                //Flag Moves
                if (piece.flag == 1)
                {
                    iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.flagMove1, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);
                }
                else if (piece.flag == 2)
                {
                    iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.flagMove2, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled);
                }
            }

        }

        return allMoves;
    }

    public static bool isInList(List<int[]> list, int[] compare, bool debug)
    {
        //if (debug) Debug.Log("Search Position: " + compare[0] + "," + compare[1]);
        for (int i = 0; i < list.Count; i++)
        {
            //if (debug) Debug.Log("" + list[i][0] + " : " + list[i][1]);
            if (compare[0] == list[i][0] && compare[1] == list[i][1])
            {
                return true;
            }
        }

        return false;
    }

    public static bool isCheck(Piece king)
    {
        //Debug.Log("Searching for Check. Color: " + king.color);
        List<int[]> moves = addToCurrentMoveableCoordsTotal(king.color * -1, false, false, null, false, true);
        //Debug.Log("CHECK SEARCH END");
        bool check = isInList(moves, king.getPosition(), false);

        if (check && king.color == 1)
        {
            gameData.isInCheck[0] = 1;
        }
        else if (check && king.color == -1)
        {
            gameData.isInCheck[1] = 1;
        }
        else if (!check && king.color == 1)
        {
            gameData.isInCheck[0] = 0;
        }
        else if (!check && king.color == -1)
        {
            gameData.isInCheck[1] = 0;
        }

        return check;
    }

    public static bool dummyMove(Piece piece, int[] coords) //Returns dummyIsCheck
    {
        int x = piece.position[0];
        int y = piece.position[1];

        Piece king;
        if (piece.color == 1)
        {
            king = gameData.whiteKing;
        }
        else
        {
            king = gameData.blackKing;
        }

        bool isInCheck;

        GameObject square = findSquare(coords[0], coords[1]);

        //Save State
        Piece oldPiece = null;
        bool restore = false;


        GameObject go = null;

        if (getPieceOnSquareDebug(square) != null)
        {
            oldPiece = getPieceOnSquareDebug(square);
            go = removePieceFromBoard(oldPiece);
            restore = true;
        }
        piece.setPosition(coords);
        movePieceNoImage(piece, square);

        List<int[]> moves = addToCurrentMoveableCoordsTotal(piece.color * -1, false, false, null, false, true);
        isInCheck = dummyIsCheck(moves, king);

        if (restore)
        {
            restorePieceToBoard(oldPiece, coords, go);
        }

        piece.setPosition(new int[] { x, y });
        movePieceNoImage(piece, findSquare(x, y));

        return isInCheck;
    }

    public static GameObject removePieceFromBoard(Piece piece)
    {

        piece.disabled = true;
        GameObject gp = new GameObject();
        gp.AddComponent<RectTransform>();
        movePieceNoImage(piece, gp);


        return gp;
    }

    public static void restorePieceToBoard(Piece piece, int[] position, GameObject go)
    {
        if (piece != null)
        {
            piece.disabled = false;
            movePieceNoImage(piece, findSquare(position[0], position[1]));
            Piece pieceToCheck = getPieceOnSquare(findSquare(position[0], position[1]));
        }


        DestroyWrapper(go);
    }

    public static void DestroyWrapper(GameObject go)
    {
        Destroy(go);
    }

    public static bool dummyIsCheck(List<int[]> moves, Piece king)
    {
        return isInList(moves, king.getPosition(), false);
    }

    public static bool isCheckMate(Piece king, bool execDummyMove)
    {
        //Debug.Log("Searching for Checkmate. Color: " + king.color);
        List<int[]> moves = addToCurrentMoveableCoordsTotal(king.color, true, false, null, execDummyMove, true);
        //Debug.Log("Searching for Checkmate End!");

        int arrPos = 0;
        if (king.color == -1)
        {
            arrPos = 1;
        }

        Debug.Log("Possible Moves: " + moves.Count);
        if (moves.Count == 0)
        {
            if (king.color == 1)
            {
                gameData.winner = "Black Wins!";
            }
            else
            {
                gameData.winner = "White Wins!";
            }

            if (gameData.isInCheck[arrPos] == 0)
            {
                gameData.winner = "Stalemate! Tie Game";
                gameData.staleMate = true;
                gameData.checkMate = false;
            }
            else
            {
                gameData.staleMate = false;
                gameData.checkMate = true;
            }

            return true;
        }

        return false;
    }

    public static void UpdateMovesForColor(Piece p)
    {
        for (int i = 0; i < p.moves.GetLength(0); i++)
        {
            p.moves[i, 1] = p.moves[i, 1] * p.color;
            p.moves[i, 0] = p.moves[i, 0] * p.color;
        }

        for (int i = 0; i < p.oneTimeMoves.GetLength(0); i++)
        {
            p.oneTimeMoves[i, 1] = p.oneTimeMoves[i, 1] * p.color;
            p.oneTimeMoves[i, 0] = p.oneTimeMoves[i, 0] * p.color;
        }

        for (int i = 0; i < p.attacks.GetLength(0); i++)
        {
            p.attacks[i, 1] = p.attacks[i, 1] * p.color;
            p.attacks[i, 0] = p.attacks[i, 0] * p.color;
        }

        for (int i = 0; i < p.moveAndAttacks.GetLength(0); i++)
        {
            p.moveAndAttacks[i, 1] = p.moveAndAttacks[i, 1] * p.color;
            p.moveAndAttacks[i, 0] = p.moveAndAttacks[i, 0] * p.color;
        }

        for (int i = 0; i < p.jumpAttacks.GetLength(0); i++)
        {
            p.jumpAttacks[i, 1] = p.jumpAttacks[i, 1] * p.color;
            p.jumpAttacks[i, 0] = p.jumpAttacks[i, 0] * p.color;
        }

        for (int i = 0; i < p.murderousAttacks.GetLength(0); i++)
        {
            p.murderousAttacks[i, 1] = p.murderousAttacks[i, 1] * p.color;
            p.murderousAttacks[i, 0] = p.murderousAttacks[i, 0] * p.color;
        }

        for (int i = 0; i < p.conditionalAttacks.GetLength(0); i++)
        {
            p.conditionalAttacks[i, 1] = p.conditionalAttacks[i, 1] * p.color;
            p.conditionalAttacks[i, 0] = p.conditionalAttacks[i, 0] * p.color;
        }

        for (int i = 0; i < p.oneTimeMoveAndAttacks.GetLength(0); i++)
        {
            p.oneTimeMoveAndAttacks[i, 1] = p.oneTimeMoveAndAttacks[i, 1] * p.color;
            p.oneTimeMoveAndAttacks[i, 0] = p.oneTimeMoveAndAttacks[i, 0] * p.color;
        }

        for (int i = 0; i < p.dependentAttacks.GetLength(0); i++)
        {
            p.dependentAttacks[i, 1] = p.dependentAttacks[i, 1] * p.color;
            p.dependentAttacks[i, 0] = p.dependentAttacks[i, 0] * p.color;
        }

        for (int i = 0; i < p.interactiveAttacks.GetLength(0); i++)
        {
            p.interactiveAttacks[i, 1] = p.interactiveAttacks[i, 1] * p.color;
            p.interactiveAttacks[i, 0] = p.interactiveAttacks[i, 0] * p.color;
        }

        for (int i = 0; i < p.flagMove1.GetLength(0); i++)
        {
            p.flagMove1[i, 1] = p.flagMove1[i, 1] * p.color;
            p.flagMove1[i, 0] = p.flagMove1[i, 0] * p.color;
        }

        for (int i = 0; i < p.flagMove2.GetLength(0); i++)
        {
            p.flagMove2[i, 1] = p.flagMove2[i, 1] * p.color;
            p.flagMove2[i, 0] = p.flagMove2[i, 0] * p.color;
        }

        for (int i = 0; i < p.pushMoves.GetLength(0); i++)
        {
            p.pushMoves[i, 1] = p.pushMoves[i, 1] * p.color;
            p.pushMoves[i, 0] = p.pushMoves[i, 0] * p.color;
        }

        for (int i = 0; i < p.enPassantMoves.GetLength(0); i++)
        {
            p.enPassantMoves[i, 1] = p.enPassantMoves[i, 1] * p.color;
            p.enPassantMoves[i, 0] = p.enPassantMoves[i, 0] * p.color;
        }
    }

    public static void updateCastleCondition()
    {
        List<List<int>> castleableMovesWhite = new List<List<int>>();
        List<List<int>> castleableMovesBlack = new List<List<int>>();

        gameData.whiteKing.condition = false;
        gameData.blackKing.condition = false;

        if (gameData.isInCheck[0] != 1)
        {
            if (gameData.whiteKing.hasMoved == false)
            {
                if (gameData.whiteRooks[0].hasMoved == false && !isJump(gameData.whiteKing, gameData.whiteKing.position, gameData.whiteRooks[0].position))
                {
                    if (gameData.piecesDict.ContainsValue(gameData.whiteRooks[0]) && gameData.piecesDict.ContainsValue(gameData.whiteKing))
                    {
                        gameData.whiteKing.condition = true;
                        List<int> move = new List<int> { -2, 0 };
                        castleableMovesWhite.Add(move);
                    }
                }
                else if (gameData.whiteRooks[1].hasMoved == false && !isJump(gameData.whiteKing, gameData.whiteKing.position, gameData.whiteRooks[1].position))
                {
                    if (gameData.piecesDict.ContainsValue(gameData.whiteRooks[1]) && gameData.piecesDict.ContainsValue(gameData.whiteKing))
                    {
                        gameData.whiteKing.condition = true;
                        List<int> move = new List<int> { 2, 0 };
                        castleableMovesWhite.Add(move);
                    }
                }
            }
        }

        if (gameData.isInCheck[1] != 1)
        {
            if (gameData.blackKing.hasMoved == false)
            {
                if (gameData.blackRooks[0].hasMoved == false && !isJump(gameData.blackKing, gameData.blackKing.position, gameData.blackRooks[0].position))
                {
                    if (gameData.piecesDict.ContainsValue(gameData.blackRooks[0]) && gameData.piecesDict.ContainsValue(gameData.blackKing))
                    {
                        gameData.blackKing.condition = true;
                        List<int> move = new List<int> { -2, 0 };
                        castleableMovesBlack.Add(move);
                    }
                }
                else if (gameData.blackRooks[1].hasMoved == false && !isJump(gameData.blackKing, gameData.blackKing.position, gameData.blackRooks[1].position))
                {
                    if (gameData.piecesDict.ContainsValue(gameData.blackRooks[1]) && gameData.piecesDict.ContainsValue(gameData.blackKing))
                    {
                        gameData.blackKing.condition = true;
                        List<int> move = new List<int> { 2, 0 };
                        castleableMovesBlack.Add(move);
                    }
                }
            }
        }

        gameData.whiteKing.conditionalAttacks = make2DArray(castleableMovesWhite);
        gameData.blackKing.conditionalAttacks = make2DArray(castleableMovesBlack);
    }

    static int[,] make2DArray(List<List<int>> listOfLists)
    {
        int rows = listOfLists.Count;
        if (rows == 0)
        {
            return new int[,] { };
        }
        int cols = listOfLists[0].Count;

        int[,] twoDArray = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                twoDArray[i, j] = listOfLists[i][j];
            }
        }

        return twoDArray;
    }

    public static void updatePointsOnBoard(GameObject whitePoints, GameObject blackPoints, GameObject turn, GameObject info)
    {
        gameData.pointsOnBoard = new float[] { 0, 0 };
        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                Piece p = getPieceOnSquare(findSquare(i, j));
                if (p != null)
                {
                    if (p.color == 1)
                    {
                        gameData.pointsOnBoard[0] += p.points;
                    }
                    else if (p.color == -1)
                    {
                        gameData.pointsOnBoard[1] += p.points;
                    }
                }
            }
        }

        updatePointsOnUI(whitePoints, blackPoints, turn, info);
    }

    public static void updatePointsOnUI(GameObject whitePoints, GameObject blackPoints, GameObject turn, GameObject info)
    {
        whitePoints.GetComponent<TextMeshProUGUI>().text = "White: " + gameData.pointsOnBoard[0];
        blackPoints.GetComponent<TextMeshProUGUI>().text = "Black: " + gameData.pointsOnBoard[1];

        String whoseTurn;
        if (gameData.turn == 1)
        {
            whoseTurn = "Whites";
        }
        else
        {
            whoseTurn = "Blacks";
        }
        turn.GetComponent<TextMeshProUGUI>().text = "It's " + whoseTurn + " Turn!";

        if (gameData.checkMate)
        {
            info.GetComponent<TextMeshProUGUI>().text = "Checkmate!";
        }
        else if (gameData.staleMate)
        {
            info.GetComponent<TextMeshProUGUI>().text = "Stalemate!";
        }
        else if (gameData.check)
        {
            info.GetComponent<TextMeshProUGUI>().text = "Check!";
        }
    }

    public static void updateBotMoves() //Update for choose colour
    {
        gameData.botMoves.Clear();

        foreach (Piece piece in gameData.piecesDict.Values)
        {
            if (piece.color == -1)
            {
                List<int[]> moves = addMovesToCurrentMoveableCoords(piece);
                gameData.botMoves.Add(piece, moves);
            }
        }
    }

    public static GameObject generateRandomSquare()
    {
        System.Random random = new System.Random();
        int x = random.Next(1, 9);
        int y = random.Next(1, 9);

        return findSquare(x, y);
    }

    public static int[,] addTo2DArray(int[,] arr, int[] toAdd) {
        int rows = arr.GetLength(0);
        int cols = arr.GetLength(1);

        int[,] resizedArray = new int[rows + 1, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                resizedArray[i, j] = arr[i, j];
            }
        }

        resizedArray[rows, 0] = toAdd[0];
        resizedArray[rows, 1] = toAdd[1];

        return resizedArray;
    }
}
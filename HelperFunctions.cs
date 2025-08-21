using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;
using System.Collections;
using System.Linq;

public class HelperFunctions : MonoBehaviour
{
    [SerializeField] public static string onlineL;
    static bool online;
    public static PhotonView photonView;
    public onlineGame onlineGame;
    //public SidePanelAdjust panel;


    private void Start()
    {
        if (onlineL == "True" || onlineL == "true")
        {
            online = true;
        }
        else
        {
            online = false;
        }

        online = true;
        photonView = onlineGame.photonView;
    }
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

        if (go == null || toAppend == null)
        {
            return;
        }

        go.GetComponent<RectTransform>().SetParent(toAppend.GetComponent<RectTransform>());
        go.transform.position = Vector2.zero;
        go.transform.localPosition = new Vector2((go.transform.position.x + toAppend.GetComponent<RectTransform>().sizeDelta.x / 2), (go.transform.position.y + toAppend.GetComponent<RectTransform>().sizeDelta.y / 2));
    }
    public static GameObject clicked(BaseEventData e)
    {
        PointerEventData pointerEventData = (PointerEventData)e;

        if (gameData.abilitySelected == "Freeze")
        {
            gameData.selected = pointerEventData.pointerPress;
            gameData.selectedPiece = getPieceOnSquare(pointerEventData.pointerPress); //TODO
            tempInfo.tempPiece = gameData.selectedPiece;
        }
        else if (gameData.abilitySelected == "Spawn")
        {
            tempInfo.tempPiece = gameData.selectedPiece;
            gameData.selected = pointerEventData.pointerPress;
            tempInfo.tempSquare = pointerEventData.pointerPress;
        }
        else if (gameData.abilitySelected == "Spit")
        {
            tempInfo.tempPiece = gameData.selectedPiece;
            gameData.selected = pointerEventData.pointerPress;
            tempInfo.tempSquare = pointerEventData.pointerPress;
        }
        else if (gameData.abilitySelected != "")
        {
            //return null;
        }

        if (pointerEventData.eligibleForClick && gameData.selected != pointerEventData.pointerPress)
        {
            gameData.refreshedSinceClick = false;

            gameData.selectedToMove = gameData.selected;
            gameData.selectedToMovePiece = gameData.selectedPiece;
            gameData.selected = pointerEventData.pointerPress;
            tempInfo.tempSquare = pointerEventData.pointerPress;
            gameData.selectedPiece = getPieceOnSquare(pointerEventData.pointerPress);

            Debug.Log("Clicked: (" + gameData.selectedPiece.name + ")" + pointerEventData.pointerPress + " -> " + getPiecesOnSquareBoardGrid(pointerEventData.pointerPress).Count);

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

    public static GameObject clickedSidePanel(BaseEventData e, SidePanelAdjust panel)
    {
        PointerEventData pointerEventData = (PointerEventData)e;

        if (pointerEventData.eligibleForClick && gameData.abilitySelected != "")
        {
            if (pointerEventData.pointerPress.ToString().Contains("Pass"))
            {
                tempInfo.tempPiece = null;
                tempInfo.passed = true;
            }
            else
            {
                Piece piece = findPieceFromPanelCode(pointerEventData.pointerPress.ToString().Split(' ')[0]);
                Debug.Log("Selected " + piece.name + " from panel during ability");
                tempInfo.tempPiece = piece;
            }

            tempInfo.selectedFromPanel = true;
        }
        else if (pointerEventData.eligibleForClick && gameData.selected != pointerEventData.pointerPress)
        {
            gameData.refreshedSinceClick = false;
            //gameData.selectedToMove = gameData.selected;
            Piece piece = findPieceFromPanelCode(pointerEventData.pointerPress.ToString().Split(' ')[0]);
            Debug.Log("Selected " + piece.name + " from panel");
            gameData.selected = findSquare(piece.position[0], piece.position[1]);
            gameData.selectedPiece = piece;
            gameData.selectedFromPanel = true;

            return pointerEventData.pointerPress;
        }
        else if (gameData.selected == pointerEventData.pointerPress)
        {
            gameData.readyToMove = false;
            gameData.isSelected = false;
            resetBoardColours();
            gameData.selectedToMove = null;
            gameData.selectedToMovePiece = null;
            gameData.selected = null;
            gameData.selectedFromPanel = false;
        }
        return null;
    }

    public static GameObject clickedAbility(BaseEventData e, SidePanelAdjust panel, string abilityName)
    {
        if (gameData.abilitySelected != "")
        {
            return null;
        }

        PointerEventData pointerEventData = (PointerEventData)e;

        if (pointerEventData.eligibleForClick && gameData.selected != pointerEventData.pointerPress)
        {
            gameData.readyToMove = false;
            gameData.abilitySelected = abilityName;

            string pieceName = pointerEventData.pointerPress.name.ToString().Contains("-")
            ? pointerEventData.pointerPress.name.ToString().Split('-')[1]
            : "";
            Piece piece = findPieceFromPanelCode(pieceName);
            gameData.selectedPiece = piece;
            tempInfo.tempPiece = piece;
            Debug.Log("clicked: " + pointerEventData.pointerPress.name.ToString());

            gameData.abilityAdvanceNext = true;
            tempInfo.tempCoordSet = null;
            resetBoardColours();

            return pointerEventData.pointerPress;
        }

        return null;
    }

    public static void resetBoardColours()
    {
        Debug.Log("Reset Board Colours");

        gameData.isSelected = false;
        //gameData.selected = null;
        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                Image s = findSquare(i, j).GetComponent<Image>();
                bool frozen = checkStateOnSquare(getPiecesOnSquareBoardGrid(findSquare(i, j)), "Frozen");

                if (frozen)
                {
                    s.color = (Color)(new Color32(189, 222, 236, 255));
                }
                else
                {
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
    }

    public static Piece getPieceOnSquare(GameObject square)
    {
        /*if (square != null && square.transform != null)
        {
            if (square.transform.childCount == 0)
            {
                return null;
            }

            if (gameData.piecesDict.ContainsKey(square.transform.GetChild(0).gameObject))
            {
                return gameData.piecesDict[square.transform.GetChild(0).gameObject];
            }
        }*/

        int[] coords = findCoords(square);

        if (coords[0] < 1 || coords[1] < 1)
        {
            return null;
        }

        if (gameData.boardGrid[coords[0] - 1][coords[1] - 1].Count < 1)
        {
            return null;
        }

        return gameData.boardGrid[coords[0] - 1][coords[1] - 1][0];

        //return null;
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
        if (square == null) return null;

        int[] sq;
        sq = square.name.ToIntArray();

        sq[0] = sq[0] - 48;
        sq[1] = sq[1] - 48;

        return sq;
    }

    public static bool isJumpBouncing(Piece piece, int[] from, int[] to)
    {
        int fromX = from[0];
        int fromY = from[1];
        int toX = to[0];
        int toY = to[1];

        int[] coords = { fromX, fromY };

        int[,] directions = new int[,]
        {
            { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }
        };

        for (int j = 0; j < 4; j++)
        {
            coords[0] = fromX;
            coords[1] = fromY;
            for (int i = 0; i < 14; i++)
            {
                coords[0] = coords[0] + directions[j, 0];
                coords[1] = coords[1] + directions[j, 1];
                int[] newCoords = adjustCoordsForBouncing(piece, coords[0], coords[1]);

                GameObject square = findSquare(newCoords[0], newCoords[1]);
                bool pieceOnSquare = isPieceOnSquare(square);

                if (newCoords[0] == toX && newCoords[1] == toY)
                {
                    return false;
                }

                if (pieceOnSquare)
                {
                    i = 100;
                }
            }
        }

        return true;
    }

    public static bool isJumpPortal(Piece piece, int[] from, int[] to)
    {
        int fromX = from[0];
        int fromY = from[1];
        int toX = to[0];
        int toY = to[1];

        int[][] directions = new int[][]
        {
        new int[] { 1, 0 },  // right
        new int[] {-1, 0 },  // left
        new int[] { 0, 1 },  // up
        new int[] { 0, -1 }, // down
        new int[] { 1, 1 },  // up-right
        new int[] {-1, 1 },  // up-left
        new int[] { 1, -1 }, // down-right
        new int[] {-1, -1 }  // down-left
        };

        bool anyPathFound = false;
        foreach (var dir in directions)
        {
            int x = fromX;
            int y = fromY;
            bool crossedBackRank = false;
            bool jumpedPiece = false;

            for (int step = 0; step < 8; step++)
            {
                x += dir[0];
                y += dir[1];

                if (y == 0 && piece.color == 1 || y == 9 && piece.color == -1) crossedBackRank = true;

                if (x < 1) x = 8;
                if (x > 8) x = 1;
                if (y < 1) y = 8;
                if (y > 8) y = 1;

                if (x == fromX && y == fromY) break;

                if (x == toX && y == toY)
                {
                    GameObject destSquare = findSquare(x, y);
                    if (!(crossedBackRank || jumpedPiece))
                    {
                        anyPathFound = true;
                    }
                }

                GameObject square = findSquare(x, y);
                if (isPieceOnSquare(square))
                {
                    jumpedPiece = true;
                }
            }
        }

        if (anyPathFound)
        {
            return false;
        }
        return true;
    }


    public static bool isJump(Piece piece, int[] from, int[] to)
    {

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

        for (int i = 1; i <= diff - 1; i++)
        {
            GameObject square = findSquare(from[0] + (i * dirX), from[1] + (i * dirY));
            if (square != null && isPieceOnSquare(square))
            {
                List<Piece> onSquare = getPiecesOnSquareBoardGrid(square);
                if (checkState(piece, "Ghost") && isColorNotOnSquare(square, piece.color * -1)
                    || checkStateAllOnSquare(onSquare, "Ghoul-Dematerialized") && isColorNotOnSquare(square, piece.color * -1))
                {
                    continue;
                }

                return true;
            }
        }
        return false;
    }

    public static bool moveComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        if (checkState(piece, "Crowding"))
        {
            if (checkSquareCrowdingEligible(piece, piecesOnSquare))
            {
                return !jump;
            }
        }
        else if (checkState(piece, "Dematerialized"))
        {
            return !jump;
        }

        return !jump && pieceIsNull;
    }
    public static bool moveAndAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        if (checkState(piece, "Crowding"))
        {
            if (checkSquareCrowdingEligible(piece, piecesOnSquare))
            {
                return !jump;
            }
        }
        else if (checkState(piece, "Dematerialized"))
        {
            return !jump;
        }

        return !jump && (pieceIsNull || pieceIsDiffColour);
    }
    public static bool attacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        return !jump && !pieceIsNull && pieceIsDiffColour;
    }
    public static bool oneTimeMovesComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        return !jump && !piece.hasMoved && pieceIsNull;
    }
    public static bool oneTimeMoveAndAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        return !jump && !piece.hasMoved && (pieceIsNull || pieceIsDiffColour);
    }
    public static bool murderousAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        return !jump;
    }
    public static bool conditionalAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        return !jump && piece.condition && pieceIsNull;
    }
    public static bool jumpAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        if (checkState(piece, "Crowding"))
        {
            if (checkSquareCrowdingEligible(piece, piecesOnSquare))
            {
                return true;
            }
        }
        else if (checkState(piece, "Dematerialized"))
        {
            return true;
        }

        return pieceIsNull || pieceIsDiffColour;
    }

    public static int[] adjustCoordsForPortal(Piece piece, int x, int y)
    {
        if (checkState(piece, "Portal"))
        {
            if (x > 8)
            {
                x = x - 8;
            }
            else if (x < 1)
            {
                x = x + 8;
            }

            if (y > 8)
            {
                y = y - 8;
            }
            else if (y < 1)
            {
                y = y + 8;
            }
        }

        return new int[] { x, y };
    }

    public static int[] adjustCoordsForBouncing(Piece piece, int x, int y)
    {
        if (checkState(piece, "Bouncing"))
        {
            if (x > 8)
            {
                x = 8 - (x - 8);
            }
            else if (x < 1)
            {
                x = -x + 2;
            }

            if (y > 8)
            {
                y = 8 - (y - 8);
            }
            else if (y < 1)
            {
                y = -y + 2;
            }
        }

        return new int[] { x, y };
    }


    public static bool isCoordsDifferent(int[] one, int[] two)
    {
        return !(one[0] == two[0] && one[1] == two[1]);
    }

    public static bool isKnightPortalBackRank(Piece piece, int[] old, int[] new_)
    {
        if (!piece.name.Contains("Knight"))
        {
            return false;
        }

        if (piece.color == 1)
        {
            if (new_[1] >= 7 && old[1] <= 2)
            {
                return true;
            }
        }
        else
        {
            if (new_[1] <= 2 && old[1] >= 7)
            {
                return true;
            }
        }

        return false;
    }

    public static void iterateThroughPieceMoves(Func<Piece, bool, bool, bool, List<Piece>, bool> comparator, Piece piece, int[,] moveType, Piece highlightPiece, Color highlightColor, bool check, bool highlight, bool changeValue, List<int[]> allMoves, int color, bool execDummyMove, bool ignoreDisabled)
    {
        if (checkState(piece, "Frozen"))
        {
            return;
        }

        for (int i = 0; i < moveType.GetLength(0); i++)
        {
            //Portal
            int[] oldCoords = new int[] { moveType[i, 0] + piece.position[0], moveType[i, 1] + piece.position[1] };
            int[] coordsP = adjustCoordsForPortal(piece, oldCoords[0], oldCoords[1]);
            int[] coordsB = adjustCoordsForBouncing(piece, oldCoords[0], oldCoords[1]);

            int[] newPos = new int[] { oldCoords[0], oldCoords[1] };

            if (checkState(piece, "Portal"))
            {
                newPos[0] = coordsP[0];
                newPos[1] = coordsP[1];
            }
            else if (checkState(piece, "Bouncing"))
            {
                newPos[0] = coordsB[0];
                newPos[1] = coordsB[1];
            }

            GameObject goHighlight = findSquare(newPos[0], newPos[1]);
            if (goHighlight != null)
            {
                //Piece pieceOnSquare = getPieceOnSquare(goHighlight);
                List<Piece> piecesOnSquare = getPiecesOnSquareBoardGrid(goHighlight);
                //bool pieceIsNull = pieceOnSquare == null;
                bool pieceIsNull = piecesOnSquare == null || piecesOnSquare.Count == 0;
                bool pieceIsDiffColour = false;
                if (!pieceIsNull)
                {
                    //pieceIsDiffColour = pieceOnSquare.color != color;
                    pieceIsDiffColour = !getColorsOnSquare(goHighlight).Contains(piece.color);
                    
                    //TODO: does this work with dematerialized properly?
                    if (checkPiecesDisabled(piecesOnSquare))
                    {
                        pieceIsNull = true;
                    }

                    //Check for states
                    if (checkStateOnSquare(piecesOnSquare, "Shield"))
                    {
                        continue;
                    }

                    if (checkStateOnSquare(piecesOnSquare, "CaptureTheFlag"))
                    {
                        bool _continue = false;
                        foreach (Piece piece_ in piecesOnSquare)
                        {
                            if (checkCaptureTheFlag(piece_))
                            {
                                _continue = true;
                                break;
                            }
                        }

                        if (_continue)
                        {
                            continue;
                        }
                    }
                }

                bool jump = false;

                //Portal Jump && Bouncing Jump
                if (isCoordsDifferent(oldCoords, newPos) && checkState(piece, "Portal"))
                {
                    if (isKnightPortalBackRank(piece, oldCoords, newPos))
                    {
                        continue;
                    }

                    jump = isJumpPortal(piece, piece.position, newPos);
                }
                else if (isCoordsDifferent(oldCoords, newPos) && checkState(piece, "Bouncing"))
                {
                    jump = isJumpBouncing(piece, piece.position, newPos);
                }
                else
                {
                    jump = isJump(piece, piece.position, newPos);
                }

                //Debug.Log(piece.name + " (" + newPos[0] + "," + newPos[1] + ") " + ": " + jump + " " + pieceIsNull + " " + pieceIsDiffColour + " " + piecesOnSquare.Count);
                if (comparator(piece, jump, pieceIsNull, pieceIsDiffColour, piecesOnSquare))
                {
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
        List<Piece> oldPieces = new List<Piece>();
        bool restore = false;


        List<GameObject> gos = new List<GameObject>();

        if (isPieceOnSquare(square))
        {
            oldPieces = new List<Piece>(getPiecesOnSquareBoardGrid(square));
            gos = new List<GameObject>(removePieceFromBoard(oldPieces));
            restore = true;
        }

        piece.setPosition(coords);
        movePieceBoardGrid(piece, new int[] { x, y }, coords);

        //movePieceNoImage(piece, square);

        List<int[]> moves = addToCurrentMoveableCoordsTotal(piece.color * -1, false, false, null, false, true);
        isInCheck = dummyIsCheck(moves, king);

        if (restore)
        {
            restorePieceToBoard(oldPieces, coords, gos);
        }

        movePieceBoardGrid(piece, coords, new int[] { x, y });
        piece.setPosition(new int[] { x, y });
        //movePieceNoImage(piece, findSquare(x, y));

        return isInCheck;
    }

    public static List<GameObject> removePieceFromBoard(List<Piece> pieces)
    {
        List<GameObject> gos = new List<GameObject>();

        foreach (Piece piece in new List<Piece>(pieces))
        {
            piece.disabled = true;

            updateBoardGrid(piece.position, piece, "r");
            //piece.position = new int[] { -1, -1 };

            //GameObject gp = new GameObject();
            //gp.AddComponent<RectTransform>();
            //movePieceNoImage(piece, gp);

            //gos.Add(gp);
        }

        return gos;
    }

    public static void restorePieceToBoard(List<Piece> pieces, int[] position, List<GameObject> gos)
    {
        foreach (Piece piece in new List<Piece>(pieces))
        {
            if (piece != null)
            {
                piece.disabled = false;
                //movePieceNoImage(piece, findSquare(position[0], position[1]));
                //piece.position = position;

                updateBoardGrid(position, piece, "a");
            }
        }

        //foreach (GameObject go in gos)
        //{
        //    DestroyWrapper(go);
        //}
    }

    public static void DestroyWrapper(GameObject go)
    {
        if (online)
        {
            PhotonView pv = go.GetComponent<PhotonView>();

            if (pv == null)
            {
                Debug.LogWarning($"No PhotonView -> {go.name}");
                GameObject.Destroy(go);
                return;
            }

            if (pv.ViewID == 0)
            {
                Debug.LogWarning($"ViewID is 0 -> {go.name}");
                GameObject.Destroy(go);
                return;
            }

            if (pv.IsMine || PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(go);
            }
            else
            {
                Debug.LogWarning($"Not owner -> {go.name}");
            }
        }
        else
        {
            GameObject.Destroy(go);
        }
    }


    public static bool dummyIsCheck(List<int[]> moves, Piece king)
    {
        return isInList(moves, king.getPosition(), false);
    }

    public static bool isCheckMate(Piece king, bool execDummyMove)
    {
        List<int[]> moves = addToCurrentMoveableCoordsTotal(king.color, true, false, null, execDummyMove, true);

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

        if (p.color != 1)
        {
            p.promotingRow = 9 - p.promotingRow;
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

    public static void updatePointsOnBoard(SidePanelAdjust panel)
    {
        gameData.pointsOnBoard = new float[] { 0, 0 };
        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                List<Piece> pieces = getPiecesOnSquareBoardGrid(findSquare(i, j));

                foreach (Piece p in pieces)
                {
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
        }

        updatePointsOnUI(panel);
    }

    public static void updatePointsOnUI(SidePanelAdjust panel)
    {
        panel.whiteCountText.text = $"White: {gameData.pointsOnBoard[0]}";
        panel.blackCountText.text = $"Black: {gameData.pointsOnBoard[1]}";

        //String whoseTurn;
        //if (gameData.turn == 1)
        //{
        //    whoseTurn = "Whites";
        //}
        //else
        //{
        //    whoseTurn = "Blacks";
        //}

        //if (gameData.checkMate)
        //{
        //    //Later
        //}
        //else if (gameData.staleMate)
        //{
        //    //Latewr
        //}
        //else if (gameData.check)
        //{
        //    //Later
        //}
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

    public static int[,] addTo2DArray(int[,] arr, int[] toAdd)
    {
        int rows = arr.GetLength(0);
        int cols = arr.GetLength(1);

        if (rows == 0 && cols == 0)
        {
            int[,] newArray = new int[1, toAdd.Length];
            for (int j = 0; j < toAdd.Length; j++)
            {
                newArray[0, j] = toAdd[j];
            }
            return newArray;
        }

        if (toAdd.Length != cols)
        {
            throw new ArgumentException($"toAdd length ({toAdd.Length}) must match number of columns in arr ({cols}).");
        }

        int[,] resizedArray = new int[rows + 1, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                resizedArray[i, j] = arr[i, j];
            }
        }

        for (int j = 0; j < cols; j++)
        {
            resizedArray[rows, j] = toAdd[j];
        }

        return resizedArray;
    }

    public static void collateralDeath(List<Piece> deadPieces)
    {
        foreach (Piece deadPiece in deadPieces)
        {
            Debug.Log(deadPiece.name + " died to collateral on (" + deadPiece.position[0] + "," + deadPiece.position[1] + ")");
            GameObject dead = deadPiece.go;
            if (deadPiece.lives != 0)
            {
                handleMultipleLivesDeath(deadPiece);

                continue;
            }
            else
            {
                if (gameData.piecesDict.ContainsKey(dead))
                {
                    gameData.piecesDict.Remove(dead);
                }

                updateBoardGrid(deadPiece.position, deadPiece, "r");
                DestroyWrapper(dead);
                deadPiece.alive = 0;
            }
        }
    }

    public static void forceRemove(Piece deadPiece)
    {
        GameObject dead = piece.go;
        if (gameData.piecesDict.ContainsKey(dead))
        {
            gameData.piecesDict.Remove(dead);
        }

        updateBoardGrid(deadPiece.position, deadPiece, "r");
        DestroyWrapper(dead);
        deadPiece.alive = 0;
    }

    public static void handleMultipleLivesDeath(Piece deadPiece)
    {
        deadPiece.lives--;

        if (!isOnStartSquare(deadPiece) && !isPieceOnStartSquare(deadPiece))
        {
            movePieceBoardGrid(deadPiece, deadPiece.position, deadPiece.startSquare);
            //deadPiece.position = deadPiece.startSquare;

            if (online)
            {
                photonView.RPC("_MovePieceRPC", RpcTarget.All, deadPiece.startSquare, deadPiece.position);
            }
            else
            {
                movePiece(deadPiece, findSquare(deadPiece.startSquare[0], deadPiece.startSquare[1]));
            }
        }
        else
        {
            if (gameData.piecesDict.ContainsKey(deadPiece.go))
            {
                gameData.piecesDict.Remove(deadPiece.go);
            }

            updateBoardGrid(deadPiece.position, deadPiece, "r");
            DestroyWrapper(deadPiece.go);
            deadPiece.alive = 0;
        }
    }

    public static void onDeaths(Piece attackerPiece, GameObject attacker, GameObject squareDead)
    {
        List<Piece> pieces = new List<Piece>(getPiecesOnSquareBoardGrid(squareDead));


        foreach (Piece piece in pieces)
        {
            Debug.Log(piece.name + " died on (" + piece.position[0] + "," + piece.position[1] + ")");
            onDeath(piece, piece.go, attackerPiece, attacker);
        }
    }

    public static void onDeath(Piece deadPiece, GameObject dead, Piece attackerPiece, GameObject attacker)
    {
        int[] attackerCoords = attackerPiece.position;
        int[] deadPieceCoords = deadPiece.position;

        bool skipCollateral = false;
        //Infinite / Multi-Lives
        if (deadPiece.lives != 0)
        {
            handleMultipleLivesDeath(deadPiece);

            return;
        }

        //Electric
        if (checkState(deadPiece, "Electric"))
        {
            System.Random rand = new System.Random();
            int randomNumber = rand.Next(1, 3);

            if (randomNumber == 1)
            {
                DestroyWrapper(attacker);
            }
        }

        //Hungry
        if (checkState(attackerPiece, "Hungry"))
        {
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            attackerPiece.storage.Add(deadPiece);
            skipCollateral = true;
            gameData.piecesDict.Remove(dead);
            updateBoardGrid(deadPieceCoords, deadPiece, "r");
            removePieceImageFromBoard(deadPiece);

            return;
        }

        //Spitting
        if (checkState(attackerPiece, "Spitting"))
        {
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            if (attackerPiece.storage.Count < attackerPiece.storageLimit)
            {
                Debug.Log("Consumed " + deadPiece.name + " for spit");
                attackerPiece.storage.Add(deadPiece);
                skipCollateral = true;
                gameData.piecesDict.Remove(dead);
                updateBoardGrid(deadPieceCoords, deadPiece, "r");
                removePieceImageFromBoard(deadPiece);
            }
            else
            {
                List<Piece> piece = pieceToList(deadPiece);
                collateralDeath(piece);
            }

            return;
        }

        if (!skipCollateral)
        {
            //Collateral (Attacker)
            if (attackerPiece.collateralType == 0) //Kill on Capture
            {
                if (isPieceSurroundingState(deadPiece, "Defuser"))
                {
                    collateralDeath(pieceToList(attackerPiece));
                    collateralDeath(pieceToList(deadPiece));
                    return;
                }

                for (int i = 0; i < attackerPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { attackerCoords[0] + attackerPiece.collateral[i, 0], attackerCoords[1] + attackerPiece.collateral[i, 1] };
                    GameObject square = findSquare(coords[0], coords[1]);

                    if (attackerPiece.collateral[i, 0] == 0 && attackerPiece.collateral[i, 1] == 0)
                    {
                        collateralDeath(pieceToList(attackerPiece));
                    }

                    if (!square) continue;

                    List<Piece> pieces = new List<Piece>(getPiecesOnSquareBoardGrid(square));
                    collateralDeath(pieces);
                }
            }

            //Collateral (Attackee)
            if (deadPiece.collateralType == 1)
            {
                if (isPieceSurroundingState(deadPiece, "Defuser"))
                {
                    collateralDeath(pieceToList(attackerPiece));
                    collateralDeath(pieceToList(deadPiece));
                    return;
                }

                for (int i = 0; i < deadPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { deadPieceCoords[0] + deadPiece.collateral[i, 0], deadPieceCoords[1] + deadPiece.collateral[i, 1] };
                    GameObject square = findSquare(coords[0], coords[1]);

                    if (deadPiece.collateral[i, 0] == 0 && deadPiece.collateral[i, 1] == 0)
                    {
                        collateralDeath(pieceToList(attackerPiece));
                        collateralDeath(pieceToList(deadPiece));
                    }

                    if (!square) continue;

                    List<Piece> pieces = new List<Piece>(getPiecesOnSquareBoardGrid(square));
                    collateralDeath(pieces);
                }
            }
        }

        //Only here if piece still dies
        if (gameData.piecesDict.ContainsKey(dead))
        {
            gameData.piecesDict.Remove(dead);
        }

        updateBoardGrid(deadPieceCoords, deadPiece, "r");
        DestroyWrapper(dead);
        deadPiece.alive = 0;
        deadPiece = null;
    }

    [PunRPC]
    public void _MovePieceRPC(int[] toMoveCoords, int[] coords)
    {
        GameObject square = findSquare(toMoveCoords[0], toMoveCoords[1]);
        onlineGame.movePiece(gameData.selectedPiece, coords);
    }

    public static bool isOnStartSquare(Piece piece)
    {
        return piece.startSquare[0] == piece.position[0] && piece.startSquare[1] == piece.position[1];
    }

    public static bool isPieceOnStartSquare(Piece piece)
    {
        return isPieceOnSquare(findSquare(piece.startSquare[0], piece.startSquare[1]));
    }

    public static bool checkState(Piece piece, String state)
    {
        return piece.state == state || piece.secondaryState.Contains(state);
    }

    public static bool checkStateOnSquare(List<Piece> piecesOnSquare, String state)
    {
        if (piecesOnSquare == null)
        {
            return false;
        }

        foreach (Piece piece in piecesOnSquare)
        {
            if (piece.state == state || piece.secondaryState.Contains(state))
            {
                return true;
            }
        }

        return false;
    }

    public static bool checkStateAllOnSquare(List<Piece> piecesOnSquare, string state)
    {
        if (piecesOnSquare == null)
        {
            return false;
        }

        List<string> states = state.Split('-').ToList();
        foreach (Piece piece in piecesOnSquare)
        {
            foreach (string s in states)
            {
                if (piece.state == s || piece.secondaryState.Contains(s))
                {
                    break;
                }
            }

            return false;
        }

        return true;
    }

    public static List<Sprite> generateSidePanelImages(GameObject square)
    {
        List<Sprite> squareImages = new List<Sprite>();

        List<Piece> pieces = getPiecesOnSquareBoardGrid(square);

        if (pieces == null) return null;

        squareImages = generateSidePanelImagesFromList(pieces, false);

        return squareImages;
    }

    public static List<Sprite> generateSidePanelImagesFromList(List<Piece> pieces, bool addPass)
    {
        List<Sprite> squareImages = new List<Sprite>();

        if (pieces == null) return null;

        foreach (Piece piece in pieces)
        {
            String url;
            url = piece.bImage;
            if (piece.color == 1)
            {
                url = piece.wImage;
            }
            byte[] f;
            f = File.ReadAllBytes(url);
            Texture2D t2d = new Texture2D(2, 2);
            t2d.LoadImage(f);
            Sprite s = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f));
            s.name = piece.name;

            squareImages.Add(s);
        }

        if (addPass)
        {
            String url;
            url = "Assets/Resources/Pass.png";
            byte[] f;
            f = File.ReadAllBytes(url);
            Texture2D t2d = new Texture2D(2, 2);
            t2d.LoadImage(f);
            Sprite s = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f));
            s.name = "Pass";

            squareImages.Add(s);
        }

        return squareImages;
    }

    public static List<Piece> getPiecesOnSquare(GameObject square)
    {
        List<Piece> pieces = new List<Piece>();
        if (square != null && square.transform != null)
        {
            if (square.transform.childCount == 0)
            {
                return pieces;
            }

            int[] coords = findCoords(square);
            foreach (Piece piece in gameData.piecesDict.Values)
            {
                if (piece.position[0] == coords[0] && piece.position[1] == coords[1])
                {
                    if (!piece.disabled)
                    {
                        pieces.Add(piece);
                    }
                }
            }
        }

        if (pieces.Count == 0)
        {
            return null;
        }

        return pieces;
    }

    public static List<List<List<Piece>>> initBoardGrid()
    {
        List<List<List<Piece>>> grid = new List<List<List<Piece>>>();
        for (int i = 0; i < 8; i++)
        {
            List<List<Piece>> row = new List<List<Piece>>();

            for (int j = 0; j < 8; j++)
            {
                List<Piece> piecesList = new List<Piece>();
                row.Add(piecesList);
            }

            grid.Add(row);
        }

        return grid;
    }

    public static List<Piece> getPiecesOnSquareBoardGrid(GameObject square)
    {
        List<Piece> pieces = new List<Piece>();
        if (square != null)
        {

            int[] coords = findCoords(square);

            return gameData.boardGrid[coords[0] - 1][coords[1] - 1];
        }

        return pieces;
    }

    public static bool isMultipleOnSquare(GameObject square)
    {
        int[] coords = findCoords(square);

        return gameData.boardGrid[coords[0] - 1][coords[1] - 1].Count > 1;
    }

    public static Piece findPieceFromPanelCode(String panelCode)
    {
        foreach (Piece piece in gameData.piecesDict.Values)
        {
            if (piece.name == panelCode)
            {
                return piece;
            }
        }

        foreach (Piece piece in gameData.allPiecesDict.Values)
        {
            if (piece.name == panelCode)
            {
                return piece;
            }
        }

        return null;
    }

    public static List<int> getColorsOnSquare(GameObject square)
    {
        List<int> colors = new List<int>();

        List<Piece> pieces = getPiecesOnSquareBoardGrid(square);

        if (pieces == null)
        {
            return colors;
        }

        foreach (Piece piece in pieces)
        {
            if (piece.disabled || piece.alive == 0 || checkState(piece, "Dematerialized"))
            {
                continue;
            }

            colors.Add(piece.color);
        }

        return colors;
    }

    public static bool checkPiecesDisabled(List<Piece> pieces)
    {
        foreach (Piece piece in pieces)
        {
            if (!piece.disabled || checkState(piece, "Dematerialized"))
            {
                return false;
            }
        }

        return true;
    }

    //TODO test this
    public static bool checkSquareCrowdingEligible(Piece piece, List<Piece> piecesOnSquare)
    {
        // No Pieces
        if (piecesOnSquare.Count == 0)
        {
            return true;
        }

        // Pieces Different Color
        if (getColorsOnSquare(findSquare(piecesOnSquare[0].position[0], piecesOnSquare[0].position[1])).Contains(piece.color * -1))
        {
            return false;
        }

        // Piece contains more than one other piece (not crowding)
        if (piecesOnSquare.Count > 1 && checkState(piece, "Crowding"))
        {
            foreach (Piece _piece in piecesOnSquare)
            {
                if (!checkState(_piece, "Crowding"))
                {
                    return false;
                }
            }

            // If they are all crowding
            return true;
        }

        //There is one piece on the square, but piece is not crowding
        if (!checkState(piece, "Crowding") && piecesOnSquare.Count == 1 && isColorOnSquare(findSquare(piece.position[0], piece.position[1]), piece.color * 1))
        {
            return true;
        }


        if (!checkState(piece, "Crowding"))
        {
            return false;
        }
        else
        {
            //Last case square contains one piece and its same color
            return true;
        }
    }

    public static void updateBoardGrid(int[] coords, Piece piece, String action)
    {
        if (coords[0] < 1 || coords[1] < 1)
        {
            return;
        }

        var square = gameData.boardGrid[coords[0] - 1][coords[1] - 1];

        if (action.ToLower() == "a" || action.ToLower() == "add")
        {
            bool alreadyExists = square.Any(p => p.name == piece.name);
            if (!alreadyExists)
            {
                square.Add(piece);
            }
        }

        if (action.ToLower() == "r" || action.ToLower() == "remove")
        {
            square.RemoveAll(p => p.name == piece.name);
        }
    }

    public static void movePieceBoardGrid(Piece piece, int[] position, int[] coords)
    {
        if (position[0] < 1 || position[1] < 1 || coords[0] < 1 || coords[1] < 1)
        {
            return;
        }

        updateBoardGrid(position, piece, "r");
        gameData.boardGrid[coords[0] - 1][coords[1] - 1].Add(piece);

        piece.position = coords;
    }

    public static IEnumerator waitOneFrame()
    {
        yield return null;
    }

    public static bool isPieceOnSquare(GameObject square)
    {
        if (square == null) return true;

        int[] coords = findCoords(square);

        if (coords[1] < 1 || coords[0] < 1)
        {
            return false;
        }

        return gameData.boardGrid[coords[0] - 1][coords[1] - 1].Count > 0;
    }

    public static bool isColorNotOnSquare(GameObject square, int color)
    {
        var colors = getColorsOnSquare(square);

        return !colors.Contains(color);
    }

    public static bool isColorOnSquare(GameObject square, int color)
    {
        var colors = getColorsOnSquare(square);

        return colors.Contains(color);
    }

    public static GameObject hungryPieceNextBarf(Piece hungryPiece, ref List<int[]> collateral)
    {
        if (collateral == null)
        {
            collateral = new List<int[]>
            {
                new int[] { 1, 1 },
                new int[] { 1, 0 },
                new int[] { 1, -1 },
                new int[] { 0, -1 },
                new int[] { -1, -1 },
                new int[] { -1, 0 },
                new int[] { -1, 1 },
                new int[] { 0, 1 }
            };
        }

        if (collateral.Count == 0)
        {
            return null;
        }

        int[] coord = null;
        foreach (int[] coords in new List<int[]>(collateral))
        {
            collateral.Remove(coords);

            GameObject square = findSquare(coords[0] + hungryPiece.position[0], coords[1] + hungryPiece.position[1]);
            if (square == null) { continue; }

            if (!isPieceOnSquare(square))
            {
                coord = new int[] { coords[0] + hungryPiece.position[0], coords[1] + hungryPiece.position[1] };
                break;
            }
        }

        if (coord == null)
        {
            return null;
        }

        return findSquare(coord[0], coord[1]);
    }

    public static List<Piece> pieceToList(Piece piece)
    {
        List<Piece> pieceList = new List<Piece>();
        pieceList.Add(piece);

        return pieceList;
    }

    public static void highlightSquare(GameObject square, Color color)
    {
        if (square == null) return;

        square.GetComponent<Image>().color = color;
    }

    public static void removePieceImageFromBoard(Piece piece)
    {
        GameObject go = piece.go;

        //go.transform.parent = null;
        go.SetActive(false);
    }

    public static void restorePieceImageToBoard(Piece piece)
    {
        GameObject go = piece.go;

        go.SetActive(true);
    }

    public static bool checkCaptureTheFlag(Piece piece)
    {
        if (piece.color == 1)
        {
            if (piece.position[1] <= 2)
            {
                return true;
            }
        }
        else
        {
            if (piece.position[1] >= 7)
            {
                return true;
            }
        }

        return false;
    }

    public static bool checkCanCastle(int color, int direction)
    {
        Piece king;
        Piece rook;
        GameObject kingSquare;
        GameObject rookSquare;

        if (color == 1)
        {
            king = findPieceFromPanelCode("w_k1");
            if (direction == -1)
            {
                rook = findPieceFromPanelCode("w_r1");
            }
            else
            {
                rook = findPieceFromPanelCode("w_r2");
            }
        }
        else
        {
            king = findPieceFromPanelCode("b_k1");
            if (direction == -1)
            {
                rook = findPieceFromPanelCode("b_r1");
            }
            else
            {
                rook = findPieceFromPanelCode("b_r2");
            }
        }

        if (king == null || rook == null)
        {
            return false;
        }


        if (king.alive == 1 && rook.alive == 1 && !king.hasMoved && !rook.hasMoved)
        {
            kingSquare = findSquare(king.position[0], king.position[1]);
            rookSquare = findSquare(rook.position[0], rook.position[1]);

            if (getPiecesInBetweenSquaresHorizontal(kingSquare, rookSquare).Count > 0)
            {
                return false;
            }

            if (getPiecesOnSquareBoardGrid(kingSquare).Count != 1 || getPiecesOnSquareBoardGrid(rookSquare).Count != 1)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public static List<Piece> getPiecesInBetweenSquaresHorizontal(GameObject s1, GameObject s2)
    {
        if (findCoords(s1)[1] != findCoords(s2)[1])
        {
            return new List<Piece>();
        }

        int y = findCoords(s1)[1];
        List<Piece> pieces = new List<Piece>();

        int x1 = findCoords(s1)[0];
        int x2 = findCoords(s2)[0];
        int dir = (x1 - x2) / Math.Abs(x1 - x2);


        for (int i = x2 + dir; i != x1; i += dir)
        {
            GameObject sq = findSquare(i, y);

            pieces.AddRange(getPiecesOnSquareBoardGrid(sq));
        }

        return pieces;
    }

    public static String findNextAvailablePanelCode(String panelCode)
    {
        String color;
        char type;
        int number;

        String[] parts = panelCode.Split("_");
        color = parts[0];
        type = parts[1][0];

        number = Int32.Parse(parts[1].Substring(1));

        bool found = false;
        while (!found)
        {
            number += 1;
            String str = color + "_" + type + number.ToString();
            if (!gameData.panelCodes.Contains(str))
            {
                return str;
            }
        }

        return null;
    }

    public static bool isPieceSurrounding(Piece piece)
    {
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },  // right
            new int[] {-1, 0 },  // left
            new int[] { 0, 1 },  // up
            new int[] { 0, -1 }, // down
            new int[] { 1, 1 },  // up-right
            new int[] {-1, 1 },  // up-left
            new int[] { 1, -1 }, // down-right
            new int[] {-1, -1 }  // down-left
        };

        foreach (var dir in directions)
        {
            int x = piece.position[0] + dir[0];
            int y = piece.position[1] + dir[1];

            if (getPiecesOnSquareBoardGrid(findSquare(x, y)).Count > 0)
            {
                return true;
            }
        }

        return false;
    }

    public static void highlightSurroundingSquaresWithPieces(Piece piece)
    {
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },  // right
            new int[] {-1, 0 },  // left
            new int[] { 0, 1 },  // up
            new int[] { 0, -1 }, // down
            new int[] { 1, 1 },  // up-right
            new int[] {-1, 1 },  // up-left
            new int[] { 1, -1 }, // down-right
            new int[] {-1, -1 }  // down-left
        };

        foreach (var dir in directions)
        {
            int x = piece.position[0] + dir[0];
            int y = piece.position[1] + dir[1];

            if (getPiecesOnSquareBoardGrid(findSquare(x, y)).Count > 0)
            {
                highlightSquare(findSquare(x, y), Color.red);
            }
        }
    }

    public static void highlightSurroundingSquaresWithoutPieces(Piece piece)
    {
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },  // right
            new int[] {-1, 0 },  // left
            new int[] { 0, 1 },  // up
            new int[] { 0, -1 }, // down
            new int[] { 1, 1 },  // up-right
            new int[] {-1, 1 },  // up-left
            new int[] { 1, -1 }, // down-right
            new int[] {-1, -1 }  // down-left
        };

        foreach (var dir in directions)
        {
            int x = piece.position[0] + dir[0];
            int y = piece.position[1] + dir[1];

            if (getPiecesOnSquareBoardGrid(findSquare(x, y)).Count == 0)
            {
                highlightSquare(findSquare(x, y), Color.red);
            }
        }
    }

    public static bool isPieceSurroundingColor(Piece piece, int color)
    {
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },  // right
            new int[] {-1, 0 },  // left
            new int[] { 0, 1 },  // up
            new int[] { 0, -1 }, // down
            new int[] { 1, 1 },  // up-right
            new int[] {-1, 1 },  // up-left
            new int[] { 1, -1 }, // down-right
            new int[] {-1, -1 }  // down-left
        };

        foreach (var dir in directions)
        {
            int x = piece.position[0] + dir[0];
            int y = piece.position[1] + dir[1];

            if (getPiecesOnSquareBoardGrid(findSquare(x, y)).Count > 0 && isColorOnSquare(findSquare(x, y), color))
            {
                return true;
            }
        }

        return false;
    }

    public static void addState(Piece piece, String state)
    {
        if (piece.state == "" || piece.state == null)
        {
            piece.state = state;
        }
        else
        {
            if (piece.secondaryState == "" || piece.secondaryState == null)
            {
                piece.secondaryState = state;
            }
            else
            {
                piece.secondaryState = piece.secondaryState + "-" + state;
            }
        }
    }

    public static void addAbility(Piece piece, String ability)
    {
        if (piece.ability == "" || piece.ability == null)
        {
            piece.ability = ability;
        }
        else
        {
            piece.ability = piece.ability + "-" + ability;
        }
    }

    public static bool areSurroundingSquaresFull(Piece piece)
    {
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },  // right
            new int[] {-1, 0 },  // left
            new int[] { 0, 1 },  // up
            new int[] { 0, -1 }, // down
            new int[] { 1, 1 },  // up-right
            new int[] {-1, 1 },  // up-left
            new int[] { 1, -1 }, // down-right
            new int[] {-1, -1 }  // down-left
        };

        foreach (var dir in directions)
        {
            int x = piece.position[0] + dir[0];
            int y = piece.position[1] + dir[1];

            if (getPiecesOnSquareBoardGrid(findSquare(x, y)).Count < 0)
            {
                return false;
            }
        }

        return true;
    }

    public static class Spawnables
    {
        public static Piece create(string pieceName)
        {
            switch (pieceName)
            {
                case "King": return new King(1, online);
                case "Queen": return new Queen(1, online);
                case "Rook": return new Rook(1, online);
                case "Knight": return new Knight(1, online);
                case "Bishop": return new Bishop(1, online);
                case "Pawn": return new Pawn(1, online);
                case "ZombiePawn": return new ZombiePawn(1, online);
                case "SuperPawn": return new SuperPawn(1, online);
                default: throw new ArgumentException("Bad Piece");
            }
        }
    }

    public static void removeState(Piece piece, string state)
    {
        piece.state = piece.state.Replace("-" + state, "");
        piece.state = piece.state.Replace(state, "");
        piece.secondaryState = piece.secondaryState.Replace("-" + state, "");
        piece.secondaryState = piece.secondaryState.Replace(state, "");
    }

    public static void removeAbility(Piece piece, string ability)
    {

        piece.ability = piece.ability.Replace("-" + ability, "");
        piece.ability = piece.ability.Replace(ability, "");
    }

    public static Dictionary<Piece, List<string>> getAllEligibleAbilities(int color)
    {
        Dictionary<Piece, List<string>> pieceAbilities = new Dictionary<Piece, List<string>>();

        foreach (Piece piece in gameData.piecesDict.Values)
        {
            if (piece.color != color)
            {
                continue;
            }
            
            string[] abilityNames = piece.ability.Split("-");
            List<string> abilities = new List<string>();

            foreach (string abilityName in abilityNames)
            {
                if (abilityName == "Vomit")
                {
                    if (piece.storage != null && piece.storage.Count < 1)
                    {
                        continue;
                    }
                    else if (piece.storage == null)
                    {
                        continue;
                    }
                }
                else if (abilityName == "CastleLeft")
                {
                    if (!checkCanCastle(color, -1))
                    {
                        continue;
                    }
                }
                else if (abilityName == "CastleRight")
                {
                    if (!checkCanCastle(color, 1))
                    {
                        continue;
                    }
                }
                else if (abilityName == "Unfreeze")
                {
                    if (!checkState(piece, "Frozen"))
                    {
                        continue;
                    }
                }
                else if (abilityName == "Freeze")
                {
                    if (!isPieceSurroundingColor(piece, piece.color * -1))
                    {
                        continue;
                    }
                }
                else if (abilityName == "Spawn")
                {
                    if (piece.numSpawns <= 0)
                    {
                        continue;
                    }

                    if (!areSurroundingSquaresFull(piece))
                    {
                        continue;
                    }
                }
                else if (abilityName == "Spit")
                {
                    if (piece.storage.Count <= 0)
                    {
                        continue;
                    }
                }
                else if (abilityName == "Dematerialize")
                {
                    if (piece.state == "Dematerialized")
                    {
                        continue;
                    }
                }
                else if (abilityName == "Materialize")
                {
                    if (piece.state != "Dematerialized")
                    {
                        continue;
                    }
                }

                abilities.Add(abilityName);
            }

            pieceAbilities.Add(piece, abilities);
            abilities.Clear();
        }

        return pieceAbilities;
    }

    public static bool isPieceSurroundingState(Piece piece, string state)
    {
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },  // right
            new int[] {-1, 0 },  // left
            new int[] { 0, 1 },  // up
            new int[] { 0, -1 }, // down
            new int[] { 1, 1 },  // up-right
            new int[] {-1, 1 },  // up-left
            new int[] { 1, -1 }, // down-right
            new int[] {-1, -1 },  // down-left
            new int[] { 0, 0 }  // on
        };

        foreach (var dir in directions)
        {
            int x = piece.position[0] + dir[0];
            int y = piece.position[1] + dir[1];

            if (checkStateOnSquare(getPiecesOnSquare(findSquare(x, y)), state))
            {
                return true;
            }
        }

        return false;
    }
}

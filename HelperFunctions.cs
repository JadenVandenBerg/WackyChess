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
using System.Reflection;


public class HelperFunctions : MonoBehaviour
{
    public string onlineL;
    static bool online;
    public static PhotonView photonView;
    public onlineGame onlineGame;
    public SidePanelAdjust panel;
    public GameObject checkmateUI;
    public AudioSource moveSound;
    public string isBotMatch;
    public static string botMatch;

    private void Start()
    {
        if (onlineL == "True" || onlineL == "true")
        {
            online = true;
            photonView = onlineGame.photonView;
        }
        else
        {
            online = false;
        }

        botMatch = isBotMatch;
        gameData.isBotMatch = botMatch == "True";

        //panel.Initialize();
        //updatePointsOnBoard();

        moveSound = GetComponent<AudioSource>();
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
            if (online) go.AddComponent<PhotonView>();
        }
    }

    public static GameObject clicked(BaseEventData e)
    {
        PointerEventData pointerEventData = (PointerEventData)e;

        if (gameData.abilitySelected == PieceAbilities.Freeze)
        {
            gameData.selected = pointerEventData.pointerPress;
            gameData.selectedPiece = getPieceOnSquare(pointerEventData.pointerPress); //TODO maybe
            tempInfo.tempPiece = gameData.selectedPiece;
        }
        else if (gameData.abilitySelected == PieceAbilities.Spawn)
        {
            tempInfo.tempPiece = gameData.selectedPiece;
            gameData.selected = pointerEventData.pointerPress;
            tempInfo.tempSquare = pointerEventData.pointerPress;
        }
        else if (gameData.abilitySelected == PieceAbilities.Spit)
        {
            tempInfo.tempPiece = gameData.selectedPiece;
            gameData.selected = pointerEventData.pointerPress;
            tempInfo.tempSquare = pointerEventData.pointerPress;
        }
        else if (gameData.abilitySelected == PieceAbilities.Split)
        {
            tempInfo.tempPiece = gameData.selectedPiece;
            gameData.selected = pointerEventData.pointerPress;
            tempInfo.tempSquare = pointerEventData.pointerPress;
        }
        else if (gameData.abilitySelected != PieceAbilities.None)
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

        if (pointerEventData.eligibleForClick && gameData.abilitySelected != PieceAbilities.None)
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
            gameData.selected = findSquare(piece.position.x, piece.position.y);
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

    public static GameObject clickedAbility(BaseEventData e, SidePanelAdjust panel, PieceAbilities abilityName)
    {
        if (gameData.abilitySelected != PieceAbilities.None)
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
        //Debug.Log("Reset Board Colours");

        gameData.isSelected = false;
        //gameData.selected = null;
        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                GameObject sq = findSquare(i, j);

                if (sq == null)
                {
                    return;
                }

                Image s = sq.GetComponent<Image>();

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

        coords coords = findCoords(square);

        if (coords.x < 1 || coords.y < 1)
        {
            return null;
        }

        if (gameData.boardGrid[coords.x - 1][coords.y - 1].Count < 1)
        {
            return null;
        }

        return gameData.boardGrid[coords.x - 1][coords.y - 1][0];

        //return null;
    }

    public static List<coords> addMovesToCurrentMoveableCoords(Piece piece)
    {
        clearCurrentMoveableCoords();
        if (piece == null)
        {
            return null;
        }

        List<coords> allMoves = new List<coords>();

        int color = piece.color;

        bool check = false;
        gameData.isInCheck[0] = isCheck(gameData.whiteKing) == true ? 1 : 0;
        gameData.isInCheck[1] = isCheck(gameData.blackKing) == true ? 1 : 0;
        if (color == 1 && gameData.isInCheck[0] == 1 || color == -1 && gameData.isInCheck[1] == 1)
        {
            check = true;
        }

        iterateThroughPieceMoves(moveComparator, piece, piece.moves, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.moveAndAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        iterateThroughPieceMoves(attacksComparator, piece, piece.attacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        iterateThroughPieceMoves(oneTimeMovesComparator, piece, piece.oneTimeMoves, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        iterateThroughPieceMoves(oneTimeMoveAndAttacksComparator, piece, piece.oneTimeMoveAndAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        iterateThroughPieceMoves(murderousAttacksComparator, piece, piece.murderousAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        iterateThroughPieceMoves(conditionalAttacksComparator, piece, piece.conditionalAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        iterateThroughPieceMoves(jumpAttacksComparator, piece, piece.jumpAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);

        //Dependent Attacks
        //piece.dependentMovesSet();
        //iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.dependentAttacks, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);

        updatePieceFlags(piece, check);

        //Flag Moves
        if (piece.flag == 1)
        {
            iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.flagMove1, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        }
        else if (piece.flag == 2)
        {
            iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.flagMove2, null, Color.red, check, false, false, gameData.currentMoveableCoords, color, true, false, false);
        }

        //todo use allMoves instead
        foreach (coords move in gameData.currentMoveableCoords)
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

        if (gameData.board == null)
        {
            return null;
        }

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

    public static coords findCoords(GameObject square)
    {
        if (square == null) return new coords(-1, -1);

        int[] sq;
        sq = square.name.ToIntArray();

        sq[0] = sq[0] - 48;
        sq[1] = sq[1] - 48;

        return new coords(sq[0], sq[1]);
    }

    public static bool isJumpBouncing(Piece piece, coords from, coords to)
    {
        int fromX = from.x;
        int fromY = from.y;
        int toX = to.x;
        int toY = to.y;

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
                coords[0] = fromX + directions[j, 0] * (i + 1);
                coords[1] = fromY + directions[j, 1] * (i + 1);
                coords newCoords = adjustCoordsForBouncing(piece, coords[0], coords[1]);

                GameObject square = findSquare(newCoords.x, newCoords.y);
                bool pieceOnSquare = isPieceOnSquare(square, false);

                if (newCoords.x == toX && newCoords.y == toY)
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

    public static bool isJumpPortal(Piece piece, coords from, coords to)
    {
        int fromX = from.x;
        int fromY = from.y;
        int toX = to.x;
        int toY = to.y;

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
            int x = fromX;
            int y = fromY;

            for (int step = 0; step <= 8; step++)
            {
                x += dir[0];
                y += dir[1];

                if (y == 0 && piece.color == 1 || y == 9 && piece.color == -1) break;

                if (x < 1) x = 8;
                if (x > 8) x = 1;
                if (y < 1) y = 8;
                if (y > 8) y = 1;

                if (x == fromX && y == fromY) break;

                if (x == toX && y == toY)
                {
                    return false;
                }

                GameObject square = findSquare(x, y);
                if (isPieceOnSquare(square, false))
                {
                    break;
                }
            }
        }

        return true;
    }


    public static bool isJump(Piece piece, coords from, coords to)
    {

        int dirX, dirY;

        if (from.x > to.x)
        {
            dirX = -1;
        }
        else if (from.x == to.x)
        {
            dirX = 0;
        }
        else
        {
            dirX = 1;
        }

        if (from.y > to.y)
        {
            dirY = -1;
        }
        else if (from.y == to.y)
        {
            dirY = 0;
        }
        else
        {
            dirY = 1;
        }

        int diff = Mathf.Abs(from.x - to.x);
        if (Mathf.Abs(from.y - to.y) > diff)
        {
            diff = Mathf.Abs(from.y - to.y);
        }

        for (int i = 1; i <= diff - 1; i++)
        {
            GameObject square = findSquare(from.x + (i * dirX), from.y + (i * dirY));
            if (square != null && isPieceOnSquare(square, false))
            {
                List<Piece> onSquare = getPiecesOnSquareBoardGrid(square);
                if (checkState(piece, PieceState.Ghost) && isColorNotOnSquare(square, piece.color * -1)
                    || checkStateAllOnSquare(onSquare, PieceState.Ghoul | PieceState.Dematerialized) && isColorNotOnSquare(square, piece.color * -1))
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
        if (checkState(piece, PieceState.Crowding) || checkState(piece, PieceState.Jockey))
        {
            if (checkSquareCrowdingEligible(piece, piecesOnSquare))
            {
                return !jump;
            }
        }
        else if (checkState(piece, PieceState.Dematerialized))
        {
            return true;
        }

        return !jump && pieceIsNull;
    }
    public static bool moveAndAttacksComparator(Piece piece, bool jump, bool pieceIsNull, bool pieceIsDiffColour, List<Piece> piecesOnSquare)
    {
        if (checkState(piece, PieceState.Crowding) || checkState(piece, PieceState.Jockey))
        {
            if (checkSquareCrowdingEligible(piece, piecesOnSquare))
            {
                return !jump;
            }
        }
        else if (checkState(piece, PieceState.Dematerialized))
        {
            return !jump;
        }
        else if (checkState(piece, PieceState.Feminist) && checkPieceTypeFromList(piecesOnSquare, "q"))
        {
            return false;
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
        if (checkState(piece, PieceState.Crowding) || checkState(piece, PieceState.Jockey))
        {
            if (checkSquareCrowdingEligible(piece, piecesOnSquare))
            {
                return true;
            }
        }
        else if (checkState(piece, PieceState.Dematerialized))
        {
            return true;
        }

        return pieceIsNull || pieceIsDiffColour;
    }

    public static coords adjustCoordsForPortal(Piece piece, int x, int y)
    {
        if (checkState(piece, PieceState.Portal))
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

        return new coords( x, y );
    }

    public static coords adjustCoordsForBouncing(Piece piece, int x, int y)
    {
        if (checkState(piece, PieceState.Bouncing))
        {
            for (int i = 0; i < 4; i++)
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
        }

        return new coords( x, y );
    }


    public static bool isCoordsDifferent(coords one, coords two)
    {
        return !(one.x == two.x && one.y == two.y);
    }

    public static bool isKnightPortalBackRank(Piece piece, coords old, coords new_)
    {
        if (!piece.name.Contains("n"))
        {
            return false;
        }

        if (piece.color == 1)
        {
            if (new_.y >= 7 && old.y <= 2)
            {
                return true;
            }
        }
        else
        {
            if (new_.y <= 2 && old.y >= 7)
            {
                return true;
            }
        }

        return false;
    }

    public static bool isKnightPortalBackRank_(Piece piece, int oldX, int oldY, int newX, int newY)
    {
        if (!piece.name.Contains("n"))
        {
            return false;
        }

        if (piece.color == 1)
        {
            if (newY >= 7 && oldY <= 2)
            {
                return true;
            }
        }
        else
        {
            if (newY <= 2 && oldY >= 7)
            {
                return true;
            }
        }

        return false;
    }

    /*todo rewrite this for isolated boardstate*/
    public static void iterateThroughPieceMoves(Func<Piece, bool, bool, bool, List<Piece>, bool> comparator, Piece piece, coords[] moveType, Piece highlightPiece, Color highlightColor, bool check, bool highlight, bool changeValue, List<coords> allMoves, int color, bool execDummyMove, bool ignoreDisabled, bool fromTotal)
    {
        if (checkState(piece, PieceState.Frozen) || checkState(piece, PieceState.Jailed))
        {
            return;
        }

        /*if (fromTotal && (checkState(piece, "Jailer"))) {
            return;
        }*/

        if (checkPieceType(piece, "q")) {
            if (isOppressorOnBoard(piece.color))
            {
                return;
            }
        }

        for (int i = 0; i < moveType.GetLength(0); i++)
        {
            //Portal
            coords oldCoords = new coords(moveType[i].x + piece.position.x, moveType[i].y + piece.position.y);
            coords coordsP = adjustCoordsForPortal(piece, oldCoords.x, oldCoords.y);
            coords coordsB = adjustCoordsForBouncing(piece, oldCoords.x, oldCoords.y);

            coords newPos = new coords(oldCoords.x, oldCoords.y);

            if (checkState(piece, PieceState.Portal))
            {
                newPos.x = coordsP.x;
                newPos.y = coordsP.y;
            }
            else if (checkState(piece, PieceState.Bouncing))
            {
                newPos.x = coordsB.x;
                newPos.y = coordsB.y;
            }

            GameObject goHighlight = findSquare(newPos.x, newPos.y);
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
                    pieceIsDiffColour = !getColorsOnSquare(goHighlight, true).Contains(piece.color);

                    //if there is a jailer with a jailed piece, count it
                    if ((checkStateOnSquare(piecesOnSquare, PieceState.Jailer) && checkStateOnSquare(piecesOnSquare, PieceState.Jailed)
                        || checkStateOnSquare(piecesOnSquare, PieceState.Jailed) && checkStateOnSquare(piecesOnSquare, PieceState.Crook)))
                    {
                        pieceIsDiffColour = true;
                    }

                    if (checkPiecesDisabled(piecesOnSquare))
                    {
                        pieceIsNull = true;
                    }

                    if (checkSquareCrowdingEligible(piece, piecesOnSquare))
                    {
                        pieceIsNull = true;
                    }

                    //Check for states
                    if (checkStateOnSquare(piecesOnSquare, PieceState.Shield))
                    {
                        continue;
                    }

                    if (checkStateOnSquare(piecesOnSquare, PieceState.CaptureTheFlag))
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

                    /*if (checkStateAllOnSquare(piecesOnSquare, "Dematerialized"))
                    {
                        continue;
                    }*/
                }

                bool jump = false;

                //Portal Jump && Bouncing Jump
                if (isCoordsDifferent(oldCoords, newPos) && checkState(piece, PieceState.Portal))
                {
                    if (isKnightPortalBackRank(piece, oldCoords, newPos))
                    {
                        continue;
                    }

                    jump = isJumpPortal(piece, piece.position, newPos);
                }
                else if (isCoordsDifferent(oldCoords, newPos) && checkState(piece, PieceState.Bouncing))
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
                        bool stillInCheck = dummyMove_(piece, newPos);

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

    public static List<coords> addToCurrentMoveableCoordsTotal(int color, bool changeValue, bool highlight, Piece highlightPiece, bool execDummyMove, bool ignoreDisabled)
    {
        bool fromTotal = true;
        gameData.currentMoveableCoordsAllPieces.Clear();

        if (highlightPiece != null)
        {
            clearCurrentMoveableCoords();
            resetBoardColours();
            gameData.isSelected = true;

            fromTotal = false;
            addMovesToCurrentMoveableCoords(highlightPiece);
        }

        bool check = false;
        if (color == 1 && gameData.isInCheck[0] == 1 || color == -1 && gameData.isInCheck[1] == 1)
        {
            check = true;
        }

        List<coords> allMoves = new List<coords>();
        Color highlightColor = Color.red;

        foreach (Piece piece in gameData.piecesDict.Values)
        {
            if (piece == null || piece.color != color || piece.disabled)
            {
                continue;
            }

            //if (color == -1) Debug.Log("LOOKING AT " + piece.name + "! Color: " + piece.color);

            //Moves
            iterateThroughPieceMoves(moveComparator, piece, piece.moves, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //Moves and Attacks
            iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.moveAndAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //Attacks
            iterateThroughPieceMoves(attacksComparator, piece, piece.attacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //One Time Moves
            iterateThroughPieceMoves(oneTimeMovesComparator, piece, piece.oneTimeMoves, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //One Time Move and Attacks
            iterateThroughPieceMoves(oneTimeMoveAndAttacksComparator, piece, piece.oneTimeMoveAndAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //Murderous Attacks
            iterateThroughPieceMoves(murderousAttacksComparator, piece, piece.murderousAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //Conditional Attacks
            iterateThroughPieceMoves(conditionalAttacksComparator, piece, piece.conditionalAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //Jump Attacks
            iterateThroughPieceMoves(jumpAttacksComparator, piece, piece.jumpAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //Dependent Attacks
            //piece.dependentMovesSet();
            //iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.dependentAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //Interactive Moves
            //piece.interactiveMovesSet();
            //iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.interactiveAttacks, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);

            //Flag Moves

            updatePieceFlags(piece, check);
            if (piece.flag == 1)
            {
                iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.flagMove1, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);
            }
            else if (piece.flag == 2)
            {
                iterateThroughPieceMoves(moveAndAttacksComparator, piece, piece.flagMove2, highlightPiece, highlightColor, check, highlight, changeValue, allMoves, color, execDummyMove, ignoreDisabled, fromTotal);
            }

        }

        return allMoves;
    }

    public static bool isInList(List<coords> list, coords compare, bool debug)
    {
        //if (debug) Debug.Log("Search Position: " + compare[0] + "," + compare[1]);
        for (int i = 0; i < list.Count; i++)
        {
            //if (debug) Debug.Log("" + list[i][0] + " : " + list[i][1]);
            if (compare.x == list[i].x && compare.y == list[i].y)
            {
                return true;
            }
        }

        return false;
    }

    public static bool isCheck(Piece king)
    {
        //Debug.Log("Searching for Check. Color: " + king.color);
        List<coords> moves = addToCurrentMoveableCoordsTotal(king.color * -1, false, false, null, false, true);
        //Debug.Log("CHECK SEARCH END");
        bool check = isInList(moves, king.position, false);

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

    public static bool isCheck_(Piece king, BoardState afterBS) 
    {
        List<Piece> pieces = BotHelperFunctions.getPiecesOnBoardState(afterBS, king.color * -1);

        foreach (Piece p in pieces)
        {
            if (checkState(p, PieceState.Delayed) || checkState(p, PieceState.Jailer) || checkState(p, PieceState.Dematerialized))
            {
                continue;
            }

            List<coords> moves = BotHelperFunctions.getIsolatedStatePieceAttacks(p, afterBS, false, false);

            if (isInList(moves, king.position, false)) {
                return true;
            }
        }
        return false;
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

    public static bool dummyMove_(Piece piece, coords coords)
    {
        BoardState bs = new BoardState();
        bs.refresh(BotHelperFunctions.convertBoardGrid(gameData.boardGrid));

        if (gameData.whiteKing == null || gameData.blackKing == null)
        {
            return true;
        }

        bs = BotHelperFunctions.copyBoardState(bs);
        piece = BotHelperFunctions.getCloneFromOriginalPiece(piece, bs.boardGrid);

        if (piece == null)
        {
            return true;
        }

        Piece whiteKingClone = BotHelperFunctions.getCloneFromOriginalPiece(gameData.whiteKing, bs.boardGrid);
        Piece blackKingClone = BotHelperFunctions.getCloneFromOriginalPiece(gameData.blackKing, bs.boardGrid);

        BoardState afterBS = BotHelperFunctions.simulatePieceMove_(bs, piece, coords, piece.color, whiteKingClone, blackKingClone);

        if (afterBS == null)
        {
            BotHelperFunctions.resetPiecePositions(null, BotHelperFunctions.convertBoardGrid(gameData.boardGrid));
            return true;
        }

        List<Piece>[,] afterBoardGrid_ = afterBS.boardGrid;

        Piece ogKing = piece.color == 1 ? gameData.whiteKing : gameData.blackKing;
        Piece king = BotHelperFunctions.getCloneFromOriginalPiece(ogKing, afterBS.boardGrid);

        //Piece ogKing = piece.color == 1 ? gameData.whiteKing : gameData.blackKing;
        //Piece king = BotHelperFunctions.getCloneFromOriginalPiece(ogKing, afterBoardGrid_);
        if (king == null)
        {
            BotHelperFunctions.resetPiecePositions(afterBS, BotHelperFunctions.convertBoardGrid(gameData.boardGrid));
            return true;
        }

        bool check = isCheck_(king, afterBS);
        BotHelperFunctions.resetPiecePositions(afterBS, BotHelperFunctions.convertBoardGrid(gameData.boardGrid));
        return check;

    }

    public static List<BotHelperFunctions.PieceAbility> isCheckPieceAbilities(BoardState bs, BotTemplate bot, int color)
    {
        bs = BotHelperFunctions.copyBoardState(bs);

        List<BotHelperFunctions.PieceAbility> pieceAbilities = BotHelperFunctions.getAllPossibleBotAbilities(bot, bs, color);

        List<BotHelperFunctions.PieceAbility> acceptedAbilities = new List<BotHelperFunctions.PieceAbility>();
        foreach(BotHelperFunctions.PieceAbility pa in pieceAbilities)
        {
            Piece piece = pa.piece;
            //BoardState bs = new BoardState();

            //BotHelperFunctions.PieceAbility pa_ = normalizePieceAbility(pa, bs.boardGrid);

            BoardState afterBS = BotHelperFunctions.simulatePieceAbility(null, bs, pa);

            Piece ogKing = piece.color == 1 ? gameData.whiteKing : gameData.blackKing;
            List<Piece>[,] afterBoardGrid_ = afterBS.boardGrid;

            Piece king = BotHelperFunctions.getCloneFromOriginalPiece(ogKing, afterBoardGrid_);

            if (king == null)
            {
                BotHelperFunctions.resetPiecePositions(afterBS, BotHelperFunctions.convertBoardGrid(gameData.boardGrid));
                continue;
            }

            bool check = isCheck_(king, afterBS);
            BotHelperFunctions.resetPiecePositions(afterBS, BotHelperFunctions.convertBoardGrid(gameData.boardGrid));

            if (!check)
            {
                acceptedAbilities.Add(pa);
            }
        }

        return acceptedAbilities;
    }

    public static void restorePieceToBoard(List<Piece> pieces, coords position, List<GameObject> gos)
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
        if (botMatch == "True")
        {
            //Debug.Log("BOT MATCH DEATH");
            Piece p = gameData.allPiecesDict[go];
            if (p.color == 1)
            {
                gameData.botWhite.pieces.Remove(p);
            }
            else
            {
                gameData.botBlack.pieces.Remove(p);
            }
        }

        //Debug.Log("Online Death: " + online);
        if (online)
        {
            PhotonView pv = go.GetComponent<PhotonView>();

            if (pv == null)
            {
                //Debug.LogWarning($"No PhotonView -> {go.name}");
                Destroy(go);
                return;
            }

            if (pv.ViewID == 0)
            {
                //Debug.LogWarning($"ViewID is 0 -> {go.name}");
                Destroy(go);
                return;
            }

            if (pv.IsMine || PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(go);
            }
            else
            {
                //Debug.LogWarning($"Not owner -> {go.name}");
            }
        }
        else
        {
            Destroy(go);
        }
    }

    public static bool dummyIsCheck(List<coords> moves, Piece king)
    {
        return isInList(moves, king.position, false);
    }

    public static bool isCheckMate(Piece king, bool execDummyMove)
    {
        List<coords> moves = addToCurrentMoveableCoordsTotal(king.color, true, false, null, execDummyMove, true);

        int arrPos = 0;
        if (king.color == -1)
        {
            arrPos = 1;
        }

        //Debug.Log("Possible Moves: " + moves.Count);
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
            p.moves[i].y = p.moves[i].y * p.color;
            p.moves[i].x = p.moves[i].x * p.color;
        }

        for (int i = 0; i < p.oneTimeMoves.GetLength(0); i++)
        {
            p.oneTimeMoves[i].y = p.oneTimeMoves[i].y * p.color;
            p.oneTimeMoves[i].x = p.oneTimeMoves[i].x * p.color;
        }

        for (int i = 0; i < p.attacks.GetLength(0); i++)
        {
            p.attacks[i].y = p.attacks[i].y * p.color;
            p.attacks[i].x = p.attacks[i].x * p.color;
        }

        for (int i = 0; i < p.moveAndAttacks.GetLength(0); i++)
        {
            p.moveAndAttacks[i].y = p.moveAndAttacks[i].y * p.color;
            p.moveAndAttacks[i].x = p.moveAndAttacks[i].x * p.color;
        }

        for (int i = 0; i < p.jumpAttacks.GetLength(0); i++)
        {
            p.jumpAttacks[i].y = p.jumpAttacks[i].y * p.color;
            p.jumpAttacks[i].x = p.jumpAttacks[i].x * p.color;
        }

        for (int i = 0; i < p.murderousAttacks.GetLength(0); i++)
        {
            p.murderousAttacks[i].y = p.murderousAttacks[i].y * p.color;
            p.murderousAttacks[i].x = p.murderousAttacks[i].x * p.color;
        }

        for (int i = 0; i < p.conditionalAttacks.GetLength(0); i++)
        {
            p.conditionalAttacks[i].y = p.conditionalAttacks[i].y * p.color;
            p.conditionalAttacks[i].x = p.conditionalAttacks[i].x * p.color;
        }

        for (int i = 0; i < p.oneTimeMoveAndAttacks.GetLength(0); i++)
        {
            p.oneTimeMoveAndAttacks[i].y = p.oneTimeMoveAndAttacks[i].y * p.color;
            p.oneTimeMoveAndAttacks[i].x = p.oneTimeMoveAndAttacks[i].x * p.color;
        }

        for (int i = 0; i < p.flagMove1.GetLength(0); i++)
        {
            p.flagMove1[i].y = p.flagMove1[i].y * p.color;
            p.flagMove1[i].x = p.flagMove1[i].x * p.color;
        }

        for (int i = 0; i < p.flagMove2.GetLength(0); i++)
        {
            p.flagMove2[i].y = p.flagMove2[i].y * p.color;
            p.flagMove2[i].x = p.flagMove2[i].x * p.color;
        }

        for (int i = 0; i < p.pushMoves.GetLength(0); i++)
        {
            p.pushMoves[i].y = p.pushMoves[i].y * p.color;
            p.pushMoves[i].x = p.pushMoves[i].x * p.color;
        }

        for (int i = 0; i < p.enPassantMoves.GetLength(0); i++)
        {
            p.enPassantMoves[i].y = p.enPassantMoves[i].y * p.color;
            p.enPassantMoves[i].x = p.enPassantMoves[i].x * p.color;
        }

        if (p.color != 1)
        {
            p.promotingRow = 9 - p.promotingRow;
        }
    }

    public void updatePointsOnBoard()
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
        if (panel == null || panel.whiteCountText == null || panel.blackCountText == null ||gameData.botWhite == null ||gameData.botBlack == null ||gameData.pointsOnBoard == null ||gameData.pointsOnBoard.Length < 2)
        {
            return;
        }

        panel.whiteCountText.text = $"{gameData.botWhite.name}: {gameData.pointsOnBoard[0]}";
        panel.blackCountText.text = $"{gameData.botBlack.name}: {gameData.pointsOnBoard[1]}";
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
        foreach (Piece deadPiece in new List<Piece>(deadPieces))
        {
            if (checkState(deadPiece, PieceState.Shield) || checkCaptureTheFlag(deadPiece))
            {
                continue;
            }

            Debug.Log(deadPiece.name + " died to collateral on (" + deadPiece.position.x + "," + deadPiece.position.y + ")");
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

                if (gameData.isBotMatch)
                {
                    if (deadPiece.color == 1)
                    {
                        gameData.botWhite.pieces.Remove(deadPiece);
                        gameData.botBlack.opponentPieces.Remove(deadPiece);
                    }
                    else
                    {
                        gameData.botWhite.opponentPieces.Remove(deadPiece);
                        gameData.botBlack.pieces.Remove(deadPiece);
                    }
                }

                updateBoardGrid(deadPiece.position, deadPiece, "r");

                DestroyWrapper(dead);
                deadPiece.alive = 0;
            }

            //Debug.LogWarning("IN COLLATERAL");
            //BotHelperFunctions.debug_printBoardGrid(gameData.boardGrid);
        }
    }

    public static void forceRemove(Piece deadPiece)
    {
        GameObject dead = deadPiece.go;
        if (gameData.piecesDict.ContainsKey(dead))
        {
            gameData.piecesDict.Remove(dead);
        }

        if (gameData.isBotMatch)
        {
            if (deadPiece.color == 1)
            {
                gameData.botWhite.pieces.Remove(deadPiece);
                gameData.botBlack.opponentPieces.Remove(deadPiece);
            }
            else
            {
                gameData.botWhite.opponentPieces.Remove(deadPiece);
                gameData.botBlack.pieces.Remove(deadPiece);
            }
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
                movePiece(deadPiece, findSquare(deadPiece.startSquare.x, deadPiece.startSquare.y));
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
            if (!checkState(piece, PieceState.Dematerialized))
            {
                Debug.Log(piece.name + " died on (" + piece.position.x + "," + piece.position.y + ")");
                onDeath(piece, piece.go, attackerPiece, attacker);
            }
        }
    }

    public static void onDeathsDontIncludeAttacker(Piece attackerPiece, GameObject attacker, GameObject squareDead)
    {
        List<Piece> pieces = new List<Piece>(getPiecesOnSquareBoardGrid(squareDead));
        pieces.Remove(attackerPiece);

        foreach (Piece piece in pieces)
        {
            if (!checkState(piece, PieceState.Dematerialized))
            {
                Debug.Log(piece.name + " died on (" + piece.position.x + "," + piece.position.y + ")");
                onDeath(piece, piece.go, attackerPiece, attacker);
            }
        }
    }

    //TODO test for potential bug in killing dematerialized pieces that are stacked on other pieces
    public static void onDeath(Piece deadPiece, GameObject dead, Piece attackerPiece, GameObject attacker)
    {
        

        coords attackerCoords = attackerPiece.position;
        coords deadPieceCoords = deadPiece.position;

        bool skipCollateral = false;
        bool skipInfinite = false;
        
        //Electric
        if (checkState(deadPiece, PieceState.Electric))
        {
            int randomNumber = globalDefs.globalRand.Next(1, 3);

            if (randomNumber == 1)
            {
                tempInfo.attackerDied = true;
                collateralDeath(pieceToList(attackerPiece));
                //updateBoardGrid(attackerCoords, attackerPiece, "r");
                //DestroyWrapper(attacker);
            }
        }

        //Hungry
        if (checkState(attackerPiece, PieceState.Hungry))
        {
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            attackerPiece.storage.Add(deadPiece);
            skipCollateral = true;
            skipInfinite = true;
            gameData.piecesDict.Remove(dead);
            updateBoardGrid(deadPieceCoords, deadPiece, "r");
            removePieceImageFromBoard(deadPiece);

            return;
        }

        if (checkState(deadPiece, PieceState.Hungry))
        {
            if (deadPiece.storage != null && deadPiece.storage.Count > 0)
            {
                List<coords> placeCoords = getEmptySurroundingSquares(deadPiece.position);
                List<Piece> placePieces = deadPiece.storage;

                BotHelperFunctions.PieceAbility pa = new BotHelperFunctions.PieceAbility(deadPiece, PieceAbilities.Vomit, deadPiece.position, placePieces, placeCoords, null);
                gameData.helper.executeAbility(pa);
            }
        }

        //Spitting
        if (checkState(attackerPiece, PieceState.Spitting))
        {
            skipInfinite = true;
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            if (attackerPiece.storage.Count < attackerPiece.storageLimit)
            {
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

        //Stacking
        if (checkState(attackerPiece, PieceState.Stacking) && deadPiece.lives == 0)
        {
            // Abilities / States
            //attackerPiece.states |= deadPiece.states;
            tempInfo.stackingStates |= deadPiece.states;

            addAbility(attackerPiece, deadPiece.abilities);

            //Moves
            coords[] moves = combineMoveSets(attackerPiece.moves, deadPiece.moves);
            coords[] oneTimeMoves = combineMoveSets(attackerPiece.oneTimeMoves, deadPiece.oneTimeMoves);
            coords[] moveAndAttacks = combineMoveSets(attackerPiece.moveAndAttacks, deadPiece.moveAndAttacks);
            coords[] oneTimeMoveAndAttacks = combineMoveSets(attackerPiece.oneTimeMoveAndAttacks, deadPiece.oneTimeMoveAndAttacks);
            coords[] murderousAttacks = combineMoveSets(attackerPiece.murderousAttacks, deadPiece.murderousAttacks);
            coords[] conditionalAttacks = combineMoveSets(attackerPiece.conditionalAttacks, deadPiece.conditionalAttacks);
            coords[] attacks = combineMoveSets(attackerPiece.attacks, deadPiece.attacks);
            coords[] jumpAttacks = combineMoveSets(attackerPiece.jumpAttacks, deadPiece.jumpAttacks);
            //coords[] dependentAttacks = combineMoveSets(attackerPiece.dependentAttacks, deadPiece.dependentAttacks);
            //coords[] positionIndependentMoves = combineMoveSets(attackerPiece.positionIndependentMoves, deadPiece.positionIndependentMoves);
            coords[] flagMove1 = combineMoveSets(attackerPiece.flagMove1, deadPiece.flagMove1);
            coords[] flagMove2 = combineMoveSets(attackerPiece.flagMove2, deadPiece.flagMove2);
            coords[] pushMoves = combineMoveSets(attackerPiece.pushMoves, deadPiece.pushMoves);
            coords[] enPassantMoves = combineMoveSets(attackerPiece.enPassantMoves, deadPiece.enPassantMoves);

            attackerPiece.moves = moves;
            attackerPiece.oneTimeMoves = oneTimeMoves;
            attackerPiece.moveAndAttacks = moveAndAttacks;
            attackerPiece.oneTimeMoveAndAttacks = oneTimeMoveAndAttacks;
            attackerPiece.murderousAttacks = murderousAttacks;
            attackerPiece.conditionalAttacks = conditionalAttacks;
            attackerPiece.attacks = attacks;
            attackerPiece.jumpAttacks = jumpAttacks;
            //attackerPiece.dependentAttacks = dependentAttacks;
            //attackerPiece.positionIndependentMoves = positionIndependentMoves;
            attackerPiece.flagMove1 = flagMove1;
            attackerPiece.flagMove2 = flagMove2;
            attackerPiece.pushMoves = pushMoves;
            attackerPiece.enPassantMoves = enPassantMoves;

            //maybe add promotion row and storage
        }

        //Jailer
        if (checkState(attackerPiece, PieceState.Jailer))
        {
            skipInfinite = true;
            addState(deadPiece, PieceState.Jailed);

            return;
        }

        //Crook
        if (checkState(deadPiece, PieceState.Crook) && deadPiece.color != attackerPiece.color)
        {
            addState(deadPiece, PieceState.Jailed);

            return;
        }

        //Medusa
        if (checkState(attackerPiece, PieceState.Medusa))
        {
            skipInfinite = true;
            if (attackerPiece.numSpawns != 0)
            {
                attackerPiece.numSpawns--;

                coords pos = deadPiece.position;
                removePieceFromBoard(pieceToList(deadPiece));

                Piece shieldPawn = Spawnables.create("ShieldPawn", attackerPiece.color * -1, false);
                initPiece(shieldPawn, pos);
            }
        }

        if (!skipInfinite)
        {
            //Infinite / Multi-Lives
            if (deadPiece.lives != 0)
            {
                handleMultipleLivesDeath(deadPiece);

                return;
            }
        }

        if (!skipCollateral)
        {

            //Collateral (Attacker)
            if (attackerPiece.collateralType == 0) //Kill on Capture
            {
                if (isPieceSurroundingState(deadPiece, PieceState.Defuser))
                {
                    //collateralDeath(pieceToList(attackerPiece));
                    collateralDeath(pieceToList(deadPiece));
                    //tempInfo.attackerDied = true;
                    return;
                }

                for (int i = 0; i < attackerPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { deadPieceCoords.x + attackerPiece.collateral[i].x, deadPieceCoords.y + attackerPiece.collateral[i].y };
                    GameObject square = findSquare(coords[0], coords[1]);

                    if (attackerPiece.collateral[i].x == 0 && attackerPiece.collateral[i].y == 0)
                    {
                        collateralDeath(pieceToList(attackerPiece));
                        tempInfo.attackerDied = true;
                    }

                    if (!square) continue;

                    List<Piece> pieces = new List<Piece>(getPiecesOnSquareBoardGrid(square));
                    collateralDeath(pieces);
                }
            }

            if (attackerPiece.collateralType == 2) //Freeze on Capture
            {
                if (isPieceSurroundingState(deadPiece, PieceState.Defuser))
                {
                    return;
                }

                for (int i = 0; i < attackerPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { deadPieceCoords.x + attackerPiece.collateral[i].x, deadPieceCoords.y + attackerPiece.collateral[i].y };
                    GameObject square = findSquare(coords[0], coords[1]);

                    if (attackerPiece.collateral[i].x == 0 && attackerPiece.collateral[i].y == 0)
                    {
                        addState(attackerPiece, PieceState.Frozen);
                        addAbility(attackerPiece, PieceAbilities.Unfreeze);

                        Image img = attackerPiece.go.GetComponent<Image>();
                        Color blueTint = Color.blue;

                        img.color = Color.Lerp(img.color, blueTint, 0.4f);
                    }

                    if (!square) continue;

                    List<Piece> pieces = new List<Piece>(getPiecesOnSquareBoardGrid(square));
                    foreach(Piece p in pieces)
                    {
                        addState(p, PieceState.Frozen);
                        addAbility(p, PieceAbilities.Unfreeze);

                        Image img = p.go.GetComponent<Image>();
                        Color blueTint = Color.blue;

                        img.color = Color.Lerp(img.color, blueTint, 0.4f);
                    }
                }
            }

            //Collateral (Attackee)
            if (deadPiece.collateralType == 1) //Kill on death
            {
                if (isPieceSurroundingState(deadPiece, PieceState.Defuser))
                {
                    //collateralDeath(pieceToList(attackerPiece));
                    collateralDeath(pieceToList(deadPiece));
                    //tempInfo.attackerDied = true;
                    return;
                }

                for (int i = 0; i < deadPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { deadPieceCoords.x + deadPiece.collateral[i].x, deadPieceCoords.y + deadPiece.collateral[i].y };
                    GameObject square = findSquare(coords[0], coords[1]);

                    if (deadPiece.collateral[i].x == 0 && deadPiece.collateral[i].y == 0)
                    {
                        collateralDeath(pieceToList(attackerPiece));
                        collateralDeath(pieceToList(deadPiece));
                        tempInfo.attackerDied = true;
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

        if (gameData.isBotMatch)
        {
            if (deadPiece.color == 1)
            {
                gameData.botWhite.pieces.Remove(deadPiece);
                gameData.botBlack.opponentPieces.Remove(deadPiece);
            }
            else
            {
                gameData.botWhite.opponentPieces.Remove(deadPiece);
                gameData.botBlack.pieces.Remove(deadPiece);
            }
        }

        updateBoardGrid(deadPieceCoords, deadPiece, "r");
        DestroyWrapper(dead);
        deadPiece.alive = 0;
        deadPiece = null;
    }

    [PunRPC]
    public void _MovePieceRPC(int[] toMoveCoords, coords coords)
    {
        movePiece_(gameData.selectedPiece, coords);
    }

    public static bool isOnStartSquare(Piece piece)
    {
        return piece.startSquare.x == piece.position.x && piece.startSquare.y == piece.position.y;
    }

    public static bool isPieceOnStartSquare(Piece piece)
    {
        return isPieceOnSquare(findSquare(piece.startSquare.x, piece.startSquare.y), false);
    }

    public static bool checkState(Piece piece, PieceState state)
    {
        return (piece.states & state) != 0;
    }

    public static bool checkAbility(Piece piece, PieceAbilities ability)
    {
        return (piece.abilities & ability) != 0;
    }

    public static bool checkStateOnSquare(List<Piece> piecesOnSquare, PieceState state)
    {
        if (piecesOnSquare == null || piecesOnSquare.Count == 0)
            return false;

        foreach (Piece piece in piecesOnSquare)
        {
            if ((piece.states & state) != 0)
                return true;
        }

        return false;
    }

    public static bool checkStateAllOnSquare(List<Piece> piecesOnSquare, PieceState states)
    {
        if (piecesOnSquare == null || piecesOnSquare.Count == 0)
            return false;

        foreach (Piece piece in piecesOnSquare)
        {
            if ((piece.states & states) == 0)
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
            Sprite s = Resources.Load<Sprite>(url);
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

            coords coords = findCoords(square);
            foreach (Piece piece in gameData.piecesDict.Values)
            {
                if (piece.position.x == coords.x && piece.position.y == coords.y)
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

    public static List<Piece>[,] initBoardGridNew()
    {
        List<Piece>[,] grid = new List<Piece>[8, 8];

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                grid[x, y] = new List<Piece>();
            }
        }

        return grid;
    }

    public static List<Piece> getPiecesOnSquareBoardGrid(GameObject square)
    {
        List<Piece> pieces = new List<Piece>();
        if (square != null)
        {

            coords coords = findCoords(square);

            return gameData.boardGrid[coords.x - 1][coords.y - 1];
        }

        return pieces;
    }

    public static bool isMultipleOnSquare(GameObject square)
    {
        coords coords = findCoords(square);

        return gameData.boardGrid[coords.x - 1][coords.y - 1].Count > 1;
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

    public static List<int> getColorsOnSquare(GameObject square, bool ignoreDematerialized)
    {
        List<int> colors = new List<int>();

        List<Piece> pieces = getPiecesOnSquareBoardGrid(square);

        if (pieces == null)
        {
            return colors;
        }

        foreach (Piece piece in pieces)
        {
            if (piece.disabled || piece.alive == 0 || (ignoreDematerialized && checkState(piece, PieceState.Dematerialized)) || checkState(piece, PieceState.Jailed))
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
            if (!piece.disabled && !checkState(piece, PieceState.Dematerialized))
            {
                return false;
            }
        }

        return true;
    }

    public static bool checkSquareCrowdingEligible(Piece piece, List<Piece> piecesOnSquare)
    {
        // No Pieces
        if (piecesOnSquare == null || piecesOnSquare.Count == 0)
        {
            return true;
        }

        // Pieces Different Color
        //if (getColorsOnSquare(findSquare(piecesOnSquare[0].position[0], piecesOnSquare[0].position[1]), true).Contains(piece.color * -1))
        if (BotHelperFunctions.isolatedGetColorsOnCoords(piecesOnSquare, true).Contains(piece.color * -1))
        {
            return false;
        }

        // Square contains more than one other piece (not crowding)
        if (piecesOnSquare.Count > 1 && checkState(piece, PieceState.Crowding))
        {
            foreach (Piece _piece in piecesOnSquare)
            {
                if (!checkState(_piece, PieceState.Crowding))
                {
                    return false;
                }
            }

            // If they are all crowding
            return true;
        }

        //There is one piece on the square, piece is crowding
        if (checkState(piece, PieceState.Crowding) && piecesOnSquare.Count == 1 && isColorOnSquare(piecesOnSquare, piece.color * 1, true))
        {
            return true;
        }

        //There is one piece on square, piece on square is piggyback
        if (piecesOnSquare.Count == 1 && checkState(piecesOnSquare[0], PieceState.Piggyback) && isColorOnSquare(piecesOnSquare, piece.color * 1, true))
        {
            if (!checkState(piece, PieceState.CaptureTheFlag) && piece.baseType != "King")
            {
                return true;
            }
        }

        //There is one piece on square, piece is jockey
        if (piecesOnSquare.Count == 1 && checkState(piece, PieceState.Jockey) && isColorOnSquare(piecesOnSquare, piece.color * 1, true))
        {
            return true;
        }


        if (!checkState(piece, PieceState.Crowding))
        {
            return false;
        }
        else
        {
            //Last case square contains one piece and its same color
            return true;
        }
    }

    public static void updateBoardGrid(coords coords, Piece piece, String action)
    {
        if (coords.x < 1 || coords.y < 1)
        {
            return;
        }

        var square = gameData.boardGrid[coords.x - 1][coords.y - 1];

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
            int ok = square.RemoveAll(p => p.name == piece.name);
            //Debug.LogWarning("Attempted remove of " + ok + " " + piece.name + " on " + coords[0] + "," + coords[1]);
        }
    }

    public static void movePieceBoardGrid(Piece piece, coords position /*moveFrom*/, coords coords /*moveTo*/)
    {
        if (position.x < 1 || position.y < 1 || coords.x < 1 || coords.y < 1)
        {
            return;
        }

        updateBoardGrid(position, piece, "r");
        gameData.boardGrid[coords.x - 1][coords.y - 1].Add(piece);

        piece.position = coords;
    }

    public static bool isPieceOnSquare(GameObject square, bool includeDisabled)
    {
        if (square == null) return true;

        coords coords = findCoords(square);

        if (coords.y < 1 || coords.x < 1)
        {
            return false;
        }

        if (!includeDisabled)
        {
            List<Piece> pieces = gameData.boardGrid[coords.x - 1][coords.y - 1];

            foreach (Piece p in pieces)
            {
                if (checkState(p, PieceState.Dematerialized) || p.disabled)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        return gameData.boardGrid[coords.x - 1][coords.y - 1].Count > 0;
    }

    public static bool isColorNotOnSquare(GameObject square, int color)
    {
        var colors = getColorsOnSquare(square, true);

        return !colors.Contains(color);
    }

    public static bool isColorOnSquare(List<Piece> piecesOnSquare, int color, bool ignoreDematerialized)
    {
        return BotHelperFunctions.isolatedGetColorsOnCoords(piecesOnSquare, ignoreDematerialized).Contains(color);
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

            GameObject square = findSquare(coords[0] + hungryPiece.position.x, coords[1] + hungryPiece.position.y);
            if (square == null) { continue; }

            if (!isPieceOnSquare(square, false))
            {
                coord = new int[] { coords[0] + hungryPiece.position.x, coords[1] + hungryPiece.position.y };
                break;
            }
        }

        if (coord == null)
        {
            return null;
        }

        return findSquare(coord[0], coord[1]);
    }

    public static List<coords> getEmptySurroundingSquares(coords coords)
    {
        List<coords> emptySurroundingSquares = new List<coords>();

        foreach (var (dirX, dirY) in globalDefs.globalDirectionsNoZero)
        {
            int posX = coords.x + dirX;
            int posY = coords.y + dirY;

            if (!checkBounds(posX, posY)) continue;

            if (!isPieceOnSquare(findSquare(posX, posY), false))
            {
                emptySurroundingSquares.Add(new coords(posX, posY));
            }
        }

        return emptySurroundingSquares;
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
        if (!checkState(piece, PieceState.CaptureTheFlag))
        {
            return false;
        }

        if (piece.color == 1)
        {
            if (piece.position.y <= 2)
            {
                return true;
            }
        }
        else
        {
            if (piece.position.y >= 7)
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

        if (checkState(king, PieceState.Uncastle))
        {
            return false;
        }

        bool goNext = false;
        if (king.alive == 1 && rook.alive == 1 && !king.hasMoved && !rook.hasMoved)
        {
            if (king.color == 1 && gameData.isInCheck[0] == 0 || king.color == -1 && gameData.isInCheck[1] == 0)
            {
                goNext = true;
            }

            if (king.alive == 1 && rook.alive == 1 && checkState(king, PieceState.Rulebreaker))
            {
                goNext = true;
            }
        }

        if (goNext)
        {
            //Debug.Log("King: " + king.position[0] + " : " + king.position[1]);
            //Debug.Log("Rook: " + rook.position[0] + " : " + rook.position[1]);
            kingSquare = findSquare(king.position.x, king.position.y);
            rookSquare = findSquare(rook.position.x, rook.position.y);

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
        if (findCoords(s1).y != findCoords(s2).y)
        {
            return new List<Piece>();
        }

        int y = findCoords(s1).y;
        List<Piece> pieces = new List<Piece>();

        int x1 = findCoords(s1).x;
        int x2 = findCoords(s2).x;

        if (x1 == x2)
        {
            return new List<Piece>();
        }

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
            int x = piece.position.x + dir[0];
            int y = piece.position.x + dir[1];

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
            int x = piece.position.x + dir[0];
            int y = piece.position.y + dir[1];

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
            int x = piece.position.x + dir[0];
            int y = piece.position.y + dir[1];

            List<Piece> piecesOnSquare = getPiecesOnSquareBoardGrid(findSquare(x, y));

            if (getPiecesOnSquareBoardGrid(findSquare(x, y)).Count > 0 && isColorOnSquare(piecesOnSquare, color, true))
            {
                return true;
            }
        }

        return false;
    }

    public static void addState(Piece piece, PieceState state)
    {
        piece.states |= state;
    }

    public static void addAbility(Piece piece, PieceAbilities ability)
    {
        piece.abilities |= ability;
    }

    /*
    public static void addAbility(Piece piece, String ability)
    {
        if (piece.ability == "" || piece.ability == null)
        {
            piece.ability = ability;
        }
        else
        {
            var abilities = piece.ability.Split('-');

            bool exists = false;
            foreach (var a in abilities)
            {
                if (a == ability)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                piece.ability += "-" + ability;
            }
        }
    }
    */

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
            int x = piece.position.x + dir[0];
            int y = piece.position.y + dir[1];

            if (getPiecesOnSquareBoardGrid(findSquare(x, y)).Count == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static class Spawnables
    {
        public static Piece create(string pieceName, int color, bool simulated)
        {
            Piece piece;

            switch (pieceName)
            {
                case "King":
                    piece = new King(color, online, simulated);
                    break;

                case "Queen":
                    piece = new Queen(color, online, simulated);
                    break;

                case "Rook":
                    piece = new Rook(color, online, simulated);
                    break;

                case "Knight":
                    piece = new Knight(color, online, simulated);
                    break;

                case "Bishop":
                    piece = new Bishop(color, online, simulated);
                    break;

                case "Pawn":
                    piece = new Pawn(color, online, simulated);
                    break;

                case "ZombiePawn":
                    piece = new ZombiePawn(color, online, simulated);
                    break;

                case "SuperPawn":
                    piece = new SuperPawn(color, online, simulated);
                    break;

                case "LeftPawn":
                    piece = new LeftPawn(color, online, simulated);
                    break;

                case "RightPawn":
                    piece = new RightPawn(color, online, simulated);
                    break;

                case "DepressedKing":
                    piece = new DepressedKing(color, online, simulated);
                    break;

                case "ShieldPawn":
                    piece = new ShieldPawn(color, online, simulated);
                    break;

                default:
                    throw new ArgumentException("Bad Piece");
            }

            int rand = globalDefs.globalRand.Next(1, 10001);

            if (simulated || !simulated) piece.name = "simulated_" + piece.name + "_" + rand;

            return piece;
        }
    }

    public static void removeState(Piece piece, PieceState state)
    {
        piece.states &= ~state;
    }

    public static void removeAbility(Piece piece, PieceAbilities ability)
    {
        piece.abilities &= ~ability;
    }

    /*
    public static void removeAbility(Piece piece, string ability)
    {

        piece.ability = piece.ability.Replace("-" + ability, "");
        piece.ability = piece.ability.Replace(ability, "");
    }
    */

    public static bool checkBounds(int x, int y)
    {
        if (x > 8 || y > 8 || x < 1 || y < 1)
        {
            return false;
        }

        return true;
    }

    public static bool isPieceSurroundingState(Piece piece, PieceState state)
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
            int x = piece.position.x + dir[0];
            int y = piece.position.y + dir[1];

            if (checkStateOnSquare(getPiecesOnSquare(findSquare(x, y)), state))
            {
                return true;
            }
        }

        return false;
    }

    public static coords[] combineMoveSets(coords[] a, coords[] b)
    {
        if (a == null || b == null)
        {
            return new coords[] { };
        }

        List<coords> result = new List<coords>();

        for (int i = 0; i < a.GetLength(0); i++)
            result.Add(new coords( a[i].x, a[i].y ));

        for (int i = 0; i < b.GetLength(0); i++)
        {
            int x = b[i].x;
            int y = b[i].y;

            bool exists = result.Any(r => r.x == x && r.y == y);
            if (!exists)
                result.Add(new coords( x, y ));
        }

        coords[] merged = new coords[result.Count];
        for (int i = 0; i < result.Count; i++)
        {
            merged[i] = new coords(result[i].x, result[i].y);
        }

        return merged;
    }

    public static void updatePieceFlags(Piece piece, bool isInCheck)
    {
        //Debug.Log("Updating Flags for: " + piece.name);
        if (checkState(piece, PieceState.Protective) || checkState(piece, PieceState.Scaredy) || checkState(piece, PieceState.Depressed))
        {
            if (isInCheck)
            {
                piece.flag = 2;
            }
            else
            {
                piece.flag = 1;
            }
        }
    }

    public static List<Piece> getPiecesOnBoard()
    {
        List<Piece> allPieces = new List<Piece>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                List<Piece> pieces = getPiecesOnSquareBoardGrid(findSquare(i + 1, j + 1));
                allPieces.AddRange(pieces);
            }
        }

        return allPieces;
    }

    public static List<Piece> getPiecesOnBoardColor(int color)
    {
        List<Piece> allPieces = new List<Piece>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                List<Piece> pieces = getPiecesOnSquareBoardGrid(findSquare(i, j));
                allPieces.AddRange(pieces);
            }
        }

        foreach(Piece p in allPieces) {
            if (p.color != color) {
                allPieces.Remove(p);
            }
        }

        return allPieces;
    }

    //TODO use new var Piece.baseType instead
    public static bool isPieceTypeOnBoard(string pieceType, int color)
    {
        List<Piece> pieces = getPiecesOnBoard();
        foreach(Piece p in pieces)
        {
            if (p.name.Contains(pieceType) && p.color == color)
            {
                return true;
            }
        }

        return false;
    }

    public static bool isPieceBaseTypeOnBoard(string pieceType, int color)
    {
        List<Piece> pieces = getPiecesOnBoard();
        foreach (Piece p in pieces)
        {
            if (p.baseType == pieceType && p.color == color)
            {
                return true;
            }
        }

        return false;
    }

    public static bool isOppressorOnBoard(int pieceColor)
    {
        List<Piece> pieces = getPiecesOnBoard();
        foreach (Piece p in pieces)
        {
            if (checkState(p, PieceState.Oppressive) && p.color != pieceColor)
            {
                return true;
            }
        }

        return false;
    }

    public static bool checkPieceType(Piece piece, string pieceType)
    {
        if (piece.name.Contains(pieceType))
        {
            return true;
        }

        return false;
    }

    public static bool checkPieceTypeFromList(List<Piece> pieces, string pieceType)
    {
        foreach (Piece piece in pieces)
        {
            bool pieceIsType = checkPieceType(piece, pieceType);

            if (pieceIsType)
            {
                return true;
            }
        }

        return false;
    }

    public static void reinitPiece(Piece piece, coords coords)
    {
        if (!gameData.piecesDict.ContainsKey(piece.go))
        {
            gameData.piecesDict.Add(piece.go, piece);
        }

        if (!gameData.allPiecesDict.ContainsKey(piece.go))
        {
            gameData.allPiecesDict.Add(piece.go, piece);
        }

        GameObject toAppend = findSquare(coords.x, coords.y);
        piece.position = coords;

        if (checkState(piece, PieceState.Pawn))
        {
            if (piece.color == 1)
            {
                piece.position = new coords(piece.position.x, piece.position.y + 1);
            }
            else
            {
                piece.position = new coords(piece.position.x, piece.position.y - 1);
            }
            
            toAppend = findSquare(piece.position.x, piece.position.y);
        }

        if (checkState(piece, PieceState.Double))
        {
            Piece doublePawn = Spawnables.create("Pawn", piece.color, false);
            initPiece(doublePawn, piece.position);
        }

        movePiece(piece, toAppend);

        piece.alive = 1;

        //piece.go.name = piece.name;

        if (gameData.isBotMatch)
        {
            if (piece.color == 1) {
                if (!gameData.botWhite.pieces.Contains(piece)) {
                    gameData.botWhite.pieces.Add(piece);
                }

                if (!gameData.botBlack.opponentPieces.Contains(piece)) {
                    gameData.botBlack.opponentPieces.Add(piece);
                }
            }
            else if (piece.color == -1) {
                if (!gameData.botBlack.pieces.Contains(piece)) {
                    gameData.botBlack.pieces.Add(piece);
                }

                if (!gameData.botWhite.opponentPieces.Contains(piece)) {
                    gameData.botWhite.opponentPieces.Add(piece);
                }
            }
        }

        updateBoardGrid(piece.position, piece, "a");

        string panelCode = findNextAvailablePanelCode(getPieceColor(piece) + getPieceType(piece) + "0");
        piece.name = panelCode;
        gameData.panelCodes.Add(piece.name);
        piece.go.name = panelCode;
    }

    public static void initPiece(Piece piece, coords coords)
    {
        piece.startSquare = new coords(piece.position.x, piece.position.y);

        if (checkState(piece, PieceState.Fusion))
        {
            List<Piece> pieces = getFusionPieces(piece.baseType, piece.color);

            addAbility(piece, pieces[0].abilities);
            addAbility(piece, pieces[1].abilities);
            addState(piece, pieces[0].states);
            addState(piece, pieces[1].states);

            piece.collateralType = Math.Max(pieces[0].collateralType, pieces[1].collateralType);
            piece.storageLimit = Math.Max(pieces[0].storageLimit, pieces[1].storageLimit);
            piece.numSpawns = Math.Max(pieces[0].numSpawns, pieces[1].numSpawns);

            piece.moves = combineMoveSets(pieces[0].moves, pieces[1].moves);
            piece.oneTimeMoves = combineMoveSets(pieces[0].oneTimeMoves, pieces[1].oneTimeMoves);
            piece.moveAndAttacks = combineMoveSets(pieces[0].moveAndAttacks, pieces[1].moveAndAttacks);
            piece.oneTimeMoveAndAttacks = combineMoveSets(pieces[0].oneTimeMoveAndAttacks, pieces[1].oneTimeMoveAndAttacks);
            piece.murderousAttacks = combineMoveSets(pieces[0].murderousAttacks, pieces[1].murderousAttacks);
            piece.conditionalAttacks = combineMoveSets(pieces[0].conditionalAttacks, pieces[1].conditionalAttacks);
            piece.jumpAttacks = combineMoveSets(pieces[0].jumpAttacks, pieces[1].jumpAttacks);
            piece.attacks = combineMoveSets(pieces[0].attacks, pieces[1].attacks);
            piece.flagMove1 = combineMoveSets(pieces[0].flagMove1, pieces[1].flagMove1);
            piece.flagMove2 = combineMoveSets(pieces[0].flagMove2, pieces[1].flagMove2);
            piece.pushMoves = combineMoveSets(pieces[0].pushMoves, pieces[1].pushMoves);
            piece.enPassantMoves = combineMoveSets(pieces[0].enPassantMoves, pieces[1].enPassantMoves);

            piece.collateral = new coords[] { };
            piece.collateral = combineMoveSets(pieces[0].collateral, pieces[1].collateral);
            if (piece.collateral.Length == 0)
            {
                piece.collateral = null;
            }

            piece.lives = pieces[0].lives;
            if (piece.lives == 1)
            {
                piece.lives = pieces[1].lives;
            }

            piece.promotingRow = pieces[0].promotingRow;
            if (piece.promotingRow == 8 || piece.promotingRow == 1)
            {
                piece.promotingRow = pieces[1].promotingRow;
            }

            piece.promotesInto = pieces[0].promotesInto;
            if (piece.promotesInto == "")
            {
                piece.promotesInto = pieces[1].promotesInto;
            }

            piece.spawnable = pieces[0].spawnable;
            if (piece.spawnable == "")
            {
                piece.spawnable = pieces[1].spawnable;
            }
        }

        reinitPiece(piece, coords);
    }

    public static List<Piece> getFusionPieces(string type, int color)
    {
        List<Type> pieces = BotHelperFunctions.getAllTypePieces(type, color);

        List<Piece> selected = new List<Piece>();

        int count = 2;

        System.Random rand = new System.Random();

        for (int i = 0; i < count; i++)
        {
            int index = rand.Next(pieces.Count);

            Type type_ = pieces[index];
            Piece piece = (Piece)Activator.CreateInstance(type_, color, false, false);

            selected.Add(piece);
            pieces.RemoveAt(index);
        }

        return selected;
    }

    public static string getPieceType(Piece p)
    {
        if (p.baseType == "Pawn")
        {
            return "p";
        }

        if (p.baseType == "Rook")
        {
            return "r";
        }

        if (p.baseType == "Bishop")
        {
            return "b";
        }

        if (p.baseType == "Knight")
        {
            return "n";
        }

        if (p.baseType == "Queen")
        {
            return "q";
        }

        if (p.baseType == "King")
        {
            return "k";
        }

        return "m";
    }

    public static string getPieceColor(Piece p)
    {
        if (p.color == 1)
        {
            return "w_";
        }

        return "b_";
    }

    public void toggleCheckmateUI()
    {
        checkmateUI.SetActive(true);
    }

    public (bool death, int check) executeAbility(BotHelperFunctions.PieceAbility pieceAbility)
    {
        bool death = false;
        int check = 0;

        Piece piece = BotHelperFunctions.getOriginalPieceFromClone(pieceAbility.piece);
        Piece secondPiece = BotHelperFunctions.getOriginalPieceFromClone(pieceAbility.secondPiece);
        if (secondPiece != null) Debug.Log(secondPiece.go);

        PieceAbilities ability = pieceAbility.ability;
        coords coords = new coords(pieceAbility.coords.x, pieceAbility.coords.y);

        List<Piece> placePieces = new List<Piece>();
        if (pieceAbility.placePieces != null)
        {
            foreach (Piece placePiece in pieceAbility.placePieces)
            {
                Piece thePiece = BotHelperFunctions.getOriginalPieceFromClone(placePiece);
                if (thePiece != null) placePieces.Add(thePiece);
            }
        }

        List<coords> placeCoords = new List<coords>();
        if (pieceAbility.placeCoords != null)
        {
            placeCoords = pieceAbility.placeCoords;
        }
        
        if (ability == PieceAbilities.Vomit)
        {
            int numPieces = piece.storage.Count;
            int numCoords = placeCoords.Count;

            string debugMsg = "Ability: Vomit -> ";
            foreach(Piece _p in piece.storage)
            {
                debugMsg += _p.name + ", ";
            }
            Debug.LogWarning(debugMsg);

            if (numPieces >= numCoords)
            {
                foreach (coords coords_ in placeCoords)
                {
                    System.Random rand = new System.Random();
                    int idx = rand.Next(numPieces);
                    numPieces--;

                    Piece p_ = placePieces[idx];
                    placePieces.Remove(p_);

                    Debug.LogWarning("Vomiting on adjusted cords: " + coords_.x + "," + coords_.y);

                    updateBoardGrid(coords_, p_, "a");
                    
                    restorePieceImageToBoard(p_);

                    removeState(p_, PieceState.Jailed);
                    reinitPiece(p_, coords_);

                    piece.storage.Remove(p_);
                }

                foreach(Piece p_ in new List<Piece>(piece.storage))
                {
                    piece.storage.Remove(p_);
                    forceRemove(p_);
                }
            }
            else
            {
                foreach (Piece p_ in new List<Piece>(piece.storage))
                {
                    System.Random rand = new System.Random();
                    int idx = rand.Next(numCoords); numCoords--;

                    coords c_ = placeCoords[idx];
                    placeCoords.Remove(c_);

                    Debug.LogWarning("Vomiting on adjusted cords: " + c_.x + "," + c_.y);

                    updateBoardGrid(c_, p_, "a");

                    restorePieceImageToBoard(p_);

                    removeState(p_, PieceState.Jailed);
                    reinitPiece(p_, c_);

                    piece.storage.Remove(p_);
                }
            }
        }
        else if (ability == PieceAbilities.CastleLeft)
        {
            Debug.LogWarning("Ability: CastleLeft -> " + piece.name + " " + secondPiece.name);
            coords kingCoords = coords;
            coords rookCoords = new coords(kingCoords.x + 1, kingCoords.y);

            //King
            movePieceBoardGrid(piece, piece.position, kingCoords);
            //Rook
            movePieceBoardGrid(secondPiece, secondPiece.position, rookCoords);

            piece.hasMoved = true;
            secondPiece.hasMoved = true;

            removeAbility(piece, PieceAbilities.CastleLeft);
            removeAbility(piece, PieceAbilities.CastleRight);
        }
        else if (ability == PieceAbilities.CastleRight)
        {
            Debug.LogWarning("Ability: CastleRight -> " + piece.name + " " + secondPiece.name);
            coords kingCoords = coords;
            coords rookCoords = new coords(kingCoords.x - 1, kingCoords.y);

            //King
            movePieceBoardGrid(piece, piece.position, kingCoords);
            //Rook
            movePieceBoardGrid(secondPiece, secondPiece.position, rookCoords);

            piece.hasMoved = true;
            secondPiece.hasMoved = true;

            removeAbility(piece, PieceAbilities.CastleLeft);
            removeAbility(piece, PieceAbilities.CastleRight);
        }
        else if (ability == PieceAbilities.Unfreeze)
        {
            Debug.LogWarning("Ability: Unfreeze -> " + piece.name);
            removeState(piece, PieceState.Frozen);
            removeAbility(piece, PieceAbilities.Unfreeze);

            Image img = piece.go.GetComponent<Image>();
            Color c = img.color;

            c.r = 1f;
            c.g = 1f;
            c.b = 1f;

            img.color = c;
        }
        else if (ability == PieceAbilities.Freeze)
        {
            Debug.LogWarning("Ability: Freeze -> " + piece.name);
            addState(secondPiece, PieceState.Frozen);
            addAbility(secondPiece, PieceAbilities.Unfreeze);

            Image img = secondPiece.go.GetComponent<Image>();
            Color blueTint = Color.blue;

            img.color = Color.Lerp(img.color, blueTint, 0.4f);
        }
        else if (ability == PieceAbilities.Spawn)
        {
            Debug.LogWarning("Ability: Spawn -> " + piece.spawnable + " " + coords.x + "," + coords.y);

            Piece spawned = Spawnables.create(piece.spawnable, piece.color, false);
            piece.numSpawns--;

            if (piece.numSpawns <= 0)
            {
                removeAbility(piece, PieceAbilities.Spawn);
            }

            initPiece(spawned, coords);
        }
        else if (ability == PieceAbilities.Spit)
        {
            Debug.LogWarning("Ability: Spit -> " + piece.storage[0].name + " " + coords.x + "," + coords.y);

            Piece storagePiece = BotHelperFunctions.getOriginalPieceFromClone(piece.storage[0]);
            if (storagePiece == null || storagePiece.go == null)
            {
                storagePiece = secondPiece;
            }

            List<Piece> spitPieces = getPiecesOnSquare(findSquare(coords.x, coords.y));
            if (spitPieces != null && spitPieces.Count > 0)
            {
                collateralDeath(spitPieces);
                death = true;
            }

            restorePieceImageToBoard(storagePiece);
            removeState(storagePiece, PieceState.Jailed);
            reinitPiece(storagePiece, coords);

            updateBoardGrid(coords, storagePiece, "a");

            piece.storage.Remove(storagePiece);
        }
        else if (ability == PieceAbilities.Dematerialize)
        {
            Debug.LogWarning("Ability: Dematerialize -> " + piece.name);

            addState(piece, PieceState.Dematerialized);
            removeAbility(piece, PieceAbilities.Dematerialize);
            addAbility(piece, PieceAbilities.Materialize);

            Image img = piece.go.GetComponent<Image>();
            Color c = img.color;
            c.a = 0.5f;
            img.color = c;
            resetBoardColours();
        }
        else if (ability == PieceAbilities.Materialize)
        {
            Debug.LogWarning("Ability: Materialize -> " + piece.name);

            removeState(piece, PieceState.Dematerialized);
            removeAbility(piece, PieceAbilities.Materialize);
            addAbility(piece, PieceAbilities.Dematerialize);

            death = true;

            onDeathsDontIncludeAttacker(piece, piece.go, findSquare(coords.x, coords.y));

            checkPromote(piece, piece.position);

            Image img = piece.go.GetComponent<Image>();
            Color c = img.color;
            c.a = 1f;
            img.color = c;
            resetBoardColours();
        }
        else if (ability == PieceAbilities.Split)
        {
            Debug.LogWarning("Ability: Split -> " + piece.name);

            removeAbility(piece, PieceAbilities.Split);

            forceRemove(piece);
            updateBoardGrid(coords, piece, "r");

            Piece leftPawn = Spawnables.create("LeftPawn", piece.color, false);
            initPiece(leftPawn, coords);

            Piece rightPawn = Spawnables.create("RightPawn", piece.color, false);
            initPiece(rightPawn, coords);
        }

        Piece king;
        if (piece.color == 1)
        {
            king = gameData.blackKing;
        }
        else
        {
            king = gameData.whiteKing;
        }

        bool isInCheck;

        BoardState bs = new BoardState();
        bs.refresh(BotHelperFunctions.convertBoardGrid(gameData.boardGrid));

        Piece cloneKing = BotHelperFunctions.getCloneFromOriginalPiece(king, bs.boardGrid);

        if (cloneKing == null)
        {
            isInCheck = true;
        }
        else
        {
            isInCheck = isCheck_(cloneKing, bs);
        }
        bool isInCheckMate = isCheckMate(king, true);

        Debug.Log("Check: " + isInCheck);
        Debug.Log("Checkmate: " + isInCheckMate);
        gameData.check = isInCheck;

        updatePointsOnBoard();

        gameData.selectedPiece = null;

        if (isInCheckMate)
        {
            check = 2;
        }
        else if (isInCheck)
        {
            check = 1;
        }
        else
        {
            check = 0;
        }

        return (death, check);
    }

    [PunRPC]
    public void MovePieceRPC(int[] toMoveCoords, coords coords)
    {
        GameObject square = findSquare(toMoveCoords[0], toMoveCoords[1]);
        Piece piece = gameData.selectedToMovePiece;
        movePiece_(piece, coords);
    }

    // 0 = ok
    // 1 = check
    // 2 = checkmate
    public int movePiece_(Piece piece, coords coords)
    {
        //Delayed Piece Move
        if (tempInfo.delayedQueue == null)
        {
            tempInfo.delayedQueue = new DelayedQueue();
        }
        tempInfo.delayedQueue.deIncrement();

        bool delayedMoves = true;
        while (delayedMoves)
        {
            PieceMove moveToCheck = tempInfo.delayedQueue.Peek();
            if (moveToCheck != null && moveToCheck.turnsToRemove <= 0)
            {
                moveToCheck = tempInfo.delayedQueue.Dequeue();
                delayedMove(moveToCheck);
            }
            else
            {
                delayedMoves = false;
            }
        }

        if (checkState(piece, PieceState.Delayed))
        {
            PieceMove delayedMove = new PieceMove(piece, coords, 2);
            tempInfo.delayedQueue.Enqueue(delayedMove);

            gameData.turn *= -1;
            return 0;
        }

        //Debug.Log("Flags");
        //if (gameData.selected) Debug.Log("Selected: " + gameData.selected.name);
        //if (gameData.selectedToMove) Debug.Log("SelectedToMove: " + gameData.selectedToMove.name);
        //Debug.Log("isSelected: " + gameData.isSelected);
        //Debug.Log("readyToMove: " + gameData.readyToMove);

        GameObject toAppend = findSquare(coords.x, coords.y);
        GameObject pieceOriginalSquare = findSquare(piece.position.x, piece.position.y);

        //before piece is moved
        //Loop through pieces for state check
        List<Piece> piecesOnSquare = getPiecesOnSquareBoardGrid(findSquare(piece.position.x, piece.position.y));
        foreach (Piece pieceOnSquare in piecesOnSquare)
        {
            if (checkState(pieceOnSquare, PieceState.Crook))
            {
                if (piecesOnSquare.Count == 2)
                {
                    removeState(pieceOnSquare, PieceState.Jailed);
                }
            }

            if (checkState(piece, PieceState.Jailer))
            {
                removeState(pieceOnSquare, PieceState.Jailed);
            }
        }

        if (!tempInfo.attackerDied) {
            
            piece.hasMoved = true;
            if (piece.go == null)
            {
                Debug.Log("Piece " + piece.name + " has a null gameobject. Attempting fix");

                piece.go = GameObject.Find(piece.name);
                if (piece.go)
                {
                    piece.go.SetActive(true);
                }
            }

            if (piece.go == null)
            {
                //Piece died during delayed move
                updateBoardGrid(piece.position, piece, "r");
            }
            else
            {
                movePieceBoardGrid(piece, piece.position, coords);
                movePiece(piece, toAppend);
            }

            
        }
        else {
            updateBoardGrid(piece.position, piece, "r");
        }
        tempInfo.attackerDied = false;
        //Debug.Log("Piece " + piece.name + " moved to " + coords[0] + "," + coords[1]);

        if (checkState(piece, PieceState.Piggyback))
        {
            piecesOnSquare = new List<Piece>(getPiecesOnSquareBoardGrid(pieceOriginalSquare));
            foreach (Piece pieceOnSquare in piecesOnSquare)
            {
                if (pieceOnSquare.color == piece.color)
                {
                    Debug.Log(pieceOnSquare.name + " is moved from Piggyback");

                    movePieceBoardGrid(pieceOnSquare, pieceOnSquare.position, coords);
                    pieceOnSquare.hasMoved = true;
                    movePiece(pieceOnSquare, toAppend);
                }
            }
        }

        List<Piece> piecesOnSquare2 = new List<Piece>(getPiecesOnSquareBoardGrid(pieceOriginalSquare));
        foreach (Piece pieceOnSquare in piecesOnSquare2)
        {
            if (checkState(pieceOnSquare, PieceState.Jockey))
            {
                movePieceBoardGrid(pieceOnSquare, pieceOnSquare.position, coords);
                pieceOnSquare.hasMoved = true;
                movePiece(pieceOnSquare, toAppend);
            }
        }

        // After move collateral
        if (checkState(piece, PieceState.Combustable))
        {
            System.Random rand = new System.Random();
            int random = rand.Next(1, 7);

            if (random == 3)
            {
                List<int[]> collateral = null;

                if (isPieceSurroundingState(piece, PieceState.Defuser))
                {
                    collateral = new List<int[]>
                    {
                        new int[] { 0, 0 }
                    };
                }
                else
                {
                    collateral = new List<int[]>
                    {
                        new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 1, -1 },
                        new int[] { -1, 0 }, new int[] { -1, 1 }, new int[] { -1, -1 },
                        new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 0, 0 }
                    };
                }

                for (int i = 0; i < collateral.Count; i++)
                {
                    int[] col_coords = new int[]
                    {
                        piece.position.x + collateral[i][0],
                        piece.position.y + collateral[i][1]
                    };

                    GameObject square = findSquare(col_coords[0], col_coords[1]);

                    if (collateral[i][0] == 0 && collateral[i][1] == 0)
                    {
                        collateralDeath(pieceToList(piece));
                    }

                    if (!square) continue;

                    List<Piece> sqPieces = getPiecesOnSquareBoardGrid(square);

                    if (sqPieces == null || sqPieces.Count == 0) continue;

                    collateralDeath(sqPieces);
                }
            }
        }

        if (checkState(piece, PieceState.Fragile))
        {
            System.Random rand = new System.Random();
            int random = rand.Next(1, 7);

            if (random == 3)
            {
                collateralDeath(pieceToList(piece));
            }
        }

        //Updated Since Gamebot
        //Call Interactive Move Methods Here
        /*
         * if (interactive move) { do stuff } 
         */

        //Check for Pawn Promote
        checkPromote(piece, coords);

        //Add pieces to list
        Piece king;
        if (piece.color == 1)
        {
            king = gameData.blackKing;
        }
        else
        {
            king = gameData.whiteKing;
        }

        //Last minute things
        //Heartbroken King Check
        if (checkState(gameData.whiteKing, PieceState.Heartbroken))
        {
            if (!isPieceTypeOnBoard("q", 1))
            {
                Piece tempKing = Spawnables.create("DepressedKing", 1, false);
                initPiece(tempKing, gameData.whiteKing.position);
                collateralDeath(pieceToList(gameData.whiteKing));
                gameData.whiteKing = tempKing;
            }
        }
        else if (checkState(gameData.blackKing, PieceState.Heartbroken))
        {
            if (!isPieceTypeOnBoard("q", -1))
            {
                Piece tempKing = Spawnables.create("DepressedKing", -1, false);
                initPiece(tempKing, gameData.blackKing.position);
                collateralDeath(pieceToList(gameData.blackKing));
                gameData.blackKing = tempKing;
            }
        }

        // Add stacking states
        piece.states |= tempInfo.stackingStates;
        tempInfo.stackingStates = PieceState.None;

        bool isInCheck = isCheck(king);
        bool isInCheckMate = isCheckMate(king, true);

        Debug.Log("Check: " + isInCheck);
        Debug.Log("Checkmate: " + isInCheckMate);
        gameData.check = isInCheck;

        updatePointsOnBoard();

        if (isInCheckMate)
        {
            //Invoke("toggleCheckmateUI", 1.5f);
        }

        gameData.selectedPiece = null;

        gameData.turn = gameData.turn * -1;

        if (isInCheckMate)
        {
            return 2;
        }
        else if (isInCheck)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void refreshPanelSelected()
    {
        panel.squareImages = generateSidePanelImages(gameData.selected);
        panel.panelPieces = getPiecesOnSquareBoardGrid(gameData.selected);
        panel.RefreshImageGrid();
    }

    public void refreshPanelHungry()
    {
        panel.squareImages = generateSidePanelImagesFromList(gameData.selectedPiece.storage, true);
        panel.panelPieces = gameData.selectedPiece.storage;
        panel.RefreshImageGrid();
    }

    public void abilityHandler()
    {
        if (gameData.abilitySelected == PieceAbilities.Vomit)
        {
            //TODO make it so you can only pass if there are less pieces than spaces
            if (gameData.abilityAdvanceNext)
            {
                refreshPanelHungry();

                gameData.abilityAdvanceNext = false;
                gameData.selectedFromPanel = false;

                List<int[]> tempCoordSet = tempInfo.tempCoordSet;
                tempInfo.tempSquare = hungryPieceNextBarf(gameData.selectedPiece, ref tempCoordSet);
                tempInfo.tempCoordSet = tempCoordSet;

                resetBoardColours();
                highlightSquare(tempInfo.tempSquare, Color.red);

                if (tempInfo.tempSquare == null || gameData.selectedPiece.storage == null || gameData.selectedPiece.storage.Count == 0)
                {
                    gameData.abilitySelected = PieceAbilities.None;
                    gameData.selected = null;
                    resetBoardColours();
                    gameData.turn = gameData.turn * -1;

                    gameData.selectedPiece.storage = new List<Piece>();

                    refreshPanelHungry();

                    gameData.selectedPiece = null;
                }
            }

            if (tempInfo.selectedFromPanel && tempInfo.tempPiece != null)
            {
                //Put tempPiece on Square
                Piece p = tempInfo.tempPiece;

                if (p != null)
                {
                    GameObject s = tempInfo.tempSquare;

                    updateBoardGrid(findCoords(s), p, "a");
                    restorePieceImageToBoard(p);
                    reinitPiece(p, findCoords(s));

                    gameData.selectedPiece.storage.Remove(p);
                }

                gameData.abilityAdvanceNext = true;
                gameData.selectedFromPanel = false;
                tempInfo.tempPiece = null;
                tempInfo.tempSquare = null;
                gameData.selected = null;
            }

            if (tempInfo.passed)
            {
                tempInfo.passed = false;

                gameData.abilityAdvanceNext = true;
                gameData.selectedFromPanel = false;
                tempInfo.tempPiece = null;
                tempInfo.tempSquare = null;
                gameData.selected = null;
            }
        }
        else if (gameData.abilitySelected == PieceAbilities.CastleLeft)
        {
            string color;

            if (gameData.turn == 1)
            {
                color = "w";
            }
            else
            {
                color = "b";
            }

            Piece king = findPieceFromPanelCode(color + "_k1");
            Piece rook = findPieceFromPanelCode(color + "_r1");

            int kingMove = -2;
            int rookMove = 3;

            if (checkState(king, PieceState.Switch))
            {
                kingMove -= 2;
                rookMove++;
            }

            gameData.selectedToMovePiece = king;
            photonView.RPC("MovePieceRPC", RpcTarget.All, king.position, new int[] { king.position.x + kingMove, king.position.y });
            gameData.selectedToMovePiece = rook;
            photonView.RPC("MovePieceRPC", RpcTarget.All, rook.position, new int[] { rook.position.x + rookMove, rook.position.y });

            gameData.abilitySelected = PieceAbilities.None;
            gameData.selected = null;
            resetBoardColours();
            gameData.turn = gameData.turn * -1;

            removeAbility(king, PieceAbilities.CastleLeft);
            removeAbility(king, PieceAbilities.CastleRight);
        }
        else if (gameData.abilitySelected == PieceAbilities.CastleRight)
        {
            string color;

            if (gameData.turn == 1)
            {
                color = "w";
            }
            else
            {
                color = "b";
            }

            Piece king = findPieceFromPanelCode(color + "_k1");
            Piece rook = findPieceFromPanelCode(color + "_r2");

            int kingMove = 2;
            int rookMove = -2;

            if (checkState(king, PieceState.Switch))
            {
                kingMove++;
                rookMove--;
            }

            gameData.selectedToMovePiece = king;
            photonView.RPC("MovePieceRPC", RpcTarget.All, king.position, new int[] { king.position.x + kingMove, king.position.y });
            gameData.selectedToMovePiece = rook;
            photonView.RPC("MovePieceRPC", RpcTarget.All, rook.position, new int[] { rook.position.x + rookMove, rook.position.y });

            gameData.abilitySelected = PieceAbilities.None;
            gameData.selected = null;
            resetBoardColours();
            gameData.turn = gameData.turn * -1;

            removeAbility(king, PieceAbilities.CastleLeft);
            removeAbility(king, PieceAbilities.CastleRight);
        }
        else if (gameData.abilitySelected == PieceAbilities.Freeze)
        {
            if (gameData.abilityAdvanceNext)
            {
                highlightSurroundingSquaresWithPieces(gameData.selectedPiece);

                gameData.abilityAdvanceNext = false;
                gameData.selected = null;
            }
            else if (gameData.selectedPiece != null && tempInfo.tempPiece == gameData.selectedPiece)
            {
                addState(tempInfo.tempPiece, PieceState.Frozen);
                addAbility(tempInfo.tempPiece, PieceAbilities.Unfreeze);

                gameData.abilitySelected = PieceAbilities.None;
                gameData.selected = null;
                resetBoardColours();
                gameData.turn = gameData.turn * -1;
                tempInfo.tempPiece = null;
            }
        }
        else if (gameData.abilitySelected == PieceAbilities.Unfreeze)
        {
            Piece piece = gameData.selectedPiece;
            removeState(piece, PieceState.Frozen);
            removeAbility(piece, PieceAbilities.Unfreeze);

            gameData.abilitySelected = PieceAbilities.None;
            gameData.selected = null;
            resetBoardColours();
            gameData.turn = gameData.turn * -1;
        }
        else if (gameData.abilitySelected == PieceAbilities.Spawn)
        {
            if (gameData.abilityAdvanceNext)
            {
                highlightSurroundingSquaresWithoutPieces(gameData.selectedPiece);

                gameData.abilityAdvanceNext = false;
                gameData.selected = null;
                tempInfo.tempSquare = null;
            }
            else if (tempInfo.tempSquare != null)
            {
                GameObject square = tempInfo.tempSquare;
                string pieceName = tempInfo.tempPiece.spawnable;

                Piece piece = Spawnables.create(pieceName, tempInfo.tempPiece.color, false);
                gameData.selectedPiece.numSpawns--;
                initPiece(piece, findCoords(square));

                gameData.abilitySelected = PieceAbilities.None;
                gameData.selected = null;
                resetBoardColours();
                gameData.turn = gameData.turn * -1;
            }
        }
        else if (gameData.abilitySelected == PieceAbilities.Spit)
        {
            if (gameData.abilityAdvanceNext)
            {
                highlightSurroundingSquaresWithoutPieces(gameData.selectedPiece);
                highlightSurroundingSquaresWithPieces(gameData.selectedPiece);

                gameData.abilityAdvanceNext = false;
                gameData.selected = null;
                tempInfo.tempSquare = null;
            }
            else if (tempInfo.tempSquare != null)
            {
                Piece p = gameData.selectedPiece.storage[0];
                if (p != null)
                {
                    GameObject s = tempInfo.tempSquare;

                    //Todo maybe trigger collateral of killed piece
                    collateralDeath(getPiecesOnSquare(s));

                    reinitPiece(p, findCoords(s));
                    updateBoardGrid(findCoords(s), p, "a");
                    restorePieceImageToBoard(p);

                    gameData.selectedPiece.storage.Remove(p);
                }

                gameData.abilityAdvanceNext = true; //todo is this needed?
                gameData.selectedFromPanel = false;
                tempInfo.tempPiece = null;
                tempInfo.tempSquare = null;
                gameData.selected = null;
                gameData.abilitySelected = PieceAbilities.None;
                gameData.turn = gameData.turn * -1;
                resetBoardColours();
            }
        }
        else if (gameData.abilitySelected == PieceAbilities.Dematerialize)
        {
            Piece piece = gameData.selectedPiece;
            addState(piece, PieceState.Dematerialized);
            removeAbility(piece, PieceAbilities.Dematerialize);
            addAbility(piece, PieceAbilities.Materialize);

            Debug.Log("ABILITY: " + piece.abilities);
            Debug.Log("STATE: " + piece.states);

            gameData.selectedFromPanel = false;
            tempInfo.tempPiece = null;
            tempInfo.tempSquare = null;
            gameData.selected = null;
            gameData.abilitySelected = PieceAbilities.None;
            gameData.turn = gameData.turn * -1;

            Image img = piece.go.GetComponent<Image>();
            Color c = img.color;
            c.a = 0.5f;
            img.color = c;
            resetBoardColours();

        }
        else if (gameData.abilitySelected == PieceAbilities.Materialize)
        {
            Piece piece = gameData.selectedPiece;
            removeState(piece, PieceState.Dematerialized);
            removeAbility(piece, PieceAbilities.Materialize);
            addAbility(piece, PieceAbilities.Dematerialize);

            List<Piece> piecesOnSquare = getPiecesOnSquareBoardGrid(gameData.selected);
            piecesOnSquare.Remove(piece);

            onDeaths(piece, piece.go, gameData.selected);

            gameData.selectedFromPanel = false;
            tempInfo.tempPiece = null;
            tempInfo.tempSquare = null;
            gameData.selected = null;
            gameData.abilitySelected = PieceAbilities.None;
            gameData.turn = gameData.turn * -1;

            Image img = piece.go.GetComponent<Image>();
            Color c = img.color;
            c.a = 1f;
            img.color = c;
            resetBoardColours();
        }
        else if (gameData.abilitySelected == PieceAbilities.Split)
        {
            forceRemove(gameData.selectedPiece);

            Piece piece = Spawnables.create("LeftPawn", tempInfo.tempPiece.color, false);
            initPiece(piece, findCoords(gameData.selected));

            Piece piece2 = Spawnables.create("RightPawn", tempInfo.tempPiece.color, false);
            initPiece(piece2, findCoords(gameData.selected));

            gameData.abilitySelected = PieceAbilities.None;
            gameData.selected = null;
            resetBoardColours();
            gameData.turn = gameData.turn * -1;
        }
    }

    public static void checkPromote(Piece piece, coords coords)
    {
        if (piece.promotesInto != "" && !checkState(piece, PieceState.Dematerialized))
        {
            if (piece.position.y == piece.promotingRow)
            {
                string pname = piece.promotesInto;

                if (nonResettables.ruleset == "Normal")
                {
                    pname = "Queen";
                }

                Piece p = Spawnables.create(pname, piece.color, false);

                if (piece.storage != null)
                {
                    foreach (Piece p_ in piece.storage)
                    {
                        forceRemove(p_);
                    }
                }

                collateralDeath(getPiecesOnSquareBoardGrid(findSquare(piece.position.x, piece.position.y)));
                forceRemove(piece);
                initPiece(p, coords);
            }
        }
    }

    public static void delayedMove(PieceMove pMove)
    {
        Piece piece = pMove.piece;
        coords coords = pMove.coords;

        GameObject square = findSquare(coords.x, coords.y);

        if (true/*!getColorsOnSquare(square, true).Contains(piece.color)*/) {
            bool death;
            bool countDeath;
            GameObject selectedToMoveGo = null;

            if (piece.go == null)
            {
                return; //Delayed Piece Died
            }

            var deathVars = isDeath(selectedToMoveGo, square, piece, true);

            death = deathVars.death;
            selectedToMoveGo = deathVars.selectedToMoveGo;
            countDeath = deathVars.countDeath;

            if (selectedToMoveGo == null || gameData.piecesDict.ContainsKey(selectedToMoveGo))
            {
                Debug.Log("Delayed Move FAILED for: " + piece.name);
                if (death)
                {
                    Piece destroyer = gameData.piecesDict[selectedToMoveGo];

                    onDeaths(destroyer, selectedToMoveGo, square);
                }

                piece.hasMoved = true;
                movePieceBoardGrid(piece, piece.position, coords);
                movePiece(piece, square);

                checkPromote(piece, coords);
            }
        }
    }

    public static (bool death, GameObject selectedToMoveGo, bool countDeath) isDeath(GameObject selectedToMoveGo, GameObject square, Piece piece, bool fromDelayed)
    {
        bool death = false;
        if (square.transform.childCount != 0)
        {
            selectedToMoveGo = piece.go;

            death = true;
            //Debug.Log("Checking for Death: Square: " + square + " SelectedToMoveGo: " + selectedToMoveGo + " " + " Piece: " + piece.name);

            List<Piece> piecesOnSquare = getPiecesOnSquareBoardGrid(square);

            if (!getColorsOnSquare(square, true).Contains(piece.color * -1) && (!checkState(piece, PieceState.Murderous)))
            {
                if ((checkStateOnSquare(piecesOnSquare, PieceState.Jailed) && checkStateOnSquare(piecesOnSquare, PieceState.Jailer))
                    || (checkStateOnSquare(piecesOnSquare, PieceState.Jailed) && checkStateOnSquare(piecesOnSquare, PieceState.Crook)))
                {
                    death = true;
                }
                else
                {
                    death = false;
                }
            }
            else if (checkStateAllOnSquare(piecesOnSquare, PieceState.Dematerialized))
            {
                death = false;
            }
            else if (checkSquareCrowdingEligible(piece, piecesOnSquare))
            {
                death = false;
            }
            else if (checkState(piece, PieceState.Dematerialized))
            {
                death = false;
            }
            else if (checkState(piece, PieceState.Delayed) && !fromDelayed)
            {
                death = false;
            }
        }

        bool countDeath = death;
        if (countDeath)
        {
            if (checkState(piece, PieceState.Jailer))
            {
                countDeath = false;
            }

            if (checkStateAllOnSquare(getPiecesOnSquare(square), PieceState.Crook))
            {
                countDeath = false;
            }

            List<Piece> piecesOnSquare = getPiecesOnSquare(square);
            foreach(Piece p in piecesOnSquare)
            {
                if (p.lives == 0)
                {
                    continue;
                }

                countDeath = false;
            }
        }

        return (death, selectedToMoveGo, countDeath);
    }

    public (bool death, bool countDeath) performPreMove()
    {
        moveSound.Play();

        var deathVars = isDeath(gameData.selectedToMovePiece.go, gameData.selected, gameData.selectedToMovePiece, false);

        bool death = deathVars.death;
        bool countDeath = deathVars.countDeath;
        GameObject selectedToMoveGo = deathVars.selectedToMoveGo;

        if (death && selectedToMoveGo != null)
        {
            //Piece destroyer = gameData.piecesDict[selectedToMoveGo];
            Piece destroyer = gameData.selectedToMovePiece;

            Debug.Log("DESTROYING: Square: " + findCoords(gameData.selected).x + "," + findCoords(gameData.selected).y);
            onDeaths(destroyer, selectedToMoveGo, gameData.selected);
            //photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            //PhotonNetwork.Destroy(gameData.selected.transform.GetChild(0).gameObject);
        }

        return (death, countDeath);
    }
    /*
    public static Piece clonePiece(Piece original)
    {
        Type type = original.GetType();

        Piece clone = (Piece)Activator.CreateInstance(type, original.color, false, true);

        clone.disabled = original.disabled;
        clone.color = original.color;
        clone.points = original.points;
        clone.rarityLevel = original.rarityLevel;
        //clone.startSquare = original.startSquare?.ToArray();
        clone.startSquare = original.startSquare;
        clone.baseType = original.baseType;
        clone.description = original.description;
        clone.longDescription = original.longDescription;
        clone.alive = original.alive;
        clone.lives = original.lives;
        clone.abilities = original.abilities;
        //clone.state = original.state.ToString();
        clone.states = original.states;
        //clone.secondaryState = original.secondaryState.ToString();
        clone.collateralType = original.collateralType;
        //clone.collateral = clone2dArray(original.collateral);
        clone.collateral = original.collateral;
        //clone.size = original.size?.ToArray();
        clone.promotesInto = original.promotesInto.ToString();
        clone.promotingRow = original.promotingRow;
        clone.canMoveTwice = original.canMoveTwice;
        clone.storageLimit = original.storageLimit;
        if (original.storage == null || original.storage.Count == 0)
        {
            clone.storage = null;
        }
        else
        {
            clone.storage = new List<Piece>();
            foreach(Piece p in original.storage)
            {
                if (p.name != original.name)
                {
                    clone.storage.Add(clonePiece(p));
                }
            }
        }

        //clone.dependentAttacks = clone2dArray(original.dependentAttacks);
        //clone.positionIndependentMoves = clone2dArray(original.positionIndependentMoves);

        /*
        clone.moves = clone2dArray(original.moves);
        clone.oneTimeMoves = clone2dArray(original.oneTimeMoves);
        clone.moveAndAttacks = clone2dArray(original.moveAndAttacks);
        clone.oneTimeMoveAndAttacks = clone2dArray(original.oneTimeMoveAndAttacks);
        clone.murderousAttacks = clone2dArray(original.murderousAttacks);
        clone.condition = original.condition;
        clone.conditionalAttacks = clone2dArray(original.conditionalAttacks);
        clone.jumpAttacks = clone2dArray(original.jumpAttacks);
        clone.attacks = clone2dArray(original.attacks);
        clone.forceStayTurnMoves = clone2dArray(original.forceStayTurnMoves);
        clone.flagMove1 = clone2dArray(original.flagMove1);
        clone.flagMove2 = clone2dArray(original.flagMove2);
        clone.pushMoves = clone2dArray(original.pushMoves);
        clone.enPassantMoves = clone2dArray(original.enPassantMoves);
        *//*
        clone.moves = original.moves;
        clone.oneTimeMoves = original.oneTimeMoves;
        clone.moveAndAttacks = original.moveAndAttacks;
        clone.oneTimeMoveAndAttacks = original.oneTimeMoveAndAttacks;
        clone.murderousAttacks = original.murderousAttacks;
        clone.condition = original.condition;
        clone.conditionalAttacks = original.conditionalAttacks;
        clone.jumpAttacks = original.jumpAttacks;
        clone.attacks = original.attacks;
        clone.flagMove1 = original.flagMove1;
        clone.flagMove2 = original.flagMove2;
        clone.pushMoves = original.pushMoves;
        clone.enPassantMoves = original.enPassantMoves;

        clone.position = original.position;
        clone.hasMoved = original.hasMoved;
        //clone.wImage = original.wImage.ToString();
        //clone.bImage = original.bImage.ToString();
        clone.name = original.name.ToString();
        clone.flag = original.flag;
        clone.spawnable = original.spawnable.ToString();
        clone.numSpawns = original.numSpawns;
        clone.go = null;

        return clone;
    }
    */
    public static Piece clonePiece(Piece original)
    {
        Type type = original.GetType();

        Piece clone = (Piece)Activator.CreateInstance(type, original.color, false, true);

        clone.disabled = original.disabled;
        clone.color = original.color;
        clone.points = original.points;
        clone.startSquare = original.startSquare;
        clone.baseType = original.baseType;
        clone.alive = original.alive;
        clone.lives = original.lives;
        clone.abilities = original.abilities;
        clone.states = original.states;
        clone.collateralType = original.collateralType;
        clone.collateral = original.collateral;
        clone.promotesInto = original.promotesInto.ToString();
        clone.promotingRow = original.promotingRow;
        clone.storageLimit = original.storageLimit;
        if (original.storage == null || original.storage.Count == 0)
        {
            clone.storage = null;
        }
        else
        {
            clone.storage = new List<Piece>();
            foreach (Piece p in original.storage)
            {
                if (p.name != original.name)
                {
                    clone.storage.Add(clonePiece(p));
                }
            }
        }

        clone.moves = original.moves;
        clone.oneTimeMoves = original.oneTimeMoves;
        clone.moveAndAttacks = original.moveAndAttacks;
        clone.oneTimeMoveAndAttacks = original.oneTimeMoveAndAttacks;
        clone.murderousAttacks = original.murderousAttacks;
        clone.condition = original.condition;
        clone.conditionalAttacks = original.conditionalAttacks;
        clone.jumpAttacks = original.jumpAttacks;
        clone.attacks = original.attacks;
        clone.flagMove1 = original.flagMove1;
        clone.flagMove2 = original.flagMove2;
        clone.pushMoves = original.pushMoves;
        clone.enPassantMoves = original.enPassantMoves;

        clone.position = original.position;
        clone.hasMoved = original.hasMoved;
        clone.name = original.name.ToString();
        clone.flag = original.flag;
        clone.spawnable = original.spawnable.ToString();
        clone.numSpawns = original.numSpawns;
        clone.go = null;

        return clone;
    }

    public static int[,] clone2dArray(int[,] arr)
    {
        int[,] clone = null;
        if (arr != null)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            clone = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    clone[i, j] = arr[i, j];
                }
            }
        }

        return clone;
    }

    public static void resetGameVars()
    {
        gameData.selected = null;
        gameData.selectedPiece = null;
        gameData.selectedToMovePiece = null;

        gameData.piecesDict = new Dictionary<GameObject, Piece>();
        gameData.allPiecesDict = new Dictionary<GameObject, Piece>();

        gameData.board = null;

        gameData.isSelected = false;
        gameData.selectedFromPanel = false;
        gameData.abilityAdvanceNext = false;
        gameData.refreshedSinceClick = false;

        gameData.turn = 1;
        gameData.readyToMove = false;
        gameData.abilitySelected = PieceAbilities.None;

        gameData.selectedToMove = null;

        gameData.currentMoveableCoords = new List<coords>();
        gameData.currentMoveableCoordsAllPieces = new List<coords>();

        gameData.isInCheck = new int[] { 0, 0 };

        gameData.whiteKing = null;
        gameData.blackKing = null;

        gameData.whiteRooks = new List<Piece>();
        gameData.blackRooks = new List<Piece>();

        gameData.pointsOnBoard = new float[] { 0, 0 };

        gameData.winner = "";

        gameData.isPaused = false;
        gameData.check = false;
        gameData.checkMate = false;
        gameData.staleMate = false;

        gameData.botMove = false;
        gameData.botMoves = new Dictionary<Piece, List<int[]>>();

        gameData.playMode = "";

        gameData.bestMovePiece = null;
        gameData.bestMoveCoords = new int[] { 0, 0 };

        gameData.boardGrid = new List<List<List<Piece>>>();

        gameData.panelCodes = new List<string>();

        gameData.botWhite = null;
        gameData.botBlack = null;

        gameData.isBotMatch = false;

        tempInfo.botMoveOpponentBestPoints = 0;

        tempInfo.tempCoordSet = null;
        tempInfo.tempSquare = null;
        tempInfo.tempPiece = null;

        tempInfo.selectedFromPanel = false;
        tempInfo.passed = false;

        tempInfo.delayedQueue = new DelayedQueue();

        tempInfo.attackerDied = false;
        gameData.helper = null;
    }

    public static bool coordsInList(List<coords> coordsList, coords coords) {
        foreach (coords c in coordsList) {
            if (c.x == coords.x && c.y == coords.y) {
                return true;
            } 
        }

        return false;
    }

    public static bool pieceInList(List<Piece> pieceList, Piece piece) {
        foreach (Piece p in pieceList) {
            if (piece.name == p.name) {
                return true;
            } 
        }

        return false;
    }

    public void addBotMessage(string message)
    {
        panel.AddBotMessage(message);
    }

    public static NextMove thread_nextMove(NextMove nm, BoardState cloneState)
    {
        if (nm == null) return null;

        if (nm.moveType == "move" && nm.move != null)
        {
            Piece newPiece = BotHelperFunctions.getCloneFromOriginalPiece(nm.move.p, cloneState.boardGrid);

            coords newCoords = new coords(-1, -1);
            if (nm.move.coords.x != -1)
            {
                newCoords = new coords( nm.move.coords.x, nm.move.coords.y );
            }

            Move newMove = new Move(newPiece, newCoords);
            return new NextMove(newMove);
        }

        if (nm.moveType == "ability" && nm.ability != null)
        {
            BotHelperFunctions.PieceAbility old = nm.ability;

            Piece newMainPiece = BotHelperFunctions.getCloneFromOriginalPiece(old.piece, cloneState.boardGrid);
            Piece newSecondPiece = BotHelperFunctions.getCloneFromOriginalPiece(old.secondPiece, cloneState.boardGrid);

            coords newCoords = old.coords;

            List<Piece> newPlacePieces = null;
            if (old.placePieces != null)
            {
                newPlacePieces = new List<Piece>();
                foreach (Piece p in old.placePieces)
                {
                    newPlacePieces.Add(BotHelperFunctions.getCloneFromOriginalPiece(p, cloneState.boardGrid));
                }
            }

            List<coords> newPlaceCoords = null;
            if (old.placeCoords != null)
            {
                newPlaceCoords = new List<coords>();
                foreach (coords c in old.placeCoords)
                {
                    newPlaceCoords.Add(new coords( c.x, c.y ));
                }
            }

            BotHelperFunctions.PieceAbility newAbility = new BotHelperFunctions.PieceAbility(
                newMainPiece,
                old.ability,
                newCoords,
                newPlacePieces,
                newPlaceCoords,
                newSecondPiece
            );

            return new NextMove(newAbility);
        }

        return null;
    }

    public static PieceAbilities[] getAllAbilities(PieceAbilities abilities)
    {
        List<PieceAbilities> singleAbilities = new List<PieceAbilities>();

        foreach (PieceAbilities ability in System.Enum.GetValues(typeof(PieceAbilities)))
        {
            if (ability == PieceAbilities.None)
                continue;

            if ((abilities & ability) == ability)
            {
                singleAbilities.Add(ability);
            }
        }

        return singleAbilities.ToArray();
    }
}

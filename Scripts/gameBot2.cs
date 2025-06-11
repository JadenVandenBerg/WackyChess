using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Linq;

public class gameBot2 : MonoBehaviour
{
    public GameObject board2;
    public GameObject boardWrapper;
    public GameObject checkmateUI;

    public Pawn pawn, pawn2, pawn3, pawn4, pawn5, pawn6, pawn7, pawn8;
    public Pawn bpawn, bpawn2, bpawn3, bpawn4, bpawn5, bpawn6, bpawn7, bpawn8;
    public Rook wRook, wRook2, bRook, bRook2;
    public Bishop wBishop, wBishop2, bBishop, bBishop2;
    public Knight wKnight, wKnight2, bKnight, bKnight2;
    public Queen wQueen, bQueen;
    public King wKing, bKing;

    public GameObject wp, bp, t, a;

    private AudioSource moveSound;

    void Start()
    {
        gameData.playMode = "Bot2";
        gameData.turn = 1;
        gameData.board = board2;

        moveSound = GetComponent<AudioSource>();

        pawn = new Pawn(1, false);
        pawn2 = new Pawn(1, false);
        pawn3 = new Pawn(1, false);
        pawn4 = new Pawn(1, false);
        pawn5 = new Pawn(1, false);
        pawn6 = new Pawn(1, false);
        pawn7 = new Pawn(1, false);
        pawn8 = new Pawn(1, false);

        gameData.piecesDict.Add(pawn.go, pawn);
        gameData.piecesDict.Add(pawn2.go, pawn2);
        gameData.piecesDict.Add(pawn3.go, pawn3);
        gameData.piecesDict.Add(pawn4.go, pawn4);
        gameData.piecesDict.Add(pawn5.go, pawn5);
        gameData.piecesDict.Add(pawn6.go, pawn6);
        gameData.piecesDict.Add(pawn7.go, pawn7);
        gameData.piecesDict.Add(pawn8.go, pawn8);

        wRook = new Rook(1, false);
        wRook2 = new Rook(1, false);
        wBishop = new Bishop(1, false);
        wBishop2 = new Bishop(1, false);
        wKnight = new Knight(1, false);
        wKnight2 = new Knight(1, false);
        wQueen = new Queen(1, false);
        wKing = new King(1, false);

        gameData.piecesDict.Add(wRook.go, wRook);
        gameData.piecesDict.Add(wRook2.go, wRook2);
        gameData.piecesDict.Add(wBishop.go, wBishop);
        gameData.piecesDict.Add(wBishop2.go, wBishop2);
        gameData.piecesDict.Add(wKnight.go, wKnight);
        gameData.piecesDict.Add(wQueen.go, wQueen);
        gameData.piecesDict.Add(wKing.go, wKing);
        gameData.piecesDict.Add(wKnight2.go, wKnight2);

        bpawn = new Pawn(-1, false);
        bpawn2 = new Pawn(-1, false);
        bpawn3 = new Pawn(-1, false);
        bpawn4 = new Pawn(-1, false);
        bpawn5 = new Pawn(-1, false);
        bpawn6 = new Pawn(-1, false);
        bpawn7 = new Pawn(-1, false);
        bpawn8 = new Pawn(-1, false);

        gameData.piecesDict.Add(bpawn.go, bpawn);
        gameData.piecesDict.Add(bpawn2.go, bpawn2);
        gameData.piecesDict.Add(bpawn3.go, bpawn3);
        gameData.piecesDict.Add(bpawn4.go, bpawn4);
        gameData.piecesDict.Add(bpawn5.go, bpawn5);
        gameData.piecesDict.Add(bpawn6.go, bpawn6);
        gameData.piecesDict.Add(bpawn7.go, bpawn7);
        gameData.piecesDict.Add(bpawn8.go, bpawn8);

        bRook = new Rook(-1, false);
        bRook2 = new Rook(-1, false);
        bBishop = new Bishop(-1, false);
        bBishop2 = new Bishop(-1, false);
        bKnight = new Knight(-1, false);
        bKnight2 = new Knight(-1, false);
        bQueen = new Queen(-1, false);
        bKing = new King(-1, false);

        gameData.piecesDict.Add(bRook.go, bRook);
        gameData.piecesDict.Add(bRook2.go, bRook2);
        gameData.piecesDict.Add(bBishop.go, bBishop);
        gameData.piecesDict.Add(bBishop2.go, bBishop2);
        gameData.piecesDict.Add(bKnight.go, bKnight);
        gameData.piecesDict.Add(bQueen.go, bQueen);
        gameData.piecesDict.Add(bKing.go, bKing);
        gameData.piecesDict.Add(bKnight2.go, bKnight2);

        initPiece(pawn, new int[] { 1, 2 });
        initPiece(pawn2, new int[] { 2, 2 });
        initPiece(pawn3, new int[] { 3, 2 });
        initPiece(pawn4, new int[] { 4, 2 });
        initPiece(pawn5, new int[] { 5, 2 });
        initPiece(pawn6, new int[] { 6, 2 });
        initPiece(pawn7, new int[] { 7, 2 });
        initPiece(pawn8, new int[] { 8, 2 });

        initPiece(wRook, new int[] { 1, 1 });
        initPiece(wRook2, new int[] { 8, 1 });
        initPiece(wBishop, new int[] { 3, 1 });
        initPiece(wBishop2, new int[] { 6, 1 });
        initPiece(wKnight, new int[] { 2, 1 });
        initPiece(wKnight2, new int[] { 7, 1 });
        initPiece(wQueen, new int[] { 4, 1 });
        initPiece(wKing, new int[] { 5, 1 });

        initPiece(bpawn, new int[] { 1, 7 });
        initPiece(bpawn2, new int[] { 2, 7 });
        initPiece(bpawn3, new int[] { 3, 7 });
        initPiece(bpawn4, new int[] { 4, 7 });
        initPiece(bpawn5, new int[] { 5, 7 });
        initPiece(bpawn6, new int[] { 6, 7 });
        initPiece(bpawn7, new int[] { 7, 7 });
        initPiece(bpawn8, new int[] { 8, 7 });

        initPiece(bRook, new int[] { 1, 8 });
        initPiece(bRook2, new int[] { 8, 8 });
        initPiece(bBishop, new int[] { 3, 8 });
        initPiece(bBishop2, new int[] { 6, 8 });
        initPiece(bKnight, new int[] { 2, 8 });
        initPiece(bKnight2, new int[] { 7, 8 });
        initPiece(bQueen, new int[] { 4, 8 });
        initPiece(bKing, new int[] { 5, 8 });

        gameData.whiteRooks.Add(wRook);
        gameData.whiteRooks.Add(wRook2);

        gameData.blackRooks.Add(bRook);
        gameData.blackRooks.Add(bRook2);

        gameData.whiteKing = wKing;
        gameData.blackKing = bKing;

        pawn.go.tag = "Pawn";
        pawn2.go.tag = "Pawn";
        pawn3.go.tag = "Pawn";
        pawn4.go.tag = "Pawn";
        pawn5.go.tag = "Pawn";
        pawn6.go.tag = "Pawn";
        pawn7.go.tag = "Pawn";
        pawn8.go.tag = "Pawn";
        bpawn.go.tag = "Pawn";
        bpawn2.go.tag = "Pawn";
        bpawn3.go.tag = "Pawn";
        bpawn4.go.tag = "Pawn";
        bpawn5.go.tag = "Pawn";
        bpawn6.go.tag = "Pawn";
        bpawn7.go.tag = "Pawn";
        bpawn8.go.tag = "Pawn";

        wBishop.go.tag = "Bishop";
        wBishop2.go.tag = "Bishop";
        wRook.go.tag = "Rook";
        wRook2.go.tag = "Rook";
        wKing.go.tag = "King";
        wQueen.go.tag = "Queen";
        wKnight.go.tag = "Knight";
        wKnight2.go.tag = "Knight";
        bBishop.go.tag = "Bishop";
        bBishop2.go.tag = "Bishop";
        bRook.go.tag = "Rook";
        bRook2.go.tag = "Rook";
        bKing.go.tag = "King";
        bQueen.go.tag = "Queen";
        bKnight.go.tag = "Knight";
        bKnight2.go.tag = "Knight";


        //HelperFunctions.updatePointsOnBoard(wp, bp, t, a);
    }

    void Update()
    {
        if (!gameData.isSelected)
        {
            HelperFunctions.resetBoardColours();
        }

        if (!gameData.readyToMove && gameData.selected && gameData.selected.transform.childCount != 0)
        {
            Piece currentPiece = HelperFunctions.getPieceOnSquare(gameData.selected);
            int currentColor = currentPiece.color;
            if (gameData.selected.transform.GetChild(0).gameObject != null && currentColor == gameData.turn && gameData.turn == 1)
            {
                gameData.readyToMove = true;

                HelperFunctions.updateCastleCondition();
                HelperFunctions.addToCurrentMoveableCoordsTotal(currentColor, true, true, currentPiece, true, true);
            }
        }

        if (gameData.readyToMove && gameData.isSelected && gameData.selected && gameData.selectedToMove && HelperFunctions.getPieceOnSquare(gameData.selectedToMove) != null)
        {
            //Debug.Log("Found Move? " + HelperFunctions.findCoords(gameData.selected)[0] + "," + HelperFunctions.findCoords(gameData.selected)[1] + " : " + isInList(gameData.currentMoveableCoords, HelperFunctions.findCoords(gameData.selected)));

            if (gameData.turn == HelperFunctions.getPieceOnSquare(gameData.selectedToMove).color && gameData.turn == 1)
            {
                if (HelperFunctions.isInList(gameData.currentMoveableCoords, HelperFunctions.findCoords(gameData.selected), false))
                {
                    moveSound.Play();

                    if (gameData.selected.transform.childCount != 0)
                    {
                        //Piece toDestroy = gameData.piecesDict[gameData.selected.transform.GetChild(0).gameObject];
                        if (gameData.piecesDict.ContainsKey(gameData.selected.transform.GetChild(0).gameObject))
                        {
                            gameData.piecesDict.Remove(gameData.selected.transform.GetChild(0).gameObject);
                        }
                        //Debug.Log("DESTROYING: " + toDestroy.name + ". Square: " + HelperFunctions.findCoords(gameData.selected)[0] + "," + HelperFunctions.findCoords(gameData.selected)[1]);
                        Destroy(gameData.selected.transform.GetChild(0).gameObject);
                        //toDestroy = null;
                    }

                    movePiece(HelperFunctions.getPieceOnSquare(gameData.selectedToMove), HelperFunctions.findCoords(gameData.selected));
                }
            }

            HelperFunctions.resetBoardColours();
            gameData.readyToMove = false;
            gameData.isSelected = false;
            gameData.selected = null;
        }

        
        Invoke("botPieceMoveInvokee", Time.deltaTime);
    }

    public void botPieceMoveInvokee()
    {
        if (gameData.turn == -1 && gameData.botMove == false)
        {

            gameData.botMove = true;

            HelperFunctions.updateBotMoves();

            bool isMoveable = false;

            if (HelperFunctions.isCheckMate(gameData.blackKing, true))
            {
                isMoveable = true;
            }

            if (!isMoveable)
            {
                float bestPointsCaptured = -100;
                List<Dictionary<Piece, int[]>> bestMovesTieList = new List<Dictionary<Piece, int[]>>();

                foreach (Piece piece in gameData.piecesDict.Values)
                {
                    if (piece.color == 1)
                    {
                        continue;
                    }

                    foreach (int[] move in gameData.botMoves[piece])
                    {
                        bool check = HelperFunctions.dummyMove(piece, move);

                        if (!check)
                        {
                            float points = 0;

                            //Save Piece
                            int[] oldPosition = piece.getPosition();

                            //Change Coords
                            //piece.setPosition(move);

                            Piece onSquare = HelperFunctions.getPieceOnSquare(HelperFunctions.findSquare(move[0], move[1]));

                            if (onSquare != null)
                            {
                                points = onSquare.points;
                                onSquare.disabled = true;
                                onSquare.go.SetActive(false);
                            }

                            HelperFunctions.movePiece(piece, HelperFunctions.findSquare(move[0], move[1]));
                            Invoke("allOpponentMoves", Time.deltaTime);

                            Thread.Sleep(500);
                            points -= tempInfo.botMoveOpponentBestPoints;
                            Debug.Log("my best move if i move " + piece.name + "is worth: " + points);

                            Dictionary<Piece, int[]> pieceMovePair = new Dictionary<Piece, int[]>
                            {
                                { piece, move }
                            };

                            if (onSquare != null && gameData.piecesDict.ContainsKey(onSquare.go))
                            {
                                if (bestPointsCaptured < points)
                                {
                                    bestMovesTieList.Clear();
                                    bestPointsCaptured = points;

                                    bestMovesTieList.Add(pieceMovePair);
                                } else
                                {
                                    bestMovesTieList.Add(pieceMovePair);
                                }
                            }
                            else if (bestPointsCaptured == -100)
                            {
                                bestMovesTieList.Add(pieceMovePair);
                            }

                            if (onSquare != null)
                            {
                                onSquare.go.SetActive(true);
                                onSquare.disabled = false;
                            }
                            HelperFunctions.movePiece(piece, HelperFunctions.findSquare(oldPosition[0], oldPosition[1]));
                        }
                    }
                }

                System.Random rand = new System.Random();
                Dictionary<Piece, int[]> winnerPieceMovePair = bestMovesTieList[rand.Next(bestMovesTieList.Count)];

                Piece onSquareMove = HelperFunctions.getPieceOnSquare(HelperFunctions.findSquare(winnerPieceMovePair.First().Value[0], winnerPieceMovePair.First().Value[1]));
                if (onSquareMove != null && gameData.piecesDict.ContainsKey(onSquareMove.go))
                {
                    gameData.piecesDict.Remove(onSquareMove.go);
                    Destroy(onSquareMove.go);
                }


                movePiece(winnerPieceMovePair.First().Key, winnerPieceMovePair.First().Value);

                gameData.botMove = false;
                gameData.turn = 1;
            }
        }
    }

    private void allOpponentMoves()
    {
        float bestOpponentMove = 0f;
        foreach (Piece piece in gameData.piecesDict.Values)
        {
            if (piece.color == -1)
            {
                continue;
            }

            //Check all moves for points
            List<int[]> moves = HelperFunctions.addMovesToCurrentMoveableCoords(piece);
            foreach (int[] move in moves)
            {
                Piece capturedPiece = HelperFunctions.getPieceOnSquare(HelperFunctions.findSquare(move[0], move[1]));
                if (capturedPiece != null && capturedPiece.points > bestOpponentMove)
                {
                    bestOpponentMove = capturedPiece.points;
                }
            }
        }

        tempInfo.botMoveOpponentBestPoints = bestOpponentMove;
    }

    private void movePiece(Piece piece, int[] coords)
    {

        //Debug.Log("Flags");
        //if (gameData.selected) Debug.Log("Selected: " + gameData.selected.name);
        //if (gameData.selectedToMove) Debug.Log("SelectedToMove: " + gameData.selectedToMove.name);
        //Debug.Log("isSelected: " + gameData.isSelected);
        //Debug.Log("readyToMove: " + gameData.readyToMove);

        GameObject toAppend = HelperFunctions.findSquare(coords[0], coords[1]);

        //Check for castle (maybe make helper func)
        int xDisp = piece.position[0] - coords[0];
        if ((piece == wKing || piece == bKing) && (xDisp == 2 || xDisp == -2))
        {
            //Debug.Log("Castle Taken Place");
            if (piece == wKing && xDisp == 2)
            {
                gameData.whiteRooks[0].hasMoved = true;
                HelperFunctions.movePiece(gameData.whiteRooks[0], HelperFunctions.findSquare(4, 1));
                gameData.whiteRooks[0].position = new int[] { 4, 1 };
            }
            else if (piece == wKing && xDisp == -2)
            {
                gameData.whiteRooks[1].hasMoved = true;
                HelperFunctions.movePiece(gameData.whiteRooks[1], HelperFunctions.findSquare(6, 1));
                gameData.whiteRooks[1].position = new int[] { 6, 1 };
            }
            else if (piece == bKing && xDisp == 2)
            {
                gameData.blackRooks[0].hasMoved = true;
                HelperFunctions.movePiece(gameData.blackRooks[0], HelperFunctions.findSquare(4, 8));
                gameData.blackRooks[0].position = new int[] { 4, 8 };
            }
            else if (piece == bKing && xDisp == -2)
            {
                gameData.blackRooks[1].hasMoved = true;
                HelperFunctions.movePiece(gameData.blackRooks[1], HelperFunctions.findSquare(6, 8));
                gameData.blackRooks[0].position = new int[] { 6, 8 };
            }
        }

        piece.position = HelperFunctions.findCoords(toAppend);
        piece.hasMoved = true;

        HelperFunctions.movePiece(piece, toAppend);

        //Check for Pawn Promote
        if (piece.go.tag == "Pawn")
        {
            if (piece.color == 1 && piece.position[1] == 8)
            {
                Destroy(piece.go);
                Piece superPawn = new SuperPawn(1, false);
                initPiece(superPawn, coords);
                superPawn.go.tag = "SuperPawn";

                gameData.piecesDict.Add(superPawn.go, superPawn);
            }
            else if (piece.color == -1 && piece.position[1] == 1)
            {
                Destroy(piece.go);
                Piece superPawn = new SuperPawn(-1, false);
                initPiece(superPawn, coords);
                superPawn.go.tag = "SuperPawn";

                gameData.piecesDict.Add(superPawn.go, superPawn);
            }
        }

        gameData.turn = gameData.turn * -1;

        //Add pieces to list
        Piece king;
        if (piece.color == 1)
        {
            king = bKing;
        }
        else
        {
            king = wKing;
        }

        bool isInCheck = HelperFunctions.isCheck(king);
        bool isInCheckMate;
        isInCheckMate = HelperFunctions.isCheckMate(king, true);

        Debug.Log("Check: " + isInCheck);
        Debug.Log("Checkmate: " + isInCheckMate);
        gameData.check = isInCheck;

        //HelperFunctions.updatePointsOnBoard(wp, bp, t, a);

        if (isInCheckMate)
        {
            Invoke("toggleCheckmateUI", 1.5f);
        }
    }

    public void toggleCheckmateUI()
    {
        checkmateUI.SetActive(true);
    }

    private void initPiece(Piece piece, int[] coords)
    {
        GameObject toAppend = HelperFunctions.findSquare(coords[0], coords[1]);
        piece.position = HelperFunctions.findCoords(toAppend);
        HelperFunctions.movePiece(piece, toAppend);
    }
}


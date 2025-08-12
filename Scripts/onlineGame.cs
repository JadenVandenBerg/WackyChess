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
using Photon.Pun;

public class onlineGame : MonoBehaviour
{
    public GameObject board2;
    public GameObject boardWrapper;
    public GameObject checkmateUI;

    public Piece pawn, pawn2, pawn3, pawn4, pawn5, pawn6, pawn7, pawn8;
    public Piece bpawn, bpawn2, bpawn3, bpawn4, bpawn5, bpawn6, bpawn7, bpawn8;
    public Piece wRook, wRook2, bRook, bRook2;
    public Piece wBishop, wBishop2, bBishop, bBishop2;
    public Piece wKnight, wKnight2, bKnight, bKnight2;
    public Piece wQueen, bQueen;
    public Piece wKing, bKing;

    public PhotonView photonView;

    //public GameObject wp, bp, t, a;
    public SidePanelAdjust panel;

    private AudioSource moveSound;

    IEnumerator Start()
    {
        gameData.playMode = "2Player";
        gameData.turn = 1;
        gameData.board = board2;

        moveSound = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();

        gameData.boardGrid = HelperFunctions.initBoardGrid();

        pawn = new FreezingBishop(1, true);
        pawn2 = new CrowdingKnight(1, true);
        pawn3 = new Pawn(1, true);
        pawn4 = new RoyalKnight(1, true);
        pawn5 = new Pawn(1, true);
        pawn6 = new HungryKnight(1, true);
        pawn7 = new LandminePawn(1, true);
        pawn8 = new LandminePawn(1, true);

        wRook = new FragileRook(1, true);
        wRook2 = new Empress(1, true);
        wBishop = new FragileBishop(1, true);
        wBishop2 = new ColorChangingBishop(1, true);
        wKnight = new FragileKnight(1, true);
        wKnight2 = new LandmineKnight(1, true);
        wQueen = new ReverseMinister(1, true);
        wKing = new HungryKing(1, true);

        bpawn = new SuperGhostPawn(-1, true);
        bpawn2 = new LandminePawn(-1, true);
        bpawn3 = new LandminePawn(-1, true);
        bpawn4 = new LandminePawn(-1, true);
        bpawn5 = new LandminePawn(-1, true);
        bpawn6 = new LandminePawn(-1, true);
        bpawn7 = new LandminePawn(-1, true);
        bpawn8 = new LandminePawn(-1, true);

        bRook = new InfiniteRook(-1, true);
        bRook2 = new InfiniteRook(-1, true);
        bBishop = new InfiniteBishop(-1, true);
        bBishop2 = new InfiniteBishop(-1, true);
        bKnight = new Knight(-1, true);
        bKnight2 = new InfiniteKnight(-1, true);
        bQueen = new InfiniteQueen(-1, true);
        bKing = new HyperFastKing(-1, true);

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

        //This system will need to change once players can get more pieces, keep a tally of pieces using game vars
        pawn.name = "w_p1";
        pawn2.name = "w_p2";
        pawn3.name = "w_p3";
        pawn4.name = "w_p4";
        pawn5.name = "w_p5";
        pawn6.name = "w_p6";
        pawn7.name = "w_p7";
        pawn8.name = "w_p8";
        wRook.name = "w_r1";
        wRook2.name = "w_r2";
        wBishop.name = "w_b1";
        wBishop2.name = "w_b2";
        wKnight.name = "w_n1";
        wKnight2.name = "w_n2";
        wKing.name = "w_k1";
        wQueen.name = "w_q1";

        bpawn.name = "b_p1";
        bpawn2.name = "b_p2";
        bpawn3.name = "b_p3";
        bpawn4.name = "b_p4";
        bpawn5.name = "b_p5";
        bpawn6.name = "b_p6";
        bpawn7.name = "b_p7";
        bpawn8.name = "b_p8";
        bRook.name = "b_r1";
        bRook2.name = "b_r2";
        bBishop.name = "b_b1";
        bBishop2.name = "b_b2";
        bKnight.name = "b_n1";
        bKnight2.name = "b_n2";
        bKing.name = "b_k1";
        bQueen.name = "b_q1";

        gameData.whiteRooks.Add(wRook);
        gameData.whiteRooks.Add(wRook2);

        gameData.blackRooks.Add(bRook);
        gameData.blackRooks.Add(bRook2);

        gameData.whiteKing = wKing;
        gameData.blackKing = bKing;

        panel.Initialize();

        yield return null;
        HelperFunctions.updatePointsOnBoard(panel);
    }

    void Update()
    {
        if (!gameData.isSelected && gameData.abilitySelected == "")
        {
            HelperFunctions.resetBoardColours();
        }

        if (!gameData.readyToMove && gameData.selected && gameData.selected.transform.childCount != 0)
        {
            Piece currentPiece = gameData.selectedPiece;
            int currentColor = currentPiece.color;
            if ((gameData.selected.transform.GetChild(0).gameObject != null && currentColor == gameData.turn) || gameData.selectedFromPanel)
            {
                if (!HelperFunctions.isMultipleOnSquare(gameData.selected) && gameData.abilitySelected == "")
                {
                    gameData.readyToMove = true;

                    //HelperFunctions.updateCastleCondition();
                    HelperFunctions.addToCurrentMoveableCoordsTotal(currentColor, true, true, currentPiece, true, true);
                }
                else
                {
                    //TODO: Force selection from side panel
                    if (gameData.selectedFromPanel)
                    {
                        gameData.readyToMove = true;

                        //HelperFunctions.updateCastleCondition();
                        HelperFunctions.addToCurrentMoveableCoordsTotal(currentColor, true, true, currentPiece, true, true);
                    }
                    //Debug.Log("PANEL");
                    //Debug.Log(gameData.selected);
                    //Debug.Log(gameData.selectedPiece.name);
                    //Debug.Log(gameData.readyToMove);
                    //Debug.Log(gameData.selectedToMove);
                }

                if (!gameData.refreshedSinceClick)
                {
                    gameData.refreshedSinceClick = true;
                    panel.squareImages = HelperFunctions.generateSidePanelImages(gameData.selected);
                    panel.panelPieces = HelperFunctions.getPiecesOnSquareBoardGrid(gameData.selected);
                    panel.RefreshImageGrid();
                }
            }
        }

        Debug.Log("ISREADY");
        Debug.Log("ReadyToMove: " + gameData.readyToMove);
        Debug.Log("Selected: " + gameData.selected);
        if (gameData.selectedPiece != null) Debug.Log("SelectedPiece: " + gameData.selectedPiece.name);
        Debug.Log("SelectedToMove: " + gameData.selectedToMove);
        Debug.Log("Ability: " + gameData.abilitySelected);

        //MOVE
        if (gameData.readyToMove && gameData.isSelected && gameData.selected && gameData.selectedToMove && HelperFunctions.isPieceOnSquare(gameData.selectedToMove))
        {
            //Debug.Log("READY TO MOVE");
            //Debug.Log("Found Move? " + HelperFunctions.findCoords(gameData.selected)[0] + "," + HelperFunctions.findCoords(gameData.selected)[1] + " : " + isInList(gameData.currentMoveableCoords, HelperFunctions.findCoords(gameData.selected)));

            if (gameData.selectedToMovePiece.color == gameData.turn)
            {
                if (HelperFunctions.isInList(gameData.currentMoveableCoords, HelperFunctions.findCoords(gameData.selected), false))
                {
                    moveSound.Play();

                    bool death = false;
                    GameObject selectedGo = null;
                    GameObject selectedToMoveGo = null;

                    if (gameData.selected.transform.childCount != 0)
                    {
                        selectedGo = gameData.selected.transform.GetChild(0).gameObject;
                        selectedToMoveGo = gameData.selectedToMove.transform.GetChild(0).gameObject;

                        death = true;
                        if (!HelperFunctions.getColorsOnSquare(gameData.selected).Contains(gameData.selectedToMovePiece.color * -1))
                        {
                            death = false;
                        }
                    }

                    Debug.Log(death);
                    if (death)
                    {
                        Piece destroyer = gameData.piecesDict[selectedToMoveGo];

                        //Debug.Log("DESTROYING: " + toDestroy.name + ". Square: " + HelperFunctions.findCoords(gameData.selected)[0] + "," + HelperFunctions.findCoords(gameData.selected)[1]);
                        HelperFunctions.onDeaths(destroyer, selectedToMoveGo, gameData.selected);
                        //photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
                        //PhotonNetwork.Destroy(gameData.selected.transform.GetChild(0).gameObject);
                    }

                    photonView.RPC("MovePieceRPC", RpcTarget.All, HelperFunctions.findCoords(gameData.selectedToMove), HelperFunctions.findCoords(gameData.selected));
                    //movePiece(gameData.selectedPiece, HelperFunctions.findCoords(gameData.selected));
                }
            }

            HelperFunctions.resetBoardColours();
            gameData.readyToMove = false;
            gameData.isSelected = false;
            gameData.selectedFromPanel = false;
            gameData.selected = null;
        }

        //ABILITY
        if (gameData.abilitySelected != "" && gameData.selectedPiece != null)
        {
            if (gameData.abilitySelected == "Vomit")
            {
                if (gameData.abilityAdvanceNext)
                {
                    Debug.Log("Advancing Next Vomit");
                    panel.squareImages = HelperFunctions.generateSidePanelImagesFromList(gameData.selectedPiece.storage, true);
                    panel.panelPieces = gameData.selectedPiece.storage;
                    panel.RefreshImageGrid();

                    gameData.abilityAdvanceNext = false;
                    gameData.selectedFromPanel = false;

                    List<int[]> tempCoordSet = tempInfo.tempCoordSet;
                    tempInfo.tempSquare = HelperFunctions.hungryPieceNextBarf(gameData.selectedPiece, ref tempCoordSet);
                    tempInfo.tempCoordSet = tempCoordSet;

                    HelperFunctions.resetBoardColours();
                    HelperFunctions.highlightSquare(tempInfo.tempSquare, Color.red);

                    Debug.Log(tempInfo.tempSquare);
                    if (tempInfo.tempSquare == null || gameData.selectedPiece.storage == null || gameData.selectedPiece.storage.Count == 0)
                    {
                        gameData.abilitySelected = "";
                        gameData.selected = null;
                        HelperFunctions.resetBoardColours();
                        gameData.turn = gameData.turn * -1;

                        gameData.selectedPiece.storage = new List<Piece>();

                        panel.squareImages = HelperFunctions.generateSidePanelImagesFromList(gameData.selectedPiece.storage, true);
                        panel.panelPieces = gameData.selectedPiece.storage;
                        panel.RefreshImageGrid();

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

                        HelperFunctions.updateBoardGrid(HelperFunctions.findCoords(s), p, "a");
                        HelperFunctions.restorePieceImageToBoard(p);
                        initPiece(p, HelperFunctions.findCoords(s));

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
            else if (gameData.abilitySelected == "CastleLeft")
            {
                string color;

                if (gameData.turn == 1)
                {
                    color = "w";
                }
                else
                {
                    color = "r";
                }

                Piece king = HelperFunctions.findPieceFromPanelCode(color + "_k1");
                Piece rook = HelperFunctions.findPieceFromPanelCode(color + "_r1");

                gameData.selectedToMovePiece = king;
                photonView.RPC("MovePieceRPC", RpcTarget.All, king.position, new int[] { king.position[0] - 2, king.position[1] });
                gameData.selectedToMovePiece = rook;
                photonView.RPC("MovePieceRPC", RpcTarget.All, rook.position, new int[] { rook.position[0] + 3, rook.position[1] });

                gameData.abilitySelected = "";
                gameData.selected = null;
                HelperFunctions.resetBoardColours();
                gameData.turn = gameData.turn * -1;
            }
            else if (gameData.abilitySelected == "CastleRight")
            {
                string color;

                if (gameData.turn == 1)
                {
                    color = "w";
                }
                else
                {
                    color = "r";
                }

                Piece king = HelperFunctions.findPieceFromPanelCode(color + "_k1");
                Piece rook = HelperFunctions.findPieceFromPanelCode(color + "_r2");

                gameData.selectedToMovePiece = king;
                photonView.RPC("MovePieceRPC", RpcTarget.All, king.position, new int[] { king.position[0] + 2, king.position[1] });
                gameData.selectedToMovePiece = rook;
                photonView.RPC("MovePieceRPC", RpcTarget.All, rook.position, new int[] { rook.position[0] - 2, rook.position[1] });

                gameData.abilitySelected = "";
                gameData.selected = null;
                HelperFunctions.resetBoardColours();
                gameData.turn = gameData.turn * -1;
            }
            else if (gameData.abilitySelected == "Freeze")
            {
                if (gameData.abilityAdvanceNext)
                {
                    HelperFunctions.highlightSurroundingSquaresWithPieces(gameData.selectedPiece);

                    gameData.abilityAdvanceNext = false;
                    gameData.selected = null;
                }
                else if (gameData.selectedPiece != null && tempInfo.tempPiece == gameData.selectedPiece)
                {
                    HelperFunctions.addState(tempInfo.tempPiece, "Frozen");
                    HelperFunctions.addAbility(tempInfo.tempPiece, "Unfreeze");

                    gameData.abilitySelected = "";
                    gameData.selected = null;
                    HelperFunctions.resetBoardColours();
                    gameData.turn = gameData.turn * -1;
                    tempInfo.tempPiece = null;
                }
            }
            else if (gameData.abilitySelected == "Unfreeze")
            {
                Piece piece = gameData.selectedPiece;

                piece.state = piece.state.Replace("-Frozen", "");
                piece.state = piece.state.Replace("Frozen", "");
                piece.secondaryState = piece.secondaryState.Replace("-Frozen", "");
                piece.secondaryState = piece.secondaryState.Replace("Frozen", "");
                piece.ability = piece.ability.Replace("-Unfreeze", "");
                piece.ability = piece.ability.Replace("Unfreeze", "");

                gameData.abilitySelected = "";
                gameData.selected = null;
                HelperFunctions.resetBoardColours();
                gameData.turn = gameData.turn * -1;
            }
            else if (gameData.abilitySelected == "Spawn")
            {
                GameObject square = gameData.selected;
                string pieceName = tempInfo.tempPiece.spawnable;

                Piece piece = Spawnables.create(pieceName);
                piece.color = tempInfo.tempPiece.color;
                piece.numSpawns--;
                initPiece(piece, HelperFunctions.findCoords(square));
                
                gameData.abilitySelected = "";
                gameData.selected = null;
                HelperFunctions.resetBoardColours();
                gameData.turn = gameData.turn * -1;
            }
        }
    }

    [PunRPC]
    public void MovePieceRPC(int[] toMoveCoords, int[] coords)
    {
        GameObject square = HelperFunctions.findSquare(toMoveCoords[0], toMoveCoords[1]);
        Piece piece = gameData.selectedToMovePiece;
        movePiece(piece, coords);
    }

    public void movePiece(Piece piece, int[] coords)
    {

        //Debug.Log("Flags");
        //if (gameData.selected) Debug.Log("Selected: " + gameData.selected.name);
        //if (gameData.selectedToMove) Debug.Log("SelectedToMove: " + gameData.selectedToMove.name);
        //Debug.Log("isSelected: " + gameData.isSelected);
        //Debug.Log("readyToMove: " + gameData.readyToMove);

        GameObject toAppend = HelperFunctions.findSquare(coords[0], coords[1]);

        //Check for castle (maybe make helper func)
        //int xDisp = piece.position[0] - coords[0];
        //if ((piece == wKing || piece == bKing) && (xDisp == 2 || xDisp == -2))
        //{
        //    //Debug.Log("Castle Taken Place");
        //    if (piece == wKing && xDisp == 2)
        //    {
        //        gameData.whiteRooks[0].hasMoved = true;
        //        HelperFunctions.movePiece(gameData.whiteRooks[0], HelperFunctions.findSquare(4, 1));
        //        gameData.whiteRooks[0].position = new int[] { 4, 1 };
        //    }
        //    else if (piece == wKing && xDisp == -2)
        //    {
        //        gameData.whiteRooks[1].hasMoved = true;
        //        HelperFunctions.movePiece(gameData.whiteRooks[1], HelperFunctions.findSquare(6, 1));
        //        gameData.whiteRooks[1].position = new int[] { 6, 1 };
        //    }
        //    else if (piece == bKing && xDisp == 2)
        //    {
        //        gameData.blackRooks[0].hasMoved = true;
        //        HelperFunctions.movePiece(gameData.blackRooks[0], HelperFunctions.findSquare(4, 8));
        //        gameData.blackRooks[0].position = new int[] { 4, 8 };
        //    }
        //    else if (piece == bKing && xDisp == -2)
        //    {
        //        gameData.blackRooks[1].hasMoved = true;
        //        HelperFunctions.movePiece(gameData.blackRooks[1], HelperFunctions.findSquare(6, 8));
        //        gameData.blackRooks[0].position = new int[] { 6, 8 };
        //    }
        //}

        HelperFunctions.movePieceBoardGrid(piece, piece.position, coords);
        
        piece.hasMoved = true;

        HelperFunctions.movePiece(piece, toAppend);

        if (piece.stayTurn())
        {
            gameData.turn = gameData.turn * -1;
            gameData.forceStayTurn = piece.color;
        }
        else
        {
            gameData.forceStayTurn = 0;
        }

        if (HelperFunctions.checkState(piece, "Combustable"))
        {
            System.Random rand = new System.Random();
            int random = rand.Next(1, 7);

            if (random == 3)
            {
                int[,] collateral = {
                    { 1, 0 }, { 1, 1 }, { 1, -1 },
                    { -1, 0 }, { -1, 1 }, { -1, -1 },
                    { 0, 1 }, { 0, -1 }, { 0, 0 }
                };

                for (int i = 0; i < collateral.GetLength(0); i++)
                {
                    int[] col_coords = new int[] { piece.position[0] + collateral[i, 0], piece.position[1] + collateral[i, 1] };
                    GameObject square = HelperFunctions.findSquare(col_coords[0], col_coords[1]);

                    if (collateral[i, 0] == 0 && collateral[i, 1] == 0)
                    {
                        HelperFunctions.collateralDeath(HelperFunctions.pieceToList(piece));
                    }

                    if (!square) continue;

                    List<Piece> sqPieces = HelperFunctions.getPiecesOnSquareBoardGrid(square);

                    if (sqPieces == null || sqPieces.Count == 0) continue;

                    HelperFunctions.collateralDeath(sqPieces);
                }
            }
        }

        if (HelperFunctions.checkState(piece, "Fragile"))
        {
            System.Random rand = new System.Random();
            int random = rand.Next(1, 7);

            if (random == 3)
            {
                HelperFunctions.collateralDeath(HelperFunctions.pieceToList(piece));
            }
        }

        //Updated Since Gamebot
        //Call Interactive Move Methods Here
        /*
         * if (interactive move) { do stuff } 
         */

        //Check for Pawn Promote
        //TODO Generalize to function
        if (piece.go.tag == "Pawn")
        {
            if (piece.color == 1 && piece.position[1] == 8)
            {
                PhotonNetwork.Destroy(piece.go);
                piece.alive = 0;
                Piece superPawn = new SuperPawn(1, true);
                initPiece(superPawn, coords);
                superPawn.go.tag = "SuperPawn";

                gameData.piecesDict.Add(superPawn.go, superPawn);
            }
            else if (piece.color == -1 && piece.position[1] == 1)
            {
                PhotonNetwork.Destroy(piece.go);
                piece.alive = 0;
                Piece superPawn = new SuperPawn(-1, true);
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

        HelperFunctions.updatePointsOnBoard(panel);

        if (isInCheckMate)
        {
            Invoke("toggleCheckmateUI", 1.5f);
        }

        gameData.selectedPiece = null;
    }

    public void toggleCheckmateUI()
    {
        checkmateUI.SetActive(true);
    }

    private void initPiece(Piece piece, int[] coords)
    {
        if (!gameData.piecesDict.ContainsKey(piece.go))
        {
            gameData.piecesDict.Add(piece.go, piece);
        }

        if (!gameData.allPiecesDict.ContainsKey(piece.go))
        {
            gameData.allPiecesDict.Add(piece.go, piece);
        }

        GameObject toAppend = HelperFunctions.findSquare(coords[0], coords[1]);
        piece.position = HelperFunctions.findCoords(toAppend);

        if (piece.startSquare == null)
        {
            piece.startSquare = piece.position;
        }

        HelperFunctions.movePiece(piece, toAppend);

        piece.alive = 1;

        //piece.go.tag = piece.name;
        HelperFunctions.updateBoardGrid(coords, piece, "a");
        gameData.panelCodes.Add(piece.name);
    }
}

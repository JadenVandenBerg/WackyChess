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
    public HelperFunctions helper;

    public Piece pawn, pawn2, pawn3, pawn4, pawn5, pawn6, pawn7, pawn8;
    public Piece bpawn, bpawn2, bpawn3, bpawn4, bpawn5, bpawn6, bpawn7, bpawn8;
    public Piece wRook, wRook2, bRook, bRook2;
    public Piece wBishop, wBishop2, bBishop, bBishop2;
    public Piece wKnight, wKnight2, bKnight, bKnight2;
    public Piece wQueen, bQueen;
    public Piece wKing, bKing;

    public PhotonView photonView;

    //public GameObject wp, bp, t, a;
   // public SidePanelAdjust panel;

    IEnumerator Start()
    {
        gameData.playMode = "2Player";
        gameData.turn = 1;
        gameData.board = board2;

        photonView = GetComponent<PhotonView>();

        gameData.boardGrid = HelperFunctions.initBoardGrid();

        pawn = new DelayedPawn(1, true);
        pawn2 = new Crook(1, true);
        pawn3 = new ProtectivePawn(1, true);
        pawn4 = new RoyalKnight(1, true);
        pawn5 = new PAWN(1, true);
        pawn6 = new JockeyKnight(1, true);
        pawn7 = new PromotingPawn(1, true);
        pawn8 = new LandminePawn(1, true);

        wRook = new Rook(1, true);
        wRook2 = new Empress(1, true);
        wBishop = new FragileBishop(1, true);
        wBishop2 = new ColorChangingBishop(1, true);
        wKnight = new FragileKnight(1, true);
        wKnight2 = new Knight(1, true);
        wQueen = new Medusa(1, true);
        wKing = new RulebreakerKing(1, true);

        bpawn = new AtomicPawn(-1, true);
        bpawn2 = new PiggybackKnight(-1, true);
        bpawn3 = new LandminePawn(-1, true);
        bpawn4 = new Crook(-1, true);
        bpawn5 = new LandminePawn(-1, true);
        bpawn6 = new LandminePawn(-1, true);
        bpawn7 = new LandminePawn(-1, true);
        bpawn8 = new LandminePawn(-1, true);

        bRook = new Rook(-1, true);
        bRook2 = new Rook(-1, true);
        bBishop = new Bishop(-1, true);
        bBishop2 = new Bishop(-1, true);
        bKnight = new Knight(-1, true);
        bKnight2 = new Knight(-1, true);
        bQueen = new Queen(-1, true);
        bKing = new HyperFastKing(-1, true);

        HelperFunctions.initPiece(pawn, new int[] { 1, 2 });
        HelperFunctions.initPiece(pawn2, new int[] { 2, 2 });
        HelperFunctions.initPiece(pawn3, new int[] { 3, 2 });
        HelperFunctions.initPiece(pawn4, new int[] { 4, 2 });
        HelperFunctions.initPiece(pawn5, new int[] { 5, 2 });
        HelperFunctions.initPiece(pawn6, new int[] { 6, 2 });
        HelperFunctions.initPiece(pawn7, new int[] { 7, 2 });
        HelperFunctions.initPiece(pawn8, new int[] { 8, 2 });

        HelperFunctions.initPiece(wRook, new int[] { 1, 1 });
        HelperFunctions.initPiece(wRook2, new int[] { 8, 1 });
        HelperFunctions.initPiece(wBishop, new int[] { 3, 1 });
        HelperFunctions.initPiece(wBishop2, new int[] { 6, 1 });
        HelperFunctions.initPiece(wKnight, new int[] { 2, 1 });
        HelperFunctions.initPiece(wKnight2, new int[] { 7, 1 });
        HelperFunctions.initPiece(wQueen, new int[] { 4, 1 });
        HelperFunctions.initPiece(wKing, new int[] { 5, 1 });

        HelperFunctions.initPiece(bpawn, new int[] { 1, 7 });
        HelperFunctions.initPiece(bpawn2, new int[] { 2, 7 });
        HelperFunctions.initPiece(bpawn3, new int[] { 3, 7 });
        HelperFunctions.initPiece(bpawn4, new int[] { 4, 7 });
        HelperFunctions.initPiece(bpawn5, new int[] { 5, 7 });
        HelperFunctions.initPiece(bpawn6, new int[] { 6, 7 });
        HelperFunctions.initPiece(bpawn7, new int[] { 7, 7 });
        HelperFunctions.initPiece(bpawn8, new int[] { 8, 7 });

        HelperFunctions.initPiece(bRook, new int[] { 1, 8 });
        HelperFunctions.initPiece(bRook2, new int[] { 8, 8 });
        HelperFunctions.initPiece(bBishop, new int[] { 3, 8 });
        HelperFunctions.initPiece(bBishop2, new int[] { 6, 8 });
        HelperFunctions.initPiece(bKnight, new int[] { 2, 8 });
        HelperFunctions.initPiece(bKnight2, new int[] { 7, 8 });
        HelperFunctions.initPiece(bQueen, new int[] { 4, 8 });
        HelperFunctions.initPiece(bKing, new int[] { 5, 8 });

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

        //panel.Initialize();

        yield return null;
        //HelperFunctions.updatePointsOnBoard(panel);
    }

    void Update()
    {
        if (!gameData.isSelected && gameData.abilitySelected == "")
        {
            HelperFunctions.resetBoardColours();
        }

        if (!gameData.readyToMove && gameData.selected && gameData.selected.transform.childCount != 0 && gameData.selectedPiece != null)
        {
            Piece currentPiece = gameData.selectedPiece;
            int currentColor = currentPiece.color;
            if ((gameData.selected.transform.GetChild(0).gameObject != null) || gameData.selectedFromPanel)
            {
                if (!HelperFunctions.isMultipleOnSquare(gameData.selected) && gameData.abilitySelected == "" && currentColor == gameData.turn)
                {
                    gameData.readyToMove = true;

                    //HelperFunctions.updateCastleCondition();
                    HelperFunctions.addToCurrentMoveableCoordsTotal(currentColor, true, true, currentPiece, true, true);
                }
                else if (HelperFunctions.isColorOnSquare(gameData.selected, gameData.turn, false))
                {
                    //TODO: Force selection from side panel
                    Debug.Log("SIDE PANEL SELECTION");
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
                    helper.refreshPanelSelected();
                }
            }
        }

        /*Debug.Log("ReadyToMove: " + gameData.readyToMove);
        Debug.Log("Selected: " + gameData.selected);
        if (gameData.selectedPiece != null) Debug.Log("SelectedPiece: " + gameData.selectedPiece.name);
        Debug.Log("SelectedToMove: " + gameData.selectedToMove);
        Debug.Log("SelectedToMovePiece: " + gameData.selectedToMovePiece + ". State: " + gameData.selectedToMovePiece.state + ". Ability: " + gameData.selectedToMovePiece.ability);
        Debug.Log("Ability: " + gameData.abilitySelected);*/

        //MOVE
        if (gameData.readyToMove && gameData.isSelected && gameData.selected && gameData.selectedToMovePiece != null && gameData.selectedToMove && HelperFunctions.isPieceOnSquare(gameData.selectedToMove))
        {
            //Debug.Log("READY TO MOVE");
            //Debug.Log("Found Move? " + HelperFunctions.findCoords(gameData.selected)[0] + "," + HelperFunctions.findCoords(gameData.selected)[1] + " : " + isInList(gameData.currentMoveableCoords, HelperFunctions.findCoords(gameData.selected)));

            if (gameData.selectedToMovePiece.color == gameData.turn)
            {
                if (HelperFunctions.isInList(gameData.currentMoveableCoords, HelperFunctions.findCoords(gameData.selected), false))
                {
                    helper.performPreMove();
                    //TODO MOVE DEATH LOGIC TO MOVEPIECE AND MOVE MOVEPIECE TO HELPERFUNCTIONS

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
            helper.abilityHandler();
        }
    }
}

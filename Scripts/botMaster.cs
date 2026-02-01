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
using System.Linq;

public class botMaster : MonoBehaviour
{
    public GameObject board2;
    public GameObject boardWrapper;
    public GameObject checkmateUI;
    public HelperFunctions helper;

    // public Piece pawn, pawn2, pawn3, pawn4, pawn5, pawn6, pawn7, pawn8;
    // public Piece bpawn, bpawn2, bpawn3, bpawn4, bpawn5, bpawn6, bpawn7, bpawn8;
    // public Piece wRook, wRook2, bRook, bRook2;
    // public Piece wBishop, wBishop2, bBishop, bBishop2;
    // public Piece wKnight, wKnight2, bKnight, bKnight2;
    // public Piece wQueen, bQueen;
    // public Piece wKing, bKing;

    //public GameObject wp, bp, t, a;

    BotTemplate botWhite;
    BotTemplate botBlack;

    IEnumerator Start()
    {
        gameData.playMode = "BotvBot";
        gameData.turn = 1;
        gameData.board = board2;

        botWhite = new RandomBot(1);
        botBlack = new RandomBot(-1);
        gameData.botWhite = botWhite;
        gameData.botBlack = botBlack;

        gameData.boardGrid = HelperFunctions.initBoardGrid();

        List<Piece> botWhitePawns = BotHelperFunctions.filterPieces("Pawn", botWhite.pieces);
        HelperFunctions.initPiece(botWhitePawns[0], new int[] { 1, 2 });
        HelperFunctions.initPiece(botWhitePawns[1], new int[] { 2, 2 });
        HelperFunctions.initPiece(botWhitePawns[2], new int[] { 3, 2 });
        HelperFunctions.initPiece(botWhitePawns[3], new int[] { 4, 2 });
        HelperFunctions.initPiece(botWhitePawns[4], new int[] { 5, 2 });
        HelperFunctions.initPiece(botWhitePawns[5], new int[] { 6, 2 });
        HelperFunctions.initPiece(botWhitePawns[6], new int[] { 7, 2 });
        HelperFunctions.initPiece(botWhitePawns[7], new int[] { 8, 2 });

        List<Piece> botWhiteRooks = BotHelperFunctions.filterPieces("Rook", botWhite.pieces);
        List<Piece> botWhiteBishops = BotHelperFunctions.filterPieces("Bishop", botWhite.pieces);
        List<Piece> botWhiteKnights = BotHelperFunctions.filterPieces("Knight", botWhite.pieces);
        List<Piece> botWhiteKing = BotHelperFunctions.filterPieces("King", botWhite.pieces);
        List<Piece> botWhiteQueen = BotHelperFunctions.filterPieces("Queen", botWhite.pieces);
        HelperFunctions.initPiece(botWhiteRooks[0], new int[] { 1, 1 });
        HelperFunctions.initPiece(botWhiteRooks[1], new int[] { 8, 1 });
        HelperFunctions.initPiece(botWhiteBishops[0], new int[] { 3, 1 });
        HelperFunctions.initPiece(botWhiteBishops[1], new int[] { 6, 1 });
        HelperFunctions.initPiece(botWhiteKnights[0], new int[] { 2, 1 });
        HelperFunctions.initPiece(botWhiteKnights[1], new int[] { 7, 1 });
        HelperFunctions.initPiece(botWhiteQueen[0], new int[] { 4, 1 });
        HelperFunctions.initPiece(botWhiteKing[0], new int[] { 5, 1 });

        List<Piece> botBlackPawns = BotHelperFunctions.filterPieces("Pawn", botBlack.pieces);
        HelperFunctions.initPiece(botBlackPawns[0], new int[] { 1, 7 });
        HelperFunctions.initPiece(botBlackPawns[1], new int[] { 2, 7 });
        HelperFunctions.initPiece(botBlackPawns[2], new int[] { 3, 7 });
        HelperFunctions.initPiece(botBlackPawns[3], new int[] { 4, 7 });
        HelperFunctions.initPiece(botBlackPawns[4], new int[] { 5, 7 });
        HelperFunctions.initPiece(botBlackPawns[5], new int[] { 6, 7 });
        HelperFunctions.initPiece(botBlackPawns[6], new int[] { 7, 7 });
        HelperFunctions.initPiece(botBlackPawns[7], new int[] { 8, 7 });

        List<Piece> botBlackRooks = BotHelperFunctions.filterPieces("Rook", botBlack.pieces);
        List<Piece> botBlackBishops = BotHelperFunctions.filterPieces("Bishop", botBlack.pieces);
        List<Piece> botBlackKnights = BotHelperFunctions.filterPieces("Knight", botBlack.pieces);
        List<Piece> botBlackKing = BotHelperFunctions.filterPieces("King", botBlack.pieces);
        List<Piece> botBlackQueen = BotHelperFunctions.filterPieces("Queen", botBlack.pieces);
        HelperFunctions.initPiece(botBlackRooks[0], new int[] { 1, 8 });
        HelperFunctions.initPiece(botBlackRooks[1], new int[] { 8, 8 });
        HelperFunctions.initPiece(botBlackBishops[0], new int[] { 3, 8 });
        HelperFunctions.initPiece(botBlackBishops[1], new int[] { 6, 8 });
        HelperFunctions.initPiece(botBlackKnights[0], new int[] { 2, 8 });
        HelperFunctions.initPiece(botBlackKnights[1], new int[] { 7, 8 });
        HelperFunctions.initPiece(botBlackQueen[0], new int[] { 4, 8 });
        HelperFunctions.initPiece(botBlackKing[0], new int[] { 5, 8 });

        //This system will need to change once players can get more pieces, keep a tally of pieces using game vars
        botWhitePawns[0].name = "w_p1";
        botWhitePawns[1].name = "w_p2";
        botWhitePawns[2].name = "w_p3";
        botWhitePawns[3].name = "w_p4";
        botWhitePawns[4].name = "w_p5";
        botWhitePawns[5].name = "w_p6";
        botWhitePawns[6].name = "w_p7";
        botWhitePawns[7].name = "w_p8";
        botWhiteRooks[0].name = "w_r1";
        botWhiteRooks[1].name = "w_r2";
        botWhiteBishops[0].name = "w_b1";
        botWhiteBishops[1].name = "w_b2";
        botWhiteKnights[0].name = "w_n1";
        botWhiteKnights[1].name = "w_n2";
        botWhiteKing[0].name = "w_k1";
        botWhiteQueen[0].name = "w_q1";

        botBlackPawns[0].name = "b_p1";
        botBlackPawns[1].name = "b_p2";
        botBlackPawns[2].name = "b_p3";
        botBlackPawns[3].name = "b_p4";
        botBlackPawns[4].name = "b_p5";
        botBlackPawns[5].name = "b_p6";
        botBlackPawns[6].name = "b_p7";
        botBlackPawns[7].name = "b_p8";
        botBlackRooks[0].name = "b_r1";
        botBlackRooks[1].name = "b_r2";
        botBlackBishops[0].name = "b_b1";
        botBlackBishops[1].name = "b_b2";
        botBlackKnights[0].name = "b_n1";
        botBlackKnights[1].name = "b_n2";
        botBlackKing[0].name = "b_k1";
        botBlackQueen[0].name = "b_q1";

        gameData.whiteRooks.Add(botWhiteRooks[0]);
        gameData.whiteRooks.Add(botWhiteRooks[1]);

        gameData.blackRooks.Add(botBlackRooks[0]);
        gameData.blackRooks.Add(botBlackRooks[1]);

        gameData.whiteKing = botWhiteKing[0];
        gameData.blackKing = botBlackKing[0];

        yield return null;
    }

    int turn = 1;
    bool isTurn = true;

    void Update()
    {
        if (!isTurn) return;

        isTurn = false;

        StartCoroutine(BotTurn());
    }

    IEnumerator BotTurn()
    {
        BotTemplate currentBot;
        bool valid = true;

        Piece movePieceObj = null;
        int[] moveCoords = null;
        long watchMS = 0;

        if (turn == 1)
        {
            currentBot = botWhite;
        }
        else
        {
            currentBot = botBlack;
        }

        if (currentBot.penalty)
        {
            Debug.Log("Bot " + currentBot.name + " has a penalty. Executing random move");
            valid = false;
            currentBot.penalty = false;
        }
        else
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Dictionary<Piece, int[]> nextMove = currentBot.nextMove();
            watch.Stop();

            watchMS = watch.ElapsedMilliseconds;

            if (watchMS > 5000)
            {
                currentBot.penalty = true;
                valid = false;
            }
            else
            {
                KeyValuePair<Piece, int[]> movePair = nextMove.First();
                movePieceObj = movePair.Key;
                moveCoords = movePair.Value;

                valid = botValidateMove(movePieceObj, moveCoords);
            }
        }

        Debug.Log("Bot " + currentBot.name + " moved " + movePieceObj.name + " to " + HelperFunctions.findSquare(moveCoords[0], moveCoords[1]).name + " in " + watchMS + "ms.");
        gameData.selected = HelperFunctions.findSquare(moveCoords[0], moveCoords[1]);
        gameData.selectedToMove = HelperFunctions.findSquare(movePieceObj.position[0], movePieceObj.position[1]);
        gameData.selectedPiece = HelperFunctions.getPieceOnSquare(gameData.selected);
        gameData.selectedToMovePiece = movePieceObj;

        if (valid)
        {
            helper.performPreMove();
            helper.movePiece_(movePieceObj, moveCoords);
        }
        else
        {
            Debug.Log("MOVE IS INVALID - PERFORMING RANDOM MOVE");
            var randomMove = BotHelperFunctions.getRandomBotMove(currentBot);
            helper.performPreMove();
            helper.movePiece_(randomMove.piece, randomMove.coords);
        }

        turn *= -1;
        currentBot.currentBoardState.refresh();

        yield return new WaitForSeconds(3.0f);
        isTurn = true;


        //Check if check/update bot boardstate
    }

    public bool botValidateMove(Piece piece, int[] coords) {
        List<int[]> moves = HelperFunctions.addMovesToCurrentMoveableCoords(piece);

        if (HelperFunctions.isInList(moves, coords, false)) {
            return true;
        }

        return false;
    }
}

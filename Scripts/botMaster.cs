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

    // public Piece pawn, pawn2, pawn3, pawn4, pawn5, pawn6, pawn7, pawn8;
    // public Piece bpawn, bpawn2, bpawn3, bpawn4, bpawn5, bpawn6, bpawn7, bpawn8;
    // public Piece wRook, wRook2, bRook, bRook2;
    // public Piece wBishop, wBishop2, bBishop, bBishop2;
    // public Piece wKnight, wKnight2, bKnight, bKnight2;
    // public Piece wQueen, bQueen;
    // public Piece wKing, bKing;

    public PhotonView photonView;

    //public GameObject wp, bp, t, a;
    public SidePanelAdjust panel;

    private AudioSource moveSound;

    BotTemplate botWhite;
    BotTemplate botBlack;

    IEnumerator Start()
    {
        gameData.playMode = "BotvBot";
        gameData.turn = 1;
        gameData.board = board2;

        moveSound = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();

        botWhite = new RandomBot(1);
        botBlack = new RandomBot(-1);

        gameData.boardGrid = HelperFunctions.initBoardGrid();

        List<Piece> botWhitePawns = BotHelperFunctions.filterPieces("Pawn", botWhite.pieces);
        initPiece(botWhitePawns[0], new int[] { 1, 2 });
        initPiece(botWhitePawns[1], new int[] { 2, 2 });
        initPiece(botWhitePawns[2], new int[] { 3, 2 });
        initPiece(botWhitePawns[3], new int[] { 4, 2 });
        initPiece(botWhitePawns[4], new int[] { 5, 2 });
        initPiece(botWhitePawns[5], new int[] { 6, 2 });
        initPiece(botWhitePawns[6], new int[] { 7, 2 });
        initPiece(botWhitePawns[7], new int[] { 8, 2 });

        List<Piece> botWhiteRooks = BotHelperFunctions.filterPieces("Rook", botWhite.pieces);
        List<Piece> botWhiteBishops = BotHelperFunctions.filterPieces("Bishop", botWhite.pieces);
        List<Piece> botWhiteKnights = BotHelperFunctions.filterPieces("Knight", botWhite.pieces);
        List<Piece> botWhiteQueen = BotHelperFunctions.filterPieces("King", botWhite.pieces);
        List<Piece> botWhiteKing = BotHelperFunctions.filterPieces("Queen", botWhite.pieces);
        initPiece(botWhiteRooks[0], new int[] { 1, 1 });
        initPiece(botWhiteRooks[1], new int[] { 8, 1 });
        initPiece(botWhiteBishops[0], new int[] { 3, 1 });
        initPiece(botWhiteBishops[1], new int[] { 6, 1 });
        initPiece(botWhiteKnights[0], new int[] { 2, 1 });
        initPiece(botWhiteKnights[1], new int[] { 7, 1 });
        initPiece(botWhiteQueen[0], new int[] { 4, 1 });
        initPiece(botWhiteKing[0], new int[] { 5, 1 });

        List<Piece> botBlackPawns = BotHelperFunctions.filterPieces("Pawn", botBlack.pieces);
        initPiece(botBlackPawns[0], new int[] { 1, 2 });
        initPiece(botBlackPawns[1], new int[] { 2, 2 });
        initPiece(botBlackPawns[2], new int[] { 3, 2 });
        initPiece(botBlackPawns[3], new int[] { 4, 2 });
        initPiece(botBlackPawns[4], new int[] { 5, 2 });
        initPiece(botBlackPawns[5], new int[] { 6, 2 });
        initPiece(botBlackPawns[6], new int[] { 7, 2 });
        initPiece(botBlackPawns[7], new int[] { 8, 2 });

        List<Piece> botBlackRooks = BotHelperFunctions.filterPieces("Rook", botBlack.pieces);
        List<Piece> botBlackBishops = BotHelperFunctions.filterPieces("Bishop", botBlack.pieces);
        List<Piece> botBlackKnights = BotHelperFunctions.filterPieces("Knight", botBlack.pieces);
        List<Piece> botBlackQueen = BotHelperFunctions.filterPieces("King", botBlack.pieces);
        List<Piece> botBlackKing = BotHelperFunctions.filterPieces("Queen", botBlack.pieces);
        initPiece(botBlackRooks[0], new int[] { 1, 1 });
        initPiece(botBlackRooks[1], new int[] { 8, 1 });
        initPiece(botBlackBishops[0], new int[] { 3, 1 });
        initPiece(botBlackBishops[1], new int[] { 6, 1 });
        initPiece(botBlackKnights[0], new int[] { 2, 1 });
        initPiece(botBlackKnights[1], new int[] { 7, 1 });
        initPiece(botBlackQueen[0], new int[] { 4, 1 });
        initPiece(botBlackKing[0], new int[] { 5, 1 });

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
        panel.Initialize();

        yield return null;
        HelperFunctions.updatePointsOnBoard(panel);
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

            if (watch.ElapsedMilliseconds > 5000)
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

        if (valid)
        {
            movePiece(movePieceObj, moveCoords);
        }
        else
        {
            var randomMove = BotHelperFunctions.getRandomBotMove(currentBot);
            movePiece(randomMove.piece, randomMove.coords);
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

    public void movePiece(Piece piece, int[] coords)
    {

        //Debug.Log("Flags");
        //if (gameData.selected) Debug.Log("Selected: " + gameData.selected.name);
        //if (gameData.selectedToMove) Debug.Log("SelectedToMove: " + gameData.selectedToMove.name);
        //Debug.Log("isSelected: " + gameData.isSelected);
        //Debug.Log("readyToMove: " + gameData.readyToMove);

        GameObject toAppend = HelperFunctions.findSquare(coords[0], coords[1]);
        GameObject pieceOriginalSquare = HelperFunctions.findSquare(piece.position[0], piece.position[1]);

        //before piece is moved
        //Loop through pieces for state check
        List<Piece> piecesOnSquare = HelperFunctions.getPiecesOnSquareBoardGrid(HelperFunctions.findSquare(piece.position[0], piece.position[1]));
        foreach (Piece pieceOnSquare in piecesOnSquare)
        {
            if (HelperFunctions.checkState(pieceOnSquare, "Crook")) {
                if (piecesOnSquare.Count == 2)
                {
                    HelperFunctions.removeState(pieceOnSquare, "Jailed");
                }
            }

            if (HelperFunctions.checkState(piece, "Jailer"))
            {
                HelperFunctions.removeState(pieceOnSquare, "Jailed");
            }
        }

        HelperFunctions.movePieceBoardGrid(piece, piece.position, coords);
        Debug.Log("Piece " + piece.name + " moved to " + coords[0] + "," + coords[1]);
        
        piece.hasMoved = true;
        HelperFunctions.movePiece(piece, toAppend);

        if (HelperFunctions.checkState(piece, "Piggyback"))
        {
            piecesOnSquare = new List<Piece> (HelperFunctions.getPiecesOnSquareBoardGrid(pieceOriginalSquare));
            foreach (Piece pieceOnSquare in piecesOnSquare)
            {
                Debug.Log(pieceOnSquare.name + " is moved from Piggyback");

                HelperFunctions.movePieceBoardGrid(pieceOnSquare, pieceOnSquare.position, coords);
                pieceOnSquare.hasMoved = true;
                HelperFunctions.movePiece(pieceOnSquare, toAppend);
            }
        }

        List<Piece> piecesOnSquare2 = new List<Piece>(HelperFunctions.getPiecesOnSquareBoardGrid(pieceOriginalSquare));
        foreach (Piece pieceOnSquare in piecesOnSquare2)
        {
            if (HelperFunctions.checkState(pieceOnSquare, "Jockey"))
            {
                HelperFunctions.movePieceBoardGrid(pieceOnSquare, pieceOnSquare.position, coords);
                pieceOnSquare.hasMoved = true;
                HelperFunctions.movePiece(pieceOnSquare, toAppend);
            }
        }

        if (piece.stayTurn())
        {
            gameData.turn = gameData.turn * -1;
            gameData.forceStayTurn = piece.color;
        }
        else
        {
            gameData.forceStayTurn = 0;
        }

        // After move collateral
        if (HelperFunctions.checkState(piece, "Combustable"))
        {
            System.Random rand = new System.Random();
            int random = rand.Next(1, 7);

            if (random == 3)
            {
                List<int[]> collateral = null;

                if (HelperFunctions.isPieceSurroundingState(piece, "Defuser"))
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
                        piece.position[0] + collateral[i][0],
                        piece.position[1] + collateral[i][1]
                    };

                    GameObject square = HelperFunctions.findSquare(col_coords[0], col_coords[1]);

                    if (collateral[i][0] == 0 && collateral[i][1] == 0)
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
        //TODO make sure this works
        if (piece.promotesInto != "")
        {
            if (piece.position[1] == piece.promotingRow)
            {
                string pname = piece.promotesInto;
                Piece p = HelperFunctions.Spawnables.create(pname, piece.color);
                HelperFunctions.forceRemove(piece);
                initPiece(p, coords);
            }
        }

        gameData.turn = gameData.turn * -1;

        //Add pieces to list
        Piece king;
        if (piece.color == 1)
        {
            king = gameData.whiteKing;
        }
        else
        {
            king = gameData.blackKing;
        }

        //Last minute things
        //Heartbroken King Check
        if (HelperFunctions.checkState(gameData.whiteKing, "Heartbroken"))
        {
            if (!HelperFunctions.isPieceTypeOnBoard("q", 1))
            {
                Piece tempKing = HelperFunctions.Spawnables.create("DepressedKing", 1);
                initPiece(tempKing, gameData.whiteKing.position);
                HelperFunctions.collateralDeath(HelperFunctions.pieceToList(gameData.whiteKing));
                gameData.whiteKing = tempKing;
            }
        }
        else if (HelperFunctions.checkState(gameData.blackKing, "Heartbroken")) {
            if (!HelperFunctions.isPieceTypeOnBoard("q", -1))
            {
                Piece tempKing = HelperFunctions.Spawnables.create("DepressedKing", -1);
                initPiece(tempKing, gameData.blackKing.position);
                HelperFunctions.collateralDeath(HelperFunctions.pieceToList(gameData.blackKing));
                gameData.blackKing = tempKing;
            }
        }

        bool isInCheck = HelperFunctions.isCheck(king);
        bool isInCheckMate = HelperFunctions.isCheckMate(king, true);

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

        if (HelperFunctions.checkState(piece, "PAWN"))
        {
            piece.position[1] = piece.position[1] + 1;
            toAppend = HelperFunctions.findSquare(piece.position[0], piece.position[1]);
        }

        if (HelperFunctions.checkState(piece, "Double"))
        {
            Piece doublePawn = HelperFunctions.Spawnables.create("Pawn", piece.color);
            initPiece(doublePawn, piece.position);
        }

        piece.startSquare = new int[] { piece.position[0], piece.position[1] };

        HelperFunctions.movePiece(piece, toAppend);

        piece.alive = 1;

        //piece.go.tag = piece.name;
        HelperFunctions.updateBoardGrid(piece.position, piece, "a");
        gameData.panelCodes.Add(piece.name);
    }
}

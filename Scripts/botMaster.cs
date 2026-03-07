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
using System.IO;
using System.Text;
using static BotHelperFunctions;

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
    bool started = false;

    BotGameStatus bgs = new BotGameStatus();

    IEnumerator Start()
    {
        gameData.playMode = "BotvBot";
        gameData.turn = 1;
        gameData.board = board2;

        botWhite = new OneMoveBot(1);
        botBlack = new OneMoveBot(-1);
        gameData.botWhite = botWhite;
        gameData.botBlack = botBlack;

        bgs.white = botWhite.name;
        bgs.black = botBlack.name;

        gameData.boardGrid = HelperFunctions.initBoardGrid();

        List<Piece> botWhitePawns = BotHelperFunctions.filterPieces("Pawn", botWhite.pieces);
        /*
        List<Piece> botWhitePawns = new List<Piece>
        {
            getPieceTypeInstance("JockeyPawn", 1),
            getPieceTypeInstance("TwoPawnDLDR", 1),
            getPieceTypeInstance("HungryPawn", 1),
            getPieceTypeInstance("TwoPawnLR", 1),
            getPieceTypeInstance("FragilePawn", 1),
            getPieceTypeInstance("PAWN", 1),
            getPieceTypeInstance("JailPawn", 1),
            getPieceTypeInstance("LeftPawn", 1),
        };
        */

        HelperFunctions.initPiece(botWhitePawns[0], new int[] { 1, 2 });
        HelperFunctions.initPiece(botWhitePawns[1], new int[] { 2, 2 });
        HelperFunctions.initPiece(botWhitePawns[2], new int[] { 3, 2 });
        HelperFunctions.initPiece(botWhitePawns[3], new int[] { 4, 2 });
        HelperFunctions.initPiece(botWhitePawns[4], new int[] { 5, 2 });
        HelperFunctions.initPiece(botWhitePawns[5], new int[] { 6, 2 });
        HelperFunctions.initPiece(botWhitePawns[6], new int[] { 7, 2 });
        HelperFunctions.initPiece(botWhitePawns[7], new int[] { 8, 2 });

        List<Piece> botWhiteRooks = BotHelperFunctions.filterPieces("Rook", botWhite.pieces);
        /*
        List<Piece> botWhiteRooks = new List<Piece>
        {
            getPieceTypeInstance("PortalRook", 1),
            getPieceTypeInstance("DefuserRook", 1),
        };
        */
        List<Piece> botWhiteBishops = BotHelperFunctions.filterPieces("Bishop", botWhite.pieces);
        /*
        List<Piece> botWhiteBishops = new List<Piece>
        {
            getPieceTypeInstance("HungryBishop", 1),
            getPieceTypeInstance("SuperGhostBishop", 1),
        };
        */
        List<Piece> botWhiteKnights = BotHelperFunctions.filterPieces("Knight", botWhite.pieces);
        /*
        List<Piece> botWhiteKnights = new List<Piece>
        {
            getPieceTypeInstance("HungryKnight", 1),
            getPieceTypeInstance("LongKnight", 1),
        };
        */
        List<Piece> botWhiteKing = BotHelperFunctions.filterPieces("King", botWhite.pieces);
        /*
        List<Piece> botWhiteKing = new List<Piece>
        {
            getPieceTypeInstance("StackingKing", 1)
        };
        */
        List<Piece> botWhiteQueen = BotHelperFunctions.filterPieces("Queen", botWhite.pieces);
        /*
        List<Piece> botWhiteQueen = new List<Piece>
        {
            getPieceTypeInstance("AtomicQueen", 1)
        };
        */

        HelperFunctions.initPiece(botWhiteRooks[0], new int[] { 1, 1 });
        HelperFunctions.initPiece(botWhiteRooks[1], new int[] { 8, 1 });
        HelperFunctions.initPiece(botWhiteBishops[0], new int[] { 3, 1 });
        HelperFunctions.initPiece(botWhiteBishops[1], new int[] { 6, 1 });
        HelperFunctions.initPiece(botWhiteKnights[0], new int[] { 2, 1 });
        HelperFunctions.initPiece(botWhiteKnights[1], new int[] { 7, 1 });
        HelperFunctions.initPiece(botWhiteQueen[0], new int[] { 4, 1 });
        HelperFunctions.initPiece(botWhiteKing[0], new int[] { 5, 1 });

        List<Piece> botBlackPawns = BotHelperFunctions.filterPieces("Pawn", botBlack.pieces);
        /*
        List<Piece> botBlackPawns = new List<Piece>
        {
            getPieceTypeInstance("OneTwoPawnULUUR", -1),
            getPieceTypeInstance("CloningPawn", -1),
            getPieceTypeInstance("OneTwoPawnLite", -1),
            getPieceTypeInstance("OneTwoPawnD", -1),
            getPieceTypeInstance("SuperGhostPawn", -1),
            getPieceTypeInstance("TwoPawnLR", -1),
            getPieceTypeInstance("DelayedPawn", -1),
            getPieceTypeInstance("OnePawnULUURLR", -1),
        };
        */

        HelperFunctions.initPiece(botBlackPawns[0], new int[] { 1, 7 });
        HelperFunctions.initPiece(botBlackPawns[1], new int[] { 2, 7 });
        HelperFunctions.initPiece(botBlackPawns[2], new int[] { 3, 7 });
        HelperFunctions.initPiece(botBlackPawns[3], new int[] { 4, 7 });
        HelperFunctions.initPiece(botBlackPawns[4], new int[] { 5, 7 });
        HelperFunctions.initPiece(botBlackPawns[5], new int[] { 6, 7 });
        HelperFunctions.initPiece(botBlackPawns[6], new int[] { 7, 7 });
        HelperFunctions.initPiece(botBlackPawns[7], new int[] { 8, 7 });

        List<Piece> botBlackRooks = BotHelperFunctions.filterPieces("Rook", botBlack.pieces);
        /*
        List<Piece> botBlackRooks = new List<Piece>
        {
            getPieceTypeInstance("PromotionRook", -1),
            getPieceTypeInstance("PortalRook", -1),
        };
        */
        List<Piece> botBlackBishops = BotHelperFunctions.filterPieces("Bishop", botBlack.pieces);
        /*
        List<Piece> botBlackBishops = new List<Piece>
        {
            getPieceTypeInstance("JailBishop", -1),
            getPieceTypeInstance("BouncingBishop", -1),
        };
        */
        List<Piece> botBlackKnights = BotHelperFunctions.filterPieces("Knight", botBlack.pieces);
        /*
        List<Piece> botBlackKnights = new List<Piece>
        {
            getPieceTypeInstance("UndeadKnight", -1),
            getPieceTypeInstance("JailKnight", -1),
        };
        */
        List<Piece> botBlackKing = BotHelperFunctions.filterPieces("King", botBlack.pieces);
        /*
        List<Piece> botBlackKing = new List<Piece>
        {
            getPieceTypeInstance("Overlord", -1),
        };
        */
        List<Piece> botBlackQueen = BotHelperFunctions.filterPieces("Queen", botBlack.pieces);
        /*
        List<Piece> botBlackQueen = new List<Piece>
        {
            getPieceTypeInstance("PhantomQueen", -1),
        };
        */
        HelperFunctions.initPiece(botBlackRooks[0], new int[] { 1, 8 });
        HelperFunctions.initPiece(botBlackRooks[1], new int[] { 8, 8 });
        HelperFunctions.initPiece(botBlackBishops[0], new int[] { 3, 8 });
        HelperFunctions.initPiece(botBlackBishops[1], new int[] { 6, 8 });
        HelperFunctions.initPiece(botBlackKnights[0], new int[] { 2, 8 });
        HelperFunctions.initPiece(botBlackKnights[1], new int[] { 7, 8 });
        HelperFunctions.initPiece(botBlackQueen[0], new int[] { 4, 8 });
        HelperFunctions.initPiece(botBlackKing[0], new int[] { 5, 8 });

        foreach (Piece wp in botWhite.pieces)
        {
            bgs.whitePieces.Add(wp.name);
        }

        foreach (Piece wp in botBlack.pieces)
        {
            bgs.blackPieces.Add(wp.name);
        }

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

        foreach (Piece piece in gameData.allPiecesDict.Values)
        {
            piece.go.name = piece.name;
        }

        gameData.whiteRooks.Add(botWhiteRooks[0]);
        gameData.whiteRooks.Add(botWhiteRooks[1]);

        gameData.blackRooks.Add(botBlackRooks[0]);
        gameData.blackRooks.Add(botBlackRooks[1]);

        gameData.whiteKing = botWhiteKing[0];
        gameData.blackKing = botBlackKing[0];

        botWhite.king = gameData.whiteKing;
        botBlack.king = gameData.blackKing;

        botWhite.currentBoardState.refresh(gameData.boardGrid);
        List<float> points = BotHelperFunctions.getPointsOnBoardState(botWhite.currentBoardState, false);
        bgs.whitePoints = points[0];
        bgs.blackPoints = points[1];

        helper.updatePointsOnBoard();

        yield return null;
        started = true;
    }

    int turn = 1;
    bool isTurn = true;
    int movesWithoutCapture = 0;
    int subsequentChecks = 0;

    bool gameOver = false;
    bool kingDead = false;

    void Update()
    {
        if (!isTurn || !started || gameOver) return;

        isTurn = false;

        StartCoroutine(BotTurn());
    }

    IEnumerator BotTurn()
    {
        yield return new WaitForSeconds(0.5f);

        resetBotPieces(botWhite);
        resetBotPieces(botBlack);
        botWhite.currentBoardState.refresh(gameData.boardGrid);
        botBlack.currentBoardState.refresh(gameData.boardGrid);

        BotTemplate currentBot;
        bool valid;

        Piece movePieceObj = null;
        int[] moveCoords = null;
        long watchMS = 0;

        if (turn == 1)
        {
            bgs.numTurns++;
            currentBot = botWhite;
        }
        else
        {
            currentBot = botBlack;
        }

        NextMove selectedMove = null;

        if (currentBot.penalty)
        {
            Debug.Log("Bot " + currentBot.name + " has a penalty. Executing random move");
            valid = false;
            currentBot.penalty = false;
        }
        else
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            NextMove nextMove = currentBot.nextMove();
            watch.Stop();
            watchMS = watch.ElapsedMilliseconds;

            Move mv = null;
            PieceAbility pa = null;
            if (nextMove.moveType == "move")
            {
                mv = nextMove.move;

                movePieceObj = mv.p;
                moveCoords = mv.coords;
            }
            else if (nextMove.moveType == "ability")
            {
                pa = nextMove.ability;

                movePieceObj = pa.piece;
                moveCoords = pa.coords;
            }

            movePieceObj = getOriginalPieceFromClone(movePieceObj);

            selectedMove = nextMove;

            if (watchMS > 5000)
            {
                currentBot.penalty = true;

                Debug.Log("BOT HAS RECIEVED A PENALTY. MOVE TOOK " + watchMS + "ms.");

                if (currentBot.color == 1)
                {
                    bgs.whitePenalties++;
                }
                else
                {
                    bgs.blackPenalties++;
                }
            }

            Debug.Log("RECIEVED MOVE: " + movePieceObj.name + " to " + moveCoords[0] + "," + moveCoords[1]);

            if (nextMove.moveType == "move")
            {
                valid = botValidateMove(movePieceObj, moveCoords);
            } else
            {
                valid = botValidateAbility(pa, currentBot);
            }
        }

        movePieceObj = getOriginalPieceFromClone(movePieceObj);

        bool death;
        int check;
        if (valid)
        {
            gameData.selected = HelperFunctions.findSquare(moveCoords[0], moveCoords[1]);
            gameData.selectedToMove = HelperFunctions.findSquare(movePieceObj.position[0], movePieceObj.position[1]);
            gameData.selectedPiece = HelperFunctions.getPieceOnSquare(gameData.selected);
            gameData.selectedToMovePiece = movePieceObj;

            Debug.Log("Bot " + currentBot.name + " moved " + movePieceObj.name + " to " + HelperFunctions.findSquare(moveCoords[0], moveCoords[1]).name + " in " + watchMS + "ms.");
            if (selectedMove.moveType == "move")
            {
                death = helper.performPreMove();
                check = helper.movePiece_(movePieceObj, moveCoords);
            }
            else
            {
                var deathVars = helper.executeAbility(selectedMove.ability);
                death = deathVars.death;
                check = deathVars.check;
            }
        }
        else
        {
            Debug.Log("MOVE BY " + currentBot.color + " IS INVALID - PERFORMING RANDOM MOVE");
            if (movePieceObj != null)
            {
                Debug.Log("Attempted Move: " + movePieceObj.name + " to " + moveCoords[0] + "," + moveCoords[1]);
            }
            else
            {
                Debug.Log("No Move Provided or Penalty");
            }

            NextMove randomMove = performRandomBotMove_(currentBot);

            if (randomMove.moveType == "move")
            {
                movePieceObj = randomMove.move.p;
                moveCoords = randomMove.move.coords;
            }
            else if (randomMove.moveType == "ability")
            {
                movePieceObj = randomMove.ability.piece;
                moveCoords = randomMove.ability.coords;
            }

            gameData.selected = HelperFunctions.findSquare(moveCoords[0], moveCoords[1]);
            gameData.selectedToMove = HelperFunctions.findSquare(movePieceObj.position[0], movePieceObj.position[1]);
            gameData.selectedPiece = HelperFunctions.getPieceOnSquare(gameData.selected);
            gameData.selectedToMovePiece = movePieceObj;

            if (randomMove.moveType == "move")
            {
                death = helper.performPreMove();
                check = helper.movePiece_(movePieceObj, moveCoords);
            }
            else
            {
                var deathVars = helper.executeAbility(randomMove.ability);
                death = deathVars.death;
                check = deathVars.check;
            }
        }

        turn *= -1;
        //currentBot.currentBoardState.refresh(gameData.boardGrid);

        if (!HelperFunctions.isPieceBaseTypeOnBoard("King", 1))
        {
            if (HelperFunctions.isPieceBaseTypeOnBoard("King", -1))
            {
                Debug.Log("Game Over - King Death");
                bgs.result = "Won by opposing king death";
                bgs.winner = "Black";
            }
            else
            {
                Debug.Log("Game Over - Tie");
                bgs.result = "Draw by both kings death";
            }

            kingDead = true;
            gameOver = true;
        }

        if (!HelperFunctions.isPieceBaseTypeOnBoard("King", -1))
        {
            if (HelperFunctions.isPieceBaseTypeOnBoard("King", 1))
            {
                Debug.Log("Game Over - King Death");
                bgs.result = "Won by opposing king death";
                bgs.winner = "White";
            }
            else
            {
                Debug.Log("Game Over - Tie");
                bgs.result = "Draw by both kings death";
            }

            kingDead = true;
            gameOver = true;
        }

        if (!death && check == 0) {
            if (turn == -1)
            {
                movesWithoutCapture++;
            }
            subsequentChecks = 0;
        }
        else if (death) {
            movesWithoutCapture = 0;
            subsequentChecks = 0;
        }
        else if (check == 1) {
            subsequentChecks++;
        }

        if (check == 2)
        {
            if (!gameData.staleMate)
            {
                Debug.Log("Game Over - Checkmate");
                bgs.result = "Won by Checkmate";
                if (turn == -1)
                {
                    bgs.winner = "White";
                }
                else
                {
                    bgs.winner = "Black";
                }
            }
            else
            {
                if (!kingDead)
                {
                    Debug.Log("Game Over - Stalemate");
                    bgs.result = "Draw by Stalemate";
                }
            }

            gameOver = true;
        }

        if (subsequentChecks > 8 || movesWithoutCapture > 25)
        {
            //GAME OVER, go to next match
            Debug.Log("Game Over - Tie");
            gameOver = true;

            if (subsequentChecks > 8)
            {
                bgs.result = "Draw by Subsequent Checks";
            }
            else
            {
                bgs.result = "Draw by Moves Without Capture";
            }
        }

        //BotHelperFunctions.debug_printBoardGrid(gameData.boardGrid);
        yield return new WaitForSeconds(0.5f);
        isTurn = true;

        if (gameOver)
        {
            printBGS(bgs);
            yield return new WaitForSeconds(5f);
        }

        //Check if check/update bot boardstate

        HelperFunctions.resetBoardColours();
    }

    public bool botValidateMove(Piece piece, int[] coords) {
        if (piece == null || coords == null)
        {
            return false;
        }

        List<int[]> moves = HelperFunctions.addMovesToCurrentMoveableCoords(piece);

        if (HelperFunctions.isInList(moves, coords, false)) {
            return true;
        }

        return false;
    }

    public static (Piece piece, int[] coords) performRandomBotMove(BotTemplate bot)
    {
        var botMoves = getAllPossibleMovesPenalty(bot, bot.color);

        List<Dictionary<Piece, List<int[]>>> allMoves = botMoves.pieceMoveList;

        if (allMoves.Count == 0)
        {
            return (null, null);
        }

        System.Random rand = new System.Random();

        int dictIndex = rand.Next(allMoves.Count);

        Dictionary<Piece, List<int[]>> pieceMovesDict = allMoves[dictIndex];
        //fix for checkamte
        KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = pieceMovesDict.First();

        Piece randMovePiece = pieceMovesKeyVal.Key;
        List<int[]> randMoveCoordsList = pieceMovesKeyVal.Value;

        int coordIndex = rand.Next(randMoveCoordsList.Count);
        int[] randMoveCoords = randMoveCoordsList[coordIndex];

        return (randMovePiece, randMoveCoords);
    }

    public static NextMove performRandomBotMove_(BotTemplate bot)
    {
        var botMoves = getAllPossibleMovesPenalty(bot, bot.color);

        List<Dictionary<Piece, List<int[]>>> allMoves = botMoves.pieceMoveList;
        List<PieceAbility> allAbilities = botMoves.pieceAbilities;

        int totalCount = allMoves.Count + allAbilities.Count;

        System.Random rand = new System.Random();
        int dictIndex = rand.Next(totalCount);

        NextMove next;

        if (dictIndex >= allMoves.Count)
        {
            dictIndex -= allMoves.Count;

            PieceAbility ability = allAbilities[dictIndex];
            next = new NextMove(ability);
        }
        else
        {
            Dictionary<Piece, List<int[]>> pieceMovesDict = allMoves[dictIndex];
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = pieceMovesDict.First();

            Piece randMovePiece = pieceMovesKeyVal.Key;
            List<int[]> randMoveCoordsList = pieceMovesKeyVal.Value;

            int coordIndex = rand.Next(randMoveCoordsList.Count);
            int[] randMoveCoords = randMoveCoordsList[coordIndex];

            Move move_ = new Move(randMovePiece, randMoveCoords);
            next = new NextMove(move_);
        }

        return next;
    }

    public bool botValidateAbility(PieceAbility pa, BotTemplate bot)
    {
        BoardState bs = new BoardState();
        bs.refresh(gameData.boardGrid);

        List<PieceAbility> pieceAbilities = getAllPossibleBotAbilities(bot, bs, bot.color);

        foreach (PieceAbility pa_ in pieceAbilities)
        {
            if (pa_.ability != pa.ability)
            {
                continue;
            }

            if (pa_.piece.name != pa.piece.name)
            {
                continue;
            }

            if (pa_.coords != null && pa.coords != null)
            {
                if (pa_.coords[0] != pa.coords[0])
                {
                    continue;
                }

                if (pa_.coords[1] != pa.coords[1])
                {
                    continue;
                }
            }

            if (pa_.secondPiece != null && pa.piece != null)
            {
                if (pa_.secondPiece.name != pa.secondPiece.name)
                {
                    continue;
                }
            }

            bool breaked = false;

            if (pa_.placePieces != null && pa.placePieces != null)
            {
                foreach (Piece p_ in pa_.placePieces)
                {

                    bool matched = false;

                    foreach (Piece p in pa.placePieces)
                    {
                        if (p_.name == p.name)
                        {
                            matched = true;
                        }
                    }

                    if (!matched)
                    {
                        breaked = true;
                        break;
                    }
                }
            }

            if (breaked == true)
            {
                continue;
            }

            bool breakedCoords = false;

            if (pa_.placeCoords != null && pa.placeCoords != null)
            {
                foreach (int[] c_ in pa_.placeCoords)
                {

                    bool matchedCoords = false;

                    foreach (int[] c in pa.placeCoords)
                    {
                        if (c[0] == c_[0] && c[1] == c_[1])
                        {
                            matchedCoords = true;
                        }
                    }

                    if (!matchedCoords)
                    {
                        breakedCoords = true;
                        break;
                    }
                }
            }

            if (breakedCoords == true)
            {
                continue;
            }

            return true;
        }

        return false;
    }

    public static (List<Dictionary<Piece, List<int[]>>> pieceMoveList, List<PieceAbility> pieceAbilities) getAllPossibleMovesPenalty(BotTemplate bot, int color)
    {
        List<Dictionary<Piece, List<int[]>>> totalMoves = new List<Dictionary<Piece, List<int[]>>>();

        foreach (Piece piece in bot.pieces)
        {
            List<int[]> moves = HelperFunctions.addMovesToCurrentMoveableCoords(piece);

            if (moves.Count > 0)
            {
                Dictionary<Piece, List<int[]>> pMoveDict = new Dictionary<Piece, List<int[]>>();

                pMoveDict.Add(piece, moves);

                totalMoves.Add(pMoveDict);
            }
        }

        BoardState bs = new BoardState();
        bs.refresh(gameData.boardGrid);

        List<PieceAbility> pieceAbility = getAllPossibleBotAbilities(bot, bs, color);

        return (totalMoves, pieceAbility);

    }

    public static void printBGS(BotGameStatus bgs)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Game Ended");
        sb.AppendLine(bgs.white + " (White) vs (Black) " + bgs.black);
        sb.AppendLine(bgs.winner + " " + bgs.result);
        sb.AppendLine("The match took " + bgs.numTurns + " turns");
        sb.AppendLine("Penalties: " + bgs.white + ": " + bgs.whitePenalties + ", " + bgs.black + ": " + bgs.blackPenalties);

        sb.AppendLine(bgs.white + " started with " + bgs.whitePoints + "pts");
        sb.AppendLine(bgs.black + " started with " + bgs.blackPoints + "pts");

        sb.AppendLine(bgs.white + " pieces:");
        foreach (string p in bgs.whitePieces)
            sb.Append(p + " ");
        sb.AppendLine();

        sb.AppendLine(bgs.black + " pieces:");
        foreach (string p in bgs.blackPieces)
            sb.Append(p + " ");
        sb.AppendLine();

        string logText = sb.ToString();

        // Console output
        Debug.LogWarning(logText);

        // File path (SAFE location)
        string filePath = Path.Combine(Application.persistentDataPath, "MatchHistory.txt");

        // Append instead of overwrite
        File.AppendAllText(filePath, logText + "\n------------------------\n");

        Debug.Log("Saved match log to: " + filePath);
    }

    public static void resetBotPieces(BotTemplate bot)
    {
        bot.pieces.Clear();
        bot.opponentPieces.Clear();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece p in gameData.boardGrid[x][y])
                {
                    if (p.color == bot.color)
                    {
                        bot.pieces.Add(p);
                    }
                    else
                    {
                        bot.opponentPieces.Add(p);
                    }
                }
            }
        }
    }
}

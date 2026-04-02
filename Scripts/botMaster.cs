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
using UnityEngine.SceneManagement;

public class botMaster : MonoBehaviour
{
    public GameObject board2;
    public GameObject boardWrapper;
    public GameObject checkmateUI;
    public HelperFunctions helper;

    BotTemplate botWhite;
    BotTemplate botBlack;
    bool started = false;

    BotGameStatus bgs = new BotGameStatus();

    IEnumerator Start()
    {

        // Tournament
        //Change this to true to run a tournament with random bots
        //This will only work for my bots, if you want to change that you can comment out
        //List<string> randomBots = nonResettables.get8RandomBots();
        //and replace it with
        //List<string> randomBots = new List<string>{"fsaf", "asd", "asdad", "asdasd", "asdad", "asda", "asdad", "ads"};
        nonResettables.isBotTournament = true;

        if (nonResettables.isBotTournament)
        {
            if (nonResettables.botTournament == null)
            {
                List<string> randomBots = nonResettables.get8RandomBots();

                StringBuilder sb = new StringBuilder();
                foreach (string bot in randomBots)
                {
                    sb.Append(bot + " ");
                }
                Debug.Log("Starting Tournament With: " + sb);

                nonResettables.botTournament = new BotTournament(randomBots[0], randomBots[1], randomBots[2], randomBots[3], randomBots[4], randomBots[5], randomBots[6], randomBots[7], true);
            }

            var bots = nonResettables.botTournament.nextGame();

            if (bots.botWhite == "")
            {
                gameOver = true;
                yield return null;
            }

            Type botWhiteType = Type.GetType(bots.botWhite + ", Assembly-CSharp");
            Type botBlackType = Type.GetType(bots.botBlack + ", Assembly-CSharp");

            if (botWhiteType == null || botBlackType == null)
            {
                gameOver = true;
                yield return null;
            }

            if (!gameOver)
            {
                botWhite = (BotTemplate)Activator.CreateInstance(botWhiteType, 1);
                botBlack = (BotTemplate)Activator.CreateInstance(botBlackType, -1);
            }
        }
        
        if (!gameOver)
        { 
            gameData.playMode = "BotvBot";
            gameData.turn = 1;
            gameData.board = board2;

            if (!nonResettables.isBotTournament)
            {
                //Replace these with your bots if it is a tournament
                botWhite = new OneMoveBot(1);
                botBlack = new OneMoveBot(-1);
            }

            gameData.botWhite = botWhite;
            gameData.botBlack = botBlack;

            helper.panel.Initialize();
            gameData.helper = helper;

            bgs.white = botWhite.name;
            bgs.black = botBlack.name;

            gameData.boardGrid = HelperFunctions.initBoardGrid();

            List<Piece> botWhitePawns = BotHelperFunctions.filterPieces("Pawn", botWhite.pieces);
            /*
            List<Piece> botWhitePawns = new List<Piece>
            {
                getPieceTypeInstance("SpittingPawn", 1),
                getPieceTypeInstance("SpittingPawn", 1),
                getPieceTypeInstance("SpittingPawn", 1),
                getPieceTypeInstance("SpittingPawn", 1),
                getPieceTypeInstance("SpittingPawn", 1),
                getPieceTypeInstance("SpittingPawn", 1),
                getPieceTypeInstance("SpittingPawn", 1),
                getPieceTypeInstance("SpittingPawn", 1),
            };
            */

            List<Piece> botWhiteRooks = BotHelperFunctions.filterPieces("Rook", botWhite.pieces);
            /*
            List<Piece> botWhiteRooks = new List<Piece>
            {
                getPieceTypeInstance("GhostRook", 1),
                getPieceTypeInstance("GhostRook", 1),
            };
            */
            List<Piece> botWhiteBishops = BotHelperFunctions.filterPieces("Bishop", botWhite.pieces);
            /*
            List<Piece> botWhiteBishops = new List<Piece>
            {
                getPieceTypeInstance("SpittingBishop", 1),
                getPieceTypeInstance("SpittingBishop", 1),
            };
            */
            List<Piece> botWhiteKnights = BotHelperFunctions.filterPieces("Knight", botWhite.pieces);
            /*
            List<Piece> botWhiteKnights = new List<Piece>
            {
                getPieceTypeInstance("SpittingKnight", 1),
                getPieceTypeInstance("SpittingKnight", 1),
            };
            */
            List<Piece> botWhiteKing = BotHelperFunctions.filterPieces("King", botWhite.pieces);
            /*
            List<Piece> botWhiteKing = new List<Piece>
            {
                getPieceTypeInstance("SpittingKing", 1)
            };
            */
            List<Piece> botWhiteQueen = BotHelperFunctions.filterPieces("Queen", botWhite.pieces);
            /*
            List<Piece> botWhiteQueen = new List<Piece>
            {
                getPieceTypeInstance("SpittingQueen", 1)
            };
            */

            foreach (Piece wp in botWhite.pieces)
            {
                bgs.whitePieces.Add(wp.name.Replace(" ", string.Empty));
            }

            HelperFunctions.initPiece(botWhitePawns[0], new int[] { 1, 2 });
            HelperFunctions.initPiece(botWhitePawns[1], new int[] { 2, 2 });
            HelperFunctions.initPiece(botWhitePawns[2], new int[] { 3, 2 });
            HelperFunctions.initPiece(botWhitePawns[3], new int[] { 4, 2 });
            HelperFunctions.initPiece(botWhitePawns[4], new int[] { 5, 2 });
            HelperFunctions.initPiece(botWhitePawns[5], new int[] { 6, 2 });
            HelperFunctions.initPiece(botWhitePawns[6], new int[] { 7, 2 });
            HelperFunctions.initPiece(botWhitePawns[7], new int[] { 8, 2 });
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
                getPieceTypeInstance("Pawn", -1),
                getPieceTypeInstance("Pawn", -1),
                getPieceTypeInstance("Pawn", -1),
                getPieceTypeInstance("Pawn", -1),
                getPieceTypeInstance("Pawn", -1),
                getPieceTypeInstance("Pawn", -1),
                getPieceTypeInstance("Pawn", -1),
                getPieceTypeInstance("Pawn", -1),
            };
            */

            List<Piece> botBlackRooks = BotHelperFunctions.filterPieces("Rook", botBlack.pieces);
            /*
            List<Piece> botBlackRooks = new List<Piece>
            {
                getPieceTypeInstance("Empress", -1),
                getPieceTypeInstance("Empress", -1),
            };
            */
            List<Piece> botBlackBishops = BotHelperFunctions.filterPieces("Bishop", botBlack.pieces);
            /*
            List<Piece> botBlackBishops = new List<Piece>
            {
                getPieceTypeInstance("HungryBishop", -1),
                getPieceTypeInstance("PortalBishop", -1),
            };
            */
            List<Piece> botBlackKnights = BotHelperFunctions.filterPieces("Knight", botBlack.pieces);
            /*
            List<Piece> botBlackKnights = new List<Piece>
            {
                getPieceTypeInstance("CrowdingKnight", -1),
                getPieceTypeInstance("Knight", -1),
            };
            */
            List<Piece> botBlackKing = BotHelperFunctions.filterPieces("King", botBlack.pieces);
            /*
            List<Piece> botBlackKing = new List<Piece>
            {
                getPieceTypeInstance("HeartbrokenKing", -1),
            };
            */
            List<Piece> botBlackQueen = BotHelperFunctions.filterPieces("Queen", botBlack.pieces);
            /*
            List<Piece> botBlackQueen = new List<Piece>
            {
                getPieceTypeInstance("PhantomQueen", -1),
            };
            */

            foreach (Piece wp in botBlack.pieces)
            {
                bgs.blackPieces.Add(wp.name.Replace(" ", string.Empty));
            }

            HelperFunctions.initPiece(botBlackPawns[0], new int[] { 1, 7 });
            HelperFunctions.initPiece(botBlackPawns[1], new int[] { 2, 7 });
            HelperFunctions.initPiece(botBlackPawns[2], new int[] { 3, 7 });
            HelperFunctions.initPiece(botBlackPawns[3], new int[] { 4, 7 });
            HelperFunctions.initPiece(botBlackPawns[4], new int[] { 5, 7 });
            HelperFunctions.initPiece(botBlackPawns[5], new int[] { 6, 7 });
            HelperFunctions.initPiece(botBlackPawns[6], new int[] { 7, 7 });
            HelperFunctions.initPiece(botBlackPawns[7], new int[] { 8, 7 });
            HelperFunctions.initPiece(botBlackRooks[0], new int[] { 1, 8 });
            HelperFunctions.initPiece(botBlackRooks[1], new int[] { 8, 8 });
            HelperFunctions.initPiece(botBlackBishops[0], new int[] { 3, 8 });
            HelperFunctions.initPiece(botBlackBishops[1], new int[] { 6, 8 });
            HelperFunctions.initPiece(botBlackKnights[0], new int[] { 2, 8 });
            HelperFunctions.initPiece(botBlackKnights[1], new int[] { 7, 8 });
            HelperFunctions.initPiece(botBlackQueen[0], new int[] { 4, 8 });
            HelperFunctions.initPiece(botBlackKing[0], new int[] { 5, 8 });

            gameData.whiteRooks.Add(botWhiteRooks[0]);
            gameData.whiteRooks.Add(botWhiteRooks[1]);

            gameData.blackRooks.Add(botBlackRooks[0]);
            gameData.blackRooks.Add(botBlackRooks[1]);

            gameData.whiteKing = botWhiteKing[0];
            gameData.blackKing = botBlackKing[0];

            botWhite.king = gameData.whiteKing;
            botBlack.king = gameData.blackKing;

            botWhite.currentBoardState.refresh(convertBoardGrid(gameData.boardGrid));
            List<float> points = getPointsOnBoardState(botWhite.currentBoardState, false);
            bgs.whitePoints = points[0];
            bgs.blackPoints = points[1];

            helper.updatePointsOnBoard();

            yield return null;
            started = true;

            movesWithoutCapture = 0;
            subsequentChecks = 0;
            turn = 1;
            gameOver = false;
            kingDead = false;
        }
    }

    int turn = 1;
    bool isTurn = true;
    int movesWithoutCapture = 0;
    float subsequentChecks = 0;

    bool gameOver = false;
    bool kingDead = false;

    bool checkLastTurn = false;

    void Update()
    {
        if (!isTurn || !started || gameOver) return;

        isTurn = false;

        StartCoroutine(BotTurn());
    }

    IEnumerator BotTurn()
    {
        yield return new WaitForSeconds(0.1f);

        resetBotPieces(botWhite);
        resetBotPieces(botBlack);
        botWhite.currentBoardState.refresh(convertBoardGrid(gameData.boardGrid));
        botBlack.currentBoardState.refresh(convertBoardGrid(gameData.boardGrid));

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

        List<int[]> allMoves = HelperFunctions.addToCurrentMoveableCoordsTotal(currentBot.color, true, false, null, true, true);
        Debug.LogWarning("AllMoves: " + allMoves.Count);
        debug_printBoardGrid(gameData.boardGrid);

        if (currentBot.penalty)
        {
            Debug.Log("Bot " + currentBot.name + " has a penalty. Executing random move");
            helper.addBotMessage(" " + currentBot.name + " has a penalty. Executing random move");
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

            if (moveCoords != null && movePieceObj != null)
            {
                HelperFunctions.highlightSquare(HelperFunctions.findSquare(movePieceObj.position[0], movePieceObj.position[1]), Color.green);
                HelperFunctions.highlightSquare(HelperFunctions.findSquare(moveCoords[0], moveCoords[1]), Color.red);
            }
            yield return new WaitForSeconds(0.5f);

            selectedMove = nextMove;

            if (watchMS > 5000)
            {
                currentBot.penalty = true;

                Debug.Log("BOT HAS RECIEVED A PENALTY. MOVE TOOK " + watchMS + "ms.");
                helper.addBotMessage(" " + currentBot.name + " has recieved a penalty. Move took " + watchMS + "ms.");

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

        bool death = false;
        bool countDeath = false;
        int check = 0;
        if (valid)
        {
            gameData.selected = HelperFunctions.findSquare(moveCoords[0], moveCoords[1]);
            gameData.selectedToMove = HelperFunctions.findSquare(movePieceObj.position[0], movePieceObj.position[1]);
            gameData.selectedPiece = HelperFunctions.getPieceOnSquare(gameData.selected);
            gameData.selectedToMovePiece = movePieceObj;

            Debug.Log("Bot " + currentBot.name + " moved " + movePieceObj.name + " to " + HelperFunctions.findSquare(moveCoords[0], moveCoords[1]).name + " in " + watchMS + "ms.");
            helper.addBotMessage(" " + currentBot.name + " moved " + movePieceObj.name + " to " + HelperFunctions.findSquare(moveCoords[0], moveCoords[1]).name + " in " + watchMS + "ms.");
            if (selectedMove.moveType == "move")
            {
                if (HelperFunctions.checkState(movePieceObj, PieceState.Delayed))
                {
                    death = false;
                    countDeath = false;
                }
                else
                {
                    var premoveVars = helper.performPreMove();
                    death = premoveVars.death;
                    countDeath = premoveVars.countDeath;
                }
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
            helper.addBotMessage(" Move by " + currentBot.name + " is invalid. Performing Random Move");

            if (movePieceObj != null)
            {
                Debug.Log("Attempted Move: " + movePieceObj.name + " to " + moveCoords[0] + "," + moveCoords[1]);
                helper.addBotMessage(" Attempted Move: " + movePieceObj.name + " to " + moveCoords[0] + "," + moveCoords[1]);
            }
            else
            {
                Debug.Log("No Move Provided or Penalty");
                helper.addBotMessage(" No Move Provided or Penalty");
            }

            NextMove randomMove = performRandomBotMove_(currentBot);

            if (randomMove == null)
            {
                Debug.Log("Game Over - Stalemate (Condition 1)");
                bgs.result = "Draw by Stalemate";
            }
            else
            {
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
                    if (HelperFunctions.checkState(movePieceObj, PieceState.Delayed))
                    {
                        death = false;
                        countDeath = false;
                    }
                    else
                    {
                        var premoveVars = helper.performPreMove();
                        death = premoveVars.death;
                        countDeath = premoveVars.countDeath;
                    }

                    check = helper.movePiece_(movePieceObj, moveCoords);
                }
                else
                {
                    var deathVars = helper.executeAbility(randomMove.ability);
                    death = deathVars.death;
                    check = deathVars.check;
                }
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

                bgs.winnerName = botBlack.name;
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

                bgs.winnerName = botWhite.name;
            }
            else
            {
                Debug.Log("Game Over - Tie");
                bgs.result = "Draw by both kings death";
            }

            kingDead = true;
            gameOver = true;
        }

        if (check == 1 || checkLastTurn == true)
        {
            subsequentChecks += 0.5f;
        }

        if (!countDeath && check == 0) {
            movesWithoutCapture++;
            subsequentChecks = 0;
        }
        else if (countDeath) {
            movesWithoutCapture = 0;
            subsequentChecks = 0;
        }
        else if (check == 1) {
            checkLastTurn = true;
        }

        if (check == 0)
        {
            checkLastTurn = false;
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
                    bgs.winnerName = botWhite.name;
                }
                else
                {
                    bgs.winner = "Black";
                    bgs.winnerName = botBlack.name;
                }
            }
            else
            {
                if (!kingDead)
                {
                    Debug.Log("Game Over - Stalemate (Condition 2)");
                    bgs.result = "Draw by Stalemate";
                }
            }

            gameOver = true;
        }

        if (subsequentChecks > 8 || movesWithoutCapture > 45)
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
        HelperFunctions.resetBoardColours();
        yield return new WaitForSeconds(0.1f);
        isTurn = true;

        if (gameOver)
        {
            printBGS(bgs);

            if (nonResettables.isBotTournament) nonResettables.postBotMatch(bgs.white, bgs.black, bgs.winnerName);

            yield return new WaitForSeconds(5f);

            HelperFunctions.resetGameVars();
            SceneManager.LoadScene(7);
        }

        //Check if check/update bot boardstate
        Debug.LogWarning("Moves Without Capture: " + movesWithoutCapture);
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

        if (totalCount == 0)
        {
            return null;
        }

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
        bs.refresh(convertBoardGrid(gameData.boardGrid));

        List<PieceAbility> pieceAbilities = HelperFunctions.isCheckPieceAbilities(bs, bot, bot.color);

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

            //TODO IMPORTANT simulate ability, check if check, return false

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
        bs.refresh(convertBoardGrid(gameData.boardGrid));

        List<PieceAbility> pieceAbilityCheck = HelperFunctions.isCheckPieceAbilities(bs, bot, color);
        return (totalMoves, pieceAbilityCheck);

    }

    public void printBGS(BotGameStatus bgs)
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

        Debug.LogWarning(logText);
        string filePath = Path.Combine(Application.persistentDataPath, "MatchHistory.txt");
        File.AppendAllText(filePath, logText + "\n------------------------\n");

        Debug.Log("Saved match log to: " + filePath);

        helper.addBotMessage(" " + bgs.white + " (White) vs (Black) " + bgs.black);
        helper.addBotMessage(" " + bgs.winner + " " + bgs.result);
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

                        if (p.baseType == "King")
                        {
                            bot.king = p;
                        }
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

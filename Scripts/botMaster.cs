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
    public AudioSource thinkingBotII;
    GameLogger logger = new GameLogger();

    BotTemplate botWhite;
    BotTemplate botBlack;
    bool started = true;

    static string SEASON_NAME;

    BotGameStatus bgs = new BotGameStatus();
    float waitTime;

    IEnumerator Start()
    {
        // Tournament
        //Change this to true to run a tournament with random bots
        //This will only work for my bots, if you want to change that you can comment out
        //List<string> randomBots = nonResettables.get8RandomBots();
        //and replace it with
        //List<string> randomBots = new List<string>{"fsaf", "asd", "asdad", "asdasd", "asdad", "asda", "asdad", "ads"};
        
        nonResettables.isBotTournament = true;
        //SEASON_NAME = "LCC_SEASON1";
        waitTime = 0f;
        nonResettables.logMatch = false;
        nonResettables.ruleset = "Wacky";

        if (nonResettables.isBotTournament)
        {
            if (nonResettables.botTournament == null)
            {
                List<string> forceNames = new List<string>
                {
                    "Botkrieg"
                };

                List<string> randomBots = nonResettables.get8RandomBots(forceNames, "Jaden");

                List<string> div1 = new List<string>
                {
                    Bots.RandomBot,
                    Bots.IdiotBot,
                    Bots.OnePieceRandomBot,
                    Bots.TwoMoveBot,
                    Bots.SavageBeastBot,
                    Bots.Lobotomy,
                    Bots.HitmanBot,
                    Bots.RestrictorBot,
                };

                List<string> div2 = new List<string>
                {
                    Bots.BOTential,
                    Bots.OneMoveBot,
                    Bots.EqualityBot,
                    Bots.BotsUnited,
                    Bots.Lobotomy,
                    Bots.OnePieceRandomBot,
                    Bots.BotDefender,
                    Bots.ChristopherColumbot,
                };

                List<string> div3 = new List<string>
                {
                    Bots.AdventurousKingBot,
                    Bots.ShieldBot,
                    Bots.SavageBeastBot,
                    Bots.RandomBot,
                    Bots.BotRoss,
                    Bots.Bloodbot,
                    Bots.FiveXRandomBot,
                    Bots.HitmanBot,
                };

                List<string> div4 = new List<string>
                {
                    Bots.EqualityBot,
                    Bots.BotDefender,
                    Bots.ShieldBot,
                    Bots.Abilibot,
                    Bots.IdiotBot,
                    Bots.FiveXRandomBot,
                    Bots.BotRoss,
                    Bots.RandomBot,
                };

                StringBuilder sb = new StringBuilder();
                foreach (string bot in randomBots)
                {
                    sb.Append(bot + " ");
                }
                Debug.Log("Starting Tournament With: " + sb);

                nonResettables.botTournament = new BotTournament(randomBots[0], randomBots[1], randomBots[2], randomBots[3], randomBots[4], randomBots[5], randomBots[6], randomBots[7], true);

                //Div 1
                //nonResettables.botTournament = new BotTournament(div1[0], div1[1], div1[2], div1[3], div1[4], div1[5], div1[6], div1[7], false);

                //Div 2
                //nonResettables.botTournament = new BotTournament(div2[0], div2[1], div2[2], div2[3], div2[4], div2[5], div2[6], div2[7], false);

                //Div 3
                //nonResettables.botTournament = new BotTournament(div3[0], div3[1], div3[2], div3[3], div3[4], div3[5], div3[6], div3[7], false);

                //Div 4 
                //nonResettables.botTournament = new BotTournament(div4[0], div4[1], div4[2], div4[3], div4[4], div4[5], div4[6], div4[7], false);
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
                botWhite = new Botkrieg(1);
                botBlack = new Bot618(-1);
                /*
                // For WCSingle
                int rand = globalDefs.globalRand.Next(1, 3);
                if (rand == 1)
                {
                    botWhite = new HitmanBot(1);
                    botBlack = new RestrictorBot(-1);
                }
                else
                {
                    botWhite = new RestrictorBot(1);
                    botBlack = new HitmanBot(-1);
                }
                */
            }

            gameData.botWhite = botWhite;
            gameData.botBlack = botBlack;

            helper.panel.Initialize();
            gameData.helper = helper;

            bgs.white = botWhite.name;
            bgs.black = botBlack.name;

            gameData.boardGrid = HelperFunctions.initBoardGrid();

            List<Piece> botWhitePawns;
            List<Piece> botWhiteRooks;
            List<Piece> botWhiteBishops;
            List<Piece> botWhiteKnights;
            List<Piece> botWhiteKing;
            List<Piece> botWhiteQueen;

            List<Piece> botBlackPawns;
            List<Piece> botBlackRooks;
            List<Piece> botBlackBishops;
            List<Piece> botBlackKnights;
            List<Piece> botBlackKing;
            List<Piece> botBlackQueen;

            if (nonResettables.ruleset == "Wacky")
            {
                botWhitePawns = filterPieces("Pawn", botWhite.pieces);
                botWhiteRooks = filterPieces("Rook", botWhite.pieces);
                botWhiteBishops = filterPieces("Bishop", botWhite.pieces);
                botWhiteKnights = filterPieces("Knight", botWhite.pieces);
                botWhiteKing = filterPieces("King", botWhite.pieces);
                botWhiteQueen = filterPieces("Queen", botWhite.pieces);

                botBlackPawns = filterPieces("Pawn", botBlack.pieces);
                botBlackRooks = filterPieces("Rook", botBlack.pieces);
                botBlackBishops = filterPieces("Bishop", botBlack.pieces);
                botBlackKnights = filterPieces("Knight", botBlack.pieces);
                botBlackKing = filterPieces("King", botBlack.pieces);
                botBlackQueen = filterPieces("Queen", botBlack.pieces);
            }
            else if (nonResettables.ruleset == "Fusion")
            {
                botWhitePawns = new List<Piece>
                {
                    getPieceTypeInstance("FusionPawn", 1),
                    getPieceTypeInstance("FusionPawn", 1),
                    getPieceTypeInstance("FusionPawn", 1),
                    getPieceTypeInstance("FusionPawn", 1),
                    getPieceTypeInstance("FusionPawn", 1),
                    getPieceTypeInstance("FusionPawn", 1),
                    getPieceTypeInstance("FusionPawn", 1),
                    getPieceTypeInstance("FusionPawn", 1),
                };

                botWhiteRooks = new List<Piece>
                {
                    getPieceTypeInstance("FusionRook", 1),
                    getPieceTypeInstance("FusionRook", 1),
                };

                botWhiteBishops = new List<Piece>
                {
                    getPieceTypeInstance("FusionBishop", 1),
                    getPieceTypeInstance("FusionBishop", 1),
                };
                botWhiteKnights = new List<Piece>
                {
                    getPieceTypeInstance("FusionKnight", 1),
                    getPieceTypeInstance("FusionKnight", 1),
                };
                botWhiteKing = new List<Piece>
                {
                    getPieceTypeInstance("FusionKing", 1)
                };

                botWhiteQueen = new List<Piece>
                {
                    getPieceTypeInstance("FusionQueen", 1)
                };


                botBlackPawns = new List<Piece>
                {
                    getPieceTypeInstance("FusionPawn", -1),
                    getPieceTypeInstance("FusionPawn", -1),
                    getPieceTypeInstance("FusionPawn", -1),
                    getPieceTypeInstance("FusionPawn", -1),
                    getPieceTypeInstance("FusionPawn", -1),
                    getPieceTypeInstance("FusionPawn", -1),
                    getPieceTypeInstance("FusionPawn", -1),
                    getPieceTypeInstance("FusionPawn", -1),
                };

                botBlackRooks = new List<Piece>
                {
                    getPieceTypeInstance("FusionRook", -1),
                    getPieceTypeInstance("FusionRook", -1),
                };

                botBlackBishops = new List<Piece>
                {
                    getPieceTypeInstance("FusionBishop", -1),
                    getPieceTypeInstance("FusionBishop", -1),
                };
                botBlackKnights = new List<Piece>
                {
                    getPieceTypeInstance("FusionKnight", -1),
                    getPieceTypeInstance("FusionKnight", -1),
                };
                botBlackKing = new List<Piece>
                {
                    getPieceTypeInstance("FusionKing", -1),
                };

                botBlackQueen = new List<Piece>
                {
                    getPieceTypeInstance("FusionQueen", -1),
                };
            }
            else if (nonResettables.ruleset == "Landmine")
            {
                botWhitePawns = new List<Piece>
                {
                    getPieceTypeInstance("LandminePawn", 1),
                    getPieceTypeInstance("LandminePawn", 1),
                    getPieceTypeInstance("LandminePawn", 1),
                    getPieceTypeInstance("LandminePawn", 1),
                    getPieceTypeInstance("LandminePawn", 1),
                    getPieceTypeInstance("LandminePawn", 1),
                    getPieceTypeInstance("LandminePawn", 1),
                    getPieceTypeInstance("LandminePawn", 1),
                };

                botWhiteRooks = new List<Piece>
                {
                    getPieceTypeInstance("LandmineRook", 1),
                    getPieceTypeInstance("LandmineRook", 1),
                };

                botWhiteBishops = new List<Piece>
                {
                    getPieceTypeInstance("LandmineBishop", 1),
                    getPieceTypeInstance("LandmineBishop", 1),
                };
                botWhiteKnights = new List<Piece>
                {
                    getPieceTypeInstance("LandmineKnight", 1),
                    getPieceTypeInstance("LandmineKnight", 1),
                };
                botWhiteKing = new List<Piece>
                {
                    getPieceTypeInstance("LandmineKing", 1)
                };

                botWhiteQueen = new List<Piece>
                {
                    getPieceTypeInstance("LandmineQueen", 1)
                };


                botBlackPawns = new List<Piece>
                {
                    getPieceTypeInstance("LandminePawn", -1),
                    getPieceTypeInstance("LandminePawn", -1),
                    getPieceTypeInstance("LandminePawn", -1),
                    getPieceTypeInstance("LandminePawn", -1),
                    getPieceTypeInstance("LandminePawn", -1),
                    getPieceTypeInstance("LandminePawn", -1),
                    getPieceTypeInstance("LandminePawn", -1),
                    getPieceTypeInstance("LandminePawn", -1),
                };

                botBlackRooks = new List<Piece>
                {
                    getPieceTypeInstance("LandmineRook", -1),
                    getPieceTypeInstance("LandmineRook", -1),
                };

                botBlackBishops = new List<Piece>
                {
                    getPieceTypeInstance("LandmineBishop", -1),
                    getPieceTypeInstance("LandmineBishop", -1),
                };
                botBlackKnights = new List<Piece>
                {
                    getPieceTypeInstance("LandmineKnight", -1),
                    getPieceTypeInstance("LandmineKnight", -1),
                };
                botBlackKing = new List<Piece>
                {
                    getPieceTypeInstance("LandmineKing", -1),
                };

                botBlackQueen = new List<Piece>
                {
                    getPieceTypeInstance("LandmineQueen", -1),
                };
            }
            else //if (nonResettables.ruleset == "Normal")
            {
                botWhitePawns = new List<Piece>
                {
                    getPieceTypeInstance("Pawn", 1),
                    getPieceTypeInstance("Pawn", 1),
                    getPieceTypeInstance("Pawn", 1),
                    getPieceTypeInstance("Pawn", 1),
                    getPieceTypeInstance("Pawn", 1),
                    getPieceTypeInstance("Pawn", 1),
                    getPieceTypeInstance("Pawn", 1),
                    getPieceTypeInstance("Pawn", 1),
                };

                botWhiteRooks = new List<Piece>
                {
                    getPieceTypeInstance("Rook", 1),
                    getPieceTypeInstance("Rook", 1),
                };

                botWhiteBishops = new List<Piece>
                {
                    getPieceTypeInstance("Bishop", 1),
                    getPieceTypeInstance("Bishop", 1),
                };
                botWhiteKnights = new List<Piece>
                {
                    getPieceTypeInstance("Knight", 1),
                    getPieceTypeInstance("Knight", 1),
                };
                botWhiteKing = new List<Piece>
                {
                    getPieceTypeInstance("King", 1)
                };

                botWhiteQueen = new List<Piece>
                {
                    getPieceTypeInstance("Queen", 1)
                };


                botBlackPawns = new List<Piece>
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

                botBlackRooks = new List<Piece>
                {
                    getPieceTypeInstance("Rook", -1),
                    getPieceTypeInstance("Rook", -1),
                };

                botBlackBishops = new List<Piece>
                {
                    getPieceTypeInstance("Bishop", -1),
                    getPieceTypeInstance("Bishop", -1),
                };
                botBlackKnights = new List<Piece>
                {
                    getPieceTypeInstance("Knight", -1),
                    getPieceTypeInstance("Knight", -1),
                };
                botBlackKing = new List<Piece>
                {
                    getPieceTypeInstance("King", -1),
                };

                botBlackQueen = new List<Piece>
                {
                    getPieceTypeInstance("Queen", -1),
                };
            }

            foreach (Piece wp in botWhite.pieces)
            {
                bgs.whitePieces.Add(wp.name.Replace(" ", string.Empty) + " (" + wp.points + ")");
            }

            HelperFunctions.initPiece(botWhitePawns[0], new coords(1, 2));
            HelperFunctions.initPiece(botWhitePawns[1], new coords(2, 2));
            HelperFunctions.initPiece(botWhitePawns[2], new coords(3, 2));
            HelperFunctions.initPiece(botWhitePawns[3], new coords(4, 2));
            HelperFunctions.initPiece(botWhitePawns[4], new coords(5, 2));
            HelperFunctions.initPiece(botWhitePawns[5], new coords(6, 2));
            HelperFunctions.initPiece(botWhitePawns[6], new coords(7, 2));
            HelperFunctions.initPiece(botWhitePawns[7], new coords(8, 2));
            HelperFunctions.initPiece(botWhiteRooks[0], new coords(1, 1));
            HelperFunctions.initPiece(botWhiteRooks[1], new coords(8, 1));
            HelperFunctions.initPiece(botWhiteBishops[0], new coords(3, 1));
            HelperFunctions.initPiece(botWhiteBishops[1], new coords(6, 1));
            HelperFunctions.initPiece(botWhiteKnights[0], new coords(2, 1));
            HelperFunctions.initPiece(botWhiteKnights[1], new coords(7, 1));
            HelperFunctions.initPiece(botWhiteQueen[0], new coords(4, 1));
            HelperFunctions.initPiece(botWhiteKing[0], new coords(5, 1));

            foreach (Piece wp in botBlack.pieces)
            {
                bgs.blackPieces.Add(wp.name.Replace(" ", string.Empty) + " (" + wp.points + ")");
            }

            HelperFunctions.initPiece(botBlackPawns[0], new coords ( 1, 7 ));
            HelperFunctions.initPiece(botBlackPawns[1], new coords ( 2, 7 ));
            HelperFunctions.initPiece(botBlackPawns[2], new coords ( 3, 7 ));
            HelperFunctions.initPiece(botBlackPawns[3], new coords ( 4, 7 ));
            HelperFunctions.initPiece(botBlackPawns[4], new coords ( 5, 7 ));
            HelperFunctions.initPiece(botBlackPawns[5], new coords ( 6, 7 ));
            HelperFunctions.initPiece(botBlackPawns[6], new coords ( 7, 7 ));
            HelperFunctions.initPiece(botBlackPawns[7], new coords ( 8, 7 ));
            HelperFunctions.initPiece(botBlackRooks[0], new coords ( 1, 8 ));
            HelperFunctions.initPiece(botBlackRooks[1], new coords ( 8, 8 ));
            HelperFunctions.initPiece(botBlackBishops[0], new coords ( 3, 8 ));
            HelperFunctions.initPiece(botBlackBishops[1], new coords ( 6, 8 ));
            HelperFunctions.initPiece(botBlackKnights[0], new coords ( 2, 8 ));
            HelperFunctions.initPiece(botBlackKnights[1], new coords ( 7, 8 ));
            HelperFunctions.initPiece(botBlackQueen[0], new coords ( 4, 8 ));
            HelperFunctions.initPiece(botBlackKing[0], new coords ( 5, 8 ));

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

            if (nonResettables.logMatch)
            {
                logger.logGameStart(bgs);

                BoardState board = new BoardState();
                board.refresh(convertBoardGrid(gameData.boardGrid));
                logger.logBoardState(turn, "Initial Board", board);
            }

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

    RepetitionTracker repetitionTracker = new RepetitionTracker();


    void Update()
    {
        if (!isTurn || !started || gameOver) return;

        isTurn = false;

        StartCoroutine(BotTurn());
    }

    IEnumerator BotTurn()
    {
        resetBotPieces(botWhite);
        resetBotPieces(botBlack);
        botWhite.currentBoardState.refresh(convertBoardGrid(gameData.boardGrid));
        botBlack.currentBoardState.refresh(convertBoardGrid(gameData.boardGrid));

        botWhite.currentBoardState = copyBoardState(botWhite.currentBoardState);
        botBlack.currentBoardState = copyBoardState(botBlack.currentBoardState);

        BotTemplate currentBot;
        bool valid;

        Piece movePieceObj = null;
        coords moveCoords = new coords(-1, -1);
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

        List<coords> allMoves = HelperFunctions.addToCurrentMoveableCoordsTotal(currentBot.color, true, false, null, true, true);
        Debug.LogWarning("AllMoves: " + allMoves.Count);
        debug_printBoardGrid(gameData.boardGrid, true, false);

        if (currentBot.penalty)
        {
            Debug.Log("Bot " + currentBot.name + " has a penalty. Executing random move");
            helper.addBotMessage(" " + currentBot.name + " has a penalty. Executing random move");
            valid = false;
            currentBot.penalty = false;
        }
        else
        {
            if (currentBot.name == "Thinking Bot II")
            {
                thinkingBotII.Play();
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();
            NextMove nextMove = currentBot.nextMove();
            watch.Stop();
            watchMS = watch.ElapsedMilliseconds;

            if (currentBot.name == "Thinking Bot II")
            {
                thinkingBotII.Pause();
            }

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

            if (moveCoords.x != -1 && movePieceObj != null)
            {
                HelperFunctions.highlightSquare(HelperFunctions.findSquare(movePieceObj.position.x, movePieceObj.position.y), Color.green);
                HelperFunctions.highlightSquare(HelperFunctions.findSquare(moveCoords.x, moveCoords.y), Color.red);
            }
            yield return new WaitForSeconds(waitTime);

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

            Debug.Log("RECIEVED MOVE: " + movePieceObj.name + " to " + moveCoords.x + "," + moveCoords.y);

            if (nextMove.moveType == "move")
            {
                valid = botValidateMove(movePieceObj, moveCoords);
            } else
            {
                valid = botValidateAbility(pa, currentBot);
            }

            movePieceObj = getOriginalPieceFromClone(movePieceObj);

            if (movePieceObj.go == null)
            {
                GameObject newGO = new GameObject();
                RectTransform rect = newGO.AddComponent<RectTransform>();
                Image s = newGO.AddComponent<Image>();
                string imgPath = movePieceObj.color == 1 ? movePieceObj.wImage : movePieceObj.bImage;
                Sprite sp = Resources.Load<Sprite>(imgPath);
                s.sprite = sp;
                s.preserveAspect = true;
                newGO.name = movePieceObj.name;
                movePieceObj.go = newGO;
                GameObject square = HelperFunctions.findSquare(movePieceObj.position.x, movePieceObj.position.y);
                HelperFunctions.movePiece(movePieceObj, square);

                Debug.Log("Reassembling Broken GO");

                gameData.allPiecesDict.Add(movePieceObj.go, movePieceObj);
                //Debug.Break();
            }
        }

        // Safe
        gameData.selectedToMovePiece = movePieceObj;

        bool death = false;
        bool countDeath = false;
        int check = 0;
        if (valid)
        {
            gameData.selected = HelperFunctions.findSquare(moveCoords.x, moveCoords.y);
            gameData.selectedToMove = HelperFunctions.findSquare(movePieceObj.position.x, movePieceObj.position.y);
            gameData.selectedPiece = HelperFunctions.getPieceOnSquare(gameData.selected);
            gameData.selectedToMovePiece = movePieceObj;

            Debug.Log("Bot " + currentBot.name + " moved " + movePieceObj.name + " to " + HelperFunctions.findSquare(moveCoords.x, moveCoords.y).name + " in " + watchMS + "ms.");
            helper.addBotMessage(" " + currentBot.name + " moved " + movePieceObj.name + " to " + HelperFunctions.findSquare(moveCoords.x, moveCoords.y).name + " in " + watchMS + "ms.");

            repetitionTracker.addMove(selectedMove);

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
                //helper.moveSound.Play();
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
                Debug.Log("Attempted Move: " + movePieceObj.name + " to " + moveCoords.x + "," + moveCoords.y);
                helper.addBotMessage(" Attempted Move: " + movePieceObj.name + " to " + moveCoords.x + "," + moveCoords.y);
            }
            else
            {
                Debug.Log("No Move Provided or Penalty");
                //helper.addBotMessage(" No Move Provided or Penalty");
            }

            NextMove randomMove = performRandomBotMove_(currentBot);

            if (randomMove == null)
            {
                Debug.Log("Game Over - Stalemate (Condition 1)");
                bgs.result = "Draw by Stalemate";
                //Debug.Break();
            }
            else
            {
                repetitionTracker.addMove(randomMove);

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

                movePieceObj = getOriginalPieceFromClone(movePieceObj);

                gameData.selected = HelperFunctions.findSquare(moveCoords.x, moveCoords.y);
                gameData.selectedToMove = HelperFunctions.findSquare(movePieceObj.position.x, movePieceObj.position.y);
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
                    //helper.moveSound.Play();
                    var deathVars = helper.executeAbility(randomMove.ability);
                    death = deathVars.death;
                    check = deathVars.check;
                }
            }
        }

        if (nonResettables.logMatch)
        {
            BoardState board = new BoardState();
            board.refresh(convertBoardGrid(gameData.boardGrid));
            logger.logBoardState(turn, currentBot.name, board);
        }

        turn *= -1;
        //currentBot.currentBoardState.refresh(gameData.boardGrid);

        if (!HelperFunctions.isPieceBaseTypeOnBoard("King", 1))
        {
            if (HelperFunctions.isPieceBaseTypeOnBoard("King", -1))
            {
                Debug.Log("Game Over - King Death");
                bgs.result = "Won by Opposing King Death";
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
                bgs.result = "Won by Opposing King Death";
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

        if (check == 2 && !gameOver)
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
                //Double check if its really stalemate
                Piece king = null;
                List<Piece> pieces = HelperFunctions.getPiecesOnBoard();
                foreach (Piece p in pieces)
                {
                    if (p.color == turn && p.baseType == "King")
                    {
                        king = p;
                    }
                }

                if (king != null)
                {
                    bool check_ = HelperFunctions.isCheck(king);

                    if (check_)
                    {
                        Debug.Log("Game Over - Checkmate. Originally marked as Stalemate");
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
                        Debug.Log("Game Over - Stalemate (Condition 2)");
                        bgs.result = "Draw by Stalemate";
                        //Debug.Break();
                    }
                }
                
                else if (!kingDead)
                {
                    //Debug.Break();
                    Debug.Log("Game Over - Stalemate (Condition 2.1)");
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

        if (repetitionTracker.isRepetition())
        {
            Debug.Log("Game Over - Tie");
            gameOver = true;

            bgs.result = "Draw by Repetition";
        }

        //BotHelperFunctions.debug_printBoardGrid(gameData.boardGrid);
        //HelperFunctions.resetBoardColours();
        isTurn = true;

        if (gameOver)
        {
            printBGS(bgs);

            if (nonResettables.logMatch)
            {
                logger.publishLog(SEASON_NAME + "_" + botWhite.name + " vs " + botBlack.name + ".txt");
            }

            if (nonResettables.isBotTournament) nonResettables.postBotMatch(bgs.white, bgs.black, bgs.winnerName);

            yield return new WaitForSeconds(5f);

            HelperFunctions.resetGameVars();
            SceneManager.LoadScene(7);
        }

        //Check if check/update bot boardstate
        Debug.LogWarning("Moves Without Capture: " + movesWithoutCapture);
        HelperFunctions.resetBoardColours();
    }

    public bool botValidateMove(Piece piece, coords coords) {
        if (piece == null || coords.x == -1)
        {
            return false;
        }

        List<coords> moves = HelperFunctions.addMovesToCurrentMoveableCoords(piece);

        if (HelperFunctions.isInList(moves, coords, false)) {
            return true;
        }

        return false;
    }

    public static (Piece piece, coords coords) performRandomBotMove(BotTemplate bot)
    {
        var botMoves = getAllPossibleMovesPenalty(bot, bot.color);

        List<Dictionary<Piece, List<coords>>> allMoves = botMoves.pieceMoveList;

        if (allMoves.Count == 0)
        {
            return (null, new coords(-1, -1));
        }

        System.Random rand = new System.Random();

        int dictIndex = rand.Next(allMoves.Count);

        Dictionary<Piece, List<coords>> pieceMovesDict = allMoves[dictIndex];
        //fix for checkamte
        KeyValuePair<Piece, List<coords>> pieceMovesKeyVal = pieceMovesDict.First();

        Piece randMovePiece = pieceMovesKeyVal.Key;
        List<coords> randMoveCoordsList = pieceMovesKeyVal.Value;

        int coordIndex = rand.Next(randMoveCoordsList.Count);
        coords randMoveCoords = randMoveCoordsList[coordIndex];

        return (randMovePiece, randMoveCoords);
    }

    public static NextMove performRandomBotMove_(BotTemplate bot)
    {
        var botMoves = getAllPossibleMovesPenalty(bot, bot.color);

        List<Dictionary<Piece, List<coords>>> allMoves = botMoves.pieceMoveList;
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
            Dictionary<Piece, List<coords>> pieceMovesDict = allMoves[dictIndex];
            KeyValuePair<Piece, List<coords>> pieceMovesKeyVal = pieceMovesDict.First();

            Piece randMovePiece = pieceMovesKeyVal.Key;
            List<coords> randMoveCoordsList = pieceMovesKeyVal.Value;

            int coordIndex = rand.Next(randMoveCoordsList.Count);
            coords randMoveCoords = randMoveCoordsList[coordIndex];

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

            if (pa_.coords.x != -1 && pa.coords.x != -1)
            {
                if (pa_.coords.x != pa.coords.x)
                {
                    continue;
                }

                if (pa_.coords.y != pa.coords.y)
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
                foreach (coords c_ in pa_.placeCoords)
                {

                    bool matchedCoords = false;

                    foreach (coords c in pa.placeCoords)
                    {
                        if (c.x == c_.x && c.y == c_.y)
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

    public static (List<Dictionary<Piece, List<coords>>> pieceMoveList, List<PieceAbility> pieceAbilities) getAllPossibleMovesPenalty(BotTemplate bot, int color)
    {
        List<Dictionary<Piece, List<coords>>> totalMoves = new List<Dictionary<Piece, List<coords>>>();

        foreach (Piece piece in bot.pieces)
        {
            List<coords> moves = HelperFunctions.addMovesToCurrentMoveableCoords(piece);

            if (moves.Count > 0)
            {
                Dictionary<Piece, List<coords>> pMoveDict = new Dictionary<Piece, List<coords>>();

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
        string filePath = Path.Combine(Application.persistentDataPath, SEASON_NAME + "MatchHistory.txt");
        File.AppendAllText(filePath, logText + "\n------------------------\n");

        Debug.Log("Saved match log to: " + filePath);

        if (nonResettables.logMatch)
        {
            logger.logGameEnd(bgs);
        }

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

    public static bool areMovesEqual(NextMove a, NextMove b)
    {
        if (a.moveType != b.moveType)
            return false;

        if (a.moveType == "move")
        {
            return a.move.p.name == b.move.p.name &&
                   a.move.coords.x == b.move.coords.x &&
                   a.move.coords.y == b.move.coords.y;
        }
        else
        {
            return a.ability.piece.name == b.ability.piece.name;
        }
    }

    public class RepetitionTracker
    {
        private Queue<NextMove> moveQueue = new Queue<NextMove>();
        private const int MAX_MOVES = 12;

        public void addMove(NextMove move)
        {
            moveQueue.Enqueue(move);

            if (moveQueue.Count > MAX_MOVES)
                moveQueue.Dequeue();
        }

        public bool isRepetition()
        {
            if (moveQueue.Count < MAX_MOVES)
            {
                return false;
            }

            NextMove[] moves = moveQueue.ToArray();

            for (int i = 0; i < 8; i++)
            {
                if (!areMovesEqual(moves[i], moves[i + 4]))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class GameLogger
    {
        private StringBuilder sb = new StringBuilder();

        public void logGameStart(BotGameStatus bgs)
        {
            sb.AppendLine("Game Start");
            sb.AppendLine(bgs.white + " (White) vs (Black) " + bgs.black);

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

            sb.AppendLine("---- Moves ----");
        }

        public void logBoardState(int turn, string bot, BoardState bs)
        {
            sb.AppendLine("Turn " + turn + " - " + bot);
            sb.AppendLine(debug_printBoardGrid(revertBoardGrid(bs.boardGrid), true, true).ToString());
        }

        public void logGameEnd(BotGameStatus bgs)
        {
            sb.AppendLine("---- Game End ----");
            sb.AppendLine(bgs.white + " (White) vs (Black) " + bgs.black);
            sb.AppendLine(bgs.winner + " " + bgs.result);
            sb.AppendLine("The match took " + bgs.numTurns + " turns");

            sb.AppendLine("Penalties: " + bgs.white + ": " + bgs.whitePenalties + ", " + bgs.black + ": " + bgs.blackPenalties);
        }

        public string getLog()
        {
            return sb.ToString();
        }

        public void publishLog(string fileName = null)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string folderPath = Path.Combine(documentsPath, "WC_Tournaments/" + SEASON_NAME);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (string.IsNullOrEmpty(fileName))
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                fileName = "match_" + timestamp + ".txt";
            }

            string fullPath = Path.Combine(folderPath, fileName);

            File.WriteAllText(fullPath, sb.ToString());
            //System.Diagnostics.Process.Start(fullPath);
        }
    }
}

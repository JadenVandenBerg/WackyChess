using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.Text.RegularExpressions;

public class Botany : BotTemplate
{
    int turn = 0;
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public Botany(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Botany";

        //This function populates the pieces variable
        choosePieces();
    }

    override

    //Still moving straight into captures even tough its hardcoded not to.
    public NextMove nextMove()
    {
        turn += 1;
        //Initialize for later
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        List<NextMove> tierOneMove = new List<NextMove>();
        List<float> pieceScoresT1 = new List<float>();
        List<NextMove> tierZeroMove = new List<NextMove>();
        List<float> pieceScoresT0 = new List<float>();
        List<NextMove> tierTwoMove = new List<NextMove>();
        List<NextMove> tierThreeMove = new List<NextMove>();


        //Add some speedrunner code



        //Capture ungaurded pieces
        if (validMoves.Count == 0)
        {
            //Opponent peices can't move there because there is a piece there!
            foreach (NextMove nextMove in allMoves)
            {
                Piece piece;
                coords coords;
                string moveType = nextMove.moveType;
                bool cull = false;

                if (moveType == "move")
                {
                    Move mv = nextMove.move;

                    piece = mv.p;
                    coords = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = nextMove.ability;

                    piece = pa.piece;
                    coords = pa.coords;
                }

                BoardState originalBoardState = this.currentBoardState;

                //Simulate the piece move
                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
                }

                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);
                List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(cloneState, this.color * -1);

                foreach (NextMove oppMove in allMovesOpp)
                {
                    Piece badPiece;
                    coords badCoords;
                    string moveTypeOpp = oppMove.moveType;

                    if (moveTypeOpp == "move")
                    {
                        Move mv = oppMove.move;

                        badPiece = mv.p;
                        badCoords = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = oppMove.ability;

                        badPiece = pa.piece;
                        badCoords = pa.coords;
                    }

                    if (coords.x == badCoords.x && coords.y == badCoords.y)
                    {
                        cull = true;
                        gameData.helper.addBotMessage("Tier Zero Move Culled because of protection");
                    }
                }

                if (cull == false)
                {
                    foreach (Piece oppPiece in allPiecesOpp)
                    {
                        if (coords.x == oppPiece.position.x && coords.y == oppPiece.position.y)
                        {
                            tierZeroMove.Add(nextMove);
                            pieceScoresT0.Add(oppPiece.points);

                        }
                    }
                }
            }
            float highest = -1000;
            for (int i = tierZeroMove.Count - 1; i >= 0; i--)
            {
                NextMove bestMove = tierZeroMove[i];
                float moveScore = pieceScoresT0[i];
                if (moveScore > highest)
                {
                    highest = moveScore;
                    validMoves.Clear();
                    validMoves.Add(bestMove);
                    gameData.helper.addBotMessage("Tier Zero Move Executed");
                }
            }
        }

        //Move to attack ungaurded pieces
        if (validMoves.Count == 0)
        {
            foreach (NextMove nextMove in allMoves)
            {
                Piece piece;
                coords coords;
                string moveType = nextMove.moveType;
                bool cull = false;

                if (moveType == "move")
                {
                    Move mv = nextMove.move;

                    piece = mv.p;
                    coords = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = nextMove.ability;

                    piece = pa.piece;
                    coords = pa.coords;
                }

                BoardState originalBoardState = this.currentBoardState;

                //Simulate the piece move
                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
                }
                this.currentBoardState = cloneState;

                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);
                List<NextMove> allMovesMe = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

                foreach (NextMove nextmove in allMovesMe)
                {
                    List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, this.color * -1);
                    foreach (Piece pieceOpp in allPiecesOpp)
                    {
                        //If I will capture next
                        if (pieceOpp.position.x == coords.x && pieceOpp.position.y == coords.y)
                        {

                            foreach (NextMove nextMoveOpp in allMovesOpp)
                            {
                                Piece pieceMoveOpp;
                                coords coordsOpp;
                                string moveTypeOpp = nextMoveOpp.moveType;

                                if (moveTypeOpp == "move")
                                {
                                    Move mv = nextMoveOpp.move;

                                    pieceMoveOpp = mv.p;
                                    coordsOpp = mv.coords;
                                }
                                else // moveType == "ability" guarenteed
                                {
                                    PieceAbility pa = nextMoveOpp.ability;

                                    pieceMoveOpp = pa.piece;
                                    coordsOpp = pa.coords;
                                }

                                //Check if the opposing piece is going to capture me (without simulating)
                                if (coordsOpp.x == coords.x && coordsOpp.y == coords.y)
                                {
                                    cull = true;
                                }
                                if (coordsOpp.x == pieceOpp.position.x && coordsOpp.y == pieceOpp.position.y)
                                {
                                    cull = true;
                                }
                                if (cull == false)
                                {
                                    tierOneMove.Add(nextMove);
                                    pieceScoresT1.Add(pieceOpp.points);
                                }
                            }
                        }
                    }
                }

                //Find the highest scoring move out of all those other moves
                this.currentBoardState = originalBoardState;
                float highest = -1000;
                for (int i = tierOneMove.Count - 1; i >= 0; i--)
                {
                    NextMove bestMove = tierOneMove[i];
                    float moveScore = pieceScoresT1[i];
                    if (moveScore > highest)
                    {
                        highest = moveScore;
                        validMoves.Clear();
                        validMoves.Add(bestMove);
                        gameData.helper.addBotMessage("Tier One Move Executed");
                    }
                }
            }
        }

        //No moves that exploit unguarded pieces
        //Develop knights or push pawns based on turn
        if (validMoves.Count == 0)
        {
            List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, true);
            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

            foreach (NextMove nextMove in allMoves)
            {
                Piece piece;
                coords coords;
                string moveType = nextMove.moveType;

                if (moveType == "move")
                {
                    Move mv = nextMove.move;

                    piece = mv.p;
                    coords = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = nextMove.ability;

                    piece = pa.piece;
                    coords = pa.coords;
                }

                if (piece.baseType == "Knight" && turn < 3)
                {
                    validMoves.Add(nextMove);
                    gameData.helper.addBotMessage("Tier Three Move Executed");
                }
                else if (piece.baseType == "Pawn")
                {
                    if (turn > 2 && turn < 7)
                    {
                        validMoves.Add(nextMove);
                        gameData.helper.addBotMessage("Tier Three Move Executed");
                    }
                    else if (botPoints < 20 && oppPoints < 13.5)
                    {
                        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);
                        bool cullMove3 = false;

                        foreach (NextMove defenseMove in allMovesOpp)
                        {
                            Piece badPiece;
                            coords badCoords;
                            string defenseMoveType = defenseMove.moveType;

                            if (defenseMoveType == "move")
                            {
                                Move mv = defenseMove.move;

                                badPiece = mv.p;
                                badCoords = mv.coords;
                            }
                            else // moveType == "ability" guarenteed
                            {
                                PieceAbility pa = defenseMove.ability;

                                badPiece = pa.piece;
                                badCoords = pa.coords;
                            }

                            if (badCoords.x == coords.x && badCoords.x == coords.x)
                            {
                                cullMove3 = true;
                            }
                        }
                        if (cullMove3 == false)
                        {
                            tierThreeMove.Add(nextMove);
                        }
                    }
                }
            }

            int closestDistance = 100;
            foreach (NextMove pawnPush in tierThreeMove)
            {
                coords coords;
                string moveType = pawnPush.moveType;

                if (moveType == "move")
                {
                    Move mv = pawnPush.move;

                    coords = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = pawnPush.ability;

                    coords = pa.coords;
                }

                //Check for the furthest to the other side
                if (this.color == -1) //Black wants 1
                {
                    int myDistance = coords.y;
                    if (myDistance <= closestDistance)
                    {
                        closestDistance = myDistance;
                        validMoves.Clear();
                        validMoves.Add(pawnPush);
                        gameData.helper.addBotMessage("Tier Three Move Executed");
                    }
                }
                else //White wants 8
                {
                    int myDistance = 8 - coords.y;
                    if (myDistance <= closestDistance)
                    {
                        closestDistance = myDistance;
                        validMoves.Clear();
                        validMoves.Add(pawnPush);
                        gameData.helper.addBotMessage("Tier Three Move Executed");
                    }
                }

            }

        }

        //No good moves
        //Develop
        if (validMoves.Count == 0)
        {
            foreach (NextMove spaceMove in allMoves)
            {
                Piece piece;
                coords coords;
                string moveType = spaceMove.moveType;
                bool cullMove2 = false;

                if (moveType == "move")
                {
                    Move mv = spaceMove.move;

                    piece = mv.p;
                    coords = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = spaceMove.ability;

                    piece = pa.piece;
                    coords = pa.coords;
                }

                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

                foreach (NextMove retakeMove in allMovesOpp)
                {
                    Piece badPiece;
                    coords badCoords;
                    string retakeMoveType = retakeMove.moveType;

                    if (retakeMoveType == "move")
                    {
                        Move mv = retakeMove.move;

                        badPiece = mv.p;
                        badCoords = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = retakeMove.ability;

                        badPiece = pa.piece;
                        badCoords = pa.coords;
                    }

                    if (badCoords.x == coords.x && badCoords.x == coords.x)
                    {
                        cullMove2 = true;
                    }
                }
                if (cullMove2 == false)
                {
                    tierTwoMove.Add(spaceMove);
                }
            }

            int mostMoves = -1;
            foreach (NextMove spaceTaker in tierTwoMove)
            {
                //Simulate here
                Piece piece;
                coords coords;
                string moveType = spaceTaker.moveType;

                if (moveType == "move")
                {
                    Move mv = spaceTaker.move;

                    piece = mv.p;
                    coords = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = spaceTaker.ability;

                    piece = pa.piece;
                    coords = pa.coords;
                }

                //Simulate the piece move
                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, this.currentBoardState, spaceTaker.ability);
                }

                List<NextMove> allMovesCloneState = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color);

                if (allMovesCloneState.Count > mostMoves)
                {
                    mostMoves = allMovesCloneState.Count;
                    validMoves.Clear();
                    validMoves.Add(spaceTaker);
                    gameData.helper.addBotMessage("Tier Two Move Executed");
                }
            }
        }

        //Idk how this even happend bro just do a random move
        if (validMoves.Count == 0)
        {
            System.Random rand0 = new System.Random();
            int rndIdx0 = rand0.Next(allMoves.Count);
            validMoves.Add(allMoves[rndIdx0]);
            gameData.helper.addBotMessage("Random Move Executed");
        }

        //Pick a random move from our list of tied moves
        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

        //Get the original piece, you can just copy paste this part (ill probably add this to botMaster.cs later)
        if (move.moveType == "move")
        {
            move.move.p = getOriginalPieceFromClone(move.move.p);
        }
        else
        {
            move.ability.piece = getOriginalPieceFromClone(move.ability.piece);
        }
        return move;
    }
}

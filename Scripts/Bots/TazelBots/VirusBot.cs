using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System;


public class VirusBot : BotTemplate
{
    int turn = 0;
    Piece berserkPiece;
    List<coords> virusT1 = new List<coords>();
    List<coords> virusT2 = new List<coords>();
    List<coords> virusT3 = new List<coords>();
    Color virusPurple = new Color(1.0f, 0f, 1.0f, 1.0f);
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public VirusBot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Virus Bot";

        //This function populates the pieces variable
        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        turn += 1;
        //Initialize for later
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();
        bool killMove = false;

        //Get all the possible moves. Note that some of these moves may result in check, which will not be allowed
        //Other than that these moves are all legal.
        //NextMove is a class with 3 vars inside, NextMove.moveType == "move" | "ability"
        //if move, NextMove.move will be populated
        //Move contains Move.p, Move.coords
        //if ability, NextMove.ability will be populated
        //public Piece piece; //The piece with the ability
        //public string ability; //Ability name
        //public int[] coords; //Coords for abilities with one action (ie. Spawning, Freezing)
        //public List<Piece> placePieces; //Pieces for abilities with multiple actions. Only hungry for now
        //public List<int[]> placeCoords; //Coords for abilities with multiple actions. Only hungry for now
        //public Piece secondPiece; //The second piece used in abilities. Used for castling/spawning


        //


        //Start the infection
        if (turn == 1)
        {
            List<Piece> allMaPieces = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, color);
            foreach (Piece piece in allMaPieces)
            {
                if (piece.baseType != "King")
                {
                    virusT1.Add(piece.position);
                }
                else
                {
                    virusT2.Add(piece.position);
                }
            }

        }

        //Spread the infection
        for (int i = virusT1.Count - 1; i >= 0; i--)
        {
            coords virus = virusT1[i];

            System.Random infectNum = new System.Random();
            int value = infectNum.Next(100);
            if (value <= 10)
            {
                //Grow
                virusT2.Add(virus);
                virusT1.Remove(virus);
            }
            //A little bit of overlap
            if (value >= 8 && value <= 18)
            {
                //Replicate
                virusT1.Add(new coords(virus.x, virus.y));

            }
            //Move
            if (value >= 16 && value <= 46)
            {
                virusT1.Add(new coords(virus.x + infectNum.Next(-1, 1), virus.y + infectNum.Next(-1, 1)));
                virusT1.Remove(virus);
            }
            //Die
            if (value >= 89)
            {
                virusT1.Remove(virus);
            }
        }


        for (int i = virusT2.Count - 1; i >= 0; i--)
        {
            coords virus = virusT2[i];
            System.Random infectNum = new System.Random();
            int value = infectNum.Next(100);
            if (value <= 2)
            {
                //Grow
                virusT3.Add(virus);
                virusT2.Remove(virus);
            }
            if (value >= 4 && value <= 25)
            {
                //Replicate
                virusT1.Add(new coords(virus.x, virus.y));
            }
            //Die
            if (value >= 97)
            {
                virusT1.Remove(virus);
            }

        }

        for (int i = virusT3.Count - 1; i >= 0; i--)
        {
            coords virus = virusT3[i];
            System.Random infectNum = new System.Random();
            int value = infectNum.Next(100);
            //A little bit of overlap
            if (value <= 25)
            {
                //Replicate
                virusT1.Add(new coords(virus.x, virus.y));
            }
            if (value >= 56 && value <= 80)
            {
                //Explode
                virusT1.Add(new coords(virus.x, virus.y));
                virusT1.Add(new coords(virus.x + 1, virus.y + 1));
                virusT1.Add(new coords(virus.x - 1, virus.y - 1));
                virusT1.Add(new coords(virus.x + 1, virus.y - 1));
                virusT1.Add(new coords(virus.x - 1, virus.y + 1));
                virusT3.Remove(virus);
            }
        }

        int totalInfectedSquares = virusT1.Count + virusT2.Count + virusT3.Count;
        if (totalInfectedSquares > 15000)
        {
            gameData.helper.addBotMessage("Virus dominated the board and starved");
            virusT1.Clear();
            virusT2.Clear();
        }
        else
        {
            gameData.helper.addBotMessage("Virus Bot infected squares: " + totalInfectedSquares.ToString());
        }

        //Show infected squares
        foreach (coords pos in virusT1)
        {
            System.Random rndValue = new System.Random();
            float rndColorModifier = rndValue.Next(40);
            Color virusGreen = new Color(0.0f, 1.0f - (rndColorModifier / 100), 0.0f, 1.0f);
            HelperFunctions.highlightSquare(HelperFunctions.findSquare(pos.x, pos.y), virusGreen);
        }
        foreach (coords pos in virusT2)
        {
            HelperFunctions.highlightSquare(HelperFunctions.findSquare(pos.x, pos.y), Color.black);
        }
        foreach (coords pos in virusT3)
        {
            HelperFunctions.highlightSquare(HelperFunctions.findSquare(pos.x, pos.y), virusPurple);
        }

        List<Piece> hitList = new List<Piece>();
        List<Piece> hitListStorage = hitList;
        List<Piece> allOppPieces = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, color * -1);

        foreach (coords infectTile in virusT1)
        {
            foreach (Piece oppPiece in allOppPieces)
            {
                if (oppPiece.position.x == infectTile.x && oppPiece.position.y == infectTile.y)
                {
                    hitList.Add(oppPiece);
                }
            }
        }

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        //loop through all moves

        if (hitList.Count > 0)
        {
            //Target pieces that are on infected squares
            foreach (NextMove nextMove in allMoves)
            {
                //Find out what the moveType is and set vars accordingly
                Piece piece;
                coords coords;
                string moveType = nextMove.moveType;
                killMove = false;

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

                //this.currentBoardState at the start of nextMove is a BoardState containing info of all the pieces. Save this. After we loop through all opponent moves, we set
                //this.currentBoardState = originalBoardstate
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

                List<Piece> allOppPiecesInLoop = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, color * -1);
                List<Piece> hitList2 = new List<Piece>();

                foreach (coords infectTile in virusT1)
                {
                    foreach (Piece oppPiece in allOppPiecesInLoop)
                    {
                        if (oppPiece.position.x == infectTile.x && oppPiece.position.y == infectTile.y)
                        {
                            hitList2.Add(oppPiece);
                        }
                    }
                }

                if (hitList2.Count < hitList.Count)
                {
                    killMove = true;
                }

                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

                NextMove bestOppNextMove;
                float bestOppMoveDiff = +1000;

                if (killMove == true)
                {
                    //Loop through all opponent moves
                    foreach (NextMove nextMoveOpp in allMovesOpp)
                    {
                        Piece pieceOpp;
                        coords coordsOpp;

                        string moveTypeOpp = nextMoveOpp.moveType;

                        if (moveTypeOpp == "move")
                        {
                            Move mv = nextMoveOpp.move;

                            pieceOpp = mv.p;
                            coordsOpp = mv.coords;
                        }
                        else // moveType == "ability" guarenteed
                        {
                            PieceAbility pa = nextMoveOpp.ability;

                            pieceOpp = pa.piece;
                            coordsOpp = pa.coords;
                        }

                        //Save the boardstate again, then simulate opponent move
                        //After move is simulated, revert back to the boardstate after our move
                        BoardState originalBoardState_ = this.currentBoardState;
                        BoardState cloneState_;
                        if (moveTypeOpp == "move")
                        {
                            cloneState_ = simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);
                        }
                        else
                        {
                            cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp.ability);
                        }
                        this.currentBoardState = originalBoardState_;

                        //Using the boardstate after opponents boardstate, get the points on board
                        // this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1] means if bot is white, use [0] else [1]
                        List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        //Debug.Log("Testing a move by " + piece.name + " and opp " + pieceOpp.name + " results in " + (botPoints - 100) + " : " + (oppPoints - 100));
                        //if (this.color == 1) Debug.LogWarning("Points on board after " + moveType + "," + moveTypeOpp + " " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                        //debug_printBoardState(cloneState_);

                        float diff = botPoints - oppPoints;
                        if (diff < bestOppMoveDiff)
                        {
                            bestOppMoveDiff = diff;
                            bestOppNextMove = nextMoveOpp;
                        }
                    }
                }

                //Now back to the outer loop, if the move we checked, assuming the opponent makes the best move, is better than the current best, save it
                //If it is tied also save it
                if (killMove == true)
                {
                    if (bestOppMoveDiff >= bestMoveDiff)
                    {
                        if (bestOppMoveDiff > bestMoveDiff)
                        {
                            //If it is better, clear all saved moves
                            validMoves.Clear();
                        }

                        bestMoveDiff = bestOppMoveDiff;

                        //If it is a tie or better, save the move
                        validMoves.Add(nextMove);
                    }
                }


                //Reset the currentBoardState and go to the next move
                this.currentBoardState = originalBoardState;
            }

            //Could not capture a piece on an infected square
            if (validMoves.Count == 0)
            {
                double closestDistance = 10000;
                double secondClosestDistance = 10000000;
                NextMove savedMove = null;
                NextMove secondSavedMove = null;
                foreach (NextMove nextmove in allMoves)
                {
                    Piece piece;
                    coords coords;

                    string moveType = nextmove.moveType;

                    if (moveType == "move")
                    {
                        Move mv = nextmove.move;

                        piece = mv.p;
                        coords = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = nextmove.ability;

                        piece = pa.piece;
                        coords = pa.coords;
                    }

                    if (piece.baseType != "Knight" || piece.baseType != "Bishop")
                    {
                        Piece pieceToKill = null;
                        float highestScore = -1000;
                        foreach (Piece possiblePTK in hitList)
                        {
                            if (possiblePTK.points > highestScore)
                            {
                                highestScore = possiblePTK.points;
                                pieceToKill = possiblePTK;
                            }
                        }

                        coords pTKCoords = pieceToKill.position;
                        double pieceDistance = Math.Sqrt(Math.Pow(coords.y - pTKCoords.y, 2) + Math.Pow(coords.x - pTKCoords.x, 2));
                        if (pieceDistance < closestDistance)
                        {
                            secondClosestDistance = closestDistance;
                            closestDistance = pieceDistance;
                            if (savedMove is not null)
                            {
                                secondSavedMove = savedMove;
                            }
                            savedMove = nextmove;
                        }
                        else if (pieceDistance < secondClosestDistance)
                        {
                            secondClosestDistance = pieceDistance;
                            secondSavedMove = nextmove;
                        }
                    }
                }

                BoardState cloneStateFinal;
                if (savedMove.moveType == "move")
                {
                    cloneStateFinal = simulatePieceMove(this, this.currentBoardState, savedMove.move.p, savedMove.move.coords);
                }
                else
                {
                    cloneStateFinal = simulatePieceAbility(this, this.currentBoardState, savedMove.ability);
                }
                this.currentBoardState = cloneStateFinal;

                List<NextMove> allMovesOppFinal = getAllPossibleBotMovesAndAbilities(this, cloneStateFinal, this.color * -1);

                foreach (NextMove nextMoveOpp in allMovesOppFinal)
                {
                    Piece pieceOpp;
                    coords coordsOpp;

                    string moveTypeOpp = nextMoveOpp.moveType;

                    if (moveTypeOpp == "move")
                    {
                        Move mv = nextMoveOpp.move;

                        pieceOpp = mv.p;
                        coordsOpp = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = nextMoveOpp.ability;

                        pieceOpp = pa.piece;
                        coordsOpp = pa.coords;
                    }

                    if (savedMove is not null && secondSavedMove is not null)
                    {
                        if (savedMove.moveType == "move")
                        {
                            if (coordsOpp.x != savedMove.move.coords.x || coordsOpp.y != savedMove.move.coords.y)
                            {
                                validMoves.Add(savedMove);
                            }
                            else
                            {
                                validMoves.Add(secondSavedMove);
                            }
                        }
                        else
                        {
                            if (coordsOpp.x != savedMove.ability.coords.x || coordsOpp.y != savedMove.ability.coords.y)
                            {
                                validMoves.Add(savedMove);
                            }
                            else
                            {
                                validMoves.Add(secondSavedMove);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            //Run normally
            foreach (NextMove nextMove in allMoves)
            {
                //Find out what the moveType is and set vars accordingly
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

                //this.currentBoardState at the start of nextMove is a BoardState containing info of all the pieces. Save this. After we loop through all opponent moves, we set
                //this.currentBoardState = originalBoardstate
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

                //Now that we have simulated our move, we do the same with opponent moves
                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

                NextMove bestOppNextMove;
                float bestOppMoveDiff = +1000;

                //Loop through all opponent moves
                foreach (NextMove nextMoveOpp in allMovesOpp)
                {
                    Piece pieceOpp;
                    coords coordsOpp;

                    string moveTypeOpp = nextMoveOpp.moveType;

                    if (moveTypeOpp == "move")
                    {
                        Move mv = nextMoveOpp.move;

                        pieceOpp = mv.p;
                        coordsOpp = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = nextMoveOpp.ability;

                        pieceOpp = pa.piece;
                        coordsOpp = pa.coords;
                    }

                    //Save the boardstate again, then simulate opponent move
                    //After move is simulated, revert back to the boardstate after our move
                    BoardState originalBoardState_ = this.currentBoardState;
                    BoardState cloneState_;
                    if (moveTypeOpp == "move")
                    {
                        cloneState_ = simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);
                    }
                    else
                    {
                        cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp.ability);
                    }
                    this.currentBoardState = originalBoardState_;

                    //Using the boardstate after opponents boardstate, get the points on board
                    // this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1] means if bot is white, use [0] else [1]
                    List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                    float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                    float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                    //Debug.Log("Testing a move by " + piece.name + " and opp " + pieceOpp.name + " results in " + (botPoints - 100) + " : " + (oppPoints - 100));
                    //if (this.color == 1) Debug.LogWarning("Points on board after " + moveType + "," + moveTypeOpp + " " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                    //debug_printBoardState(cloneState_);

                    //Compare the difference of points. If the diff is a new best (in the sense of black made a good move), mark it as best
                    //In this algorithm, this is considered to be the best move the opponent can make
                    float diff = botPoints - oppPoints;
                    if (diff < bestOppMoveDiff)
                    {
                        bestOppMoveDiff = diff;
                        bestOppNextMove = nextMoveOpp;
                    }
                }

                //Now back to the outer loop, if the move we checked, assuming the opponent makes the best move, is better than the current best, save it
                //If it is tied also save it
                if (bestOppMoveDiff >= bestMoveDiff)
                {
                    if (bestOppMoveDiff > bestMoveDiff)
                    {
                        //If it is better, clear all saved moves
                        validMoves.Clear();
                    }

                    bestMoveDiff = bestOppMoveDiff;

                    //If it is a tie or better, save the move
                    validMoves.Add(nextMove);
                }


                //Reset the currentBoardState and go to the next move
                this.currentBoardState = originalBoardState;
            }
        }


        //Pick a random move from our list of tied moves
        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

        if (move.moveType == "move")
        {
            foreach (Piece pieceOpp in allOppPieces)
            {
                if (move.move.coords.x == pieceOpp.position.x && move.move.coords.y == pieceOpp.position.y)
                {
                    virusT1.Add(move.move.coords);
                }
            }

            if (rand.Next(10) == 1)
            {
                virusT1.Add(move.move.coords);
            }
        }
        else
        {
            if (move.ability.ability == PieceAbilities.Spawn)
            {
                virusT1.Add(move.ability.coords);
            }
            else
            {
                foreach (Piece pieceOpp in allOppPieces)
                {
                    if (move.ability.coords.x == pieceOpp.position.x && move.ability.coords.y == pieceOpp.position.y)
                    {
                        virusT1.Add(move.ability.coords);
                    }
                }
            }

            if (rand.Next(10) == 1)
            {
                virusT1.Add(move.ability.coords);
            }

        }

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
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.Runtime.ExceptionServices;

public class NegativeOneMoveBot : BotTemplate
{
    int turn = 0;
    BoardState StoredBoardState;
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public NegativeOneMoveBot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Negative One Move Bot";

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
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        if (turn == 1)
        {
            System.Random randomNum = new System.Random();
            validMoves.Add(allMoves[randomNum.Next(allMoves.Count)]);
        }
        else
        {
            List<NextMove> allOldMoves = getAllPossibleBotMovesAndAbilities(this, StoredBoardState, this.color);
            foreach (NextMove nextMove in allOldMoves)
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

                //this.currentBoardState at the start of nextMove is a BoardState containing info of all the pieces. Save this. After we loop through all opponent moves, we set
                //this.currentBoardState = originalBoardstate
                BoardState originalBoardState = this.currentBoardState;

                //Simulate the piece move
                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, StoredBoardState, piece, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, StoredBoardState, nextMove.ability);
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
                //If the move is available
                if (allMoves.Contains(nextMove))
                {
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
                } 
                else
                {
                    if (bestOppMoveDiff >= bestMoveDiff)
                    {
                        //gameData.helper.addBotMessage("Negative one move bot attempted an invalid move. Attempting another move.");
                        //Spams the console. I don't like it
                    }
                }


                //Reset the currentBoardState and go to the next move
                this.currentBoardState = originalBoardState;
            }
        }

        //If we just removed everything do a random move
        if (validMoves.Count == 0)
        {
            System.Random randomNum = new System.Random();
            validMoves.Add(allMoves[randomNum.Next(allMoves.Count)]);
        }

        //Save the board
        StoredBoardState = this.currentBoardState;

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
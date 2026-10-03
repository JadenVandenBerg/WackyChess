using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System;

public class BotOnPot : BotTemplate
{
    int turn = 0;
    List<coords> mazeWalls = new List<coords>();
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public BotOnPot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Bot on pot";

        //This function populates the pieces variable
        choosePieces();
    }

    public static List<coords> GetLinePoints(coords p1, coords p2)
    {
        List<coords> positions = new List<coords>();

        int x1 = p1.x;
        int y1 = p1.y;
        int x2 = p2.x;
        int y2 = p2.y;

        int dx = Math.Abs(x2 - x1);
        int dy = Math.Abs(y2 - y1);

        int sx = (x1 < x2) ? 1 : -1;
        int sy = (y1 < y2) ? 1 : -1;

        int err = dx - dy;

        while (true)
        {
            positions.Add(new coords(x1, y1));

            if (x1 == x2 && y1 == y2) break;

            int e2 = 2 * err;

            if (e2 > -dy)
            {
                err -= dy;
                x1 += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                y1 += sy;
            }
        }

        return positions;
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
        //public int[] coords; //coords for abilities with one action (ie. Spawning, Freezing)
        //public List<Piece> placePieces; //Pieces for abilities with multiple actions. Only hungry for now
        //public List<int[]> placecoords; //coords for abilities with multiple actions. Only hungry for now
        //public Piece secondPiece; //The second piece used in abilities. Used for castling/spawning

        //Generate the maze
        if (turn == 1)
        {
            int loopsSinceWall = 0;
            for (int i = 1; i < 9; i++) //x
            {
                for (int j = 3; j < 7; j++) //y
                {
                    int chance = 0; int total = 5;
                    if (mazeWalls.Count > 0)
                    {
                        if (mazeWalls[mazeWalls.Count - 1].x == i && mazeWalls[mazeWalls.Count - 1].y == j - 1)
                        {
                            total += 2;
                        }
                    }
                    chance += loopsSinceWall;

                    System.Random rand4maze = new System.Random();
                    int rndNum = rand4maze.Next(total);
                    if (rndNum <= chance)
                    {
                        mazeWalls.Add(new coords(i, j));
                        loopsSinceWall = -1;
                    }
                    else
                    {
                        loopsSinceWall++;
                    }
                }
            }
        }

        //Show walls
        foreach (coords wallpos in mazeWalls)
        {
            System.Random rndValue = new System.Random();
            float rndColorModifier = rndValue.Next(40);
            Color wallColor = new Color(0.6f, 0.6f, 0.0f + (rndColorModifier / 100), 1.0f);
            HelperFunctions.highlightSquare(HelperFunctions.findSquare(wallpos.x, wallpos.y), wallColor);
        }

        //Loop through all moves
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
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

            bool cullMove = false;
            bool cullMove2 = false;
            foreach (coords mazeWall in mazeWalls)
            {
                if (HelperFunctions.checkState(piece, PieceState.Portal))
                {
                    cullMove = true;
                    break;
                }
                if (coords.x == mazeWall.x && mazeWall.y == coords.y)
                {
                    cullMove = true;
                }
                List<coords> line = GetLinePoints(piece.position, coords);
                foreach (coords pos in line)
                {
                    if (pos.x == mazeWall.x && mazeWall.y == pos.y)
                    {
                        cullMove = true;
                        break;
                    }
                }
                if (cullMove == true)
                {
                    break;
                }
            }

            NextMove bestOppNextMove;
            float bestOppMoveDiff = +1000;

            BoardState originalBoardState = this.currentBoardState;

            if (cullMove == false)
            {
                //this.currentBoardState at the start of nextMove is a BoardState containing info of all the pieces. Save this. After we loop through all opponent moves, we set
                //this.currentBoardState = originalBoardstate

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

                    List<NextMove> allMoves2 = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
                    BoardState originalBoardState2 = this.currentBoardState;

                    foreach (NextMove nextMove2 in allMoves2)
                    {
                        moveType = nextMove2.moveType;

                        if (moveType == "move")
                        {
                            Move mv = nextMove2.move;

                            piece = mv.p;
                            coords = mv.coords;
                        }
                        else // moveType == "ability" guarenteed
                        {
                            PieceAbility pa = nextMove2.ability;

                            piece = pa.piece;
                            coords = pa.coords;
                        }

                        cullMove2 = false;
                        foreach (coords mazeWall in mazeWalls)
                        {
                            if (HelperFunctions.checkState(piece, PieceState.Portal))
                            {
                                cullMove = true;
                                break;
                            }
                            if (coords.x == mazeWall.x && mazeWall.y == coords.y)
                            {
                                cullMove2 = true;
                            }
                            List<coords> line = GetLinePoints(piece.position, coords);
                            foreach (coords pos in line)
                            {
                                if (pos.x == mazeWall.x && mazeWall.y == pos.y)
                                {
                                    cullMove2 = true;
                                    break;
                                }
                            }
                            if (cullMove2 == true)
                            {
                                break;
                            }
                        }

                        if (cullMove2 == false)
                        {
                            BoardState cloneState2;
                            if (moveType == "move")
                            {
                                cloneState2 = simulatePieceMove(this, this.currentBoardState, piece, coords);
                            }
                            else
                            {
                                cloneState2 = simulatePieceAbility(this, this.currentBoardState, nextMove2.ability);
                            }
                            this.currentBoardState = cloneState2;

                            List<float> pointsOnBoard = getPointsOnBoardState(cloneState2, true);
                            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                            float diff = botPoints - oppPoints;
                            if (diff < bestOppMoveDiff)
                            {
                                bestOppMoveDiff = diff;
                                bestOppNextMove = nextMoveOpp;
                            }

                            this.currentBoardState = originalBoardState2;
                        }
                    }

                    this.currentBoardState = originalBoardState_;
                }
            }

            //Now back to the outer loop, if the move we checked, assuming the opponent makes the best move, is better than the current best, save it
            //If it is tied also save it
            if (cullMove == false && cullMove2 == false)
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

        if (validMoves.Count == 0)
        {

            System.Random rand2 = new System.Random();
            int rndIdx2 = rand2.Next(allMoves.Count);
            validMoves.Add(allMoves[rndIdx2]);
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
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System;

public class BotWithAGlock : BotTemplate
{
    int turn = 0;
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public BotWithAGlock(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Bot with a Glock";

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
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        NextMove bestMove;
        if (validMoves.Count == 0)
        {
            foreach (NextMove nextMove in allMoves)
            {
                Piece piece;
                coords coords;
                string moveType = nextMove.moveType;
                int mostMoves = 0;

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

                if (allMovesOpp.Count > mostMoves)
                {
                    mostMoves = allMovesOpp.Count;
                    bestMove = nextMove;
                    validMoves.Clear();
                    validMoves.Add(bestMove);
                }
            }
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
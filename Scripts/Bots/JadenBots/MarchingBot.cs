using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;


public class MarchingBot : BotTemplate
{
    public MarchingBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Marching Bot";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        foreach (NextMove nextMove in allMoves)
        {
            var nextMoveVars = getNextMoveVars(nextMove);
            Piece piece = nextMoveVars.piece;
            coords ogPiecePosition = new coords(piece.position.x, piece.position.y);
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            UndoMove undo;

            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(this.currentBoardState, piece, new coords(coords.x, coords.y));
            }
            else
            {
                undo = undo_simulatePieceAbility(this.currentBoardState, nextMove.ability);
            }

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

            NextMove bestOppNextMove;
            float bestOppMoveDiff = +1000;

            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                var nextMoveOppVars = getNextMoveVars(nextMoveOpp);
                Piece pieceOpp = nextMoveOppVars.piece;
                coords coordsOpp = nextMoveOppVars.coords;
                string moveTypeOpp = nextMoveOppVars.moveType;

                UndoMove undo_ = null;
                if (moveTypeOpp == "move")
                {
                    undo_ = undo_simulatePieceMove(this.currentBoardState, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
                }
                else
                {
                    //cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp.ability);
                    undo_ = undo_simulatePieceAbility(this.currentBoardState, nextMoveOpp.ability);
                }

                List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff = botPoints - oppPoints;

                if (diff < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff;
                    bestOppNextMove = nextMoveOpp;
                }

                undoMove(undo_, this.currentBoardState);

            }

            float finalDiff = bestOppMoveDiff;
            finalDiff += evalForwardMove(piece, ogPiecePosition, coords);

            if (finalDiff >= bestMoveDiff)
            {
                //Debug.Log("New Best or Equal Move: " + piece + " to " + coords.x + "," + coords.y + " Score: " + finalDiff);

                if (finalDiff > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = finalDiff;
                validMoves.Add(nextMove);
            }

            //this.currentBoardState = originalBoardState;
            undoMove(undo, this.currentBoardState);

            /*
            Debug.Log("Undoing Move");
            Debug.Log("Last Move: " + piece.name + " to " + coords[0] + "," + coords[1]);
            debug_printBoardState(currentBoardState);
            */
        }


        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
        return move;
    }

    private static int evalForwardMove(Piece piece, coords p, coords c)
    {
        if (piece.color == 1)
        {
            if (c.y - p.y > 0)
            {
                return 20;
            }
            else if (c.y - p.y == 0)
            {
                return 5;
            }
            else
            {
                return -20;
            }
        }
        else
        {
            if (c.y - p.y < 0)
            {
                return 20;
            }
            else if (c.y - p.y == 0)
            {
                return 5;
            }
            else
            {
                return -20;
            }
        }
    }
}
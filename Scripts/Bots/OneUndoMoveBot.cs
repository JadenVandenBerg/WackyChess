using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;


public class OneUndoMoveBot : BotTemplate
{
    //1 is white, -1 is black
    public OneUndoMoveBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "One Undo Move Bot";

        //This function populates the pieces variable
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
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            //BoardState originalBoardState = this.currentBoardState;
            UndoMove undo;

            if (moveType == "move")
            {
                //cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                undo = undo_simulatePieceMove(this.currentBoardState, piece, new coords(coords.x, coords.y));
            }
            else
            {
                //cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
                undo = undo_simulatePieceAbility(this.currentBoardState, nextMove.ability);
            }

            /*
            Debug.Log("Simulating Piece " + moveType + ": " + piece.name + " to " + coords[0] + ", " + coords[1]);
            debug_printBoardState(currentBoardState);
            */

            //this.currentBoardState = cloneState;

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

            NextMove bestOppNextMove;
            float bestOppMoveDiff = +1000;

            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                var nextMoveOppVars = getNextMoveVars(nextMoveOpp);
                Piece pieceOpp = nextMoveOppVars.piece;
                coords coordsOpp = nextMoveOppVars.coords;
                string moveTypeOpp = nextMoveOppVars.moveType;

                //BoardState originalBoardState_ = this.currentBoardState;
                //BoardState cloneState_;
                UndoMove undo_ = null;
                if (moveTypeOpp == "move")
                {
                    //cloneState_ = simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);
                    undo_ = undo_simulatePieceMove(this.currentBoardState, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
                }
                else
                {
                    //cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp.ability);
                    undo_ = undo_simulatePieceAbility(this.currentBoardState, nextMoveOpp.ability);
                }

                //Debug.Log("Simulating Opponent Piece " + moveTypeOpp + ": " + pieceOpp.name + " to " + coordsOpp[0] + ", " + coordsOpp[1]);
                //debug_printBoardState(currentBoardState);

                //this.currentBoardState = originalBoardState_;

                //List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
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

                /*
                Debug.Log("Undoing Move Outer");
                Debug.Log("Last Move: " + pieceOpp.name + " to " + coordsOpp[0] + "," + coordsOpp[1]);
                debug_printBoardState(currentBoardState);
                */
            }

            if (bestOppMoveDiff >= bestMoveDiff)
            {
                if (bestOppMoveDiff > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = bestOppMoveDiff;
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
}
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;


public class BerserkerBot : BotTemplate
{
    //1 is white, -1 is black
    public BerserkerBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Berserker Bot";

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

            UndoMove undo;

            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(this.currentBoardState, piece, new coords(coords.x, coords.y));
            }
            else
            {
                undo = undo_simulatePieceAbility(this.currentBoardState, nextMove.ability);
            }

            List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, true);
            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

            float diff = botPoints - oppPoints;

            if (diff >= bestMoveDiff)
            {
                if (diff > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                validMoves.Add(nextMove);
                bestMoveDiff = diff;
            }

            undoMove(undo, this.currentBoardState);
        }


        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
        return move;
    }
}
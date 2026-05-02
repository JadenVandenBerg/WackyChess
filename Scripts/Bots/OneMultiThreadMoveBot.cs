using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static HelperFunctions;
using static UndoMoveBotHelperFunctions;
using System.Threading.Tasks;
using System.Collections.Concurrent;


public class OneMultiThreadMoveBot : BotTemplate
{
    //1 is white, -1 is black
    public OneMultiThreadMoveBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "One Multi Thread Move Bot";

        //This function populates the pieces variable
        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        float bestMoveDiff = -1000;

        ConcurrentBag<(NextMove move, float diff)> results = new ConcurrentBag<(NextMove, float)>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        Parallel.ForEach(allMoves, nextMove =>
        {
            BoardState thread_BoardState = copyBoardState(this.currentBoardState);
            NextMove thread_NextMove = thread_nextMove(nextMove, thread_BoardState);

            float localBestOppMoveDiff = +1000;

            var nextMoveVars = getNextMoveVars(thread_NextMove);
            Piece piece = nextMoveVars.piece;
            int[] coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            UndoMove undo;

            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(thread_BoardState, piece, new coords(coords[0], coords[1]));
            }
            else
            {
                undo = undo_simulatePieceAbility(thread_BoardState, nextMove.ability);
            }

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, thread_BoardState, this.color * -1);

            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                var nextMoveOppVars = getNextMoveVars(nextMoveOpp);
                Piece pieceOpp = nextMoveOppVars.piece;
                int[] coordsOpp = nextMoveOppVars.coords;
                string moveTypeOpp = nextMoveOppVars.moveType;

                UndoMove undo_ = null;

                if (moveTypeOpp == "move")
                {
                    undo_ = undo_simulatePieceMove(thread_BoardState, pieceOpp, new coords(coordsOpp[0], coordsOpp[1]));
                }
                else
                {
                    undo_ = undo_simulatePieceAbility(thread_BoardState, nextMoveOpp.ability);
                }

                List<float> pointsOnBoard = getPointsOnBoardState(thread_BoardState, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff = botPoints - oppPoints;

                if (diff < localBestOppMoveDiff)
                {
                    localBestOppMoveDiff = diff;
                }

                undoMove(undo_, thread_BoardState);
            }

            undoMove(undo, thread_BoardState);

            results.Add((nextMove, localBestOppMoveDiff));
        });

        List<NextMove> validMoves = new List<NextMove>();

        foreach (var result in results)
        {
            if (result.diff >= bestMoveDiff)
            {
                if (result.diff > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = result.diff;
                validMoves.Add(result.move);
            }
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        return validMoves[rndIdx];
    }
}
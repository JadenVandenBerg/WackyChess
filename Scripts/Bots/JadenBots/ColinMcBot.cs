using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static JadenBotHelperFunctions;

public class ColinMcBot : BotTemplate
{
    //1 is white, -1 is black
    public ColinMcBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Colin McBot";

        //This function populates the pieces variable
        choosePieces();
    }

    Queue<NextMove> lastFiveMoves = new Queue<NextMove>();

    override
    public NextMove nextMove()
    {
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        List<PieceCoords> hangingPieces = Jay_getHangingPieces(currentBoardState, this.color * -1);
        foreach(PieceCoords hp in hangingPieces)
        {
            //Debug.LogWarning(hp.p + " is hanging on " + hp.c.x + hp.c.y); 
        }

        foreach (NextMove nextMove in allMoves)
        {
            var nextMoveVars = getNextMoveVars(nextMove);
            Piece piece = nextMoveVars.piece;
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            //Debug.Log("ColinMcBot moving piece " + piece + " to coords " + coords.x + "," + coords.y + ".");

            //Look for hanging piece here
            float hangingPieceBonus = 0f;

            foreach(PieceCoords hangingPiece in hangingPieces)
            {
                if (coords.x == hangingPiece.c.x + 1 && coords.y == hangingPiece.c.y + 1)
                {
                    //Debug.Log("Hanging Piece Found!");
                    hangingPieceBonus += 10f;
                }
            }

            UndoMove undo;

            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(this.currentBoardState, piece, new coords(coords.x, coords.y));
            }
            else
            {
                undo = undo_simulatePieceAbility(this.currentBoardState, nextMove.ability);
            }

            List<float> pointsOnBoard = Jay_getPointsOnBoardState(currentBoardState, false, piece, coords, color);
            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

            List<PieceCoords> hangingPieces_postMove = Jay_getHangingPieces(currentBoardState, this.color);
            foreach (PieceCoords hangingPiece_postMove in hangingPieces_postMove)
            {
                //Debug.Log("Hanging Piece Found: " + hangingPiece_postMove.p);

                float points = hangingPiece_postMove.p.points * 3;
                if (points < 0)
                {
                    points = 0.1f;
                }

                hangingPieceBonus -= points;
            }

            float score = evaluateMove(currentBoardState, color, lastFiveMoves, nextMove) + hangingPieceBonus;

            //Debug.Log("Score after move is " + score);

            if (score >= bestMoveDiff)
            {
                if (score > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = score;
                validMoves.Add(nextMove);
            }

            undoMove(undo, this.currentBoardState);
        }


        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

        lastFiveMoves.Enqueue(move);

        if (lastFiveMoves.Count > 6)
        {
            lastFiveMoves.Dequeue();
        }

        return move;
    }

    private float evaluateMove(BoardState bs, int botColor, Queue<NextMove> lastFiveMoves, NextMove nextMove)
    {
        var move = getNextMoveVars(nextMove);

        List<float> points = Jay_getPointsOnBoardState(bs, false, move.piece, move.coords, botColor);
        var boardControlEVAL = Jay_getBoardControlOnBoardState(bs, this);
        List<int> pawnStructure = Jay_getPawnStructureDefense(bs, this);

        float botPoints = this.color == 1 ? points[0] : points[1];
        float oppPoints = this.color == -1 ? points[0] : points[1];
        int botBoardControl = this.color == 1 ? boardControlEVAL.boardControl[0] : boardControlEVAL.boardControl[1];
        int oppBoardControl = this.color == -1 ? boardControlEVAL.boardControl[0] : boardControlEVAL.boardControl[1];
        int botCenterControl = this.color == 1 ? boardControlEVAL.centerControl[0] : boardControlEVAL.centerControl[1];
        int oppCenterControl = this.color == -1 ? boardControlEVAL.centerControl[0] : boardControlEVAL.centerControl[1];
        int botKingAttacking = this.color == 1 ? boardControlEVAL.kingAttacking[0] : boardControlEVAL.kingAttacking[1];
        int oppKingAttacking = this.color == -1 ? boardControlEVAL.kingAttacking[0] : boardControlEVAL.kingAttacking[1];
        int botPawnStructure = this.color == 1 ? pawnStructure[0] : pawnStructure[1];
        int oppPawnStructure = this.color == -1 ? pawnStructure[0] : pawnStructure[1];

        int botKingDefending = this.color == -1 ? boardControlEVAL.kingAttacking[0] : boardControlEVAL.kingAttacking[1];
        int oppKingDefending = this.color == 1 ? boardControlEVAL.kingAttacking[0] : boardControlEVAL.kingAttacking[1];

        int botPawnPromotionPotential = Jay_getPawnPromotionPotential(this.color, bs);
        int oppPawnPromotionPotential = Jay_getPawnPromotionPotential(this.color * -1, bs);

        float pointsDiff = botPoints - oppPoints;
        float boardControlDiff = botBoardControl - oppBoardControl;
        float centerControlDiff = botCenterControl - oppCenterControl;

        float endPenalty = 0f;

        if (Jay_moveInQueue(lastFiveMoves, nextMove))
        {
            endPenalty += 2f;
        }

        if (Jay_moveInQueueTwice(lastFiveMoves, nextMove))
        {
            endPenalty += 10f;
        }

        return (float)((pointsDiff * 3) + (boardControlDiff * 0.1) + (centerControlDiff * 0.2) + (botKingAttacking * 0.4) - (botKingDefending * 0.2) + (botPawnStructure * 0.2) + (botPawnPromotionPotential * 0.1) - (oppPawnPromotionPotential * 0.1) - endPenalty);
    }
}
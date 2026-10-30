using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static JadenBotHelperFunctions;

public class Egbot : BotTemplate
{
    public Egbot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Egbot";
        choosePieces();
    }

    public float getMoveOrderingScore(NextMove nm, BoardState bs, int searchColor, List<Piece> opponentPieces)
    {
        var moveVars = getNextMoveVars(nm);
        coords c = moveVars.coords;

        foreach (Piece piece in opponentPieces)
        {
            if (piece.position.x == c.x && piece.position.y == c.y)
            {
                return piece.points;
            }
        }

        return 0;
    }

    public List<NextMove> orderMoves(List<NextMove> allMoves, BoardState bs, int searchColor)
    {
        List<Piece> opponentPieces = getPiecesOnBoardState(bs, searchColor * -1);

        Dictionary<NextMove, float> moveScores = new Dictionary<NextMove, float>();

        foreach (NextMove nm in allMoves)
        {
            moveScores[nm] = getMoveOrderingScore(nm, bs, searchColor, opponentPieces);
        }

        allMoves.Sort((a, b) =>
        {
            return moveScores[b].CompareTo(moveScores[a]);
        });

        return allMoves;
    }

    float PRUNE_CUTOFF = 0.5f;
    float PRUNE_SCALE = 0.5f;

    int nodes = 0;

    override
    public NextMove nextMove()
    {
        int DEPTH = 4;
        float max = Mathf.NegativeInfinity;

        NextMove bestMove = null;

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, color);
        allMoves = orderMoves(allMoves, this.currentBoardState, color);

        foreach (NextMove nm in allMoves)
        {
            var moveVars = getNextMoveVars(nm);
            Piece p = moveVars.piece;
            coords c = moveVars.coords;
            string moveType = moveVars.moveType;

            UndoMove undo;
            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(this.currentBoardState, p, c);
            }
            else
            {
                undo = undo_simulatePieceAbility(this.currentBoardState, nm.ability);
            }

            float score = alphaBetaMin(DEPTH - 1, Mathf.NegativeInfinity, Mathf.Infinity, this.currentBoardState, color * -1);

            if (score > max)
            {
                max = score;
                bestMove = nm;
            }

            undoMove(undo, this.currentBoardState);
        }

        Debug.Log("Egbot looked at " + nodes + " nodes.");

        nodes = 0;
        return bestMove == null ? getRandomBotMove(this) : bestMove;
    }

    public float alphaBetaMin(int depth, float alpha, float beta, BoardState bs, int color)
    {
        if (depth == 0)
        {
            nodes++;
            return evaluate(bs);
        }

        float min = Mathf.Infinity;

        List<NextMove> allMoves;

        if (depth == 1)
        {
            allMoves = getAllPossibleBotAttacksAndAbilities(this, bs, color);
        }
        else
        {
            allMoves = getAllPossibleBotMovesAndAbilities(this, bs, color);
        }

        if (allMoves.Count == 0)
        {
            return evaluate(bs);
        }

        allMoves = orderMoves(allMoves, bs, color);

        foreach (NextMove nm in allMoves)
        {
            var moveVars = getNextMoveVars(nm);
            Piece p = moveVars.piece;
            coords c = moveVars.coords;
            string moveType = moveVars.moveType;

            UndoMove undo;
            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(bs, p, c);
            }
            else
            {
                undo = undo_simulatePieceAbility(bs, nm.ability);
            }

            float score = alphaBetaMax(depth - 1, alpha, beta, bs, color * -1);

            if (score < min)
            {
                min = score;
            }

            if (score < beta)
            {
                beta = score;
            }

            undoMove(undo, bs);

            if (score <= alpha + PRUNE_CUTOFF + ((depth + 1) * PRUNE_SCALE) && alpha != Mathf.NegativeInfinity)
            {
                return min;
            }
        }

        return min;
    }

    public float alphaBetaMax(int depth, float alpha, float beta, BoardState bs, int color)
    {
        if (depth == 0)
        {
            nodes++;
            return evaluate(bs);
        }

        float max = Mathf.NegativeInfinity;

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, bs, color);

        if (allMoves.Count == 0)
        {
            return evaluate(bs);
        }

        allMoves = orderMoves(allMoves, bs, color);

        foreach (NextMove nm in allMoves)
        {
            var moveVars = getNextMoveVars(nm);
            Piece p = moveVars.piece;
            coords c = moveVars.coords;
            string moveType = moveVars.moveType;

            UndoMove undo;
            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(bs, p, c);
            }
            else
            {
                undo = undo_simulatePieceAbility(bs, nm.ability);
            }

            float score = alphaBetaMin(depth - 1, alpha, beta, bs, color * -1);

            if (score > max)
            {
                max = score;
            }

            if (score > alpha)
            {
                alpha = score;
            }

            undoMove(undo, bs);

            if (score >= beta - PRUNE_CUTOFF - ((depth + 1) * PRUNE_SCALE) && beta != Mathf.Infinity)
            {
                return max;
            }
        }

        return max;
    }

    public float evaluate(BoardState bs)
    {
        List<float> pob = Jay_getPointsOnBoardState_simple(bs, true, color);

        if (color == 1)
        {
            return pob[0] - pob[1];
        }
        else
        {
            return pob[1] - pob[0];
        }
    }
}
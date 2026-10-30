using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static JadenBotHelperFunctions;
using static HelperFunctions;

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

    float PRUNE_CUTOFF = 0f;
    float PRUNE_SCALE = 0.25f;
    Queue<NextMove> lastFiveMoves = new Queue<NextMove>();

    int nodes = 0;

    override
    public NextMove nextMove()
    {
        int DEPTH = 4;
        float max = Mathf.NegativeInfinity;
        float maxScore = Mathf.NegativeInfinity;

        NextMove bestMove = null;

        List<NextMove> allMoves = Jay_getAllLegalMovesAndAbilities(this, this.currentBoardState, color);
        if (allMoves.Count == 0)
        {
            return getRandomBotMove(this);
        }

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

            var evalMoves = Jay_getBoardControlOnBoardState(this.currentBoardState, this);

            float kingAttack = evalMoves.kingAttacking[colorToIdx(this.color)] / 20f;
            float boardControl = evalMoves.boardControl[colorToIdx(this.color)] / 50f;
            float development = Jay_getDevelopment(this.currentBoardState, this.color) / 10f;

            float penalties = 0f;
            if (Jay_moveInQueue(lastFiveMoves, nm))
            {
                if (Jay_moveInQueueTwice(lastFiveMoves, nm))
                {
                    penalties += 20f;
                }
            }

            if (p.baseType == "King")
            {
                if (checkState(p, PieceState.Fragile))
                {
                    penalties += 50f;
                }
            }

            float bonus = kingAttack + boardControl + development - penalties;

            float alpha = maxScore - bonus;
            float rawScore = alphaBetaMin(DEPTH - 1, alpha, Mathf.Infinity, this.currentBoardState, color * -1);
            float score = rawScore + bonus;

            //Debug.LogWarning("Egbot Move: " + p + " to " + c.x + c.y + " Total: " + score + " Score: " + rawScore + ", King Attack: " + kingAttack + ", Board Control: " + boardControl + ", Development: " + development);
            /*
            if (penalties > 0)
            {
                Debug.LogWarning("Egbot: Penalties: " + penalties);
            }
            */
            if (score > maxScore)
            {
                maxScore = score;
                bestMove = nm;
                //Debug.LogWarning("Egbot: New Best Move Found");
            }

            //validateBoardState(this.currentBoardState);

            undoMove(undo, this.currentBoardState);
        }

        //Debug.Log("Egbot looked at " + nodes + " nodes.");

        nodes = 0;
        if (bestMove != null)
        {
            lastFiveMoves.Enqueue(bestMove);

            if (lastFiveMoves.Count > 5)
            {
                lastFiveMoves.Dequeue();
            }
        }
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
        else if (depth == 3)
        {
            allMoves = Jay_getAllLegalMovesAndAbilities(this, bs, color);
        }
        else
        {
            allMoves = getAllPossibleBotMovesAndAbilities(this, bs, color);
        }

        if (allMoves.Count == 0)
        {
            nodes++;

            if (depth != 1) {
                if (Jay_isCheck(this, bs, color))
                {
                    //Debug.LogError("Egbot: This is checkmate");
                    //Debug.Break();

                    return 500;
                }
                else
                {
                    //Debug.LogError("Egbot: This is stalemate");
                    //Debug.Break();

                    return -100;
                }
            }

            return evaluate(bs);
        }

        allMoves = orderMoves(allMoves, bs, color);

        foreach (NextMove nm in allMoves)
        {
            var moveVars = getNextMoveVars(nm);
            Piece p = moveVars.piece;
            coords c = moveVars.coords;
            string moveType = moveVars.moveType;

            if (moveType == "ability")
            {
                if (checkAbilityStrict(nm.ability.ability, PieceAbilities.Spawn))
                {
                    continue;
                }
            }

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
            validateBoardState(bs);
            undoMove(undo, bs);

            //if (score <= alpha + PRUNE_CUTOFF + ((depth + 1) * PRUNE_SCALE) && alpha != Mathf.NegativeInfinity)
            if (score < alpha)
            {
                //Debug.Log("Egbot MIN CUTOFF | Depth: " + depth + " | Move: " + p + " to " + c.x + "," + c.y + " | Score: " + score + " | Alpha: " + alpha + " | Beta: " + beta);
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
            nodes++;
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

            //validateBoardState(bs);

            undoMove(undo, bs);

            //if (score >= beta - PRUNE_CUTOFF - ((depth + 1) * PRUNE_SCALE) && beta != Mathf.Infinity)
            if (score > beta)
            {
                //Debug.Log("Egbot MAX CUTOFF | Depth: " + depth + " | Move: " + p + " to " + c.x + "," + c.y + " | Score: " + score + " | Alpha: " + alpha + " | Beta: " + beta);
                return max;
            }
        }

        return max;
    }

    public float evaluate(BoardState bs)
    {
        List<float> pob = Jay_getPointsOnBoardState_simple(bs, true, color, true);

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
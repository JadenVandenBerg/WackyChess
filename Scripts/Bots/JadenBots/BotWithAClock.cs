using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using System.Text;

public class BotWithAClock : BotTemplate
{

    System.Diagnostics.Stopwatch watch;
    //1 is white, -1 is black
    public BotWithAClock(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Bot With a Clock";

        choosePieces();
    }

    /*
     * Workflow
     * 1: Initial loop through level one boardstates. Add to a queue for analysis.
     * 1b: Save boardstates
     * 2: Assign alpha/beta pruning values, (best/worst case scenario)
     * 
     */

    public class BestMove
    {
        public NextMove bestMove;
        public float score;

        public BestMove(NextMove bestMove, float score)
        {
            this.bestMove = bestMove;
            this.score = score;
        }
    }

    int nodeCount = 0;
    int MAX_MS = 4500;
    //int MAX_MS = 15000;
    bool timeExpired = false;
    int move = 0;

    override
    public NextMove nextMove()
    {
        int depth = 1;
        nodeCount = 0;
        move++;

        watch = System.Diagnostics.Stopwatch.StartNew();
        timeExpired = false;

        NextMove bestMove = null;
        float bestScore = Mathf.NegativeInfinity;

        while (!timeExpired)
        {
            BestMove result;

            if (depth < 3)
            {
                result = rootSearch(depth, Mathf.NegativeInfinity, Mathf.Infinity);
            }
            else
            {
                result = aspirationSearch(depth, bestScore);
            }

            if (!timeExpired && result != null)
            {
                bestMove = result.bestMove;
                bestScore = result.score;

                if (bestMove == null)
                {
                    Debug.LogError("BotWithAClock bestmove is null");
                    return getRandomBotMove(this);
                }

                if (bestMove.moveType == "move")
                {
                    Debug.Log("Depth: " + depth + ". Move: " + bestMove.move.p + " to " + bestMove.move.coords.x + "," + bestMove.move.coords.y + " Score: " + bestScore);
                }
                else if (bestMove.moveType == "ability")
                {
                    Debug.Log("Depth: " + depth + ". Ability: " + bestMove.ability.piece + "(" + bestMove.ability.ability + ") to " + bestMove.ability.coords.x + "," + bestMove.ability.coords.y + " Score: " + bestScore);
                }
            }

            depth++;
        }

        if (bestMove == null)
        {
            Debug.LogWarning("ERROR. No move returned");
            return getRandomBotMove(this);
        }

        return bestMove;
    }

    private BestMove aspirationSearch(int depth, float previousScore)
    {
        float window = 1.0f;

        while (!timeExpired)
        {
            float alpha = previousScore - window;
            float beta = previousScore + window;

            BestMove result = rootSearch(depth, alpha, beta);

            if (timeExpired)
            {
                return result;
            }

            if (result.score > alpha && result.score < beta)
            {
                return result;
            }

            window *= 2.0f;
        }

        return null;
    }

    public BestMove rootSearch(int depth, float alpha, float beta)
    {
        NextMove move = null;
        float bestScore = Mathf.NegativeInfinity;

        List<NextMove> moves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        moves.Sort((a, b) => getMoveOrderingScore(b).CompareTo(getMoveOrderingScore(a)));

        foreach (NextMove mv in moves)
        {
            var moveVars = getNextMoveVars(mv);
            Piece piece = moveVars.piece;
            coords coords = moveVars.coords;
            string moveType = moveVars.moveType;

            if (depth >= 4 && piece.baseType == "Pawn" && piece.color == this.color)
            {
                continue;
            }

            UndoMove undo;

            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(this.currentBoardState, piece, coords);
            }
            else
            {
                undo = undo_simulatePieceAbility(this.currentBoardState, mv.ability);
            }

            float score = alphaBeta(depth - 1, alpha, beta, false);

            undoMove(undo, this.currentBoardState);

            if (timeExpired)
            {
                break;
            }

            if (score > bestScore)
            {
                bestScore = score;
                move = mv;
            }

            if (score > alpha)
            {
                alpha = score;
            }

            if (alpha >= beta)
            {
                break;
            }
        }

        return new BestMove(move, bestScore);
    }

    private float getMoveOrderingScore(NextMove mv)
    {
        float score = 0;

        var moveVars = getNextMoveVars(mv);
        Piece piece = moveVars.piece;
        coords coords = moveVars.coords;
        string moveType = moveVars.moveType;

        UndoMove undo;

        if (moveType == "move")
        {
            undo = undo_simulatePieceMove(this.currentBoardState, piece, coords);
        }
        else
        {
            undo = undo_simulatePieceAbility(this.currentBoardState, mv.ability);
        }

        if (this.color == 1)
        {
            score += getPointsOnBoardState(this.currentBoardState, true)[0];
            score -= getPointsOnBoardState(this.currentBoardState, true)[1];
        }
        else
        {
            score -= getPointsOnBoardState(this.currentBoardState, true)[0];
            score += getPointsOnBoardState(this.currentBoardState, true)[1];
        }

        undoMove(undo, this.currentBoardState);

        return score;
    }

    private float alphaBeta(int depth, float alpha, float beta, bool maximizing)
    {
        nodeCount++;

        if ((nodeCount & 255) == 0)
        {
            if (watch.ElapsedMilliseconds >= MAX_MS)
            {
                timeExpired = true;
                return 0;
            }
        }

        if (depth == 0)
        {
            return evaluate(this.currentBoardState);
        }

        int sideToMove = maximizing ? this.color : -this.color;
        List<NextMove> moves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, sideToMove);

        if (maximizing)
        {
            float value = Mathf.NegativeInfinity;

            foreach(NextMove move in moves)
            {
                var moveVars = getNextMoveVars(move);
                Piece piece = moveVars.piece;
                coords coords = moveVars.coords;
                string moveType = moveVars.moveType;

                UndoMove undo;

                if (moveType == "move")
                {
                    undo = undo_simulatePieceMove(this.currentBoardState, piece, coords);
                }
                else
                {
                    undo = undo_simulatePieceAbility(this.currentBoardState, move.ability);
                }

                if (timeExpired)
                {
                    undoMove(undo, this.currentBoardState);
                    break;
                }

                value = Mathf.Max(value, alphaBeta(depth - 1, alpha, beta, false));
                undoMove(undo, this.currentBoardState);

                alpha = Mathf.Max(alpha, value);

                if (beta <= alpha)
                {
                    break;
                }
            }

            return value;
        }
        else
        {
            float value = Mathf.Infinity;

            foreach (NextMove move in moves)
            {
                var moveVars = getNextMoveVars(move);
                Piece piece = moveVars.piece;
                coords coords = moveVars.coords;
                string moveType = moveVars.moveType;

                UndoMove undo;

                if (moveType == "move")
                {
                    undo = undo_simulatePieceMove(this.currentBoardState, piece, coords);
                }
                else
                {
                    undo = undo_simulatePieceAbility(this.currentBoardState, move.ability);
                }

                if (timeExpired)
                {
                    undoMove(undo, this.currentBoardState);
                    break;
                }

                value = Mathf.Min(value, alphaBeta(depth - 1, alpha, beta, true));
                undoMove(undo, this.currentBoardState);

                beta = Mathf.Min(beta, value);

                if (beta <= alpha)
                {
                    break;
                }
            }

            return value;
        }
    }

    public float evaluate(BoardState bs)
    {
        List<float> pointsOnBoard = getPointsOnBoardState(bs, true);
        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

        int numMoves = 0;
        if (move < 15 || move >= 30)
        {
            numMoves = getAllPossibleBotMovesAndAbilities(this, bs, this.color).Count;
        }

        return (botPoints - oppPoints) + numMoves * 0.01f;
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;

class MoveState
{
    public BoardState bs;
    public int moveIter = 0;
    public float diff = 0;
    public Piece leadingPiece;
    public int[] leadingCoords = new int[] { };
    public NextMove leadingNextMove;

    public MoveState(BoardState bs, int moveIter, float diff, Piece leadingPiece, int[] leadingCoords, NextMove leadingNextMove)
    {
        this.bs = bs;
        this.moveIter = moveIter;
        this.diff = diff;
        this.leadingPiece = leadingPiece;
        this.leadingCoords = leadingCoords;
        this.leadingNextMove = leadingNextMove;
    }
}

public class TwoMoveBot : BotTemplate
{
    public TwoMoveBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Two Move Bot";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        Queue<MoveState> moveStates = new Queue<MoveState>();

        BotHelperFunctions.resetPiecePositions(null, BotHelperFunctions.convertBoardGrid(gameData.boardGrid));
        this.currentBoardState = BotHelperFunctions.copyBoardState(this.currentBoardState);

        List<float> startPoints = BotHelperFunctions.getPointsOnBoardState(this.currentBoardState, true);
        float botPoints = this.color == 1 ? startPoints[0] : startPoints[1];
        float oppPoints = this.color == -1 ? startPoints[0] : startPoints[1];

        float startingDiff = botPoints - oppPoints;

        MoveState ogMoveState = new MoveState(this.currentBoardState, 0, 0, null, null, null);
        moveStates.Enqueue(ogMoveState);

        float bestDiff = -100;
        List<MoveState> bestMoveStates = new List<MoveState>();

        while (moveStates.Count > 0)
        {
            MoveState next = moveStates.Dequeue();

            if (next.moveIter >= 2)
            {
                if (next.diff >= bestDiff)
                {
                    if (next.diff > bestDiff)
                    {
                        bestMoveStates.Clear();
                    }

                    bestMoveStates.Add(next);
                    bestDiff = next.diff;
                }
            }

            if (next.diff < -2 || next.moveIter >= 2)
            {
                continue;
            }

            //Don't look at next move if no captures
            if (next.diff == 0 && next.moveIter != 0)
            {
                if (next.diff >= bestDiff)
                {
                    if (next.diff > bestDiff)
                    {
                        bestMoveStates.Clear();
                    }

                    bestMoveStates.Add(next);
                    bestDiff = next.diff;
                }

                continue;
            }

            //BotHelperFunctions.resetPiecePositions(null, next.bs.boardGrid);
            //BoardState clone = BotHelperFunctions.copyBoardState(next.bs);

            List<NextMove> allBotMoves = getAllPossibleBotMovesAndAbilities(this, next.bs, this.color);

            foreach (NextMove nextMove in allBotMoves)
            {
                Piece piece_;
                int[] coords;

                string moveType = nextMove.moveType;

                if (moveType == "move")
                {
                    Move mv = nextMove.move;

                    piece_ = mv.p;
                    coords = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = nextMove.ability;

                    piece_ = pa.piece;
                    coords = pa.coords;
                }

                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, next.bs, piece_, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, next.bs, nextMove.ability);
                }

                List<NextMove> allBotMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

                BoardState bestMoveOppBS = null;
                float bestOppMoveDiff = +1000;
                bool ignoreMove = false;

                foreach (NextMove nextMoveOpp in allBotMovesOpp)
                {
                    Piece pieceOpp;
                    int[] coordsOpp;
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

                    BoardState cloneState_;
                    if (moveTypeOpp == "move")
                    {
                        cloneState_ = simulatePieceMove(this, cloneState, pieceOpp, coordsOpp);
                    }
                    else
                    {
                        cloneState_ = simulatePieceAbility(this, cloneState, nextMoveOpp.ability);
                    }

                    List<float> pointsOnBoard = BotHelperFunctions.getPointsOnBoardState(cloneState_, true);
                    float botPoints_ = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                    float oppPoints_ = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                    float diff = botPoints_ - oppPoints_;
                    if (diff < bestOppMoveDiff)
                    {
                        bestOppMoveDiff = diff;

                        if (startingDiff - bestOppMoveDiff <= -5) {
                            ignoreMove = true;
                            break;
                        }

                        bestMoveOppBS = cloneState_;
                    }
                }

                float realDiff = startingDiff - bestOppMoveDiff;

                if (!ignoreMove || moveStates.Count != 0) {
                    MoveState ms;
                    if (next.moveIter == 0)
                    {
                        ms = new MoveState(bestMoveOppBS, next.moveIter + 1, realDiff, piece_, coords, nextMove);
                    }
                    else
                    {
                        ms = new MoveState(bestMoveOppBS, next.moveIter + 1, realDiff, next.leadingPiece, next.leadingCoords, next.leadingNextMove);
                    }
                    moveStates.Enqueue(ms);
                }
            }
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(bestMoveStates.Count);

        NextMove move = bestMoveStates[rndIdx].leadingNextMove;
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
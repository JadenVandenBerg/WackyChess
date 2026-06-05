using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;


class MoveState
{
    public BoardState bs;
    public int moveIter = 0;
    public float diff = 0;
    public Piece leadingPiece;
    public coords leadingCoords = new coords(-1, -1);
    public NextMove leadingNextMove;
    public string moveChain;

    public MoveState(BoardState bs, int moveIter, float diff, Piece leadingPiece, coords leadingCoords, NextMove leadingNextMove, string moveChain)
    {
        this.bs = bs;
        this.moveIter = moveIter;
        this.diff = diff;
        this.leadingPiece = leadingPiece;
        this.leadingCoords = leadingCoords;
        this.leadingNextMove = leadingNextMove;
        this.moveChain = moveChain;
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

        resetPiecePositions(null, convertBoardGrid(gameData.boardGrid));
        this.currentBoardState = copyBoardState(this.currentBoardState);

        List<float> startPoints = getPointsOnBoardState(this.currentBoardState, true);
        float botPointsStart = this.color == 1 ? startPoints[0] : startPoints[1];
        float oppPointsStart = this.color == -1 ? startPoints[0] : startPoints[1];

        float ogDiff = botPointsStart - oppPointsStart;

        MoveState ogMoveState = new MoveState(this.currentBoardState, 0, 0, null, new coords(-1, -1), null, "");
        moveStates.Enqueue(ogMoveState);

        float bestDiff = -1000;
        List<MoveState> bestMoveStates = new List<MoveState>();

        int movesAnalyzed = 0;

        while (moveStates.Count > 0)
        {
            MoveState next = moveStates.Dequeue();

            if (next.moveIter >= 2)
            {
                //Debug.Log("NextMoveState Iter >= 2");
                if (next.diff >= bestDiff) {

                    //Debug.Log("next.diff > bestdiff");
                    if (next.diff > bestDiff)
                    {
                        bestMoveStates.Clear();
                    }

                    bestMoveStates.Add(next);
                    bestDiff = next.diff;
                }

                continue;
            }

            if (next.bs == null)
            {
                //Debug.LogWarning("Next.bs is null");
                continue;
            }

            //BotHelperFunctions.resetPiecePositions(null, next.bs.boardGrid);
            //BoardState clone = BotHelperFunctions.copyBoardState(next.bs);

            List<NextMove> allBotMoves = getAllPossibleBotMovesAndAbilities(this, next.bs, this.color);

            foreach (NextMove nextMove in allBotMoves)
            {
                var nextMoveVars = getNextMoveVars(nextMove);
                Piece piece_ = nextMoveVars.piece;
                coords coords = nextMoveVars.coords;
                string moveType = nextMoveVars.moveType;

                //Debug.Log("Analyzing Move: " + piece_.name + " to " + coords[0] + ", " + coords[1]);

                UndoMove undo;
                if (moveType == "move")
                {
                    undo = undo_simulatePieceMove(next.bs, piece_, coords);
                }
                else
                {
                    undo = undo_simulatePieceAbility(next.bs, nextMove.ability);
                }

                List<NextMove> allBotMovesOpp = getAllPossibleBotMovesAndAbilities(this, next.bs, this.color * -1);

                BoardState bestMoveOppBS = null;
                float bestOppMoveDiff = +1000;
                NextMove bestOppMove = null;

                foreach (NextMove nextMoveOpp in allBotMovesOpp)
                {
                    var nextMoveOppVars = getNextMoveVars(nextMoveOpp);
                    Piece pieceOpp = nextMoveOppVars.piece;
                    coords coordsOpp = nextMoveOppVars.coords;
                    string moveTypeOpp = nextMoveOppVars.moveType;

                    //Debug.Log("Analyzing Opp Move: " + pieceOpp.name + " to " + coordsOpp[0] + ", " + coordsOpp[1]);

                    UndoMove undo_;
                    if (moveTypeOpp == "move")
                    {
                        undo_ = undo_simulatePieceMove(next.bs, pieceOpp, coordsOpp);
                    }
                    else
                    {
                        undo_ = undo_simulatePieceAbility(next.bs, nextMoveOpp.ability);
                    }

                    if (next.moveIter == 0)
                    {
                        float bestResponseDiff = -1000;
                        NextMove bestResponse = null;

                        List<NextMove> allBotMovesResponse = getAllPossibleBotMovesAndAbilities(this, next.bs, this.color);

                        foreach (NextMove nextMoveResponse in allBotMovesResponse)
                        {
                            movesAnalyzed++;

                            var nextMoveResponseVars = getNextMoveVars(nextMoveResponse);
                            Piece pieceResponse = nextMoveResponseVars.piece;
                            coords coordsResponse = nextMoveResponseVars.coords;
                            string moveTypeResponse = nextMoveResponseVars.moveType;

                            //Debug.Log("Analyzing Response Move: " + pieceResponse.name + " to " + coordsResponse[0] + ", " + coordsResponse[1]);

                            UndoMove undo__;
                            if (moveTypeResponse == "move")
                            {
                                undo__ = undo_simulatePieceMove(next.bs, pieceResponse, coordsResponse);
                            }
                            else
                            {
                                undo__ = undo_simulatePieceAbility(next.bs, nextMoveResponse.ability);
                            }

                            List<float> pointsOnBoard_ = getPointsOnBoardState(next.bs, true);
                            float botPoints__ = this.color == 1 ? pointsOnBoard_[0] : pointsOnBoard_[1];
                            float oppPoints__ = this.color == -1 ? pointsOnBoard_[0] : pointsOnBoard_[1];

                            float diff_ = botPoints__ - oppPoints__;

                            if (diff_ > bestResponseDiff || bestResponse == null)
                            {
                                bestResponseDiff = diff_;

                                bestResponse = nextMoveResponse;
                            }

                            undoMove(undo__, next.bs);
                        }

                        if (bestResponseDiff < bestOppMoveDiff || bestOppMove == null)
                        {
                            bestOppMoveDiff = bestResponseDiff;

                            bestOppMove = nextMoveOpp;
                            bestMoveOppBS = copyBoardState(next.bs);
                        }
                    }
                    else
                    {
                        movesAnalyzed++;

                        List<float> pointsOnBoard = getPointsOnBoardState(next.bs, true);
                        float botPoints_ = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints_ = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        float diff = botPoints_ - oppPoints_;

                        if (diff < bestOppMoveDiff || bestOppMove == null)
                        {
                            bestOppMoveDiff = diff;

                            bestOppMove = nextMoveOpp;
                            bestMoveOppBS = copyBoardState(next.bs);
                        }
                    }

                    undoMove(undo_, next.bs);
                }

                float realDiff = bestOppMoveDiff - ogDiff;

                var nextMoveVarsOpp = getNextMoveVars(bestOppMove);
                Piece pieceOpp_ = nextMoveVarsOpp.piece;

                if (pieceOpp_ == null)
                {
                    continue;
                }
                else
                {
                    coords coordsOpp_ = nextMoveVarsOpp.coords;
                    string moveChain = "Bot: " + piece_.name + " to " + coords.x + "," + coords.y + ". Opp: " + pieceOpp_.name + " to " + coordsOpp_.x + "," + coordsOpp_.y + ". ";

                    MoveState ms;
                    if (next.moveIter == 0)
                    {
                        ms = new MoveState(bestMoveOppBS, next.moveIter + 1, realDiff, piece_, coords, nextMove, moveChain);
                    }
                    else
                    {
                        ms = new MoveState(bestMoveOppBS, next.moveIter + 1, realDiff, next.leadingPiece, next.leadingCoords, next.leadingNextMove, next.moveChain + moveChain);
                    }
                    //Debug.Log("Enqueueing Move");
                    moveStates.Enqueue(ms);

                    undoMove(undo, next.bs);
                }
                
            }
        }

        Debug.Log("Moves Analyzed: " + movesAnalyzed);

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

        Debug.Log("Selected Move: " + bestMoveStates[rndIdx].leadingPiece + " to " + bestMoveStates[rndIdx].leadingCoords.x + "," + bestMoveStates[rndIdx].leadingCoords.y + " Diff: " + bestMoveStates[rndIdx].diff);
        Debug.Log("Move Chain: " + bestMoveStates[rndIdx].moveChain);
        debug_printBoardState(bestMoveStates[rndIdx].bs);
        return move;
    }
}
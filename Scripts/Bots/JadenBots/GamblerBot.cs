using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;

public class GamblingBot : BotTemplate
{
    int DEPTH = 0;
    int MOVES_PERCENTAGE = 0;

    public GamblingBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Gambling Bot";

        DEPTH = globalDefs.globalRand.Next(1, 5);
        MOVES_PERCENTAGE = globalDefs.globalRand.Next(1, 101);

        Debug.LogWarning("Gambling Bot: " + DEPTH + " - " + MOVES_PERCENTAGE + "%");

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        if (DEPTH == 1)
        {
            return depth_1_nextMove();
        }
        else if (DEPTH == 2)
        {
            return depth_2_nextMove();
        }
        else if (DEPTH == 3)
        {
            return depth_3_nextMove();
        }
        else if (DEPTH == 4)
        {
            return depth_4_nextMove();
        }

        return getRandomBotMove(this);
    }

    public NextMove depth_1_nextMove()
    {
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        int movesLookedAt = 0;
        int movesToLookAt = Mathf.CeilToInt(allMoves.Count * (MOVES_PERCENTAGE / 100f));

        foreach (NextMove nextMove in allMoves)
        {
            if (movesLookedAt > movesToLookAt)
            {
                continue;
            }
            movesLookedAt++;

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

    public NextMove depth_2_nextMove()
    {
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        int movesLookedAt = 0;
        int movesToLookAt = Mathf.CeilToInt(allMoves.Count * (MOVES_PERCENTAGE / 100f));

        foreach (NextMove nextMove in allMoves)
        {
            if (movesLookedAt > movesToLookAt)
            {
                continue;
            }
            movesLookedAt++;

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
                    undo_ = undo_simulatePieceAbility(this.currentBoardState, nextMoveOpp.ability);
                }

                //Debug.Log("Simulating Opponent Piece " + moveTypeOpp + ": " + pieceOpp.name + " to " + coordsOpp.x + ", " + coordsOpp.y + " after " + piece.name + " to " + coords.x + ", " + coords.y);

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

            if (bestOppMoveDiff >= bestMoveDiff)
            {
                if (bestOppMoveDiff > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = bestOppMoveDiff;
                validMoves.Add(nextMove);
            }

            undoMove(undo, this.currentBoardState);
        }


        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
        return move;
    }

    public NextMove depth_3_nextMove()
    {
        float bestL2MoveDiff = -99999999f;
        List<NextMove> validMoves_L2 = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        int movesLookedAt = 0;
        int movesToLookAt = Mathf.CeilToInt(allMoves.Count * (MOVES_PERCENTAGE / 100f));

        foreach (NextMove nextMove in allMoves)
        {
            if (movesLookedAt > movesToLookAt)
            {
                continue;
            }
            movesLookedAt++;

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

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

            BoardState bestMoveOppBS = null;
            NextMove bestOppMove = null;
            float bestOppMoveDiff = +1000;

            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                var nextMoveOppVars = getNextMoveVars(nextMoveOpp);
                Piece pieceOpp = nextMoveOppVars.piece;
                coords coordsOpp = nextMoveOppVars.coords;
                string moveTypeOpp = nextMoveOppVars.moveType;

                if (nextMoveOpp.moveType == "ability" && nextMoveOpp.ability.ability == PieceAbilities.Spawn)
                {
                    continue;
                }

                UndoMove undo_ = null;
                if (moveTypeOpp == "move")
                {
                    undo_ = undo_simulatePieceMove(this.currentBoardState, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
                }
                else
                {
                    undo_ = undo_simulatePieceAbility(this.currentBoardState, nextMoveOpp.ability);
                }

                float bestResponseDiff = -1000;
                NextMove bestResponse = null;

                List<NextMove> allBotMovesResponse = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

                foreach (NextMove nextMoveResponse in allBotMovesResponse)
                {
                    var nextMoveResponseVars = getNextMoveVars(nextMoveResponse);
                    Piece pieceResponse = nextMoveResponseVars.piece;
                    coords coordsResponse = nextMoveResponseVars.coords;
                    string moveTypeResponse = nextMoveResponseVars.moveType;

                    UndoMove undo__;
                    if (moveTypeResponse == "move")
                    {
                        undo__ = undo_simulatePieceMove(this.currentBoardState, pieceResponse, new coords(coordsResponse.x, coordsResponse.y));
                    }
                    else
                    {
                        undo__ = undo_simulatePieceAbility(this.currentBoardState, nextMoveResponse.ability);
                    }

                    List<float> pointsOnBoard_ = getPointsOnBoardState(this.currentBoardState, true);
                    float botPoints__ = this.color == 1 ? pointsOnBoard_[0] : pointsOnBoard_[1];
                    float oppPoints__ = this.color == -1 ? pointsOnBoard_[0] : pointsOnBoard_[1];

                    float diff_ = botPoints__ - oppPoints__;

                    if (diff_ > bestResponseDiff || bestResponse == null)
                    {
                        bestResponseDiff = diff_;

                        bestResponse = nextMoveResponse;
                    }

                    undoMove(undo__, this.currentBoardState);
                }

                if (bestResponseDiff < bestOppMoveDiff || bestOppMove == null)
                {
                    bestOppMoveDiff = bestResponseDiff;

                    bestOppMove = nextMoveOpp;
                    bestMoveOppBS = copyBoardState(this.currentBoardState);
                }

                undoMove(undo_, this.currentBoardState);
            }

            if (bestMoveOppBS == null)
            {
                continue;
            }

            List<NextMove> allMoves_L2 = getAllPossibleBotMovesAndAbilities(this, bestMoveOppBS, this.color);

            foreach (NextMove nextMove_L2 in allMoves_L2)
            {
                var nextMoveVars_L2 = getNextMoveVars(nextMove_L2);
                Piece piece_L2 = nextMoveVars_L2.piece;
                coords coords_L2 = nextMoveVars_L2.coords;
                string moveType_L2 = nextMoveVars_L2.moveType;

                UndoMove undo_L2;

                if (moveType_L2 == "move")
                {
                    undo_L2 = undo_simulatePieceMove(bestMoveOppBS, piece_L2, new coords(coords_L2.x, coords_L2.y));
                }
                else
                {
                    undo_L2 = undo_simulatePieceAbility(bestMoveOppBS, nextMove_L2.ability);
                }

                List<float> pointsOnBoard = getPointsOnBoardState(bestMoveOppBS, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff = botPoints - oppPoints;

                if (diff >= bestL2MoveDiff)
                {
                    if (diff > bestL2MoveDiff)
                    {
                        validMoves_L2.Clear();
                    }

                    bestL2MoveDiff = diff;
                    validMoves_L2.Add(nextMove);
                }

                undoMove(undo_L2, bestMoveOppBS);
            }

            undoMove(undo, this.currentBoardState);
        }


        System.Random rand = new System.Random();

        if (validMoves_L2.Count == 0)
        {
            return getRandomBotMove(this);
        }
        else
        {
            int rndIdx = rand.Next(validMoves_L2.Count);

            NextMove move = validMoves_L2[rndIdx];

            return move;
        }
    }

    public NextMove depth_4_nextMove()
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

            if (next.bs == null)
            {
                continue;
            }

            List<NextMove> allBotMoves = getAllPossibleBotMovesAndAbilities(this, next.bs, this.color);
            
            int movesLookedAt = 0;
            int movesToLookAt = Mathf.CeilToInt(allBotMoves.Count * (MOVES_PERCENTAGE / 100f));

            foreach (NextMove nextMove in allBotMoves)
            {
                if (next.moveIter == 0)
                {
                    if (movesLookedAt > movesToLookAt)
                    {
                        continue;
                    }
                    movesLookedAt++;
                }

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
                    moveStates.Enqueue(ms);

                    undoMove(undo, next.bs);
                }

            }
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(bestMoveStates.Count);

        if (bestMoveStates.Count == 0)
        {
            return getRandomBotMove(this);
        }

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
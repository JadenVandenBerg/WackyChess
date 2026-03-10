using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;

public class Bloodbot : BotTemplate
{
    public Bloodbot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Bloodbot";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        int bestBoardControlDiff = -1000;
        float bestMoveDiff = -1000;

        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        List<float> startPoints = getPointsOnBoardState(this.currentBoardState, true);
        float botPoints_ = this.color == 1 ? startPoints[0] : startPoints[1];
        float oppPoints_ = this.color == -1 ? startPoints[0] : startPoints[1];

        float startingDiff = botPoints_ - oppPoints_;
        bool positivePts = false;

        //Each piece
        foreach (NextMove nextMove in allMoves)
        {
            Piece piece;
            int[] coords;

            string moveType = nextMove.moveType;

            if (moveType == "move")
            {
                Move mv = nextMove.move;

                piece = mv.p;
                coords = mv.coords;
            }
            else // moveType == "ability" guarenteed
            {
                PieceAbility pa = nextMove.ability;

                piece = pa.piece;
                coords = pa.coords;
            }

            BoardState originalBoardState = this.currentBoardState;

            BoardState cloneState;
            if (moveType == "move")
            {
                cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
            }
            else
            {
                cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
            }
            this.currentBoardState = cloneState;

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

            //best simulated move opponent can make
            NextMove bestOppMove;
            float bestOppMoveDiff = +1000;
            int bestOppBoardControlDiff = +1000;

            foreach (NextMove nextMoveOpp in allMovesOpp)
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

                BoardState originalBoardState_ = this.currentBoardState;
                BoardState cloneState_;
                if (moveTypeOpp == "move")
                {
                    cloneState_ = simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);
                }
                else
                {
                    cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp.ability);
                }
                this.currentBoardState = originalBoardState_;

                List<int> boardControlOnBS = getBoardControlOnBoardState(cloneState_);
                int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];
                int oppBoardControl = this.color == -1 ? boardControlOnBS[0] : boardControlOnBS[1];

                List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                //Debug.LogWarning("Points on board after " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                float diff_ = botPoints - oppPoints;
                if (diff_ < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff_;
                    bestOppMove = nextMoveOpp;
                    bestOppBoardControlDiff = botBoardControl - oppBoardControl;
                }
            }

            if (nextMove.moveType == "move") Debug.Log("Analyzed move: " + nextMove.move.p.name + " to " + coords[0] + "," + coords[1] + ". Points Diff: " + bestOppMoveDiff + " Board Control Diff: " + bestOppBoardControlDiff);
            if (nextMove.moveType == "ability") Debug.Log("Analyzed ability: " + nextMove.ability.piece.name + ": " + nextMove.ability.ability + " to " + coords[0] + "," + coords[1] + ". Points Diff: " + bestOppMoveDiff + " Board Control Diff: " + bestOppBoardControlDiff);

            float diff = bestOppMoveDiff;
            int boardControlDiff = bestOppBoardControlDiff;

            if (diff > startingDiff)
            {
                if (!positivePts || diff > bestMoveDiff)
                {
                    positivePts = true;
                    bestMoveDiff = diff;

                    validMoves.Clear();
                    validMoves.Add(nextMove);
                }
                else if (diff == bestMoveDiff)
                {
                    validMoves.Add(nextMove);
                }
            }
            else if (!positivePts)
            {
                if (diff > bestMoveDiff)
                {
                    bestMoveDiff = diff;
                    bestBoardControlDiff = boardControlDiff;

                    validMoves.Clear();
                    validMoves.Add(nextMove);
                }
                else if (diff == bestMoveDiff)
                {
                    if (boardControlDiff > bestBoardControlDiff)
                    {
                        bestBoardControlDiff = boardControlDiff;

                        validMoves.Clear();
                        validMoves.Add(nextMove);
                    }
                    else if (boardControlDiff == bestBoardControlDiff)
                    {
                        validMoves.Add(nextMove);
                    }
                }
            }

            this.currentBoardState = originalBoardState;
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
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

    private List<int> getBoardControlOnBoardState(BoardState bs)
    {
        List<int> boardControl = new List<int>();

        var botMovesWhite = BotHelperFunctions.getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = BotHelperFunctions.getAllPossibleBotMoves(this, bs, -1);
        List<PieceMoveList> listBotBlackMoves = botMovesBlack.pieceMoveList;

        List<int[]> uniqueCoords = new List<int[]>();
        foreach (PieceMoveList pml in listBotWhiteMoves)
        {
            Piece piece = pml.piece;
            List<int[]> _mL = pml.moves;

            foreach (int[] coords in _mL)
            {
                if (!HelperFunctions.coordsInList(uniqueCoords, coords))
                {
                    uniqueCoords.Add(coords);
                }
            }
        }
        boardControl.Add(uniqueCoords.Count);

        uniqueCoords = new List<int[]>();
        foreach (PieceMoveList pml in listBotBlackMoves)
        {
            Piece piece = pml.piece;
            List<int[]> _mL = pml.moves;

            foreach (int[] coords in _mL)
            {
                if (!HelperFunctions.coordsInList(uniqueCoords, coords))
                {
                    uniqueCoords.Add(coords);
                }
            }
        }
        boardControl.Add(uniqueCoords.Count);

        return boardControl;
    }
}

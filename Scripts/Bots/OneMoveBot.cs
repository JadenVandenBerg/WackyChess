using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class Move
{
    public Piece p;
    public int[] coords;

    public Move(Piece piece, int[] coords)
    {
        this.p = piece;
        this.coords = coords;
    }
}

public class OneMoveBot : BotTemplate
{
    public OneMoveBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "One Move Bot";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

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

            NextMove bestOppNextMove;
            float bestOppMoveDiff = +1000;

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

                List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                //Debug.Log("Testing a move by " + piece.name + " and opp " + pieceOpp.name + " results in " + (botPoints - 100) + " : " + (oppPoints - 100));
                //if (this.color == 1) Debug.LogWarning("Points on board after " + moveType + "," + moveTypeOpp + " " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                //debug_printBoardState(cloneState_);

                float diff = botPoints - oppPoints;
                if (diff < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff;
                    bestOppNextMove = nextMoveOpp;
                }
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
}
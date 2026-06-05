using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;


public class Lobotomy : BotTemplate
{
    public Lobotomy(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Lobotomy";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        float bestMoveDiff = +1000;
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        foreach (NextMove nextMove in allMoves)
        {
            var nextMoveVars = getNextMoveVars(nextMove);
            Piece piece = nextMoveVars.piece;
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            bool landmineKingTouch = false;
            if (piece.collateralType == 1)
            {
                if (isPieceTouchingKing(piece))
                {
                    landmineKingTouch = true;
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

                List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, false);
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

            float penalty = 0f;

            if (HelperFunctions.checkState(piece, PieceState.Fragile)) {
                penalty += 0.5f;

                if (piece.baseType == "King") {
                    penalty += 10f;
                }
            }

            float kingDist = -(checkKingDistance() / 10);
            penalty += kingDist;

            if (isCheckmateMinusBlocking()) {
                penalty -= 200;
            }

            Piece king = isolatedGetKing(this.currentBoardState, this.color);
            if (king != null && isInCheck(king.position))
            {
                penalty -= 200;
            }

            Piece enemyKing = isolatedGetKing(this.currentBoardState, this.color * -1);
            if (enemyKing != null && isInCheck(enemyKing.position))
            {
                penalty -= 50;
            }

            if (!landmineKingTouch && piece.collateralType == 1)
            {
                if (isPieceTouchingKing(piece))
                {
                    penalty += 15f;
                }
            }

            float realDiff = bestOppMoveDiff - penalty;

            if (realDiff <= bestMoveDiff)
            {
                if (realDiff < bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = realDiff;
                validMoves.Add(nextMove);
            }

            undoMove(undo, this.currentBoardState);
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
        return move;
    }

    private bool isPieceTouchingKing(Piece piece)
    {
        Piece king = BotHelperFunctions.isolatedGetKing(this.currentBoardState, this.color);

        if (king == null)
        {
            return false;
        }

        int x = Mathf.Abs(piece.position.x - king.position.x);
        int y = Mathf.Abs(piece.position.y - king.position.y);

        if (x == 1 || y == 1)
        {
            return true;
        }

        return false;
    }

    private float checkKingDistance() {
        Piece king = BotHelperFunctions.isolatedGetKing(this.currentBoardState, this.color);
        Piece oppKing = BotHelperFunctions.isolatedGetKing(this.currentBoardState, this.color * -1);

        if (king == null || oppKing == null)
        {
            return 0;
        }

        return ((float)System.Math.Sqrt(king.position.x * oppKing.position.x + king.position.y * oppKing.position.y));
    }

    private bool isCheckmateMinusBlocking() {
        Piece oppKing = BotHelperFunctions.isolatedGetKing(this.currentBoardState, this.color * -1);

        if (oppKing == null)
        {
            return true;
        }

        coords oppKingPos = oppKing.position;

        List<coords> oppKingMoves = getIsolatedStatePieceMoves(oppKing, this.currentBoardState, false);
        oppKingMoves.Add(new coords( oppKingPos.x, oppKingPos.y ));

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        foreach (NextMove nextMove in allMoves)
        {
            var nextMoveVars = getNextMoveVars(nextMove);
            Piece piece = nextMoveVars.piece;
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            foreach(coords oppKingPotentialCoords in new List<coords>(oppKingMoves)) {
                if (oppKingPotentialCoords.x == coords.x && oppKingPotentialCoords.y == coords.y) {
                    oppKingMoves.RemoveAll(a => a.x == coords.x && a.y == coords.y);
                }
            }
        }

        return oppKingMoves.Count == 0;
    }

    private bool isInCheck(coords kingCoords)
    {
        bool check = false;

        var allAttacks = getAllPossibleBotAttacks(this, this.currentBoardState, this.color * -1);
        List<PieceMoveList> pml = allAttacks.pieceMoveList;

        foreach (PieceMoveList pml_ in pml)
        {
            foreach(coords coords in pml_.moves)
            {
                if (kingCoords.x == coords.x && kingCoords.y == coords.y)
                {
                    check = true;
                }
            }
        }

        return check;
    }
}
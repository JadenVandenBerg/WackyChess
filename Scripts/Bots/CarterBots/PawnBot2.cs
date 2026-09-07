using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using System;

public class PawnBot2 : BotTemplate
{
    public PawnBot2(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Pawn Bot 2.0";
        choosePieces();
    }

    override

    public NextMove nextMove()
    {
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        foreach (NextMove nextMove in allMoves)
        {
            Piece piece;
            coords coords;
            string moveType = nextMove.moveType;

            if (moveType == "move")
            {
                Move mv = nextMove.move;

                piece = mv.p;
                coords = mv.coords;
            }
            else
            {
                PieceAbility pa = nextMove.ability;

                piece = pa.piece;
                coords = pa.coords;
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

            float bestOppMoveDiff = +1000;

            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                Piece pieceOpp;
                coords coordsOpp;

                string moveTypeOpp = nextMoveOpp.moveType;

                if (moveTypeOpp == "move")
                {
                    Move mv = nextMoveOpp.move;

                    pieceOpp = mv.p;
                    coordsOpp = mv.coords;
                }
                else
                {
                    PieceAbility pa = nextMoveOpp.ability;

                    pieceOpp = pa.piece;
                    coordsOpp = pa.coords;
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

                List<NextMove> allMoves2 = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

                float bestMoveDiff2 = -1000;

                foreach (NextMove nextMove2 in allMoves2)
                {
                    Piece piece2;
                    coords coords2;
                    string moveType2 = nextMove2.moveType;

                    if (moveType2 == "move")
                    {
                        Move mv2 = nextMove2.move;

                        piece2 = mv2.p;
                        coords2 = mv2.coords;
                    }
                    else
                    {
                        PieceAbility pa2 = nextMove2.ability;

                        piece2 = pa2.piece;
                        coords2 = pa2.coords;
                    }

                    UndoMove undo2 = null;

                    if (moveType2 == "move")
                    {
                        undo2 = undo_simulatePieceMove(this.currentBoardState, piece2, new coords(coords2.x, coords2.y));
                    }
                    else
                    {
                        undo2 = undo_simulatePieceAbility(this.currentBoardState, nextMove2.ability);
                    }

                    List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, true);
                    float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                    float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                    if (piece.baseType == "Pawn")
                    {
                        botPoints += 5;
                    }

                    List<Piece> piecesOnBoard = getPiecesOnBoardState(this.currentBoardState, this.color);
                    foreach (Piece item in piecesOnBoard)
                    {
                        if (item.baseType == "Pawn" || item.baseType == "Misc")
                        {
                            botPoints += 5;
                        }
                    }

                    float diff = botPoints - oppPoints;
                    if (diff > bestMoveDiff2)
                    {
                        bestMoveDiff2 = diff;
                    }

                    undoMove(undo2, this.currentBoardState);

                }

                if (bestOppMoveDiff > bestMoveDiff2)
                {
                    bestOppMoveDiff = bestMoveDiff2;
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

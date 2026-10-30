using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static JadenBotHelperFunctions;
using static HelperFunctions;

public class AbsobotZero : BotTemplate
{
    //1 is white, -1 is black
    public AbsobotZero(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Absobot Zero";

        //This function populates the pieces variable
        choosePieces();
    }

    Queue<NextMove> lastTenMoves = new Queue<NextMove>();

    override
    public NextMove nextMove()
    {
        Dictionary<Piece, float> pieceValues = new Dictionary<Piece, float>();

        List<Piece> pieces = getPiecesOnBoardState(currentBoardState, this.color);

        foreach(Piece p in pieces)
        {
            float points = p.points;

            if (!p.hasMoved)
            {
                p.points += 2f;
            }

            if (p.baseType == "Pawn")
            {
                points += globalDefs.globalRand.Next(1, 5);
            }

            if (p.baseType == "King")
            {
                points -= 25f;
            }

            if (checkState(p, PieceState.Frozen))
            {
                points *= 3;

                if (p.baseType == "King")
                {
                    points += 1000f;
                }
            }

            if (checkState(p, PieceState.Fragile))
            {
                points /= 2f;
            }

            if (p.collateralType != -1)
            {
                points += 2f;
            }

            if (p.lives != 0)
            {
                points += 2f;
            }

            if (color == 1 && p.position.y <= 3)
            {
                points += 10 - p.position.y;
            }

            if (color == -1 && p.position.y >= 6)
            {
                points += 10 - (8 - p.position.y);
            }

            foreach(NextMove nm in lastTenMoves)
            {
                Piece p_;
                if (nm.moveType == "move")
                {
                    p_ = nm.move.p;
                }
                else
                {
                    p_ = nm.ability.piece;
                }

                if (p_.name == p.name)
                {
                    points -= 1.25f;
                }
            }

            pieceValues.Add(p, points);
        }


        List<PieceCoords> hangingPieces = Jay_getHangingPieces(currentBoardState, this.color);
        foreach(PieceCoords hangingPiece in hangingPieces)
        {
            pieceValues[hangingPiece.p] += 15f;
        }

        float maxValue = Mathf.NegativeInfinity;
        Piece bestPiece = null;
        foreach (Piece piece in pieceValues.Keys)
        {
            float value = pieceValues[piece];

            if (value > maxValue)
            {
                maxValue = value;
                bestPiece = piece;
            }
        }

        List<NextMove> allMoves = getAllPossibleBotPieceMoves(currentBoardState, bestPiece);

        NextMove move;
        if (allMoves.Count == 0)
        {
            move = getRandomBotMove(this);
        }
        else
        {
            move = allMoves[globalDefs.globalRand.Next(allMoves.Count)];
        }

        lastTenMoves.Enqueue(move);

        if (lastTenMoves.Count > 11)
        {
            lastTenMoves.Dequeue();
        }

        return move;
    }
}
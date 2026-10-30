using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static JadenBotHelperFunctions;

public class Botgrabber : BotTemplate
{
    //1 is white, -1 is black
    public Botgrabber(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Botgrabber";

        //This function populates the pieces variable
        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        List<PieceCoords> hangingPieces = Jay_getHangingPieces(currentBoardState, this.color * -1);

        foreach (NextMove nextMove in allMoves)
        {
            var nextMoveVars = getNextMoveVars(nextMove);
            Piece piece = nextMoveVars.piece;
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            //Look for hanging piece here
            float hangingPieceBonus = 0f;

            foreach (PieceCoords hangingPiece in hangingPieces)
            {
                if (coords.x == hangingPiece.c.x + 1 && coords.y == hangingPiece.c.y + 1)
                {
                    hangingPieceBonus += 10f + hangingPiece.p.points;
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

            float score = hangingPieceBonus;

            if (score >= bestMoveDiff)
            {
                if (score > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = score;
                validMoves.Add(nextMove);
            }

            undoMove(undo, this.currentBoardState);
        }


        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

        return move;
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class RestrictorBot : BotTemplate
{
    public RestrictorBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Restrictor Bot";
        choosePieces();
    }
    private bool isGuarded(BotTemplate bot, BoardState bs, int color, coords coords)
    {
        bool isGuarded = false;
        var attacks = getAllPossibleBotAttacks(bot, bs, color);

        string coordsStr = "";
        coordsStr += (coords.x).ToString();
        coordsStr += (coords.y).ToString();

        foreach (var piece in attacks.pieceMoveList)
        {
            foreach (var attack in piece.moves)
            {
                string attackStr = "";
                attackStr += (attack.x).ToString();
                attackStr += (attack.y).ToString();
                if (attackStr == coordsStr)
                {
                    isGuarded = true;
                }
            }
        }
        return isGuarded;
    }

    override

    public NextMove nextMove()
    {
        float bestOppNumMoves = 100000;
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

            List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState, this.color);

            coords kingCoords;
            kingCoords.x = 0;
            kingCoords.y = 0;

            foreach (Piece item in piecesOnBoard)
            {
                if (item.baseType == "King")
                {
                    kingCoords = item.position;
                }
            }

            List<Piece> piecesOnBoardOpp = getPiecesOnBoardState(cloneState, this.color * -1);

            coords kingCoordsOpp;
            kingCoordsOpp.x = 0;
            kingCoordsOpp.y = 0;

            foreach (Piece item in piecesOnBoardOpp)
            {
                if (item.baseType == "King")
                {
                    kingCoordsOpp = item.position;
                }
            }

            bool checkOpp = false;
            bool inCheck = false;

            if (kingCoordsOpp.x != 0 && kingCoordsOpp.y != 0)
            {
                checkOpp = isGuarded(this, cloneState, this.color, kingCoordsOpp);
            }

            if (kingCoords.x != 0 && kingCoords.y != 0)
            {
                inCheck = isGuarded(this, cloneState, this.color * -1, kingCoords);
            }

            int moveScore = 1000000;

            if (inCheck == false)
            {
                moveScore = allMovesOpp.Count;
            }
            else
            {
                moveScore = 100000000;
            }

            if (checkOpp == true)
            {
                moveScore -= 1000000;
            }

            if (bestOppNumMoves >= moveScore)
            {
                if (bestOppNumMoves > moveScore)
                {
                    validMoves.Clear();
                }

                bestOppNumMoves = moveScore;
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
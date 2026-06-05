using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class EqualityBot : BotTemplate
{
	public EqualityBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Equality Bot";

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

    Dictionary<string,int> numMoves = new Dictionary<string,int>();

	override
	public NextMove nextMove()
	{
        List<Piece> piecesOnBoard = getPiecesOnBoardState(this.currentBoardState, this.color);

        foreach (Piece piece_ in piecesOnBoard)
		{
			if (numMoves.ContainsKey(piece_.name) == false)
			{
				numMoves.Add(piece_.name, 0);
			}
		}

        float bestMoveDiff = -100000000;
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

			NextMove bestOppNextMove;
			float bestOppMoveDiff = +1000;

            List<Piece> piecesOnBoard_ = getPiecesOnBoardState(cloneState, this.color);

            coords kingCoords;
            kingCoords.x = 0;
            kingCoords.y = 0;

            foreach (Piece item in piecesOnBoard_)
            {
                if (item.baseType == "King")
                {
                    kingCoords = item.position;
                }
            }

            bool inCheck = false;

            if (kingCoords.x != 0 && kingCoords.y != 0)
            {
                inCheck = isGuarded(this, cloneState, this.color * -1, kingCoords);
            }

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

				botPoints -= numMoves[piece.name]*100;

				if (inCheck == true)
                {
                    botPoints -= 100000000000;
                }

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
			numMoves[move.move.p.name] += 1;
		}
		else
		{
			move.ability.piece = getOriginalPieceFromClone(move.ability.piece);
            numMoves[move.ability.piece.name] += 1;
        }

		return move;
	}
}
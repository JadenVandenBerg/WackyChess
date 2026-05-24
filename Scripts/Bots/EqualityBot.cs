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
			int[] coords;
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
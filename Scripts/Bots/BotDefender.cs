using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class BotDefender : BotTemplate
{	
	public BotDefender(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "BotDefender";

		choosePieces();
	}

	private bool isGuarded(BotTemplate bot, BoardState bs, int color, int[] coords)
	{
		bool isGuarded = false;
		var attacks = getAllTheoreticalBotAttacks(bot, bs, color);

		string coordsStr = "";
		coordsStr += (coords[0]).ToString();
		coordsStr += (coords[1]).ToString();

		foreach (var piece in attacks.pieceMoveList)
		{
			foreach (var attack in piece.moves)
			{
				string attackStr = "";
				attackStr += (attack[0]).ToString();
				attackStr += (attack[1]).ToString();
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
		float bestMoveDiff = -1000;
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

				List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState, this.color);

				foreach (Piece piece_ in piecesOnBoard)
				{ 
					if (isGuarded(this, cloneState, this.color, piece_.position) == true)
					{
						botPoints += 5;
					}
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
		}
		else
		{
			move.ability.piece = getOriginalPieceFromClone(move.ability.piece);
		}
		return move;
	}
}
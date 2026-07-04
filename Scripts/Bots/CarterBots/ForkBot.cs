using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class ForkBot : BotTemplate
{
	public ForkBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Fork Bot";
		choosePieces();
	}

	private List<Piece> getGuards(BotTemplate bot, BoardState bs, int color, coords coords)
	{
		List<Piece> guards = new List<Piece>();
		var attacks = getAllTheoreticalBotAttacks(bot, bs, color);

		string coordsStr = "";
		coordsStr += (coords.x).ToString();
		coordsStr += (coords.y).ToString();

		foreach (var piece in attacks.pieceMoveList)
		{
			bool isPieceGuarding = false;
			foreach (var attack in piece.moves)
			{
				string attackStr = "";
				attackStr += (attack.x).ToString();
				attackStr += (attack.y).ToString();
				if (attackStr == coordsStr)
				{
					isPieceGuarding = true;
				}
			}
			if (isPieceGuarding == true)
			{
				guards.Add(piece.piece);
			}
		}
		return guards;
	}

	private List<Piece> getAttacking(BotTemplate bot, BoardState bs, int color, Piece piece)
	{
		List<Piece> Attacking = new List<Piece>();
		var attacks = getIsolatedStatePieceAttacks(piece, bs, false, false);
		List<Piece> oppPieces = getPiecesOnBoardState(bs, color * -1);

		foreach (Piece piece_ in oppPieces)
		{
			string pieceStr = "";
			pieceStr += (piece_.position.x).ToString();
			pieceStr += (piece_.position.y).ToString();
			bool isPieceAttacked = false;
			foreach (coords coords in attacks)
			{
				string coordsStr = "";
				coordsStr += (coords.x).ToString();
				coordsStr += (coords.y).ToString();
				if (coordsStr == pieceStr)
				{
					isPieceAttacked = true;
				}
			}
			if (isPieceAttacked == true)
			{
				Attacking.Add(piece_);
			}
		}
		return Attacking;
	}

	private float getForkValue(BotTemplate bot, BoardState bs, int color, Piece piece)
	{
		float forkValue = 0;
		List<Piece> forkedPieces = new List<Piece>();
		List<Piece> targets = getAttacking(bot, bs, color, piece);

		foreach (Piece target in targets)
		{
			if (target.baseType == "King")
			{
				forkedPieces.Add(target);
			}
			else
			{
				List<Piece> guards = getGuards(bot, bs, color * -1, target.position);
				if (guards.Count == 0)
				{
					forkedPieces.Add(target);
				}
			}
		}

		if (forkedPieces.Count > 1)
		{
			int kingIndex = -1;
			List<Piece> sortedPieces = forkedPieces.OrderByDescending(p => p.points).ToList();
			foreach (Piece sp in sortedPieces)
			{
				if (sp.baseType == "King")
				{
					kingIndex = sortedPieces.IndexOf(sp);
				}
			}
			if (kingIndex >= 0)
			{
				Piece king = sortedPieces[kingIndex];
				sortedPieces.Remove(king);
				sortedPieces.Insert(0, king);
			}
			forkValue = sortedPieces[1].points;
		}

		return forkValue;

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

			List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState, this.color);

			float bestForkValue = 0;

			foreach(Piece myPiece in piecesOnBoard)
			{
				float forkValue = getForkValue(this, cloneState, this.color, myPiece);
				if (forkValue > bestForkValue)
				{
					bestForkValue = forkValue;
				}
			}

			List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

			NextMove bestOppNextMove;
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

                float diff = botPoints - oppPoints;

				botPoints += bestForkValue;

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
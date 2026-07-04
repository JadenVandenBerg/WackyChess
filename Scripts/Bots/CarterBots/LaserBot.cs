using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System;

public class LaserBot : BotTemplate
{
	public LaserBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Laser Bot";
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
		float bestMoveDiff = -1000;
		Dictionary<NextMove, float> validMoves = new Dictionary<NextMove, float>();
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

			if (kingCoordsOpp.x != 0 && kingCoordsOpp.y != 0)
			{
				checkOpp = isGuarded(this, cloneState, this.color, kingCoordsOpp);
			}

			NextMove bestOppNextMove;
			float bestOppMoveDiff = +1000;
			int numPinMoves = 0;

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

				List<Piece> piecesOnBoardOpp_ = getPiecesOnBoardState(cloneState_, this.color * -1);

				coords kingCoordsOpp_;
				kingCoordsOpp_.x = 0;
				kingCoordsOpp_.y = 0;

				foreach (Piece item in piecesOnBoardOpp_)
				{
					if (item.baseType == "King")
					{
						kingCoordsOpp_ = item.position;
					}
				}

				bool checkOpp_ = false;

				if (kingCoordsOpp_.x != 0 && kingCoordsOpp_.y != 0)
				{
					checkOpp_ = isGuarded(this, cloneState_, this.color, kingCoordsOpp_);
				}

				if (checkOpp_ == true)
				{
					if (checkOpp == false && pieceOpp.baseType != "King")
					{
						numPinMoves += 1;
					}
				}

				float diff = botPoints - oppPoints;
				if (diff < bestOppMoveDiff)
				{
					bestOppMoveDiff = diff;
					bestOppNextMove = nextMoveOpp;
				}
			}

			if (checkOpp == true)
			{
				bestOppMoveDiff += 2.5f;
			}

			bestOppMoveDiff += (numPinMoves * 0.5f);

			List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState, this.color);
			List<Piece> piecesOnBoardOpp__ = getPiecesOnBoardState(cloneState, this.color * -1);

			coords kingCoords = new coords(-1, -1);
			foreach (Piece item in piecesOnBoardOpp__)
			{
				if (item.baseType == "King")
				{
					kingCoords = item.position;
				}
			}

			float botPointsT = 0;

			foreach (Piece item in piecesOnBoard)
			{
				float distance = 0;
				if (kingCoords.x == -1)
				{
					distance += 0;
				}
				else
				{
					distance += Math.Abs(item.position.x - kingCoords.x) + Math.Abs(item.position.y - kingCoords.y);
				}
				botPointsT += distance;
			}

			if (bestOppMoveDiff >= bestMoveDiff)
			{
				if (bestOppMoveDiff > bestMoveDiff)
				{
					validMoves.Clear();
				}

				bestMoveDiff = bestOppMoveDiff;
				validMoves.Add(nextMove, botPointsT);
			}

			this.currentBoardState = originalBoardState;
		}

		List<NextMove> validMovesL = new List<NextMove>();
		float bestDistance = 99999999999999;

		foreach (NextMove vMove in validMoves.Keys)
		{
			if (validMoves[vMove] <= bestDistance)
			{
				if (validMoves[vMove] < bestDistance)
				{
					validMovesL.Clear();
				}

				bestDistance = validMoves[vMove];

				validMovesL.Add(vMove);
			}
		}

		System.Random rand = new System.Random();
		int rndIdx = rand.Next(validMovesL.Count);

		NextMove move = validMovesL[rndIdx];

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
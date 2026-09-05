using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static BotHelperFunctions;
using static UnityEditor.Progress;

public class YOLOBot : BotTemplate
{
	public YOLOBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "YOLO Bot";
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
		float bestDistance = 100000;
		List<NextMove> validMoves = new List<NextMove>();
		List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

		Dictionary<Piece, float> originalDistances = new Dictionary<Piece, float>();

		List<Piece> piecesOnBoardPreMove = getPiecesOnBoardState(this.currentBoardState, this.color);
		List<Piece> piecesOnBoardOpp = getPiecesOnBoardState(this.currentBoardState, this.color * -1);

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

		foreach (Piece oPiece in piecesOnBoardPreMove)
		{
			float distance_;
			float distanceX_ = Math.Abs(oPiece.position.x - kingCoordsOpp.x);
			float distanceY_ = Math.Abs(oPiece.position.y - kingCoordsOpp.y);
			distance_ = (float)Math.Sqrt(distanceX_ * distanceX_ + distanceY_ * distanceY_);
			originalDistances.Add(oPiece, distance_);
		}

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

			bool inCheck = false;

			if (kingCoords.x != 0 && kingCoords.y != 0)
			{
				inCheck = isGuarded(this, cloneState, this.color * -1, kingCoords);
			}

			float distance;
			float newDistance;

			if (inCheck == false)
			{
				float distanceX = Math.Abs(coords.x - kingCoordsOpp.x);
				float distanceY = Math.Abs(coords.y - kingCoordsOpp.y);
				newDistance = (float)Math.Sqrt(distanceX*distanceX + distanceY*distanceY);
				if (originalDistances.ContainsKey(piece))
				{
					distance = newDistance - originalDistances[piece];
				}
				else
				{
					distance = 100;
				}
				if (isGuarded(this, cloneState, this.color * -1, piece.position) == true)
				{
					distance += 0.01f;
				}
				if (isGuarded(this, cloneState, this.color, piece.position) == false)
				{
					distance += 0.01f;
				}
			}
			else
			{
				distance = 10000;
			}

			if (bestDistance >= distance)
			{
				if (bestDistance > distance)
				{
					validMoves.Clear();
				}

				bestDistance = distance;
				validMoves.Add(nextMove);
			}

			this.currentBoardState = originalBoardState;
		}

		System.Random rand = new System.Random();

		if (validMoves.Count == 0)
		{
			return getRandomBotMove(this);
		}

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
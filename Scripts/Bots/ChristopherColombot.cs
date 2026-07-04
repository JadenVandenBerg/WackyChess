using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BotHelperFunctions;
using System;

public class ChristopherColumbot : BotTemplate
{
	public ChristopherColumbot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Christopher Columbot";

		choosePieces();
	}

	List<string> discoveredSquares = new List<string>();

	override
	public NextMove nextMove()
	{
		List<Piece> piecesOnBoard = getPiecesOnBoardState(this.currentBoardState, this.color);

        foreach (Piece piece_ in piecesOnBoard)
		{
			string pieceStr = "";
			pieceStr += (piece_.position.x).ToString();
			pieceStr += (piece_.position.y).ToString();
			if (discoveredSquares.Contains(pieceStr) == false)
			{
				discoveredSquares.Add(pieceStr);
				//Debug.Log("New Square discovered at " + pieceStr + ". " + discoveredSquares.Count() + " total.");
			}
		}

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

                string coordsStr = "";
                coordsStr += (coords.x).ToString();
                coordsStr += (coords.y).ToString();

                if (discoveredSquares.Contains(coordsStr) == false)
				{
					botPoints += 4;
				}

                float diff = botPoints - oppPoints;
                if (diff < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff;
                    bestOppNextMove = nextMoveOpp;
                }

            }

            List<string> undiscoveredSquares = new List<string>();
            for (int x = 1; x < 9; x++)
            {
                for (int y = 1; y < 9; y++)
                {
                    string str = "";
                    str += x.ToString();
                    str += y.ToString();

                    if (discoveredSquares.Contains(str) == false)
                    {
                        undiscoveredSquares.Add(str);
                    }
                }
            }

            List<Piece> piecesOnBoard_ = getPiecesOnBoardState(cloneState, this.color);

            float botPointsT = 0;
            foreach (Piece piece__ in piecesOnBoard_)
            {
                foreach (string square in undiscoveredSquares)
                {
                    float distance = 0;
                    distance += Math.Abs(piece__.position.x - square[0]) + Math.Abs(piece__.position.y - square[1]);
                    botPointsT += distance;
                }
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
        coords mvCoords;
        if (move.moveType == "move")
		{
			move.move.p = getOriginalPieceFromClone(move.move.p);
			mvCoords = move.move.coords;
		}
		else
		{
			move.ability.piece = getOriginalPieceFromClone(move.ability.piece);
			mvCoords = move.ability.coords;
		}

        string finalStr = "";
        finalStr += (mvCoords.x).ToString();
        finalStr += (mvCoords.y).ToString();

        if (discoveredSquares.Contains(finalStr) == false)
        {
            discoveredSquares.Add(finalStr);
            Debug.Log("New Square discovered at " + finalStr + ". " + discoveredSquares.Count() + " total.");
        }

        return move;
	}
}
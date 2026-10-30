using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using System;
using static HelperFunctions;

public class NearsightedBot : BotTemplate
{
	public NearsightedBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Nearsighted Bot";
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

	bool firstTurn = true;

	int sightDist;

    override

	public NextMove nextMove()
	{
		if (firstTurn)
		{
			System.Random rand1 = new System.Random();
			sightDist = rand1.Next(1, 4);
			firstTurn = false;
		}

		List<coords> hiddenSquares = new List<coords>();
		List<Piece> piecesPreMove = getPiecesOnBoardState(this.currentBoardState, this.color);

		for (int x = 1; x < 9; x++)
		{
			for (int y = 1; y < 9; y++)
			{
				bool squareSeen = false;
				foreach (Piece piece1 in piecesPreMove)
				{
					if (Math.Abs(piece1.position.x - x) <= sightDist && Math.Abs(piece1.position.y - y) <= sightDist)
					{
						squareSeen = true;
					}
				}

				if (squareSeen == false)
				{
					coords unseen = new coords(x, y);
					hiddenSquares.Add(unseen);
				}
			}
		}

		foreach (coords square in hiddenSquares)
		{ 

			int x = square.x;
			int y = square.y;

			highlightSquare(findSquare(x, y), Color.black);

			this.currentBoardState.boardGrid[x - 1, y - 1].Clear();
			
			/*
			foreach (Piece piece in this.currentBoardState.boardGrid[x, y])
			{
				piece.points = 0;
			}
			*/
		}

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

            UndoMove undo;

			if (moveType == "move")
			{
				undo = undo_simulatePieceMove(this.currentBoardState, piece, new coords(coords.x, coords.y));
			}
			else
			{
				undo = undo_simulatePieceAbility(this.currentBoardState, nextMove.ability);
			}

            coords kingPos = new coords(-1, -1);
            List<Piece> piecesOnBoard1 = getPiecesOnBoardState(this.currentBoardState, this.color);
            foreach (Piece item in piecesOnBoard1)
            {
                if (item.baseType == "King")
                {
                    kingPos = item.position;
                }
            }

            bool inCheck = isGuarded(this, this.currentBoardState, this.color * -1, kingPos);

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

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

				UndoMove undo_ = null;

				if (moveTypeOpp == "move")
				{
					undo_ = undo_simulatePieceMove(this.currentBoardState, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
				}
				else
				{
                    undo_ = undo_simulatePieceAbility(this.currentBoardState, nextMoveOpp.ability);
				}

				List<NextMove> allMoves2 = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

				float bestMoveDiff2 = -1000;

				foreach (NextMove nextMove2 in allMoves2)
				{
					Piece piece2;
					coords coords2;
					string moveType2 = nextMove2.moveType;

					if (moveType2 == "move")
					{
						Move mv2 = nextMove2.move;

						piece2 = mv2.p;
						coords2 = mv2.coords;
					}
					else
					{
						PieceAbility pa2 = nextMove2.ability;

						piece2 = pa2.piece;
						coords2 = pa2.coords;
					}

					UndoMove undo2 = null;

                    if (moveType2 == "move")
                    {
                        undo2 = undo_simulatePieceMove(this.currentBoardState, piece2, new coords(coords2.x, coords2.y));
                    }
                    else
                    {
                        undo2 = undo_simulatePieceAbility(this.currentBoardState, nextMove2.ability);
                    }

					List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, true);
					float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
					float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                    if (inCheck == true)
                    {
                        botPoints -= 100;
                    }

                    float diff = botPoints - oppPoints;
					if (diff > bestMoveDiff2)
					{
						bestMoveDiff2 = diff;
					}

                    undoMove(undo2, this.currentBoardState);

                }

				if (bestOppMoveDiff > bestMoveDiff2)
				{
					bestOppMoveDiff = bestMoveDiff2;
				}

                undoMove(undo_, this.currentBoardState);
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

            undoMove(undo, this.currentBoardState);
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
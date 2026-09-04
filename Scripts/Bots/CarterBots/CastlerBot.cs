using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static BotHelperFunctions;
using static HelperFunctions;

public class CastlerBot : BotTemplate
{
	public CastlerBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Castler Bot";
		choosePieces();
	}

	private List<Piece> getAttackers(BotTemplate bot, BoardState bs, int color, coords coords)
    {
        List<Piece> Attackers = new List<Piece>();
        var attacks = getAllPossibleBotAttacks(bot, bs, color * -1);

        string coordsStr = "";
        coordsStr += (coords.x).ToString();
        coordsStr += (coords.y).ToString();

        foreach (var piece in attacks.pieceMoveList)
        {
            bool isPieceAttacking = false;
            foreach (var attack in piece.moves)
            {
                string attackStr = "";
                attackStr += (attack.x).ToString();
                attackStr += (attack.y).ToString();
                if (attackStr == coordsStr)
                {
                    isPieceAttacking = true;
                }
            }
            if (isPieceAttacking == true)
            {
                Attackers.Add(piece.piece);
            }
        }
        return Attackers;
    }

	override

	public NextMove nextMove()
	{
		bool castleLeftPossible = true;
		bool castleRightPossible = true;
		bool canCastleLeft = isolatedCheckCanCastle(this.currentBoardState, -1, this.color);
        bool canCastleRight = isolatedCheckCanCastle(this.currentBoardState, 1, this.color);

        List<Piece> piecesOnBoard = getPiecesOnBoardState(this.currentBoardState, this.color);
        List<Piece> piecesOnBoardOpp = getPiecesOnBoardState(this.currentBoardState, this.color * -1);
		List<Piece> piecesInWayRight = new List<Piece>();

        Piece king = this.king;
		Piece rookL;
		Piece rookR;

		if (this.color == 1)
		{
			rookL = findPieceOnBoardStateFromPanelCode(this.currentBoardState, "w_r1");
			rookR = findPieceOnBoardStateFromPanelCode(this.currentBoardState, "w_r2");
		}
		else
		{
			rookL = findPieceOnBoardStateFromPanelCode(this.currentBoardState, "b_r1");
			rookR = findPieceOnBoardStateFromPanelCode(this.currentBoardState, "b_r2");
		}

		if (rookL.alive == 0)
		{
			castleLeftPossible = false;
		}
		else
		{
            if (rookL.hasMoved == true || king.hasMoved == true)
            {
                castleLeftPossible = false;
            }
        }

        if (rookR.alive == 0)
        {
            castleRightPossible = false;
        }
		else
		{
            if (rookR.hasMoved == true || king.hasMoved == true)
            {
                castleRightPossible = false;
            }
        }

        foreach (Piece piece1 in piecesOnBoard)
        {
            if (piece1.position.y == king.position.y)
			{
				if (piece1.position.x == king.position.x + 1 || piece1.position.x == king.position.x + 2)
				{
					piecesInWayRight.Add(piece1);
				}
			}
        }
        foreach (Piece piece2 in piecesOnBoardOpp)
        {
            if (piece2.position.y == king.position.y)
            {
                if (piece2.position.x == king.position.x + 1 || piece2.position.x == king.position.x + 2)
                {
                    piecesInWayRight.Add(piece2);
                }
            }
        }

        float bestMoveDiff = -1000;
		List<NextMove> validMoves = new List<NextMove>();
		List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

		foreach (NextMove nextMove in allMoves)
		{
			bool isCastlingLeft = false;
			bool isCastlingRight = false;
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
				PieceAbilities ability_ = pa.ability;

                if (ability_ == PieceAbilities.CastleLeft)
				{
					isCastlingLeft = true;
				}
				else if (ability_ == PieceAbilities.CastleRight)
                {
                    isCastlingRight = true;
                }
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

                if (this.color == 1 && cloneState_.inCheck[0] == 0 || this.color == -1 && cloneState_.inCheck[1] == 0)
				{
					botPoints += 800;
				}

                if (isCastlingRight == true)
				{
					botPoints += 1000;
				}
				if (isCastlingLeft == true)
				{
					botPoints += 900;
				}

				if (castleRightPossible == true)
				{
					if (piece.baseType == "King" || piece.baseType == "Rook")
					{
						botPoints -= 100;
					}
					if (canCastleRight == false)
					{
						if (piecesInWayRight.Count != 0)
						{
							foreach (Piece piece3 in piecesInWayRight)
							{
								if (piece3.color == this.color)
								{
									bool canMoveAway = false;
									List<coords> pieceMoves = getIsolatedStatePieceMoves(piece3, originalBoardState, false);
									foreach (coords move1 in pieceMoves)
									{
										if (move1.y != king.position.y)
										{
											canMoveAway = true;
										}
									}

									if (piece3.position.y != king.position.y)
									{
										botPoints += 100;
									}

									if (canMoveAway == false)
									{
										bool canMoveAwayNow = false;
										List<coords> newPieceMoves = getIsolatedStatePieceMoves(piece3, cloneState, false);
										foreach (coords move1 in pieceMoves)
										{
											if (move1.y != king.position.y)
											{
												canMoveAwayNow = true;
											}
										}
										if (canMoveAwayNow == true)
										{
											botPoints += 100;
										}
									}

								}

								else
								{
									if (piece3.alive == 0)
									{
										botPoints += 100;
									}
									List<Piece> attackers = getAttackers(this, originalBoardState, this.color * -1, piece3.position);

									if (attackers.Count == 0)
									{
										List<Piece> attackersNow = getAttackers(this, cloneState, this.color * -1, piece3.position);
										botPoints += attackersNow.Count * 20;
									}
								}
							}
						}
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

		Debug.Log("Can castle right? " + canCastleRight + ".");
        Debug.Log("Can castle left? " + canCastleLeft + ".");

        return move;
	}
}

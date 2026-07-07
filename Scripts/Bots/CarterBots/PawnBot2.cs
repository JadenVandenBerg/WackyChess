using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class PawnBot2 : BotTemplate
{
	public PawnBot2(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Pawn Bot 2.0";
		choosePieces();
	}

	private int numGuards(BotTemplate bot, BoardState bs, int color, coords coords)
	{
		int numGuards = 0;
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
				numGuards += 1;
			}
        }
		return numGuards;
	}

    private int numAttackers(BotTemplate bot, BoardState bs, int color, coords coords)
    {
        int numAttackers = 0;
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
                numAttackers += 1;
            }
        }
        return numAttackers;
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

				if (piece.baseType == "Pawn")
				{
					botPoints += 2;
				}

				List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState_, this.color);

				foreach (Piece piece_ in piecesOnBoard)
				{
					int numOfAttackers = 0;
					int numOfGuards = 0;
					if (piece_.baseType == "Pawn")
					{
						botPoints += 3;
						numOfAttackers = numAttackers(this, cloneState_, this.color, piece_.position);
						numOfGuards = numGuards(this, cloneState_, this.color, piece_.position);
						if (numOfAttackers != 0)
						{
                            botPoints += piece_.points * (numOfGuards - numOfAttackers);
                        }
						
						if (numOfGuards > numOfAttackers)
						{
                            if (this.color == 1)
                            {
                                botPoints += piece_.position.y;
                            }
                            else
                            {
                                botPoints += 9 - piece_.position.y;
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
		return move;
	}
}


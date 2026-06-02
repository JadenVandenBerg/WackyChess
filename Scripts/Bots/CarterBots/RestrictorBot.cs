using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
/*
public class RestrictorBot : BotTemplate
{
	public RestrictorBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "RestrictorBot";
		choosePieces();
	}

	/*
    private bool isGuarded(BotTemplate bot, BoardState bs, int color, int[] coords)
    {
        bool isGuarded = false;
        var attacks = getAllPossibleBotAttacks(bot, bs, color);

        foreach (var piece in attacks.pieceMoveList)
        {
            foreach (var attack in piece.moves)
            {
                if (attack == coords)
                {
                    isGuarded = true;
                }
            }
        }
        return isGuarded;
    }
	*/
/*
    override

	public NextMove nextMove()
	{
		float bestOppNumMoves = 100000;
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

			bool kingAlive = true;

            List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState, this.color);

            int[] kingCoords = null;
            foreach (Piece item in piecesOnBoard)
            {
                if (item.baseType == "King")
                {
                    kingCoords = item.position;
                }
            }

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

				if (coordsOpp == kingCoords)
				{
					kingAlive = false;
				}
            }

			/*
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

				List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState_, this.color);

				foreach (Piece piece_ in piecesOnBoard)
				{
					if (piece_.baseType == "King")
					{
						if (piece_.alive == 1)
						{
                            kingAlive = true;
                        }
					}
				}
			}
			*/


			/*
            int[] kingCoords = null;
            foreach (Piece item in piecesOnBoard)
            {
                if (item.baseType == "King")
                {
                    kingCoords = item.position;
                }
            }

            List<Piece> piecesOnBoardOpp = getPiecesOnBoardState(cloneState, this.color * -1);

            int[] kingCoordsOpp = null;
            foreach (Piece item in piecesOnBoardOpp)
            {
                if (item.baseType == "King")
                {
                    kingCoordsOpp = item.position;
                }
            }

            bool checkOpp = isGuarded(this, cloneState, this.color, kingCoordsOpp);
            bool inCheck = isGuarded(this, cloneState, this.color * -1, kingCoords);

			*//*

			int moveScore = 1000000;

			/*
			if (checkOpp == true)
			{
				moveScore -= 1000000;
			}
			*//*

			if (kingAlive == true)
			{
                moveScore = allMovesOpp.Count;
			}
			else
			{
				moveScore = 100000000;
			}

			if (bestOppNumMoves >= moveScore)
			{
				if (bestOppNumMoves > moveScore)
				{
					validMoves.Clear();
				}

				bestOppNumMoves = moveScore;
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
}*/
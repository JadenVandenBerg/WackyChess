using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BotHelperFunctions;
using static HelperFunctions;
using static UndoMoveBotHelperFunctions;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class BotThatDoesStuff : BotTemplate
{
	public BotThatDoesStuff(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Bot That Does Stuff";
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

	private bool checkIfStalemate(BoardState bs, int color)
	{
		List<Piece> piecesOnBoardOpp = getPiecesOnBoardState(bs, color);

		foreach (Piece pieceOpp in piecesOnBoardOpp)
		{
			List<NextMove> pieceMoves = getAllPossibleBotPieceMoves(bs, pieceOpp);

			foreach (NextMove move in pieceMoves)
			{
                Piece piece;
                coords coords;

                Move mv = move.move;
                piece = mv.p;
				coords = mv.coords;

				BoardState clonestate = simulatePieceMove(this, bs, piece, coords);

				coords oppKingPos = new coords(-1, -1);

				if (isolatedGetKing(clonestate, color) != null)
				{
                    oppKingPos = isolatedGetKing(clonestate, color).position;
                }
				
                bool inCheck_ = isGuarded(this, clonestate, color * -1, oppKingPos);

                if (inCheck_ == false)
                {
					return false;
                }
            }
		}

		return true;
	}

	float botPointsLastTurn = 0;
	float oppPointsLastTurn = 0;
	int movesWithoutCapture = 0;

	List<NextMove> recentMoves = new List<NextMove>();

    override

	public NextMove nextMove()
	{

        List<float> pointsOnBoardPreMove = getPointsOnBoardState(this.currentBoardState, true);
        float botPointsPreMove = this.color == 1 ? pointsOnBoardPreMove[0] : pointsOnBoardPreMove[1];
        float oppPointsPreMove = this.color == -1 ? pointsOnBoardPreMove[0] : pointsOnBoardPreMove[1];

		if (botPointsLastTurn != botPointsPreMove || oppPointsLastTurn != oppPointsPreMove)
		{
			movesWithoutCapture = 0;
		}
		else
		{
			movesWithoutCapture += 1;
		}

		botPointsLastTurn = botPointsPreMove;
		oppPointsLastTurn = oppPointsPreMove;

		Dictionary<Piece, coords> originalPositionsOpp = new Dictionary<Piece, coords>();

		List<Piece> oppPiecesPreMove = getPiecesOnBoardState(this.currentBoardState, this.color * -1);

		foreach (Piece piece in oppPiecesPreMove)
		{
			originalPositionsOpp[piece] = piece.position;
		}

		float bestMoveDiff = -100000000000;
		List<NextMove> validMoves = new List<NextMove>();
		List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

		foreach (NextMove nextMove in allMoves)
		{
			float moveAddOn = 0;
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

			Piece king = null;

			float queenValue = 0;

            coords kingPos = new coords(-1, -1);
            List<Piece> piecesOnBoard1 = getPiecesOnBoardState(this.currentBoardState, this.color);
            foreach (Piece item in piecesOnBoard1)
            {
                if (item.baseType == "King")
                {
                    kingPos = item.position;
					king = item;
                }

				if (item.baseType == "Queen")
				{
					queenValue += item.points;
				}
            }

			if (kingPos.x == -1 || king == null)
			{
				moveAddOn -= 100000;
			}

			/* Check for repetition */
			int numRepetitions = recentMoves.Count(x => x == nextMove);
			if (numRepetitions > 1)
			{
				moveAddOn -= 8;
			}

			/* Avoid moving fragile pieces, especially fragile king */
			if (checkState(piece, PieceState.Fragile))
			{
				if (piece.baseType == "King")
				{
					moveAddOn -= 100;
				}
				else
				{
					moveAddOn -= (piece.points * 0.5f);
				}
			}

			/* Leave escape squares for king */
			if (king != null)
			{
				List<coords> kingMoves = getIsolatedStatePieceMoves(king, this.currentBoardState, false);

				if (kingMoves.Count < 2)
				{
					moveAddOn -= 15;
				}
			}

			/* Push pawns if no good moves */
			if (piece.baseType == "Pawn")
			{
				moveAddOn += 0.1f;
			}

			bool oppHasBomb = false;
			float oppQueenValue = 0;
			Piece oppKing = null;
			coords kingPosOpp = new coords(-1, -1);
			List<Piece> piecesOnBoardOpp = getPiecesOnBoardState(this.currentBoardState, this.color * -1);
			foreach (Piece item in piecesOnBoardOpp)
			{
				if (item.baseType == "King")
				{
					oppKing = item;
					kingPosOpp = item.position;

					/* Reward freezing opponents king */
					if (checkState(item, PieceState.Frozen))
					{
						moveAddOn += 50;
					}
				}

				if (item.baseType == "Queen")
				{
					oppQueenValue += item.points;
				}

				if (item.collateralType >= 0)
				{
					oppHasBomb = true;
				}
				
				/* Revalue opponent oppressive pawn based on queen value */
                if (checkState(item, PieceState.Oppressive))
                {
                    moveAddOn += item.points;
                    moveAddOn -= queenValue;
                }
            }

			/* Avoid unnecessary movement of one time king */
			if (piece.baseType == "King" && piece.points == -6)
			{
				moveAddOn -= 50;
			}

			bool inCheck = isGuarded(this, this.currentBoardState, this.color * -1, kingPos);
			bool checkOpp = isGuarded(this, this.currentBoardState, this.color, kingPosOpp);

			/* Reward checks, especially if opponent has bad king */
			if (checkOpp)
			{
				if (checkState(oppKing, PieceState.Fragile) || checkState(oppKing, PieceState.Delayed) || checkState(oppKing, PieceState.Depressed) || oppKing.points == -6 || oppKing.points == -8)
				{
					moveAddOn += 4;
				}
				else
				{
					moveAddOn += 1;
				}
			}

			/* Reward moving pieces on top of opponents king (e.g. Jail Pieces) */
			if (isolatedGetPiecesOnCoordsBoardGrid(kingPosOpp.x, kingPosOpp.y, this.currentBoardState.boardGrid, false).Count > 1)
			{
				moveAddOn += 50;
			}

			/* Reward turning heartbroken king to depressed king */
            if (oppKing != null)
            {
                if (checkState(oppKing, PieceState.Heartbroken))
                {
                    moveAddOn -= 15;
                }
            }

			/* Penalize capturing electric pieces, especially with king */
            foreach (Piece oppPiece in originalPositionsOpp.Keys)
            {
				if (checkState(oppPiece, PieceState.Electric))
				{
					if (originalPositionsOpp[oppPiece].x == coords.x && originalPositionsOpp[oppPiece].y == coords.y)
					{
						if (piece.baseType == "King")
						{
							moveAddOn -= 100;
						}
						else
						{
							moveAddOn -= piece.points * 0.5f;
						}
					}
				}
            }

            foreach (Piece item in piecesOnBoard1)
			{

				/* If opponent has any explosive pieces, move defusers next to king */
				if (oppHasBomb == true)
				{
					if (checkState(item, PieceState.Defuser))
					{
                        if (Math.Abs(item.position.x - kingPos.x) <= 1 && Math.Abs(item.position.y - kingPos.y) <= 1)
                        {
                            moveAddOn += 2;
                        }
                    }
				}

				/* Attack king adjacent squares with atomic & freeze bomb pieces */
				if (item.collateralType == 0 || item.collateralType == 2)
				{
					List<Piece> targets = getAttacking(this, this.currentBoardState, this.color, item);

					foreach (Piece target in targets)
					{
                        if (Math.Abs(target.position.x - kingPosOpp.x) <= 1 && Math.Abs(target.position.y - kingPosOpp.y) <= 1)
                        {
                            moveAddOn += 2;
                        }
                    }
                }

				/* Penalize having pieces on the same square as dematerialized opponent pieces */
				foreach(Piece pieceOC in piecesOnBoardOpp)
				{
					if (checkState(pieceOC, PieceState.Dematerialized)){
						if(pieceOC.position.x == item.position.x && pieceOC.position.y == item.position.y)
						{
                            if (item.baseType == "King")
                            {
                                moveAddOn -= 10000;
                            }
                            else
                            {
                                moveAddOn -= item.points;
                            }
                        }
					}
				}

				/* Penalize leaving infinite pieces on their starting squares */
				if (item.lives == -1)
				{
					if (item.position.x == item.startSquare.x && item.position.y == item.startSquare.y)
					{
						moveAddOn -= 0.5f;
					}
				}

				/* Penalize leaving pieces in places they can be captured by stacking pieces, especially stacking king */
				List<Piece> guards = getGuards(this, this.currentBoardState, this.color * -1, item.position);

				foreach(Piece guard in guards)
				{
					if (checkState(guard, PieceState.Stacking))
					{
						if (guard.baseType == "King")
						{
							moveAddOn -= (item.points * 10);
						}
						else
						{
							moveAddOn -= item.points;
						}
					}
				}

				/* Reward capturing pieces with stacking pieces */
				if (checkState(item, PieceState.Stacking))
				{
					foreach(Piece oppPiece in originalPositionsOpp.Keys)
					{
						if (originalPositionsOpp[oppPiece].x == item.position.x && originalPositionsOpp[oppPiece].y == item.position.y)
						{
							if (item.baseType == "King")
							{
								moveAddOn += (oppPiece.points * 10);
							}
							else
							{
								moveAddOn += oppPiece.points;
							}
						}
					}
				}

				/* Move electric pieces next to opponents king */
				if (oppKing != null)
				{
					if (checkState(item, PieceState.Electric) || oppKing.collateralType == 0)
					{
						if (Math.Abs(item.position.x - kingPosOpp.x) <= 1 && Math.Abs(item.position.y - kingPosOpp.y) <= 1)
						{
							moveAddOn += 4;
						}
					}
				}

				/* Move landmine pieces next to opponents king */
				if (item.collateralType == 1)
				{
					if (Math.Abs(item.position.x - kingPosOpp.x) <= 1 && Math.Abs(item.position.y - kingPosOpp.y) <= 1)
					{
						moveAddOn += 50;
					}
				}
			}
			
            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

			if (oppKing != null)
			{
                if (checkIfStalemate(this.currentBoardState, this.color * -1))
                {
					/* if opponent is in check it's checkmate, otherwise it's stalemate */
					if (checkOpp)
					{
						moveAddOn += 5000;
					}
					else
					{
                        moveAddOn -= 100;
                    }	
                }
            }

            float bestOppMoveDiff = +1000;

			foreach (NextMove nextMoveOpp in allMovesOpp)
			{
				float moveAddOn_ = 0;
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

				Piece king_ = isolatedGetKing(this.currentBoardState, this.color);

				/* Avoid moves that allow opponent to... */
				if (king_ != null)
				{
					/* checkmate */
                    bool inCheckAfterOppMove = isGuarded(this, this.currentBoardState, this.color * -1, king_.position);

					if (!inCheck && inCheckAfterOppMove)
					{
						if (checkIfStalemate(this.currentBoardState, this.color))
						{
							moveAddOn_ -= 5000;
						}
					}

					/* jail the king */ 
                    if (checkState(king_, PieceState.Jailed))
					{
						moveAddOn_ -= 100;
					}

					/* make the king depressed */
                    if (checkState(king_, PieceState.Heartbroken))
                    {
                        moveAddOn_ += 15;
                    }

					/* freeze the king */
                    if (checkState(king_, PieceState.Frozen))
                    {
                        moveAddOn_ -= 50;
                    }

					/* put the king in check (only for bad kings) */
                    if (king_.points <= -3)
					{
                        if (inCheckAfterOppMove == true)
                        {
                            moveAddOn_ -= 5;
                        }
                    }

                    List<Piece> oppPiecesAfterOppMove = getPiecesOnBoardState(this.currentBoardState, this.color * -1);

					foreach(Piece oppPiece in oppPiecesAfterOppMove)
					{
						/* move landmine pieces next to king */
                        if (oppPiece.collateralType == 1)
                        {
                            if (Math.Abs(oppPiece.position.x - king_.position.x) <= 1 && Math.Abs(oppPiece.position.y - king_.position.y) <= 1)
                            {
                                moveAddOn_ -= 15;
                            }
                        }

						/* move Medusa next to king */
                        if (checkState(oppPiece, PieceState.Medusa))
                        {
                            if (Math.Abs(oppPiece.position.x - king_.position.x) <= 1 && Math.Abs(oppPiece.position.y - king_.position.y) <= 1)
                            {
                                moveAddOn_ -= 25;
                            }
                        }
                    }
                }


                List<Piece> piecesOnBoardAfterOppMove = getPiecesOnBoardState(this.currentBoardState, this.color);

                /* Revalue oppressive pawn based on opponent queen value */
                foreach (Piece item in piecesOnBoardAfterOppMove)
				{
                    if (checkState(item, PieceState.Oppressive))
                    {
                        moveAddOn_ -= item.points;
                        moveAddOn_ += oppQueenValue;
                    }
                }
                

                List<NextMove> allMoves2 = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

				float bestMoveDiff2 = -100000;

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

					botPoints += moveAddOn;
					botPoints += moveAddOn_;

					if (movesWithoutCapture >= 20)
					{
						oppPoints *= 100;
					}

                    if (inCheck == true)
                    {
                        botPoints -= 1000000;
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

		if (recentMoves.Count >= 5)
		{
			recentMoves.RemoveAt(0);
		}

		recentMoves.Add(move);

		return move;
	}
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static UnityEngine.GraphicsBuffer;

public class ProBot : BotTemplate
{
	public ProBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "ProBot";

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
			if (kingIndex > 0)
			{
				Piece king = sortedPieces[kingIndex];
                sortedPieces.Remove(king);
                sortedPieces.Insert(0, king);
            }
            forkValue = sortedPieces[1].points;
		}

		return forkValue;

	}

	private float tradeOutcome(List<Piece> guards, List<Piece> oppGuards, Piece ogPiece)
	{
		float tradeOutcome = ogPiece.points * -1;
		int dead;
		int deadOpp;

		if (guards.Count >= oppGuards.Count)
		{
			dead = oppGuards.Count - 1;
			deadOpp = oppGuards.Count;
		}
		else
		{
			dead = oppGuards.Count;
			deadOpp = oppGuards.Count;
		}

        List<Piece> sortedGuards = guards.OrderBy(p => p.points).ToList();
        List<Piece> sortedOppGuards = oppGuards.OrderBy(p => p.points).ToList();

		List<Piece> dead_ = sortedGuards.Take(dead).ToList();
        List<Piece> deadOpp_ = sortedGuards.Take(deadOpp).ToList();

		foreach (Piece piece in dead_)
		{
			tradeOutcome -= piece.points;
		}
        foreach (Piece piece in deadOpp_)
        {
            tradeOutcome += piece.points;
        }

		return tradeOutcome;
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

            bool inCheck = getAttackers(this, this.currentBoardState, this.color * -1, kingPos).Count > 0;

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

            float bestOppMoveDiff = +1000;
            NextMove bestOppNextMove;

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

                    /* Calculation area! */

                    List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, true);
                    float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                    float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                    if (inCheck == true)
                    {
                        botPoints -= 100;
                    }

                    bool inCheckSecondMove = getAttackers(this, this.currentBoardState, this.color * -1, kingPos).Count > 0;

                    if (inCheckSecondMove == true)
                    {
                        botPoints -= 100;
                    }

                    if (piece.baseType == "Pawn")
                    {
                        botPoints += 0.01f;
                    }

                    List<Piece> piecesOnBoard = getPiecesOnBoardState(this.currentBoardState, this.color);
                    List<Piece> piecesOnBoardOpp = getPiecesOnBoardState(this.currentBoardState, this.color * -1);

                    foreach (Piece piece1 in piecesOnBoard)
                    {
                        float pieceValue = 0;
                        List<Piece> guards = getGuards(this, this.currentBoardState, this.color, piece1.position);
                        List<Piece> attackers = getAttackers(this, this.currentBoardState, this.color, piece1.position);
                        List<Piece> attacking = getAttacking(this, this.currentBoardState, this.color, piece1);

                        if (guards.Count > 0)
                        {
                            pieceValue += 0.001f;
                            if (attackers.Count > 0)
                            {
                                if (attacking.Count > 0)
                                {
                                    float tradeValue = tradeOutcome(guards, attackers, piece1);
                                    if (tradeValue < 0)
                                    {
                                        pieceValue += tradeValue;
                                    }
                                    else
                                    {
                                        /* pieceValue += tradeValue * 0.3f; */
                                    }
                                }
                                else
                                {
                                    float tradeValue = tradeOutcome(guards, attackers, piece1);
                                    if (tradeValue < 0)
                                    {
                                        pieceValue += tradeValue;
                                    }
                                }
                            }
                            else
                            {
                                if (attacking.Count > 0)
                                {
                                    if (attacking.Count > 1)
                                    {
                                        pieceValue += getForkValue(this, this.currentBoardState, this.color, piece1);
                                    }
                                    else
                                    {
                                        List<Piece> oppGuards = getGuards(this, this.currentBoardState, this.color * -1, attacking[0].position);
                                        if (oppGuards.Count == 0)
                                        {
                                            pieceValue += attacking[0].points * 0.3f;
                                        }
                                        else
                                        {
                                            pieceValue += attacking[0].points * 0.1f;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (attackers.Count > 0)
                            {
                                pieceValue -= piece1.points;
                            }
                            else
                            {
                                if (attacking.Count > 0)
                                {
                                    if (attacking.Count > 1)
                                    {
                                        pieceValue += getForkValue(this, this.currentBoardState, this.color, piece1);
                                    }
                                    else
                                    {
                                        List<Piece> oppGuards = getGuards(this, this.currentBoardState, this.color * -1, attacking[0].position);
                                        if (oppGuards.Count == 0)
                                        {
                                            pieceValue += attacking[0].points * 0.3f;
                                        }
                                        else
                                        {
                                            pieceValue += attacking[0].points * 0.1f;
                                        }
                                    }
                                }
                            }
                        }

                        botPoints += pieceValue;

                    }

                    /* End of calculation area! */

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
	/*public NextMove nextMove()
	{
		float bestMoveValue = -10000;
		List<NextMove> validMoves = new List<NextMove>();
		List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

		foreach (NextMove nextMove in allMoves)
		{
			float moveValue = 0;
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

             Calculation area! 

            List<float> pointsOnBoard = getPointsOnBoardState(cloneState, true);
            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];
			moveValue += botPoints;
			moveValue -= oppPoints;

			if (piece.baseType == "Pawn")
			{
				moveValue += 0.01f;
			}

            List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState, this.color);
			List<Piece> piecesOnBoardOpp = getPiecesOnBoardState(cloneState, this.color * -1);

			foreach (Piece piece1 in piecesOnBoard)
			{
				float pieceValue = 0;
				List<Piece> guards = getGuards(this, cloneState, this.color, piece1.position);
                List<Piece> attackers = getAttackers(this, cloneState, this.color, piece1.position);
                List<Piece> attacking = getAttacking(this, cloneState, this.color, piece1);

				if (piece1.baseType == "King")
				{
					if (attackers.Count > 0)
					{
						pieceValue -= 50;
					}
				}

				if (guards.Count > 0)
				{
					pieceValue += 0.001f;
					if (attackers.Count > 0)
					{
						if (attacking.Count > 0)
						{
                            float tradeValue = tradeOutcome(guards, attackers, piece1);
                            if (tradeValue < 0)
                            {
                                pieceValue += tradeValue;
                            }
							else
							{
								pieceValue += tradeValue * 0.3f;
							}
                        }
						else
						{
							float tradeValue = tradeOutcome(guards, attackers, piece1);
							if (tradeValue < 0)
							{
								pieceValue += tradeValue;
							}
						}
					}
					else
					{
						if (attacking.Count > 0)
						{
							if (attacking.Count > 1)
							{
                                pieceValue += getForkValue(this, cloneState, this.color, piece1);
                            }
							else
							{
                                List<Piece> oppGuards = getGuards(this, cloneState, this.color * -1, attacking[0].position);
                                if (oppGuards.Count == 0)
                                {
                                    pieceValue += attacking[0].points * 0.3f;
                                }
                                else
                                {
                                    pieceValue += attacking[0].points * 0.1f;
                                }
                            }
						}
					}
				}
				else
				{
					if (attackers.Count > 0)
					{
						pieceValue -= piece1.points;
					}
					else
					{
                        if (attacking.Count > 0)
                        {
                            if (attacking.Count > 1)
                            {
                                pieceValue += getForkValue(this, cloneState, this.color, piece1);
                            }
                            else
                            {
                                List<Piece> oppGuards = getGuards(this, cloneState, this.color * -1, attacking[0].position);
                                if (oppGuards.Count == 0)
                                {
                                    pieceValue += attacking[0].points * 0.3f;
                                }
                                else
                                {
                                    pieceValue += attacking[0].points * 0.1f;
                                }
                            }
                        }
                    }
				}

				moveValue += pieceValue;

            }

			 End of calculation area! 

			if (moveValue >= bestMoveValue)
			{
				if (moveValue > bestMoveValue)
				{
					validMoves.Clear();
				}

				bestMoveValue = moveValue;

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
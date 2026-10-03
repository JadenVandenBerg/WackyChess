/*using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;

public class ForkBot : BotTemplate
{
	public ForkBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Fork Bot";
		choosePieces();
	}

    private List<Piece> getHangingPawns(BotTemplate bot, BoardState bs, int color)
    {
        List<Piece> hangingPawns = new List<Piece>();
        List<Piece> piecesOnBoard = getPiecesOnBoardState(bs, color);

        foreach (Piece piece in piecesOnBoard)
        {
            if (piece.baseType == "Pawn")
            {
                List<Piece> guards = getGuards(this, bs, color, piece.position);
                List<Piece> attackers = getGuards(this, bs, color * -1, piece.position);

                if (guards.Count == 0 && attackers.Count > 0)
                {
                    if (piece.points > 0)
                    {
                        hangingPawns.Add(piece);
                    }
                }
            }
        }
        return hangingPawns;
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
        bool baitSet = false;
        coords baitPosition;
        baitPosition.x = -1;
        baitPosition.y = -1;
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

            bool inCheck = getGuards(this, this.currentBoardState, this.color * -1, kingPos).Count > 0;

            List<Piece> hangingPawns = getHangingPawns(this, this.currentBoardState, this.color);

			if (hangingPawns.Count == 1)
			{
				List<Piece> hangingPawnAttackers = getGuards(this, this.currentBoardState, this.color * -1, hangingPawns[0].position);

				if (hangingPawnAttackers.Count == 1)
				{
					if (hangingPawnAttackers[0].points > 2)
					{
						baitSet = true;
                        baitPosition = hangingPawns[0].position;
					}
				}
			}

			if (baitSet == true)
			{
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
*/
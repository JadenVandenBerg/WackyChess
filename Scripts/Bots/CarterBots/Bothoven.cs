using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using System;

public class Bothoven : BotTemplate
{
	public Bothoven(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Bothoven";
		choosePieces();
	}

	List<NextMove> recentMoves = new List<NextMove>();

	public static List<int> bhNotesToPlay = new List<int>();

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
		var dict = new Dictionary<int, int>();
        var noteDict = new Dictionary<int, int>();
        var noteValueDict = new Dictionary<int, int>();

        for (int i = 1; i < 9; i++)
		{
			dict[i] = i + 7;
		}

		noteDict[1] = 1;
        noteDict[2] = 3;
        noteDict[3] = 5;
        noteDict[4] = 6;
        noteDict[5] = 8;
        noteDict[6] = 10;
        noteDict[7] = 11;
        noteDict[8] = 12;

		noteValueDict[1] = -50;
        noteValueDict[2] = 1;
        noteValueDict[3] = 3;
        noteValueDict[4] = 3;
        noteValueDict[5] = 4;
        noteValueDict[6] = 1;
        noteValueDict[7] = 5;
        noteValueDict[8] = 2;
        noteValueDict[9] = 3;
        noteValueDict[10] = 3;
        noteValueDict[11] = -50;

        float bestMoveValue = -10000;
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

            coords kingPos = new coords(-1, -1);
            List<Piece> piecesOnBoard_ = getPiecesOnBoardState(cloneState, this.color);
            foreach (Piece item in piecesOnBoard_)
            {
                if (item.baseType == "King")
                {
                    kingPos = item.position;
                }
            }

            bool inCheck = isGuarded(this, cloneState, this.color * -1, kingPos);

            float moveValue = 0;
			List<int> notes = new List<int>();
			notes.Add(coords.x - 1);

			List<int> notes_ = new List<int>();

			foreach(Piece piece1 in piecesOnBoard_)
			{
				if (piece1.position.y == coords.y && notes_.Contains(piece1.position.x) == false)
				{
					notes_.Add(piece1.position.x);
					notes.Add(dict[piece1.position.x]);
				}
			}

			float noteScore = 0;
			int numCombos = 0;

			foreach(int note in notes_)
			{
				foreach(int note_ in notes_)
				{
					if (note != note_)
					{
						int distance = Math.Abs(noteDict[note] - noteDict[note_]);

						if (note == coords.x || note_ == coords.x)
						{
							noteScore += noteValueDict[distance] * 1.5f;
						}
						else
						{
                            noteScore += noteValueDict[distance];
                        }
						numCombos += 1;
					}
				}
			}

			float averageNoteScore = noteScore / numCombos;

			moveValue = averageNoteScore;

			if (numCombos == 0)
			{
				moveValue = 0.5f;
			}

			if (notes_.Count == 3)
			{
				moveValue *= 1.5f;
			}

			if (notes_.Count() > 4)
			{
				moveValue = -500;
			}

			if (inCheck == true)
			{
				moveValue = -5000;
			}

			foreach (NextMove recentMove in recentMoves)
			{
                Piece pieceT;
                coords coordsT;
                if (recentMove.moveType == "move")
                {
                    Move mvT = recentMove.move;
                    pieceT = mvT.p;
                    coordsT = mvT.coords;
                }
                else
                {
                    PieceAbility paT = recentMove.ability;

                    pieceT = paT.piece;
                    coordsT = paT.coords;
                }

				if (pieceT.name == piece.name && coordsT.x == coords.x && coordsT.y == coords.y)
				{
                    moveValue -= 800;
                }

            }

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

		var nextMoveVars = getNextMoveVars(move);
		Piece piece2 = nextMoveVars.piece;
		coords coords2 = nextMoveVars.coords;
		string moveType2 = nextMoveVars.moveType;

		UndoMove undo;

		if (moveType2 == "move")
		{
			undo = undo_simulatePieceMove(this.currentBoardState, piece2, new coords(coords2.x, coords2.y));
		}
		else
		{
			undo = undo_simulatePieceAbility(this.currentBoardState, move.ability);
		}

		bhNotesToPlay.Clear();

		int rootNote = coords2.x - 1;
		bhNotesToPlay.Add(rootNote);

		List<Piece> piecesOnBoard = getPiecesOnBoardState(this.currentBoardState, this.color);

		foreach (Piece piece in piecesOnBoard)
		{
			if (piece.position.y == coords2.y)
			{
				int note = dict[piece.position.x];
				bhNotesToPlay.Add(note);
			}
		}

		recentMoves.Add(move);

		if (recentMoves.Count >= 3)
		{
			recentMoves.Remove(recentMoves[0]);
		}

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


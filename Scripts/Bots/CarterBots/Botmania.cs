using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static UnityEditor.Progress;
using static UnityEngine.Audio.ProcessorInstance;


public class Botmania : BotTemplate
{
	public Botmania(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Botmania";
		choosePieces();
	}

    Dictionary<string, object> testTrack1 = new Dictionary<string, object>
    {
        ["TrackName"] = "Test Track 1",
        ["Checkpoints"] = new List<string> { "42", "52" },
        ["Finish"] = "33"
    };

	Dictionary<string, float> testTrack1Ldb = new Dictionary<string, float>();

    bool checkPointsCollected = false;
	bool finishedMap = false;
	int turns = 0;
	List<coords> checkPointsAlreadyCollected = new List<coords>();

    override

	public NextMove nextMove()
	{
		turns += 1;
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

			List<float> pointsOnBoard = getPointsOnBoardState(cloneState, true);
			float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
			float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];


			float diff = botPoints - oppPoints;
			if (diff >= bestMoveDiff)
			{
				if (diff > bestMoveDiff)
				{
					validMoves.Clear();
				}

				bestMoveDiff = diff;
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

        BoardState cloneState_;
        if (move.moveType == "move")
        {
            cloneState_ = simulatePieceMove(this, this.currentBoardState, move.move.p, move.move.coords);
        }
        else
        {
            cloneState_ = simulatePieceAbility(this, this.currentBoardState, move.ability);
        }

		if (finishedMap == false)
		{
			if (checkPointsCollected == false)
			{
				checkPointsCollected = true;
				foreach (string cp in (IEnumerable<string>)testTrack1["Checkpoints"])
				{
					coords cpCoords = new coords();
					string x = cp[0].ToString();
					cpCoords.x = int.Parse(x);
                    string y = cp[1].ToString();
                    cpCoords.y = int.Parse(y);

					if (checkPointsAlreadyCollected.Contains(cpCoords) == false)
					{
                        bool onCoords = false;
                        List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(cpCoords.x - 1, cpCoords.y - 1, cloneState_.boardGrid, false);

						foreach (Piece piece in piecesOnCoords)
						{
							if (piece.color == this.color)
							{
								onCoords = true;
								checkPointsAlreadyCollected.Add(cpCoords);
							}
						}

						if (onCoords == false)
						{
							checkPointsCollected = false;
						}
                    }
                }
			}

            if (checkPointsCollected == true)
            {
				coords finCoords = new coords();
				string finish = testTrack1["Finish"].ToString();
                string x = finish[0].ToString();
                finCoords.x = int.Parse(x);
                string y = finish[1].ToString();
                finCoords.y = int.Parse(y);

                List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(finCoords.x - 1, finCoords.y - 1, cloneState_.boardGrid, false);

                foreach (Piece piece in piecesOnCoords)
                {
                    if (piece.color == this.color)
                    {
						finishedMap = true;
						Debug.Log("Track finished in " + turns + " turns.");
						testTrack1Ldb[this.name] = turns;

                    }
                }

            }
        }

        return move;
	}
}


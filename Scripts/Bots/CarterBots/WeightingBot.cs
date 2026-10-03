using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static BotHelperFunctions;

public class WeightingBot : BotTemplate
{
	public WeightingBot(int botColor)
	{
		color = botColor;
		pieces = new List<Piece>();
		name = "Weighting Bot";
		choosePieces();
	}

    List<int> weights = new List<int>();
    int turns = 0;

	public float boardControl(BotTemplate bot, BoardState bs, int color)
	{
		float boardControl;
		List<coords> controlledSquares = new List<coords>();
		var attacks = getAllTheoreticalBotAttacks(bot, bs, color);
		
		foreach (var piece in attacks.pieceMoveList)
		{
			foreach (var attack in piece.moves)
			{
				bool inList = false;
				foreach (coords square in controlledSquares)
				{
					if (square.x == attack.x && square.y == attack.y)
					{
						inList = true;
					}
				}
				if (inList == false)
				{
					controlledSquares.Add(attack);
				}
			}
		}

		boardControl = controlledSquares.Count;
		return boardControl;
	}

    private bool isGuarded(BotTemplate bot, BoardState bs, int color, coords coords)
    {
        bool isGuarded = false;
        var attacks = getAllTheoreticalBotAttacks(bot, bs, color);

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

    int turnsSinceLastMsg = 5;

    override

    public NextMove nextMove()
    {
        Dictionary<NextMove, List<float>> valuesDict = new Dictionary<NextMove, List<float>>();
        if (turns == 0)
        {
             List<int> weights_ = new List<int>();
             int remainder = 100;
             for (int i = 1; i < 4; i++)
             {
                 System.Random random = new System.Random();
                 int weight = random.Next(remainder + 1);
                 weights_.Add(weight);
                 remainder -= weight;
             }
             weights_.Add(remainder);

             System.Random rnd = new System.Random();

             List<int> sortedList = weights_.OrderBy(_ => rnd.Next()).ToList();
             foreach (int sorted in sortedList)
             {
                 weights.Add(sorted);
             }

            /* testing 
            weights[0] = 0;
            weights[1] = 0;
            weights[2] = 100;
            weights[3] = 0;
            */
        }

        if(turnsSinceLastMsg == 5)
        {
            gameData.helper.addBotMessage("Valuing " + weights[0] + "% Control, " + weights[1] + "% Points, " + weights[2] + "% Aggression, " + weights[3] + "% Defence.");
            turnsSinceLastMsg = 0;
        }
        turnsSinceLastMsg += 1;

        Dictionary<Piece, float> originalDistances = new Dictionary<Piece, float>();

        List<Piece> piecesOnBoardPreMove = getPiecesOnBoardState(this.currentBoardState, this.color);
        List<Piece> piecesOnBoardOppPreMove = getPiecesOnBoardState(this.currentBoardState, this.color * -1);

        coords kingCoordsOpp;
        kingCoordsOpp.x = 0;
        kingCoordsOpp.y = 0;

        foreach (Piece item in piecesOnBoardOppPreMove)
        {
            if (item.baseType == "King")
            {
                kingCoordsOpp = item.position;
            }
        }

        foreach (Piece oPiece in piecesOnBoardPreMove)
        {
            float distance_;
            float distanceX_ = Math.Abs(oPiece.position.x - kingCoordsOpp.x);
            float distanceY_ = Math.Abs(oPiece.position.y - kingCoordsOpp.y);
            distance_ = (float)Math.Sqrt(distanceX_ * distanceX_ + distanceY_ * distanceY_);
            originalDistances.Add(oPiece, distance_);
        }

        List<float> pointsOnBoardPreMove = getPointsOnBoardState(this.currentBoardState, true);
        float botPointsPreMove = this.color == 1 ? pointsOnBoardPreMove[0] : pointsOnBoardPreMove[1];
        float oppPointsPreMove = this.color == -1 ? pointsOnBoardPreMove[0] : pointsOnBoardPreMove[1];
        float numGuardedPreMove = 0;
        foreach (Piece item in piecesOnBoardPreMove)
        {
            bool guarded = isGuarded(this, this.currentBoardState, this.color, item.position);
            if (guarded == true)
            {
                numGuardedPreMove += 1;
            }
        }

        float preMovePoints = botPointsPreMove - oppPointsPreMove;
        float preMoveBoardControl = boardControl(this, this.currentBoardState, this.color);

        Debug.Log("Board Control: " + boardControl(this, this.currentBoardState, this.color) + ".");
		float bestMoveDiff = -1000;
		List<NextMove> validMoves = new List<NextMove>();
		List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

		var boardControlDict = new Dictionary<NextMove, float>();
		var pointsDict = new Dictionary<NextMove, float>();
		var kingDistanceDict = new Dictionary<NextMove, float>();
		var defenceDict = new Dictionary<NextMove, float>();
        var checkDict = new Dictionary<NextMove, bool>();

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

            Piece king = isolatedGetKing(this.currentBoardState, this.color);
            bool inCheck = false;

            if (king != null)
            {
                inCheck = isGuarded(this, this.currentBoardState, this.color * -1, king.position);
            }

            float distance;
            float newDistance;

            float distanceX = Math.Abs(coords.x - kingCoordsOpp.x);
            float distanceY = Math.Abs(coords.y - kingCoordsOpp.y);
            newDistance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            if (originalDistances.ContainsKey(piece))
            {
                distance = newDistance - originalDistances[piece];
            }
            else
            {
                distance = 100;
            }

			distance = distance * -1;

            List<Piece> piecesOnBoard = getPiecesOnBoardState(cloneState, this.color);

            float numGuarded = 0;
            foreach (Piece item in piecesOnBoard)
            {
                bool guarded = isGuarded(this, cloneState, this.color, item.position);
                if (guarded == true)
                {
                    numGuarded += 1;
                }
            }

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

			NextMove bestOppNextMove;
			float bestOppMoveDiff = +1000;
			float bestOppMoveBoardControl = -1;

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

				float boardControlValue = boardControl(this, cloneState_, this.color);

				float diff = botPoints - oppPoints;
				if (diff < bestOppMoveDiff)
				{
					bestOppMoveDiff = diff;
					bestOppNextMove = nextMoveOpp;
					bestOppMoveBoardControl = boardControlValue;
				}
			}

			pointsDict[nextMove] = bestOppMoveDiff;		
			boardControlDict[nextMove] = bestOppMoveBoardControl;
			kingDistanceDict[nextMove] = distance;
			defenceDict[nextMove] = numGuarded;
            checkDict[nextMove] = inCheck;

			this.currentBoardState = originalBoardState;
		}

        float bestMoveValue = -1000;

        foreach (NextMove nextMove in allMoves)
		{
            List<float> values = new List<float>();

			/* board Control */
			float boardControlValue = new float();
			float boardControlMax = boardControlDict.Values.Max();
			float boardControlMin = boardControlDict.Values.Min();

			if (boardControlDict[nextMove] == preMoveBoardControl)
			{
				boardControlValue = 0.5f;
			}
			else if (boardControlDict[nextMove] == boardControlMax)
			{
				boardControlValue = 1;
			}
            else if (boardControlDict[nextMove] == boardControlMin)
            {
                boardControlValue = 0;
            }
            else if (boardControlDict[nextMove] < boardControlMax && boardControlDict[nextMove] > preMoveBoardControl)
            {
				float difference = boardControlDict[nextMove] - preMoveBoardControl;
				float percent = difference / (boardControlMax - preMoveBoardControl);
				boardControlValue = 0.5f + (percent * 0.5f);
            }
            else if (boardControlDict[nextMove] > boardControlMin && boardControlDict[nextMove] < preMoveBoardControl)
            {
                float difference = preMoveBoardControl - boardControlDict[nextMove];
                float percent = difference / (preMoveBoardControl - boardControlMin);
                boardControlValue = 0.5f - (percent * 0.5f);
            }
            values.Add(boardControlValue);

            /* points */
            float pointsValue = new float();
            float pointsMax = pointsDict.Values.Max();
            float pointsMin = pointsDict.Values.Min();

            if (pointsDict[nextMove] == preMovePoints)
            {
                pointsValue = 0.5f;
            }
            else if (pointsDict[nextMove] == pointsMax)
            {
                pointsValue = 1;
            }
            else if (pointsDict[nextMove] == pointsMin)
            {
                pointsValue = 0;
            }
            else if (pointsDict[nextMove] < pointsMax && pointsDict[nextMove] > preMovePoints)
            {
                float difference = pointsDict[nextMove] - preMovePoints;
                float percent = difference / (pointsMax - preMovePoints);
                pointsValue = 0.5f + (percent * 0.5f);
            }
            else if (pointsDict[nextMove] > pointsMin && pointsDict[nextMove] < preMovePoints)
            {
                float difference = preMovePoints - pointsDict[nextMove];
                float percent = difference / (preMovePoints - pointsMin);
                pointsValue = 0.5f - (percent * 0.5f);
            }

            values.Add(pointsValue);

            /* kingDistance */
            float kingDistanceValue = new float();
            float kingDistanceMax = kingDistanceDict.Values.Max();
            float kingDistanceMin = kingDistanceDict.Values.Min();

            if (kingDistanceDict[nextMove] == 0)
            {
                kingDistanceValue = 0.5f;
            }
            else if (kingDistanceDict[nextMove] == kingDistanceMax)
            {
                kingDistanceValue = 1;
            }
            else if (kingDistanceDict[nextMove] == kingDistanceMin)
            {
                kingDistanceValue = 0;
            }
            else if (kingDistanceDict[nextMove] < kingDistanceMax && kingDistanceDict[nextMove] > 0)
            {
                float difference = kingDistanceDict[nextMove] - 0;
                float percent = difference / (kingDistanceMax - 0);
                kingDistanceValue = 0.5f + (percent * 0.5f);
            }
            else if (kingDistanceDict[nextMove] > kingDistanceMin && kingDistanceDict[nextMove] < 0)
            {
                float difference = 0 - kingDistanceDict[nextMove];
                float percent = difference / (0 - kingDistanceMin);
                kingDistanceValue = 0.5f - (percent * 0.5f);
            }
            values.Add(kingDistanceValue);

            /* Defence */
            float defenceValue = new float();
            float numGuardedMax = defenceDict.Values.Max();
            float numGuardedMin = defenceDict.Values.Min();

            if (defenceDict[nextMove] == numGuardedPreMove)
            {
                defenceValue = 0.5f;
            }
            else if (defenceDict[nextMove] == numGuardedMax)
            {
                defenceValue = 1;
            }
            else if (defenceDict[nextMove] == numGuardedMin)
            {
                defenceValue = 0;
            }
            else if (defenceDict[nextMove] < numGuardedMax && defenceDict[nextMove] > numGuardedPreMove)
            {
                float difference = defenceDict[nextMove] - numGuardedPreMove;
                float percent = difference / (numGuardedMax - numGuardedPreMove);
                defenceValue = 0.5f + (percent * 0.5f);
            }
            else if (defenceDict[nextMove] > numGuardedMin && defenceDict[nextMove] < numGuardedPreMove)
            {
                float difference = numGuardedPreMove - defenceDict[nextMove];
                float percent = difference / (numGuardedPreMove - numGuardedMin);
                defenceValue = 0.5f - (percent * 0.5f);
            }
            values.Add(defenceValue);

            /* Final Move Value Calculation */

            float moveValue = 0;

            int count = 0;
            foreach(float value in values)
            {
                int weight = weights[count];
                float categoryValue = value * (weight * 0.01f);
                moveValue += categoryValue;
                count += 1;
            }

            if (checkDict[nextMove] == true)
            {
                moveValue = -100;
            }

            values.Add(moveValue);
            valuesDict[nextMove] = values;

            if (moveValue >= bestMoveValue)
            {
                if (moveValue > bestMoveValue)
                {
                    validMoves.Clear();
                }

                bestMoveValue = moveValue;

                validMoves.Add(nextMove);
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

        turns += 1;

        List<float> catValues = valuesDict[move];

        Debug.Log("Category Scores: " + catValues[0] + " Board Control, " + catValues[1] + " Points, " + catValues[2] + " Aggression, " + catValues[3] + " Defence. " + catValues[4] + " Total.");

        return move;
	}
}


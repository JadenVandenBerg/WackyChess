using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.Text;

public class BottusMaximus : BotTemplate
{
	public BottusMaximus(int botColor) {
		color = botColor;
		pieces = new List<Piece>();
		name = "Bottus Maximus";

		choosePieces();
	}

	override
	public NextMove nextMove() {
		float bestMoveWeight = -1000f;
		int bestBoardControlDiff = -1000;
		NextMove bestMove = null;

		List<NextMove> validMoves = new List<NextMove>();
		List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

		List<float> startPoints = getPointsOnBoardState(this.currentBoardState, true);
		float botPoints_ = this.color == 1 ? startPoints[0] : startPoints[1];
		float oppPoints_ = this.color == -1 ? startPoints[0] : startPoints[1];

		float startingDiff = botPoints_ - oppPoints_;

		CoordsInfo[,] coordsInfo;

		//Each piece
		foreach (NextMove nextMove in allMoves) {
			Piece piece;
			coords coords;

			string moveType = nextMove.moveType;

			if (moveType == "move") {
				Move mv = nextMove.move;

				piece = mv.p;
				coords = mv.coords;
			}
			else {
				PieceAbility pa = nextMove.ability;

				piece = pa.piece;
				coords = pa.coords;
			}

			BoardState originalBoardState = this.currentBoardState;

			BoardState cloneState;
			if (moveType == "move") {
				cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
			}
			else {
				cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
			}
			this.currentBoardState = cloneState;

			List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

			//best simulated move opponent can make
			float bestOppMoveDiff = +1000f;
			int bestOppBoardControlDiff = +1000;
			BoardState bestOppBS = null;
			float bestOppAttackerValue = 1000f;
			NextMove bestOppNextMove = null;

			foreach (NextMove nextMoveOpp in allMovesOpp) {
				Piece pieceOpp;
				coords coordsOpp;

                string moveTypeOpp = nextMoveOpp.moveType;

                if (moveTypeOpp == "move")
                {
                    Move mv = nextMoveOpp.move;

                    pieceOpp = mv.p;
                    coordsOpp = mv.coords;
                }
                else // moveType == "ability" guarenteed
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

				List<int> boardControlOnBS = getBoardControlOnBoardState(cloneState_);
				int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];
				int oppBoardControl = this.color == -1 ? boardControlOnBS[0] : boardControlOnBS[1];

				List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff_ = botPoints - oppPoints;

				float attackerValue = pieceOpp.points;

				if (diff_ < bestOppMoveDiff)
				{
					bestOppMoveDiff = diff_;
					bestOppNextMove = nextMoveOpp;
					bestOppBoardControlDiff = botBoardControl - oppBoardControl;
					bestOppBS = cloneState_;
					bestOppAttackerValue = attackerValue;
				}
				else if (diff_ == bestOppMoveDiff)
				{
					if (attackerValue < bestOppAttackerValue)
					{
						bestOppBoardControlDiff = botBoardControl - oppBoardControl;
						bestOppBS = cloneState_;
						bestOppNextMove = nextMoveOpp;
						bestOppAttackerValue = attackerValue;
					}
				}
			}

			float moveWeight = 0;
			float tradeScore = 0;

			Piece p = nextMove.moveType == "move" ? nextMove.move.p : nextMove.ability.piece;
			//Piece pOpp = bestOppNextMove.moveType == "move" ? bestOppNextMove.move.p : bestOppNextMove.ability.piece;
			//int[] cOpp = bestOppNextMove.moveType == "move" ? bestOppNextMove.move.coords : bestOppNextMove.ability.coords;

			if (coords.x - 1 >= 8 || coords.y - 1 >= 8 || coords.y - 1 < 0 || coords.x - 1 < 0)
			{
				//if (nextMove.moveType == "move") Debug.Log("Analyzed move: " + p.name + " to " + coords[0] + "," + coords[1] + ". Move Weight: " + moveWeight + " Board Control: " + bestOppBoardControlDiff);
				//if (nextMove.moveType == "ability") Debug.Log("Analyzed ability: " + p.name + ": " + nextMove.ability.ability + " to " + coords[0] + "," + coords[1] + ". Move Weight: " + moveWeight + " Board Control: " + bestOppBoardControlDiff);
				//Debug.LogWarning("BottusMaximus FAIL");
				moveWeight = bestOppMoveDiff;
			}
			else
            {
				moveWeight = bestOppMoveDiff;

				if (bestOppBS != null)
				{
					/*
					coordsInfo = getPieceAttackPatterns(bestOppBS, this.color);
					CoordsInfo cInfo = coordsInfo[coords[0] - 1, coords[1] - 1];

					tradeScore = evaluateTrade(cInfo);

					moveWeight += tradeScore;
					*/

					coordsInfo = getPieceAttackPatterns(cloneState, this.color);
					CoordsInfo cInfo = coordsInfo[coords.x - 1, coords.y - 1];

					if (cInfo.colorOnSquare && cInfo.oppAttacking.Count > 0)
					{
						tradeScore = evaluateTrade(cInfo);
						moveWeight += tradeScore;
					}

					/*
					//Debug
					if (nextMove.moveType == "move")
					{
						var sb = new StringBuilder();

						sb.AppendLine("Analyzed move: " + p.name + " to " + coords[0] + "," + coords[1] + ".");
						sb.AppendLine("Opp best move: " + pOpp.name + " to " + cOpp[0] + "," + cOpp[1] + ".");
						sb.AppendLine("Move Weight: " + (moveWeight - tradeScore));
						sb.AppendLine("Trade Weight: " + tradeScore);
						sb.AppendLine("Total Weight: " + moveWeight);
						sb.AppendLine("Board Control: " + bestOppBoardControlDiff);
						sb.AppendLine("Attacking Count: " + cInfo.attacking.Count);
						sb.AppendLine("Opp Attacking Count: " + cInfo.oppAttacking.Count);

						Debug.Log(sb.ToString());
					}
					else if (nextMove.moveType == "ability")
					{
						var sb = new StringBuilder();

						sb.AppendLine("Analyzed ability: " + p.name + " to " + coords[0] + "," + coords[1] + ".");
						sb.AppendLine("Ability: " + nextMove.ability.ability);
						sb.AppendLine("Opp best move: " + pOpp.name + " to " + cOpp[0] + "," + cOpp[1] + ".");
						sb.AppendLine("Move Weight: " + (moveWeight - tradeScore));
						sb.AppendLine("Trade Weight: " + tradeScore);
						sb.AppendLine("Total Weight: " + moveWeight);
						sb.AppendLine("Board Control: " + bestOppBoardControlDiff);
						sb.AppendLine("Attacking Count: " + cInfo.attacking.Count);
						sb.AppendLine("Opp Attacking Count: " + cInfo.oppAttacking.Count);

						Debug.Log(sb.ToString());
					}
					debug_printBoardState(bestOppBS);
					*/
				}
			}

			if (moveWeight > bestMoveWeight) {
				// set move
				bestMove = nextMove;
				bestMoveWeight = moveWeight;
			}
			else if (moveWeight == bestMoveWeight) {
				//check board control
				int boardControlDiff = bestOppBoardControlDiff;

				if (boardControlDiff > bestBoardControlDiff)
                {
					bestBoardControlDiff = boardControlDiff;
					bestMove = nextMove;
                }
			}

			this.currentBoardState = originalBoardState;
		}

		if (bestMove.moveType == "move")
        {
            bestMove.move.p = getOriginalPieceFromClone(bestMove.move.p);
        }
        else
        {
            bestMove.ability.piece = getOriginalPieceFromClone(bestMove.ability.piece);
        }
        return bestMove;
	}

	private struct CoordsInfo {
		public CoordsInfo (int x_, int y_) {
			x = x_;
			y = y_;
			points = 0;
			oppPoints = 0;
			attackingPoints = 0;
			oppAttackingPoints = 0;
			piecesOnCoords = new List<Piece>();
			attacking = new List<Piece>();
			oppAttacking = new List<Piece>();
			colorOnSquare = false;
		}

		public CoordsInfo (int x_, int y_, List<Piece> piecesOnCoords_, List<Piece> attacking_, List<Piece> oppAttacking_) {
			x = x_;
			y = y_;
			points = 0;
			oppPoints = 0;
			attackingPoints = 0;
			oppAttackingPoints = 0;
			piecesOnCoords = piecesOnCoords_;
			attacking = attacking_;
			oppAttacking = oppAttacking_;
			colorOnSquare = false;
		}

		public int x { get; set; }
		public int y { get; set; }
		public float points { get; set; }
		public float oppPoints { get; set; }
		public float attackingPoints { get; set; }
		public float oppAttackingPoints { get; set; }
		public List<Piece> piecesOnCoords { get; set; }
		public List<Piece> attacking { get; set; }
		public List<Piece> oppAttacking { get; set; }
		public bool colorOnSquare { get; set; }
	}

	private CoordsInfo[,] getPieceAttackPatterns(BoardState bs, int color)
	{
		CoordsInfo[,] coordsInfo = new CoordsInfo[8, 8];

		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				coordsInfo[i, j] = new CoordsInfo(i, j);

				List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(i, j, bs.boardGrid, false);
				if (pieces != null)
				{
					coordsInfo[i, j].piecesOnCoords.AddRange(pieces);

					foreach (Piece p in pieces)
					{
						if (p.color == color)
                        {
							coordsInfo[i, j].points += p.points;
							coordsInfo[i, j].colorOnSquare = true;
						}
						else
                        {
							coordsInfo[i, j].oppPoints += p.points;
						}
					}
				}
			}
		}

		var whiteMoves = getAllTheoreticalBotAttacks(this, bs, 1).pieceMoveList;
		var blackMoves = getAllTheoreticalBotAttacks(this, bs, -1).pieceMoveList;

		List<PieceMoveList> allMoves = new List<PieceMoveList>();
		allMoves.AddRange(whiteMoves);
		allMoves.AddRange(blackMoves);

		foreach (PieceMoveList pml in allMoves)
		{
			Piece piece = pml.piece;

			foreach (coords coords in pml.moves)
			{
				int x = coords.x - 1;
				int y = coords.y - 1;

				CoordsInfo c = coordsInfo[x, y];

				bool isBotPiece = piece.color == color;

				List<Piece> targetList = isBotPiece ? c.attacking : c.oppAttacking;

				if (!HelperFunctions.pieceInList(targetList, piece))
				{
					targetList.Add(piece);
				}

				if (isBotPiece)
                {
					c.attackingPoints += piece.points;
				}
				else
                {
					c.oppAttackingPoints += piece.points;
				}
			}
		}

		return coordsInfo;
	}

	private List<int> getBoardControlOnBoardState(BoardState bs)
	{
		coords oppKingPos = BotHelperFunctions.filterPieces("King", this.opponentPieces)[0].position;
		coords kingPos = this.king.position;

		coords whiteKingPos = this.color == 1 ? kingPos : oppKingPos;
		coords blackKingPos = this.color == -1 ? kingPos : oppKingPos;

		List<int> boardControl = new List<int>();

		var botMovesWhite = BotHelperFunctions.getAllPossibleBotMoves(this, bs, 1);
		List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

		var botMovesBlack = BotHelperFunctions.getAllPossibleBotMoves(this, bs, -1);
		List<PieceMoveList> listBotBlackMoves = botMovesBlack.pieceMoveList;

		List<coords> uniqueCoords = new List<coords>();
		float score = 0;
		foreach (PieceMoveList pml in listBotWhiteMoves)
		{
			Piece piece = pml.piece;
			List<coords> _mL = pml.moves;

			foreach (coords coords in _mL)
			{
				if (!HelperFunctions.coordsInList(uniqueCoords, coords))
				{
					uniqueCoords.Add(coords);
					score += 8 - Mathf.Abs(coords.x - blackKingPos.x);
					score += 8 - Mathf.Abs(coords.y - blackKingPos.y);

					if (coords.x == blackKingPos.x && coords.y == blackKingPos.y)
					{
						score += 12;
					}
				}
			}
		}
		boardControl.Add((int)score);

		uniqueCoords = new List<coords>();
		score = 0;
		foreach (PieceMoveList pml in listBotBlackMoves)
		{
			Piece piece = pml.piece;
			List<coords> _mL = pml.moves;

			foreach (coords coords in _mL)
			{
				if (!HelperFunctions.coordsInList(uniqueCoords, coords))
				{
					uniqueCoords.Add(coords);
					score += 8 - Mathf.Abs(coords.x - whiteKingPos.x);
					score += 8 - Mathf.Abs(coords.y - whiteKingPos.y);

					if (coords.x == whiteKingPos.x && coords.y == whiteKingPos.y)
					{
						score += 12;
					}
				}
			}
		}
		boardControl.Add((int)score);

		return boardControl;
	}

	private float evaluateTrade(CoordsInfo cInfo)
	{
		List<float> attackers = cInfo.attacking.Select(x => x.points).OrderBy(x => x).ToList();
		List<float> oppAttackers = cInfo.oppAttacking.Select(x => x.points).OrderBy(x => x).ToList();

		bool botOwnsSquare = cInfo.points > 0;
		bool oppOwnsSquare = cInfo.oppPoints > 0;

		if (!botOwnsSquare && !oppOwnsSquare)
        {
			return 0;
		}

		float squarePoints = botOwnsSquare ? cInfo.points : cInfo.oppPoints;

		List<float> gain = new List<float>();

		gain.Add(-squarePoints);

		int i = 0;
		bool botTurn = true;

		while (true)
		{
			if (botTurn)
			{
				if (attackers.Count == 0) break;

				float attacker = attackers[0];
				attackers.RemoveAt(0);

				gain.Add(squarePoints - gain[i]);
				squarePoints = attacker;
			}
			else
			{
				if (oppAttackers.Count == 0) break;

				float attacker = oppAttackers[0];
				oppAttackers.RemoveAt(0);

				gain.Add(squarePoints - gain[i]);
				squarePoints = attacker;
			}

			botTurn = !botTurn;
			i++;
		}

		for (int j = gain.Count - 2; j >= 0; j--)
		{
			gain[j] = Mathf.Max(gain[j], -gain[j + 1]);
		}

		return gain[0];
	}
}
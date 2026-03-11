using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

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
		NextMove bestMove;

		List<NextMove> validMoves = new List<NextMove>();
		List<NextMove> allMoves = getAllPossibleMovesAndAbilities(this, this.currentBoardState, this.color);

		List<float> startPoints = getPointsOnBoardState(this.currentBoardState, true);
		float botPoints_ = this.color == 1 ? startPoints[0] : startPoints[1];
		float oppPoints_ = this.color == -1 ? startPoints[0] : startPoints[1];

		float startingDiff = botPoints_ - oppPoints_;

		List<CoordsInfo>[,] coordsInfo = getPieceAttackPatterns(this.currentBoardState, this.color);

		//Each piece
		foreach (NextMove nextMove in allMoves) {
			Piece piece;
			int[] coords;

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
			this.currentBoardState;

			List<NextMove> allMovesOpp = getAllPossibleMovesAndAbilities(this, cloneState, this.color * -1);

			//best simulated move opponent can make
			float bestOppMoveDiff = +1000f;

			foreach(NextMove nextMoveOpp in allMovesOpp) {
				Piece pieceOpp;
                int[] coordsOpp;

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

                List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff_ = botPoints - oppPoints;
                if (diff_ < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff_;
                }
			}

			CoordsInfo cInfo = coordsInfo[coords[0] - 1, coords[1] - 1];

			float moveWeight = bestOppMoveDiff;
			if (cInfo.points > 0) {
				moveWeight -= cInfo.points;
			}
			else if (cInfo.oppPoints > 0) {
				//worth looking for
				if (cInfo.attacking.Count > cInfo.oppAttacking.Count) {
					if (cInfo.attackingPoints < cInfo.oppAttackingPoints) {
						//If this is true, disregard points lost

						moveWeight = cInfo.oppPoints;
					}
				}
			}

			if (moveWeight > bestMoveWeight) {
				// set move
			}
			else if (moveWeight == bestMoveWeight) {
				//check board control
			}

			this.currentBoardState = originalBoardState;
		}

		if (bestMove.moveType == "move")
        {
            bestMove.move.p = getOriginalPieceFromClone(move.move.p);
        }
        else
        {
            bestMove.ability.piece = getOriginalPieceFromClone(move.ability.piece);
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
	}

	private List<CoordsInfo>[,] getPieceAttackPatterns(BoardState bs, int color) {

		List<CoordsInfo>[,] coordsInfo = new List<CoordsInfo>[8, 8];

		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				coordsInfo[i, j] = new CoordsInfo(i, j);

				List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(i, j, bs.boardGrid, false);
				if (pieces != null) {
					coordsInfo[i, j].piecesOnCoords.AddRange(pieces);

					for (Piece p in pieces) {
						if (p.color == color) {
							coordsInfo[i, j].points += p.points;
						}
						else {
							coordsInfo[i, j].oppPoints += p.points;
						}
					}
				}
			}
		}

		var botMovesWhite = BotHelperFunctions.getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = BotHelperFunctions.getAllPossibleBotMoves(this, bs, -1);
        List<PieceMoveList> listBotBlackMoves = botMovesBlack.pieceMoveList;

        foreach (PieceMoveList pml in listBotWhiteMoves)
        {
            Piece piece = pml.piece;
            List<int[]> _mL = pml.moves;

            foreach (int[] coords in _mL)
            {
            	List<Piece> theList = coordsInfo[coords[0] - 1, coords[1] - 1].attacking;
            	if (color == -1) {
            		theList = coordsInfo[coords[0] - 1, coords[1] - 1].oppAttacking;
            	}

                if (!HelperFunctions.pieceInList(theList), piece))
                {
                    coordsInfo[coords[0] - 1, coords[1] - 1].theList.Add(piece);
                }

                if (color == 1) {
                	attackingPoints += piece.points;
                }
                else {
                	oppAttackingPoints += piece.points;
                }
            }
        }

        foreach (PieceMoveList pml in listBotBlackMoves)
        {
            Piece piece = pml.piece;
            List<int[]> _mL = pml.moves;

            foreach (int[] coords in _mL)
            {
                List<Piece> theList = coordsInfo[coords[0] - 1, coords[1] - 1].attacking;
            	if (color == 1) {
            		theList = coordsInfo[coords[0] - 1, coords[1] - 1].oppAttacking;
            	}

                if (!HelperFunctions.pieceInList(theList), piece))
                {
                    coordsInfo[coords[0] - 1, coords[1] - 1].theList.Add(piece);
                }

                if (color == -1) {
                	attackingPoints += piece.points;
                }
                else {
                	oppAttackingPoints += piece.points;
                }
            }
        }

        return coordsInfo;
	}
}
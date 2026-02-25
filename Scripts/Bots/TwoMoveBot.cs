using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

class MoveState {
	public BoardState bs;
	public int moveIter = 0;
	public float diff = 0;
	public Piece leadingPiece;
	public int[] leadingCoords = new int[] { };

	public MoveState(BoardState bs, int moveIter, float diff, Piece leadingPiece, int[] leadingCoords) {
		this.bs = bs;
		this.moveIter = moveIter;
		this.diff = diff;
		this.leadingPiece = leadingPiece;
		this.leadingCoords = leadingCoords;
	}
}

public class TwoMoveBot : BotTemplate
{
	public TwoMoveBot(int botColor) {
		color = botColor;
		pieces = new List<Piece>();
		name = "Two Move Bot";

		choosePieces();
	}

	override
    public Dictionary<Piece, int[]> nextMove()
    {
        List<MoveState> moveStates = new List<MoveState>();
        BotHelperFunctions.resetPiecePositions(null, gameData.boardGrid);
        this.currentBoardState = BotHelperFunctions.copyBoardState(this.currentBoardState);

        var botMovesCLONE = BotHelperFunctions.getAllPossibleBotMoves(this, this.currentBoardState, this.color);

        List<float> startPoints = BotHelperFunctions.getPointsOnBoardState(this.currentBoardState, true);
        float botPoints = this.color == 1 ? startPoints[0] : startPoints[1];
        float oppPoints = this.color == -1 ? startPoints[0] : startPoints[1];

        float startingDiff = botPoints - oppPoints;

        List<Dictionary<Piece, List<int[]>>> allMovesCLONE = botMovesCLONE.pieceMoveList;
        List<Move> validMoves = new List<Move>();

        //Each piece
        foreach (Dictionary<Piece, List<int[]>> movePair in allMovesCLONE) {
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = movePair.First();
            Piece piece = pieceMovesKeyVal.Key;
            List<int[]> _mL = pieceMovesKeyVal.Value;

            //Loop through moves
            foreach(int[] coords in _mL) {
                BotHelperFunctions.resetPiecePositions(null, this.currentBoardState.boardGrid);
                BoardState originalBoardState = this.currentBoardState;
                BoardState cloneState = BotHelperFunctions.copyBoardState(this.currentBoardState);
                BotHelperFunctions.simulatePieceMove(this, cloneState, piece, coords);

                this.currentBoardState = cloneState;
                var botMovesOpp = BotHelperFunctions.getAllPossibleBotMoves(this, cloneState, this.color * -1);
                List<Dictionary<Piece, List<int[]>>> allMovesOpp = botMovesOpp.pieceMoveList;

                //best simulated move opponent can make
                float bestOppMoveDiff = +1000;
                BoardState bestMoveOppBS = null;

                foreach(Dictionary<Piece, List<int[]>> movePairOpp in allMovesOpp) {
                    KeyValuePair<Piece, List<int[]>> pieceMovesKeyValOpp = movePairOpp.First();
                    Piece pieceOpp = pieceMovesKeyValOpp.Key;
                    List<int[]> _mLOpp = pieceMovesKeyValOpp.Value;

                    foreach(int[] coordsOpp in _mLOpp) {
                        BotHelperFunctions.resetPiecePositions(null, this.currentBoardState.boardGrid);
                        BoardState originalBoardState_ = this.currentBoardState;
                        BoardState cloneState_ = BotHelperFunctions.copyBoardState(this.currentBoardState);
                        BotHelperFunctions.simulatePieceMove(this, cloneState_, pieceOpp, coordsOpp);

                        this.currentBoardState = originalBoardState_;

                        List<float> pointsOnBoard = BotHelperFunctions.getPointsOnBoardState(cloneState_, true);
                        float botPoints_ = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints_ = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        float diff = botPoints_ - oppPoints_;
                        if (diff < bestOppMoveDiff) {
                            bestOppMoveDiff = diff;
                            bestMoveOppBS = cloneState_;
                        }
                    }
                }

                float realDiff = startingDiff - bestOppMoveDiff;

                MoveState ms = new MoveState(bestMoveOppBS, 1, realDiff, piece, coords);
                moveStates.Add(ms);

                this.currentBoardState = originalBoardState;
            }
        }

        float bestDiff = -100;
        List<MoveState> bestMoveStates = new List<MoveState>();

        while (moveStates.Count > 0) {
        	MoveState next = moveStates[0];
        	moveStates.Remove(next);

        	if (next.moveIter >= 2) {
        		if (next.diff >= bestDiff) {
        			if (next.diff > bestDiff) {
        				bestMoveStates.Clear();
        			}

        			bestMoveStates.Add(next);
        			bestDiff = next.diff;
        		}
        	}

        	if (next.diff < -1 || next.moveIter >= 2) {
        		continue;
        	}

        	BotHelperFunctions.resetPiecePositions(null, next.bs.boardGrid);
        	BoardState clone = BotHelperFunctions.copyBoardState(next.bs);

        	var allBotMoves = BotHelperFunctions.getAllPossibleBotMoves(this, clone, this.color);
            List<Dictionary<Piece, List<int[]>>> allBotMoves_ = allBotMoves.pieceMoveList;

            foreach (Dictionary<Piece, List<int[]>> movePair in allBotMoves_) {
                KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal_ = movePair.First();
                Piece piece_ = pieceMovesKeyVal_.Key;
                List<int[]> mL__ = pieceMovesKeyVal_.Value;

                foreach (int[] coords in mL__) {
                    BotHelperFunctions.resetPiecePositions(null, clone.boardGrid);
                    BoardState cloneState = BotHelperFunctions.copyBoardState(next.bs);
                    BotHelperFunctions.simulatePieceMove(this, cloneState, piece_, coords);

                    var botMovesOpp_ = BotHelperFunctions.getAllPossibleBotMoves(this, cloneState, this.color * -1);
                    List<Dictionary<Piece, List<int[]>>> allMovesOpp_ = botMovesOpp_.pieceMoveList;

                    BoardState bestMoveOppBS = null;
                    float bestOppMoveDiff = +1000;

                    foreach (Dictionary<Piece, List<int[]>> movePairOpp in allMovesOpp_) {
                        KeyValuePair<Piece, List<int[]>> pieceMovesKeyValOpp = movePairOpp.First();
                        Piece pieceOpp = pieceMovesKeyValOpp.Key;
                        List<int[]> _mLOpp = pieceMovesKeyValOpp.Value;

                        foreach (int[] coordsOpp in _mLOpp) {
                            BotHelperFunctions.resetPiecePositions(null, cloneState.boardGrid);
                            BoardState cloneState_ = BotHelperFunctions.copyBoardState(cloneState);
                            BotHelperFunctions.simulatePieceMove(this, cloneState_, pieceOpp, coordsOpp);

                            List<float> pointsOnBoard = BotHelperFunctions.getPointsOnBoardState(cloneState_, true);
                            float botPoints_ = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                            float oppPoints_ = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                            float diff = botPoints_ - oppPoints_;
                            if (diff < bestOppMoveDiff) {
                                bestOppMoveDiff = diff;
                                bestMoveOppBS = cloneState_;
                            }
                        }
                    }

                    float realDiff = startingDiff - bestOppMoveDiff;

                    MoveState ms = new MoveState(bestMoveOppBS, next.moveIter + 1, realDiff, next.leadingPiece, next.leadingCoords);
                    moveStates.Add(ms);
                }
            }
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(bestMoveStates.Count);

        Piece sendP = bestMoveStates[rndIdx].leadingPiece;
        int[] sendC = bestMoveStates[rndIdx].leadingCoords;

        Move sendMove = new Move(sendP, sendC);

        Debug.Log("SENDING MOVE: " + sendMove.p.name + " to " + sendMove.coords[0] + "," + sendMove.coords[1]);
        Dictionary<Piece, int[]> moveDict = new Dictionary<Piece, int[]>();
        moveDict.Add(sendMove.p, sendMove.coords);

        return moveDict;
    }
}
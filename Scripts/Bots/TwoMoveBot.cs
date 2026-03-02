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

        List<float> startPoints = BotHelperFunctions.getPointsOnBoardState(this.currentBoardState, true);
        float botPoints = this.color == 1 ? startPoints[0] : startPoints[1];
        float oppPoints = this.color == -1 ? startPoints[0] : startPoints[1];

        float startingDiff = botPoints - oppPoints;

        MoveState ogMoveState = new MoveState(this.currentBoardState, 0, 0, null, null);
        moveStates.Add(ogMoveState);

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

        	if (next.diff < -2 || next.moveIter >= 2) {
        		continue;
        	}

        	//BotHelperFunctions.resetPiecePositions(null, next.bs.boardGrid);
        	//BoardState clone = BotHelperFunctions.copyBoardState(next.bs);

        	var allBotMoves = BotHelperFunctions.getAllPossibleBotMoves(this, this.currentBoardState, this.color);
            List<Dictionary<Piece, List<int[]>>> allBotMoves_ = allBotMoves.pieceMoveList;

            foreach (Dictionary<Piece, List<int[]>> movePair in allBotMoves_) {
                KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal_ = movePair.First();
                Piece piece_ = pieceMovesKeyVal_.Key;
                List<int[]> mL__ = pieceMovesKeyVal_.Value;

                foreach (int[] coords in mL__) {
                    //BotHelperFunctions.resetPiecePositions(null, clone.boardGrid);
                    //BoardState cloneState = BotHelperFunctions.copyBoardState(next.bs);
                    BoardState cloneState = BotHelperFunctions.simulatePieceMove(this, next.bs, piece_, coords);

                    var botMovesOpp_ = BotHelperFunctions.getAllPossibleBotMoves(this, cloneState, this.color * -1);
                    List<Dictionary<Piece, List<int[]>>> allMovesOpp_ = botMovesOpp_.pieceMoveList;

                    BoardState bestMoveOppBS = null;
                    float bestOppMoveDiff = +1000;

                    foreach (Dictionary<Piece, List<int[]>> movePairOpp in allMovesOpp_) {
                        KeyValuePair<Piece, List<int[]>> pieceMovesKeyValOpp = movePairOpp.First();
                        Piece pieceOpp = pieceMovesKeyValOpp.Key;
                        List<int[]> _mLOpp = pieceMovesKeyValOpp.Value;

                        foreach (int[] coordsOpp in _mLOpp) {
                            //BotHelperFunctions.resetPiecePositions(null, cloneState.boardGrid);
                            //BoardState cloneState_ = BotHelperFunctions.copyBoardState(cloneState);
                            BoardState cloneState_ = BotHelperFunctions.simulatePieceMove(this, cloneState, pieceOpp, coordsOpp);

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

                    MoveState ms;
                    if (next.moveIter == 0)
                    {
                        ms = new MoveState(bestMoveOppBS, next.moveIter + 1, realDiff, piece_, coords);
                    }
                    else
                    {
                        ms = new MoveState(bestMoveOppBS, next.moveIter + 1, realDiff, next.leadingPiece, next.leadingCoords);
                    }
                    moveStates.Add(ms);
                }
            }
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(bestMoveStates.Count);

        Piece sendP = BotHelperFunctions.getOriginalPieceFromClone(bestMoveStates[rndIdx].leadingPiece);
        int[] sendC = bestMoveStates[rndIdx].leadingCoords;

        Move sendMove = new Move(sendP, sendC);

        Debug.Log("SENDING MOVE: " + sendMove.p.name + " to " + sendMove.coords[0] + "," + sendMove.coords[1]);
        Dictionary<Piece, int[]> moveDict = new Dictionary<Piece, int[]>();
        moveDict.Add(sendMove.p, sendMove.coords);

        return moveDict;
    }
}
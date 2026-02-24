using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

class Move
{
    public Piece p;
    public int[] coords;

    public Move(Piece piece, int[] coords)
    {
        this.p = piece;
        this.coords = coords;
    }
}

public class OneMoveBot : BotTemplate
{
    public OneMoveBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "One Move Bot";

        choosePieces();
    }

    override
    public Dictionary<Piece, int[]> nextMove()
    {
        Piece bestMovePiece;
        int[] bestMoveCoords;
        float bestMoveDiff = -1000;

        BotHelperFunctions.resetPiecePositions(null, gameData.boardGrid);
        this.currentBoardState = BotHelperFunctions.copyBoardState(this.currentBoardState);

        var botMovesCLONE = BotHelperFunctions.getAllPossibleBotMoves(this, this.currentBoardState, this.color);

        List<Dictionary<Piece, List<int[]>>> allMovesCLONE = botMovesCLONE.pieceMoveList;
        List<Move> validMoves = new List<Move>();

        //Each piece
        foreach (Dictionary<Piece, List<int[]>> movePair in allMovesCLONE) {
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = movePair.First();
            Piece piece = pieceMovesKeyVal.Key;
            Piece realPiece = BotHelperFunctions.getOriginalPieceFromClone(piece);
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
                Piece bestOppMovePiece;
                int[] bestOppMoveCoords;
                float bestOppMoveDiff = +1000;

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
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        float diff = botPoints - oppPoints;
                        if (diff < bestOppMoveDiff) {
                            bestOppMoveDiff = diff;
                            bestOppMovePiece = pieceOpp;
                            bestOppMoveCoords = coordsOpp;
                        }
                    }
                }

                // Take the best outcome assuming the opponent captures the highest value piece it can
                if (bestOppMoveDiff >= bestMoveDiff) {
                    if (bestOppMoveDiff > bestMoveDiff)
                    {
                        validMoves.Clear();
                    }

                    bestMoveDiff = bestOppMoveDiff;
                    bestMoveCoords = coords;
                    bestMovePiece = realPiece;

                    validMoves.Add(new Move(bestMovePiece, bestMoveCoords));
                }

                this.currentBoardState = originalBoardState;
            }
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        Move sendMove = validMoves[rndIdx];

        Debug.Log("SENDING MOVE: " + sendMove.p.name + " to " + sendMove.coords[0] + "," + sendMove.coords[1]);
        Dictionary<Piece, int[]> moveDict = new Dictionary<Piece, int[]>();
        moveDict.Add(sendMove.p, sendMove.coords);

        return moveDict;
    }
}

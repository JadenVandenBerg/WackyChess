using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class IdiotBot : BotTemplate
{
    public IdiotBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Idiot";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        Piece worstMovePiece = null;
        int[] worstMoveCoords = null;
        float worstMoveDiff = +1000;

        BoardState ogBoardState = this.currentBoardState;

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
                //BotHelperFunctions.resetPiecePositions(null, this.currentBoardState.boardGrid);
                BoardState originalBoardState = this.currentBoardState;
                //BoardState cloneState = BotHelperFunctions.copyBoardState(this.currentBoardState);
                BoardState cloneState = BotHelperFunctions.simulatePieceMove(this, this.currentBoardState, piece, coords);

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
                        //BotHelperFunctions.resetPiecePositions(null, this.currentBoardState.boardGrid);
                        BoardState originalBoardState_ = this.currentBoardState;
                        //BoardState cloneState_ = BotHelperFunctions.copyBoardState(this.currentBoardState);
                        BoardState cloneState_ = BotHelperFunctions.simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);

                        this.currentBoardState = originalBoardState_;

                        List<float> pointsOnBoard = BotHelperFunctions.getPointsOnBoardState(cloneState_, false);
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

                // Take the worst outcome assuming the opponent captures the highest value piece it can
                if (bestOppMoveDiff <= worstMoveDiff) {
                    if (bestOppMoveDiff < worstMoveDiff)
                    {
                        validMoves.Clear();
                    }

                    worstMoveDiff = bestOppMoveDiff;
                    worstMoveCoords = coords;
                    worstMovePiece = realPiece;

                    validMoves.Add(new Move(worstMovePiece, worstMoveCoords));
                }

                this.currentBoardState = originalBoardState;
            }
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        Move sendMove = validMoves[rndIdx];

        Debug.Log("SENDING MOVE: " + sendMove.p.name + " to " + sendMove.coords[0] + "," + sendMove.coords[1]);
        //Dictionary<Piece, int[]> moveDict = new Dictionary<Piece, int[]>();
        //moveDict.Add(sendMove.p, sendMove.coords);

        NextMove move = new NextMove(sendMove);

        return move;
    }
}

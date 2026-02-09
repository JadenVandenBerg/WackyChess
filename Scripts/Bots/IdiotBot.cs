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
    public Dictionary<Piece, int[]> nextMove()
    {
        Piece worstMovePiece = null;
        int[] worstMoveCoords = null;
        float worstMoveDiff = +1000;

        var botMoves = BotHelperFunctions.getAllPossibleBotMoves(this, this.currentBoardState, this.color);

        List<Dictionary<Piece, List<int[]>>> allMoves = botMoves.pieceMoveList;
        //Dictionary<Piece, List<string>> allAbilities = botMoves.piecesAbilities;

        //Each piece
        foreach(Dictionary<Piece, List<int[]>> movePair in allMoves) {
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = movePair.First();
            Piece piece = pieceMovesKeyVal.Key;
            List<int[]> _mL = pieceMovesKeyVal.Value;

            //Loop through moves
            foreach(int[] coords in _mL) {                
                BoardState originalBoardState = this.currentBoardState;
                BoardState cloneState = BotHelperFunctions.copyBoardState(this.currentBoardState);
                BotHelperFunctions.movePieceBoardState(piece, coords, cloneState);

                //Simulate all opponent moves
                //getallpossiblebotmoves uses bot.currentBoardState
                this.currentBoardState = cloneState;
                var botMovesOpp = BotHelperFunctions.getAllPossibleBotMoves(this, cloneState, this.color * -1);
                List<Dictionary<Piece, List<int[]>>> allMovesOpp = botMovesOpp.pieceMoveList;
                //Dictionary<Piece, List<string>> allAbilitiesOpp = botMovesOpp.piecesAbilities;

                //best simulated move opponent can make
                Piece bestOppMovePiece;
                int[] bestOppMoveCoords;
                float bestOppMoveDiff = +1000;

                foreach(Dictionary<Piece, List<int[]>> movePairOpp in allMovesOpp) {
                    KeyValuePair<Piece, List<int[]>> pieceMovesKeyValOpp = movePairOpp.First();
                    Piece pieceOpp = pieceMovesKeyValOpp.Key;
                    List<int[]> _mLOpp = pieceMovesKeyValOpp.Value;

                    foreach(int[] coordsOpp in _mLOpp) {
                        BoardState originalBoardState_ = this.currentBoardState;
                        BoardState cloneState_ = BotHelperFunctions.copyBoardState(this.currentBoardState);
                        BotHelperFunctions.movePieceBoardState(piece, coords, cloneState_);

                        List<float> pointsOnBoard = BotHelperFunctions.getPointsOnBoardState(cloneState_);
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
                if (bestOppMoveDiff < worstMoveDiff) {
                    worstMoveDiff = bestOppMoveDiff;
                    worstMoveCoords = coords;
                    worstMovePiece = piece;
                }
            }
        }

        Dictionary<Piece, int[]> moveDict = new Dictionary<Piece, int[]>();
        moveDict.Add(worstMovePiece, worstMoveCoords);

        return moveDict;
    }
}

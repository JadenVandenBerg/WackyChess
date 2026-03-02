using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Bloodbot : BotTemplate
{
    public Bloodbot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Bloodbot";

        choosePieces();
    }

    override
    public Dictionary<Piece, int[]> nextMove()
    {
        Piece bestMovePiece;
        int[] bestMoveCoords;
        int bestBoardControlDiff = -1000;
        float bestMoveDiff = -1000;

        BotHelperFunctions.resetPiecePositions(null, gameData.boardGrid);
        this.currentBoardState = BotHelperFunctions.copyBoardState(this.currentBoardState);

        List<float> startPoints = BotHelperFunctions.getPointsOnBoardState(this.currentBoardState, true);
        float botPoints_ = this.color == 1 ? startPoints[0] : startPoints[1];
        float oppPoints_ = this.color == -1 ? startPoints[0] : startPoints[1];

        float startingDiff = botPoints_ - oppPoints_;

        var botMovesCLONE = BotHelperFunctions.getAllPossibleBotMoves(this, this.currentBoardState, this.color);

        List<Dictionary<Piece, List<int[]>>> allMovesCLONE = botMovesCLONE.pieceMoveList;
        List<Move> validMoves = new List<Move>();
        bool positivePts = false;

        //Each piece
        foreach (Dictionary<Piece, List<int[]>> movePair in allMovesCLONE)
        {
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = movePair.First();
            Piece piece = pieceMovesKeyVal.Key;
            Piece realPiece = BotHelperFunctions.getOriginalPieceFromClone(piece);
            List<int[]> _mL = pieceMovesKeyVal.Value;

            //Loop through moves
            foreach (int[] coords in _mL)
            {
                BoardState originalBoardState = this.currentBoardState;
                BoardState cloneState = BotHelperFunctions.simulatePieceMove(this, this.currentBoardState, piece, coords);

                //if (this.color == 1) Debug.LogWarning("Analyzing Move: " + piece.name + " to " + coords[0] + "," + coords[1]);
                //if (this.color == 1) BotHelperFunctions.debug_printBoardState(cloneState);

                this.currentBoardState = cloneState;
                var botMovesOpp = BotHelperFunctions.getAllPossibleBotMoves(this, cloneState, this.color * -1);
                List<Dictionary<Piece, List<int[]>>> allMovesOpp = botMovesOpp.pieceMoveList;

                //best simulated move opponent can make
                Piece bestOppMovePiece;
                int[] bestOppMoveCoords;
                float bestOppMoveDiff = +1000;
                int bestOppBoardControlDiff = +1000;

                foreach (Dictionary<Piece, List<int[]>> movePairOpp in allMovesOpp)
                {
                    KeyValuePair<Piece, List<int[]>> pieceMovesKeyValOpp = movePairOpp.First();
                    Piece pieceOpp = pieceMovesKeyValOpp.Key;
                    List<int[]> _mLOpp = pieceMovesKeyValOpp.Value;

                    foreach (int[] coordsOpp in _mLOpp)
                    {
                        BoardState originalBoardState_ = this.currentBoardState;
                        BoardState cloneState_ = BotHelperFunctions.simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);

                        this.currentBoardState = originalBoardState_;

                        List<int> boardControlOnBS = getBoardControlOnBoardState(cloneState_);
                        int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];
                        int oppBoardControl = this.color == -1 ? boardControlOnBS[0] : boardControlOnBS[1];

                        List<float> pointsOnBoard = BotHelperFunctions.getPointsOnBoardState(cloneState_, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        //if (this.color == 1) Debug.LogWarning("Points on board after " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                        float diff = botPoints - oppPoints;
                        if (diff < bestOppMoveDiff)
                        {
                            bestOppMoveDiff = diff;
                            bestOppMovePiece = pieceOpp;
                            bestOppMoveCoords = coordsOpp;
                            bestOppBoardControlDiff = botBoardControl - oppBoardControl;
                        }
                    }
                }

                // Take the best outcome assuming the opponent captures the highest value piece it can
               

                if (!positivePts && bestOppMoveDiff >= bestMoveDiff)
                {
                    float realDiff = startingDiff - bestOppMoveDiff;
                    bestMovePiece = realPiece;
                    bestMoveCoords = coords;
                    if (realDiff > 0)
                    {
                        positivePts = true;
                        validMoves.Clear();
                        validMoves.Add(new Move(bestMovePiece, bestMoveCoords));
                    }
                    else
                    {
                        if (bestOppBoardControlDiff >= bestBoardControlDiff)
                        {
                            if (bestOppBoardControlDiff > bestBoardControlDiff)
                            {
                                validMoves.Clear();
                            }

                            bestBoardControlDiff = bestOppBoardControlDiff;
                            bestMoveCoords = coords;
                            bestMovePiece = realPiece;

                            validMoves.Add(new Move(bestMovePiece, bestMoveCoords));
                        }
                    }
                }
                else if (bestOppMoveDiff >= bestMoveDiff)
                {
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

    private List<int> getBoardControlOnBoardState(BoardState bs)
    {
        List<int> boardControl = new List<int>();

        var botMovesWhite = BotHelperFunctions.getAllPossibleBotMoves(this, bs, 1);
        List<Dictionary<Piece, List<int[]>>> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = BotHelperFunctions.getAllPossibleBotMoves(this, bs, -1);
        List<Dictionary<Piece, List<int[]>>> listBotBlackMoves = botMovesBlack.pieceMoveList;

        List<int[]> uniqueCoords = new List<int[]>();
        foreach (Dictionary<Piece, List<int[]>> movePair in listBotWhiteMoves)
        {
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = movePair.First();
            Piece piece = pieceMovesKeyVal.Key;
            List<int[]> _mL = pieceMovesKeyVal.Value;

            foreach (int[] coords in _mL)
            {
                if (!uniqueCoords.Contains(coords))
                {
                    uniqueCoords.Add(coords);
                }
            }
        }
        boardControl.Add(uniqueCoords.Count);

        uniqueCoords = new List<int[]>();
        foreach (Dictionary<Piece, List<int[]>> movePair in listBotBlackMoves)
        {
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = movePair.First();
            Piece piece = pieceMovesKeyVal.Key;
            List<int[]> _mL = pieceMovesKeyVal.Value;

            foreach (int[] coords in _mL)
            {
                if (!uniqueCoords.Contains(coords))
                {
                    uniqueCoords.Add(coords);
                }
            }
        }
        boardControl.Add(uniqueCoords.Count);

        return boardControl;
    }
}

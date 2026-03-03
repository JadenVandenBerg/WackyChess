using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Move
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
    public NextMove nextMove()
    {
        Piece bestMovePiece;
        int[] bestMoveCoords;
        float bestMoveDiff = -1000;

        var botMovesCLONE = BotHelperFunctions.getAllPossibleBotMoves(this, this.currentBoardState, this.color);

        List<Dictionary<Piece, List<int[]>>> allMovesCLONE = botMovesCLONE.pieceMoveList;
        List<BotHelperFunctions.PieceAbility> pieceAbilities = botMovesCLONE.piecesAbilities;

        Debug.Log(this.name + " has " + pieceAbilities.Count + " abilities.");
        foreach (BotHelperFunctions.PieceAbility pa in pieceAbilities)
        {
            Debug.Log("Piece " + pa.piece.name + " has ability: " + pa.ability);

            if (pa.ability == "Spawn")
            {
                Debug.Log(pa.ability + " on " + pa.coords[0] + "," + pa.coords[1]);
            }
        }


        List<Move> validMoves = new List<Move>();

        //Each piece
        foreach (Dictionary<Piece, List<int[]>> movePair in allMovesCLONE) {
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = movePair.First();
            Piece piece = pieceMovesKeyVal.Key;
            Piece realPiece = BotHelperFunctions.getOriginalPieceFromClone(piece);
            List<int[]> _mL = pieceMovesKeyVal.Value;

            //Loop through moves
            foreach(int[] coords in _mL) {
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

                foreach(Dictionary<Piece, List<int[]>> movePairOpp in allMovesOpp) {
                    KeyValuePair<Piece, List<int[]>> pieceMovesKeyValOpp = movePairOpp.First();
                    Piece pieceOpp = pieceMovesKeyValOpp.Key;
                    List<int[]> _mLOpp = pieceMovesKeyValOpp.Value;

                    foreach(int[] coordsOpp in _mLOpp) {
                        BoardState originalBoardState_ = this.currentBoardState;
                        BoardState cloneState_ = BotHelperFunctions.simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);

                        this.currentBoardState = originalBoardState_;

                        List<float> pointsOnBoard = BotHelperFunctions.getPointsOnBoardState(cloneState_, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        //if (this.color == 1) Debug.LogWarning("Points on board after " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

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
        //Dictionary<Piece, int[]> moveDict = new Dictionary<Piece, int[]>();
        //moveDict.Add(sendMove.p, sendMove.coords);

        NextMove move = new NextMove(sendMove);

        return move;
    }
}

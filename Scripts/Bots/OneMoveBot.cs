using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

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
        var botMoves = BotHelperFunctions.getAllPossibleBotMoves(this, color);

        List<Dictionary<Piece, List<int[]>>> allMoves = botMoves.pieceMoveList;
        Dictionary<Piece, List<string>> allAbilities = botMoves.piecesAbilities;

        //Each piece
        foreach(Dictionary<Piece, List<int[]>> movePair in allMoves) {
            KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = movePair.First();
            Piece piece = pieceMovesKeyVal.Key;
            List<int[]> _mL = pieceMovesKeyVal.Value;

            //Loop through moves
            foreach(int[] coords in _mL) {
                BoardState cloneState = BotHelperFunctions.copyBoardState(currentBoardState);
                BotHelperFunctions.movePieceBoardState(piece, coords, cloneState);
            }
        }

        Dictionary<Piece, int[]> moveDict = new Dictionary<Piece, int[]>();
        //moveDict.Add(piece, coords);

        return moveDict;
    }
}

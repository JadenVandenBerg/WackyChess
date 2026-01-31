using System.Collections.Generic;
using UnityEngine;

public class RandomBot : BotTemplate
{
    public RandomBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();

        choosePieces();
    }

    public Dictionary<Piece, int[]> nextMove()
    {
        var move = BotHelperFunctions.getRandomBotMove(this);
        Piece piece = move.piece;
        int[] coords = move.coords;

        Dictionary<Piece, int[]> moveDict = new Dictionary<Piece, int[]>();
        moveDict.Add(piece, coords);
    }
}

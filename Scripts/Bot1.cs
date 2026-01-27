using System.Collections.Generic;
using UnityEngine;

public class Bot1 : BotTemplate
{
    public Bot1(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();

        choosePieces();
    }

    public Dictionary<Piece, int[]> nextMove()
    {
    }
}

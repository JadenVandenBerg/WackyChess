using System.Collections.Generic;
using UnityEngine;

public class RandomBot : BotTemplate
{
    public RandomBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Random Bot";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        this.currentBoardState.refresh(gameData.boardGrid);
        var move = BotHelperFunctions.getRandomBotMove(this);
        Piece piece = move.piece;
        int[] coords = move.coords;

        Move sendMove = new Move(piece, coords);
        NextMove move_ = new NextMove(sendMove);
        return move_;
    }
}

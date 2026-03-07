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
        this.currentBoardState = BotHelperFunctions.copyBoardState(this.currentBoardState);

        NextMove move_ = BotHelperFunctions.getRandomBotMove(this);

        if (move_.moveType == "move")
        {
            move_.move.p = BotHelperFunctions.getOriginalPieceFromClone(move_.move.p);
        }
        else
        {
            move_.ability.piece = BotHelperFunctions.getOriginalPieceFromClone(move_.ability.piece);
        }

        return move_;
    }
}

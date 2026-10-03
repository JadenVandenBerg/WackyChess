using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.Text.RegularExpressions;

public class SafetyBot : BotTemplate
{
    int turn = 0;
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public SafetyBot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Safety Bot";

        //This function populates the pieces variable
        choosePieces();
    }

    override

    public NextMove nextMove()
    {
        turn += 1;
        //Initialize for later
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> protectionMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

        foreach (NextMove nextMove in allMoves)
        {
            Piece piece;
            coords coords;
            string moveType = nextMove.moveType;

            if (moveType == "move")
            {
                Move mv = nextMove.move;

                piece = mv.p;
                coords = mv.coords;
            }
            else // moveType == "ability" guarenteed
            {
                PieceAbility pa = nextMove.ability;

                piece = pa.piece;
                coords = pa.coords;
            }

            foreach (NextMove nextMoveOpp in allMoves)
            {
                Piece pieceOpp;
                coords coordsOpp;
                string moveTypeOpp = nextMoveOpp.moveType;

                if (moveTypeOpp == "move")
                {
                    Move mv = nextMoveOpp.move;

                    pieceOpp = mv.p;
                    coordsOpp = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = nextMoveOpp.ability;

                    pieceOpp = pa.piece;
                    coordsOpp = pa.coords;
                }

                if (piece.position.x == coordsOpp.x && piece.position.y == coordsOpp.y)
                {
                    validMoves.Add(nextMove);
                    gameData.helper.addBotMessage("Protection Move Executed");
                }

            }
        }


        if (validMoves.Count == 0)
        {
            System.Random rand0 = new System.Random();
            int rndIdx0 = rand0.Next(allMoves.Count);
            validMoves.Add(allMoves[rndIdx0]);
            gameData.helper.addBotMessage("Random Move Executed");
        }

        //Pick a random move from our list of tied moves
        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

        //Get the original piece, you can just copy paste this part (ill probably add this to botMaster.cs later)
        if (move.moveType == "move")
        {
            move.move.p = getOriginalPieceFromClone(move.move.p);
        }
        else
        {
            move.ability.piece = getOriginalPieceFromClone(move.ability.piece);
        }
        return move;
    }
}

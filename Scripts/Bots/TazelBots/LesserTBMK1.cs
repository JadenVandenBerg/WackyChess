/*using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public class LesserT : BotTemplate
{
    public static string path = "TBMK1storage/" + "enemyBot" + ".txt";
    int turn = 0;
    double lastTurnPoints = 0;
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //1 is white, -1 is black
    public LesserT(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Lesser TBMK1";
        //This function populates the pieces variable
        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        string content = "Hello, World!";
        File.AppendAllText(path, content); 
        
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
*/
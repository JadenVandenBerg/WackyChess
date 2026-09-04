using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static HelperFunctions;
using System.Drawing;
using System.Runtime.InteropServices;
using System;

public class Robot : BotTemplate
{
    int turn = 0;
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black

    public HelperFunctions helper;

    public Robot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Robot";

        //This function populates the pieces variable
        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        //Initialize for later
        //float bestMoveDiff = -1000;
        float bestMoveDiff2D = -1000;
        List<NextMove> validMoves = new List<NextMove>();

        //Get all the possible moves. Note that some of these moves may result in check, which will not be allowed
        //Other than that these moves are all legal.
        //NextMove is a class with 3 vars inside, NextMove.moveType == "move" | "ability"
        //if move, NextMove.move will be populated
        //Move contains Move.p, Move.coords
        //if ability, NextMove.ability will be populated
        //public Piece piece; //The piece with the ability
        //public string ability; //Ability name
        //public int[] coords; //Coords for abilities with one action (ie. Spawning, Freezing)
        //public List<Piece> placePieces; //Pieces for abilities with multiple actions. Only hungry for now
        //public List<int[]> placeCoords; //Coords for abilities with multiple actions. Only hungry for now
        //public Piece secondPiece; //The second piece used in abilities. Used for castling/spawning
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        turn += 1;
        bool moveHasBeenDicided = false;

        gameData.helper.addBotMessage("*̸̢̢̡̧̧̢̛̛̛̛͈̫̬̬̝̖̘̪͖̞͇͚̜̦͈͉̪̘͖̰̦͖̦̜̤̩͒͋̐̿̔̑̽̔̆̈́̀̈̏̈̋͆̿͒̊̀̋̾̄͌͐̔̄̓͛͑́̄͊̏̋̿̅̓͋̉̓̈́͋̿͌̃̂̔̇̈́͒̎̓́͋̅̽̉͛̊̄͂̓͗͆̃͘̚͘̕̕͘̚̚͜͝͝͝͠͝͝ͅ#̷̨̢̧̡̢̨̧̧̨̧̛̛̠̩̼̠͕̤͓̫̜̟͕͚̠̞̥̜͇̥̹͙̳̞̯̩͔͔̥̘̫͚͚̤̥̞͖͍̦͉̪̫̪͕͙̘̺̫͈͉̗̯͉̞̞͚̦̩̦̙̘̜̝̤̱̭͓̼͈͇̬͖̝͓̱̹͇̬̝͙͈͔̦̥̜̦̣̼͖̱̳͕͉͉̤̲̮͚̗̤̻̣̦̱̗͕͖͙̲̱̂̓͒͌̈̓͐͒̔͋̈́͆̊̇̈̒͊͋̔͂̏͌̋̽͊̍̑͆̀͒̋̿̒̓̒̍͋̿͊̓́̋́̋̈̉̋͒͛͑͐̊͆͌͑̀̓̍̀̄̎͑̄̀͊̓̎̇̀̇̎͋̓̾͂̋̂̑́̊̏̒̊̎̀͋̅̀̎͋̃͌̎͆́͗̔̚͘̚̕̕̚͜͝͝͝͠͝͝͝͝͝͠͝ͅͅͅ*̵̡̧̥͉͇̞͍̣̟͖̰̦͔͍̞̫̖̭̣̮̣̱̺̬̦͍͔̟̣͓̩̹͙̙̠͎̙̳̝̥͇͇̭̱̺͍͔̦͙̱̹̯͔͎͔̮̘̺̫̮̳̮̠͔̳͚̋̽̈́̓͆̀͂̆͌̑͑̒̋̓͐̈́͐̒̃̏͐͂̒͂͑̓̈̃̉́͆̊́͋͗͊̉͌̃̽̈́͋̏̒̑̏̔͋͘̚͘̚̚̕͜͜͜͜͝͝ͅͅ ̴̡̨̧̢̨̨̡̛̛̛̞̙̭͖̪̘̲̤̮̪͍̭̝̜̱̼̖̮̹̖̬̦̤͖͈̭̭͖͉̟̫̻͈̺͉̤̻̩͖̜̘̯̘̯͙̽̃̾̇̆͋͌͋̾͂̇̌̑̒̌͊̋́́̽̿̂̅̀̓̆̊̈̏̒́́̐̋̔̊͂̒̈́̎͌̒̆̐̀̆̊̍̊̓̋̍̔̿̏̾̃̆̒̾̇͛͋̓͒̔̅͑͗̋̐̓͒̓̆̈́͗̈̈́̄̊͑̋͒͊̈̃̑̀́̈̉̑̌̆͛̈́̉̓̈̊̆̈̕͘̚̚̕͜͜͝͠͝͝͝ͅͅŖ̶̧̧̢̢̡̡̡̛̛͉̻͎̼̙̲̝̦͎̗͓͈̥̟̺͎͍̯̲̦̘̲͓̦̳͍̫̖͕͕̖̣͇͖͎̤̘̱̮̬̥̹͓͇̯̼̱͎̣͎̣̲̤̦̥̠̱̭̣̺͇̙̣̼̞̖̘̰̭̻̟̩͓͖͚̗͙͇̯͕̹͖͖͕̹͓̜̜̰͈̊̈́̀̾͑̓̉̑́̈́͑́͛́̅̍̑̒̎̇̎̉̆͌̌͑̀́́̆̒̈̉̓̈̆̒͒̇̅͆̄͂̌̓̂̃̒̽̊̒̂̓̅̀͑̓̊̂̉͋̓̿̄́̾̊̍̓̽́̈̇͒͑̈́̍̓̈́̇͌̐̐̔̒͋͜͜͜͝͝͠͠͝͠͝͝͠͠ͅͅͅͅͅö̶̡̡̨͙̲̩̰̻̝̬̜̱̬̭̥̤̱͇̲̫̭̭̹̼̗̣̤̹̱̞͎̖̫̻̝͉͔̙̥̦̟̘̦̘̥̲̦͉̟̺̣͎̳͚͈̹̘͙̙̙̰̻̥̫̘̦͇͚̍̃̒̄̄̊̐͊̐͐̃̿̿͐͆̆̍͛̍̓̏̉̓̊̉̈́͋̅̉̊̍̀̎̑̕̚̚̕̕͘͜͜͜͝ͅͅͅb̵̢̡̡̡̧̢̨̧̡̡̧̡̛̛̛̛͔̳͖͙̟̤͙̹͇̬̖̝̪͈̗͉͙̩͔͎̥̳̖̙͚̤̬̳̯͕̹̜͙̠̝͎͍̦͉̜̦̤̻̻͔͍̼̝͚͚̗̯̼̞̟̙͖̪̜͖̥͇̭͍̘̟̞̖̦̭̱̝͍̺̲̼͚̠͙̫̣͕̮̜̗̬̠̟̻͖̫̺͆̇̈͊͌́͛͆̈́̀̄̆̂͛̂͆̌̆̐̈̓̐̀̈̃͂͐́̂̑͐̅̀͛̉́̈́̽̋̎̀̄̿͋̾͆̔͋͆̑̉̍͋͊͂̏̍̍̋̿̓͛̆͗̈́̋́̓̈́̕̕̕̚̚͘͘̚̚̚͜͜͜͜͝͝͝ͅͅͅͅǫ̷̛̗̻̥̞̠͔̭̖̲̲̩̥̦̫̘̫̱̲̙̲͖͉͚̹̖͔̠̇͗͌̓̋̌̎̓͑̀͒̀͋̂̀̾͋̄̐̎͑̈́̃͊͛͑̈́̈́͒̽̇̽̓̉̔̓̈́̑̓̈̑̕̚̕͜͜͜͝͝͝͝͝͠͠͝ͅt̷̢̨̢̧̢̟̖̥̝̼͖̖̣̮̜̭̘͓͈͔̲̖̣̫͖̯̖̮̠͕̯̪̠͚̙̱͕̳̣̣̣̦̠̙̟͇͈̺̩̘̰̹͕͙̼̰̲̥͇̘̙͚̼͙̆̑͐̋̊̍͗̈́͛͌̎̊̊́͗͘͜ͅͅ ̴̨̢̛͚͙͙̦̭̜͇̪͈̥̰͒̏̊͒̍͌͗̍̽̈̓͌͑͂̆̃̾̀̒̊́̂̔͌͒̿̀̍͋̎̀͗̔̆͊̓́͒̅̃̓̔̈́͒̒͒͒͒̾̈́̈́̏̒͑̈́͋̀́͂̈́̓͒́͊͐̃̒͆͌̽͒̈̈́̓̌́̂̍͆̄̐́͊́͆͑̕͘͘̕͘̕̕͠͝͝͝͠͝͠͝i̸̢̡̡̡̨̨̧̨̧̡̨͖̬̫̤̭͍̠͔̱̥̟̩̰̱͚͇͙̯̻̖̜̹̩̘̲̱̱̮͚̥͎̳̠̲̳̺̥͉͉̬̤̫̣̞͕͍̮̤͖̭̫̥̖̻̮̬͓̭̠̩̘̜̪̽̇͜͜ͅͅs̶̢̧̧̧̢̢̛̠̥̱̙̩͔̬̯̰̖͎̹̺͓̝͖͕̠̮͖̞̪̲̠̤̙͙̪̫̲͖̮̪̝͍͎̮̜̱̹̣͔͚̳̣͈̣̬͇͎͚̤͓͚̰̙͍̗͖͉͕͇̼̟̮̙͕̥̤͓̍̐͋̈̿̽̐̂͂̏̈͐̽͒̓̂͋̒̓͛̐̏̍̾͒̍̔̅̃͆͒̈́͊̔̎̎̊̈́̾̎̓̔͘̕̕͜͝ ̸̬͔̘̼̼̗͈͑̏̆̄̈̿͂͌̌͑͛̈̌̍̏͒̉̔̀̇͑́̏͆͂̿́́́́̋̽̔͗̈́̉̈́̌̀̒͌̓̾̍͂̉̌̀̌́͛̊͒̇̐̒͊̄̀͊̇̈́̋̈́̑̃̃̿̂͌͋̈́̉͒͂̑̎̄̈́̊̈́̎̀̈́͌̌̎͌̽̃̓̉̓̓̇̀̒͆̇̀̕̕̚̕̕̕̚͠͝͝͝͝͠͠͝͠c̶̢̡̧̧̡̛̛̝̘̞͚̥͚̫̼̮̤̯̟͓̤̻͓̲͎̤̙̦̭̠̰̻͉̥̺̮͍̖͍̤͙̖̳̬̤̤̪̲͚̺̜̲̼͒̈͐́̉̏̈́̃̍̐͐̈̅͑̍̋͛̊̿̓̍́̍̒͌͗̀̒̅̆̓̍̽̔͛̀͋̓͋̓̆͋̆̎̃̈̇̈͛̈́̃͂͊͗̇͐̍̈́̈͛́̒̑͊͗̍̊͑̉͌̌̊͆̔̀͋͊̐̽͋̒͒͆͑̀͆̓̄̐̆̕̚̚̕̚̚̚̕̕̚͘͜͝͝͝͝͠͝͝͝͝ͅͅǫ̵̨̡̢̛̛̞̱̞̘̖̙̹̠̩͉͍̗͕̼̰͉͔͚͖̦̻̰͍̪͓̳̭̮̥̻̙̩̙͈͔͉̮̦̼̪̫̻̪͓͍̘̩̪͉̺̥̻͉̣̖̜̫̽̽̿̾̇̿̅̋̄̊́̈́͛̅̐̃̽̊̍͌̂̀͛̽̀͌̈́̊͆̆̔̐̑̄̃͗̈́̔̈́́͐̎̌͒̄̓̋̏́́́͌̾́̄͛͊̚͘̕̕̚̕͜͜͜͜͝͝͝͠͠͝ͅͅͅm̸̨̡̧̨̨̡̡̧̛̦͙̣͔̫͓͖͎̙͔̺̣̻̪̥̩̖̹̝͈͎̹̦̰̼͈͔̬̩̙̮̯̙͖̙̫̦̪̥̥̝̗̗̮̳̟͚̪͎̫̝̳̬̿̆̽͐̇̉͗͌͊́̔̃͐̄̇̆̽͒̏̆̈́́̂̄̒̃͋̓̊̿̅̑͒̅͛̔̆̿̒̉͒͒̾̓̄̌͌̎͒̅̄̀̈́̎̓̓̌̒̎̀́̓͋͌̚̚͘͜͜͠͝͝͝͠͠͠͝p̶̨̡̧̧̡̡̨̨̛̛̹̭͎̲̘̝̯͇̟̪̺͇̳̜̦̗̲̹̘̞̭̞̠̬̪͕͖̞̥͇̹̬̝̗͓̻̰͔̩̗̞̱͎̜̬̟̘̯͎̣̟͍̬̬̹̬̪͔͚̘̜̩̻̗̗̳͙͎̪͓̝̘̰̰̬͈̯̫̫̙̹͔̞̘͎̩̱̭̘͙̖͚̍̊͒̀̉̑͊̿̌͑̓̊͆̔̓̔͐͐͛̅͗̌̌̊̐̽͆̀̈́̒̅̃̐͆̂͊͌͑͗̊̈́͆̓̽̇̒̑͐̒͛͂̽̆͛̆͊̀̉̕̕͜͜͝͝͠͝͝͝ͅͅư̴̢̨̡̧̡̡̡̡̟̠̯̪͔̫̝̟͔͎̮͈͙̩͔̹̯̦͖͉̜͉͙̰̼̱̬̖̝̻̙͙̜̞̮̜̤͖̦͕̠͖̰̝̼̬̖͈͎͙̲͓̼͕͇͇͖͕̪͙̜̫̮̟̤͉̩̣͍̞̼̩̣̰͍͉̞̣͈̤̗̹̰̰̼͖̻͕͚͖͔̪̗̱̞̯͋̔̽̐̿͛͒̆̑́̊̽̓͗̆̋͋̓̐͑͂̃̉̐̀̈́̈́͒̐̿̂̋͊̆͑̂͊͒͆͋̄̅̈͆̉̐̉̈̐̋͛͑̍́͒̐̌́̈́̈́̐̓̄̂͛̒̓̋̓̕͘͜͝͠͝͝͝͝͠ͅţ̶̧̡̡̧̨̧̧̡̡̨̖̻̲͕͕͉͎͕̹͉͖̝͔̝̫̝͍͎̼̦̼̬̗̙̘̣̪̗̣̹̦̺͍̼̮̮̙̟̞̞͉̝̘͖͍͕͕̥̯͓͇̗̩͔̠̤̘͚̖͕͇̼̝̗͉͎̙̰̮̳͕̳̦͚͈̭͈̘̭͖̺̜̭̮͈͉̙̣͙̬̟͑́͛̈́͊̉̑̃̇͒́͂͐͆̿͊͋̍͋͘̚͘͘͜͝͝į̴̧̨̨̨̛̻̥̦̹͉̦͕̳̫͈̘͖̹̞̦̩̭̗͓̝̗̝̗̬͈̯̼̙̝̤̼̖͓͙̭͙̤̪͇̫͎̺̼̲͚̼̪̺̗̘̥̩͉͛͆̊͑͂̃̊̍̅̀͊́͋́͊́̒͜ͅͅͅͅͅǹ̷̡̧̢̧̛̛̛̛̹̖͉̗͈͚̗̳̪̬̪̪̼̟̹͚͈̪̘̣̦̠̰͉̤͙̜͖͓̣̙̬̳̫̝̰͓͚͕͇͎̼̱̟̱̱̘̟̝͙̤͎͎̹͈͓̝͍̖̝̤̫̬̫̬̰̻̖̩͚͔̘͍̳̾͑̿̃͛̑́̔̒̆͆́̾̃̉̆̔̔̀̀̀̈́͌̑̈̏͌͐͊̾̈́̈́̿̈͆̉͐͗͛́̔̇̄̈́̆͌̋̈̒̽̏̍̈͛̋̋͗̂́̒͒͌͗͂̐̾͂͌̎̂̐́͂̔̂̃̿̏͗̈́̂̅̅́̿͌̃̚̕̚̕̚͘͝͠͝͝͠͝͝͝͝͝g̵̨̘̱͕̯̺̹̳͎̗̬͇̮̪̙̪͎̲̭̪̣̮̯̘̠̬̈́̀̆̃̄́̊͑̆̒̀̅̉̿̌̽̒͐̒̈́̈͋̆̓̋͋̃̀̋̈͛̐̅̀̾̏̓̍̃̿̓̀̓̇̈́̃͛̿̓̋́̐͌̾̉͛̋́̑̔̔̓̄͆̐͒̀̊̀̇̋̆̓̈́̌̓̑̓̾̈́̈̄́͂͋̏̿̀͌̌̏̀̾͗͒̐̄̎̍̀͊͂͒̏̃̔̌̆̕͘͘̚̚̚̕̕̕͘͜͝͝͝͝͠ͅͅ!̷̨̨̛͙͉̤̞̱͎̦̮͔̟̯̙̼̺̙͔̭̰̭̹̰͓̘̹̲͕̞̳̺̘͓̒̔͊̑͋̂͒̋̍͐̔̆̐͒̿͆̈̌̑͆̔͒̒̈̾͆̌̃̈́͠͠͠ͅ ̴̡̛̛̛̮̹͉̭̙̲̓́̓͋͛͊̍̋̒̊̎̎͂̾͑͆͑͒͂͛͆̅́̍͆͂͌̀͐̐͒̈́̐͑̅̾̓̑̌̍́͌̀̑͐͛̀̈́͐͆͘̕͜͠͝͝*̷̡̨̛̛̱̘͉͙̎̃̓͂̂̒̇̿͊̈̎͐̑̌̂͊͒̆̀̒̓̏̐̽̽̋̈̄̽̅̉̑̽̊̄̃̒̓́̾̈͑̏̈́́̿͆̈́̉͆̍̂͂̏̾͌͒͆̂͗͊̎̃̀̿̀͗̏͋̾̄͒͒̄́̏̐̆̑͌̄͛̚͘̕̚̚̚͝͝͝͝͠͠#̶̡̡̛̛͔̺̲͍͎͚̟͔̖̝̙̘͔͖̥͎̮̙͇͖͍̹̀̀͑̑̈̑̄̈͊̆̃͌́̓̇̀̀͊͌̽̈̍́̆̂́͒͑͊̽̋͒͊̉̓͆̐̎̋̅̑̀͑͑͒͗̀͑̓͊̃͊̑͌̋̃̋̈́̌̇̊͛̇̾̍̓͒̓̇́̆̎̿̽̋̄͒̑̈͗͂̉̽̂̀͂̌͒̈́̆̔͌̓̾̒̎́͒̽̑̅̀̿̌͌̇̚̕̕̕̚͝͝͝͝͝͝͝*̸̡̧̧̡̡̧̧̧̨̧̺̥̙̭̞̟͍̞̤̠̬̞̖̠̙̭̝̼̖͈͍̜̰͍̪͔̦̤͍͔͈͔̘̬̤̲͙̱͙̞͖͍̘̗̦̙͔̟̬̫̪͍̪̙̫͔̈́̋̉́̓̆͂̈́͌̾̒̂̍̍̃͋̉̆̉͐̿̀́̐̿͛̌̅̇̋̊͛͊̔͆͐̈́͑̿̑͑̓̃͑̐͂͋͐̉͑͒̌̉̉͌͋̚̚͜͜͝͠͠͠ͅͅͅ");
        
        if (turn == 3 || turn == 5)
        {
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

                    if (piece.baseType == "Knight")
                    {
                        if (color == -1)
                        {
                            if ((coords.y == 6 && coords.x == 3) || (coords.y == 6 && coords.x == 6))
                            {
                                validMoves.Add(nextMove);
                                moveHasBeenDicided = true;
                            }
                        }
                        else if (color == 1)
                        {
                            if ((coords.y == 3 && coords.x == 3) || (coords.y == 3 && coords.x == 6))
                            {
                                validMoves.Add(nextMove);
                                moveHasBeenDicided = true;
                            }
                        }

                    }
                }
            }
        }
        else if (turn == 1)
        {
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

                    if (color == -1)
                    {
                        if (piece.baseType == "Pawn" && piece.startSquare.y == 7 && (piece.startSquare.x == 4 || piece.startSquare.x == 5))
                        {
                            if (coords.y == 5 && (coords.x == 5 || coords.x == 4))
                            {
                                validMoves.Add(nextMove);
                                moveHasBeenDicided = true;
                            }
                        }
                    }
                    else if (color == 1)
                    {
                        if (piece.baseType == "Pawn" && piece.startSquare.y == 2 && (piece.startSquare.x == 4 || piece.startSquare.x == 5))
                        {
                            if (coords.y == 4 && (coords.x == 5 || coords.x == 4))
                            {
                                validMoves.Add(nextMove);
                                moveHasBeenDicided = true;
                            }
                        }
                    }
                }
            }
        }
        else if (turn == 2)
        {
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

                    if (color == -1)
                    {
                        if (piece.baseType == "Pawn" && piece.startSquare.y == 7 && (piece.startSquare.x == 4 || piece.startSquare.x == 5))
                        {
                            if (coords.y == 6 && (coords.x == 5 || coords.x == 4))
                            {
                                validMoves.Add(nextMove);
                                moveHasBeenDicided = true;
                            }
                        }
                    }
                    else if (color == 1)
                    {
                        if (piece.baseType == "Pawn" && piece.startSquare.y == 2 && (piece.startSquare.x == 4 || piece.startSquare.x == 5))
                        {
                            if (coords.y == 3 && (coords.x == 5 || coords.x == 4))
                            {
                                validMoves.Add(nextMove);
                                moveHasBeenDicided = true;
                            }
                        }
                    }
                }
            }
        }
        else if (turn == 5)
        {
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

                    if (color == -1)
                    {
                        if (piece.baseType == "Bishop")
                        {
                            if (coords.y == 7 && (coords.x == 5 || coords.x == 4))
                            {
                                validMoves.Add(nextMove);
                                moveHasBeenDicided = true;
                            }
                        }
                    }
                    else if (color == 1)
                    {
                        if (piece.baseType == "Bishop")
                        {
                            if (coords.y == 2 && (coords.x == 5 || coords.x == 4))
                            {
                                validMoves.Add(nextMove);
                                moveHasBeenDicided = true;
                            }
                        }
                    }
                }
            }
        }
        if (moveHasBeenDicided == false)
        {
            //Loop through all moves
            foreach (NextMove nextMove in allMoves)
            {
                //Find out what the moveType is and set vars accordingly
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

                //this.currentBoardState at the start of nextMove is a BoardState containing info of all the pieces. Save this. After we loop through all opponent moves, we set
                //this.currentBoardState = originalBoardstate
                BoardState originalBoardState = this.currentBoardState;

                //Simulate the piece move
                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
                }
                this.currentBoardState = cloneState;

                //Now that we have simulated our move, we do the same with opponent moves
                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

                NextMove bestOppNextMove;
                float bestOppMoveDiff = +1000;

                //Loop through all opponent moves
                foreach (NextMove nextMoveOpp in allMovesOpp)
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

                    //Save the boardstate again, then simulate opponent move
                    BoardState originalBoardState_ = this.currentBoardState;
                    BoardState cloneState_;
                    if (moveTypeOpp == "move")
                    {
                        cloneState_ = simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);
                    }
                    else
                    {
                        cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp.ability);
                    }
                    this.currentBoardState = cloneState_;
                    //Using the boardstate after opponents boardstate, get the points on board
                    // this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1] means if bot is white, use [0] else [1]
                    List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                    float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                    float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                    //Debug.Log("Testing a move by " + piece.name + " and opp " + pieceOpp.name + " results in " + (botPoints - 100) + " : " + (oppPoints - 100));
                    //if (this.color == 1) Debug.LogWarning("Points on board after " + moveType + "," + moveTypeOpp + " " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                    //debug_printBoardState(cloneState_);

                    //Compare the difference of points. If the diff is a new best (in the sense of black made a good move), mark it as best
                    //In this algorithm, this is considered to be the best move the opponent can make
                    float diff = botPoints - oppPoints;
                    if (diff < bestOppMoveDiff)
                    {
                        bestOppMoveDiff = diff;
                        bestOppNextMove = nextMoveOpp;
                    }
                    this.currentBoardState = originalBoardState_;

                    //Now that we have simulated our opponents move, we do the same with our moves
                    List<NextMove> allMovesDepth2 = getAllPossibleBotMovesAndAbilities(this, cloneState_, this.color);
                    foreach (NextMove nextMoveD2 in allMovesDepth2)
                    {
                        //Find out what the moveType is and set vars accordingly
                        Piece pieceD2;
                        coords coordsD2;
                        string moveTypeD2 = nextMoveD2.moveType;
                        if (moveTypeD2 == "move")
                        {
                            Move mvd2 = nextMoveD2.move;

                            pieceD2 = mvd2.p;
                            coordsD2 = mvd2.coords;
                        }
                        else // moveType == "ability" guarenteed
                        {
                            PieceAbility pad2 = nextMoveD2.ability;

                            pieceD2 = pad2.piece;
                            coordsD2 = pad2.coords;
                        }

                        BoardState originalBoardStateD2 = this.currentBoardState;
                        BoardState cloneStateD2;
                        //Save the boardstate again, then simulate our move
                        if (moveTypeD2 == "move")
                        {
                            cloneStateD2 = simulatePieceMove(this, this.currentBoardState, pieceD2, coordsD2);
                        }
                        else
                        {
                            cloneStateD2 = simulatePieceAbility(this, this.currentBoardState, nextMoveD2.ability);
                        }
                        this.currentBoardState = cloneStateD2;
                        //Using the boardstate after opponents boardstate, get the points on board
                        // this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1] means if bot is white, use [0] else [1]
                        List<float> pointsOnBoard2 = getPointsOnBoardState(cloneStateD2, true);
                        float botPoints2 = this.color == 1 ? pointsOnBoard2[0] : pointsOnBoard2[1];
                        float oppPoints2 = this.color == -1 ? pointsOnBoard2[0] : pointsOnBoard2[1];

                        //Debug.Log("Testing a move by " + piece.name + " and opp " + pieceOpp.name + " results in " + (botPoints - 100) + " : " + (oppPoints - 100));
                        //if (this.color == 1) Debug.LogWarning("Points on board after " + moveType + "," + moveTypeOpp + " " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                        //debug_printBoardState(cloneState_);

                        //Compare the difference of points. If the diff is a new best (in the sense of black made a good move), mark it as best
                        //In this algorithm, this is considered to be the best move the opponent can make
                        float diff2 = botPoints2 - oppPoints2;
                        if (diff2 < bestMoveDiff2D)
                        {
                            bestMoveDiff2D = diff2;
                            //bestNextMove = nextMoveOpp;
                        }
                        this.currentBoardState = originalBoardStateD2;
                    }

                }

                //Now back to the outer loop, if the move we checked, assuming the opponent makes the best move, is better than the current best, save it
                //If it is tied also save it
                if (bestOppMoveDiff >= bestMoveDiff2D)
                {
                    if (bestOppMoveDiff > bestMoveDiff2D)
                    {
                        //If it is better, clear all saved moves
                        validMoves.Clear();
                    }

                    bestMoveDiff2D = bestOppMoveDiff;

                    //If it is a tie or better, save the move
                    validMoves.Add(nextMove);
                }


                //Reset the currentBoardState and go to the next move
                this.currentBoardState = originalBoardState;

            }
        }


        //Pick a random move from our list of tied moves
        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

        //Get the original piece, you can just copy paste this part (ill probably add this to botMaster.cs later
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
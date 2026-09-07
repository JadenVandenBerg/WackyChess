using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
/*
To Do:
Add my "best" next move to the calculation for opponent best move TOO EXPENSIVE
Maybe add more bad move culling
Add checks that don't cause pieces to die
*/
public class SicklyBotChild : BotTemplate
{
    int turn = 0;
    float moveDiff = 6;
    bool acceptableLineFound = false;
    NextMove bestOppNextMove;
    NextMove bestOppNextMove2;
    NextMove bestOppNextMove3;
    NextMove bestOppNextMove4;
    NextMove bestOppNextMove5;
    BoardState originalBoardState;
    float penaltyTimer = 4800;
    bool checkOccured = false;

    //Aim for four and a half seconds as a 500ms safty net
    List<Piece> piecesList = new List<Piece>();
    List<NextMove> moveList = new List<NextMove>();
    List<string> moveTypeList = new List<string>();
    bool run = true;
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public SicklyBotChild(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Sickly Bot Child";

        //This function populates the pieces variable
        choosePieces();
    }

    public NextMove runAcceptableLine(BoardState currentBoardstate)
    {
        int num = 0;
        List<Color> colors = new() {Color.orange, Color.yellow, Color.blue, Color.purple, Color.black};
        foreach(NextMove listmove in moveList)
        {
            if (listmove.moveType == "move")
            {
                HelperFunctions.highlightSquare(HelperFunctions.findSquare(listmove.move.coords.x, listmove.move.coords.y), colors[num]); 
            } 
            else
            {
                HelperFunctions.highlightSquare(HelperFunctions.findSquare(listmove.ability.coords.x, listmove.ability.coords.y), colors[num]); 
            }
            num += 1;
        }

        List<NextMove> allMovesCompair = getAllPossibleBotMovesAndAbilities(this, currentBoardstate, this.color);
        foreach (NextMove listmove in moveList)
        {
            foreach (NextMove checkingMove in allMovesCompair)
            {
                if (string.Equals(checkingMove.moveType, listmove.moveType, StringComparison.OrdinalIgnoreCase))
                {
                    if (checkingMove.moveType == "move")
                    {
                        if (string.Equals(checkingMove.move.p.name, listmove.move.p.name, StringComparison.OrdinalIgnoreCase) && string.Equals(checkingMove.move.coords.x.ToString(), listmove.move.coords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(checkingMove.move.coords.y.ToString(), listmove.move.coords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            return listmove;
                        }
                    }
                    else
                    {
                        if (string.Equals(checkingMove.ability.piece.name, listmove.ability.piece.name, StringComparison.OrdinalIgnoreCase) && string.Equals(checkingMove.ability.coords.x.ToString(), listmove.ability.coords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(checkingMove.ability.coords.y.ToString(), listmove.ability.coords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            return listmove;
                        }
                    }
                }
            }
            //These can cause crashes
            //piecesList.RemoveAt(0);
            //moveTypeList.RemoveAt(0);
        }

        //Abort if it can't find the move
        gameData.helper.addBotMessage("Sickly Bot Child is aborting the line!");
        foreach (NextMove prollyamove in allMovesCompair)
        {
            if (prollyamove.moveType == "move")
            {
                UnityEngine.Debug.Log("Coords: " + prollyamove.move.coords.x.ToString() + ", " + prollyamove.move.coords.y.ToString() + " Piece: " + prollyamove.move.p);
            }
            else
            {
                UnityEngine.Debug.Log("Coords: " + prollyamove.ability.coords.x.ToString() + ", " + prollyamove.ability.coords.y.ToString() + " Piece: " + prollyamove.ability.piece);
            }
        }
        UnityEngine.Debug.Log("End of available moves list");
        foreach (NextMove move in moveList)
        {
            if (move.moveType == "move")
            {
                UnityEngine.Debug.Log("Coords: " + move.move.coords.x.ToString() + ", " + move.move.coords.y.ToString() + " Piece: " + move.move.p);
            }
            else
            {
                UnityEngine.Debug.Log("Coords: " + move.ability.coords.x.ToString() + ", " + move.ability.coords.y.ToString() + " Piece: " + move.ability.piece);
            }
        }
        run = true;
        acceptableLineFound = false;
        float bestMoveDiff = -1000;
        List<NextMove> filterList = new List<NextMove>();
        List<NextMove> validMovesSecondLayer = new List<NextMove>();
        foreach (NextMove nextmove in allMovesCompair)
        {
            Piece piece;
            coords coords;
            string moveType = nextmove.moveType;

            if (moveType == "move")
            {
                Move mv = nextmove.move;

                piece = mv.p;
                coords = mv.coords;
            }
            else
            {
                PieceAbility pa = nextmove.ability;

                piece = pa.piece;
                coords = pa.coords;
            }
            if (piece == piecesList[0] && moveType == moveTypeList[0])
            {
                filterList.Add(nextmove);
            }
        }
        if (filterList.Count > 0)
        {
            foreach (NextMove nextMove in filterList)
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
                    else
                    {
                        PieceAbility pa = nextMoveOpp.ability;

                        pieceOpp = pa.piece;
                        coordsOpp = pa.coords;
                    }

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
                    this.currentBoardState = originalBoardState_;

                    List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                    float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                    float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];
                    float diff = botPoints - oppPoints;
                    if (diff < bestOppMoveDiff)
                    {
                        bestOppMoveDiff = diff;
                        bestOppNextMove = nextMoveOpp;
                    }
                }
                if (bestOppMoveDiff >= bestMoveDiff)
                {
                    if (bestOppMoveDiff > bestMoveDiff)
                    {
                        validMovesSecondLayer.Clear();
                    }

                    bestMoveDiff = bestOppMoveDiff;
                    validMovesSecondLayer.Add(nextMove);
                }


                //Reset the currentBoardState and go to the next move
                this.currentBoardState = originalBoardState;
            }
            System.Random randomNum = new System.Random();
            return validMovesSecondLayer[randomNum.Next(validMovesSecondLayer.Count)];
        }
        else
        {
            System.Random randomNum = new System.Random();
            NextMove move2return = allMovesCompair[randomNum.Next(allMovesCompair.Count)];
            if (move2return.moveType == "move")
            {
                if (move2return.move.p.baseType == "Pawn" || move2return.move.p.baseType == "King")
                {
                    move2return = allMovesCompair[randomNum.Next(allMovesCompair.Count)];
                }
            }
            else
            {
                if (move2return.ability.piece.baseType == "Pawn" || move2return.ability.piece.baseType == "King")
                {
                    move2return = allMovesCompair[randomNum.Next(allMovesCompair.Count)];
                }
            }
            return move2return;
        }
    }

    public static List<coords> GetLinePoints(coords p1, coords p2)
    {
        List<coords> positions = new List<coords>();

        int x1 = p1.x;
        int y1 = p1.y;
        int x2 = p2.x;
        int y2 = p2.y;

        int dx = Math.Abs(x2 - x1);
        int dy = Math.Abs(y2 - y1);

        int sx = (x1 < x2) ? 1 : -1;
        int sy = (y1 < y2) ? 1 : -1;

        int err = dx - dy;

        while (true)
        {
            positions.Add(new coords(x1, y1));

            if (x1 == x2 && y1 == y2) break;

            int e2 = 2 * err;

            if (e2 > -dy)
            {
                err -= dy;
                x1 += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                y1 += sy;
            }
        }

        return positions;
    }

    public bool inCheck(BoardState bs, int color) //Doesn't do abilities too many false positives
    {
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, bs, color * -1);
        bool check = false;
        coords kingPos;
        Piece king = isolatedGetKing(bs, color);
        if (king is not null)
        {
            kingPos = king.position;
            foreach (NextMove nextmove in allMoves)
            {
                if (nextmove.moveType == "move")
                {
                    if (nextmove.move.coords.x == kingPos.x && nextmove.move.coords.y == kingPos.y)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return check;
    }

    public bool inCheckmate(BoardState bs, int color)
    {
        if (inCheck(bs, color))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    override
    public NextMove nextMove()
    {
        turn += 1;
        //Initialize for later
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

        List<float> acctualpointsOnBoard = getPointsOnBoardState(this.currentBoardState, true);
        float acctualbotPoints = this.color == 1 ? acctualpointsOnBoard[0] : acctualpointsOnBoard[1];
        float acctualoppPoints = this.color == -1 ? acctualpointsOnBoard[0] : acctualpointsOnBoard[1];
        float acctualDiff = acctualbotPoints - acctualoppPoints;


        //If a line has been found follow it
        if (acceptableLineFound == true)
        {
            run = false;
            validMoves.Add(runAcceptableLine(this.currentBoardState));
            piecesList.RemoveAt(0);
            moveTypeList.RemoveAt(0);
            moveList.RemoveAt(0);
            if (moveList.Count == 0)
            {
                run = true;
                acceptableLineFound = false;
            }
        }
        else
        //Otherwise attempt to find one
        {
            //Start timing the program
            Stopwatch stopwatch = Stopwatch.StartNew();
            int loop = 0;
            run = true;
            while (run == true)
            {
                piecesList.Clear();
                moveTypeList.Clear();
                moveList.Clear();

                System.Random randomNum = new System.Random();
                NextMove nextMove = allMoves[randomNum.Next(allMoves.Count)];

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

                //Add the piece to my list
                piecesList.Add(piece);
                moveTypeList.Add(moveType);
                moveList.Add(nextMove);

                originalBoardState = this.currentBoardState;

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

                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);
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

                    if (inCheck(this.currentBoardState, this.color * -1))
                    {
                        checkOccured = true;
                        bool canblockorcapture = false;
                        //King cannot be null because we are in check
                        coords kingPos = isolatedGetKing(this.currentBoardState, color).position;
                        if (moveList[^1].moveType == "move")
                        {
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].move.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        else
                        {
                            //King cannot be null because we are in check
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].ability.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        if (pieceOpp.baseType == "King" || canblockorcapture == true)
                        {
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
                            this.currentBoardState = originalBoardState_;

                            List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                            float diff = botPoints - oppPoints;
                            if (diff < bestOppMoveDiff)
                            {
                                bestOppMoveDiff = diff;
                                bestOppNextMove = nextMoveOpp;
                            }

                        }
                    }
                    else
                    {
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
                        this.currentBoardState = originalBoardState_;

                        List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        float diff = botPoints - oppPoints;
                        if (diff < bestOppMoveDiff)
                        {
                            bestOppMoveDiff = diff;
                            bestOppNextMove = nextMoveOpp;
                        }
                    }
                }

                if (bestOppNextMove is null)
                {
                    acceptableLineFound = true;
                    break;
                }

                BoardState cloneState2;
                if (bestOppNextMove.moveType == "move")
                {
                    cloneState2 = simulatePieceMove(this, this.currentBoardState, bestOppNextMove.move.p, bestOppNextMove.move.coords);
                }
                else
                {
                    cloneState2 = simulatePieceAbility(this, this.currentBoardState, bestOppNextMove.ability);
                }
                this.currentBoardState = cloneState2;

                List<NextMove> allCurrentMoves = getAllPossibleBotMovesAndAbilities(this, cloneState2, this.color);

                if (allCurrentMoves.Count == 0)
                {
                    gameData.helper.addBotMessage("Sickly Bot Child has declaired this game unwinnable!");
                    break;
                }

                //Cull non ability pawn moves
                System.Random secondRandomNum = new System.Random();
                NextMove nextMove2 = allCurrentMoves[secondRandomNum.Next(allCurrentMoves.Count)];

                if (nextMove2.moveType == "move")
                {
                    if (nextMove2.move.p.baseType == "Pawn" || nextMove2.move.p.baseType == "King")
                    {
                        nextMove2 = allCurrentMoves[secondRandomNum.Next(allCurrentMoves.Count)];
                        if (nextMove2.moveType == "move")
                        {
                            if (nextMove2.move.p.baseType == "Pawn" || nextMove2.move.p.baseType == "King")
                            {
                                nextMove2 = allCurrentMoves[secondRandomNum.Next(allCurrentMoves.Count)];
                            }
                        }
                    }
                }

                Piece piece2;
                coords coords2;
                string moveType2 = nextMove2.moveType;

                if (moveType2 == "move")
                {
                    Move mv2 = nextMove2.move;

                    piece2 = mv2.p;
                    coords2 = mv2.coords;
                }
                else
                {
                    PieceAbility pa2 = nextMove2.ability;

                    piece2 = pa2.piece;
                    coords2 = pa2.coords;
                }

                //Add the piece to my list
                piecesList.Add(piece2);
                moveTypeList.Add(moveType2);
                moveList.Add(nextMove2);

                BoardState originalBoardState2 = this.currentBoardState;

                BoardState cloneState3;
                if (moveType2 == "move")
                {
                    cloneState3 = simulatePieceMove(this, this.currentBoardState, piece2, coords2);
                }
                else
                {
                    cloneState3 = simulatePieceAbility(this, this.currentBoardState, nextMove2.ability);
                }
                this.currentBoardState = cloneState3;

                List<NextMove> allMovesOpp2 = getAllPossibleBotMovesAndAbilities(this, cloneState3, this.color * -1);
                float bestOppMoveDiff2 = +1000;

                //Loop through all opponent moves
                foreach (NextMove nextMoveOpp2 in allMovesOpp2)
                {
                    Piece pieceOpp2;
                    coords coordsOpp2;

                    string moveTypeOpp2 = nextMoveOpp2.moveType;

                    if (moveTypeOpp2 == "move")
                    {
                        Move mv = nextMoveOpp2.move;

                        pieceOpp2 = mv.p;
                        coordsOpp2 = mv.coords;
                    }
                    else
                    {
                        PieceAbility pa = nextMoveOpp2.ability;

                        pieceOpp2 = pa.piece;
                        coordsOpp2 = pa.coords;
                    }

                    if (inCheck(this.currentBoardState, this.color * -1))
                    {
                        checkOccured = true;
                        bool canblockorcapture = false;
                        //King cannot be null because we are in check
                        coords kingPos = isolatedGetKing(this.currentBoardState, color).position;
                        if (moveList[^1].moveType == "move")
                        {
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].move.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp2.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp2.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        else
                        {
                            //King cannot be null because we are in check
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].ability.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp2.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp2.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        if (pieceOpp2.baseType == "King" || canblockorcapture == true)
                        {
                            BoardState originalBoardState_ = this.currentBoardState;
                            BoardState cloneState_;
                            if (moveTypeOpp2 == "move")
                            {
                                cloneState_ = simulatePieceMove(this, this.currentBoardState, pieceOpp2, coordsOpp2);
                            }
                            else
                            {
                                cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp2.ability);
                            }
                            this.currentBoardState = originalBoardState_;

                            List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                            float diff = botPoints - oppPoints;
                            if (diff < bestOppMoveDiff2)
                            {
                                bestOppMoveDiff2 = diff;
                                bestOppNextMove2 = nextMoveOpp2;
                            }
                        }
                    }
                    else
                    {
                        BoardState originalBoardState_ = this.currentBoardState;
                        BoardState cloneState_;
                        if (moveTypeOpp2 == "move")
                        {
                            cloneState_ = simulatePieceMove(this, this.currentBoardState, pieceOpp2, coordsOpp2);
                        }
                        else
                        {
                            cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp2.ability);
                        }
                        this.currentBoardState = originalBoardState_;

                        List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        float diff = botPoints - oppPoints;
                        if (diff < bestOppMoveDiff2)
                        {
                            bestOppMoveDiff2 = diff;
                            bestOppNextMove2 = nextMoveOpp2;
                        }
                    }
                }

                BoardState cloneState4;
                if (bestOppNextMove2.moveType == "move")
                {
                    cloneState4 = simulatePieceMove(this, this.currentBoardState, bestOppNextMove2.move.p, bestOppNextMove2.move.coords);
                }
                else
                {
                    cloneState4 = simulatePieceAbility(this, this.currentBoardState, bestOppNextMove2.ability);
                }
                this.currentBoardState = cloneState4;

                allCurrentMoves = getAllPossibleBotMovesAndAbilities(this, cloneState4, this.color);

                if (allCurrentMoves.Count == 0)
                {
                    gameData.helper.addBotMessage("Sickly Bot Child has declaired this game unwinnable!");
                    break;
                }

                //Cull more pawn and king moves
                System.Random thirdRandomNum = new System.Random();
                NextMove nextMove3 = allCurrentMoves[thirdRandomNum.Next(allCurrentMoves.Count)];

                if (nextMove3.moveType == "move")
                {
                    if (nextMove3.move.p.baseType == "Pawn" || nextMove3.move.p.baseType == "King")
                    {
                        nextMove3 = allCurrentMoves[thirdRandomNum.Next(allCurrentMoves.Count)];
                        if (nextMove3.moveType == "move")
                        {
                            if (nextMove3.move.p.baseType == "Pawn" || nextMove3.move.p.baseType == "King")
                            {
                                nextMove3 = allCurrentMoves[thirdRandomNum.Next(allCurrentMoves.Count)];
                            }
                        }
                    }
                }

                Piece piece3;
                coords coords3;
                string moveType3 = nextMove3.moveType;

                if (moveType3 == "move")
                {
                    Move mv3 = nextMove3.move;

                    piece3 = mv3.p;
                    coords3 = mv3.coords;
                }
                else
                {
                    PieceAbility pa3 = nextMove3.ability;

                    piece3 = pa3.piece;
                    coords3 = pa3.coords;
                }

                //Add the piece to my list
                piecesList.Add(piece3);
                moveTypeList.Add(moveType3);
                moveList.Add(nextMove3);

                BoardState originalBoardState3 = this.currentBoardState;

                BoardState cloneState5;
                if (moveType3 == "move")
                {
                    cloneState5 = simulatePieceMove(this, this.currentBoardState, piece3, coords3);
                }
                else
                {
                    cloneState5 = simulatePieceAbility(this, this.currentBoardState, nextMove3.ability);
                }
                this.currentBoardState = cloneState5;

                List<NextMove> allMovesOpp3 = getAllPossibleBotMovesAndAbilities(this, cloneState5, this.color * -1);
                bestOppMoveDiff = +1000;

                //Loop through all opponent moves
                foreach (NextMove nextMoveOpp3 in allMovesOpp3)
                {
                    Piece pieceOpp3;
                    coords coordsOpp3;

                    string moveTypeOpp3 = nextMoveOpp3.moveType;

                    if (moveTypeOpp3 == "move")
                    {
                        Move mv = nextMoveOpp3.move;

                        pieceOpp3 = mv.p;
                        coordsOpp3 = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = nextMoveOpp3.ability;

                        pieceOpp3 = pa.piece;
                        coordsOpp3 = pa.coords;
                    }

                    if (inCheck(this.currentBoardState, this.color * -1))
                    {
                        checkOccured = true;
                        bool canblockorcapture = false;
                        //King cannot be null because we are in check
                        coords kingPos = isolatedGetKing(this.currentBoardState, color).position;
                        if (moveList[^1].moveType == "move")
                        {
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].move.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp3.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp3.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        else
                        {
                            //King cannot be null because we are in check
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].ability.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp3.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp3.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        if (pieceOpp3.baseType == "King" || canblockorcapture == true)
                        {
                            BoardState originalBoardState2_ = this.currentBoardState;
                            BoardState cloneState2_;
                            if (moveTypeOpp3 == "move")
                            {
                                cloneState2_ = simulatePieceMove(this, this.currentBoardState, pieceOpp3, coordsOpp3);
                            }
                            else
                            {
                                cloneState2_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp3.ability);
                            }
                            this.currentBoardState = originalBoardState2_;

                            List<float> pointsOnBoard = getPointsOnBoardState(cloneState2_, true);
                            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                            float diff = botPoints - oppPoints;
                            if (diff < bestOppMoveDiff)
                            {
                                bestOppMoveDiff = diff;
                                bestOppNextMove3 = nextMoveOpp3;
                            }
                        }
                    }
                    else
                    {
                        BoardState originalBoardState2_ = this.currentBoardState;
                        BoardState cloneState2_;
                        if (moveTypeOpp3 == "move")
                        {
                            cloneState2_ = simulatePieceMove(this, this.currentBoardState, pieceOpp3, coordsOpp3);
                        }
                        else
                        {
                            cloneState2_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp3.ability);
                        }
                        this.currentBoardState = originalBoardState2_;

                        List<float> pointsOnBoard = getPointsOnBoardState(cloneState2_, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        float diff = botPoints - oppPoints;
                        if (diff < bestOppMoveDiff)
                        {
                            bestOppMoveDiff = diff;
                            bestOppNextMove3 = nextMoveOpp3;
                        }
                    }


                }

                BoardState cloneState6;
                if (bestOppNextMove3.moveType == "move")
                {
                    cloneState6 = simulatePieceMove(this, this.currentBoardState, bestOppNextMove3.move.p, bestOppNextMove3.move.coords);
                }
                else
                {
                    cloneState6 = simulatePieceAbility(this, this.currentBoardState, bestOppNextMove3.ability);
                }
                this.currentBoardState = cloneState6;

                allCurrentMoves = getAllPossibleBotMovesAndAbilities(this, cloneState6, this.color);

                if (allCurrentMoves.Count == 0)
                {
                    gameData.helper.addBotMessage("Sickly Bot Child has declaired this game unwinnable!");
                    break;
                }

                System.Random fourthRandomNum = new System.Random();
                NextMove nextMove4 = allCurrentMoves[fourthRandomNum.Next(allCurrentMoves.Count)];

                //More pawn and king culling!
                if (nextMove4.moveType == "move")
                {
                    if (nextMove4.move.p.baseType == "Pawn" || nextMove4.move.p.baseType == "King")
                    {
                        nextMove4 = allCurrentMoves[fourthRandomNum.Next(allCurrentMoves.Count)];
                        if (nextMove4.moveType == "move")
                        {
                            if (nextMove4.move.p.baseType == "Pawn" || nextMove4.move.p.baseType == "King")
                            {
                                nextMove4 = allCurrentMoves[fourthRandomNum.Next(allCurrentMoves.Count)];
                            }
                        }
                    }
                }

                Piece piece4;
                coords coords4;
                string moveType4 = nextMove4.moveType;

                if (moveType4 == "move")
                {
                    Move mv4 = nextMove4.move;

                    piece4 = mv4.p;
                    coords4 = mv4.coords;
                }
                else
                {
                    PieceAbility pa4 = nextMove4.ability;

                    piece4 = pa4.piece;
                    coords4 = pa4.coords;
                }

                //Add the piece to my list
                piecesList.Add(piece4);
                moveTypeList.Add(moveType4);
                moveList.Add(nextMove4);

                BoardState originalBoardState4 = this.currentBoardState;

                BoardState cloneState7;
                if (moveType4 == "move")
                {
                    cloneState7 = simulatePieceMove(this, this.currentBoardState, piece4, coords4);
                }
                else
                {
                    cloneState7 = simulatePieceAbility(this, this.currentBoardState, nextMove4.ability);
                }
                this.currentBoardState = cloneState7;

                List<NextMove> allMovesOpp4 = getAllPossibleBotMovesAndAbilities(this, cloneState7, this.color * -1);
                bestOppMoveDiff = +1000;

                //Loop through all opponent moves
                foreach (NextMove nextMoveOpp4 in allMovesOpp4)
                {
                    Piece pieceOpp4;
                    coords coordsOpp4;

                    string moveTypeOpp4 = nextMoveOpp4.moveType;

                    if (moveTypeOpp4 == "move")
                    {
                        Move mv = nextMoveOpp4.move;

                        pieceOpp4 = mv.p;
                        coordsOpp4 = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = nextMoveOpp4.ability;

                        pieceOpp4 = pa.piece;
                        coordsOpp4 = pa.coords;
                    }

                    if (inCheck(this.currentBoardState, this.color * -1))
                    {
                        checkOccured = true;
                        bool canblockorcapture = false;
                        //King cannot be null because we are in check
                        coords kingPos = isolatedGetKing(this.currentBoardState, color).position;
                        if (moveList[^1].moveType == "move")
                        {
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].move.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp4.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp4.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        else
                        {
                            //King cannot be null because we are in check
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].ability.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp4.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp4.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        if (pieceOpp4.baseType == "King" || canblockorcapture == true)
                        {
                            BoardState originalBoardState3_ = this.currentBoardState;
                            BoardState cloneState3_;
                            if (moveTypeOpp4 == "move")
                            {
                                cloneState3_ = simulatePieceMove(this, this.currentBoardState, pieceOpp4, coordsOpp4);
                            }
                            else
                            {
                                cloneState3_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp4.ability);
                            }
                            this.currentBoardState = originalBoardState3_;

                            List<float> pointsOnBoard = getPointsOnBoardState(cloneState3_, true);
                            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                            float diff = botPoints - oppPoints;
                            if (diff < bestOppMoveDiff)
                            {
                                bestOppMoveDiff = diff;
                                bestOppNextMove4 = nextMoveOpp4;
                            }
                        }
                    }
                    else
                    {
                        BoardState originalBoardState3_ = this.currentBoardState;
                        BoardState cloneState3_;
                        if (moveTypeOpp4 == "move")
                        {
                            cloneState3_ = simulatePieceMove(this, this.currentBoardState, pieceOpp4, coordsOpp4);
                        }
                        else
                        {
                            cloneState3_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp4.ability);
                        }
                        this.currentBoardState = originalBoardState3_;

                        List<float> pointsOnBoard = getPointsOnBoardState(cloneState3_, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        float diff = botPoints - oppPoints;
                        if (diff < bestOppMoveDiff)
                        {
                            bestOppMoveDiff = diff;
                            bestOppNextMove4 = nextMoveOpp4;
                        }
                    }
                }

                BoardState cloneState8;
                if (bestOppNextMove4.moveType == "move")
                {
                    cloneState8 = simulatePieceMove(this, this.currentBoardState, bestOppNextMove4.move.p, bestOppNextMove4.move.coords);
                }
                else
                {
                    cloneState8 = simulatePieceAbility(this, this.currentBoardState, bestOppNextMove4.ability);
                }
                this.currentBoardState = cloneState8;

                allCurrentMoves = getAllPossibleBotMovesAndAbilities(this, cloneState8, this.color);

                if (allCurrentMoves.Count == 0)
                {
                    gameData.helper.addBotMessage("Sickly Bot Child has declaired this game unwinnable!");
                    break;
                }

                System.Random fifthRandomNum = new System.Random();
                NextMove nextMove5 = allCurrentMoves[fifthRandomNum.Next(allCurrentMoves.Count)];

                //More pawn and king culling!
                if (nextMove5.moveType == "move")
                {
                    if (nextMove5.move.p.baseType == "Pawn" || nextMove5.move.p.baseType == "King")
                    {
                        nextMove5 = allCurrentMoves[fourthRandomNum.Next(allCurrentMoves.Count)];
                        if (nextMove5.moveType == "move")
                        {
                            if (nextMove5.move.p.baseType == "Pawn" || nextMove5.move.p.baseType == "King")
                            {
                                nextMove5 = allCurrentMoves[fourthRandomNum.Next(allCurrentMoves.Count)];
                            }
                        }
                    }
                }

                Piece piece5;
                coords coords5;
                string moveType5 = nextMove5.moveType;

                if (moveType5 == "move")
                {
                    Move mv5 = nextMove5.move;

                    piece5 = mv5.p;
                    coords5 = mv5.coords;
                }
                else
                {
                    PieceAbility pa5 = nextMove5.ability;

                    piece5 = pa5.piece;
                    coords5 = pa5.coords;
                }

                //Add the piece to my list
                piecesList.Add(piece5);
                moveTypeList.Add(moveType5);
                moveList.Add(nextMove5);

                BoardState originalBoardState5 = this.currentBoardState;

                BoardState cloneState9;
                if (moveType5 == "move")
                {
                    cloneState9 = simulatePieceMove(this, this.currentBoardState, piece5, coords5);
                }
                else
                {
                    cloneState9 = simulatePieceAbility(this, this.currentBoardState, nextMove5.ability);
                }
                this.currentBoardState = cloneState9;

                List<NextMove> allMovesOpp5 = getAllPossibleBotMovesAndAbilities(this, cloneState9, this.color * -1);
                bestOppMoveDiff = +1000;

                //Loop through all opponent moves
                foreach (NextMove nextMoveOpp5 in allMovesOpp5)
                {
                    Piece pieceOpp5;
                    coords coordsOpp5;

                    string moveTypeOpp5 = nextMoveOpp5.moveType;

                    if (moveTypeOpp5 == "move")
                    {
                        Move mv = nextMoveOpp5.move;

                        pieceOpp5 = mv.p;
                        coordsOpp5 = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = nextMoveOpp5.ability;

                        pieceOpp5 = pa.piece;
                        coordsOpp5 = pa.coords;
                    }

                    if (inCheck(this.currentBoardState, this.color * -1))
                    {
                        bool canblockorcapture = false;
                        coords kingPos;
                        if (isolatedGetKing(this.currentBoardState, color) is not null)
                        {
                            kingPos = isolatedGetKing(this.currentBoardState, color).position; 
                        } 
                        else
                        {
                            break;   
                        }
                        if (moveList[^1].moveType == "move")
                        {
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].move.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp5.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp5.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        else
                        {
                            //King cannot be null because we are in check
                            List<coords> pieceCoords = GetLinePoints(moveList[^1].ability.coords, kingPos);
                            foreach (coords listcoords in pieceCoords)
                            {
                                if (string.Equals(coordsOpp5.x.ToString(), listcoords.x.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(coordsOpp5.y.ToString(), listcoords.y.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    canblockorcapture = true;
                                }
                            }
                        }
                        if (pieceOpp5.baseType == "King" || canblockorcapture == true)
                        {
                            BoardState originalBoardState4_ = this.currentBoardState;
                            BoardState cloneState4_;
                            if (moveTypeOpp5 == "move")
                            {
                                cloneState4_ = simulatePieceMove(this, this.currentBoardState, pieceOpp5, coordsOpp5);
                            }
                            else
                            {
                                cloneState4_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp5.ability);
                            }
                            this.currentBoardState = originalBoardState4_;

                            List<float> pointsOnBoard = getPointsOnBoardState(cloneState4_, true);
                            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                            float diff = botPoints - oppPoints;
                            if (diff < bestOppMoveDiff)
                            {
                                bestOppMoveDiff = diff;
                                bestOppNextMove5 = nextMoveOpp5;
                            }
                        }
                    }
                    else
                    {
                        BoardState originalBoardState4_ = this.currentBoardState;
                        BoardState cloneState4_;
                        if (moveTypeOpp5 == "move")
                        {
                            cloneState4_ = simulatePieceMove(this, this.currentBoardState, pieceOpp5, coordsOpp5);
                        }
                        else
                        {
                            cloneState4_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp5.ability);
                        }
                        this.currentBoardState = originalBoardState4_;

                        List<float> pointsOnBoard = getPointsOnBoardState(cloneState4_, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                        float diff = botPoints - oppPoints;
                        if (diff < bestOppMoveDiff)
                        {
                            bestOppMoveDiff = diff;
                            bestOppNextMove5 = nextMoveOpp5;
                        }
                    }

                }

                BoardState cloneState10;
                if (bestOppNextMove5.moveType == "move")
                {
                    cloneState10 = simulatePieceMove(this, this.currentBoardState, bestOppNextMove5.move.p, bestOppNextMove5.move.coords);
                }
                else
                {
                    cloneState10 = simulatePieceAbility(this, this.currentBoardState, bestOppNextMove5.ability);
                }
                this.currentBoardState = cloneState10;

                List<float> totalPointsOnBoard = getPointsOnBoardState(cloneState10, true);
                float totalBotPoints = this.color == 1 ? totalPointsOnBoard[0] : totalPointsOnBoard[1];
                float totalOppPoints = this.color == -1 ? totalPointsOnBoard[0] : totalPointsOnBoard[1];

                float totalDiff = totalBotPoints - totalOppPoints;
                float finalDiff = totalDiff - acctualDiff;

                if (isolatedGetKing(this.currentBoardState, this.color * -1) is null)
                {
                    acceptableLineFound = true;
                    gameData.helper.addBotMessage("Sickly Bot Child found a good line!");
                }
                else if (finalDiff >= moveDiff)
                {
                    acceptableLineFound = true;
                    gameData.helper.addBotMessage("Sickly Bot Child found a good line!");
                }
                else if (checkOccured == true)
                {
                    acceptableLineFound = true;
                    gameData.helper.addBotMessage("Sickly Bot Child found a good line!");
                }

                //Reset the currentBoardState and go to the next move
                this.currentBoardState = originalBoardState;
                checkOccured = false;

                TimeSpan runTime = stopwatch.Elapsed;
                float totalMS = (float)runTime.TotalSeconds * 1000;
                loop += 1;

                if ((totalMS > penaltyTimer) || (acceptableLineFound == true))
                {
                    run = false;
                    UnityEngine.Debug.Log("Time Elapsed: " + totalMS.ToString() + " Loops: " + loop.ToString());
                }
            }
        }

        if (validMoves.Count == 0)
        {
            if (acceptableLineFound == true)
            {
                validMoves.Add(runAcceptableLine(originalBoardState));
                piecesList.RemoveAt(0);
                moveTypeList.RemoveAt(0);
                moveList.RemoveAt(0);
                if (moveList.Count == 0)
                {
                    run = true;
                    acceptableLineFound = false;
                }
            }
            else
            {
                if (turn == 1)
                {
                    System.Random randomNum = new System.Random();
                    NextMove move2return = allMoves[randomNum.Next(allMoves.Count)];
                    if (move2return.moveType == "move")
                    {
                        if (move2return.move.p.baseType != "Pawn")
                        {
                            move2return = allMoves[randomNum.Next(allMoves.Count)];
                        }
                    }
                    else
                    {
                        move2return = allMoves[randomNum.Next(allMoves.Count)];
                    }
                    return move2return;
                }
                else
                {
                    System.Random randomNum = new System.Random();
                    NextMove move2return = allMoves[randomNum.Next(allMoves.Count)];
                    if (move2return.moveType == "move")
                    {
                        if (move2return.move.p.baseType == "Pawn" || move2return.move.p.baseType == "King")
                        {
                            move2return = allMoves[randomNum.Next(allMoves.Count)];
                        }
                    }
                    else
                    {
                        if (move2return.ability.piece.baseType == "Pawn" || move2return.ability.piece.baseType == "King")
                        {
                            move2return = allMoves[randomNum.Next(allMoves.Count)];
                        }
                    }
                    return move2return;
                }
            }
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
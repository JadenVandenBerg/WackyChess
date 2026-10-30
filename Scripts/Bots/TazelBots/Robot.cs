using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.Text.RegularExpressions;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
//To Do:
//Organize the code
//Move everything into functions
//Optimize
//Make the rest of the bot  

public class Robot : BotTemplate
{
    bool hasSoloPiece;
    Piece mainPiece;
    Piece secondPiece;
    //int developChance = 15;
    List<Piece> piecesList = new List<Piece>();
    List<NextMove> moveList = new List<NextMove>();
    List<string> moveTypeList = new List<string>();
    //bool run = true;
    int turn = 0;
    NextMove lastMove = null;
    NextMove lastLastMove = null;

    //Maybe move this somewhere else
    bool acceptableLineFound = false;
    float penaltyTimer = 4800;

    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public Robot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Robot";

        //This function populates the pieces variable
        choosePieces();
    }

    /*public BoardState simulateMove()
    {

    }
    */

    public NextMove runAcceptableLine(BoardState currentBoardstate)
    {
        int num = 0;
        List<Color> colors = new() { Color.orange, Color.yellow, Color.blue, Color.purple, Color.black };
        foreach (NextMove listmove in moveList)
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
        //run = true;
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

    public List<NextMove> attackUnguardedOrCheck()
    {
        //Returned list can be empty
        //make sure you set the boardstate you want to "this.currentBoardState" before calling

        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        List<NextMove> tierZeroMove = new List<NextMove>();
        List<float> pieceScoresT0 = new List<float>();
        Piece oppKing = filterPieces("King", this.opponentPieces)[0];
        coords oppKingPos = oppKing.position;

        foreach (NextMove nextMove in allMoves)
        {
            Piece piece;
            coords coords;
            string moveType = nextMove.moveType;
            bool cull = false;
            //bool jailing = false;
            //float jailPiecePts = 0;
            //Maybe add jailing support later

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

            //If it currently has a piece in jail, don't move
            List<Piece> allPiecesOppBeforeSim = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, this.color * -1);
            foreach (Piece oppPiece in allPiecesOppBeforeSim)
            {
                coords oppPos = oppPiece.position;
                if (oppPos.x == coords.x && oppPos.y == coords.y && HelperFunctions.checkState(piece, PieceState.Dematerialized) == false)
                {
                    cull = true;
                    //jailing = true;
                    //jailPiecePts = oppPiece.points;
                }
            }

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

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);
            List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(cloneState, this.color * -1);
            List<NextMove> allMoves2nd = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color);

            foreach (NextMove oppMove in allMovesOpp)
            {
                Piece badPiece;
                coords badCoords;
                string moveTypeOpp = oppMove.moveType;

                if (moveTypeOpp == "move")
                {
                    Move mv = oppMove.move;

                    badPiece = mv.p;
                    badCoords = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = oppMove.ability;

                    badPiece = pa.piece;
                    badCoords = pa.coords;
                }

                if (coords.x == badCoords.x && coords.y == badCoords.y)
                {
                    if (badPiece.baseType == "King" && HelperFunctions.checkState(badPiece, PieceState.Delayed) == false)
                    {
                        BoardState cloneStateOppKing;
                        if (moveTypeOpp == "move")
                        {
                            cloneStateOppKing = simulatePieceMove(this, this.currentBoardState, badPiece, badCoords);
                        }
                        else
                        {
                            cloneStateOppKing = simulatePieceAbility(this, this.currentBoardState, oppMove.ability);
                        }

                        bool pieceProtected = false;
                        List<NextMove> allMoves3rd = getAllPossibleBotMovesAndAbilities(this, cloneStateOppKing, this.color);
                        foreach (NextMove simMove in allMoves3rd)
                        {
                            Piece simPiece;
                            coords simCoords;
                            string moveTypeSim = simMove.moveType;

                            if (moveTypeSim == "move")
                            {
                                Move mv = simMove.move;

                                simPiece = mv.p;
                                simCoords = mv.coords;
                            }
                            else // moveType == "ability" guarenteed
                            {
                                PieceAbility pa = simMove.ability;

                                simPiece = pa.piece;
                                simCoords = pa.coords;
                            }

                            if (simCoords.x == oppKingPos.x && simCoords.y == oppKingPos.y)
                            {
                                pieceProtected = true;
                            }
                        }

                        if (pieceProtected == false)
                        {
                            cull = true;
                        }
                    }
                    else if (badPiece.baseType != "King")
                    {
                        cull = true;
                    }
                }
            }

            if (cull == false)
            {
                foreach (Piece oppPiece in allPiecesOpp)
                {
                    if (coords.x == oppPiece.position.x && coords.y == oppPiece.position.y)
                    {
                        tierZeroMove.Add(nextMove);
                        pieceScoresT0.Add(oppPiece.points);

                    }
                }

                //If I can kill the king here, it is check.
                foreach (NextMove nextMove2nd in allMoves2nd)
                {
                    coords coords2nd;
                    string moveType2nd = nextMove2nd.moveType;

                    if (moveType == "move")
                    {
                        Move mv = nextMove2nd.move;
                        if (nextMove2nd.move is not null)
                        {
                            coords2nd = mv.coords; //This line is null for some reason
                                                   //gameData.helper.addBotMessage("Coords is not null");
                        }
                        else
                        {
                            coords2nd = new coords(0, 0);
                        }
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = nextMove2nd.ability;
                        if (nextMove2nd.ability is not null)
                        {
                            coords2nd = pa.coords;
                        }
                        else
                        {
                            coords2nd = new coords(0, 0);
                        }
                    }

                    if (coords2nd.x == oppKingPos.x && coords2nd.y == oppKingPos.y)
                    {
                        tierZeroMove.Add(nextMove);
                        pieceScoresT0.Add(15);
                        //Check has a value of 15 points because
                        //I can generally do any other move right after
                    }
                }
            }
        }
        float highest = -1000;
        for (int i = tierZeroMove.Count - 1; i >= 0; i--)
        {
            NextMove bestMove = tierZeroMove[i];
            float moveScore = pieceScoresT0[i];
            if (moveScore > highest)
            {
                highest = moveScore;
                validMoves.Clear();
                validMoves.Add(bestMove);
            }
        }

        return validMoves;
    }

    public List<NextMove> protect()
    {
        //Returns a list of valid moves. This list CAN be empty
        //Remember to set the boardState to this.currentBoardState before calling

        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> tierProtMove = new List<NextMove>();
        List<float> pieceScoresTProt = new List<float>();
        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);
        List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, this.color * -1);
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);


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

            bool currentSquareSafe = true;
            bool moveToSquareSafe = true;
            float deadPiecepts = 0;
            if (piece.points > 3)
            {
                foreach (NextMove oppMove in allMovesOpp)
                {
                    Piece pieceOpp;
                    coords coordsOpp;
                    string moveTypeOpp = oppMove.moveType;

                    if (moveTypeOpp == "move")
                    {
                        Move mv = oppMove.move;

                        pieceOpp = mv.p;
                        coordsOpp = mv.coords;
                    }
                    else // moveType == "ability" guarenteed
                    {
                        PieceAbility pa = oppMove.ability;

                        pieceOpp = pa.piece;
                        coordsOpp = pa.coords;
                    }

                    if (coordsOpp.x == piece.position.x && coordsOpp.y == piece.position.y)
                    {
                        currentSquareSafe = false;
                    }
                    else if (coordsOpp.x == coords.x && coordsOpp.y == coords.y)
                    {
                        moveToSquareSafe = false;
                    }
                    foreach (Piece oppPiece in allPiecesOpp)
                    {
                        if (oppPiece.position.x == coords.x && oppPiece.position.y == coords.y)
                        {
                            BoardState cloneState;
                            if (moveType == "move")
                            {
                                cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                            }
                            else
                            {
                                cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
                            }

                            List<NextMove> allMovesOppCloneState = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

                            bool safe = true;
                            foreach (NextMove cloneOppMove in allMovesOppCloneState)
                            {
                                Piece pieceOppClone;
                                coords coordsOppClone;
                                string moveTypeOppClone = cloneOppMove.moveType;

                                if (moveTypeOppClone == "move")
                                {
                                    Move mv = cloneOppMove.move;

                                    pieceOppClone = mv.p;
                                    coordsOppClone = mv.coords;
                                }
                                else // moveType == "ability" guarenteed
                                {
                                    PieceAbility pa = cloneOppMove.ability;

                                    pieceOppClone = pa.piece;
                                    coordsOppClone = pa.coords;
                                }

                                if (coordsOppClone.x == coords.x && coordsOppClone.y == coords.y)
                                {
                                    safe = false;
                                }

                            }
                            if (safe == true)
                            {
                                deadPiecepts += oppPiece.points;
                            }
                        }
                    }
                }
            }
            if (moveToSquareSafe == true && currentSquareSafe == false)
            {
                tierProtMove.Add(nextMove);
                float pointVal = piece.points + deadPiecepts;
                pieceScoresTProt.Add(pointVal);
            }
        }

        float highest = -1000;
        for (int i = tierProtMove.Count - 1; i >= 0; i--)
        {
            NextMove bestMove = tierProtMove[i];
            float moveScore = pieceScoresTProt[i];
            if (moveScore > highest)
            {
                highest = moveScore;
                validMoves.Clear();
                validMoves.Add(bestMove);
            }
        }

        return validMoves;
    }

    /*   
    public NextMove findSpace()
       {

       }*/

    override
    public NextMove nextMove()
    {
        turn += 1;
        //Initialize for later
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        List<NextMove> tierZeroMove = new List<NextMove>();
        List<float> pieceScoresT0 = new List<float>();
        List<NextMove> tierProtMove = new List<NextMove>();
        List<float> pieceScoresTProt = new List<float>();

        //int bestPiece = 0;
        //int secondBestPiece = 0;
        //bool hasSoloPiece = false;
        //Get enemy king's position
        Piece oppKing = filterPieces("King", this.opponentPieces)[0];
        coords oppKingPos = oppKing.position;



        if (acceptableLineFound == true)
        {
            runAcceptableLine(this.currentBoardState);
            piecesList.RemoveAt(0);
            moveTypeList.RemoveAt(0);
            moveList.RemoveAt(0);
            if (moveList.Count == 0)
            {
                acceptableLineFound = false;
            }
        }

        //Protect pieces in danger (only if they are good enough)
        if (validMoves.Count == 0)
        {

            List<NextMove> possibleMoves = protect();
            validMoves = possibleMoves;
            /*
            foreach (NextMove moveProt in possibleMoves)
            {
                validMoves.Add(moveProt);
            }//*/
        }

        if (validMoves.Count == 0)
        {
            List<NextMove> possibleMoves = attackUnguardedOrCheck();
            validMoves = possibleMoves;
            /*foreach (NextMove moveCheck in possibleMoves)
            {
                validMoves.Add(moveCheck);
            }*/
        }

        //Idk how this even happend bro just do a random move
        if (validMoves.Count == 0)
        {
            System.Random rand0 = new System.Random();
            int rndIdx0 = rand0.Next(allMoves.Count);
            validMoves.Add(allMoves[rndIdx0]);
            gameData.helper.addBotMessage("Robot didn't return a move, executing random move instead");
        }

        Stopwatch stopwatch = Stopwatch.StartNew();

        while (true)
        {
            TimeSpan runTime = stopwatch.Elapsed;
            float totalMS = (float)runTime.TotalSeconds * 1000;
            if (totalMS > penaltyTimer)
            {
                break;
            }
        }

        //Pick a random move from our list of tied moves
        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
        if (lastMove is not null)
        {
            lastLastMove = lastMove;
        }
        lastMove = move;

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

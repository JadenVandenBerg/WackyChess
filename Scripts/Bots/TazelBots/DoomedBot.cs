using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.Text.RegularExpressions;
using System;

public class DoomedBot : BotTemplate
{
    bool hasSoloPiece;
    Piece mainPiece;
    Piece secondPiece;
    int developChance = 15;
    int turn = 0;
    NextMove lastMove = null;
    NextMove lastLastMove = null;
    List<string> appliedCurses = new List<string>();
    List<string> availableCurses = new List<string>();
    List<string> allCurses = new List<string>();
    List<Piece> petrifiedPieces = new List<Piece>();
    List<coords> mazeWalls = new List<coords>();

    //The constructor, this function gets called when a new bot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public DoomedBot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Doomed Bot";

        //Populate Lists
        allCurses.Add("petrification");
        allCurses.Add("the beast");
        allCurses.Add("the labrynth");
        //allCurses.Add("the dead");
        //allCurses.Add("darkness");
        //allCurses.Add("the plague");
        //allCurses.Add("remorse");
        //allCurses.Add("hopelessness");

        availableCurses = allCurses;

        //This function populates the pieces variable
        choosePieces();
    }

    public BoardState simulateMove(BoardState bs, NextMove nextMove)
    {
        BoardState cloneState;
        if (nextMove.moveType == "move")
        {
            cloneState = simulatePieceMove(this, bs, nextMove.move.p, nextMove.move.coords);
        }
        else
        {
            cloneState = simulatePieceAbility(this, bs, nextMove.ability);
        }
        return cloneState;
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

    public List<NextMove> getMoves(BoardState bs)
    {
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, bs, this.color);
        return allMoves;
    }

    public List<NextMove> getOppMoves(BoardState bs)
    {
        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, bs, this.color * -1);
        return allMovesOpp;
    }
    public bool checkForSafeMove(BoardState bs, NextMove nextMove)
    {
        Piece oppKing = filterPieces("King", this.opponentPieces)[0];
        coords oppKingPos = oppKing.position;
        List<NextMove> allMovesOpp = getOppMoves(bs);
        List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(bs, this.color * -1);
        Piece piece;
        coords coords;
        string moveType = nextMove.moveType;
        bool cull = false;
        bool invinsible = false;

        if (moveType == "move")
        {
            Move mv = nextMove.move;

            piece = mv.p;
            coords = mv.coords;
        }
        else
        {
            PieceAbility pa = nextMove.ability;

            piece = pa.piece;
            coords = pa.coords;
        }

        if (HelperFunctions.checkState(piece, PieceState.Medusa))
        {
            foreach (Piece oppPiece in allPiecesOpp)
            {
                if (coords.x == oppPiece.position.x && coords.y == oppPiece.position.y)
                {
                    invinsible = true;
                }
            }
        }

        if (HelperFunctions.checkState(piece, PieceState.CaptureTheFlag))
        {
            if (this.color == 1)
            {
                if (coords.y < 3)
                {
                    invinsible = true;
                }
            }
            else
            {
                if (coords.y > 6)
                {
                    invinsible = true;
                }
            }
        }

        if (HelperFunctions.checkState(piece, PieceState.Crook) || HelperFunctions.checkState(piece, PieceState.Dematerialized) || piece.collateralType == 1) //Change to piece states and such do the same for protect function
        {
            invinsible = true;
        }

        if (!invinsible)
        {
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
                else
                {
                    PieceAbility pa = oppMove.ability;

                    badPiece = pa.piece;
                    badCoords = pa.coords;
                }

                if (badPiece is null)
                {
                    cull = true;
                }
                if (coords.x == badCoords.x && coords.y == badCoords.y && cull == false)
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
                        List<NextMove> allMoves3rd = getMoves(bs);
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
                            else
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
                BoardState clonestate = simulateMove(bs, nextMove);
                List<NextMove> allMovesOpp2 = getOppMoves(bs);

                foreach (NextMove oppMove in allMovesOpp2)
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
                    else
                    {
                        PieceAbility pa = oppMove.ability;

                        badPiece = pa.piece;
                        badCoords = pa.coords;
                    }

                    if (badCoords.x == coords.x && badCoords.y == coords.y)
                    {
                        cull = true;
                    }
                }
            }
        }

        return !cull;
    }

    override
    public NextMove nextMove()
    {
        turn += 1;

        if (turn == 1)
        {
            int loopsSinceWall = 0;
            for (int i = 1; i < 9; i++) //x
            {
                for (int j = 3; j < 7; j++) //y
                {
                    int chance = 0; int total = 5;
                    if (mazeWalls.Count > 0)
                    {
                        if (mazeWalls[mazeWalls.Count - 1].x == i && mazeWalls[mazeWalls.Count - 1].y == j - 1)
                        {
                            total += 2;
                        }
                    }
                    chance += loopsSinceWall;

                    System.Random rand4maze = new System.Random();
                    int rndNum = rand4maze.Next(total);
                    if (rndNum <= chance)
                    {
                        mazeWalls.Add(new coords(i, j));
                        loopsSinceWall = -1;
                    }
                    else
                    {
                        loopsSinceWall++;
                    }
                }
            }
        }

        //Curse stuff:
        System.Random randCurse = new System.Random();
        int randomNum = randCurse.Next(100);

        if (randomNum < 10)
        {
            //Give a curse
            int randomNum2 = randCurse.Next(availableCurses.Count);
            appliedCurses.Add(availableCurses[randomNum2]);
            gameData.helper.addBotMessage("DoomedBot has been cursed by the curse of " + availableCurses[randomNum2] + ".");
            availableCurses.Remove(availableCurses[randomNum2]);
        }

        foreach (string curse in appliedCurses)
        {
            if (curse == allCurses[0] || curse == allCurses[1])
            {
                foreach (Piece piece in petrifiedPieces)
                {
                    Color color = new Color(0.6f, 0.6f, 0.0f, 1.0f); //This is yellowish rn not grey it should be grey.
                    HelperFunctions.highlightSquare(HelperFunctions.findSquare(piece.position.x, piece.position.y), color);
                }
            }

            if (curse == allCurses[2])
            {
                //Show Walls
                foreach (coords wallpos in mazeWalls)
                {
                    System.Random rndValue = new System.Random();
                    float rndColorModifier = rndValue.Next(40);
                    Color wallColor = new Color(0.6f, 0.6f, 0.0f + (rndColorModifier / 100), 1.0f);
                    HelperFunctions.highlightSquare(HelperFunctions.findSquare(wallpos.x, wallpos.y), wallColor);
                }
            }
        }













        //Initialize for later
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getMoves(this.currentBoardState);
        List<NextMove> tierZeroMove = new List<NextMove>();
        List<float> pieceScoresT0 = new List<float>();
        List<NextMove> tierProtMove = new List<NextMove>();
        List<float> pieceScoresTProt = new List<float>();

        //Get enemy king's position
        Piece oppKing = filterPieces("King", this.opponentPieces)[0];
        coords oppKingPos = oppKing.position;

        //Protect pieces in danger (only if they are good enough)
        if (validMoves.Count == 0)
        {
            List<NextMove> allMovesOpp = getOppMoves(this.currentBoardState);
            List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, this.color * -1);

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

                                List<NextMove> allMovesOppCloneState = getOppMoves(cloneState);

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
        }

        if (validMoves.Count == 0)
        {
            //Snag them ungaurded pieces
            //To Do:
            //Check if my piece is gaurded. If it is, ignore king attacks
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

                List<NextMove> allMovesOpp = getOppMoves(cloneState);
                List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(cloneState, this.color * -1);
                List<NextMove> allMoves2nd = getMoves(cloneState);

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
                        if (badPiece is null)
                        {
                            cull = true;
                            gameData.helper.addBotMessage("Crisis Averted!");
                        }
                        if (badPiece.baseType == "King" && HelperFunctions.checkState(badPiece, PieceState.Delayed) == false && cull == false)
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
                            List<NextMove> allMoves3rd = getMoves(cloneStateOppKing);
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
        }

        if (validMoves.Count == 0)
        {
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
                                }
                            }
                            else if (color == 1)
                            {
                                if ((coords.y == 3 && coords.x == 3) || (coords.y == 3 && coords.x == 6))
                                {
                                    validMoves.Add(nextMove);
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
                                }
                            }
                        }
                    }
                }
            }
        }

        if (validMoves.Count == 0)
        {
            int randTotal = 11 + turn;
            System.Random randNum = new System.Random();
            int randomFireEmoji = randNum.Next(randTotal);

            if (randomFireEmoji <= developChance)
            {
                developChance -= 1;
                foreach (NextMove nextMove in allMoves)
                {
                    //Move pawns into wall formation
                    //Develop on the sides to take space in the middle
                    //To do: Move peices that have not moved yet
                    Piece piece;
                    coords coords;
                    string moveType = nextMove.moveType;
                    bool cull = false;

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

                    if (lastLastMove is not null)
                    {
                        if (nextMove == lastLastMove)
                        {
                            cull = true;
                        }
                    }

                    if (moveType != "move")
                    {
                        if (HelperFunctions.checkAbility(piece, PieceAbilities.Spawn))
                        {
                            validMoves.Add(nextMove);
                        }
                    }

                    //Don't move our awesome middle pawns
                    if (piece.baseType == "Pawn" && piece.position.x != 4 && piece.position.x != 5 && turn < 13)
                    {
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

                        List<NextMove> allMovesOpp = getOppMoves(cloneState);

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
                                cull = true;
                            }
                        }
                        if (cull == false)
                        {
                            tierZeroMove.Add(nextMove);
                        }
                    }
                    else if (piece.baseType != "King" && (turn < 11 && (piece.baseType == "Bishop" || piece.baseType == "Knight")) == false)
                    {
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

                        List<NextMove> allMovesOpp = getOppMoves(cloneState);

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
                                cull = true;
                            }
                        }
                        if (cull == false)
                        {
                            tierZeroMove.Add(nextMove);
                        }
                    }
                }
                for (int i = tierZeroMove.Count - 1; i >= 0; i--)
                {
                    NextMove bestMove = tierZeroMove[i];
                    validMoves.Add(bestMove);
                    //Maybe add something here later
                }
            }
            else
            {
                float bestMoveDiff = -1000;
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
                    else
                    {
                        PieceAbility pa = nextMove.ability;

                        piece = pa.piece;
                        coords = pa.coords;
                    }

                    BoardState originalBoardState = this.currentBoardState;

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

                    List<NextMove> allMovesOpp = getOppMoves(cloneState);

                    NextMove bestOppNextMove;
                    float bestOppMoveDiff = +1000;

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
                            validMoves.Clear();
                        }

                        bestMoveDiff = bestOppMoveDiff;
                        validMoves.Add(nextMove);
                    }
                    this.currentBoardState = originalBoardState;
                }
            }

        }

        //Idk how this even happend bro just do a random move
        if (validMoves.Count == 0)
        {
            System.Random rand0 = new System.Random();
            int rndIdx0 = rand0.Next(allMoves.Count);
            validMoves.Add(allMoves[rndIdx0]);
            gameData.helper.addBotMessage("DoomedBot didn't return a move, executing random move instead");
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

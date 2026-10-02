using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.Text.RegularExpressions;
using System;

//To do: 
// Maybe lattermate functionality
// Update speedrunner to only use very few specific pieces and to keep them safe
// Maybe add the piece taking and checking func stuff from last time

public class AggroBot : BotTemplate
{
    bool hasSoloPiece;
    Piece mainPiece;
    Piece secondPiece;
    int developChance = 15;
    int turn = 0;
    NextMove lastMove = null;
    NextMove lastLastMove = null;
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public AggroBot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Aggro Bot";

        //This function populates the pieces variable
        choosePieces();
    }

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
        //Loop through all pieces and get main piece
        /*foreach (Piece piece in pieces)
        {
            int currentPieceValue = 0;
            if (piece.baseType != "King" & !HelperFunctions.checkState(piece, PieceState.Jailed))
            {
                if (piece.position.x == oppKingPos.x & piece.position.y == oppKingPos.y) { }
                else
                {
                    if (piece.lives == -1)
                    {
                        currentPieceValue += 6;
                    }
                    else if (HelperFunctions.checkState(piece, PieceState.Combustable))
                    {
                        currentPieceValue += 15;
                        hasSoloPiece = true;
                    }
                    else if (HelperFunctions.checkAbility(piece, PieceAbilities.Dematerialize) && piece.baseType != "Knight")
                    {
                        currentPieceValue += 8;
                        hasSoloPiece = true;
                    }
                    else if (HelperFunctions.checkState(piece, PieceState.Medusa))
                    {
                        currentPieceValue += 16;
                        hasSoloPiece = true;
                    }
                    else if (HelperFunctions.checkState(piece, PieceState.Delayed))
                    {
                        currentPieceValue -= 8;
                    }
                    else if (piece.collateralType == 0 && HelperFunctions.checkState(oppKing, PieceState.Defuser) == false)
                    {
                        currentPieceValue += 10;
                        hasSoloPiece = true;

                    }
                    else if (piece.collateralType == 1 && HelperFunctions.checkState(oppKing, PieceState.Defuser) == false && piece.baseType != "Knight")
                    {
                        currentPieceValue += 9;
                        hasSoloPiece = true;
                    }
                    else if (piece.collateralType == 2 && HelperFunctions.checkState(oppKing, PieceState.Defuser) == false)
                    {
                        currentPieceValue += 7;
                    }
                    if (piece.baseType == "Misc")
                    {
                        currentPieceValue += 5;
                    }
                    if (piece.baseType == "Queen")
                    {
                        currentPieceValue += 4;
                    }
                    else if (piece.baseType == "Bishop")
                    {
                        currentPieceValue += 3;
                    }
                    else if (piece.baseType == "Rook")
                    {
                        currentPieceValue += 2;
                    }
                    else if (piece.baseType == "Knight")
                    {
                        currentPieceValue += 1;

                    }
                    else if (piece.baseType == "Pawn")
                    {
                        currentPieceValue -= 6;
                    }
                    //If the opposing king can't really move and the turn is past 15
                    //Maybe update the turn thing to points on board
                    if ((HelperFunctions.checkState(oppKing, PieceState.Frozen) || HelperFunctions.checkState(oppKing, PieceState.Depressed) || HelperFunctions.checkState(oppKing, PieceState.Delayed)) && turn > 14)
                    {
                        hasSoloPiece = true;
                    }

                    if (currentPieceValue > bestPiece)
                    {
                        secondBestPiece = bestPiece;
                        bestPiece = currentPieceValue;
                        secondPiece = mainPiece;
                        mainPiece = piece;
                    }
                }
            }
        }

        bool stuckRook = false;
        coords piece2move = new coords(0, 0);
        if (hasSoloPiece == true)
        {
            double closestDistanceToKing = 1000;

            foreach (NextMove nextMove in allMoves)
            {
                Piece piece;
                coords coords;
                string moveType = nextMove.moveType;
                bool repetition = false;

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

                if (piece.name == mainPiece.name)
                {
                    if (moveType != "move")
                    {
                        if (HelperFunctions.checkAbility(piece, PieceAbilities.Dematerialize) && HelperFunctions.checkState(piece, PieceState.Dematerialized) == false)
                        {
                            validMoves.Add(nextMove);
                        }
                        else if (HelperFunctions.checkAbility(piece, PieceAbilities.Dematerialize) && HelperFunctions.checkState(piece, PieceState.Dematerialized) == true && oppKingPos.x == coords.x && oppKingPos.y == coords.y)
                        {
                            validMoves.Add(nextMove);
                        }
                        else if (HelperFunctions.checkState(piece, PieceState.Frozen))
                        {
                            validMoves.Add(nextMove);
                        }
                    }
                    else
                    {
                        if (lastLastMove is not null)
                        {
                            if (nextMove == lastLastMove)
                            {
                                //Stuck
                                repetition = true;
                                if (piece.baseType == "Rook")
                                {
                                    stuckRook = true;
                                    if (this.color == 1)
                                    {
                                        piece2move = new coords(coords.x, coords.y + 1);
                                    }
                                    else
                                    {
                                        piece2move = new coords(coords.x, coords.y - 1);
                                    }
                                }
                            }
                        }

                        if (HelperFunctions.checkState(piece, PieceState.Dematerialized))
                        {
                            if (coords.x == oppKingPos.x && piece.position.x != oppKingPos.x)
                            {
                                validMoves.Add(nextMove);
                            }
                            else if (coords.y == oppKingPos.y && piece.position.y != oppKingPos.y)
                            {
                                validMoves.Add(nextMove);
                            }
                        }
                        else
                        {
                            if (repetition == false)
                            {
                                double pieceDistanceToKing = Math.Sqrt(Math.Pow(coords.y - oppKingPos.y, 2) + Math.Pow(coords.x - oppKingPos.x, 2));
                                double curretPieceDistanceToKing = Math.Sqrt(Math.Pow(piece.position.y - oppKingPos.y, 2) + Math.Pow(piece.position.x - oppKingPos.x, 2));

                                if (pieceDistanceToKing < closestDistanceToKing)
                                {
                                    validMoves.Clear();
                                    validMoves.Add(nextMove);
                                    closestDistanceToKing = pieceDistanceToKing;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (validMoves.Count == 0 && hasSoloPiece == true)
        {
            double furthestDistance = 1000;
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

                if (stuckRook == true)
                {
                    if (coords.x == piece2move.x && coords.y == piece2move.y)
                    {
                        validMoves.Add(nextMove);
                    }
                }
                else
                {
                    if (mainPiece.baseType == "Rook")
                    {
                        if (piece.position.x == mainPiece.position.x)
                        {
                            if (piece.color == -1)
                            {
                                if (coords.y - 8 > furthestDistance)
                                {
                                    validMoves.Add(nextMove);
                                    furthestDistance = coords.y - 8;
                                }
                                else if (coords.y == furthestDistance)
                                {
                                    validMoves.Add(nextMove);
                                }
                            }
                            else if (piece.color == 1)
                            {
                                if (coords.y > furthestDistance)
                                {
                                    validMoves.Add(nextMove);
                                    furthestDistance = coords.y;
                                }
                                else if (coords.y == furthestDistance)
                                {
                                    validMoves.Add(nextMove);
                                }
                            }
                        }
                    }
                    else if (mainPiece.baseType == "Bishop" | mainPiece.baseType == "Queen")
                    {
                        if (piece.color == 1 & piece.position.y - 1 == mainPiece.position.y)
                        {
                            if (piece.position.x + 1 == mainPiece.position.x | piece.position.x - 1 == mainPiece.position.x)
                            {
                                if (piece.color == -1)
                                {
                                    if (coords.y - 8 > furthestDistance)
                                    {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        furthestDistance = coords.y - 8;
                                    }
                                    else if (coords.y == furthestDistance)
                                    {
                                        validMoves.Add(nextMove);
                                    }
                                }
                                else if (piece.color == 1)
                                {
                                    if (coords.y > furthestDistance)
                                    {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        furthestDistance = coords.y;
                                    }
                                    else if (coords.y == furthestDistance)
                                    {
                                        validMoves.Add(nextMove);
                                    }
                                }
                            }
                        }
                        else if (piece.color == -1 & piece.position.y + 1 == mainPiece.position.y)
                        {
                            if (piece.position.x + 1 == mainPiece.position.x | piece.position.x - 1 == mainPiece.position.x)
                            {
                                if (piece.color == -1)
                                {
                                    if (coords.y - 8 > furthestDistance)
                                    {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        furthestDistance = coords.y - 8;
                                    }
                                    else if (coords.y == furthestDistance)
                                    {
                                        validMoves.Add(nextMove);
                                    }
                                }
                                else if (piece.color == 1)
                                {
                                    if (coords.y > furthestDistance)
                                    {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        furthestDistance = coords.y;
                                    }
                                    else if (coords.y == furthestDistance)
                                    {
                                        validMoves.Add(nextMove);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }*/

        //Protect pieces in danger (only if they are good enough)
        if (validMoves.Count == 0)
        {
            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);
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
                                deadPiecepts += oppPiece.points;
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
            //Be wary, this does not account for protected pieces
            //Add one simulation aswell.
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
                    if (oppPos.x == coords.x && oppPos.y == coords.y)
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
                        cull = true;
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

                        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

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

                        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

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

                    List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

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
            gameData.helper.addBotMessage("AggroBot didn't return a move, executing random move instead");
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

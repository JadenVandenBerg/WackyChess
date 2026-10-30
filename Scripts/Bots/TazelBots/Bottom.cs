using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System.Text.RegularExpressions;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;

//To Do:
//Add insentive for the bot to move forward and develop

//we need trading and ghost piece support
//Maybe make protect worth more points
//Safe check will deside a piece is safe if it is protected and it has a lower value then the attacker
//Insentive to protect pieces
//Note the "protect" function moves pieces out of the line of attack
//Add a sim check to the protect function
//Target exploding pieces, have its own exploding piece logic
//Push pawns

public class Bottom : BotTemplate
{
    int turn = 0;
    NextMove lastMove = null;
    NextMove lastLastMove = null;

    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public Bottom(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Bottom";

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

    public bool checkForSafeMove(BoardState bs, NextMove nextMove)
    {
        Piece oppKing = filterPieces("King", this.opponentPieces)[0];
        coords oppKingPos = oppKing.position;
        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, bs, this.color * -1);
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
                        List<NextMove> allMoves3rd = getAllPossibleBotMovesAndAbilities(this, bs, this.color * 1);
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
                List<NextMove> allMovesOpp2 = getAllPossibleBotMovesAndAbilities(this, bs, this.color * -1);

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
                        gameData.helper.addBotMessage("Sim worx");
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
        //Initialize for later
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        float bestMove = -1;
        float bestDist = -1;
        bool check = false;

        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);
        List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, this.color * -1);

        List<NextMove> protMove = new List<NextMove>();
        List<float> protMoveScores = new List<float>();

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

            bool currentSquareSafe = true;
            bool moveToSquareSafe = true;
            float deadPiecepts = 0;

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
            if (moveToSquareSafe == true && currentSquareSafe == false)
            {
                protMove.Add(nextMove);
                float pointVal = piece.points + deadPiecepts;
                protMoveScores.Add(pointVal);
            }
        }

        for (int i = 0; i < protMove.Count; i++)
        {
            if (protMoveScores[i] >= bestMove)
            {
                if (protMoveScores[i] > bestMove)
                {
                    validMoves.Clear();
                }
                bestMove = protMoveScores[i];
                validMoves.Add(protMove[i]);
            }
        }

        foreach (NextMove nextMove in allMoves)
        {
            if (checkForSafeMove(this.currentBoardState, nextMove))
            {
                BoardState cloneState = simulateMove(this.currentBoardState, nextMove);
                List<NextMove> allMoves2 = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color);
                foreach (NextMove nextMove2 in allMoves2)
                {
                    if (checkForSafeMove(cloneState, nextMove2))
                    {
                        BoardState cloneState2 = simulateMove(cloneState, nextMove2);
                        List<float> pointsOnBoard = getPointsOnBoardState(cloneState2, true);
                        float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];
                        float dif = botPoints - oppPoints;
                        float dist;
                        string moveType = nextMove2.moveType;

                        if (moveType == "move")
                        {
                            dist = nextMove2.move.coords.y;
                        }
                        else
                        {
                            dist = nextMove2.ability.coords.y;
                        }

                        if (this.color == -1)
                        {
                            dist -= 8;
                            dist *= -1;
                        }

                        if (isolatedGetKing(cloneState2, this.color * -1) is null)
                        {
                            if (check == false)
                            {
                                check = true;
                                validMoves.Clear();
                                validMoves.Add(nextMove);
                                bestMove = dif;
                            }
                            else
                            {
                                if (dif >= bestMove)
                                {
                                    if (dif == bestMove)
                                    {
                                        if (dist >= bestDist)
                                        {
                                            if (dist > bestDist)
                                            {
                                                validMoves.Clear();
                                            }
                                            validMoves.Add(nextMove);
                                        }
                                    }
                                    else
                                    {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                    }
                                    bestMove = dif;
                                    bestDist = dist;
                                }
                            }
                        }
                        else
                        {
                            Piece king = isolatedGetKing(cloneState2, this.color * -1);
                            coords kingPos = king.position;
                            List<NextMove> allMoves3 = getAllPossibleBotMovesAndAbilities(this, cloneState2, this.color);
                            foreach (NextMove nextMove3 in allMoves3)
                            {
                                Piece piece;
                                coords coords;
                                string moveType2 = nextMove3.moveType;

                                if (moveType2 == "move")
                                {
                                    Move mv = nextMove3.move;

                                    piece = mv.p;
                                    coords = mv.coords;
                                }
                                else
                                {
                                    PieceAbility pa = nextMove3.ability;

                                    piece = pa.piece;
                                    coords = pa.coords;
                                }

                                if (coords.x == kingPos.x && coords.y == kingPos.y)
                                {
                                    check = true;
                                    validMoves.Clear();
                                    validMoves.Add(nextMove);
                                    bestMove = dif;
                                }

                            }
                            if (check == false)
                            {
                                if (dif >= bestMove)
                                {
                                    if (dif == bestMove)
                                    {
                                        if (dist >= bestDist)
                                        {
                                            if (dist > bestDist)
                                            {
                                                validMoves.Clear();
                                            }
                                            validMoves.Add(nextMove);
                                        }
                                    }
                                    else
                                    {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                    }
                                    bestMove = dif;
                                    bestDist = dist;
                                }
                            }
                        }
                    }
                }
            }
        }

        //Idk how this even happend bro just do a random move
        if (validMoves.Count == 0)
        {
            System.Random rand0 = new System.Random();
            int rndIdx0 = rand0.Next(allMoves.Count);
            validMoves.Add(allMoves[rndIdx0]);
            gameData.helper.addBotMessage("Bottom didn't return a move, executing random move instead");
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

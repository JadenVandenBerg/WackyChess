using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class Both : BotTemplate
{
    int turn = 0;

    List<NextMove> lastTurnsOppMoves = new List<NextMove>();

    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public Both(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Both";

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

        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);
        List<NextMove> copyMoves = new List<NextMove>();
        List<Piece> allPiecesOpp = BotHelperFunctions.getPiecesOnBoardState(this.currentBoardState, this.color * -1);
        NextMove oppsLastMove = allMovesOpp[0]; //Assigned with this just to get rid of an error
        int distOpp = 0; //Same thing here

        foreach (NextMove killShot in allMoves)
        {
            if (checkForSafeMove(this.currentBoardState, killShot))
            {
                BoardState cloneState = simulateMove(this.currentBoardState, killShot);
                List<NextMove> allMoves2 = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color);

                foreach (NextMove simMove in allMoves2)
                {
                    if (checkForSafeMove(cloneState, simMove))
                    {
                        BoardState cloneState2 = simulateMove(cloneState, simMove);
                        List<NextMove> allMoves3 = getAllPossibleBotMovesAndAbilities(this, cloneState2, this.color);
                        if (isolatedGetKing(cloneState2, this.color * -1) is null)
                        {
                            validMoves.Add(killShot);
                        }
                    }
                }
            }
        }

        if (turn != 1 && validMoves.Count == 0)
        {
            foreach (NextMove lastMove in lastTurnsOppMoves)
            {
                Piece pieceOpp2;
                coords coordsOpp2;
                string moveTypeOpp2 = lastMove.moveType;
                int dist2;

                if (moveTypeOpp2 == "move")
                {
                    Move mv = lastMove.move;

                    pieceOpp2 = mv.p;
                    coordsOpp2 = mv.coords;
                    dist2 = lastMove.move.coords.y;
                }
                else
                {
                    PieceAbility pa = lastMove.ability;

                    pieceOpp2 = pa.piece;
                    coordsOpp2 = pa.coords;
                    dist2 = lastMove.ability.coords.y;
                }

                if (this.color == 1)
                {
                    dist2 -= 8;
                    dist2 *= -1;
                }

                foreach (Piece currPiece in allPiecesOpp)
                {
                    if (currPiece.position.x == coordsOpp2.x && currPiece.position.y == coordsOpp2.y && currPiece.name == pieceOpp2.name)
                    {
                        oppsLastMove = lastMove;
                        distOpp = dist2;
                    }
                }
            }

            int bestMatch = -1;

            Piece pieceOpp;
            coords coordsOpp;
            string moveTypeOpp = oppsLastMove.moveType;

            if (moveTypeOpp == "move")
            {
                Move mv = oppsLastMove.move;

                pieceOpp = mv.p;
                coordsOpp = mv.coords;
            }
            else
            {
                PieceAbility pa = oppsLastMove.ability;

                pieceOpp = pa.piece;
                coordsOpp = pa.coords;
            }

            foreach (NextMove move2 in allMoves)
            {
                Piece piece;
                coords coords;
                string moveType = move2.moveType;
                int match = -2;
                int dist;

                if (moveType == "move")
                {
                    Move mv = move2.move;

                    piece = mv.p;
                    coords = mv.coords;
                    dist = move2.move.coords.y;
                }
                else
                {
                    PieceAbility pa = move2.ability;

                    piece = pa.piece;
                    coords = pa.coords;
                    dist = move2.ability.coords.y;
                }

                if (this.color == -1)
                {
                    dist -= 8;
                    dist *= -1;
                }

                if (piece.baseType == pieceOpp.baseType && dist == distOpp && coords.x == coordsOpp.x)
                {
                    gameData.helper.addBotMessage("Perfect Match!");
                    match = 5;
                }
                else if (dist == distOpp && coords.x == coordsOpp.x)
                {
                    match = 4;
                }
                else if (piece.position.x == pieceOpp.position.x && piece.position.x == pieceOpp.position.x && piece.baseType == pieceOpp.baseType)
                {
                    match = 3;
                }
                else if (piece.position.x == pieceOpp.position.x && piece.position.x == pieceOpp.position.x)
                {
                    match = 2;
                }
                else if (piece.baseType == pieceOpp.baseType)
                {
                    match = 1;
                }

                if (match >= bestMatch)
                {
                    if (match > bestMatch)
                    {
                        validMoves.Clear();
                        bestMatch = match;
                    }
                    validMoves.Add(move2);
                }
            }

        }

        if (validMoves.Count == 0)
        {
            foreach (NextMove move2 in allMoves)
            {
                Piece piece;
                coords coords;
                string moveType = move2.moveType;
                int dist;

                if (moveType == "move")
                {
                    Move mv = move2.move;

                    piece = mv.p;
                    coords = mv.coords;
                    dist = move2.move.coords.y;
                }
                else
                {
                    PieceAbility pa = move2.ability;

                    piece = pa.piece;
                    coords = pa.coords;
                    dist = move2.ability.coords.y;
                }

                if (this.color == -1)
                {
                    dist -= 8;
                    dist *= -1;
                }

                foreach (NextMove oppMove in allMovesOpp)
                {
                    Piece pieceOpp;
                    coords coordsOpp;
                    string moveTypeOpp = oppMove.moveType;
                    int dist2;

                    if (moveTypeOpp == "move")
                    {
                        Move mv = oppMove.move;

                        pieceOpp = mv.p;
                        coordsOpp = mv.coords;
                        dist2 = oppMove.move.coords.y;
                    }
                    else
                    {
                        PieceAbility pa = oppMove.ability;

                        pieceOpp = pa.piece;
                        coordsOpp = pa.coords;
                        dist2 = oppMove.ability.coords.y;
                    }

                    if (this.color * -1 == -1)
                    {
                        dist2 -= 8;
                        dist2 *= -1;
                    }

                    if (piece.baseType == pieceOpp.baseType && dist == dist2 && coords.x == coordsOpp.x)
                    {
                        copyMoves.Add(move2);
                    }

                }
            }

            foreach (NextMove movie in allMoves)
            {
                Piece piece8;
                coords coords8;
                string moveType8 = movie.moveType;
                bool cull = false;

                if (moveType8 == "move")
                {
                    Move mv = movie.move;

                    piece8 = mv.p;
                    coords8 = mv.coords;
                }
                else
                {
                    PieceAbility pa = movie.ability;

                    piece8 = pa.piece;
                    coords8 = pa.coords;
                }

                foreach (NextMove copyMove in copyMoves)
                {
                    Piece copyP;
                    coords copyC;
                    string copyMT = copyMove.moveType;

                    if (copyMT == "move")
                    {
                        Move mv = copyMove.move;

                        copyP = mv.p;
                        copyC = mv.coords;
                    }
                    else
                    {
                        PieceAbility pa = copyMove.ability;

                        copyP = pa.piece;
                        copyC = pa.coords;
                    }

                    if (copyP.name == piece8.name)
                    {
                        cull = true;
                    }
                }

                if (cull == false)
                {
                    if (checkForSafeMove(this.currentBoardState, movie))
                    {
                        validMoves.Add(movie);
                    }
                }
            }
        }

        if (validMoves.Count == 0)
        {
            System.Random rand0 = new System.Random();
            int rndIdx0 = rand0.Next(allMoves.Count);
            validMoves.Add(allMoves[rndIdx0]);
            gameData.helper.addBotMessage("Both didn't return a move, executing random move instead");
        }

        lastTurnsOppMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);


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
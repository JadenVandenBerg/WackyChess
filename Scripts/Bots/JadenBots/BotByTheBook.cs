using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static JadenBotHelperFunctions;

public class BotByTheBook : BotTemplate
{
    //1 is white, -1 is black
    public BotByTheBook(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Bot by the Book";

        //This function populates the pieces variable
        choosePieces();
    }

    [System.Serializable]
    public class ChessOpening
    {
        public string eco;
        public string name;
        public string moves;
        public bool dontPlayAsWhite;
        public bool dontPlayAsBlack;
    }

    public static class jsonParser
    {
        public static T[] FromJson<T>(string json)
        {
            string wrappedJson = "{\"items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
            return wrapper.items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] items;
        }
    }

    public static ChessOpening getRandomOpening()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JadenBots/opening_book");

        ChessOpening[] openings = jsonParser.FromJson<ChessOpening>(jsonFile.text);

        return openings[Random.Range(0, openings.Length)];
    }

    public static List<ChessOpening> getAllOpenings()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JadenBots/opening_book");

        ChessOpening[] openings = jsonParser.FromJson<ChessOpening>(jsonFile.text);

        return new List<ChessOpening>(openings);
    }

    public static NextMove notationToMove(string notation, List<Piece> pieces, int color, BoardState bs)
    {
        if (notation == null)
        {
            notation = "";
        }

        if (notation == "O-O")
        {
            Piece king = isolatedGetKing(bs, color);
            coords coords = new coords(king.position.x + 2, king.position.y);
            if (color == 1)
            {
                return new NextMove(new PieceAbility(king, PieceAbilities.CastleRight, coords, null, null, findPieceOnBoardStateFromPanelCode(bs, "w_r2")));
            }
            else
            {

                return new NextMove(new PieceAbility(king, PieceAbilities.CastleRight, coords, null, null, findPieceOnBoardStateFromPanelCode(bs, "b_r2")));
            }
        }
        else if (notation == "O-O-O")
        {
            Piece king = isolatedGetKing(bs, color);
            coords coords = new coords(king.position.x - 2, king.position.y);
            if (color == 1)
            {
                return new NextMove(new PieceAbility(king, PieceAbilities.CastleLeft, coords, null, null, findPieceOnBoardStateFromPanelCode(bs, "w_r1")));
            }
            else
            {
                return new NextMove(new PieceAbility(king, PieceAbilities.CastleLeft, coords, null, null, findPieceOnBoardStateFromPanelCode(bs, "b_r1")));
            }
        }

        notation = notation.Replace("+", "");
        notation = notation.Replace("#", "");

        notation = notation.Replace("x", "");

        char file = notation[notation.Length - 2];
        char rank = notation[notation.Length - 1];

        coords destination = new coords(file - 'a' + 1, rank - '1' + 1);

        // Determine piece type
        char pieceChar = notation[0];

        if (pieceChar >= 'a' && pieceChar <= 'h')
        {
            pieceChar = 'P';
        }

        // Get disambiguation, such as Nbd2 or R1e2
        string disambiguation = "";

        if (notation.Length > 2)
        {
            int startIndex = pieceChar == 'P' ? 0 : 1;
            int endIndex = notation.Length - 2;

            if (endIndex > startIndex)
            {
                disambiguation = notation.Substring(startIndex, endIndex - startIndex);
            }
        }

        foreach (Piece piece in pieces)
        {
            if (piece.color != color)
            {
                continue;
            }

            if (piece.baseType.ToString().ToUpper()[0] != pieceChar && pieceChar != 'N')
            {
                continue;
            }

            if (piece.baseType != "Knight" && pieceChar == 'N')
            {
                continue;
            }

            if (disambiguation != "")
            {
                char disambiguationChar = disambiguation[0];

                if (disambiguationChar >= 'a' && disambiguationChar <= 'h')
                {
                    if (piece.position.x != disambiguationChar - 'a' + 1)
                    {
                        continue;
                    }
                }
                else if (disambiguationChar >= '1' && disambiguationChar <= '8')
                {
                    if (piece.position.y != disambiguationChar - '1' + 1)
                    {
                        continue;
                    }
                }
            }

            List<coords> legalMoves = getIsolatedStatePieceMoves(piece, bs, false);

            foreach (coords legalMove in legalMoves)
            {
                if (legalMove.x == destination.x && legalMove.y == destination.y)
                {
                    return new NextMove(new Move(piece, destination));
                }
            }
        }

        //Debug.LogError("Could not find legal piece for notation: " + notation);

        return null;
    }

    public static string moveToNotation(Move move, List<Piece> pieces, BoardState bs)
    {
        Piece piece = move.p;
        coords destination = move.coords;

        string notation = "";

        // Pawns don't have a piece letter
        if (piece.baseType == "Pawn")
        {
            notation += (char)('a' + piece.position.x - 1);
        }
        else
        {
            if (piece.baseType == "Knight")
            {
                notation += "N";
            }
            else if (piece.baseType == "Bishop")
            {
                notation += "B";
            }
            else if (piece.baseType == "Rook")
            {
                notation += "R";
            }
            else if (piece.baseType == "Queen")
            {
                notation += "Q";
            }
            else if (piece.baseType == "King")
            {
                notation += "K";
            }

            // Find all pieces of the same type that can legally move there
            List<Piece> possiblePieces = new List<Piece>();

            foreach (Piece otherPiece in pieces)
            {
                if (otherPiece.color != piece.color)
                {
                    continue;
                }

                if (otherPiece.baseType != piece.baseType)
                {
                    continue;
                }

                List<coords> legalMoves = getIsolatedStatePieceMoves(otherPiece, bs, false);

                foreach (coords legalMove in legalMoves)
                {
                    if (legalMove.x == destination.x && legalMove.y == destination.y)
                    {
                        Debug.LogWarning(otherPiece + " can move to " + legalMove.x + ", " + legalMove.y);
                        possiblePieces.Add(otherPiece);
                        break;
                    }
                }
            }

            // Only add disambiguation if multiple pieces can actually make the move
            if (possiblePieces.Count > 1)
            {
                bool sameFile = false;

                foreach (Piece otherPiece in possiblePieces)
                {
                    if (otherPiece.name == piece.name)
                    {
                        continue;
                    }

                    if (otherPiece.position.x == piece.position.x)
                    {
                        sameFile = true;
                        break;
                    }
                }

                // If another possible piece is on the same file,
                // use the rank. Otherwise use the file.
                if (sameFile)
                {
                    notation += (char)('1' + piece.position.y - 1);
                }
                else
                {
                    notation += (char)('a' + piece.position.x - 1);
                }
            }
        }

        if (piece.baseType != "Pawn" || piece.position.x != destination.x)
        {
            notation += (char)('a' + destination.x - 1);
            notation += (char)('1' + destination.y - 1);
        }
        else
        {
            notation += (char)('1' + destination.y - 1);
        }


        return notation;
    }

    public static string getNthMove(string moves, int n, int color)
    {
        string[] tokens = moves.Split(' ');

        List<string> actualMoves = new List<string>();

        foreach (string token in tokens)
        {
            if (!token.Contains("."))
            {
                actualMoves.Add(token);
            }
        }

        int index = (n - 1) * 2;

        if (color == -1)
        {
            index++;
        }

        if (index < 0 || index >= actualMoves.Count)
        {
            return null;
        }

        return actualMoves[index];
    }

    public static string cleanMoveString(string moves)
    {
        moves = moves.Replace("+", "");
        moves = moves.Replace("#", "");

        moves = moves.Replace("x", "");

        return moves;
    }

    public static List<UndoMove> simulateOpening(string moveString)
    {
        string[] moves = moveString.Split(" ");
        int simulColor = -1;

        List<UndoMove> undoMoves = new List<UndoMove>();

        foreach (string move_ in moves)
        {
            if (move_.Contains(".") || move_ == " " || string.IsNullOrEmpty(move_))
            {
                continue;
            }

            simulColor *= -1;

            debug_printBoardState(startingBoardState);
            Debug.Log(move_);

            NextMove nm = notationToMove(move_, getAllPiecesOnBoardState(startingBoardState), simulColor, startingBoardState);

            if (nm == null)
            {
                foreach(UndoMove undo in undoMoves)
                {
                    undoMove(undo, startingBoardState);
                }

                return undoMoves;
            }

            if (nm.moveType == "move")
            {
                Move m = nm.move;

                Debug.Log("Simulating Move: " + m.p + " to " + m.coords.x + "," + m.coords.y);
                UndoMove undo = undo_simpleSimulatePieceMove(startingBoardState, m.p, m.coords);
                undoMoves.Insert(0, undo);
            }
            else
            {
                PieceAbility m = nm.ability;

                Debug.Log("Simulating Ability: " + m.piece + " to " + m.coords.x + "," + m.coords.y);
                UndoMove undo = undo_simulatePieceAbility(startingBoardState, m);
                undoMoves.Insert(0, undo);
            }
        }

        Debug.Log("Number of Simulated Moves: " + undoMoves.Count);
        return undoMoves;
    }

    int move = 0;
    bool bookMoves = true;
    bool hasPrinted = false;
    string moveString = "";
    ChessOpening preferredOpening = null;
    static BoardState startingBoardState = null;

    public static (NextMove nextMove, bool bookMoves, bool hasPrinted, string moveString, ChessOpening preferredOpening) getNextOpeningMove(int color, int move, string moveString, BoardState currentBoardState, bool bookMoves, bool hasPrinted, ChessOpening preferredOpening)
    {
        startingBoardState = gameData.startingBoardState;
        if (move != 1 || color != 1)
        {
            if (gameData.lastBotMove != null && gameData.lastBotMove.moveType == "move")
            {
                if (color == -1)
                {
                    if (move == 1)
                    {
                        moveString += move + ". " + moveToNotation(gameData.lastBotMove.move, getAllPiecesOnBoardState(currentBoardState), currentBoardState);
                    }
                    else
                    {
                        moveString += " " + move + ". " + moveToNotation(gameData.lastBotMove.move, getAllPiecesOnBoardState(currentBoardState), currentBoardState);
                    }
                }
                else
                {
                    if (move == 1)
                    {
                        moveString += moveToNotation(gameData.lastBotMove.move, getAllPiecesOnBoardState(currentBoardState), currentBoardState);
                    }
                    else
                    {
                        moveString += " " + moveToNotation(gameData.lastBotMove.move, getAllPiecesOnBoardState(currentBoardState), currentBoardState);
                    }
                }
            }
            else
            {
                bookMoves = false;
            }
        }

        /*if (move == 1 && color == 1)
        {
            ChessOpening opening = getRandomOpening();

            if (opening.dontPlayAsWhite && opening.dontPlayAsWhite == true)
            {
                opening = getRandomOpening();
            }

            preferredOpening = opening;

            //Debug
            /*List<ChessOpening> chessOpenings = getAllOpenings();
            foreach (ChessOpening opening1 in chessOpenings)
            {
                if (opening1.name == "Englund Gambit")
                {
                    opening = opening1;
                    break;
                }
            }
            //Debug End

            Debug.Log(opening.name);
            Debug.Log(opening.moves);

            gameData.helper.addBotMessage(" Bot by the Book played a book move. (" + opening.name + ").");
            moveString += "1. " + getNthMove(opening.moves, move, color);

            return (notationToMove(getNthMove(opening.moves, move, color), getAllPiecesOnBoardState(currentBoardState), color, currentBoardState), bookMoves, hasPrinted, moveString, preferredOpening);
        }
        else */if (bookMoves)
        {
            List<ChessOpening> chessOpenings = getAllOpenings();
            List<ChessOpening> eligibleOpenings = new List<ChessOpening>();

            ChessOpening _preferredOpening = null;

            startingBoardState = copyBoardState(startingBoardState);

            foreach (ChessOpening opening in chessOpenings)
            {
                /*if (opening.name == "Englund Gambit")
                {
                    Debug.Log(cleanMoveString(opening.moves));
                    Debug.Log(cleanMoveString(moveString));
                }*/

                // Simulate each part of the movestring for each opening and compare with the boardstate
                string simulMoveChain = "";
                for (int i = 0; i < move; i++)
                {
                    // No previous moves
                    if (i == 0 && color == 1)
                    {
                        simulMoveChain = "";
                    }
                    // White only
                    else if (i == 0 && color == -1) {
                        simulMoveChain += getNthMove(opening.moves, i + 1, 1);
                    }
                    else
                    {
                        if (color == 1)
                        {
                            simulMoveChain += " " + getNthMove(opening.moves, i, 1);
                            simulMoveChain += " " + getNthMove(opening.moves, i, -1);
                        }
                        else
                        {
                            simulMoveChain += " " + getNthMove(opening.moves, i, -1);
                            simulMoveChain += " " + getNthMove(opening.moves, i + 1, 1);
                        }
                    }
                }

                Debug.Log("Opening Name: " + opening.name);
                Debug.Log("Opening Moves: " + opening.moves);
                Debug.Log("Color: " + color);
                Debug.Log("Move: " + move);
                Debug.Log("Simul Move Chain:" + simulMoveChain);
                Debug.Log("Move Chain: " + moveString);

                List<UndoMove> undoMoves = simulateOpening(simulMoveChain);

                if (undoMoves == null)
                {
                    continue;
                }

                //if (cleanMoveString(opening.moves).Contains(cleanMoveString(moveString)))
                if (areBoardStatesEqual(currentBoardState, startingBoardState))
                {
                    if (color == 1 && opening.dontPlayAsWhite && opening.dontPlayAsWhite == true)
                    {
                        foreach (UndoMove undo in undoMoves)
                        {
                            undoMove(undo, startingBoardState);
                        }
                        continue;
                    }

                    else if (color == -1 && opening.dontPlayAsBlack && opening.dontPlayAsBlack == true)
                    {
                        foreach (UndoMove undo in undoMoves)
                        {
                            undoMove(undo, startingBoardState);
                        }
                        continue;
                    }

                    if (getNthMove(opening.moves, move, color) == null)
                    {
                        foreach (UndoMove undo in undoMoves)
                        {
                            undoMove(undo, startingBoardState);
                        }
                        continue;
                    }

                    //Debug
                    /*if (opening.name == "Englund Gambit")
                    {
                        eligibleOpenings.Add(opening);
                        break;
                    }*/
                    //Debug end

                    Debug.Log("Opening is Eligible");
                    eligibleOpenings.Add(opening);

                    if (preferredOpening != null && preferredOpening.name == opening.name)
                    {
                        _preferredOpening = opening;
                    } 
                }
                else
                {

                    Debug.Log("Opening is not Eligible");
                }

                foreach (UndoMove undo in undoMoves)
                {
                    undoMove(undo, startingBoardState);
                }
            }

            if (eligibleOpenings.Count == 0)
            {
                bookMoves = false;
                if (!hasPrinted)
                {
                    hasPrinted = true;
                    gameData.helper.addBotMessage(" No more book moves found");
                }

            }
            else
            {
                ChessOpening opening = eligibleOpenings[globalDefs.globalRand.Next(eligibleOpenings.Count)];

                if (_preferredOpening != null)
                {
                    opening = _preferredOpening;
                }

                preferredOpening = opening;

                Debug.Log(opening.name);
                Debug.Log(opening.moves);

                gameData.helper.addBotMessage(" Bot by the Book played a book move. (" + opening.name + ").");

                if (color == 1)
                {
                    moveString += " " + move + ". " + getNthMove(opening.moves, move, color);
                }
                else
                {
                    moveString += " " + getNthMove(opening.moves, move, color);
                }

                string moveNotation = getNthMove(opening.moves, move, color);

                if (!(moveNotation == null || moveNotation == ""))
                {
                    return (notationToMove(moveNotation, getAllPiecesOnBoardState(currentBoardState), color, currentBoardState), bookMoves, hasPrinted, moveString, preferredOpening);
                }
            }
        }

        return (null, bookMoves, hasPrinted, moveString, preferredOpening);
    }

    override
    public NextMove nextMove()
    {
        move++;

        NextMove nextMove = null;

        if (bookMoves)
        {
            var openingMoves = getNextOpeningMove(color, move, moveString, currentBoardState, bookMoves, hasPrinted, preferredOpening);

            nextMove = openingMoves.nextMove;
            bookMoves = openingMoves.bookMoves;
            hasPrinted = openingMoves.hasPrinted;
            moveString = openingMoves.moveString;
        }

        if (nextMove != null)
        {
            return nextMove;
        }
        else
        {

            resetPiecePositions(this.currentBoardState, convertBoardGrid(gameData.boardGrid));

            float bestL2Eval = Mathf.NegativeInfinity;
            List<NextMove> validMoves_L2 = new List<NextMove>();

            List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
            foreach (NextMove nextMove_1 in allMoves)
            {
                var nextMoveVars = getNextMoveVars(nextMove_1);
                Piece piece = nextMoveVars.piece;
                coords coords = nextMoveVars.coords;
                string moveType = nextMoveVars.moveType;

                UndoMove undo;

                if (moveType == "move")
                {
                    undo = undo_simulatePieceMove(this.currentBoardState, piece, new coords(coords.x, coords.y));
                }
                else
                {
                    undo = undo_simulatePieceAbility(this.currentBoardState, nextMove_1.ability);
                }

                BoardState bestMoveOppBS = null;
                NextMove bestOppMove = null;
                float bestOppMoveDiff = Mathf.Infinity;

                List<NextMove> allMoves_2 = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);
                foreach (NextMove nextMove_2 in allMoves_2)
                {
                    var nextMoveVars_2 = getNextMoveVars(nextMove_2);
                    Piece piece_2 = nextMoveVars_2.piece;
                    coords coords_2 = nextMoveVars_2.coords;
                    string moveType_2 = nextMoveVars_2.moveType;

                    UndoMove undo_2;

                    if (moveType_2 == "move")
                    {
                        undo_2 = undo_simulatePieceMove(this.currentBoardState, piece_2, new coords(coords_2.x, coords_2.y));
                    }
                    else
                    {
                        undo_2 = undo_simulatePieceAbility(this.currentBoardState, nextMove_2.ability);
                    }

                    float bestResponseDiff = Mathf.NegativeInfinity;
                    NextMove bestResponse = null;

                    List<NextMove> allMoves_3 = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
                    foreach (NextMove nextMove_3 in allMoves_3)
                    {
                        var nextMoveVars_3 = getNextMoveVars(nextMove_3);
                        Piece piece_3 = nextMoveVars_3.piece;
                        coords coords_3 = nextMoveVars_3.coords;
                        string moveType_3 = nextMoveVars_3.moveType;

                        UndoMove undo_3;

                        if (moveType_3 == "move")
                        {
                            undo_3 = undo_simulatePieceMove(this.currentBoardState, piece_3, new coords(coords_3.x, coords_3.y));
                        }
                        else
                        {
                            undo_3 = undo_simulatePieceAbility(this.currentBoardState, nextMove_3.ability);
                        }

                        List<float> pob = Jay_getPointsOnBoardState_simple(currentBoardState, true, color);

                        float botPoints = this.color == 1 ? pob[0] : pob[1];
                        float oppPoints = this.color == -1 ? pob[0] : pob[1];

                        float score = botPoints - oppPoints;
                        if (score > bestResponseDiff || bestResponse == null)
                        {
                            bestResponseDiff = score;
                            bestResponse = nextMove_3;
                        }

                        undoMove(undo_3, this.currentBoardState);
                    }

                    if (bestResponseDiff < bestOppMoveDiff || bestOppMove == null)
                    {
                        bestOppMoveDiff = bestResponseDiff;

                        bestOppMove = nextMove_2;
                        bestMoveOppBS = copyBoardState(this.currentBoardState);
                    }

                    undoMove(undo_2, this.currentBoardState);
                }

                if (bestMoveOppBS == null)
                {
                    continue;
                }

                List<NextMove> allMoves_L2 = getAllPossibleBotMovesAndAbilities(this, bestMoveOppBS, this.color);
                foreach (NextMove nextMove_L2 in allMoves_L2)
                {
                    var nextMoveVars_L2 = getNextMoveVars(nextMove_L2);
                    Piece piece_L2 = nextMoveVars_L2.piece;
                    coords coords_L2 = nextMoveVars_L2.coords;
                    string moveType_L2 = nextMoveVars_L2.moveType;

                    UndoMove undo_L2;

                    if (moveType_L2 == "move")
                    {
                        undo_L2 = undo_simulatePieceMove(bestMoveOppBS, piece_L2, new coords(coords_L2.x, coords_L2.y));
                    }
                    else
                    {
                        undo_L2 = undo_simulatePieceAbility(bestMoveOppBS, nextMove_L2.ability);
                    }

                    List<NextMove> allMovesOpp_L2 = getAllPossibleBotAttacksAndAbilities(this, bestMoveOppBS, this.color * -1);

                    if (allMovesOpp_L2.Count == 0)
                    {
                        List<float> pob = Jay_getPointsOnBoardState_simple(bestMoveOppBS, true, color);

                        float botPoints = this.color == 1 ? pob[0] : pob[1];
                        float oppPoints = this.color == -1 ? pob[0] : pob[1];

                        float score = botPoints - oppPoints;

                        if (score >= bestL2Eval)
                        {
                            if (score > bestL2Eval)
                            {
                                validMoves_L2.Clear();
                            }

                            bestL2Eval = score;
                            validMoves_L2.Add(nextMove_1);
                        }
                    }
                    else
                    {
                        float bestOppMoveDiff_L2 = Mathf.Infinity;

                        foreach (NextMove nextMove_L2_opp in allMovesOpp_L2)
                        {
                            var nextMoveVars_L2_opp = getNextMoveVars(nextMove_L2_opp);
                            Piece piece_L2_opp = nextMoveVars_L2_opp.piece;
                            coords coords_L2_opp = nextMoveVars_L2_opp.coords;
                            string moveType_L2_opp = nextMoveVars_L2_opp.moveType;

                            UndoMove undo_L2_opp;

                            if (moveType_L2_opp == "move")
                            {
                                undo_L2_opp = undo_simulatePieceMove(bestMoveOppBS, piece_L2_opp, new coords(coords_L2_opp.x, coords_L2_opp.y));
                            }
                            else
                            {
                                undo_L2_opp = undo_simulatePieceAbility(bestMoveOppBS, nextMove_L2_opp.ability);
                            }

                            List<float> pob_l2 = Jay_getPointsOnBoardState_simple(bestMoveOppBS, true, color);

                            float botPoints_l2 = this.color == 1 ? pob_l2[0] : pob_l2[1];
                            float oppPoints_l2 = this.color == -1 ? pob_l2[0] : pob_l2[1];

                            float score_l2 = botPoints_l2 - oppPoints_l2;

                            if (score_l2 < bestOppMoveDiff_L2)
                            {
                                bestOppMoveDiff_L2 = score_l2;
                            }

                            undoMove(undo_L2_opp, bestMoveOppBS);
                        }

                        float candidateScore = bestOppMoveDiff_L2;

                        if (candidateScore > bestL2Eval)
                        {
                            bestL2Eval = candidateScore;
                            validMoves_L2.Clear();
                            validMoves_L2.Add(nextMove_1);
                        }
                        else if (candidateScore == bestL2Eval)
                        {
                            validMoves_L2.Add(nextMove_1);
                        }
                    }

                    undoMove(undo_L2, bestMoveOppBS);
                }

                undoMove(undo, this.currentBoardState);
            }

            System.Random rand = new System.Random();
            int rndIdx = rand.Next(validMoves_L2.Count);

            NextMove move = validMoves_L2[rndIdx];

            return move;
        }
    }
}
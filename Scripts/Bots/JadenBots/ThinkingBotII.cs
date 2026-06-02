using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;
using static HelperFunctions;
using static UndoMoveBotHelperFunctions;
using System.Threading.Tasks;
using System.Collections.Concurrent;

public class ThinkingBotII : BotTemplate
{
    public ThinkingBotII(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Thinking Bot II";

        choosePieces();
    }

    Queue<NextMove> lastFiveMoves = new Queue<NextMove>();

    override
    public NextMove nextMove()
    {
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        int movesAnalyzed = 0;

        ConcurrentBag<(NextMove move, float diff)> results = new ConcurrentBag<(NextMove, float)>();

        Parallel.ForEach(allMoves, nextMove =>
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            BoardState thread_BoardState = copyBoardState(this.currentBoardState);
            nextMove = thread_nextMove(nextMove, thread_BoardState);

            var nextMoveVars = getNextMoveVars(nextMove);

            Piece piece = nextMoveVars.piece;
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            UndoMove undo;

            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(thread_BoardState, piece, new coords(coords.x, coords.y));
            }
            else
            {
                undo = undo_simulatePieceAbility(thread_BoardState, nextMove.ability);
            }

            if (isBoardStateOppStalemate(thread_BoardState))
            {
                return;
            }

            float firstModePointsAdd = 0f;

            if (moveInQueue(lastFiveMoves, nextMove))
            {
                firstModePointsAdd -= 2f;
            }

            if (moveInQueueTwice(lastFiveMoves, nextMove))
            {
                firstModePointsAdd -= 6f;
            }

            if (isCheckBoardState(thread_BoardState, this.color * -1))
            {
                firstModePointsAdd += 0.1f;
            }

            var boardControl = ThinkingBot_getBoardControlOnBoardState(thread_BoardState);
            int boardControlDiff = this.color == 1 ? boardControl.white - boardControl.black : boardControl.black - boardControl.white;

            float worstCaseScenario = 999999f;

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, thread_BoardState, this.color * -1);

            List<(NextMove move, float score)> orderedOpponentMoves = new List<(NextMove, float)>();

            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                var nextMoveOppVars = getNextMoveVars(nextMoveOpp);

                Piece pieceOpp = nextMoveOppVars.piece;
                coords coordsOpp = nextMoveOppVars.coords;
                string moveTypeOpp = nextMoveOppVars.moveType;

                UndoMove undoOppOrdering;

                if (moveTypeOpp == "move")
                {
                    undoOppOrdering = undo_simulatePieceMove(thread_BoardState, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
                }
                else
                {
                    undoOppOrdering = undo_simulatePieceAbility(thread_BoardState, nextMoveOpp.ability);
                }

                float orderingScore = 0f;

                var points = ThinkingBot_getPointsOnBoardState(thread_BoardState, true, pieceOpp, coordsOpp);

                float botPoints = this.color == 1 ? points.white : points.black;
                float oppPoints = this.color == -1 ? points.white : points.black;

                orderingScore = oppPoints - botPoints;

                orderedOpponentMoves.Add((nextMoveOpp, orderingScore));

                undoMove(undoOppOrdering, thread_BoardState);
            }

            orderedOpponentMoves = orderedOpponentMoves.OrderByDescending(x => x.score).Take(5).ToList();
            debug_printMoveOrder(orderedOpponentMoves, nextMove);

            foreach (var orderedMove in orderedOpponentMoves)
            {
                NextMove nextMoveOpp = orderedMove.move;
                var nextMoveOppVars = getNextMoveVars(nextMoveOpp);

                Piece pieceOpp = nextMoveOppVars.piece;
                coords coordsOpp = nextMoveOppVars.coords;
                string moveTypeOpp = nextMoveOppVars.moveType;

                if (nextMoveOpp.moveType == "ability" && nextMoveOpp.ability.ability == PieceAbilities.Spawn)
                {
                    continue;
                }

                UndoMove undoOpp;

                if (moveTypeOpp == "move")
                {
                    undoOpp = undo_simulatePieceMove(thread_BoardState, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
                }
                else
                {
                    undoOpp = undo_simulatePieceAbility(thread_BoardState, nextMoveOpp.ability);
                }

                float bestResponseDiff = -999999f;

                List<NextMove> allBotMovesResponse = getAllPossibleBotMovesAndAbilities(this, thread_BoardState, this.color);

                foreach (NextMove nextMoveResponse in allBotMovesResponse)
                {
                    var nextMoveResponseVars = getNextMoveVars(nextMoveResponse);

                    Piece pieceResponse = nextMoveResponseVars.piece;
                    coords coordsResponse = nextMoveResponseVars.coords;
                    string moveTypeResponse = nextMoveResponseVars.moveType;

                    UndoMove undoResponse;

                    if (moveTypeResponse == "move")
                    {
                        undoResponse = undo_simulatePieceMove(thread_BoardState, pieceResponse, new coords(coordsResponse.x, coordsResponse.y));
                    }
                    else
                    {
                        undoResponse = undo_simulatePieceAbility(thread_BoardState, nextMoveResponse.ability);
                    }

                    float opponentBestCounter = 999999f;

                    List<NextMove> allOpponentCounters = getAllPossibleBotKillsAndAbilities(this, thread_BoardState, this.color * -1);

                    foreach (NextMove opponentCounter in allOpponentCounters)
                    {
                        movesAnalyzed++;

                        var opponentCounterVars = getNextMoveVars(opponentCounter);

                        Piece opponentCounterPiece = opponentCounterVars.piece;
                        coords opponentCounterCoords = opponentCounterVars.coords;
                        string opponentCounterMoveType = opponentCounterVars.moveType;

                        UndoMove undoOpponentCounter;

                        if (opponentCounterMoveType == "move")
                        {
                            undoOpponentCounter = undo_simulatePieceMove(thread_BoardState, opponentCounterPiece, new coords(opponentCounterCoords.x, opponentCounterCoords.y));
                        }
                        else
                        {
                            undoOpponentCounter = undo_simulatePieceAbility(thread_BoardState, opponentCounter.ability);
                        }

                        var points = ThinkingBot_getPointsOnBoardState(thread_BoardState, true, pieceResponse, coordsResponse);

                        float botPoints = this.color == 1 ? points.white : points.black;
                        float oppPoints = this.color == -1 ? points.white : points.black;

                        float diff = botPoints - oppPoints + firstModePointsAdd;

                        diff += boardControlDiff * 0.01f;

                        if (diff < opponentBestCounter)
                        {
                            opponentBestCounter = diff;
                        }

                        undoMove(undoOpponentCounter, thread_BoardState);
                    }

                    if (opponentBestCounter > bestResponseDiff)
                    {
                        bestResponseDiff = opponentBestCounter;
                    }

                    undoMove(undoResponse, thread_BoardState);
                }

                if (bestResponseDiff < worstCaseScenario)
                {
                    worstCaseScenario = bestResponseDiff;
                }

                undoMove(undoOpp, thread_BoardState);
            }

            results.Add((nextMove, worstCaseScenario));

            undoMove(undo, thread_BoardState);

            watch.Stop();
            var watchMS = watch.ElapsedMilliseconds;

            Debug.Log("Thread for move: " + piece + " to " + coords.x + "," + coords.y + " took " + watchMS + "ms.");
        });

        if (results.Count == 0)
        {
            return allMoves[0];
        }

        List<NextMove> validMoves = new List<NextMove>();

        float bestMoveDiff = -1000;
        foreach (var result in results)
        {
            if (result.diff >= bestMoveDiff)
            {
                if (result.diff > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = result.diff;
                validMoves.Add(result.move);
            }
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

        lastFiveMoves.Enqueue(move);

        if (lastFiveMoves.Count > 6)
        {
            lastFiveMoves.Dequeue();
        }

        Debug.Log("Analyzed: " + movesAnalyzed);

        return move;
    }

    private (float white, float black) ThinkingBot_getPointsOnBoardState(BoardState bs, bool isKingWorthMore, Piece movePiece, coords moveCoords)
    {
        if (bs == null)
        {
            if (this.color == 1)
            {
                return (0, 100f);
            }
            else
            {
                return (100f, 0);
            }
        }

        List<Piece>[,] board = bs.boardGrid;
        float wCount = 0;
        float bCount = 0;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece piece in board[x, y])
                {

                    float pts = piece.points;

                    if (pts <= 0)
                    {
                        pts = 0.1f;
                    }

                    if (isKingWorthMore && piece.baseType == "King")
                    {
                        pts += 100;
                    }

                    if (checkState(piece, PieceState.Fragile))
                    {
                        pts += 1.5f;
                    }

                    if (checkState(piece, PieceState.Shield))
                    {
                        pts -= piece.points;
                    }

                    if (checkState(piece, PieceState.Frozen))
                    {
                        pts -= piece.points / 2;
                    }

                    if (x == moveCoords.x - 1 && y == moveCoords.y - 1)
                    {
                        if (checkState(piece, PieceState.Electric))
                        {
                            pts -= (Mathf.Floor(movePiece.points / 2) + 1);
                        }
                    }

                    if (piece.color == 1)
                    {
                        wCount += pts;
                    }
                    else
                    {
                        bCount += pts;
                    }
                }
            }
        }

        return (wCount, bCount);
    }

    private (int white, int black) ThinkingBot_getBoardControlOnBoardState(BoardState bs)
    {
        Piece whiteKing = isolatedGetKing(bs, 1);
        Piece blackKing = isolatedGetKing(bs, -1);

        if (whiteKing == null)
        {
            whiteKing = this.king;
        }

        if (blackKing == null)
        {
            blackKing = filterPieces("King", this.opponentPieces)[0];
        }

        coords whiteKingPos = whiteKing.position;
        coords blackKingPos = blackKing.position;

        var botMovesWhite = getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = getAllPossibleBotMoves(this, bs, -1);
        List<PieceMoveList> listBotBlackMoves = botMovesBlack.pieceMoveList;

        List<coords> uniqueCoords = new List<coords>();
        int whiteScore = 0;
        foreach (PieceMoveList pml in listBotWhiteMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                if (!coordsInList(uniqueCoords, coords))
                {
                    uniqueCoords.Add(coords);
                    whiteScore += 8 - Mathf.Abs(coords.x - blackKingPos.x);
                    whiteScore += 8 - Mathf.Abs(coords.y - blackKingPos.y);

                    if (coords.x == blackKingPos.x && coords.y == blackKingPos.y)
                    {
                        whiteScore += 16;
                    }
                }
            }
        }

        int blackScore = 0;
        uniqueCoords = new List<coords>();
        foreach (PieceMoveList pml in listBotBlackMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                if (!coordsInList(uniqueCoords, coords))
                {
                    uniqueCoords.Add(coords);
                    blackScore += 8 - Mathf.Abs(coords.x - whiteKingPos.x);
                    blackScore += 8 - Mathf.Abs(coords.y - whiteKingPos.y);

                    if (coords.x == whiteKingPos.x && coords.y == whiteKingPos.y)
                    {
                        blackScore += 16;
                    }
                }
            }
        }

        return ( whiteScore, blackScore );
    }

    private static bool moveInQueue(Queue<NextMove> moves, NextMove move)
    {
        foreach (NextMove m in moves)
        {
            if (m.moveType != move.moveType)
                continue;

            if (move.moveType == "move")
            {
                if (m.move.p.name == move.move.p.name &&
                    m.move.coords.x == move.move.coords.x &&
                    m.move.coords.y == move.move.coords.y)
                {
                    return true;
                }
            }
            else if (move.moveType == "ability")
            {
                if (m.ability.piece.name == move.ability.piece.name &&
                    m.ability.coords.x == move.ability.coords.x &&
                    m.ability.coords.y == move.ability.coords.y)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool moveInQueueTwice(Queue<NextMove> moves, NextMove move)
    {
        bool once = false;
        foreach (NextMove m in moves)
        {
            if (m.moveType != move.moveType)
                continue;

            if (move.moveType == "move")
            {
                if (m.move.p.name == move.move.p.name &&
                    m.move.coords.x == move.move.coords.x &&
                    m.move.coords.y == move.move.coords.y)
                {
                    if (once == true)
                    {
                        return true;
                    }

                    once = true;
                }
            }
            else if (move.moveType == "ability")
            {
                if (m.ability.piece.name == move.ability.piece.name &&
                    m.ability.coords.x == move.ability.coords.x &&
                    m.ability.coords.y == move.ability.coords.y)
                {
                    if (once == true)
                    {
                        return true;
                    }

                    once = true;
                }
            }
        }

        return false;
    }

    private bool isBoardStateOppStalemate(BoardState bs)
    {
        bool _check = isCheckBoardState(bs, this.color * -1);
        if (_check)
        {
            return false;
        }

        List<Piece> oppPieces = getPiecesOnBoardState(bs, this.color * -1);

        foreach (Piece oppPiece in oppPieces)
        {
            List<NextMove> possibleNextMoves = getAllPossibleBotPieceMoves(bs, oppPiece);

            foreach (NextMove nm in possibleNextMoves)
            {
                Piece p = nm.move.p;
                coords coords = nm.move.coords;

                BoardState afterMoveBS = simulatePieceMove(this, bs, p, coords);

                bool check = isCheckBoardState(afterMoveBS, this.color * -1);

                if (!check)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool isCheckBoardState(BoardState bs, int color)
    {
        List<NextMove> allMoves = getAllPossibleBotAttacksAndAbilities(this, bs, color * -1);

        Piece king = isolatedGetKing(bs, color);

        foreach (NextMove nm in allMoves)
        {
            if (nm.moveType == "ability")
            {
                continue;
            }

            if (king == null)
            {
                return true;
            }

            if (king.position.x == nm.move.coords.x && king.position.y == nm.move.coords.y)
            {
                return true;
            }
        }

        return false;
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static HelperFunctions;


public class Botkrieg : BotTemplate
{
    public Botkrieg(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Botkrieg";

        choosePieces();
    }

    /* Todo
    *   Make atomic pieces attempt to blow up the king
    *   Make landmine pieces go next to the king, checks if possible
    *   Dematerialize phantom pieces and rematerialize them on the king
    *   Repeatedly attack opponents pieces with infinite pieces / crook
    *   Jail the opponents king
    */

    Queue<NextMove> lastFiveMoves = new Queue<NextMove>();

    class Botkrieg_info
    {
        public List<Piece> atomic;
        public List<Piece> phantom;
        public List<Piece> landmine;
        public List<Piece> infinite;
        public List<Piece> jailer;
        public List<Piece> freezeBomb;

        public List<Piece> all;

        public Botkrieg_info()
        {
            atomic = new List<Piece>();
            phantom = new List<Piece>();
            landmine = new List<Piece>();
            infinite = new List<Piece>();
            jailer = new List<Piece>();
            freezeBomb = new List<Piece>();

            all = new List<Piece>();
        }
    }

    private int checkNumDead(Botkrieg_info info)
    {
        int numDead = 0;

        foreach (Piece p in info.atomic)
        {
            if (p.alive == 0 || p.position.x == -1)
            {
                numDead++;
            }
        }

        foreach (Piece p in info.phantom)
        {
            if (p.alive == 0 || p.position.x == -1)
            {
                numDead++;
            }
        }

        foreach (Piece p in info.infinite)
        {
            if (p.alive == 0 || p.position.x == -1)
            {
                numDead++;
            }
        }

        foreach (Piece p in info.landmine)
        {
            if (p.alive == 0 || p.position.x == -1)
            {
                numDead++;
            }
        }

        foreach (Piece p in info.jailer)
        {
            if (p.alive == 0 || p.position.x == -1)
            {
                numDead++;
            }
        }

        foreach (Piece p in info.freezeBomb)
        {
            if (p.alive == 0 || p.position.x == -1)
            {
                numDead++;
            }
        }

        return numDead;
    }

    private bool isLandmineNextToKing(Piece p, BoardState bs)
    {
        if (p.collateralType != 1)
        {
            return false;
        }

        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1)
        {
            return false;
        }

        int x = Mathf.Abs(p.position.x - oppKing.position.x);
        int y = Mathf.Abs(p.position.y - oppKing.position.y);

        if (x == 1 && y == 1 || x == 0 && y == 1 || x == 1 && y == 0)
        {
            return true;
        }

        return false;
    }

    private bool isLandmineAttackingNextToKing(Piece p, BoardState bs)
    {
        if (p.collateralType != 1)
        {
            return false;
        }

        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1)
        {
            return false;
        }

        List<coords> landmineAttacking = getIsolatedStatePieceMoves(p, bs, false);

        foreach (coords c in landmineAttacking)
        {
            int x = Mathf.Abs(c.x - oppKing.position.x);
            int y = Mathf.Abs(c.y - oppKing.position.y);

            if (x == 1 && y == 1 || x == 0 && y == 1 || x == 1 && y == 0)
            {
                return true;
            }
        }

        return false;
    }

    private bool isOppKingDead(BoardState bs)
    {
        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1 || oppKing.alive == 0)
        {
            return true;
        }

        return false;
    }

    private bool isOppKingJailed(BoardState bs)
    {
        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1 || oppKing.alive == 0)
        {
            return false;
        }

        if (checkState(oppKing, PieceState.Jailed))
        {
            return true;
        }

        return false;
    }

    private bool isAtomicAttackingKingCollateral(Piece p, BoardState bs)
    {
        if (p.collateralType != 0)
        {
            return false;
        }

        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1)
        {
            return false;
        }

        List<coords> atomicAttacking = getIsolatedStatePieceMoves(p, bs, false);

        bool kingDies = false;

        foreach (coords c in atomicAttacking)
        {
            int x = Mathf.Abs(c.x - oppKing.position.x);
            int y = Mathf.Abs(c.y - oppKing.position.y);

            if (x == 1 && y == 1 || x == 0 && y == 0)
            {
                kingDies = true;
            }

            if (!checkBounds(c.x, c.y))
            {
                continue;
            }

            foreach (Piece p_ in bs.boardGrid[c.x - 1, c.y - 1])
            {
                if (checkState(p_, PieceState.Defuser) && !(x == 0 && y == 0))
                {
                    return false;
                }
            }
        }

        return kingDies;
    }

    private bool isFreezeBombAttackingKingCollateral(Piece p, BoardState bs)
    {
        if (p.collateralType != 2)
        {
            return false;
        }

        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1)
        {
            return false;
        }

        List<coords> freezeBombAttacking = getIsolatedStatePieceMoves(p, bs, false);
        bool kingDies = false;

        foreach (coords c in freezeBombAttacking)
        {
            int x = Mathf.Abs(c.x - oppKing.position.x);
            int y = Mathf.Abs(c.y - oppKing.position.y);

            if (x == 1 && y == 1 || x == 0 && y == 0)
            {
                kingDies = true;
            }

            if (!checkBounds(c.x, c.y))
            {
                continue;
            }

            foreach (Piece p_ in bs.boardGrid[c.x - 1, c.y - 1])
            {
                if (checkState(p_, PieceState.Defuser) && !(x == 0 && y == 0))
                {
                    return false;
                }
            }
        }

        return kingDies;
    }

    private bool isPhantomDematerialized(Piece p)
    {
        return checkState(p, PieceState.Dematerialized);
    }

    private bool isDematerializedPhantomOnKing(Piece p, BoardState bs)
    {
        if (!isPhantomDematerialized(p))
        {
            return false;
        }

        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1 || oppKing.alive == 0)
        {
            return false;
        }

        int x = Mathf.Abs(p.position.x - oppKing.position.x);
        int y = Mathf.Abs(p.position.y - oppKing.position.y);

        if (x == 0 && y == 0)
        {
            return true;
        }

        return false;
    }

    private bool isDematerializedPhantomAttackingKing(Piece p, BoardState bs)
    {
        if (!isPhantomDematerialized(p))
        {
            return false;
        }

        List<coords> phantomAttacking = getIsolatedStatePieceMoves(p, bs, false);

        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1 || oppKing.alive == 0)
        {
            return false;
        }

        foreach (coords c in phantomAttacking)
        {
            int x = Mathf.Abs(p.position.x + c.x - oppKing.position.x);
            int y = Mathf.Abs(p.position.y + c.y - oppKing.position.y);

            if (x == 0 && y == 0)
            {
                return true;
            }
        }

        return false;
    }

    private const int MAX_PHANTOM_SEARCH_DEPTH = 5;

    private List<coords> getPhantomPathToKing(Piece p, BoardState bs)
    {
        // Only call this if dematerialized
        Piece oppKing = isolatedGetKing(bs, this.color * -1);

        if (oppKing == null || oppKing.position.x == -1 || oppKing.alive == 0)
        {
            return null;
        }

        List<coords> path = new List<coords>();
        HashSet<coords> visited = new HashSet<coords>();

        if (DFS_phantomToKing(p, bs, oppKing, path, visited, 0))
        {
            return path;
        }

        return null;
    }

    private bool DFS_phantomToKing(Piece p, BoardState bs, Piece oppKing, List<coords> path, HashSet<coords> visited, int depth)
    {
        if (isPiecePositionOppKing(p, oppKing))
        {
            return true;
        }

        if (depth >= MAX_PHANTOM_SEARCH_DEPTH)
        {
            return false;
        }

        coords currentPos = p.position;
        visited.Add(currentPos);

        List<coords> moves = getIsolatedStatePieceMoves(p, bs, false);

        foreach (coords offset in moves)
        {
            coords move = new coords(offset.x, offset.y);

            if (visited.Contains(move) || !checkBounds(move.x, move.y))
            {
                continue;
            }

            Debug.Log("DFS Searh Move: " + move.x + ", " + move.y);
            UndoMove undo = undo_simulatePieceMove(bs, p, move);

            path.Add(move);

            if (DFS_phantomToKing(p, bs, oppKing, path, visited, depth + 1))
            {
                return true;
            }

            path.RemoveAt(path.Count - 1);
            undoMove(undo, bs);
        }

        visited.Remove(currentPos);

        return false;
    }

    private bool isPiecePositionOppKing(Piece p, Piece oppKing)
    {
        return p.position.x == oppKing.position.x && p.position.y == oppKing.position.y;
    }

    override
    public NextMove nextMove()
    {
        float bestL2MoveDiff = -99999999f;
        int bestL2MoveBoardControl = -1;
        List<NextMove> validMoves_L2 = new List<NextMove>();

        Botkrieg_info botkrieg_info = new Botkrieg_info();
        List<Piece> piecesForInfo = getPiecesOnBoardState(this.currentBoardState, this.color);

        foreach (Piece pieceForInfo in piecesForInfo)
        {
            bool any = false;

            if (pieceForInfo.collateralType == 0)
            {
                botkrieg_info.atomic.Add(pieceForInfo);
                any = true;
            }
            else if (pieceForInfo.collateralType == 1)
            {
                botkrieg_info.landmine.Add(pieceForInfo);
                any = true;
            }
            else if (pieceForInfo.collateralType == 2)
            {
                botkrieg_info.freezeBomb.Add(pieceForInfo);
                any = true;
            }
            else if (checkAbility(pieceForInfo, PieceAbilities.Dematerialize) || checkAbility(pieceForInfo, PieceAbilities.Materialize))
            {
                botkrieg_info.phantom.Add(pieceForInfo);
                any = true;
            }
            else if (checkState(pieceForInfo, PieceState.Jailer))
            {
                botkrieg_info.jailer.Add(pieceForInfo);
                any = true;
            }

            if (any)
            {
                botkrieg_info.all.Add(pieceForInfo);
            }
        }

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        foreach (NextMove nextMove in allMoves)
        {
            var nextMoveVars = getNextMoveVars(nextMove);
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
                undo = undo_simulatePieceAbility(this.currentBoardState, nextMove.ability);
            }

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color * -1);

            float addPointsMoveOne = 0f;

            BoardState bestMoveOppBS = null;
            NextMove bestOppMove = null;
            float bestOppMoveDiff = +1000;

            Debug.LogWarning("Botkrieg: Simulating Piece Move: " + piece + " to " + coords.x + "," + coords.y);

            // If we do not need to look at more moves (ie. opp king can be exploded)
            if (isOppKingDead(this.currentBoardState))
            {
                Debug.Log("Botkrieg: Opponents King is Dead");

                undoMove(undo, this.currentBoardState);

                return nextMove;
            }


            // Move point addons
            if (isDematerializedPhantomOnKing(piece, this.currentBoardState))
            {
                Debug.Log("Botkrieg: Phantom Piece On King");

                addPointsMoveOne += 1f;
            }

            if (isAtomicAttackingKingCollateral(piece, this.currentBoardState))
            {
                Debug.Log("Botkrieg: Atomic Piece is Attacking King");

                addPointsMoveOne += 2f;
            }

            if (isOppKingJailed(this.currentBoardState))
            {
                Debug.Log("Botkrieg: Opponents King is Jailed");

                addPointsMoveOne += 1f;
            }

            if (isDematerializedPhantomAttackingKing(piece, this.currentBoardState))
            {
                Debug.Log("Botkrieg: Dematerialized Phantom Attacking King");

                addPointsMoveOne += 0.5f;
            }

            if (isFreezeBombAttackingKingCollateral(piece, this.currentBoardState))
            {
                Debug.Log("Botkrieg: Freeze Bomb Attacking King");

                addPointsMoveOne += 0.5f;
            }

            if (isLandmineAttackingNextToKing(piece, this.currentBoardState))
            {
                Debug.Log("Botkrieg: Landmine Attacking Next to King");

                addPointsMoveOne += 0.5f;
            }

            if (isLandmineNextToKing(piece, this.currentBoardState))
            {
                Debug.Log("Botkrieg: Landmine Next to King");

                addPointsMoveOne += 5f;
            }

            float subtractPointsMoveOne = 0f;

            if (moveInQueue(lastFiveMoves, nextMove))
            {
                subtractPointsMoveOne += 2f;
            }

            if (moveInQueueTwice(lastFiveMoves, nextMove))
            {
                subtractPointsMoveOne += 10f;
            }

            if (checkState(piece, PieceState.Dematerialized))
            {
                List<coords> phantomPath = getPhantomPathToKing(piece, this.currentBoardState);

                if (!(phantomPath == null || phantomPath.Count == 0))
                {
                    coords coordsPath = phantomPath[0];
                    if (coordsPath.x == coords.x && coordsPath.y == coords.y)
                    {
                        Debug.Log("Botkrieg: Phantom Move in Path");

                        addPointsMoveOne += 0.5f;
                    }
                }
            }

            var boardControlAttributes_levelOne = Botkrieg_getBoardControlOnBoardState(this.currentBoardState);
            List<int> cbc = boardControlAttributes_levelOne.choiceBoardControl;
            int choiceBoardControlBot = this.color == 1 ? cbc[0] : cbc[1];

            float addBoardControlMoveOne = choiceBoardControlBot;

            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                var nextMoveOppVars = getNextMoveVars(nextMoveOpp);
                Piece pieceOpp = nextMoveOppVars.piece;
                coords coordsOpp = nextMoveOppVars.coords;
                string moveTypeOpp = nextMoveOppVars.moveType;

                if (nextMoveOpp.moveType == "ability" && nextMoveOpp.ability.ability == PieceAbilities.Spawn)
                {
                    continue;
                }

                UndoMove undo_ = null;
                if (moveTypeOpp == "move")
                {
                    undo_ = undo_simulatePieceMove(this.currentBoardState, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
                }
                else
                {
                    undo_ = undo_simulatePieceAbility(this.currentBoardState, nextMoveOpp.ability);
                }

                float bestResponseDiff = -1000;
                NextMove bestResponse = null;

                List<NextMove> allBotMovesResponse = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

                foreach (NextMove nextMoveResponse in allBotMovesResponse)
                {
                    var nextMoveResponseVars = getNextMoveVars(nextMoveResponse);
                    Piece pieceResponse = nextMoveResponseVars.piece;
                    coords coordsResponse = nextMoveResponseVars.coords;
                    string moveTypeResponse = nextMoveResponseVars.moveType;

                    UndoMove undo__;
                    if (moveTypeResponse == "move")
                    {
                        undo__ = undo_simulatePieceMove(this.currentBoardState, pieceResponse, new coords(coordsResponse.x, coordsResponse.y));
                    }
                    else
                    {
                        undo__ = undo_simulatePieceAbility(this.currentBoardState, nextMoveResponse.ability);
                    }

                    List<float> pointsOnBoard_ = Botkrieg_getPointsOnBoardState(this.currentBoardState, true, pieceResponse, coordsResponse);
                    float botPoints__ = this.color == 1 ? pointsOnBoard_[0] : pointsOnBoard_[1];
                    float oppPoints__ = this.color == -1 ? pointsOnBoard_[0] : pointsOnBoard_[1];

                    float diff_ = botPoints__ - oppPoints__;

                    if (diff_ > bestResponseDiff || bestResponse == null)
                    {
                        bestResponseDiff = diff_;

                        bestResponse = nextMoveResponse;
                    }

                    undoMove(undo__, this.currentBoardState);
                }

                if (bestResponseDiff < bestOppMoveDiff || bestOppMove == null)
                {
                    bestOppMoveDiff = bestResponseDiff;

                    bestOppMove = nextMoveOpp;
                    bestMoveOppBS = copyBoardState(this.currentBoardState);
                }

                undoMove(undo_, this.currentBoardState);
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

                List<float> pointsOnBoard = Botkrieg_getPointsOnBoardState(bestMoveOppBS, true, piece_L2, coords_L2);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff = botPoints - oppPoints;
                float addDiff = (addPointsMoveOne * 0.5f) + (addBoardControlMoveOne * 0.1f);
                diff += addDiff;

                float minusDiff = subtractPointsMoveOne;
                diff -= minusDiff;

                if (diff >= bestL2MoveDiff)
                {
                    if (diff > bestL2MoveDiff)
                    {
                        Debug.LogWarning("Botkrieg: After Move Analysis: " + (diff - addDiff + minusDiff) + " (" + (addDiff - minusDiff - addBoardControlMoveOne) + ") " + " (" + (addDiff - minusDiff - addPointsMoveOne) + ").");

                        validMoves_L2.Clear();
                    }
                    else
                    {
                        var boardControlAttributes = Botkrieg_getBoardControlOnBoardState(bestMoveOppBS);
                        List<int> bc = boardControlAttributes.boardControl;
                        int boardControlBot = this.color == 1 ? bc[0] : bc[1];

                        if (boardControlBot > bestL2MoveBoardControl)
                        {
                            bestL2MoveBoardControl = boardControlBot;

                            validMoves_L2.Clear();
                        }
                    }

                    bestL2MoveDiff = diff;
                    validMoves_L2.Add(nextMove);
                }

                undoMove(undo_L2, bestMoveOppBS);
            }

            undoMove(undo, this.currentBoardState);
        }


        System.Random rand = new System.Random();

        if (validMoves_L2.Count == 0)
        {
            return getRandomBotMove(this);
        }
        else
        {
            int rndIdx = rand.Next(validMoves_L2.Count);

            NextMove move = validMoves_L2[rndIdx];

            lastFiveMoves.Enqueue(move);

            if (lastFiveMoves.Count > 6)
            {
                lastFiveMoves.Dequeue();
            }

            return move;
        }
    }

    private List<float> Botkrieg_getPointsOnBoardState(BoardState bs, bool isKingWorthMore, Piece movePiece, coords moveCoords)
    {
        if (bs == null)
        {
            if (this.color == 1)
            {
                return new List<float> { 0f, 100f };
            }
            else
            {
                return new List<float> { 100f, 0f };
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
                        //Debug.Log(piece.name + " found worth " + piece.points + ". Total is now " + wCount);
                    }
                    else
                    {
                        bCount += pts;
                    }
                }
            }
        }

        List<float> l = new List<float>();
        l.Add(wCount);
        l.Add(bCount);

        return l;
    }

    private (List<int> boardControl, List<int> choiceBoardControl) Botkrieg_getBoardControlOnBoardState(BoardState bs)
    {
        coords oppKingPos = filterPieces("King", this.opponentPieces)[0].position;
        coords kingPos = this.king.position;

        coords whiteKingPos = this.color == 1 ? kingPos : oppKingPos;
        coords blackKingPos = this.color == -1 ? kingPos : oppKingPos;

        List<int> boardControl = new List<int>();
        List<int> choiceBoardControl = new List<int>();

        var botMovesWhite = getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = getAllPossibleBotMoves(this, bs, -1);
        List<PieceMoveList> listBotBlackMoves = botMovesBlack.pieceMoveList;

        float score = 0;
        float choiceScore = 0;
        foreach (PieceMoveList pml in listBotWhiteMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                score += 8 - Mathf.Abs(coords.x - blackKingPos.x);
                score += 8 - Mathf.Abs(coords.y - blackKingPos.y);

                if (coords.x == blackKingPos.x && coords.y == blackKingPos.y)
                {
                    score += 16;
                }

                if (piece.collateralType == 0
                    || piece.collateralType == 1
                    || piece.collateralType == 2
                    || checkState(piece, PieceState.Dematerialized)
                    || checkState(piece, PieceState.Jailer)
                    || piece.lives == -1
                )
                {
                    choiceScore += 1;
                }
            }
        }
        boardControl.Add((int)score);
        choiceBoardControl.Add((int)choiceScore);

        choiceScore = 0;
        score = 0;
        foreach (PieceMoveList pml in listBotBlackMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                score += 8 - Mathf.Abs(coords.x - whiteKingPos.x);
                score += 8 - Mathf.Abs(coords.y - whiteKingPos.y);

                if (coords.x == whiteKingPos.x && coords.y == whiteKingPos.y)
                {
                    score += 16;
                }

                if (piece.collateralType == 0
                    || piece.collateralType == 1
                    || piece.collateralType == 2
                    || checkState(piece, PieceState.Dematerialized)
                    || checkState(piece, PieceState.Jailer)
                    || piece.lives == -1
                )
                {
                    choiceScore += 1;
                }
            }
        }
        boardControl.Add((int)score);
        choiceBoardControl.Add((int)choiceScore);

        return (boardControl, choiceBoardControl);
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
}
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class KamikazeBot : BotTemplate
{
    public KamikazeBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Kamikaze Bot";

        choosePieces();
    }

    private struct NextMoveBoardControl
    {
        public NextMove nm;
        public float boardControl;

        public NextMoveBoardControl(NextMove next, float bc)
        {
            nm = next;
            boardControl = bc;
        }
    }

    bool containsAtomic = false;
    bool containsLandmine = false;
    bool containsSpontaneousCombusting = false;
    bool containsInfinite = false;
    bool containsCrook = false;

    override
    public NextMove nextMove()
    {
        containsAtomic = false;
        containsLandmine = false;
        containsSpontaneousCombusting = false;
        containsInfinite = false;
        containsCrook = false;

        List<Piece> pieces = getPiecesOnBoardState(this.currentBoardState, color);
        foreach (Piece p in pieces)
        {
            if (p.baseType != "King")
            {
                if (p.collateralType == 0)
                {
                    containsAtomic = true;
                }

                if (p.collateralType == 1)
                {
                    containsLandmine = true;
                }

                if (HelperFunctions.checkState(p, PieceState.Combustable))
                {
                    containsSpontaneousCombusting = true;
                }

                if (HelperFunctions.checkState(p, PieceState.Crook))
                {
                    containsCrook = true;
                }

                if (p.lives > 0 || p.lives <= -1)
                {
                    containsInfinite = true;
                }
            }
        }

        NextMove move = null;

        if (containsAtomic)
        {
            Debug.Log("Kamikaze Bot Move Type: Atomic");
            move = atomicMove();
        }
        else if (containsLandmine || containsSpontaneousCombusting)
        {
            Debug.Log("Kamikaze Bot Move Type: Landmine");
            move = landmineMove();
        }
        else if (containsInfinite || containsCrook)
        {
            Debug.Log("Kamikaze Bot Move Type: Infinite");
            move = infiniteMove();
        }

        if (move == null)
        {
            Debug.Log("Kamikaze Bot Move Type: One Move");
            move = oneMove();
        }

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

    private NextMove landmineMove()
    {
        List<Piece> landmines = new List<Piece>();

        foreach (Piece p in this.pieces)
        {
            if (p.collateralType == 1 || HelperFunctions.checkState(p, PieceState.Combustable))
            {
                landmines.Add(p);
            }
        }

        bool hasMove = false;

        NextMove bestMove = null;
        int bestMoveDistanceFromKing = 10000;

        foreach (Piece landmine in landmines)
        {
            int dist = getPieceDistanceToKing(landmine);
            if (dist < bestMoveDistanceFromKing)
            {
                bestMoveDistanceFromKing = dist;
            }
        }

        bool breakOuter = false;

        //Simulate piece moves and opp moves to find out distance from king and if opp can capture
        //only simulate opp moves if new best atomic move
        foreach (Piece atomic in landmines)
        {
            if (breakOuter)
            {
                break;
            }

            List<NextMove> allMovesAtomic = getAllPossibleBotPieceMoves(this.currentBoardState, atomic);

            if (allMovesAtomic.Count == 0)
            {
                continue;
            }

            foreach (NextMove nextMove in allMovesAtomic)
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
                    continue;
                }

                hasMove = true;

                //If Landmine was ALREADY next to king, reject
                int[] distPrev = getPieceDistanceToKingXY(piece);
                if (distPrev[0] <= 1 && distPrev[1] <= 1)
                {
                    Debug.Log("Landmine Move: Instant Reject");
                    hasMove = false;
                    break;
                }

                BoardState originalBoardState = this.currentBoardState;

                BoardState cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                this.currentBoardState = cloneState;
                
                Piece clonePiece = getCloneFromOriginalPiece(piece, cloneState.boardGrid);
                int dist = getPieceDistanceToKing(clonePiece);
                int[] dist_ = getPieceDistanceToKingXY(clonePiece);

                bool pieceCanDie = false;

                if (dist_[0] <= 1 && dist_[1] <= 1)
                {
                    Debug.Log("Landmine Move: Instant Accept");
                    bestMove = nextMove;
                    breakOuter = true;
                    break;
                }

                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

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

                    Piece clonePiecePostOppMove = getCloneFromOriginalPiece(clonePiece, cloneState_.boardGrid);

                    this.currentBoardState = originalBoardState_;

                    if (clonePiecePostOppMove == null)
                    {
                        //Debug.Log("Landmine Move: " + piece.name + " can die. Do not accept.");
                        pieceCanDie = true;
                        break;
                    }
                }

                if (!pieceCanDie)
                {
                    if (dist < bestMoveDistanceFromKing && dist_[0] > 1 && dist_[1] > 1)
                    {
                        //Debug.Log("Landmine Move: " + piece.name + " cannot die. Accept.");
                        bestMoveDistanceFromKing = dist;
                        bestMove = nextMove;
                    }
                }

                this.currentBoardState = originalBoardState;
            }
        }

        if (containsInfinite || containsCrook)
        {
            Debug.Log("No Landmine Moves: Choosing Infinite Move");
            bestMove = infiniteMove();
        }
        else if (!hasMove || bestMove == null)
        {
            Debug.Log("No Landmine Moves: ");
            //Simulate piecemove in a way that maximizes board control for only landmines
            float bestBoardControl = -1000;
            List<NextMove> validMoves = new List<NextMove>();

            List<int> boardControlOnBS_Start = Kamikaze_getBoardControlOnBoardState(this.currentBoardState, 1);
            int botBoardControl_Start = this.color == 1 ? boardControlOnBS_Start[0] : boardControlOnBS_Start[1];

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

                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
                }

                List<int> boardControlOnBS = Kamikaze_getBoardControlOnBoardState(cloneState, 1);

                int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];
                //Debug.Log("Simulating Piece Move: " + piece.name + " to " + coords[0] + "," + coords[1] + ": " + botBoardControl);

                if (botBoardControl > bestBoardControl && !Kamimaze_canOppAttackPiece(getCloneFromOriginalPiece(piece, cloneState.boardGrid), cloneState))
                {
                    Debug.Log("New Best Landmine Board Control: " + botBoardControl + " Start: " + botBoardControl_Start);
                    bestMove = nextMove;
                    bestBoardControl = botBoardControl;
                }
            }

            if (bestBoardControl <= botBoardControl_Start)
            {
                bestMove = oneMove();
            }
        }

        return bestMove;
    }

    private NextMove infiniteMove()
    {
        List<Piece> infinites = new List<Piece>();

        foreach (Piece p in this.pieces)
        {
            if (p.lives > 0 || p.lives <= -1 || HelperFunctions.checkState(p, PieceState.Crook))
            {
                infinites.Add(p);
            }
        }

        NextMove bestMove = null;

        List<float> pob = Kamikaze_getPointsOnBoardState(this.currentBoardState, true);
        float originalPoints = this.color == 1 ? pob[0] : pob[1];
        float originalOppPoints = this.color == -1 ? pob[0] : pob[1];

        float bestMovePoints = originalPoints - originalOppPoints;

        List<int> boc = Kamikaze_getBoardControlOnBoardState(this.currentBoardState, 2);
        int originalBoardControl = this.color == 1 ? boc[0] : boc[1];

        int bestMoveBoardControl = originalBoardControl;
        bool foundKill = false;

        foreach (Piece infinite in infinites)
        {
            List<NextMove> allMovesAtomic = getAllPossibleBotPieceMoves(this.currentBoardState, infinite);

            if (allMovesAtomic.Count == 0)
            {
                continue;
            }

            foreach (NextMove nextMove in allMovesAtomic)
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
                    continue;
                }

                BoardState cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);

                List<int> boardControlOnBS = Kamikaze_getBoardControlOnBoardState(cloneState, 2);
                int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];

                List<float> pointsOnBoard = Kamikaze_getPointsOnBoardState(cloneState, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff = botPoints - oppPoints;

                if (diff > bestMovePoints)
                {
                    bestMovePoints = diff;
                    bestMove = nextMove;
                    foundKill = true;
                }
                else if (diff == bestMovePoints)
                {
                    if (botBoardControl > bestMoveBoardControl)
                    {
                        bestMoveBoardControl = botBoardControl;
                        bestMove = nextMove;
                    }
                }
            }
        }

        List<NextMoveBoardControl> possibleMoves = new List<NextMoveBoardControl>();

        Debug.Log("Kamikaze Best Board Control: " + bestMoveBoardControl + ". Kamikaze Original Board Control: " + originalBoardControl + " Found Kill: " + foundKill);

        if (bestMove == null || (bestMoveBoardControl <= originalBoardControl && !foundKill))
        {
            Debug.Log("No Legal Infinite Moves");
            //Simulate piecemove in a way that maximizes board control for only atomics
            float bestBoardControl = -1000;

            this.currentBoardState.refresh(convertBoardGrid(gameData.boardGrid));
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

                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
                }

                List<int> boardControlOnBS = Kamikaze_getBoardControlOnBoardState(cloneState, 2);

                int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];

                if (botBoardControl > bestBoardControl)
                {
                    possibleMoves.Clear();
                    possibleMoves.Add(new NextMoveBoardControl(nextMove, botBoardControl));

                    bestBoardControl = botBoardControl;
                    //Debug.Log("New Best Infinite Board Control: " + botBoardControl + " Found after " + piece.name + " moved to " + coords.x + "," + coords.y);
                }
            }

            if (bestBoardControl <= originalBoardControl)
            {
                bestMove = oneMove();
                possibleMoves.Clear();
            }
        }

        if (possibleMoves.Count > 0)
        {
            int rand = globalDefs.globalRand.Next(possibleMoves.Count);
            return possibleMoves[rand].nm;
        }

        return bestMove;
    }

    private NextMove atomicMove()
    {
        List<Piece> atomics = new List<Piece>();

        foreach (Piece p in this.pieces)
        {
            if (p.collateralType == 0)
            {
                atomics.Add(p);
            }
        }

        bool hasMove = false;

        NextMove bestMove = null;
        int bestMoveDistanceFromKing = 100000;

        foreach(Piece atomic in atomics)
        {
            int dist = getPieceDistanceToKing(atomic);
            if (dist < bestMoveDistanceFromKing)
            {
                bestMoveDistanceFromKing = dist;
            }
        }

        //Simulate piece moves and opp moves to find out distance from king and if opp can capture
        //only simulate opp moves if new best atomic move
        foreach (Piece atomic in atomics)
        {
            List<NextMove> allMovesAtomic = getAllPossibleBotPieceMoves(this.currentBoardState, atomic);

            Debug.Log("Analyzing atomic: " + atomic.name + " All Moves: " + allMovesAtomic.Count);

            if (allMovesAtomic.Count == 0)
            {
                continue;
            }

            hasMove = true;

            foreach(NextMove nextMove in allMovesAtomic)
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
                    continue;
                }

                //ebug.Log("Analyzing atomic move to: " + coords[0] + "," + coords[1]);

                BoardState originalBoardState = this.currentBoardState;

                BoardState cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                this.currentBoardState = cloneState;

                Piece clonePiece = getCloneFromOriginalPiece(piece, cloneState.boardGrid);
                int dist = getPieceDistanceToKing(clonePiece);

                List<Piece> oppPieces = getPiecesOnBoardState(cloneState, color * -1);
                if (filterPieces("King", oppPieces).Count == 0) //Opp king died
                {
                    return nextMove;
                }

                bool pieceCanDie = false;
                List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

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

                    Piece clonePiecePostOppMove = getCloneFromOriginalPiece(piece, cloneState_.boardGrid);

                    this.currentBoardState = originalBoardState_;

                    if (clonePiecePostOppMove == null)
                    {
                        pieceCanDie = true;
                        break;
                    }

                }

                if (!pieceCanDie)
                {
                    if (dist < bestMoveDistanceFromKing)
                    {
                        Debug.Log("New Best Move Found");
                        bestMoveDistanceFromKing = dist;
                        bestMove = nextMove;
                    }
                }
                else
                {
                    Debug.Log("Move Rejected");
                }

                this.currentBoardState = originalBoardState;
            }
        }

        if ((containsInfinite || containsCrook) && bestMove == null)
        {
            bestMove = infiniteMove();
        }
        else if (!hasMove || bestMove == null)
        {
            //Debug.Log("No Legal Atomic Moves");
            //Simulate piecemove in a way that maximizes board control for only atomics
            float bestBoardControl = -1000;
            List<NextMove> validMoves = new List<NextMove>();

            List<int> boardControlOnBS_Start = Kamikaze_getBoardControlOnBoardState(this.currentBoardState, 0);
            int botBoardControl_Start = this.color == 1 ? boardControlOnBS_Start[0] : boardControlOnBS_Start[1];

            this.currentBoardState.refresh(convertBoardGrid(gameData.boardGrid));
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

                BoardState cloneState;
                if (moveType == "move")
                {
                    cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
                }
                else
                {
                    cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
                }

                List<int> boardControlOnBS = Kamikaze_getBoardControlOnBoardState(cloneState, 0);

                int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];

                if (botBoardControl > bestBoardControl && !Kamimaze_canOppAttackPiece(getCloneFromOriginalPiece(piece, cloneState.boardGrid), cloneState))
                {
                    Debug.Log("New Best Atomic Board Control: " + botBoardControl + " Board Control Start: " + botBoardControl_Start);
                    bestMove = nextMove;
                    bestBoardControl = botBoardControl;
                }
            }

            if (bestBoardControl == 0 || bestBoardControl - botBoardControl_Start == 0)
            {
                bestMove = oneMove();
            }
        }

        return bestMove;
    }

    public bool Kamimaze_canOppAttackPiece(Piece piece, BoardState bs)
    {
        if (piece == null)
        {
            return true;
        }

        coords coords = piece.position;

        List<Piece> allPiecesOpp = getPiecesOnBoardState(bs, this.color * -1);

        List<NextMove> allMovesOpp = new List<NextMove>();
        foreach (Piece p in allPiecesOpp)
        {
            List<NextMove> allPieceMovesOpp = getAllPossibleBotPieceAttacks(bs, p);
            allMovesOpp.AddRange(allPieceMovesOpp);
        }

        foreach (NextMove nextMove in allMovesOpp)
        {
            coords oppAttackCoords = nextMove.moveType == "move" ? nextMove.move.coords : nextMove.ability.coords;

            if (coords.x == oppAttackCoords.x && coords.y == oppAttackCoords.y)
            {
                return true;
            }
        }

        return false;
    }

    private List<int> Kamikaze_getBoardControlOnBoardState(BoardState bs, int pieceType)
    {
        //0: Atomic
        //1: Landmine || Spontaneously Combusting
        //2: Infinite || Crook

        List<int> boardControl = new List<int>();

        List<NextMove> listBotMovesWhite = new List<NextMove>();
        List<NextMove> listBotMovesBlack = new List<NextMove>();

        List<Piece> piecesOnBS = getPiecesOnBoardState(bs, 1);
        piecesOnBS.AddRange(getPiecesOnBoardState(bs, -1));

        if (pieceType == 0)
        {
            foreach (Piece p in piecesOnBS)
            {
                if (p.collateralType == 0)
                {
                    List<NextMove> allMovesAtomic = getAllPossibleBotPieceMoves(bs, p);
                    
                    if (p.color == 1)
                    {
                        if (allMovesAtomic.Count != 0) listBotMovesWhite.AddRange(allMovesAtomic);
                    }
                    else
                    {
                        if (allMovesAtomic.Count != 0) listBotMovesBlack.AddRange(allMovesAtomic);
                    }
                }
            }
        }
        else if (pieceType == 1)
        {
            foreach (Piece p in piecesOnBS)
            {
                if (p.collateralType == 1 || HelperFunctions.checkState(p, PieceState.Combustable))
                {
                    List<NextMove> landMine = getAllPossibleBotPieceMoves(bs, p);

                    if (p.color == 1)
                    {
                        if (landMine.Count != 0) listBotMovesWhite.AddRange(landMine);
                    }
                    else
                    {
                        if (landMine.Count != 0) listBotMovesBlack.AddRange(landMine);
                    }
                }
            }
        }
        else if (pieceType == 2)
        {
            foreach (Piece p in piecesOnBS)
            {
                if (HelperFunctions.checkState(p, PieceState.Crook) || p.lives > 0 || p.lives <= -1)
                {
                    List<NextMove> infinite = getAllPossibleBotPieceMoves(bs, p);

                    if (p.color == 1)
                    {
                        if (infinite.Count != 0) listBotMovesWhite.AddRange(infinite);
                    }
                    else
                    {
                        if (infinite.Count != 0) listBotMovesBlack.AddRange(infinite);
                    }
                }
            }
        }

        List<coords> uniqueCoords = new List<coords>();

        foreach (NextMove nextMove in listBotMovesWhite)
        {
            Piece piece;
            coords coords;

            if (nextMove.moveType == "move")
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

            if (!HelperFunctions.coordsInList(uniqueCoords, coords))
            {
                uniqueCoords.Add(coords);
            }
        }

        boardControl.Add(uniqueCoords.Count);

        uniqueCoords = new List<coords>();

        foreach (NextMove nextMove in listBotMovesBlack)
        {
            Piece piece;
            coords coords;

            if (nextMove.moveType == "move")
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

            if (!HelperFunctions.coordsInList(uniqueCoords, coords))
            {
                uniqueCoords.Add(coords);
            }
        }

        boardControl.Add(uniqueCoords.Count);

        return boardControl;
    }

    private int getPieceDistanceToKing(Piece piece)
    {
        Piece oppKing = filterPieces("King", this.opponentPieces).Count == 0 ? null : filterPieces("King", this.opponentPieces)[0];

        if (oppKing == null || piece == null)
        {
            return 100000;
        }
        else
        {
            return Mathf.Abs(piece.position.x - oppKing.position.x) + Mathf.Abs(piece.position.y - oppKing.position.y);
        }
    }

    private int[] getPieceDistanceToKingXY(Piece piece)
    {
        Piece oppKing = filterPieces("King", this.opponentPieces).Count == 0 ? null : filterPieces("King", this.opponentPieces)[0];

        if (oppKing == null || piece == null)
        {
            return new int[] { 100, 100 };
        }
        else
        {
            return new int[] { Mathf.Abs(piece.position.x - oppKing.position.x), Mathf.Abs(piece.position.y - oppKing.position.y) };
        }
    }

    public static List<float> Kamikaze_getPointsOnBoardState(BoardState bs, bool isKingWorthMore)
    {
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
                        pts = 1f;
                    }

                    if (isKingWorthMore && piece.baseType == "King")
                    {
                        pts += 100;
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

    private NextMove oneMove()
    {
        Debug.Log("In Kamikaze Bot OneMove");

        int bestBoardControlDiff = -1000;
        float bestMoveDiff = -1000;

        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        //Each piece
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

            //best simulated move opponent can make
            NextMove bestOppMove = null;
            float bestOppMoveDiff = +1000;
            int bestOppBoardControlDiff = +1000;
            BoardState bestMoveBS = null;

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

                List<int> boardControlOnBS = getBoardControlOnBoardState(cloneState_);
                int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];
                int oppBoardControl = this.color == -1 ? boardControlOnBS[0] : boardControlOnBS[1];

                List<float> pointsOnBoard = Kamikaze_getPointsOnBoardState(cloneState_, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                //Debug.LogWarning("Points on board after " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                float diff_ = botPoints - oppPoints;
                if (diff_ < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff_;
                    bestOppMove = nextMoveOpp;
                    bestOppBoardControlDiff = botBoardControl - oppBoardControl;
                    bestMoveBS = cloneState_;
                }
            }

            //if (nextMove.moveType == "move") Debug.Log("Analyzed move: " + nextMove.move.p.name + " to " + coords[0] + "," + coords[1] + ". Points Diff: " + bestOppMoveDiff + " Board Control Diff: " + bestOppBoardControlDiff);
            //if (nextMove.moveType == "ability") Debug.Log("Analyzed ability: " + nextMove.ability.piece.name + ": " + nextMove.ability.ability + " to " + coords[0] + "," + coords[1] + ". Points Diff: " + bestOppMoveDiff + " Board Control Diff: " + bestOppBoardControlDiff);

            //if (bestOppMove.moveType == "move") Debug.Log("Best opp move: " + bestOppMove.move.p.name + " to " + bestOppMove.move.coords[0] + "," + bestOppMove.move.coords[1]);
            //if (bestOppMove.moveType == "ability") Debug.Log("Best opp ability: " + bestOppMove.ability.piece.name + ": " + bestOppMove.ability.ability + " to " + bestOppMove.ability.coords[0] + "," + bestOppMove.ability.coords[1]);


            //debug_printBoardState(bestMoveBS);

            float diff = bestOppMoveDiff;

            if (piece.collateralType == 1)
            {
                int[] distance = getPieceDistanceToKingXY(piece);

                if (distance[0] == 1 || distance[1] == 0)
                {
                    diff -= 10f;
                }
            }

            int boardControlDiff = bestOppBoardControlDiff;

            if (diff > bestMoveDiff)
            {
                bestMoveDiff = diff;
                bestBoardControlDiff = boardControlDiff;

                validMoves.Clear();
                validMoves.Add(nextMove);
            }
            else if (diff == bestMoveDiff)
            {
                if (boardControlDiff > bestBoardControlDiff)
                {
                    bestBoardControlDiff = boardControlDiff;

                    validMoves.Clear();
                    validMoves.Add(nextMove);
                }
                else if (boardControlDiff == bestBoardControlDiff)
                {
                    validMoves.Add(nextMove);
                }
            }

            this.currentBoardState = originalBoardState;
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
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

    private List<int> getBoardControlOnBoardState(BoardState bs)
    {
        coords oppKingPos = filterPieces("King", this.opponentPieces)[0].position;
        coords kingPos = this.king.position;

        coords whiteKingPos = this.color == 1 ? kingPos : oppKingPos;
        coords blackKingPos = this.color == -1 ? kingPos : oppKingPos;

        List<int> boardControl = new List<int>();

        var botMovesWhite = getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = getAllPossibleBotMoves(this, bs, -1);
        List<PieceMoveList> listBotBlackMoves = botMovesBlack.pieceMoveList;

        List<coords> uniqueCoords = new List<coords>();
        float score = 0;
        foreach (PieceMoveList pml in listBotWhiteMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                if (!HelperFunctions.coordsInList(uniqueCoords, coords))
                {
                    uniqueCoords.Add(coords);
                    score += 8 - Mathf.Abs(coords.x - blackKingPos.x);
                    score += 8 - Mathf.Abs(coords.y - blackKingPos.y);

                    if (coords.x == blackKingPos.x && coords.y == blackKingPos.y)
                    {
                        score += 16;
                    }
                }
            }
        }
        boardControl.Add((int)score);

        uniqueCoords = new List<coords>();
        score = 0;
        foreach (PieceMoveList pml in listBotBlackMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                if (!HelperFunctions.coordsInList(uniqueCoords, coords))
                {
                    uniqueCoords.Add(coords);
                    score += 8 - Mathf.Abs(coords.x - whiteKingPos.x);
                    score += 8 - Mathf.Abs(coords.y - whiteKingPos.y);

                    if (coords.x == whiteKingPos.x && coords.y == whiteKingPos.y)
                    {
                        score += 16;
                    }
                }
            }
        }
        boardControl.Add((int)score);

        return boardControl;
    }
}
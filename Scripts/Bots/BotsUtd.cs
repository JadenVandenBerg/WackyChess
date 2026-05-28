using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static HelperFunctions;

public class BotsUtd : BotTemplate
{
    public BotsUtd(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Bots Utd.";

        choosePieces();
    }

    Queue<NextMove> lastFiveMoves = new Queue<NextMove>();

    bool containsAtomic = false;
    bool containsLandmine = false;
    bool containsSpontaneousCombusting = false;
    bool containsInfinite = false;
    bool containsCrook = false;

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

    override
    public NextMove nextMove()
    {
        List<string> options = new List<string>
        {
            "Bloodbot",
            "SavageBeastBot",
            "RandomBot",
            "ThinkingBot",
            "G2EBot",
            "KamikazeBot",
            "Lobotomy",
            "BerserkerBot"
        };

        int index = globalDefs.globalRand.Next(options.Count);

        string bot = options[index];

        Debug.Log("Executing " + bot + " move in BotUtd");

        if (bot == "RandomBot")
        {
            return RandomBot_nextMove();
        }
        else if(bot == "BerserkerBot")
        {
            return BerserkerBot_nextMove();
        }
        else if (bot == "KamikazeBot")
        {
            return KamikazeBot_nextMove();
        }
        else if (bot == "ThinkingBot")
        {
            return ThinkingBot_nextMove();
        }
        else if (bot == "SavageBeastBot")
        {
            return SavageBeastBot_nextMove();
        }
        else if (bot == "Bloodbot")
        {
            return Bloodbot_nextMove();
        }
        else if (bot == "G2EBot")
        {
            return G2EBot_nextMove();
        }
        else if (bot == "Lobotomy")
        {
            return Lobotomy_nextMove();
        }

        return RandomBot_nextMove();
    }

    public NextMove RandomBot_nextMove()
    {
        this.currentBoardState.refresh(BotHelperFunctions.convertBoardGrid(gameData.boardGrid));
        this.currentBoardState = BotHelperFunctions.copyBoardState(this.currentBoardState);

        NextMove move_ = BotHelperFunctions.getRandomBotMove(this);

        if (move_.moveType == "move")
        {
            move_.move.p = BotHelperFunctions.getOriginalPieceFromClone(move_.move.p);
        }
        else
        {
            move_.ability.piece = BotHelperFunctions.getOriginalPieceFromClone(move_.ability.piece);
        }

        return move_;
    }

    public NextMove BerserkerBot_nextMove()
    {
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();

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

            List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, true);
            float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
            float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

            float diff = botPoints - oppPoints;

            if (diff >= bestMoveDiff)
            {
                if (diff > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                validMoves.Add(nextMove);
                bestMoveDiff = diff;
            }

            undoMove(undo, this.currentBoardState);
        }


        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
        return move;
    }

    public NextMove KamikazeBot_nextMove()
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

        foreach (Piece atomic in atomics)
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

    public NextMove Bloodbot_nextMove()
    {
        int bestBoardControlDiff = -1000;
        float bestMoveDiff = -1000;

        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        //float startingDiff = botPoints_ - oppPoints_;

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

                List<int> boardControlOnBS = Bloodbot_getBoardControlOnBoardState(cloneState_);
                int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];
                int oppBoardControl = this.color == -1 ? boardControlOnBS[0] : boardControlOnBS[1];

                List<float> pointsOnBoard = BloodBot_getPointsOnBoardState(cloneState_, true);
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

    private List<int> Bloodbot_getBoardControlOnBoardState(BoardState bs)
    {
        coords oppKingPos = BotHelperFunctions.filterPieces("King", this.opponentPieces)[0].position;
        coords kingPos = this.king.position;

        coords whiteKingPos = this.color == 1 ? kingPos : oppKingPos;
        coords blackKingPos = this.color == -1 ? kingPos : oppKingPos;

        List<int> boardControl = new List<int>();

        var botMovesWhite = BotHelperFunctions.getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = BotHelperFunctions.getAllPossibleBotMoves(this, bs, -1);
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

                    if (coords.x == blackKingPos.x && coords.x == blackKingPos.y)
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

    private static List<float> BloodBot_getPointsOnBoardState(BoardState bs, bool isKingWorthMore)
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
                    if (isKingWorthMore && piece.baseType == "King")
                    {
                        pts += 100;
                    }

                    if (piece.color == 1)
                    {
                        wCount += pts + 2;
                        //Debug.Log(piece.name + " found worth " + piece.points + ". Total is now " + wCount);
                    }
                    else
                    {
                        bCount += pts + 2;
                    }
                }
            }
        }

        List<float> l = new List<float>();
        l.Add(wCount);
        l.Add(bCount);

        return l;
    }

    public NextMove G2EBot_nextMove()
    {
        int bestBoardControlDiff = -1000;
        float bestMoveDiff = -1000;

        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        int rand_ = globalDefs.globalRand.Next(1, 4);

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

                List<int> boardControlOnBS = G2EBot_getBoardControlOnBoardState(cloneState_);
                int botBoardControl = this.color == 1 ? boardControlOnBS[0] : boardControlOnBS[1];
                int oppBoardControl = this.color == -1 ? boardControlOnBS[0] : boardControlOnBS[1];

                List<float> pointsOnBoard = G2EBot_getPointsOnBoardState(cloneState_, true, pieceOpp, coordsOpp);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float oppPenalty = 0f;
                //Debug.LogWarning("Points on board after " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100) + ". Diff: " + (botPoints - oppPoints));
                if (nextMoveOpp.moveType == "ability" && nextMoveOpp.ability.ability == PieceAbilities.Spawn)
                {
                    oppPenalty += nextMoveOpp.ability.piece.points;
                }

                float diff_ = botPoints - (oppPoints - oppPenalty);
                if (diff_ <= bestOppMoveDiff)
                {
                    int boardControlDiff_ = botBoardControl - oppBoardControl;

                    if (diff_ < bestOppMoveDiff)
                    {
                        bestOppMoveDiff = diff_;
                        bestOppMove = nextMoveOpp;
                        bestMoveBS = cloneState_;
                        bestOppBoardControlDiff = boardControlDiff_;
                    }
                    else if (boardControlDiff_ > bestOppBoardControlDiff)
                    {
                        bestOppMoveDiff = diff_;
                        bestOppMove = nextMoveOpp;
                        bestMoveBS = cloneState_;
                        bestOppBoardControlDiff = boardControlDiff_;
                    }
                }
            }

            float endPenalty = 0f;

            if (nextMove.moveType == "move" && HelperFunctions.checkState(nextMove.move.p, PieceState.Fragile))
            {
                endPenalty += 1f;

                if (nextMove.move.p.baseType == "King")
                {
                    endPenalty += 50f;
                }
            }

            if (nextMove.moveType == "move" && nextMove.move.p.baseType == "King")
            {
                endPenalty += 2f;
            }

            if (HelperFunctions.checkState(piece, PieceState.Delayed))
            {
                endPenalty += 0.1f;
            }

            if (moveInQueue(lastFiveMoves, nextMove))
            {
                endPenalty += 2f;
            }

            if (moveInQueueTwice(lastFiveMoves, nextMove))
            {
                endPenalty += 10f;
            }

            List<float> pointsOnBoard_FINAL = G2EBot_getPointsOnBoardState(bestMoveBS, true, piece, coords);
            float botPoints_ = this.color == 1 ? pointsOnBoard_FINAL[0] : pointsOnBoard_FINAL[1];
            float oppPoints_ = this.color == -1 ? pointsOnBoard_FINAL[0] : pointsOnBoard_FINAL[1];

            //List<float> NORMAL_pointsOnBoard = getPointsOnBoardState(bestMoveBS, true);
            //float NORMAL_botPoints = this.color == 1 ? NORMAL_pointsOnBoard[0] : NORMAL_pointsOnBoard[1];
            //float NORMAL_oppPoints = this.color == -1 ? NORMAL_pointsOnBoard[0] : NORMAL_pointsOnBoard[1];

            if (oppPoints_ < 5.0f && piece.baseType == "Pawn")
            {
                if (rand_ == 2)
                {
                    endPenalty -= 0.1f;
                }
            }

            //if (nextMove.moveType == "move") Debug.Log("Analyzed move: " + nextMove.move.p.name + " to " + coords[0] + "," + coords[1] + ". Normal Diff: " + (NORMAL_botPoints - NORMAL_oppPoints) + ". Unmodified Diff: " + bestOppMoveDiff + ". Points Diff: " + (botPoints_ - oppPoints_) + ". Penalty Diff: " + ((botPoints_ - oppPoints_) - endPenalty) + " Board Control Diff: " + bestOppBoardControlDiff);
            //if (nextMove.moveType == "ability") Debug.Log("Analyzed ability: " + nextMove.ability.piece.name + ": " + nextMove.ability.ability + " to " + coords[0] + "," + coords[1] + ". Normal Diff: " + (NORMAL_botPoints - NORMAL_oppPoints) + ". Points Diff: " + (botPoints_ - oppPoints_) + " Board Control Diff: " + bestOppBoardControlDiff);

            //if (bestOppMove.moveType == "move") Debug.Log("Best opp move: " + bestOppMove.move.p.name + " to " + bestOppMove.move.coords[0] + "," + bestOppMove.move.coords[1]);
            //if (bestOppMove.moveType == "ability") Debug.Log("Best opp ability: " + bestOppMove.ability.piece.name + ": " + bestOppMove.ability.ability + " to " + bestOppMove.ability.coords[0] + "," + bestOppMove.ability.coords[1]);


            //debug_printBoardState(bestMoveBS);

            float diff = (botPoints_ - oppPoints_) - endPenalty;
            int boardControlDiff = bestOppBoardControlDiff;

            // Pre Move Checks
            if (diff >= bestMoveDiff)
            {
                //Check for stalemate
                if (isBoardStateOppStalemate(cloneState))
                {
                    diff -= 25f;
                }
            }

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

        lastFiveMoves.Enqueue(move);

        if (lastFiveMoves.Count > 6)
        {
            lastFiveMoves.Dequeue();
        }

        return move;
    }

    private List<int> G2EBot_getBoardControlOnBoardState(BoardState bs)
    {
        coords oppKingPos = BotHelperFunctions.filterPieces("King", this.opponentPieces)[0].position;
        coords kingPos = this.king.position;

        coords whiteKingPos = this.color == 1 ? kingPos : oppKingPos;
        coords blackKingPos = this.color == -1 ? kingPos : oppKingPos;

        List<int> boardControl = new List<int>();

        var botMovesWhite = BotHelperFunctions.getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = BotHelperFunctions.getAllPossibleBotMoves(this, bs, -1);
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

    private List<float> G2EBot_getPointsOnBoardState(BoardState bs, bool isKingWorthMore, Piece movePiece, coords moveCoords)
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

                    if (HelperFunctions.checkState(piece, PieceState.Fragile))
                    {
                        pts += 1.5f;
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Shield))
                    {
                        pts -= piece.points;
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Frozen))
                    {
                        pts -= piece.points / 2;
                    }

                    if (x == moveCoords.x - 1 && y == moveCoords.y - 1)
                    {
                        if (HelperFunctions.checkState(piece, PieceState.Electric))
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

    public NextMove SavageBeastBot_nextMove()
    {
        coords bestMoveCoords;
        int bestBoardControlDiff = -1000;

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
            NextMove bestOppMove;
            float bestOppMoveDiff = +1000;
            int bestOppBoardControlDiff = +1000;

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

                List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                //if (this.color == 1) Debug.LogWarning("Points on board after " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                float diff = botPoints - oppPoints;
                if (diff < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff;
                    bestOppMove = nextMoveOpp;
                    bestOppBoardControlDiff = botBoardControl - oppBoardControl;
                }
            }

            // Take the best outcome assuming the opponent captures the highest value piece it can
            if (bestOppBoardControlDiff >= bestBoardControlDiff)
            {
                if (bestOppBoardControlDiff > bestBoardControlDiff)
                {
                    validMoves.Clear();
                }

                bestBoardControlDiff = bestOppBoardControlDiff;
                bestMoveCoords = coords;

                validMoves.Add(nextMove);
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

    public NextMove Lobotomy_nextMove()
    {
        float bestMoveDiff = +1000;
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        foreach (NextMove nextMove in allMoves)
        {
            var nextMoveVars = getNextMoveVars(nextMove);
            Piece piece = nextMoveVars.piece;
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            bool landmineKingTouch = false;
            if (piece.collateralType == 1)
            {
                if (isPieceTouchingKing(piece))
                {
                    landmineKingTouch = true;
                }
            }


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

            NextMove bestOppNextMove;
            float bestOppMoveDiff = +1000;

            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                var nextMoveOppVars = getNextMoveVars(nextMoveOpp);
                Piece pieceOpp = nextMoveOppVars.piece;
                coords coordsOpp = nextMoveOppVars.coords;
                string moveTypeOpp = nextMoveOppVars.moveType;

                UndoMove undo_ = null;
                if (moveTypeOpp == "move")
                {
                    undo_ = undo_simulatePieceMove(this.currentBoardState, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
                }
                else
                {
                    undo_ = undo_simulatePieceAbility(this.currentBoardState, nextMoveOpp.ability);
                }

                List<float> pointsOnBoard = getPointsOnBoardState(this.currentBoardState, false);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff = botPoints - oppPoints;
                if (diff < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff;
                    bestOppNextMove = nextMoveOpp;
                }

                undoMove(undo_, this.currentBoardState);
            }

            float penalty = 0f;

            if (HelperFunctions.checkState(piece, PieceState.Fragile))
            {
                penalty += 0.5f;

                if (piece.baseType == "King")
                {
                    penalty += 10f;
                }
            }

            float kingDist = -(checkKingDistance() / 10);
            penalty += kingDist;

            if (isCheckmateMinusBlocking())
            {
                penalty -= 200;
            }

            Piece king = isolatedGetKing(this.currentBoardState, this.color);
            if (king != null && isInCheck(king.position))
            {
                penalty -= 200;
            }

            Piece enemyKing = isolatedGetKing(this.currentBoardState, this.color * -1);
            if (enemyKing != null && isInCheck(enemyKing.position))
            {
                penalty -= 50;
            }

            if (!landmineKingTouch && piece.collateralType == 1)
            {
                if (isPieceTouchingKing(piece))
                {
                    penalty += 15f;
                }
            }

            float realDiff = bestOppMoveDiff - penalty;

            if (realDiff <= bestMoveDiff)
            {
                if (realDiff < bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = realDiff;
                validMoves.Add(nextMove);
            }

            undoMove(undo, this.currentBoardState);
        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
        return move;
    }

    private bool isPieceTouchingKing(Piece piece)
    {
        Piece king = BotHelperFunctions.isolatedGetKing(this.currentBoardState, this.color);

        if (king == null)
        {
            return false;
        }

        int x = Mathf.Abs(piece.position.x - king.position.x);
        int y = Mathf.Abs(piece.position.y - king.position.y);

        if (x == 1 || y == 1)
        {
            return true;
        }

        return false;
    }

    private float checkKingDistance()
    {
        Piece king = BotHelperFunctions.isolatedGetKing(this.currentBoardState, this.color);
        Piece oppKing = BotHelperFunctions.isolatedGetKing(this.currentBoardState, this.color * -1);

        if (king == null || oppKing == null)
        {
            return 0;
        }

        return ((float)System.Math.Sqrt(king.position.x * oppKing.position.x + king.position.y * oppKing.position.y));
    }

    private bool isCheckmateMinusBlocking()
    {
        Piece oppKing = BotHelperFunctions.isolatedGetKing(this.currentBoardState, this.color * -1);

        if (oppKing == null)
        {
            return true;
        }

        coords oppKingPos = oppKing.position;

        List<coords> oppKingMoves = getIsolatedStatePieceMoves(oppKing, this.currentBoardState, false);
        oppKingMoves.Add(new coords(oppKingPos.x, oppKingPos.y));

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        foreach (NextMove nextMove in allMoves)
        {
            var nextMoveVars = getNextMoveVars(nextMove);
            Piece piece = nextMoveVars.piece;
            coords coords = nextMoveVars.coords;
            string moveType = nextMoveVars.moveType;

            foreach (coords oppKingPotentialCoords in new List<coords>(oppKingMoves))
            {
                if (oppKingPotentialCoords.x == coords.x && oppKingPotentialCoords.y == coords.y)
                {
                    oppKingMoves.RemoveAll(a => a.x == coords.x && a.y == coords.y);
                }
            }
        }

        return oppKingMoves.Count == 0;
    }

    private bool isInCheck(coords kingCoords)
    {
        bool check = false;

        var allAttacks = getAllPossibleBotAttacks(this, this.currentBoardState, this.color * -1);
        List<PieceMoveList> pml = allAttacks.pieceMoveList;

        foreach (PieceMoveList pml_ in pml)
        {
            foreach (coords coords in pml_.moves)
            {
                if (kingCoords.x == coords.x && kingCoords.y == coords.y)
                {
                    check = true;
                }
            }
        }

        return check;
    }

    public NextMove ThinkingBot_nextMove()
    {
        float bestMoveDiff_L2 = -1000;
        int L2_bestBoardControl = -1;
        List<NextMove> validMoves_L2 = new List<NextMove>();

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

            BoardState bestMoveOppBS = null;
            NextMove bestOppMove = null;
            float bestOppMoveDiff = +1000;

            if (isBoardStateOppStalemate(this.currentBoardState))
            {
                continue;
            }

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

                    List<float> pointsOnBoard_ = ThinkingBot_getPointsOnBoardState(this.currentBoardState, true, pieceResponse, coordsResponse);
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
                //todo
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

                List<NextMove> allMovesOpp_L2 = getAllPossibleBotMovesAndAbilities(this, bestMoveOppBS, this.color * -1);

                float bestOppMoveDiff_L2 = +1000;
                int L2_bestBoardControl_ = -1;

                foreach (NextMove nextMoveOpp_L2 in allMovesOpp_L2)
                {
                    var nextMoveOppVars = getNextMoveVars(nextMoveOpp_L2);
                    Piece pieceOpp = nextMoveOppVars.piece;
                    coords coordsOpp = nextMoveOppVars.coords;
                    string moveTypeOpp = nextMoveOppVars.moveType;

                    if (nextMoveOpp_L2.moveType == "ability" && nextMoveOpp_L2.ability.ability == PieceAbilities.Spawn)
                    {
                        continue;
                    }

                    UndoMove undo_ = null;
                    if (moveTypeOpp == "move")
                    {
                        undo_ = undo_simulatePieceMove(bestMoveOppBS, pieceOpp, new coords(coordsOpp.x, coordsOpp.y));
                    }
                    else
                    {
                        undo_ = undo_simulatePieceAbility(bestMoveOppBS, nextMoveOpp_L2.ability);
                    }

                    List<float> pointsOnBoard = ThinkingBot_getPointsOnBoardState(bestMoveOppBS, true, pieceOpp, coordsOpp);
                    float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                    float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                    float diff_L2 = botPoints - oppPoints;
                    if (diff_L2 < bestOppMoveDiff_L2)
                    {
                        bestOppMoveDiff_L2 = diff_L2;

                        List<int> bc = ThinkingBot_getBoardControlOnBoardState(bestMoveOppBS);
                        int boardControlBot = this.color == 1 ? bc[0] : bc[1];

                        if (boardControlBot > L2_bestBoardControl_)
                        {
                            L2_bestBoardControl_ = boardControlBot;
                        }
                    }

                    undoMove(undo_, bestMoveOppBS);
                }

                float endPenalty = 0f;

                if (moveInQueue(lastFiveMoves, nextMove))
                {
                    endPenalty += 2f;
                }

                if (moveInQueueTwice(lastFiveMoves, nextMove))
                {
                    endPenalty += 10f;
                }

                if (HelperFunctions.checkState(piece, PieceState.Fragile)) //L1 Move
                {
                    endPenalty += 1f;

                    if (piece.baseType == "King")
                    {
                        endPenalty += 50f;
                    }
                }

                float diff = bestOppMoveDiff_L2 - endPenalty;

                if (diff >= bestMoveDiff_L2)
                {
                    if (diff > bestMoveDiff_L2)
                    {
                        validMoves_L2.Clear();
                    }
                    else
                    {
                        if (L2_bestBoardControl_ > L2_bestBoardControl)
                        {
                            L2_bestBoardControl = L2_bestBoardControl_;

                            validMoves_L2.Clear();
                        }
                    }

                    bestMoveDiff_L2 = bestOppMoveDiff_L2;
                    validMoves_L2.Add(nextMove);
                }

                undoMove(undo_L2, bestMoveOppBS);
            }

            undoMove(undo, this.currentBoardState);
        }


        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves_L2.Count);

        NextMove move = validMoves_L2[rndIdx];

        lastFiveMoves.Enqueue(move);

        if (lastFiveMoves.Count > 6)
        {
            lastFiveMoves.Dequeue();
        }

        return move;
    }

    private List<float> ThinkingBot_getPointsOnBoardState(BoardState bs, bool isKingWorthMore, Piece movePiece, coords moveCoords)
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

                    if (HelperFunctions.checkState(piece, PieceState.Fragile))
                    {
                        pts += 1.5f;
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Shield))
                    {
                        pts -= piece.points;
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Frozen))
                    {
                        pts -= piece.points / 2;
                    }

                    if (x == moveCoords.x - 1 && y == moveCoords.y - 1)
                    {
                        if (HelperFunctions.checkState(piece, PieceState.Electric))
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

    private List<int> ThinkingBot_getBoardControlOnBoardState(BoardState bs)
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
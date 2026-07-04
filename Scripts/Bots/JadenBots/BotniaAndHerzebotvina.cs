using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;
using static HelperFunctions;
using static UndoMoveBotHelperFunctions;

public class BotniaAndHerzebotvina : BotTemplate
{
    public BotniaAndHerzebotvina(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Botnia and Herzebotvina";

        choosePieces();
    }

    Queue<NextMove> lastFiveMoves = new Queue<NextMove>();

    override
    public NextMove nextMove()
    {
        float bestL2Eval = Mathf.NegativeInfinity;
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

                    List<float> pointsOnBoard_ = Botnia_getPointsOnBoardState(this.currentBoardState, this.color);
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

                float eval = Botnia_Eval(bestMoveOppBS, this.color, lastFiveMoves, nextMove);

                if (eval >= bestL2Eval)
                {
                    if (eval > bestL2Eval)
                    {
                        validMoves_L2.Clear();
                    }

                    bestL2Eval = eval;
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

    private float Botnia_Eval(BoardState bs, int botColor, Queue<NextMove> lastFiveMoves, NextMove nextMove)
    {

        List<float> points = Botnia_getPointsOnBoardState(bs, botColor);
        var boardControlEVAL = Botnia_getBoardControlOnBoardState(bs);
        List<int> pawnStructure = Botnia_getPawnStructureDefense(bs);

        float botPoints = this.color == 1 ? points[0] : points[1];
        float oppPoints = this.color == -1 ? points[0] : points[1];
        int botBoardControl = this.color == 1 ? boardControlEVAL.boardControl[0] : boardControlEVAL.boardControl[1];
        int oppBoardControl = this.color == -1 ? boardControlEVAL.boardControl[0] : boardControlEVAL.boardControl[1];
        int botCenterControl = this.color == 1 ? boardControlEVAL.centerControl[0] : boardControlEVAL.centerControl[1];
        int oppCenterControl = this.color == -1 ? boardControlEVAL.centerControl[0] : boardControlEVAL.centerControl[1];
        int botKingAttacking = this.color == 1 ? boardControlEVAL.kingAttacking[0] : boardControlEVAL.kingAttacking[1];
        int oppKingAttacking = this.color == -1 ? boardControlEVAL.kingAttacking[0] : boardControlEVAL.kingAttacking[1];
        int botPawnStructure = this.color == 1 ? pawnStructure[0] : pawnStructure[1];
        int oppPawnStructure = this.color == -1 ? pawnStructure[0] : pawnStructure[1];

        int botKingDefending = this.color == -1 ? boardControlEVAL.kingAttacking[0] : boardControlEVAL.kingAttacking[1];
        int oppKingDefending = this.color == 1 ? boardControlEVAL.kingAttacking[0] : boardControlEVAL.kingAttacking[1];

        int botPawnPromotionPotential = Botnia_getPawnPromotionPotential(this.color, bs);
        int oppPawnPromotionPotential = Botnia_getPawnPromotionPotential(this.color * -1, bs);

        float pointsDiff = botPoints - oppPoints;
        float boardControlDiff = botBoardControl - oppBoardControl;
        float centerControlDiff = botCenterControl - oppCenterControl;

        float endPenalty = 0f;

        if (moveInQueue(lastFiveMoves, nextMove))
        {
            endPenalty += 2f;
        }

        if (moveInQueueTwice(lastFiveMoves, nextMove))
        {
            endPenalty += 10f;
        }

        return (float) ((pointsDiff * 3) + (boardControlDiff * 0.1) + (centerControlDiff * 0.2) + (botKingAttacking * 0.4) - (botKingDefending * 0.2) + (botPawnStructure * 0.2) + (botPawnPromotionPotential * 0.1) - (oppPawnPromotionPotential * 0.1) - endPenalty);
    }

    private int Botnia_getPawnPromotionPotential(int color, BoardState bs)
    {
        int score = 0;

        List<Piece> pieces = getPiecesOnBoardState(bs, color);

        foreach (Piece p in pieces)
        {
            if (p.baseType != "Pawn")
            {
                continue;
            }

            bool canPromote = false;
            //Pawn theoretically can promote
            foreach (coords move in p.moves)
            {
                int distance = Mathf.Abs((move.y + p.position.y) - p.promotingRow);

                bool thisMoveCanPromote = distance % move.y == 0;

                if (thisMoveCanPromote)
                {
                    canPromote = true;
                }
            }

            if (canPromote)
            {
                score += 8 - Mathf.Abs(p.position.y - p.promotingRow);
            }
        }

        return score;
    }

    private List<float> Botnia_getPointsOnBoardState(BoardState bs, int botColor)
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

                    if (piece.baseType == "King")
                    {
                        if (checkState(piece, PieceState.Jailed) || checkState(piece, PieceState.Frozen))
                        {
                            pts += 80;
                        }
                        else
                        {
                            pts += 100;
                        }
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Fragile))
                    {
                        pts += 1.5f;
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Shield))
                    {
                        pts -= piece.points;
                    }

                    if (HelperFunctions.checkAbility(piece, PieceAbilities.Spawn) && piece.spawnable != "ZombiePawn")
                    {
                        float basePts = piece.points / 2;
                        pts = basePts * piece.numSpawns;
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Frozen))
                    {
                        pts -= piece.points / 2;
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

        List<float> l = new List<float>();
        l.Add(wCount);
        l.Add(bCount);

        return l;
    }

    private (List<int> boardControl, List<int> centerControl, List<int> kingAttacking) Botnia_getBoardControlOnBoardState(BoardState bs)
    {
        coords oppKingPos = filterPieces("King", this.opponentPieces)[0].position;
        coords kingPos = this.king.position;

        coords whiteKingPos = this.color == 1 ? kingPos : oppKingPos;
        coords blackKingPos = this.color == -1 ? kingPos : oppKingPos;

        List<int> boardControl = new List<int>();
        List<int> centerControl = new List<int>();
        List<int> kingAttacking = new List<int>();

        var botMovesWhite = getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = getAllPossibleBotMoves(this, bs, -1);
        List<PieceMoveList> listBotBlackMoves = botMovesBlack.pieceMoveList;

        int score = 0;
        float kingAttackingScore = 0;
        int centerScore = 0;
        foreach (PieceMoveList pml in listBotWhiteMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                score += 1;

                if (coords.x == 4 || coords.x == 5)
                {
                    if (coords.y == 4 || coords.y == 5)
                    {
                        centerScore += 1;
                    }
                }

                if (Mathf.Abs(coords.x - blackKingPos.x) < 3 && Mathf.Abs(coords.y - blackKingPos.y) < 3)
                {
                    kingAttackingScore += 3 - Mathf.Abs(coords.x - blackKingPos.x);
                    kingAttackingScore += 3 - Mathf.Abs(coords.y - blackKingPos.y);
                }

                if (coords.x == blackKingPos.x && coords.y == blackKingPos.y)
                {
                    kingAttackingScore += 16;
                }
            }
        }
        boardControl.Add(score);
        centerControl.Add(centerScore);
        kingAttacking.Add((int) kingAttackingScore);

        score = 0;
        centerScore = 0;
        kingAttackingScore = 0;
        foreach (PieceMoveList pml in listBotBlackMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                score += 1;

                if (coords.x == 4 || coords.x == 5)
                {
                    if (coords.y == 4 || coords.y == 5)
                    {
                        centerScore += 1;
                    }
                }

                if (Mathf.Abs(coords.x - whiteKingPos.x) < 3 && Mathf.Abs(coords.y - whiteKingPos.y) < 3)
                {
                    kingAttackingScore += 3 - Mathf.Abs(coords.x - whiteKingPos.x);
                    kingAttackingScore += 3 - Mathf.Abs(coords.y - whiteKingPos.y);
                }

                if (coords.x == whiteKingPos.x && coords.y == whiteKingPos.y)
                {
                    score += 16;
                }
            }
        }
        boardControl.Add(score);
        centerControl.Add(centerScore);
        kingAttacking.Add((int) kingAttackingScore);

        return (boardControl, centerControl, kingAttacking);
    }

    private List<int> Botnia_getPawnStructureDefense(BoardState bs)
    {
        var botMovesWhite = getAllPossibleBotMoves(this, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = getAllPossibleBotMoves(this, bs, -1);
        List<PieceMoveList> listBotBlackMoves = botMovesBlack.pieceMoveList;

        List<int> pawnStructure = new List<int>();

        int score = 0;
        foreach (PieceMoveList pml in listBotWhiteMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            if (piece.baseType != "Pawn")
            {
                continue;
            }

            foreach (coords coords in _mL)
            {
                List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(coords.x - 1, coords.y - 1, bs.boardGrid, false);

                foreach (Piece pieceOnCoords in piecesOnCoords)
                {
                    if (pieceOnCoords.color == 1)
                    {
                        score += 1;
                    }
                }
            }
        }
        pawnStructure.Add(score);

        score = 0;
        foreach (PieceMoveList pml in listBotBlackMoves)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            if (piece.baseType != "Pawn")
            {
                continue;
            }

            foreach (coords coords in _mL)
            {
                List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(coords.x - 1, coords.y - 1, bs.boardGrid, false);

                foreach (Piece pieceOnCoords in piecesOnCoords)
                {
                    if (pieceOnCoords.color == -1)
                    {
                        score += 1;
                    }
                }
            }
        }

        pawnStructure.Add(score);

        return pawnStructure;
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
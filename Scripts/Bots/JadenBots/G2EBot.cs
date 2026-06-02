using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;

public class G2EBot : BotTemplate
{
    public G2EBot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "G2EBot";

        choosePieces();
    }

    Queue<NextMove> lastFiveMoves = new Queue<NextMove>();

    override
    public NextMove nextMove()
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

                List<int> boardControlOnBS = getBoardControlOnBoardState(cloneState_);
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

    private List<int> getBoardControlOnBoardState(BoardState bs)
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

            foreach(NextMove nm in possibleNextMoves)
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

        foreach(NextMove nm in allMoves)
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

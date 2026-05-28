using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;

public class Bot618 : BotTemplate
{
    public Bot618(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Bot 618";

        choosePieces();
    }

    Queue<NextMove> lastFiveMoves = new Queue<NextMove>();

    override
    public NextMove nextMove()
    {
        float bestL2MoveDiff = -99999999f;
        int bestL2MoveBoardControl = -1;
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

                    List<float> pointsOnBoard_ = Bot618_getPointsOnBoardState(this.currentBoardState, true, pieceResponse, coordsResponse);
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

                List<float> pointsOnBoard = Bot618_getPointsOnBoardState(bestMoveOppBS, true, piece_L2, coords_L2);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff = botPoints - oppPoints;

                if (diff >= bestL2MoveDiff)
                {
                    if (diff > bestL2MoveDiff)
                    {
                        validMoves_L2.Clear();
                    }
                    else
                    {
                        List<int> bc = Bot618_getBoardControlOnBoardState(bestMoveOppBS);
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
        int rndIdx = rand.Next(validMoves_L2.Count);

        NextMove move = validMoves_L2[rndIdx];

        return move;
    }

    private List<float> Bot618_getPointsOnBoardState(BoardState bs, bool isKingWorthMore, Piece movePiece, coords moveCoords)
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

    private List<int> Bot618_getBoardControlOnBoardState(BoardState bs)
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
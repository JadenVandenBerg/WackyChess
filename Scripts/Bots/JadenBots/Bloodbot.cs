using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static BotHelperFunctions;

public class Bloodbot : BotTemplate
{
    public Bloodbot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Bloodbot";

        choosePieces();
    }

    override
    public NextMove nextMove()
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

                List<int> boardControlOnBS = getBoardControlOnBoardState(cloneState_);
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

                    if (coords.x == blackKingPos.x && coords.x == blackKingPos.y)
                    {
                        score += 16;
                    }
                }
            }
        }
        boardControl.Add((int) score);

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
}

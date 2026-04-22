using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;

public class BotRoss : BotTemplate
{
    private struct Design
    {
        public List<int[]> coords;
        public string type;

        public Design(List<int[]> coords_, string type_)
        {
            coords = coords_;
            type = type_;
        }
    }

    Design tree;
    Design cabin;
    Design mountain;
    Design selectedDesign;

    public BotRoss(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Bot Ross";

        choosePieces();

        tree = new Design(
            new List<int[]>
            {
                new int[] { 4, 1 }
                , new int[] { 5, 1 }
                , new int[] { 4, 2 }
                , new int[] { 5, 2 }
                , new int[] { 2, 3 }
                , new int[] { 3, 3 }
                , new int[] { 4, 3 }
                , new int[] { 5, 3 }
                , new int[] { 6, 3 }
                , new int[] { 7, 3 }
                , new int[] { 3, 4 }
                , new int[] { 4, 4 }
                , new int[] { 5, 4 }
                , new int[] { 6, 4 }
                , new int[] { 4, 5 }
                , new int[] { 5, 5 }
            }
        , "tree");

        cabin = new Design(
            new List<int[]>
            {
                new int[] { 2, 1 }
                , new int[] { 4, 1 }
                , new int[] { 5, 1 }
                , new int[] { 7, 1 }
                , new int[] { 2, 2 }
                , new int[] { 4, 2 }
                , new int[] { 5, 2 }
                , new int[] { 7, 2 }
                , new int[] { 1, 3 }
                , new int[] { 2, 3 }
                , new int[] { 7, 3 }
                , new int[] { 8, 3 }
                , new int[] { 3, 4 }
                , new int[] { 6, 4 }
                , new int[] { 4, 6 }
                , new int[] { 5, 6 }
            }
        , "cabin");

        mountain = new Design(
            new List<int[]>
            {
                new int[] { 1, 1 }
                , new int[] { 2, 1 }
                , new int[] { 3, 1 }
                , new int[] { 4, 1 }
                , new int[] { 6, 1 }
                , new int[] { 7, 1 }
                , new int[] { 8, 1 }
                , new int[] { 1, 2 }
                , new int[] { 8, 2 }
                , new int[] { 1, 3 }
                , new int[] { 3, 3 }
                , new int[] { 7, 3 }
                , new int[] { 2, 4 }
                , new int[] { 4, 4 }
                , new int[] { 6, 4 }
                , new int[] { 5, 5 }
            }
        , "mountain");

        int randomDesign = globalDefs.globalRand.Next(1, 4);

        if (randomDesign == 1)
        {
            selectedDesign = BotRoss_FixDesignForColor(tree);
        }
        else if (randomDesign == 2)
        {
            selectedDesign = BotRoss_FixDesignForColor(cabin);
        }
        else
        {
            selectedDesign = BotRoss_FixDesignForColor(mountain);
        }
    }

    private Design BotRoss_FixDesignForColor(Design design)
    {
        for (int i = 0; i < design.coords.Count; i++)
        {
            if (this.color == -1)
            {
                design.coords[i][1] = 9 - design.coords[i][1];
            }
        }

        return design;
    } 

    override
    public NextMove nextMove()
    {
        float bestMoveDiff = 100000;
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        foreach (NextMove nextMove in allMoves)
        {
            Piece piece;
            int[] coords;
            string moveType = nextMove.moveType;
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

            BoardState originalBoardState = this.currentBoardState;

            //Simulate the piece move
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

            float eval = evalMove(selectedDesign, nextMove, cloneState);

            if (eval <= bestMoveDiff)
            {
                if (eval < bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = eval;

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

    private float evalMove(Design design, NextMove move, BoardState nextMoveBS)
    {
        //TODO return the total distance of all pieces to all design spots, with pieces on a spot not taking any distance penalties
        Piece piece;
        int[] coords;

        if (move.moveType == "move")
        {
            piece = move.move.p;
            coords = move.move.coords;
        }
        else
        {
            piece = move.ability.piece;
            coords = move.ability.coords;
        }

        if (piece.color != this.color)
        {
            return 1000000;
        }

        float totalDistCount = 0f;

        List<Piece> piecesOnBS = getPiecesOnBoardState(nextMoveBS, color);
        foreach(Piece p in piecesOnBS)
        {
            float distCount = 0f;
            foreach(int[] coords_ in design.coords)
            {
                distCount += Mathf.Sqrt(Mathf.Abs(coords[0] - coords_[0]) + Mathf.Abs(coords[1] - coords_[1]));

                if (coords[0] - coords_[0] == 0 && coords[1] - coords_[1] == 0)
                {
                    distCount = 0;
                    break;
                }
            }

            totalDistCount += distCount;
        }

        return totalDistCount;
    }
}
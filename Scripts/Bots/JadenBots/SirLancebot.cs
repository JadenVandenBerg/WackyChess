using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using static HelperFunctions;
using System;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;
using System.Collections;
using System.Text;



public class SirLancebot : BotTemplate
{
    //1 is white, -1 is black
    public SirLancebot(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Sir Lancebot";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        List<PieceAttack> pieceAttacks = generatePieceAttacks(this.currentBoardState, this.color);
        Square[,] squares = analyzeSquares(pieceAttacks, this.currentBoardState, this.color);

        var squareInfo = getSquareInfo(squares);

        List<Square> strongSquares = squareInfo.strongSquares;
        List<Square> weakSquares = squareInfo.weakSquares;

        foreach (Square square in strongSquares)
        {
            highlightSquare(findSquare(square.coords.x, square.coords.y), Color.lightCyan);
        }

        foreach (Square square in weakSquares)
        {
            highlightSquare(findSquare(square.coords.x, square.coords.y), Color.orange);
        }

        return BotHelperFunctions.getRandomBotMove(this);
    }

    public struct Square
    {
        public coords coords;
        public List<Piece> occupants;

        public List<Attack> attacking;

        public List<Attack> defending;

        public Square(coords c, List<Piece> o, List<Attack> a, List<Attack> d)
        {
            coords = c;
            occupants = o;
            attacking = a;
            defending = d;
        }

        public bool IsEmpty()
        {
            return occupants == null || occupants.Count == 0;
        }

        public Piece GetTopPiece()
        {
            if (IsEmpty())
            {
                return null;
            }

            return occupants[occupants.Count - 1];
        }

        public float OccupantPoints()
        {
            Piece p = GetTopPiece();

            if (p == null)
            {
                return 0;
            }

            return p.points;
        }
    }

    public struct Attack
    {
        public Piece piece;
        public coords coords;
        public List<Piece> blocking;
        public bool blockingIncludesOpp;

        public Attack(Piece p, coords c, List<Piece> b, bool bio)
        {
            piece = p;
            coords = c;
            blocking = b;
            blockingIncludesOpp = bio;
        }

        public Attack(Piece p, coords c)
        {
            piece = p;
            coords = c;
            blocking = null;
            blockingIncludesOpp = false;
        }
    }

    public struct PieceAttack
    {
        public Piece piece;
        public List<Attack> attacks;

        public PieceAttack(Piece p, List<Attack> a)
        {
            piece = p;
            attacks = a;
        }
    }

    public List<PieceAttack> generatePieceAttacks(BoardState bs, int color)
    {
        List<PieceAttack> pieceAttacks = new List<PieceAttack>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                List<Piece> pieces = bs.boardGrid[x, y];

                if (pieces == null)
                    continue;

                foreach (Piece piece in pieces)
                {
                    if (piece.color != color)
                    {
                        continue;
                    }

                    List<Attack> attacks = new List<Attack>();

                    addAttacks(attacks, bot_iterateThroughPieceMoves(bs, piece, HelperFunctions.moveAndAttacksComparator, piece.moveAndAttacks));
                    addAttacks(attacks, bot_iterateThroughPieceMoves(bs, piece, HelperFunctions.attacksComparator, piece.attacks));
                    addAttacks(attacks, bot_iterateThroughPieceMoves(bs, piece, HelperFunctions.oneTimeMoveAndAttacksComparator, piece.oneTimeMoveAndAttacks));
                    addAttacks(attacks, bot_iterateThroughPieceMoves(bs, piece, HelperFunctions.murderousAttacksComparator, piece.murderousAttacks));
                    addAttacks(attacks, bot_iterateThroughPieceMoves(bs, piece, HelperFunctions.conditionalAttacksComparator, piece.conditionalAttacks));
                    addAttacks(attacks, bot_iterateThroughPieceMoves(bs, piece, HelperFunctions.jumpAttacksComparator, piece.jumpAttacks));

                    if (attacks.Count > 0)
                    {
                        pieceAttacks.Add(new PieceAttack(piece, attacks));
                    }
                }
            }
        }

        return pieceAttacks;
    }

    private void addAttacks(List<Attack> attacks, PieceAttack pa)
    {
        if (pa.attacks != null && pa.attacks.Count > 0)
        {
            attacks.AddRange(pa.attacks);
        }
    }

    public PieceAttack bot_iterateThroughPieceMoves(BoardState bs, Piece piece, Func<Piece, bool, bool, bool, List<Piece>, bool> comparator, coords[] moveType)
    {
        if (checkState(piece, PieceState.Frozen) || checkState(piece, PieceState.Jailed))
        {
            return new PieceAttack(piece, new List<Attack>());
        }

        if (piece.baseType == "Queen")
        {
            if (isolatedIsOppressorOnBoard(bs, piece.color))
            {
                return new PieceAttack(piece, new List<Attack>());
            }
        }

        List<Attack> attacks = new List<Attack>();

        bool isPortal = HelperFunctions.checkState(piece, PieceState.Portal);
        bool isBouncing = HelperFunctions.checkState(piece, PieceState.Bouncing);

        int lastDirX = 0;
        int lastDirY = 0;
        bool previousWasJump = false;

        for (int i = 0; i < moveType.GetLength(0); i++)
        {
            List<Piece> jumpedPieces = new List<Piece>();

            //Portal
            //int[] oldCoords = new int[] { moveType[i, 0] + piece.position.x, moveType[i, 1] + piece.position.y };
            int oldCoordsX, oldCoordsY;

            oldCoordsX = moveType[i].x + piece.position.x;
            oldCoordsY = moveType[i].y + piece.position.y;

            //Optimization
            int dx = moveType[i].x;
            int dy = moveType[i].y;
            int dirX = Math.Sign(dx);
            int dirY = Math.Sign(dy);
            if (dirX != lastDirX || dirY != lastDirY)
            {
                previousWasJump = false;
                lastDirX = dirX;
                lastDirY = dirY;
            }

            //Debug.Log("Moving piece " + piece.name + " from " + piece.position.x + "," + piece.position.y + " to " + oldCoordsX + "," + oldCoordsY);

            //int[] newPos = new int[] { oldCoords.x, oldCoords.y };
            int newPosX = oldCoordsX;
            int newPosY = oldCoordsY;

            if (isPortal)
            {
                coords coordsP = adjustCoordsForPortal(piece, oldCoordsX, oldCoordsY);
                //newPos.x = coordsP.x;
                //newPos.y = coordsP.y;
                newPosX = coordsP.x;
                newPosY = coordsP.y;
                //newPos = HelperFunctions.adjustCoordsForPortal(piece, oldCoords.x, oldCoords.y);
            }
            else if (isBouncing)
            {
                coords coordsB = adjustCoordsForBouncing(piece, oldCoordsX, oldCoordsY);
                //newPos.x = coordsB.x;
                //newPos.y = coordsB.y;
                newPosX = coordsB.x;
                newPosY = coordsB.y;
                //newPos = HelperFunctions.adjustCoordsForBouncing(piece, oldCoords.x, oldCoords.y);
            }

            if (newPosX > 8 || newPosY > 8 || newPosX <= 0 || newPosY <= 0)
            {
                continue;
            }

            List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(newPosX - 1, newPosY - 1, bs.boardGrid, false);

            var diagnostics = isolatedGetPiecesOnCoordsDiagnostics(piece, piecesOnCoords, bs);
            //bool pieceIsNull = piecesOnCoords == null || piecesOnCoords.Count == 0;
            bool pieceIsNull = diagnostics.pieceIsNull;

            bool pieceIsDiffColour = false;

            if (!pieceIsNull)
            {
                pieceIsDiffColour = !diagnostics.colorOnCoords;

                if (diagnostics.squareJailed)
                {
                    pieceIsDiffColour = true;
                }

                if (diagnostics.piecesDisabled)
                {
                    pieceIsNull = true;
                }

                if (diagnostics.crowdingElegible)
                {
                    pieceIsNull = true;
                }

                if (diagnostics.shieldOnSquare || diagnostics.captureTheFlagOnSquare)
                {
                    continue;
                }

            }

            bool jump;
            if (isPortal && !((oldCoordsX == newPosX) && (oldCoordsY == newPosY)))
            {
                if (isKnightPortalBackRank_(piece, oldCoordsX, oldCoordsY, newPosX, newPosY))
                {
                    continue;
                }

                jump = isolatedIsJumpPortal(piece, piece.position, newPosX, newPosY, bs);
                previousWasJump = false;
                //jump = HelperFunctions.isJumpPortal(piece, piece.position, newPos);
            }
            else if (isBouncing && !((oldCoordsX == newPosX) && (oldCoordsY == newPosY)))
            {
                jump = isolatedIsJumpBouncing(piece, piece.position, newPosX, newPosY, bs);
                previousWasJump = false;
                //jump = HelperFunctions.isJumpBouncing(piece, piece.position, newPos);
            }
            else
            {
                jump = bot_isolatedIsJump(piece, piece.position, newPosX, newPosY, bs, jumpedPieces);
                if (!isBouncing && !isPortal)
                {
                    previousWasJump = jump;
                }
                else
                {
                    previousWasJump = false;
                }
                //jump = HelperFunctions.isJump(piece, piece.position, newPos);
            }

            if (comparator(piece, jump, pieceIsNull, pieceIsDiffColour, piecesOnCoords))
            {
                if (!pieceIsNull)
                {
                    coords c = new coords(newPosX, newPosY);

                    if (jump)
                    {
                        Attack attack = new Attack(piece, c, jumpedPieces, isolatedIsColorOnCoords(jumpedPieces, true, piece.color * -1));
                        attacks.Add(attack);
                    }
                    else
                    {
                        Attack attack = new Attack(piece, c, null, false);
                        attacks.Add(attack);
                    }
                }
                //TODO maybe add check functionality
                //if (piece.name == "w_k1" || piece.name == "b_k1") Debug.Log("MOVE SIM " + newPosX + "," + newPosY + " J: " + jump + " PIN: " + pieceIsNull + " PID: " + pieceIsDiffColour + " POC: " + piecesOnCoords.Count);

            }
        }

        return new PieceAttack(piece, attacks);
    }

    public static bool bot_isolatedIsJump(Piece piece, coords from, int toX, int toY, BoardState bs, List<Piece> jumpedPieces)
    {
        int dirX, dirY;

        if (from.x > toX)
        {
            dirX = -1;
        }
        else if (from.x == toX)
        {
            dirX = 0;
        }
        else
        {
            dirX = 1;
        }

        if (from.y > toY)
        {
            dirY = -1;
        }
        else if (from.y == toY)
        {
            dirY = 0;
        }
        else
        {
            dirY = 1;
        }

        int diff = Mathf.Abs(from.x - toX);
        if (Mathf.Abs(from.y - toY) > diff)
        {
            diff = Mathf.Abs(from.y - toY);
        }

        bool isGhost = HelperFunctions.checkState(piece, PieceState.Ghost);
        int enemyColor = piece.color * -1;

        for (int i = 1; i <= diff - 1; i++)
        {
            int x = from.x + (i * dirX);
            int y = from.y + (i * dirY);

            List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(x - 1, y - 1, bs.boardGrid, false);

            foreach (Piece p in piecesOnCoords)
            {
                if (HelperFunctions.checkState(p, PieceState.Ghoul) && p.color == piece.color)
                {
                    // Your Ghoul
                    continue;
                }

                if (HelperFunctions.checkState(p, PieceState.Dematerialized))
                {
                    // Your Dematerialized
                    continue;
                }

                if (isGhost && p.color == piece.color)
                {
                    // Your piece is a ghost, your piece
                    continue;
                }

                //Debug.Log("MOVE FROM " + piece.position.x + "," + piece.position.y + " to " + x + "," + y + " is a JUMP");
                jumpedPieces.Add(p);
            }
        }

        if (jumpedPieces.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Second loop through board: Figure out the order of attack on each square, ordered by lowest points to highest, pins moved in accordance to the pinned pieces points

    public Square[,] analyzeSquares(List<PieceAttack> pieceAttacks, BoardState bs, int color)
    {
        Square[,] squares = new Square[8, 8];

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                List<Piece> occupants = new List<Piece>();

                if (bs.boardGrid[x, y] != null)
                {
                    occupants.AddRange(bs.boardGrid[x, y]);
                }

                squares[x, y] = new Square(
                    new coords(x + 1, y + 1),
                    occupants,
                    new List<Attack>(),
                    new List<Attack>()
                );
            }
        }


        foreach (PieceAttack pa in pieceAttacks)
        {
            foreach (Attack attack in pa.attacks)
            {
                Square square = squares[
                    attack.coords.x - 1,
                    attack.coords.y - 1
                ];

                if (pa.piece.color == color)
                {
                    square.attacking.Add(attack);
                }
                else
                {
                    square.defending.Add(attack);
                }

                squares[attack.coords.x - 1, attack.coords.y - 1] = square;
            }
        }


        return squares;
    }

    private bool pieceAttacksSquare(List<PieceAttack> pieceAttacks, Piece piece, coords square)
    {
        foreach (PieceAttack pa in pieceAttacks)
        {
            if (pa.piece != piece)
                continue;

            foreach (Attack attack in pa.attacks)
            {
                if (attack.coords.x == square.x && attack.coords.y == square.y)
                {
                    return true;
                }
            }

            return false;
        }

        return false;
    }

    // Third loop through board: Identify strong and weak squares and adjust attacking and defending based on expected points value

    // Loop through all squares
    // Count attacking, count defending, only include reasonable captures

    private (List<Square> strongSquares, List<Square> weakSquares) getSquareInfo(Square[,] squares)
    {
        List<Square> strongSquares = new List<Square>();
        List<Square> weakSquares = new List<Square>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Square square = squares[x, y];

                if (square.IsEmpty())
                {
                    continue;
                }

                float result = staticExchangeEvaluation(square);

                if (result > 0)
                {
                    strongSquares.Add(square);
                }
                else if (result < 0)
                {
                    weakSquares.Add(square);
                }
            }
        }

        return (strongSquares, weakSquares);
    }

    private float staticExchangeEvaluation(Square square)
    {
        Piece target = square.GetTopPiece();

        if (target == null)
        {
            return 0;
        }

        List<Attack> attackers = new List<Attack>(square.attacking);
        List<Attack> defenders = new List<Attack>(square.defending);
        List<float> gains = new List<float>();
        HashSet<Piece> removedPieces = new HashSet<Piece>();

        if (target.color != color)
        {
            gains.Add(target.points);
        }
        else
        {
            gains.Add(0);
        }

        bool attackersTurn = true;

        while (true)
        {
            Attack? attack;

            if (attackersTurn)
            {
                attack = getCheapestAvailableAttack(attackers, removedPieces);
            }
            else
            {
                attack = getCheapestAvailableAttack(defenders, removedPieces);
            }

            if (attack == null)
            {
                break;
            }

            removedPieces.Add(attack.Value.piece);
            float previousGain = gains[gains.Count - 1];
            float newGain = attack.Value.piece.points - previousGain;
            gains.Add(newGain);

            attackersTurn = !attackersTurn;
        }

        for (int i = gains.Count - 2; i >= 0; i--)
        {
            gains[i] = Math.Max(gains[i], -gains[i + 1]);
        }

        return gains[0];
    }

    private Attack? getCheapestAvailableAttack(List<Attack> attacks, HashSet<Piece> removedPieces)
    {
        Attack? cheapest = null;


        float lowestValue = float.MaxValue;


        foreach (Attack attack in attacks)
        {
            if (removedPieces.Contains(attack.piece))
            {
                continue;
            }

            if (!isAttackAvailable(attack, removedPieces))
            {
                continue;
            }


            if (attack.piece.points < lowestValue)
            {
                lowestValue = attack.piece.points;

                cheapest = attack;
            }
        }

        return cheapest;
    }

    private bool isAttackAvailable(Attack attack, HashSet<Piece> removedPieces)
    {
        if (attack.blocking == null)
        {
            return true;
        }

        foreach (Piece blocker in attack.blocking)
        {
            if (!removedPieces.Contains(blocker))
            {
                return false;
            }
        }

        return true;
    }
}
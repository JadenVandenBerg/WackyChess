using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;
using System.Collections;
using System.Linq;
using System.Reflection;
using static HelperFunctions;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;

public class JadenBotHelperFunctions : MonoBehaviour
{
    public struct PieceCoords
    {
        public Piece p;
        public coords c;

        public PieceCoords(Piece p_, coords c_)
        {
            p = p_;
            c = c_;
        }
    }

    public static List<PieceCoords> Jay_getHangingPieces(BoardState bs, int color)
    {
        List<PieceCoords> hangingPieces = new List<PieceCoords>();

        List<Piece> piecesOnBoardAttacking = getPiecesOnBoardState(bs, color * -1);
        List<Piece> piecesOnBoard = getPiecesOnBoardState(bs, color);

        List<Piece> piecesUnderAttack = new List<Piece>();
        List<Piece> defendedPieces = new List<Piece>();
        foreach(Piece p in piecesOnBoardAttacking)
        {
            List<NextMove> moves = getAllPossibleBotPieceAttacks(bs, p, false);

            foreach(NextMove move in moves)
            {
                if (move.moveType == "ability")
                {
                    continue;
                }
                
                coords attackedCoords = move.move.coords;

                List<Piece> piecesOnAttackedCoords = isolatedGetPiecesOnCoordsBoardGrid(attackedCoords.x - 1, attackedCoords.y - 1, bs.boardGrid, false);

                foreach (Piece p_ in piecesOnAttackedCoords)
                {
                    if (p_.color == color)
                    {
                        piecesUnderAttack.Add(p_);
                    }
                }
            }
        }

        foreach (Piece p in piecesOnBoard)
        {
            List<NextMove> moves = getAllPossibleBotPieceAttacks(bs, p, true);

            foreach (NextMove move in moves)
            {
                if (move.moveType == "ability")
                {
                    continue;
                }

                coords defendedCoords = move.move.coords;

                List<Piece> piecesOnADefendedCoords = isolatedGetPiecesOnCoordsBoardGrid(defendedCoords.x - 1, defendedCoords.y - 1, bs.boardGrid, false);

                foreach (Piece p_ in piecesOnADefendedCoords)
                {
                    if (p_.color == color)
                    {
                        defendedPieces.Add(p_);
                    }
                }
            }
        }

        foreach(Piece p in piecesUnderAttack)
        {
            bool hanging = true;
            foreach(Piece p_ in defendedPieces)
            {
                if (p_.name == p.name)
                {
                    hanging = false;
                }
            }

            if (hanging)
            {
                hangingPieces.Add(new PieceCoords(p, new coords(p.position.x - 1, p.position.y - 1)));
            }
        }

        return hangingPieces;
    }

    public static List<float> Jay_getPointsOnBoardState(BoardState bs, bool isKingWorthMore, Piece movePiece, coords moveCoords, int color)
    {
        if (bs == null)
        {
            if (color == 1)
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

                        if (movePiece == piece)
                        {
                            pts -= piece.points / 3;
                        }
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Shield))
                    {
                        pts -= piece.points;
                    }

                    if (HelperFunctions.checkState(piece, PieceState.Frozen))
                    {
                        pts -= piece.points / 2;
                    }

                    if (HelperFunctions.checkAbility(piece, PieceAbilities.Spawn) && piece.spawnable != "ZombiePawn")
                    {
                        float basePts = piece.points / 2;
                        pts = basePts * piece.numSpawns;
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

    public static List<float> Jay_getPointsOnBoardState_simple(BoardState bs, bool isKingWorthMore, int color, bool calcStacking)
    {
        if (bs == null)
        {
            if (color == 1)
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

                    if (piece.baseType == "King")
                    {
                        if (checkState(piece, PieceState.Jailed))
                        {
                            pts += 80;
                        }
                        else if (checkState(piece, PieceState.Frozen))
                        {
                            pts += 95;
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

                    if (HelperFunctions.checkState(piece, PieceState.Frozen))
                    {
                        pts -= piece.points / 2;
                    }

                    if (HelperFunctions.checkAbility(piece, PieceAbilities.Spawn) && piece.spawnable != "ZombiePawn")
                    {
                        float basePts = piece.points / 2;
                        pts = basePts * piece.numSpawns;
                    }

                    if (checkState(piece, PieceState.Stacking) && calcStacking)
                    {
                        pts = Jay_calcStackingPoints(piece);
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

    public static float Jay_calcStackingPoints(Piece p)
    {
        float points = 1f;

        foreach(coords c in p.moves)
        {
            points += 0.1f;
        }
        foreach (coords c in p.oneTimeMoves)
        {
            points += 0.05f;
        }
        foreach (coords c in p.oneTimeMoveAndAttacks)
        {
            points += 0.1f;
        }
        foreach (coords c in p.attacks)
        {
            points += 0.3f;
        }
        foreach (coords c in p.jumpAttacks)
        {
            points += 0.6f;
        }
        foreach (coords c in p.moveAndAttacks)
        {
            points += 0.6f;
        }
        foreach (coords c in p.murderousAttacks)
        {
            points += 0.3f;
        }

        if (p.baseType == "King")
        {
            points = points * 5;
        }

        return points;
    }
 
    public static int Jay_getPawnPromotionPotential(int color, BoardState bs)
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

    public static (List<int> boardControl, List<int> centerControl, List<int> kingAttacking) Jay_getBoardControlOnBoardState(BoardState bs, BotTemplate bot)
    {
        coords oppKingPos = filterPieces("King", bot.opponentPieces)[0].position;
        coords kingPos = bot.king.position;

        coords whiteKingPos = bot.color == 1 ? kingPos : oppKingPos;
        coords blackKingPos = bot.color == -1 ? kingPos : oppKingPos;

        List<int> boardControl = new List<int>();
        List<int> centerControl = new List<int>();
        List<int> kingAttacking = new List<int>();

        var botMovesWhite = getAllPossibleBotMoves(bot, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = getAllPossibleBotMoves(bot, bs, -1);
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
        kingAttacking.Add((int)kingAttackingScore);

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
        kingAttacking.Add((int)kingAttackingScore);

        return (boardControl, centerControl, kingAttacking);
    }

    public static List<int> Jay_getBoardControlOnBoardState_onlyBoardControl(BoardState bs, BotTemplate bot)
    {
        Piece oppKing = isolatedGetKing(bs, bot.color * -1);

        if (oppKing == null)
        {
            return new List<int> { 0, 0 };
        }

        coords oppKingPos = oppKing.position;
        coords kingPos = bot.king.position;

        coords whiteKingPos = bot.color == 1 ? kingPos : oppKingPos;
        coords blackKingPos = bot.color == -1 ? kingPos : oppKingPos;

        List<int> boardControl = new List<int>();

        var botMovesWhite = getAllPossibleBotMoves(bot, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = getAllPossibleBotMoves(bot, bs, -1);
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

    public static List<int> Jay_getPawnStructureDefense(BoardState bs, BotTemplate bot)
    {
        var botMovesWhite = getAllPossibleBotMoves(bot, bs, 1);
        List<PieceMoveList> listBotWhiteMoves = botMovesWhite.pieceMoveList;

        var botMovesBlack = getAllPossibleBotMoves(bot, bs, -1);
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

    public static bool Jay_moveInQueue(Queue<NextMove> moves, NextMove move)
    {
        if (move == null)
        {
            return false;
        }

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

    public static bool Jay_moveInQueueTwice(Queue<NextMove> moves, NextMove move)
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

    public static int Jay_getDevelopment(BoardState bs, int color)
    {
        int score = 8;

        List<Piece> pieces = getPiecesOnBoardState(bs, color);

        foreach (Piece p in pieces)
        {
            if (p.baseType == "Pawn" || p.baseType == "King")
            {
                continue;
            }

            if (p.position.x == p.startSquare.x && p.position.y == p.startSquare.y)
            {
                score--;
            }
        }

        return score;
    }

    public static List<NextMove> Jay_getAllLegalMovesAndAbilities(BotTemplate bot, BoardState bs, int color)
    {
        bool isCheck = Jay_isCheck(bot, bs, color);

        List<NextMove> allMoves = Jay_getAllPossibleBotMovesAndAbilitiesWithCheck(bot, bs, color, isCheck);
        List<NextMove> acceptedMoves = new List<NextMove>();

        foreach (NextMove nm in allMoves)
        {
            var moveVars = getNextMoveVars(nm);
            Piece p = moveVars.piece;
            coords c = moveVars.coords;
            string moveType = moveVars.moveType;
            //Debug.LogError("Egbot: Testing Move " + p + " to " + c.x + c.y + "");

            if (isCheck)
            {
                if (checkState(p, PieceState.Depressed))
                {
                    continue;
                }
            }

            UndoMove undo;
            if (moveType == "move")
            {
                undo = undo_simulatePieceMove(bs, p, c);
            }
            else
            {
                undo = undo_simulatePieceAbility(bs, nm.ability);
            }

            if (!Jay_isCheck(bot, bs, color))
            {
                acceptedMoves.Add(nm);
            }

            undoMove(undo, bs);
        }

        return acceptedMoves;
    }

    public static List<NextMove> Jay_getAllPossibleBotMovesAndAbilitiesWithCheck(BotTemplate bot, BoardState bs, int color, bool isCheck)
    {
        //resetPiecePositions(null, bs.boardGrid);
        //bs = copyBoardState(bs);

        var botMoves = Jay_getAllPossibleBotMovesWithCheck(bot, bs, color, isCheck);

        List<PieceMoveList> allMovesBot = botMoves.pieceMoveList;
        List<PieceAbility> allAbilitiesBot = botMoves.piecesAbilities;

        List<NextMove> allMoves = new List<NextMove>();

        foreach (PieceMoveList pml in allMovesBot)
        {
            Piece piece = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                Move mv = new Move(piece, coords);
                NextMove pieceMove = new NextMove(mv);

                allMoves.Add(pieceMove);
            }
        }

        foreach (PieceAbility pa in allAbilitiesBot)
        {
            NextMove pieceAbility = new NextMove(pa);
            allMoves.Add(pieceAbility);
        }

        return allMoves;
    }

    public static (List<PieceMoveList> pieceMoveList, List<PieceAbility> piecesAbilities) Jay_getAllPossibleBotMovesWithCheck(BotTemplate bot, BoardState bs, int color, bool isCheck)
    {
        List<PieceMoveList> totalMoves = new List<PieceMoveList>();

        List<Piece> pieces = getPiecesOnBoardState(bs, color);

        for (int i = 0; i < pieces.Count; i++)
        {
            Piece piece = pieces[i];

            List<coords> moves = Jay_getIsolatedStatePieceMovesWithCheck(piece, bs, false, isCheck);

            if (moves != null && moves.Count > 0)
            {
                totalMoves.Add(new PieceMoveList(piece, moves));
            }
        }

        List<PieceAbility> pieceAbilities = getAllPossibleBotAbilities(bot, bs, color);

        return (totalMoves, pieceAbilities);
    }

    public static List<coords> Jay_getIsolatedStatePieceMovesWithCheck(Piece piece, BoardState bs, bool theoretical, bool check)
    {
        List<coords> allMoves = new List<coords>();

        List<Piece>[,] boardGrid = bs.boardGrid;

        //todo maybe forcestayturn

        isolatedIterateThroughPieceMoves(HelperFunctions.moveComparator, piece, bs, piece.moves, check, allMoves, theoretical, false);
        isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.moveAndAttacks, check, allMoves, theoretical, false);
        isolatedIterateThroughPieceMoves(HelperFunctions.attacksComparator, piece, bs, piece.attacks, check, allMoves, theoretical, false);
        isolatedIterateThroughPieceMoves(HelperFunctions.oneTimeMovesComparator, piece, bs, piece.oneTimeMoves, check, allMoves, theoretical, false);
        isolatedIterateThroughPieceMoves(HelperFunctions.oneTimeMoveAndAttacksComparator, piece, bs, piece.oneTimeMoveAndAttacks, check, allMoves, theoretical, false);
        isolatedIterateThroughPieceMoves(HelperFunctions.murderousAttacksComparator, piece, bs, piece.murderousAttacks, check, allMoves, theoretical, false);
        isolatedIterateThroughPieceMoves(HelperFunctions.conditionalAttacksComparator, piece, bs, piece.conditionalAttacks, check, allMoves, theoretical, false);
        isolatedIterateThroughPieceMoves(HelperFunctions.jumpAttacksComparator, piece, bs, piece.jumpAttacks, check, allMoves, theoretical, false);

        //TODO fix dependentMoves for isolated state
        //piece.dependentMovesSet();
        //isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.dependentAttacks, check, allMoves);

        //piece.interactiveMovesSet();
        //isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.interactiveAttacks, check, allMoves);

        HelperFunctions.updatePieceFlags(piece, check);
        if (piece.flag == 1)
        {
            isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.flagMove1, check, allMoves, theoretical, false);
        }
        else if (piece.flag == 2)
        {
            isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.flagMove2, check, allMoves, theoretical, false);
        }

        return allMoves;
    }

    public static bool Jay_isCheck(BotTemplate bot, BoardState bs, int color)
    {
        Piece king = isolatedGetKing(bs, color);

        if (king == null)
        {
            return true;
        }

        List<NextMove> allOppKills = getAllPossibleBotAttacksAndAbilities(bot, bs, color * -1);

        foreach(NextMove nm in allOppKills)
        {
            var moveVars = getNextMoveVars(nm);
            Piece p = moveVars.piece;
            coords c = moveVars.coords;
            string moveType = moveVars.moveType;

            if (moveType == "move")
            {
                if (checkState(p, PieceState.Dematerialized))
                {
                    continue;
                }

                if (king.position.x == c.x && king.position.y == c.y)
                {
                    //Debug.LogError("Egbot: Opponent Move " + p + " to " + (c.x) + (c.y) + " is causing a check");
                    //Debug.Break();
                    return true;
                }
            }
            else if (moveType == "ability")
            {
                if (checkAbilityStrict(nm.ability.ability, PieceAbilities.Materialize))
                {
                    if (Jay_areCoordsEqual(nm.ability.coords, king.position))
                    {
                        //Debug.LogError("MATERIALIZING ON KING");
                        //Debug.Break();
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static bool Jay_areCoordsEqual(coords a, coords b)
    {
        return a.x == b.x && a.y == b.y;
    }
}
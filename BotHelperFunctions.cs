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
using System.Text;

public class BotHelperFunctions : MonoBehaviour
{
	public static List<Piece> getPiecesTypeRandom(string type, int color) {
        List<Type> pieces = getAllTypePieces(type, color);

        List<Piece> selected = new List<Piece>();

		int count = 2;

		if (type == "Pawn") {
			count = 8;
		}
		else if (type == "Queen" || type == "King") {
			count = 1;
		}

        System.Random rand = new System.Random();

        for (int i = 0; i < count; i++)
        {
            int index = rand.Next(pieces.Count);

            Type type_ = pieces[index];
            Piece piece = (Piece)Activator.CreateInstance(type_, color, false, false);

            selected.Add(piece);
            pieces.RemoveAt(index);
        }

        return selected;
	}

    public static Piece getPieceTypeInstance(string type, int color)
    {
        Type type_ = Type.GetType(type + ", Assembly-CSharp");
        Piece piece = (Piece)Activator.CreateInstance(type_, color, false, false);

        return piece;
    }

    private static List<Type> getAllTypePieces(string type, int color)
    {

        List<Type> allPieces = Lootbox.GetAllPieces();
        List<Type> eligiblePieces = new List<Type>();

        foreach (var piece_ in allPieces)
        {

            Piece piece = (Piece)Activator.CreateInstance(piece_, color, false, false);

            if (piece.baseType == type)
            {
                eligiblePieces.Add(piece_);
            }

            if (piece.go != null)
            {
                Destroy(piece.go);
            }
        }

        return eligiblePieces;
    }

    public static List<coords> isolatedGetCollateralSquares(Piece p, BoardState bs) {
        List<coords> possibleCoords = new List<coords>();
        coords[] collateral = new coords[]
        {
            new coords(1, 1),
            new coords(1, 0),
            new coords(1, -1),
            new coords(-1, 1),
            new coords(-1, 0),
            new coords(-1, -1),
            new coords(0, -1),
            new coords(0, 1)
        };

        if (p.collateral != null)
        {
            collateral = p.collateral;
        }

        if (collateral.Length == 0)
        {
            return null;
        }

        for (int i = 0; i < collateral.GetLength(0); i++)
        {
            int xOffset = collateral[i].x;
            int yOffset = collateral[i].y;

            coords coords = new coords(p.position.x + xOffset, p.position.y + yOffset);

            if (!HelperFunctions.checkBounds(coords.x, coords.y)) continue;

            if (isolatedGetPiecesOnCoordsBoardGrid(coords.x - 1, coords.y - 1, bs.boardGrid, false).Count > 0)
            {
                continue;
            }

            possibleCoords.Add(coords);
        }

        return possibleCoords;
    }

    public static List<Piece> filterPieces(string type, List<Piece> pieces) {
    	List<Piece> filteredPieces = new List<Piece>();

    	foreach (var piece in pieces) {
            if (piece.baseType == type) {
    			filteredPieces.Add(piece);
    		}
    	}

        return filteredPieces;
    }

    public static bool isolatedArePiecesInBetweenSquaresHorizontal(int posX1, int posY1, int posX2, int posY2, BoardState bs)
    {
        if (posY1 != posY2)
        {
            return false;
        }

        int y = posY1;
        int x1 = posX1;
        int x2 = posX2;

        int dir = (x1 - x2) / Math.Abs(x1 - x2);

        for (int i = x2 + dir; i != x1; i += dir)
        {
            if (isolatedGetPiecesOnCoordsBoardGrid(i - 1, y - 1, bs.boardGrid, false).Count > 0)
            {
                return true;
            }
        }

        return false;
    }

    public static Piece findPieceOnBoardStateFromPanelCode(BoardState bs, string panelCode)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                foreach (Piece p in bs.boardGrid[i, j])
                {
                    if (p.name == panelCode)
                    {
                        return p;
                    }
                }
            }
        }

        return null;
    }

    public static bool isolatedCheckCanCastle(BotTemplate bot, BoardState bs, int direction)
    {
        Piece king = bot.king;
        Piece rook;

        if (bot.king == null)
        {
            bot.king = isolatedGetKing(bs, bot.color);
        }

        if (HelperFunctions.checkState(king, PieceState.Uncastle))
        {
            return false;
        }

        if (bot.color == 1)
        {
            if (direction == -1)
            {
                rook = findPieceOnBoardStateFromPanelCode(bs, "w_r1");
            }
            else
            {
                rook = findPieceOnBoardStateFromPanelCode(bs, "w_r2");
            }
        }
        else
        {
            if (direction == -1)
            {
                rook = findPieceOnBoardStateFromPanelCode(bs, "b_r1");
            }
            else
            {
                rook = findPieceOnBoardStateFromPanelCode(bs, "b_r2");
            }
        }

        if (king == null || rook == null)
        {
            return false;
        }

        bool goNext = false;
        if (!king.hasMoved && !rook.hasMoved)
        {
            if (king.color == 1 && bs.inCheck[0] == 0 || king.color == -1 && bs.inCheck[1] == 0)
            {
                goNext = true;
            }

            if (HelperFunctions.checkState(king, PieceState.Rulebreaker))
            {
                goNext = true;
            }
        }
        

        if (goNext)
        {
            //Debug.LogWarning("Checking for Castle: King:" + king.position[0] + "," + king.position.y + " Rook: " + rook.position[0] + "," + rook.position.y);
            if (isolatedArePiecesInBetweenSquaresHorizontal(king.position.x, king.position.y, rook.position.x, rook.position.y, bs))
            {
                return false;
            }

            if (isolatedGetPiecesOnCoordsBoardGrid(king.position.x - 1, king.position.y - 1, bs.boardGrid, false).Count > 0)
            {
                return false;
            }

            if (isolatedGetPiecesOnCoordsBoardGrid(rook.position.x - 1, rook.position.y - 1, bs.boardGrid, false).Count > 0)
            {
                return false;
            }

            return true;
        }
        return false;
    }

    public static string removeDuplicateAbilities(string pieceAbilities)
    {
        if (!string.IsNullOrEmpty(pieceAbilities))
        {
            var unique = new List<string>();

            foreach (var a in pieceAbilities.Split('-'))
            {
                if (!unique.Contains(a))
                {
                    unique.Add(a);
                }
            }

            pieceAbilities = string.Join("-", unique);
        }

        return pieceAbilities;
    }

    public static List<PieceAbility> getAllPossibleBotAbilities(BotTemplate bot, BoardState bs, int color) {
        //List<Piece> pieces = color == 1 ? bs.whitePieces : bs.blackPieces;
        List<Piece> pieces = getPiecesOnBoardState(bs, color);
        List<PieceAbility> pieceAbilities = new List<PieceAbility>();

        foreach(Piece piece in pieces) {

            if (HelperFunctions.checkAbility(piece, PieceAbilities.Vomit))
            {
                if (piece.storage != null && piece.storage.Count < 1)
                {
                    continue;
                }
                else if (piece.storage == null)
                {
                    continue;
                }

                List<Piece> placePieces = new List<Piece>();

                foreach (Piece storedPiece in piece.storage)
                {
                    placePieces.Add(storedPiece);
                }

                List<coords> possibleCoords = isolatedGetCollateralSquares(piece, bs);

                if (possibleCoords.Count == 0)
                {
                    continue;
                }

                PieceAbility vomit = new PieceAbility(piece, PieceAbilities.Vomit, piece.position, placePieces, possibleCoords, null);
                pieceAbilities.Add(vomit);
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.CastleLeft))
            {
                if (!isolatedCheckCanCastle(bot, bs, -1))
                {
                    continue;
                }

                Piece king = bot.king;
                Piece rook;
                if (bot.color == 1) rook = HelperFunctions.findPieceFromPanelCode("w_r1");
                else rook = HelperFunctions.findPieceFromPanelCode("b_r1");
                coords coords = new coords (king.position.x - 2, king.position.y);
                PieceAbility castle = new PieceAbility(king, PieceAbilities.CastleLeft, coords, null, null, rook);
                pieceAbilities.Add(castle);
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.CastleRight))
            {
                if (!isolatedCheckCanCastle(bot, bs, 1))
                {
                    continue;
                }

                Piece king = bot.king;
                Piece rook;
                if (bot.color == 1) rook = HelperFunctions.findPieceFromPanelCode("w_r2");
                else rook = HelperFunctions.findPieceFromPanelCode("b_r2");
                coords coords = new coords (king.position.x - 2, king.position.y);
                PieceAbility castle = new PieceAbility(king, PieceAbilities.CastleRight, coords, null, null, rook);
                pieceAbilities.Add(castle);
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.Unfreeze))
            {
                if (!HelperFunctions.checkState(piece, PieceState.Frozen))
                {
                    continue;
                }

                PieceAbility unFreeze = new PieceAbility(piece, PieceAbilities.Unfreeze, piece.position, null, null, null);
                pieceAbilities.Add(unFreeze);
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.Freeze))
            {
                if (!isolatedIsPieceSurroundingColor(piece, piece.color * -1, bs))
                {
                    continue;
                }

                foreach (var (dirX, dirY) in globalDefs.globalDirectionsNoZero)
                {
                    int posX = piece.position.x + dirX - 1;
                    int posY = piece.position.y + dirY - 1;

                    if (!HelperFunctions.checkBounds(posX + 1, posY + 1)) continue;

                    List<Piece> piecesOnDir = isolatedGetPiecesOnCoordsBoardGrid(posX, posY, bs.boardGrid, false);
                    foreach (Piece p_ in piecesOnDir)
                    {
                        if (p_.color != piece.color && !HelperFunctions.checkState(p_, PieceState.Frozen))
                        {
                            PieceAbility freeze = new PieceAbility(piece, PieceAbilities.Freeze, new coords( posX + 1, posY + 1 ), null, null, p_);
                            pieceAbilities.Add(freeze);
                        }
                    }
                }
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.Spawn))
            {
                if (piece.numSpawns <= 0)
                {
                    continue;
                }

                if (isolatedAreSurroundingSquaresFull(piece, bs))
                {
                    continue;
                }

                foreach (var (dirX, dirY) in globalDefs.globalDirectionsNoZero)
                {
                    int posX = piece.position.x + dirX - 1;
                    int posY = piece.position.y + dirY - 1;

                    if (!HelperFunctions.checkBounds(posX + 1, posY + 1)) continue;

                    List<Piece> piecesOnDir = isolatedGetPiecesOnCoordsBoardGrid(posX, posY, bs.boardGrid, false);
                    if (piecesOnDir.Count > 0)
                    {
                        continue;
                    }

                    PieceAbility spawn = new PieceAbility(piece, PieceAbilities.Spawn, new coords( posX + 1, posY + 1 ), null, null, null);
                    pieceAbilities.Add(spawn);
                }
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.Spit))
            {
                if (piece.storage == null || (piece.storage != null && piece.storage.Count <= 0))
                {
                    continue;
                }

                foreach (var (dirX, dirY) in globalDefs.globalDirectionsNoZero)
                {
                    int posX = piece.position.x + dirX - 1;
                    int posY = piece.position.y + dirY - 1;

                    if (!HelperFunctions.checkBounds(posX + 1, posY + 1)) continue;

                    //Debug.Log("NEW SPIT FOUND: " + piece.name + " -> " + piece.storage.x.name + "");

                    PieceAbility spit = new PieceAbility(piece, PieceAbilities.Spit, new coords( posX + 1, posY + 1 ), null, null, piece.storage[0]);
                    pieceAbilities.Add(spit);
                }
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.Dematerialize))
            {
                if (HelperFunctions.checkState(piece, PieceState.Dematerialized))
                {
                    continue;
                }

                PieceAbility dematerialize = new PieceAbility(piece, PieceAbilities.Dematerialize, piece.position, null, null, null);
                pieceAbilities.Add(dematerialize);
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.Materialize))
            {
                if (!HelperFunctions.checkState(piece, PieceState.Dematerialized))
                {
                    continue;
                }

                PieceAbility materialize = new PieceAbility(piece, PieceAbilities.Materialize, piece.position, null, null, null);
                pieceAbilities.Add(materialize);
            }

            if (HelperFunctions.checkAbility(piece, PieceAbilities.Split))
            {
                PieceAbility split = new PieceAbility(piece, PieceAbilities.Split, piece.position, null, null, null);
                pieceAbilities.Add(split);
            }
        }

        return pieceAbilities;
    }

    public static bool isolatedAreSurroundingSquaresFull(Piece piece, BoardState bs)
    {
        foreach (var (dirX, dirY) in globalDefs.globalDirectionsNoZero)
        {
            int coordsX = dirX + piece.position.x - 1;
            int coordsY = dirY + piece.position.y - 1;

            if (!HelperFunctions.checkBounds(coordsX + 1, coordsY + 1)) continue;

            if (isolatedGetPiecesOnCoordsBoardGrid(coordsX, coordsY, bs.boardGrid, false).Count == 0)
            {
                return false;
            }
        }

        return true;
    }

    public class PieceAbility
    {
        public Piece piece; //The piece with the ability
        //public string ability; //Ability name
        public PieceAbilities ability;
        public coords coords; //Coords for abilities with one action (ie. Spawning, Freezing)
        public List<Piece> placePieces; //Pieces for abilities with multiple actions. Only hungry for now
        public List<coords> placeCoords; //Coords for abilities with multiple actions. Only hungry for now
        public Piece secondPiece; //The second piece used in abilities. Used for castling/spawning

        public PieceAbility(Piece piece, PieceAbilities ability, coords coords, List<Piece> placePieces, List<coords> placeCoords, Piece secondPiece)
        {
            this.piece = piece;
            this.ability = ability;
            this.coords = new coords( coords.x, coords.y );
            this.placePieces = placePieces;
            this.placeCoords = placeCoords;
            this.secondPiece = secondPiece;
        }
    }

    public struct PieceMoveList
    {
        public Piece piece;
        public List<coords> moves;

        public PieceMoveList(Piece p, List<coords> m)
        {
            piece = p;
            moves = m;
        }
    }

    public static (List<PieceMoveList> pieceMoveList, List<PieceAbility> piecesAbilities) getAllPossibleBotMoves(BotTemplate bot, BoardState bs, int color)
    {
        List<PieceMoveList> totalMoves = new List<PieceMoveList>();

        List<Piece> pieces = getPiecesOnBoardState(bs, color);

        for (int i = 0; i < pieces.Count; i++)
        {
            Piece piece = pieces[i];

            List<coords> moves = getIsolatedStatePieceMoves(piece, bs, false);

            if (moves != null && moves.Count > 0)
            {
                totalMoves.Add(new PieceMoveList(piece, moves));
            }
        }

        List<PieceAbility> pieceAbilities = getAllPossibleBotAbilities(bot, bs, color);

        return (totalMoves, pieceAbilities);
    }

    public static List<NextMove> getAllPossibleBotKillsAndAbilities(BotTemplate bot, BoardState bs, int color)
    {
        //resetPiecePositions(null, bs.boardGrid);
        //bs = copyBoardState(bs);

        var botMoves = getAllPossibleBotKills(bot, bs, color);

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

    public static (List<PieceMoveList> pieceMoveList, List<PieceAbility> piecesAbilities) getAllPossibleBotKills(BotTemplate bot, BoardState bs, int color)
    {
        List<PieceMoveList> totalMoves = new List<PieceMoveList>();

        List<Piece> pieces = getPiecesOnBoardState(bs, color);

        for (int i = 0; i < pieces.Count; i++)
        {
            Piece piece = pieces[i];

            List<coords> moves = getIsolatedStatePieceAttacks(piece, bs, false, true);

            if (moves != null && moves.Count > 0)
            {
                totalMoves.Add(new PieceMoveList(piece, moves));
            }
        }

        List<PieceAbility> pieceAbilities = getAllPossibleBotAbilities(bot, bs, color);

        return (totalMoves, pieceAbilities);
    }

    public static (List<PieceMoveList> pieceMoveList, List<PieceAbility> piecesAbilities) getAllPossibleBotAttacks(BotTemplate bot, BoardState bs, int color)
    {
        List<PieceMoveList> totalMoves = new List<PieceMoveList>();

        List<Piece> pieces = getPiecesOnBoardState(bs, color);

        for (int i = 0; i < pieces.Count; i++)
        {
            Piece piece = pieces[i];

            List<coords> moves = getIsolatedStatePieceAttacks(piece, bs, false, false);

            if (moves != null && moves.Count > 0)
            {
                totalMoves.Add(new PieceMoveList(piece, moves));
            }
        }

        List<PieceAbility> pieceAbilities = getAllPossibleBotAbilities(bot, bs, color);

        return (totalMoves, pieceAbilities);
    }

    public static (List<PieceMoveList> pieceMoveList, List<PieceAbility> piecesAbilities) getAllTheoreticalBotAttacks(BotTemplate bot, BoardState bs, int color)
    {
        List<PieceMoveList> totalMoves = new List<PieceMoveList>();

        List<Piece> pieces = getPiecesOnBoardState(bs, color);

        for (int i = 0; i < pieces.Count; i++)
        {
            Piece piece = pieces[i];

            List<coords> moves = getIsolatedStatePieceAttacks(piece, bs, true, false);

            if (moves != null && moves.Count > 0)
            {
                totalMoves.Add(new PieceMoveList(piece, moves));
            }
        }

        List<PieceAbility> pieceAbilities = getAllPossibleBotAbilities(bot, bs, color);

        return (totalMoves, pieceAbilities);
    }

    public static List<NextMove> getAllPossibleBotPieceMoves(BoardState bs, Piece piece)
    {
        List<PieceMoveList> totalMoves = new List<PieceMoveList>();

        List<NextMove> allMoves = new List<NextMove>();
        List<coords> moves = getIsolatedStatePieceMoves(piece, bs, false);

        if (moves != null && moves.Count > 0)
        {
            totalMoves.Add(new PieceMoveList(piece, moves));
        }

        foreach(PieceMoveList pml in totalMoves)
        {
            Piece piece_ = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                Move mv = new Move(piece_, coords);
                NextMove pieceMove = new NextMove(mv);

                allMoves.Add(pieceMove);
            }
        }

        return allMoves;
    }

    public static List<NextMove> getAllPossibleBotPieceAttacks(BoardState bs, Piece piece)
    {
        List<PieceMoveList> totalMoves = new List<PieceMoveList>();

        List<NextMove> allMoves = new List<NextMove>();
        List<coords> moves = getIsolatedStatePieceAttacks(piece, bs, false, false);

        if (moves != null && moves.Count > 0)
        {
            totalMoves.Add(new PieceMoveList(piece, moves));
        }

        foreach (PieceMoveList pml in totalMoves)
        {
            Piece piece_ = pml.piece;
            List<coords> _mL = pml.moves;

            foreach (coords coords in _mL)
            {
                Move mv = new Move(piece_, coords);
                NextMove pieceMove = new NextMove(mv);

                allMoves.Add(pieceMove);
            }
        }

        return allMoves;
    }

    public static List<NextMove> getAllPossibleBotMovesAndAbilities(BotTemplate bot, BoardState bs, int color)
    {
        //resetPiecePositions(null, bs.boardGrid);
        //bs = copyBoardState(bs);

        var botMoves = getAllPossibleBotMoves(bot, bs, color);

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

        foreach(PieceAbility pa in allAbilitiesBot)
        {
            NextMove pieceAbility = new NextMove(pa);
            allMoves.Add(pieceAbility);
        }

        return allMoves;
    }

    public static List<NextMove> getAllPossibleBotAttacksAndAbilities(BotTemplate bot, BoardState bs, int color)
    {
        //resetPiecePositions(null, bs.boardGrid);
        //bs = copyBoardState(bs);

        var botMoves = getAllPossibleBotAttacks(bot, bs, color);

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

    public static List<Piece> getPiecesOnBoardState(BoardState bs, int color)
    {
        List<Piece> pieces = new List<Piece>();
        List<Piece>[,] boardGrid = bs.boardGrid;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece p in boardGrid[x, y])
                {
                    if (p.color == color)
                    {
                        pieces.Add(p);
                    }
                }
            }
        }

        return pieces;
    }

    public static List<coords> getIsolatedStatePieceMoves(Piece piece, BoardState bs, bool theoretical)
    {
        List<coords> allMoves = new List<coords>();

        List<Piece>[,] boardGrid = bs.boardGrid;
        bool check = false;
        //if is check TODO check = true

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

    public static List<coords> getIsolatedStatePieceAttacks(Piece piece, BoardState bs, bool theoretical, bool onlyKills)
    {
        List<coords> allMoves = new List<coords>();

        List<Piece>[,] boardGrid = bs.boardGrid;
        bool check = false;
        //if is check TODO check = true

        //todo maybe forcestayturn

        //isolatedIterateThroughPieceMoves(HelperFunctions.moveComparator, piece, bs, piece.moves, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.moveAndAttacks, check, allMoves, theoretical, onlyKills);
        isolatedIterateThroughPieceMoves(HelperFunctions.attacksComparator, piece, bs, piece.attacks, check, allMoves, theoretical, onlyKills);
        //isolatedIterateThroughPieceMoves(HelperFunctions.oneTimeMovesComparator, piece, bs, piece.oneTimeMoves, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.oneTimeMoveAndAttacksComparator, piece, bs, piece.oneTimeMoveAndAttacks, check, allMoves, theoretical, onlyKills);
        isolatedIterateThroughPieceMoves(HelperFunctions.murderousAttacksComparator, piece, bs, piece.murderousAttacks, check, allMoves, theoretical, onlyKills);
        isolatedIterateThroughPieceMoves(HelperFunctions.conditionalAttacksComparator, piece, bs, piece.conditionalAttacks, check, allMoves, theoretical, onlyKills);
        isolatedIterateThroughPieceMoves(HelperFunctions.jumpAttacksComparator, piece, bs, piece.jumpAttacks, check, allMoves, theoretical, onlyKills);

        //TODO fix dependentMoves for isolated state
        //piece.dependentMovesSet();
        //isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.dependentAttacks, check, allMoves);

        //piece.interactiveMovesSet();
        //isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.interactiveAttacks, check, allMoves);

        HelperFunctions.updatePieceFlags(piece, check);
        if (piece.flag == 1)
        {
            isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.flagMove1, check, allMoves, theoretical, onlyKills);
        }
        else if (piece.flag == 2)
        {
            isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.flagMove2, check, allMoves, theoretical, onlyKills);
        }

        return allMoves;
    }

    private static void isolatedIterateThroughPieceMoves(Func<Piece, bool, bool, bool, List<Piece>, bool> comparator, Piece piece, BoardState bs, coords[] moveType, bool check, List<coords> allMoves, bool theoretical, bool onlyKills)
    {
        if (HelperFunctions.checkState(piece, PieceState.Frozen) || HelperFunctions.checkState(piece, PieceState.Jailed))
        {
            return;
        }

        if (HelperFunctions.checkPieceType(piece, "q"))
        {
            if (isolatedIsOppressorOnBoard(bs, piece.color))
            {
                return;
            }
        }

        bool isPortal = HelperFunctions.checkState(piece, PieceState.Portal);
        bool isBouncing = HelperFunctions.checkState(piece, PieceState.Bouncing);

        // Optimization
        int lastDirX = 0;
        int lastDirY = 0;
        bool previousWasJump = false;

        for (int i = 0; i < moveType.GetLength(0); i++)
        {
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
            if (previousWasJump)
            {
                continue;
            }

            //Debug.Log("Moving piece " + piece.name + " from " + piece.position.x + "," + piece.position.y + " to " + oldCoordsX + "," + oldCoordsY);

            //int[] newPos = new int[] { oldCoords.x, oldCoords.y };
            int newPosX = oldCoordsX;
            int newPosY = oldCoordsY;

            if (isPortal)
            {
                coords coordsP = HelperFunctions.adjustCoordsForPortal(piece, oldCoordsX, oldCoordsY);
                //newPos.x = coordsP.x;
                //newPos.y = coordsP.y;
                newPosX = coordsP.x;
                newPosY = coordsP.y;
                //newPos = HelperFunctions.adjustCoordsForPortal(piece, oldCoords.x, oldCoords.y);
            }
            else if (isBouncing)
            {
                coords coordsB = HelperFunctions.adjustCoordsForBouncing(piece, oldCoordsX, oldCoordsY);
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
                //pieceIsDiffColour = !isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color);
                //pieceIsDiffColour = !isolatedIsColorOnCoords(piecesOnCoords, true, piece.color);
                pieceIsDiffColour = !diagnostics.colorOnCoords;

                if (diagnostics.squareJailed)
                {
                    pieceIsDiffColour = true;
                }

                //if (HelperFunctions.checkPiecesDisabled(piecesOnCoords))
                if (diagnostics.piecesDisabled)
                {
                    pieceIsNull = true;
                }

                //checkSquareCrowdingEligible
                //if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords))
                if (diagnostics.crowdingElegible) {
                    pieceIsNull = true;
                }

                //Check for states
                //if (HelperFunctions.checkStateOnSquare(piecesOnCoords, "Shield")) {
                if (diagnostics.shieldOnSquare || diagnostics.captureTheFlagOnSquare) {
                    continue;
                }

                /*
                if (HelperFunctions.checkStateOnSquare(piecesOnCoords, "CaptureTheFlag")) {
                    bool _continue = false;
                    foreach (Piece piece_ in piecesOnCoords) {
                        if (HelperFunctions.checkCaptureTheFlag(piece_)) {
                            _continue = true;
                            break;
                        }
                    }

                    if (_continue) {
                        continue;
                    }
                }
                */
            }

            bool jump;
            if (isPortal && !((oldCoordsX == newPosX) && (oldCoordsY == newPosY)))
            {
                if (HelperFunctions.isKnightPortalBackRank_(piece, oldCoordsX, oldCoordsY, newPosX, newPosY))
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
                jump = isolatedIsJump(piece, piece.position, newPosX, newPosY, bs);
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

            if (comparator(piece, jump, pieceIsNull, pieceIsDiffColour, piecesOnCoords) || (theoretical && !jump))
            {
                if (onlyKills)
                {
                    if (!pieceIsNull)
                    {
                        allMoves.Add(new coords(newPosX, newPosY));
                    }
                }
                else
                {
                    allMoves.Add(new coords(newPosX, newPosY));
                }
                //TODO maybe add check functionality
                //if (piece.name == "w_k1" || piece.name == "b_k1") Debug.Log("MOVE SIM " + newPosX + "," + newPosY + " J: " + jump + " PIN: " + pieceIsNull + " PID: " + pieceIsDiffColour + " POC: " + piecesOnCoords.Count);
                
            }
        }
    }

    public static (bool colorOnCoords, bool oppColorOnCoords, bool colorOnlyOnCoords, bool oppColorOnlyOnCoords, bool pieceIsNull, bool crowdingElegible, bool piecesDisabled, bool shieldOnSquare, bool captureTheFlagOnSquare, bool squareJailed) isolatedGetPiecesOnCoordsDiagnostics(Piece piece, List<Piece> piecesOnCoords, BoardState bs)
    {
        int color = piece.color;

        bool colorOnCoords = false;
        bool oppColorOnCoords = false;
        bool colorOnlyOnCoords = false;
        bool oppColorOnlyOnCoords = false;
        bool pieceIsNull = false;
        bool crowdingElegible = false;
        bool piecesDisabled = true;
        bool shieldOnSquare = false;
        bool captureTheFlagOnSquare = false;
        bool squareJailed;

        int piecesOnCoordsCount = 0;
        bool allCrowding = true;
        bool allPiggyback = true;

        bool jailerOnSquare = false;
        bool jailedOnSquare = false;

        if (piecesOnCoords == null || piecesOnCoords.Count == 0)
        {
            pieceIsNull = true;
        }
        else
        {
            foreach (Piece p in piecesOnCoords)
            {
                if (HelperFunctions.checkState(p, PieceState.Shield))
                {
                    shieldOnSquare = true;
                }

                if (HelperFunctions.checkState(p, PieceState.CaptureTheFlag))
                {
                    if (HelperFunctions.checkCaptureTheFlag(p))
                    {
                        captureTheFlagOnSquare = true;
                    }
                }

                if (HelperFunctions.checkState(p, PieceState.Jailed))
                {
                    jailedOnSquare = true;
                }

                if (HelperFunctions.checkState(p, PieceState.Jailer))
                {
                    jailerOnSquare = true;
                }

                bool dematerialized = HelperFunctions.checkState(p, PieceState.Dematerialized);

                if (dematerialized || HelperFunctions.checkState(piece, PieceState.Jailed))
                {
                    continue;
                }

                piecesOnCoordsCount++;

                if (!p.disabled && !dematerialized)
                {
                    piecesDisabled = false;
                }

                if (p.color == color)
                {
                    colorOnCoords = true;
                }
                else
                {
                    oppColorOnCoords = true;
                }

                if (!HelperFunctions.checkState(p, PieceState.Crowding))
                {
                    allCrowding = false;
                }

                if (!HelperFunctions.checkState(p, PieceState.Piggyback))
                {
                    allPiggyback = false;
                }
            }
        }

        if (colorOnCoords && !oppColorOnCoords)
        {
            colorOnlyOnCoords = true;
        }
        else if (oppColorOnCoords && !colorOnCoords)
        {
            oppColorOnlyOnCoords = true;
        }


        if (!pieceIsNull)
        {
            if (oppColorOnCoords)
            {
                crowdingElegible = false;
            }
            else if (piecesOnCoordsCount > 1 && HelperFunctions.checkState(piece, PieceState.Crowding))
            {
                crowdingElegible = allCrowding;
            }
            else if (HelperFunctions.checkState(piece, PieceState.Crowding) && piecesOnCoordsCount == 1 && colorOnlyOnCoords)
            {
                crowdingElegible = true;
            }
            else if (piecesOnCoordsCount == 1 && allPiggyback && colorOnlyOnCoords)
            {
                if (!HelperFunctions.checkState(piece, PieceState.CaptureTheFlag) && piece.baseType != "King")
                {
                    crowdingElegible = true;
                }
            }
            else if (piecesOnCoordsCount == 1 && HelperFunctions.checkState(piece, PieceState.Jockey) && colorOnlyOnCoords)
            {
                crowdingElegible = true;
            }
            else if (HelperFunctions.checkState(piece, PieceState.Crowding))
            {
                crowdingElegible = true;
            }
        }

        squareJailed = jailerOnSquare && jailedOnSquare;

        return (colorOnCoords, oppColorOnCoords, colorOnlyOnCoords, oppColorOnlyOnCoords, pieceIsNull, crowdingElegible, piecesDisabled, shieldOnSquare, captureTheFlagOnSquare, squareJailed);
    }

    public static bool isolatedIsJump(Piece piece, coords from, int toX, int toY, BoardState bs) {
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

                if (HelperFunctions.checkState(p, PieceState.Dematerialized) && p.color == piece.color)
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
                return true;
            }
        }
        return false;
    }

    public static bool isolatedIsJumpBouncing(Piece piece, coords from, int toX, int toY, BoardState bs)
    {
        //TODO this does not work at all
        int fromX = from.x;
        int fromY = from.y;

        foreach (var (dx, dy) in globalDefs.globalDiagionalDirectionsNoZero)
        {
            int x = fromX;
            int y = fromY;

            for (int i = 0; i < 14; i++)
            {
                x += dx;
                y += dy;

                coords newCoords = HelperFunctions.adjustCoordsForBouncing(piece, x, y);
                int newX = newCoords.x;
                int newY = newCoords.y;

                if (newX == toX && newY == toY) {
                    return false;
                }

                //List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(newX - 1, newY - 1, bs.boardGrid, false);

                //if (piecesOnCoords.Count > 0) {
                   //break;
                //}

                if (!isolatedPieceCanJumpOver(newX - 1, newY - 1, bs.boardGrid, piece))
                {
                    return true;
                }
            }
        }

        return true;
    }

    public static bool isolatedPieceCanJumpOver(int x, int y, List<Piece>[,] boardGrid, Piece piece)
    {
        bool isGhost = HelperFunctions.checkState(piece, PieceState.Ghost);

        foreach (Piece p in boardGrid[x, y])
        {
            if (HelperFunctions.checkState(p, PieceState.Ghoul) && p.color == piece.color)
            {
                // Your Ghoul
                continue;
            }

            if (HelperFunctions.checkState(p, PieceState.Dematerialized) && p.color == piece.color)
            {
                // Your Dematerialized
                continue;
            }

            if (isGhost && p.color == piece.color)
            {
                // Your piece is a ghost, your piece
                continue;
            }

            return false;
        }
 
        return true;
    }

    public static bool isolatedIsJumpPortal(Piece piece, coords from, int toX, int toY, BoardState bs) {
        int fromX = from.x;
        int fromY = from.y;

        //bool anyPathFound = false;
        foreach (var (dx, dy) in globalDefs.globalDirectionsNoZero)
        {
            int x = fromX;
            int y = fromY;
            //bool crossedBackRank = false;
            //bool jumpedPiece = false;

            for (int step = 0; step < 8; step++)
            {
                //x += dir.x;
                //y += dir.y;
                x += dx;
                y += dy;

                if (y == 0 && piece.color == 1 || y == 9 && piece.color == -1) {
                    //crossedBackRank = true;
                    break;
                };

                if (x < 1) x = 8;
                if (x > 8) x = 1;
                if (y < 1) y = 8;
                if (y > 8) y = 1;

                if (x == fromX && y == fromY) break;

                if (x == toX && y == toY)
                {
                    //if (!(crossedBackRank || jumpedPiece))
                    //{
                        //anyPathFound = true;
                        return false;
                    //}
                }

                //List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid, false);
                //if (bs.boardGrid[x - 1, y - 1].Count > 0)
                //{
                    //jumpedPiece = true;
                    //break;
                //}

                if (!isolatedPieceCanJumpOver(x - 1, y - 1, bs.boardGrid, piece))
                {
                    return true;
                }
            }
        }

        return true;
    }

    public static bool isolatedCheckSquareCrowdingEligible(Piece piece, List<Piece> piecesOnCoords) {
        // No pieces
        if (piecesOnCoords == null || piecesOnCoords.Count == 0) {
            return true;
        }

        /*
        List<int> colorsOnCoords = isolatedGetColorsOnCoords(piecesOnCoords, true);

        //Pieces different color
        if (colorsOnCoords.Contains(piece.color * -1)) {
            return false;
        }
        */

        if (isolatedIsColorOnCoords(piecesOnCoords, true, piece.color * -1)) {
            return false;
        }

        // Square contains more than one other piece (not crowding)
        if (piecesOnCoords.Count > 1 && HelperFunctions.checkState(piece, PieceState.Crowding))
        {
            foreach (Piece _piece in piecesOnCoords)
            {
                if (!HelperFunctions.checkState(_piece, PieceState.Crowding))
                {
                    return false;
                }
            }

            // If they are all crowding
            return true;
        }

        int piecesOnCoordsCount = piecesOnCoords.Count;
        bool sameColorOnCoords = isolatedIsColorOnCoords(piecesOnCoords, true, piece.color);

        //There is one piece on the square, piece is crowding
        if (HelperFunctions.checkState(piece, PieceState.Crowding) && piecesOnCoordsCount == 1 && sameColorOnCoords) {
            return true;
        }

        //There is one piece on the square, piece is piggyback
        if (piecesOnCoordsCount == 1 && HelperFunctions.checkStateAllOnSquare(piecesOnCoords, PieceState.Piggyback) && sameColorOnCoords) {
            if (!HelperFunctions.checkState(piece, PieceState.CaptureTheFlag) && piece.baseType != "King")
            {
                return true;
            }
        }

        //There is one piece on square, piece is jockey
        if (piecesOnCoordsCount == 1 && HelperFunctions.checkState(piece, PieceState.Jockey) && sameColorOnCoords) {
            return true;
        }

        if (!HelperFunctions.checkState(piece, PieceState.Crowding))
        {
            return false;
        }
        else
        {
            //Last case square contains one piece and its same color
            return true;
        }
    }

    public static List<int> isolatedGetColorsOnCoords(List<Piece> piecesOnCoords, bool ignoreDematerialized)
    {
        List<int> colors = new List<int>();

        if (piecesOnCoords == null)
        {
            return colors;
        }

        foreach (Piece piece in piecesOnCoords)
        {
            if (/*piece.disabled || piece.alive == 0 || */(ignoreDematerialized && HelperFunctions.checkState(piece, PieceState.Dematerialized)) || HelperFunctions.checkState(piece, PieceState.Jailed))
            {
                continue;
            }

            colors.Add(piece.color);
        }

        return colors;
    }

    public static bool isolatedIsColorOnCoords(List<Piece> piecesOnCoords, bool ignoreDematerialized, int color)
    {
        foreach (Piece piece in piecesOnCoords)
        {
            if (/*piece.disabled || piece.alive == 0 || */(ignoreDematerialized && HelperFunctions.checkState(piece, PieceState.Dematerialized)) || HelperFunctions.checkState(piece, PieceState.Jailed))
            {
                continue;
            }

            if (piece.color == color) {
                return true;
            }
        }

        return false;
    }

    private static bool isolatedIsOppressorOnBoard(BoardState bs, int color)
    {
        List<Piece>[,] boardGrid = bs.boardGrid;

        List<Piece> pieces = isolatedGetPiecesOnBoardGrid(boardGrid);
        foreach (Piece p in pieces)
        {
            if (HelperFunctions.checkState(p, PieceState.Oppressive) && p.color != color)
            {
                return true;
            }
        }

        return false;
    }

    private static List<Piece> isolatedGetPiecesOnBoardGrid(List<Piece>[,] boardGrid)
    {
        List<Piece> allPieces = new List<Piece>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(i, j, boardGrid, false);
                allPieces.AddRange(pieces);
            }
        }

        return allPieces;
    }

    public static List<Piece> isolatedGetPiecesOnCoordsBoardGrid(int x, int y, List<Piece>[,] boardGrid, bool debug)
    {
        //if (debug) Debug.Log("Getting Pieces on Coords: " + (x + 1) + "," + (y + 1));
        if (x > 7 || y > 7 || x < 0 || y < 0)
        {
            return new List<Piece>();
            //return null;
        }

        List<Piece> pieces;

        pieces = boardGrid[x, y];

        return pieces;
    }

    public static (Piece piece, coords coords) getRandomBotMove_legacy(BotTemplate bot)
    {
        var botMoves = getAllPossibleBotMoves(bot, bot.currentBoardState, bot.color);

        List<PieceMoveList> allMoves = botMoves.pieceMoveList;

        System.Random rand = new System.Random();

        int dictIndex = rand.Next(allMoves.Count);

        PieceMoveList pml = allMoves[dictIndex];

        Piece randMovePiece = pml.piece;
        List<coords> randMoveCoordsList = pml.moves;

        int coordIndex = rand.Next(randMoveCoordsList.Count);
        coords randMoveCoords = randMoveCoordsList[coordIndex];

        return (randMovePiece, randMoveCoords);
    }

    public static NextMove getRandomBotMove(BotTemplate bot)
    {
        var botMoves = getAllPossibleBotMoves(bot, bot.currentBoardState, bot.color);

        List<NextMove> allNextMoves = new List<NextMove>();

        foreach (PieceMoveList pml in botMoves.pieceMoveList)
        {
            foreach (coords coords in pml.moves)
            {
                allNextMoves.Add(new NextMove(new Move(pml.piece, coords)));
            }
        }

        foreach (PieceAbility ability in botMoves.piecesAbilities)
        {
            allNextMoves.Add(new NextMove(ability));
        }

        if (allNextMoves.Count == 0)
        {
            return null;
        }

        System.Random rand = new System.Random();
        int index = rand.Next(allNextMoves.Count);

        return allNextMoves[index];
    }

    public static void movePieceBoardState(Piece piece, coords coords, BoardState boardState)
    {
        if (coords.x < 0 || coords.y < 0)
        {
            return;
        }

        coords position = new coords( piece.position.x - 1, piece.position.y - 1 );

        updateBoardState(position, piece, "r", boardState);
        updateBoardState(coords, piece, "a", boardState);
        piece.position = new coords( coords.x + 1, coords.y + 1 );
        piece.hasMoved = true;
    }

    public static coords getPiecePositionFromBoardGrid(BoardState bs, Piece targetPiece)
    {
        if (bs == null || bs.boardGrid == null || targetPiece == null)
            return new coords(-1, -1);

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece piece in bs.boardGrid[x, y])
                {
                    if (piece.name == targetPiece.name)
                    {
                        // Return as 1-based coords
                        return new coords ( x + 1, y + 1 );
                    }
                }
            }
        }

        return new coords (-1, -1);
    }

    public static void updateBoardState(coords coords, Piece piece, String action, BoardState boardState)
    {
        if (coords.x < 0 || coords.y < 0)
        {
            return;
        }

        //Debug.Log("Accessing: " + coords.x + "," + coords.y);
        var square = boardState.boardGrid[coords.x, coords.y];

        if (action.ToLower() == "a" || action.ToLower() == "add")
        {
            bool alreadyExists = square.Any(p => p.name == piece.name);
            if (!alreadyExists)
            {
                square.Add(piece);
                //Debug.LogWarning("Added " + piece.name + " to " + (coords.x - 1) + "," + (coords.y - 1));
            }

            piece.position = new coords( coords.x + 1, coords.y + 1 );
        }

        if (action.ToLower() == "r" || action.ToLower() == "remove")
        {
            //int ok = square.RemoveAll(p => p.name == piece.name);
            int removed = square.RemoveAll(p => p.name == piece.name);
            if (removed == 0)
            {
                coords realPos = getPiecePositionFromBoardGrid(boardState, piece);

                if (!(coords.x + 1 == piece.position.x && coords.y + 1 == piece.position.y))
                {
                    if (realPos.x == -1)
                    {
                        Debug.LogError($"Failed remove {piece.name} from {coords.x + 1},{coords.y + 1}. Actual pos {piece.position.x},{piece.position.y}. BoardState pos null");
                    }
                    else
                    {
                        Debug.LogError($"Failed remove {piece.name} from {coords.x + 1},{coords.y + 1}. Actual pos {piece.position.x},{piece.position.y}. BoardState pos {realPos.x},{realPos.y}");
                    }
                }
                
            }
            //Debug.LogWarning("Attempted remove of " + ok + " " + piece.name + " on " + (coords.x + 1) + "," + (coords.y + 1));
        }
    }

    public static BoardState copyBoardState(BoardState bs) {
        BoardState copy = new BoardState();

        /*
        copy.boardGrid = bs.boardGrid.Select(row =>
            row.Select(tile =>
                tile.Select(piece => HelperFunctions.clonePiece(piece)).ToList()
            ).ToList()
        ).ToList();
        */

        //copy.boardGrid = bs.boardGrid.Select(x => x.Select(y => new List<Piece>(y)).ToList()).ToList();
        copy.boardGrid = HelperFunctions.initBoardGridNew();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach(Piece piece in bs.boardGrid[x, y])
                {
                    copy.boardGrid[x, y].Add(HelperFunctions.clonePiece(piece));
                }
                
            }
        }

        copy.whitePointsOnBoard = bs.whitePointsOnBoard;
        copy.blackPointsOnBoard = bs.blackPointsOnBoard;

        copy.inCheck = new int[] { bs.inCheck[0], bs.inCheck[1] };

        return copy;
    }

    /*
    public static List<List<List<Piece>>> copyBoardGrid(List<List<List<Piece>>> bg)
    {
        List<List<List<Piece>>> copy = HelperFunctions.initBoardGrid();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece piece in bg[x][y])
                {
                    copy[x][y].Add(HelperFunctions.clonePiece(piece));
                }

            }
        }

        return copy;
    }
    */

    public static List<Piece>[,] copyBoardGrid(List<Piece>[,] bg)
    {
        List<Piece>[,] copy = HelperFunctions.initBoardGridNew();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece piece in bg[x, y])
                {
                    copy[x, y].Add(HelperFunctions.clonePiece(piece));
                }
            }
        }

        return copy;
    }

    public static List<float> getPointsOnBoardState(BoardState bs, bool isKingWorthMore) {
        List<Piece>[,] board = bs.boardGrid;
        float wCount = 0;
        float bCount = 0;

        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++) {
                foreach (Piece piece in board[x, y]) {

                    float pts = piece.points;
                    if (isKingWorthMore && piece.baseType == "King")
                    {
                        pts += 100;
                    }

                    if (piece.color == 1) {
                        wCount += pts;
                        //Debug.Log(piece.name + " found worth " + piece.points + ". Total is now " + wCount);
                    }
                    else {
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
    /*
    public static bool isolatedIsDeath(List<Piece> piecesOnCoords, Piece piece)
    {
        bool death = false;

        //Debug.Log("Pre Checking for Death");

        if (piecesOnCoords.Count != 0)
        {
            death = true;

            //Debug.Log("Checking for Death");

            if (
                !isolatedIsColorOnCoords(piecesOnCoords, true, piece.color * -1) 
                && !HelperFunctions.checkState(piece, PieceState.Murderous)
            )
            {
                death = false;
                //Debug.Log("NO death. Piece is same colour. Colour: " + piece.color);
            }
            else if (HelperFunctions.checkStateAllOnSquare(piecesOnCoords, PieceState.Dematerialized))
            {
                death = false;
                //Debug.Log("NO death. Pieces are dematerialized");
            }
            else if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords))
            {
                death = false;
                //Debug.Log("NO death. Piece is crowding");
            }
            else if (HelperFunctions.checkState(piece, PieceState.Dematerialized))
            {
                death = false;
                //Debug.Log("NO death. Piece is dematerialized");
            }
        }

        return death;
    }
    */
    public static bool isolatedIsDeath(List<Piece> piecesOnCoords, Piece piece)
    {
        //Debug.Log("Pre Checking for Death");

        if (piecesOnCoords.Count == 0)
        {
            return false;
        }

        if (
            !isolatedIsColorOnCoords(piecesOnCoords, true, piece.color * -1)
            && !HelperFunctions.checkState(piece, PieceState.Murderous)
        )
        {
            if ((HelperFunctions.checkStateOnSquare(piecesOnCoords, PieceState.Jailed) && HelperFunctions.checkStateOnSquare(piecesOnCoords, PieceState.Jailer))
                || (HelperFunctions.checkStateOnSquare(piecesOnCoords, PieceState.Jailed) && HelperFunctions.checkStateOnSquare(piecesOnCoords, PieceState.Crook)))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else if (HelperFunctions.checkStateAllOnSquare(piecesOnCoords, PieceState.Dematerialized))
        {
            return false;
        }
        else if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords))
        {
            return false;
        }
        else if (HelperFunctions.checkState(piece, PieceState.Dematerialized))
        {
            return false;
        }

        return true;
    }

    public static BoardState simulatePieceAbility(BotTemplate bot, BoardState bs_, PieceAbility pieceAbility)
    {
        // Reset positions and clone bs
        resetPiecePositions(null, bs_.boardGrid);
        BoardState bs = copyBoardState(bs_);

        //Piece piece = pieceAbility.piece;
        Piece piece = getCloneFromOriginalPiece(pieceAbility.piece, bs.boardGrid);
        PieceAbilities ability = pieceAbility.ability;
        coords coords = new coords ( pieceAbility.coords.x, pieceAbility.coords.y );
        coords adjustedCoords = new coords(coords.x - 1, coords.y - 1 );
    coords adjustedPiecePosition = new coords(piece.position.x - 1, piece.position.y - 1 );

        //List<Piece> placePieces = pieceAbility.placePieces;
        List<Piece> placePieces = new List<Piece>();
        if (pieceAbility.placePieces != null)
        {
            foreach (Piece p in pieceAbility.placePieces)
            {
                placePieces.Add(getCloneFromOriginalPiece(p, bs.boardGrid));
            }
        }
        
        List<coords> placeCoords = pieceAbility.placeCoords;
        //Piece secondPiece = pieceAbility.secondPiece;
        Piece secondPiece = getCloneFromOriginalPiece(pieceAbility.secondPiece, bs.boardGrid);

        if (ability == PieceAbilities.Vomit)
        {
            int numPieces = placePieces.Count;
            int numCoords = placeCoords.Count;

            //Debug.Log("Simulating Vomit: " + numPieces + " : " + numCoords);

            if (numPieces >= numCoords)
            {
                foreach (coords coords_ in placeCoords)
                {
                    System.Random rand = new System.Random();
                    int idx = rand.Next(numPieces);
                    numPieces--;

                    Piece p_ = placePieces[idx];
                    placePieces.Remove(p_);

                    coords coords__ = new coords( coords_.x - 1, coords_.y - 1 );

                    //Debug.LogWarning("Simulating Vomiting on adjusted cords: " + coords_.x + "," + coords_.y);

                    HelperFunctions.removeState(p_, PieceState.Jailed);

                    updateBoardState(coords__, p_, "a", bs);

                    piece.storage.Remove(p_);
                }

                foreach (Piece p_ in placePieces)
                {
                    piece.storage.Remove(p_);
                }
            }
            else
            {
                foreach (Piece p_ in placePieces)
                {
                    System.Random rand = new System.Random();
                    int idx = rand.Next(numCoords);
                    numCoords--;

                    coords c_ = placeCoords[idx];
                    placeCoords.Remove(c_);

                    c_ = new coords( c_.x - 1, c_.y - 1 );

                    //Debug.LogWarning("Simulating Vomiting on adjusted cords: " + c_.x + "," + c_.y);

                    HelperFunctions.removeState(p_, PieceState.Jailed);

                    updateBoardState(c_, p_, "a", bs);

                    piece.storage.Remove(p_);
                }
            }
        }
        else if (ability == PieceAbilities.CastleLeft)
        {
            coords kingCoords = coords;
            coords rookCoords = new coords( kingCoords.x + 1, kingCoords.y );

            coords adjustedKingCoords = new coords(kingCoords.x - 1, kingCoords.y - 1 );
            coords adjustedRookCoords = new coords(rookCoords.x - 1, rookCoords.y - 1 );

            //King
            movePieceBoardState(piece, adjustedKingCoords, bs);
            //Rook
            movePieceBoardState(secondPiece, adjustedRookCoords, bs);

            piece.hasMoved = true;
            secondPiece.hasMoved = true;

            HelperFunctions.removeAbility(piece, PieceAbilities.CastleLeft);
            HelperFunctions.removeAbility(piece, PieceAbilities.CastleRight);
        }
        else if (ability == PieceAbilities.CastleRight)
        {
            coords kingCoords = coords;
            coords rookCoords = new coords(kingCoords.x - 1, kingCoords.y );

            coords adjustedKingCoords = new coords(kingCoords.x - 1, kingCoords.y - 1 );
            coords adjustedRookCoords = new coords(rookCoords.x - 1, rookCoords.y - 1 );

            //King
            movePieceBoardState(piece, adjustedKingCoords, bs);
            //Rook
            movePieceBoardState(secondPiece, adjustedRookCoords, bs);

            piece.hasMoved = true;
            secondPiece.hasMoved = true;

            HelperFunctions.removeAbility(piece, PieceAbilities.CastleLeft);
            HelperFunctions.removeAbility(piece, PieceAbilities.CastleRight);
        }
        else if (ability == PieceAbilities.Unfreeze)
        {
            HelperFunctions.removeAbility(piece, PieceAbilities.Unfreeze);
            HelperFunctions.removeState(piece, PieceState.Frozen);
        }
        else if (ability == PieceAbilities.Freeze)
        {
            HelperFunctions.addState(secondPiece, PieceState.Frozen);
            HelperFunctions.addAbility(secondPiece, PieceAbilities.Unfreeze);
        }
        else if (ability == PieceAbilities.Spawn)
        {
            Piece spawned = HelperFunctions.Spawnables.create(piece.spawnable, piece.color, false);
            piece.numSpawns--;
            if (piece.numSpawns <= 0)
            {
                HelperFunctions.removeAbility(piece, PieceAbilities.Spawn);
            }
            Destroy(spawned.go);
            updateBoardState(adjustedCoords, spawned, "a", bs);
        }
        else if (ability == PieceAbilities.Spit)
        {
            isolatedCollateralDeath(isolatedGetPiecesOnCoordsBoardGrid(adjustedCoords.x, adjustedCoords.y, bs.boardGrid, false), bs);

            updateBoardState(adjustedCoords, secondPiece, "a", bs);

            HelperFunctions.removeState(secondPiece, PieceState.Jailed);

            piece.storage.Remove(secondPiece);
        }
        else if (ability == PieceAbilities.Dematerialize)
        {
            HelperFunctions.addState(piece, PieceState.Dematerialized);
            HelperFunctions.removeAbility(piece, PieceAbilities.Dematerialize);
            HelperFunctions.addAbility(piece, PieceAbilities.Materialize);
        }
        else if (ability == PieceAbilities.Materialize)
        {
            HelperFunctions.removeState(piece, PieceState.Dematerialized);
            HelperFunctions.addAbility(piece, PieceAbilities.Dematerialize);
            HelperFunctions.removeAbility(piece, PieceAbilities.Materialize);

            isolatedOnDeathsDontIncludeAttacker(piece, coords, bs);

            isolatedCheckPromote(piece, bs);
        }
        else if (ability == PieceAbilities.Split)
        {
            HelperFunctions.removeAbility(piece, PieceAbilities.Split);

            updateBoardState(adjustedPiecePosition, piece, "r", bs);

            Piece leftPawn = HelperFunctions.Spawnables.create("LeftPawn", piece.color, false);
            Destroy(leftPawn.go);
            updateBoardState(adjustedPiecePosition, leftPawn, "a", bs);

            Piece rightPawn = HelperFunctions.Spawnables.create("RightPawn", piece.color, false);
            Destroy(rightPawn.go);
            updateBoardState(adjustedPiecePosition, rightPawn, "a", bs);
        }

        return bs;
    }

    public static BoardState simulatePieceMove_(BoardState bs_, Piece piece, coords coords, int botColor, Piece whiteKing, Piece blackKing)
    {
        // Reset positions and clone bs
        resetPiecePositions(null, bs_.boardGrid);
        BoardState bs = copyBoardState(bs_);

        if (whiteKing == null || blackKing == null)
        {
            return null;
        }

        coords = new coords(coords.x - 1, coords.y - 1);
        //Debug.Log("Pre-Accessing: " + coords.x + "," + coords.y);
        /*
        if (coords.x < 0 || coords.x >= 8 || coords.y < 0 || coords.y >= 8)
        {
            return null;
        }
        */
        //List<Piece> piecesOnCoordsPreDeath = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, true);
        List<Piece> piecesOnCoordsPreDeath = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);
        bool death = isolatedIsDeath(piecesOnCoordsPreDeath, piece);

        if (HelperFunctions.checkState(piece, PieceState.Delayed))
        {
            death = false;
        }

        if (death)
        {
            Piece destroyer = piece;
            isolatedOnDeaths(destroyer, coords, bs);
        }

        tempInfo.attackerDied = false;
        if (bs.delayedQueue == null)
        {
            bs.delayedQueue = new DelayedQueue();
        }
        bs.delayedQueue.deIncrement();

        bool delayedMoves = true;
        while (delayedMoves)
        {
            PieceMove moveToCheck = bs.delayedQueue.Peek();
            if (moveToCheck != null && moveToCheck.turnsToRemove <= 0)
            {
                moveToCheck = bs.delayedQueue.Dequeue();

                isolatedDelayedMove(moveToCheck, bs);
            }
            else
            {
                delayedMoves = false;
            }
        }

        if (HelperFunctions.checkState(piece, PieceState.Delayed))
        {
            PieceMove delayedMove = new PieceMove(piece, coords, 2);
            bs.delayedQueue.Enqueue(delayedMove);

            return bs;
        }

        List<Piece> piecesOnSquare = isolatedGetPiecesOnCoordsBoardGrid(piece.position.x - 1, piece.position.y - 1, bs.boardGrid, false);
        foreach (Piece pieceOnSquare in piecesOnSquare)
        {
            if (HelperFunctions.checkState(pieceOnSquare, PieceState.Crook))
            {
                if (piecesOnSquare.Count == 2)
                {
                    HelperFunctions.removeState(pieceOnSquare, PieceState.Jailed);
                }
            }

            if (HelperFunctions.checkState(piece, PieceState.Jailer))
            {
                HelperFunctions.removeState(pieceOnSquare, PieceState.Jailed);
            }
        }

        coords originalCoords = new coords( piece.position.x, piece.position.y );
        if (!tempInfo.attackerDied)
        {
            movePieceBoardState(piece, coords, bs);
        }
        else
        {
            updateBoardState(coords, piece, "r", bs);
        }

        List<Piece> piecesOnSquare2 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords.x - 1, originalCoords.y - 1, bs.boardGrid, false);
        if (HelperFunctions.checkState(piece, PieceState.Piggyback))
        {
            foreach (Piece pieceOnSquare in new List<Piece>(piecesOnSquare2))
            {
                if (pieceOnSquare.color == piece.color)
                {
                    movePieceBoardState(pieceOnSquare, coords, bs);
                    pieceOnSquare.hasMoved = true;
                }
            }
        }

        List<Piece> piecesOnSquare3 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords.x - 1, originalCoords.y - 1, bs.boardGrid, false);
        foreach (Piece pieceOnSquare in new List<Piece>(piecesOnSquare3))
        {
            if (HelperFunctions.checkState(pieceOnSquare, PieceState.Jockey))
            {
                movePieceBoardState(pieceOnSquare, coords, bs);
                pieceOnSquare.hasMoved = true;
            }
        }

        isolatedCheckPromote(piece, bs);

        Piece botBlackKing = blackKing;
        Piece botWhiteKing = whiteKing;

        Piece botKing = botColor == 1 ? botWhiteKing : botBlackKing;
        coords botKingPos = new coords( botKing.position.x - 1, botKing.position.y - 1 );

        /*
        if (botColor == -1)
        {
            botWhiteKing = filterPieces("King", bot.opponentPieces).x;
            botBlackKing = bot.king;
        }
        */

        if (HelperFunctions.checkState(botKing, PieceState.Heartbroken))
        {
            if (!isolatedIsPieceTypeOnBoard("q", botColor, bs))
            {
                Piece tempKing = HelperFunctions.Spawnables.create("DepressedKing", 1, false);
                Destroy(tempKing.go);

                updateBoardState(botKingPos, tempKing, "a", bs);
                updateBoardState(botKingPos, botKing, "r", bs);
            }
        }

        // Add stacking states
        piece.states |= tempInfo.stackingStates;
        tempInfo.stackingStates = PieceState.None;

        return bs;
    }

    public static void isolatedCheckPromote(Piece piece, BoardState bs)
    {
        if (piece.promotesInto != "" && !HelperFunctions.checkState(piece, PieceState.Dematerialized))
        {
            if (piece.position.y == piece.promotingRow)
            {
                string pname = piece.promotesInto;

                if (nonResettables.ruleset == "Normal")
                {
                    pname = "Queen";
                }

                Piece p = HelperFunctions.Spawnables.create(pname, piece.color, false);
                Destroy(p.go);
                isolatedCollateralDeath(isolatedGetPiecesOnCoordsBoardGrid(piece.position.x - 1, piece.position.y - 1, bs.boardGrid, false), bs);
                updateBoardState(new coords( piece.position.x - 1, piece.position.y - 1 ), piece, "r", bs);
                updateBoardState(new coords( piece.position.x - 1, piece.position.y - 1 ), p, "a", bs);
            }
        }
    }

    public static Piece isolatedGetKing(BoardState bs_, int color)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece p in bs_.boardGrid[x,y])
                {
                    if (p.baseType == "King" && p.color == color)
                    {
                        return p;
                    }
                }
            }
        }

        return null;
    }

    public static BoardState simulatePieceMove(BotTemplate bot, BoardState bs_, Piece piece, coords coords) {
        // Reset positions and clone bs
        resetPiecePositions(null, bs_.boardGrid);
        BoardState bs = copyBoardState(bs_);

        tempInfo.stackingStates = PieceState.None;

        piece = getCloneFromOriginalPiece(piece, bs.boardGrid);

        Piece botKing = isolatedGetKing(bs, bot.color);
        Piece oppKing = isolatedGetKing(bs, bot.color * -1);

        coords = new coords ( coords.x - 1, coords.y - 1 );

        List<Piece> piecesOnCoordsPreDeath = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);
        bool death = isolatedIsDeath(piecesOnCoordsPreDeath, piece);

        if (HelperFunctions.checkState(piece, PieceState.Delayed))
        {
            death = false;
        }

        if (death)
        {
            Piece destroyer = piece;
            isolatedOnDeaths(destroyer, coords, bs);
        }

        tempInfo.attackerDied = false;
        if (bs.delayedQueue == null) {
            bs.delayedQueue = new DelayedQueue();
        }
        bs.delayedQueue.deIncrement();

        bool delayedMoves = true;
        while (delayedMoves) {
            PieceMove moveToCheck = bs.delayedQueue.Peek();
            if (moveToCheck != null && moveToCheck.turnsToRemove <= 0)
            {
                moveToCheck = bs.delayedQueue.Dequeue();

                isolatedDelayedMove(moveToCheck, bs);
            }
            else
            {
                delayedMoves = false;
            }
        }

        if (HelperFunctions.checkState(piece, PieceState.Delayed))
        {
            PieceMove delayedMove = new PieceMove(piece, coords, 2);
            bs.delayedQueue.Enqueue(delayedMove);

            return bs;
        }

        List<Piece> piecesOnSquare = isolatedGetPiecesOnCoordsBoardGrid(piece.position.x - 1, piece.position.y - 1, bs.boardGrid, false);
        foreach (Piece pieceOnSquare in piecesOnSquare)
        {
            if (HelperFunctions.checkState(pieceOnSquare, PieceState.Crook))
            {
                if (piecesOnSquare.Count == 2) 
                {
                    HelperFunctions.removeState(pieceOnSquare, PieceState.Jailed);
                }
            }

            if (HelperFunctions.checkState(piece, PieceState.Jailer))
            {
                HelperFunctions.removeState(pieceOnSquare, PieceState.Jailed);
            }
        }

        coords originalCoords = new coords( piece.position.x, piece.position.y );
        if (!tempInfo.attackerDied)
        {
            movePieceBoardState(piece, coords, bs);
        }
        else
        {
            updateBoardState(coords, piece, "r", bs);
        }

        List<Piece> piecesOnSquare2 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords.x - 1, originalCoords.y - 1, bs.boardGrid, false);
        if (HelperFunctions.checkState(piece, PieceState.Piggyback))
        {
            foreach (Piece pieceOnSquare in new List<Piece>(piecesOnSquare2))
            {
                if (pieceOnSquare.color == piece.color)
                {
                    movePieceBoardState(pieceOnSquare, coords, bs);
                    pieceOnSquare.hasMoved = true;
                }
            }
        }

        List<Piece> piecesOnSquare3 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords.x - 1, originalCoords.y - 1, bs.boardGrid, false);
        foreach (Piece pieceOnSquare in new List<Piece> (piecesOnSquare3))
        {
            if (HelperFunctions.checkState(pieceOnSquare, PieceState.Jockey))
            {
                movePieceBoardState(pieceOnSquare, coords, bs);
                pieceOnSquare.hasMoved = true;
            }
        }

        if (piece.promotesInto != "")
        {
            if (piece.position.y == piece.promotingRow)
            {
                string pname = piece.promotesInto;

                if (nonResettables.ruleset == "Normal")
                {
                    pname = "Queen";
                }

                Piece p = HelperFunctions.Spawnables.create(pname, piece.color, false);
                Destroy(p.go);
                updateBoardState(new coords( piece.position.x - 1, piece.position.y - 1 ), piece, "r", bs);
                updateBoardState(coords, p, "a", bs);
            }
        }

        // Add stacking states
        piece.states |= tempInfo.stackingStates;
        tempInfo.stackingStates = PieceState.None;

        Piece botWhiteKing = bot.color == 1 ? botKing : oppKing;
        Piece botBlackKing = bot.color == -1 ? botKing : oppKing;

        if (botWhiteKing == null || botBlackKing == null)
        {
            //Debug.Log("King is null during simulated move.");
            return bs;
        }

        coords botWhiteKingPos = new coords( botWhiteKing.position.x - 1, botWhiteKing.position.y - 1 );
        coords botBlackKingPos = new coords( botBlackKing.position.x - 1, botBlackKing.position.y - 1 );

        if (HelperFunctions.checkState(botWhiteKing, PieceState.Heartbroken))
        {
            if (!isolatedIsPieceTypeOnBoard("q", 1, bs))
            {
                Piece tempKing = HelperFunctions.Spawnables.create("DepressedKing", 1, false);
                Destroy(tempKing.go);

                updateBoardState(botWhiteKingPos, tempKing, "a", bs);
                updateBoardState(botWhiteKingPos, botWhiteKing, "r", bs);
                if (bot.color == 1) bot.king = tempKing;
            }
        }

        if (HelperFunctions.checkState(botBlackKing, PieceState.Heartbroken))
        {
            if (!isolatedIsPieceTypeOnBoard("q", -1, bs))
            {
                Piece tempKing = HelperFunctions.Spawnables.create("DepressedKing", -1, false);
                Destroy(tempKing.go);

                updateBoardState(botBlackKingPos, tempKing, "a", bs);
                updateBoardState(botBlackKingPos, botBlackKing, "r", bs);
                if (bot.color == -1) bot.king = tempKing;
            }
        }

        return bs;
    }

    public static bool isolatedIsPieceTypeOnBoard(string pieceType, int color, BoardState bs)
    {
        List<Piece> pieces = getPiecesOnBoardState(bs, color);
        foreach (Piece p in pieces)
        {
            if (p.name.Contains(pieceType))
            {
                return true;
            }
        }

        return false;
    }

    public static void isolatedDelayedMove(PieceMove pMove, BoardState bs) {
        Piece piece = getCloneFromOriginalPiece(pMove.piece, bs.boardGrid);
        coords coords = pMove.coords;

        //TODO might be a problem if delayed piece is dead

        List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);

        if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color)*/!isolatedIsColorOnCoords(piecesOnCoords, true, piece.color)) {
            bool death = false;

            if (piecesOnCoords.Count != 0) {
                death = true;

                //Debug.LogWarning("Checking for death");

                if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color * -1)*/!isolatedIsColorOnCoords(piecesOnCoords, true, piece.color * -1) && !HelperFunctions.checkState(piece, PieceState.Murderous)) {
                    death = false;
                }
                else if (HelperFunctions.checkStateAllOnSquare(piecesOnCoords, PieceState.Dematerialized)) {
                    death = false;
                }
                else if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords)) {
                    death = false;
                }
                else if (HelperFunctions.checkState(piece, PieceState.Dematerialized)) {
                    death = false;
                }
            }

            if (death) {
                isolatedOnDeaths(piece, coords, bs);
            }

            piece.hasMoved = true;
            movePieceBoardState(piece, coords, bs);
        }
    }

    public static void isolatedOnDeaths(Piece attacker, coords deadCoords, BoardState bs) {
        List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false));

        foreach (Piece piece in pieces) {
            //Debug.Log(piece.name + " died on (" + piece.position[0] + "," + piece.position.y + ") during a simulated move");
            if (!HelperFunctions.checkState(piece, PieceState.Dematerialized))
            {
                isolatedOnDeath(piece, attacker, bs);
            }
        }
    }

    public static void isolatedOnDeathsDontIncludeAttacker(Piece attacker, coords deadCoords, BoardState bs)
    {
        List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false));
        pieces.Remove(attacker);

        foreach (Piece piece in pieces)
        {
            if (!HelperFunctions.checkState(piece, PieceState.Dematerialized))
            {
                //Debug.Log(piece.name + " died on (" + piece.position[0] + "," + piece.position.y + ") during a simulated move");
                isolatedOnDeath(piece, attacker, bs);
            }
        }
    }

    public static void isolatedOnDeath(Piece deadPiece, Piece attackerPiece, BoardState bs)
    {
        coords attackerCoords = attackerPiece.position;
        coords deadPieceCoords = deadPiece.position;

        coords adjustedAttackerCoords = new coords( attackerPiece.position.x - 1, attackerPiece.position.y - 1 );
        coords adjustedDeadPieceCoords = new coords(deadPiece.position.x - 1, deadPiece.position.y - 1 );

        bool skipCollateral = false;
        bool skipInfinite = false;

        //Hungry
        if (HelperFunctions.checkState(attackerPiece, PieceState.Hungry))
        {
            skipInfinite = true;
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            attackerPiece.storage.Add(deadPiece);
            skipCollateral = true;
            isolatedRemovePiece(deadPiece, bs);

            return;
        }

        if (HelperFunctions.checkState(deadPiece, PieceState.Hungry))
        {
            if (deadPiece.storage != null && deadPiece.storage.Count > 0)
            {
                List<coords> placeCoords_ = isolatedGetCollateralSquares(deadPiece, bs);
                List<Piece> placePieces_ = deadPiece.storage;

                PieceAbility pa = new PieceAbility(deadPiece, PieceAbilities.Vomit, deadPiece.position, placePieces_, placeCoords_, null);
                //Simulate Ability
                Piece piece = getCloneFromOriginalPiece(pa.piece, bs.boardGrid);
                PieceAbilities ability = pa.ability;

                List<Piece> placePieces = new List<Piece>();
                if (pa.placePieces != null)
                {
                    foreach (Piece p in pa.placePieces)
                    {
                        placePieces.Add(getCloneFromOriginalPiece(p, bs.boardGrid));
                    }
                }

                List<coords> placeCoords = pa.placeCoords;

                if (ability == PieceAbilities.Vomit)
                {
                    int numPieces = placePieces.Count;
                    int numCoords = placeCoords.Count;

                    //Debug.Log("Simulating Vomit: " + numPieces + " : " + numCoords);

                    if (numPieces >= numCoords)
                    {
                        foreach (coords coords_ in placeCoords)
                        {
                            System.Random rand = new System.Random();
                            int idx = rand.Next(numPieces);
                            numPieces--;

                            Piece p_ = placePieces[idx];
                            placePieces.Remove(p_);

                            coords coords__ = new coords( coords_.x - 1, coords_.y - 1 );

                            //Debug.LogWarning("_ Simulating Vomiting on adjusted cords: " + coords_[0] + "," + coords_.y);

                            HelperFunctions.removeState(p_, PieceState.Jailed);
                            updateBoardState(coords__, p_, "a", bs);

                            piece.storage.Remove(p_);
                        }

                        foreach (Piece p_ in placePieces)
                        {
                            piece.storage.Remove(p_);
                        }
                    }
                    else
                    {
                        foreach (Piece p_ in placePieces)
                        {
                            System.Random rand = new System.Random();
                            int idx = rand.Next(numCoords);
                            numCoords--;

                            coords c_ = placeCoords[idx];
                            placeCoords.Remove(c_);

                            c_ = new coords( c_.x - 1, c_.y - 1 );

                            //Debug.LogWarning("_ Simulating Vomiting on adjusted cords: " + c_[0] + "," + c_.y);

                            updateBoardState(c_, p_, "a", bs);
                            HelperFunctions.removeState(p_, PieceState.Jailed);

                            piece.storage.Remove(p_);
                        }
                    }
                }
            }
        }

        //Spitting

        if (HelperFunctions.checkState(attackerPiece, PieceState.Spitting))
        {
            skipInfinite = true;
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            if (attackerPiece.storage.Count < attackerPiece.storageLimit)
            {
                attackerPiece.storage.Add(deadPiece);
                skipCollateral = true;
                isolatedRemovePiece(deadPiece, bs);

            }
            else
            {
                List<Piece> piece = HelperFunctions.pieceToList(deadPiece);
                isolatedCollateralDeath(piece, bs);
            }

            return;
        }

        if (HelperFunctions.checkState(attackerPiece, PieceState.Stacking) && deadPiece.lives == 0)
        {
            //attackerPiece.states |= deadPiece.states;
            tempInfo.stackingStates |= deadPiece.states;

            HelperFunctions.addAbility(attackerPiece, deadPiece.abilities);

            //Moves
            coords[] moves = HelperFunctions.combineMoveSets(attackerPiece.moves, deadPiece.moves);
            coords[] oneTimeMoves = HelperFunctions.combineMoveSets(attackerPiece.oneTimeMoves, deadPiece.oneTimeMoves);
            coords[] moveAndAttacks = HelperFunctions.combineMoveSets(attackerPiece.moveAndAttacks, deadPiece.moveAndAttacks);
            coords[] oneTimeMoveAndAttacks = HelperFunctions.combineMoveSets(attackerPiece.oneTimeMoveAndAttacks, deadPiece.oneTimeMoveAndAttacks);
            coords[] murderousAttacks = HelperFunctions.combineMoveSets(attackerPiece.murderousAttacks, deadPiece.murderousAttacks);
            coords[] conditionalAttacks = HelperFunctions.combineMoveSets(attackerPiece.conditionalAttacks, deadPiece.conditionalAttacks);
            coords[] attacks = HelperFunctions.combineMoveSets(attackerPiece.attacks, deadPiece.attacks);
            coords[] jumpAttacks = HelperFunctions.combineMoveSets(attackerPiece.jumpAttacks, deadPiece.jumpAttacks);
            coords[] flagMove1 = HelperFunctions.combineMoveSets(attackerPiece.flagMove1, deadPiece.flagMove1);
            coords[] flagMove2 = HelperFunctions.combineMoveSets(attackerPiece.flagMove2, deadPiece.flagMove2);
            coords[] pushMoves = HelperFunctions.combineMoveSets(attackerPiece.pushMoves, deadPiece.pushMoves);
            coords[] enPassantMoves = HelperFunctions.combineMoveSets(attackerPiece.enPassantMoves, deadPiece.enPassantMoves);

            attackerPiece.moves = moves;
            attackerPiece.oneTimeMoves = oneTimeMoves;
            attackerPiece.moveAndAttacks = moveAndAttacks;
            attackerPiece.oneTimeMoveAndAttacks = oneTimeMoveAndAttacks;
            attackerPiece.murderousAttacks = murderousAttacks;
            attackerPiece.conditionalAttacks = conditionalAttacks;
            attackerPiece.attacks = attacks;
            attackerPiece.jumpAttacks = jumpAttacks;
            attackerPiece.flagMove1 = flagMove1;
            attackerPiece.flagMove2 = flagMove2;
            attackerPiece.pushMoves = pushMoves;
            attackerPiece.enPassantMoves = enPassantMoves;
        }

        if (HelperFunctions.checkState(attackerPiece, PieceState.Jailer))
        {
            skipInfinite = true;
            HelperFunctions.addState(deadPiece, PieceState.Jailed);

            return;
        }

        if (HelperFunctions.checkState(deadPiece, PieceState.Crook) && deadPiece.color != attackerPiece.color)
        {
            HelperFunctions.addState(deadPiece, PieceState.Jailed);

            return;
        }

        if (HelperFunctions.checkState(attackerPiece, PieceState.Medusa))
        {
            skipInfinite = true;
            if (attackerPiece.numSpawns != 0)
            {
                attackerPiece.numSpawns--;

                coords pos = new coords( deadPiece.position.x - 1, deadPiece.position.y - 1 );
                updateBoardState(pos, deadPiece, "r", bs);


                Piece shieldPawn = HelperFunctions.Spawnables.create("ShieldPawn", attackerPiece.color * -1, false);
                Destroy(shieldPawn.go);
                updateBoardState(pos, shieldPawn, "a", bs);
            }
        }

        if (!skipInfinite)
        {
            //Infinite/Multi Lives
            if (deadPiece.lives != 0)
            {
                isolatedHandleMultipleLivesDeath(deadPiece, bs);

                return;
            }
        }

        if (!skipCollateral)
        {
            if (attackerPiece.collateralType == 0)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, PieceState.Defuser, bs))
                {
                    //isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs);
                    isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs);
                    //tempInfo.attackerDied = true;
                    return;
                }

                for (int i = 0; i < attackerPiece.collateral.GetLength(0); i++)
                {
                    coords coords = new coords( adjustedDeadPieceCoords.x + attackerPiece.collateral[i].x, adjustedDeadPieceCoords.y + attackerPiece.collateral[i].y );

                    if (attackerPiece.collateral[i].x == 0 && attackerPiece.collateral[i].y == 0)
                    {
                        isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs);
                        tempInfo.attackerDied = true;
                    }

                    List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false));
                    isolatedCollateralDeath(pieces, bs);
                }
            }

            if (deadPiece.collateralType == 1)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, PieceState.Defuser, bs))
                {
                    //isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs);
                    isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs);
                    //tempInfo.attackerDied = true;
                    return;
                }

                for (int i = 0; i < deadPiece.collateral.GetLength(0); i++)
                {
                    coords coords = new coords( adjustedDeadPieceCoords.x + deadPiece.collateral[i].x, adjustedDeadPieceCoords.y + deadPiece.collateral[i].y );

                    if (deadPiece.collateral[i].x == 0 && deadPiece.collateral[i].y == 0)
                    {
                        isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs);
                        isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs);
                        tempInfo.attackerDied = true;
                    }

                    List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false));
                    isolatedCollateralDeath(pieces, bs);
                }
            }
        }

        deadPiece.alive = 0;
        //Debug.Log("DEAD DEAD DEAD");
        updateBoardState(new coords( adjustedDeadPieceCoords.x, adjustedDeadPieceCoords.y ), deadPiece, "r", bs);
    }

    /*
    public static bool isolatedIsPieceSurroundingState(Piece piece, string state, BoardState bs)
    {
        foreach (var (dirX, dirY) in globalDefs.globalDirections)
        {
            int x = piece.position[0] + dirX;
            int y = piece.position.y + dirY;

            List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid, false);

            foreach (Piece p in pieces)
            {
                if (HelperFunctions.checkState(p, state))
                {
                    return true;
                }
            }
        }

        return false;
    }
    */

    public static bool isolatedIsPieceSurroundingState(Piece piece, PieceState state, BoardState bs)
    {
        foreach (var (dirX, dirY) in globalDefs.globalDirections)
        {
            int x = piece.position.x + dirX;
            int y = piece.position.y + dirY;

            List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid, false);

            if (pieces == null) continue;

            foreach (Piece p in pieces)
            {
                if ((p.states & state) != 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool isolatedIsPieceSurroundingColor(Piece piece, int color, BoardState bs)
    {
        foreach (var (dirX, dirY) in globalDefs.globalDirections)
        {
            int x = piece.position.x + dirX;
            int y = piece.position.y + dirY;

            List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid, false);

            foreach (Piece p in pieces)
            {
                if (p.color == color)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static void isolatedCollateralDeath(List<Piece> deadPieces, BoardState bs) {
        foreach (Piece deadPiece in new List<Piece>(deadPieces))
        {
            if (HelperFunctions.checkState(deadPiece, PieceState.Shield) || HelperFunctions.checkCaptureTheFlag(deadPiece))
            {
                continue;
            }

            //Debug.Log(deadPiece.name + " died to collateral on (" + deadPiece.position.x + "," + deadPiece.position.y + ") during simulated move");
            if (deadPiece.lives != 0)
            {
                isolatedHandleMultipleLivesDeath(deadPiece, bs);

                continue;
            }
            else
            {
                updateBoardState(new coords( deadPiece.position.x - 1, deadPiece.position.y - 1 ), deadPiece, "r", bs);
                deadPiece.alive = 0;
            }
        }
    }

    public static void isolatedRemovePiece(Piece p, BoardState bs) {
        updateBoardState(new coords( p.position.x - 1, p.position.y - 1 ), p,"r", bs);
    }

    public static void isolatedHandleMultipleLivesDeath(Piece deadPiece, BoardState bs) {
        deadPiece.lives--;

        if (!HelperFunctions.isOnStartSquare(deadPiece) && !isolatedIsPieceOnStartSquare(deadPiece, bs)) {
            movePieceBoardState(deadPiece, new coords( deadPiece.startSquare.x - 1, deadPiece.startSquare.y - 1 ), bs);
        }
        else {
            updateBoardState(new coords( deadPiece.position.x - 1, deadPiece.position.y - 1 ), deadPiece, "r", bs);
        }
    }

    //Probably dont need
    public static bool isolatedIsOnStartSquare(Piece p, BoardState bs) {
        return false;
    }

    public static bool isolatedIsPieceOnStartSquare(Piece p, BoardState bs) {
        return isolatedGetPiecesOnCoordsBoardGrid(p.startSquare.x, p.startSquare.y, bs.boardGrid, false).Count != 0;
    }

    public static void resetPiecePositions(BoardState bs, List<Piece>[,] boardGrid)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                List<Piece> sourceSquare = boardGrid[x, y];

                foreach (Piece piece in sourceSquare)
                {
                    //Debug.LogWarning(piece.name + " reset to pos " + (x + 1) + "," + (y + 1));
                    piece.position = new coords( x + 1, y + 1 );
                }

                if (bs != null)
                {
                    foreach(Piece piece_ in bs.boardGrid[x, y])
                    {
                        piece_.position = new coords( x + 1, y + 1 );
                    }
                }
            }
        }
    }

    public static void debug_printBoardState(BoardState bs)
    {
        StringBuilder sb = new StringBuilder();

        List<string> names = new List<string>();
        bool duplicateName = false;

        sb.AppendLine("Pieces on Board State (" + (getPiecesOnBoardState(bs, 1).Count + getPiecesOnBoardState(bs, -1).Count) + ")");

        float w = 0;
        float b = 0;

        List<Piece>[,] boardGrid = bs.boardGrid;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach(Piece p in boardGrid[x, y])
                {
                    if (names.Contains(p.name))
                    {
                        duplicateName = true;
                    }
                    else
                    {
                        names.Add(p.name);
                    }
                    
                    sb.AppendLine(p.name + " found on " + (x + 1) + "," + (y + 1) + " worth " + p.points + ". Position: (" + p.position.x + "," + p.position.y + ")");
                    if (p.color == 1)
                    {
                        w += p.points;
                    }
                    else
                    {
                        b += p.points;
                    }
                }
            }
        }



        sb.AppendLine("White Total: " + w + "Black Total: " + b);

        Debug.LogWarning(sb.ToString());

        if (duplicateName)
        {
            Debug.LogError("Duplicate Name Found");
            Debug.Break();
        }
    }

    public static StringBuilder debug_printBoardGrid(List<List<List<Piece>>> bg, bool print, bool useType)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Pieces on Board Grid");
        List<List<List<Piece>>> boardGrid = bg;


        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece p in boardGrid[x][y])
                {
                    if (useType)
                    {
                        sb.AppendLine(p.GetType().Name + " (" + p.color + ")" + " found on " + (x + 1) + "," + (y + 1) + " worth " + p.points);
                    }
                    else
                    {
                        sb.AppendLine(p.name + " found on " + (x + 1) + "," + (y + 1) + " worth " + p.points);
                    }
                }
            }
        }

        if (print) Debug.LogWarning(sb.ToString());
        return sb;
    }

    public static Piece findPieceOnOtherBoardState(BoardState bs, string pName)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach(Piece p in bs.boardGrid[x, y])
                {
                    if (p.name == pName)
                    {
                        return p;
                    }
                }
            }
        }

        return null;
    }

    public static Piece getOriginalPieceFromClone(Piece p) {
        if (p == null) {
            return null;
        }

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece ogPiece in gameData.boardGrid[x][y]) {
                    if (ogPiece.name == p.name) {
                        return ogPiece;
                    }
                }
            }

        }

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece ogPiece in gameData.boardGrid[x][y])
                {
                    if (ogPiece.storage == null)
                    {
                        continue;
                    }

                    foreach(Piece storedPiece in ogPiece.storage)
                    {
                        if (storedPiece.name == p.name)
                        {
                            return storedPiece;
                        }
                    }
                }
            }

        }

        return null;
    }

    public static Piece getCloneFromOriginalPiece(Piece p, List<Piece>[,] boardGrid)
    {
        if (p == null)
        {
            return null;
        }

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece ogPiece in boardGrid[x, y])
                {
                    if (ogPiece.name == p.name)
                    {
                        return ogPiece;
                    }
                }
            }

        }

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece ogPiece in boardGrid[x, y])
                {
                    if (ogPiece.storage == null)
                    {
                        continue;
                    }

                    foreach (Piece storedPiece in ogPiece.storage)
                    {
                        if (storedPiece.name == p.name)
                        {
                            return storedPiece;
                        }
                    }
                }
            }

        }

        return null;
    }

    public static List<Piece>[,] convertBoardGrid(List<List<List<Piece>>> oldGrid)
    {
        List<Piece>[,] newGrid = new List<Piece>[8, 8];

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                newGrid[x, y] = new List<Piece>();

                foreach (Piece piece in oldGrid[x][y])
                {
                    newGrid[x, y].Add(piece);
                }
            }
        }

        return newGrid;
    }

    public static List<List<List<Piece>>> revertBoardGrid(List<Piece>[,] oldGrid)
    {
        List<List<List<Piece>>> newGrid = HelperFunctions.initBoardGrid();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {

                foreach (Piece piece in oldGrid[x, y])
                {
                    newGrid[x][y].Add(piece);
                }
            }
        }

        return newGrid;
    }

    public static (Piece piece, coords coords, string moveType) getNextMoveVars(NextMove nextMove)
    {
        if (nextMove == null)
        {
            return (null, new coords(-1, -1), "");
        }

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

        return (piece, coords, moveType);
    }

    public static void debug_printMoveOrder(List<(NextMove move, float score)> orderedMoves, NextMove nm)
    {
        var nextMoveOppVars = getNextMoveVars(nm);

        Piece p = nextMoveOppVars.piece;
        coords c = nextMoveOppVars.coords;

        string debugText = "Top 5 Opponent Moves after " + p + " to " + c.x + "," + c.y + ":\n";

        for (int i = 0; i < orderedMoves.Count; i++)
        {
            var moveData = orderedMoves[i];

            var vars = getNextMoveVars(moveData.move);

            Piece piece = vars.piece;
            coords target = vars.coords;
            string moveType = vars.moveType;

            string pieceName = piece != null ? piece.name : "NULL";

            debugText +=
                $"#{i + 1} | " +
                $"Piece: {pieceName} | " +
                $"Type: {moveType} | " +
                $"Target: ({target.x}, {target.y}) | " +
                $"Score: {moveData.score}\n";
        }

        Debug.Log(debugText);
    }
}
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

public class BotHelperFunctions : MonoBehaviour
{
	public static List<Piece> getPiecesTypeRandom(string type, int color) {
		List<string> pieces = getAllTypePieces(type, color);

		List<Piece> selected = new List<Piece>();

		int count = 2;

		if (type == "Pawn") {
			count = 8;
		}
		else if (type == "Queen" || type == "King") {
			count = 1;
		}

		for (int i = 0; i < count; i++)
		{
            System.Random rand = new System.Random();
		    int index = rand.Next(pieces.Count);

            Type type_ = Type.GetType(pieces[index] + ", Assembly-CSharp");

            if (type_ == null)
            {
                i -= 1;
                continue;
            }

            Piece piece = (Piece)Activator.CreateInstance(type_, color, false);

            selected.Add(piece);
		    pieces.RemoveAt(index);
		}

		return selected;
	}

	private static List<string> getAllTypePieces(string type, int color) {

    	List<Type> allPieces = Lootbox.GetAllPieces();
    	List<string> eligiblePieces = new List<string>();

    	foreach (var piece_ in allPieces) {

            Piece piece = (Piece)Activator.CreateInstance(piece_, color, false);
            if (piece.baseType == type) {
    			eligiblePieces.Add(piece.name);
    		}

            if (piece.go != null)
            {
                Destroy(piece.go);
            }
        }

    	return eligiblePieces;
    }

    public static List<int[]> isolatedGetCollateralSquares(Piece p, BoardState bs) {
        List<int[]> possibleCoords = new List<int[]>();
        int[,] collateral = new int[,]
        {
            {  1,  1 },
            {  1,  0 },
            {  1, -1 },
            {  0, -1 },
            { -1, -1 },
            { -1,  0 },
            { -1,  1 },
            {  0,  1 }
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
            int xOffset = collateral[i, 0];
            int yOffset = collateral[i, 1];

            int[] coords = new int[]
            {
                p.position[0] + xOffset,
                p.position[1] + yOffset
            };

            if (isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, false).Count > 0)
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

    public class PieceAbility {
        public Piece piece;
        public string ability;
        public int[] coords;
        public List<Piece> placePieces;
        public List<int[]> placeCoords;

        public PieceAbility(Piece piece, string ability, int[] coords, List<Piece> placePieces, List<int[]> placeCoords) {
            this.piece = piece;
            this.ability = ability;
            this.coords = new int[] { coords[0], coords[1] };
            this.placePieces = placePieces;
            this.placeCoords = placeCoords;
        }
    }

    public static List<PieceAbility> getAllPossibleBotAbilities(BotTemplate bot, BoardState bs, int color) {
        List<Piece> pieces = color == 1 ? bs.whitePieces : bs.blackPieces;
        List<PieceAbility> pieceAbilities = new List<PieceAbility>();

        foreach(Piece piece in pieces) {
            string[] abilityNames = piece.ability.Split("-");

            foreach (string ability in abilityNames) {
                if (ability == "Vomit") {
                    //TODO right now all storage is null
                    if (piece.storage != null && piece.storage.Count < 1) {
                        continue;
                    }
                    else if (piece.storage == null) {
                        continue;
                    }

                    List<Piece> placePieces = new List<Piece>();

                    foreach (Piece storedPiece in piece.storage) {
                        placePieces.Add(storedPiece);
                    }

                    List<int[]> possibleCoords = isolatedGetCollateralSquares(piece, bs);

                    PieceAbility vomit = new PieceAbility(piece, "Vomit", null, placePieces, possibleCoords);
                    pieceAbilities.Add(vomit);
                }
                //TODO
            }
        }

        return pieceAbilities;
    }

    //List so its easier to randomize. Each Dict has only one entry
    public static (List<Dictionary<Piece, List<int[]>>> pieceMoveList, Dictionary<Piece, List<string>> piecesAbilities) getAllPossibleBotMoves(BotTemplate bot, BoardState bs, int color) {
    	List<Dictionary<Piece, List<int[]>>> totalMoves = new List<Dictionary<Piece, List<int[]>>>();

        List<Piece> pieces_ = getPiecesOnBoardState(bs, color);
        foreach (Piece piece in pieces_) {

            //TODO this can return null after capture maybe only after collat
    		List<int[]> moves = getIsolatedStatePieceMoves(piece, bs);

            if (moves.Count > 0) {
    			Dictionary<Piece, List<int[]>> pMoveDict = new Dictionary<Piece, List<int[]>>();

	    		pMoveDict.Add(piece, moves);

	    		totalMoves.Add(pMoveDict);
    		}
        }

        return (totalMoves, HelperFunctions.getAllEligibleAbilities(color));
    }

    public static List<Piece> getPiecesOnBoardState(BoardState bs, int color)
    {
        List<Piece> pieces = new List<Piece>();
        List<List<List<Piece>>> boardGrid = bs.boardGrid;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece p in boardGrid[x][y])
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

    public static (List<Dictionary<Piece, List<int[]>>> pieceMoveList, Dictionary<Piece, List<string>> piecesAbilities) getAllPossibleBotMovesNew(BotTemplate bot, int color)
    {
        List<Dictionary<Piece, List<int[]>>> totalMoves = new List<Dictionary<Piece, List<int[]>>>();



        return (null, null);
    }

    public static List<int[]> getIsolatedStatePieceMoves(Piece piece, BoardState bs)
    {
        List<int[]> allMoves = new List<int[]>();

        List<List<List<Piece>>> boardGrid = bs.boardGrid;
        bool check = false;
        //if is check TODO check = true

        //todo maybe forcestayturn

        isolatedIterateThroughPieceMoves(HelperFunctions.moveComparator, piece, bs, piece.moves, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.moveAndAttacks, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.attacksComparator, piece, bs, piece.attacks, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.oneTimeMovesComparator, piece, bs, piece.oneTimeMoves, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.oneTimeMoveAndAttacksComparator, piece, bs, piece.oneTimeMoveAndAttacks, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.murderousAttacksComparator, piece, bs, piece.murderousAttacks, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.conditionalAttacksComparator, piece, bs, piece.conditionalAttacks, check, allMoves);
        isolatedIterateThroughPieceMoves(HelperFunctions.jumpAttacksComparator, piece, bs, piece.jumpAttacks, check, allMoves);

        //TODO fix dependentMoves for isolated state
        piece.dependentMovesSet();
        isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.dependentAttacks, check, allMoves);

        piece.interactiveMovesSet();
        isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.interactiveAttacks, check, allMoves);

        HelperFunctions.updatePieceFlags(piece, check);
        if (piece.flag == 1)
        {
            isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.flagMove1, check, allMoves);
        }
        else if (piece.flag == 2)
        {
            isolatedIterateThroughPieceMoves(HelperFunctions.moveAndAttacksComparator, piece, bs, piece.flagMove2, check, allMoves);
        }

        return allMoves;
    }

    private static void isolatedIterateThroughPieceMoves(Func<Piece, bool, bool, bool, List<Piece>, bool> comparator, Piece piece, BoardState bs, int[,] moveType, bool check, List<int[]> allMoves)
    {
        if (HelperFunctions.checkState(piece, "Frozen") || HelperFunctions.checkState(piece, "Jailed"))
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

        bool isPortal = HelperFunctions.checkState(piece, "Portal");
        bool isBouncing = HelperFunctions.checkState(piece, "Bouncing");

        for (int i = 0; i < moveType.GetLength(0); i++)
        {
            //Portal
            //int[] oldCoords = new int[] { moveType[i, 0] + piece.position[0], moveType[i, 1] + piece.position[1] };
            int oldCoordsX, oldCoordsY;
            oldCoordsX = moveType[i, 0] + piece.position[0];
            oldCoordsY = moveType[i, 1] + piece.position[1];
            
            //int[] newPos = new int[] { oldCoords[0], oldCoords[1] };
            int newPosX = oldCoordsX;
            int newPosY = oldCoordsY;

            if (isPortal)
            {
                int[] coordsP = HelperFunctions.adjustCoordsForPortal(piece, oldCoordsX, oldCoordsY);
                //newPos[0] = coordsP[0];
                //newPos[1] = coordsP[1];
                newPosX = coordsP[0];
                newPosY = coordsP[1];
                //newPos = HelperFunctions.adjustCoordsForPortal(piece, oldCoords[0], oldCoords[1]);
            }
            else if (isBouncing)
            {
                int[] coordsB = HelperFunctions.adjustCoordsForBouncing(piece, oldCoordsX, oldCoordsY);
                //newPos[0] = coordsB[0];
                //newPos[1] = coordsB[1];
                newPosX = coordsB[0];
                newPosY = coordsB[1];
                //newPos = HelperFunctions.adjustCoordsForBouncing(piece, oldCoords[0], oldCoords[1]);
            }

            if (newPosX > 8 || newPosY > 8 || newPosX <= 0 || newPosY <= 0)
            {
                continue;
            }

            List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(newPosX - 1, newPosY - 1, bs.boardGrid, false);
            bool pieceIsNull = piecesOnCoords == null || piecesOnCoords.Count == 0;
            bool pieceIsDiffColour = false;

            if (!pieceIsNull)
            {
                //pieceIsDiffColour = !isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color);
                pieceIsDiffColour = !isolatedIsColorOnCoords(piecesOnCoords, true, piece.color)

                if (HelperFunctions.checkPiecesDisabled(piecesOnCoords))
                {
                    pieceIsNull = true;
                }

                //checkSquareCrowdingEligible
                if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords)) {
                    pieceIsNull = true;
                }

                //Check for states
                if (HelperFunctions.checkStateOnSquare(piecesOnCoords, "Shield")) {
                    continue;
                }

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
            }

            bool jump;
            if (isPortal && !((oldCoordsX == newPosX) && (oldCoordsY == newPosY)))
            {
                if (HelperFunctions.isKnightPortalBackRank_(piece, oldCoordsX, oldCoordsY, newPosX, newPosY))
                {
                    continue;
                }

                jump = isolatedIsJumpPortal(piece, piece.position, newPosX, newPosY, bs);
                //jump = HelperFunctions.isJumpPortal(piece, piece.position, newPos);
            }
            else if (isBouncing && !((oldCoordsX == newPosX) && (oldCoordsY == newPosY)))
            {
                jump = isolatedIsJumpBouncing(piece, piece.position, newPosX, newPosY, bs);
                //jump = HelperFunctions.isJumpBouncing(piece, piece.position, newPos);
            }
            else
            {
                jump = isolatedIsJump(piece, piece.position, newPosX, newPosY, bs);
                //jump = HelperFunctions.isJump(piece, piece.position, newPos);
            }

            if (comparator(piece, jump, pieceIsNull, pieceIsDiffColour, piecesOnCoords))
            {
                //TODO maybe add check functionality
                allMoves.Add(new int[] { newPosX, newPosY });
            }
        }
    }

    public static bool isolatedIsJump(Piece piece, int[] from, int toX, int toY, BoardState bs) {
       int dirX, dirY;

        if (from[0] > toX)
        {
            dirX = -1;
        }
        else if (from[0] == toX)
        {
            dirX = 0;
        }
        else
        {
            dirX = 1;
        }

        if (from[1] > toY)
        {
            dirY = -1;
        }
        else if (from[1] == toY)
        {
            dirY = 0;
        }
        else
        {
            dirY = 1;
        }

        int diff = Mathf.Abs(from[0] - toX);
        if (Mathf.Abs(from[1] - toY) > diff)
        {
            diff = Mathf.Abs(from[1] - toY);
        }

        bool isGhost = HelperFunctions.checkState(piece, "Ghost");
        int enemyColor = piece.color * -1;

        for (int i = 1; i <= diff - 1; i++)
        {
            int x = from[0] + (i * dirX);
            int y = from[1] + (i * dirY);

            List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(x - 1, y - 1, bs.boardGrid, false);

            foreach (Piece p in piecesOnCoords)
            {
                if (HelperFunctions.checkState(p, "Ghoul") && p.color == piece.color)
                {
                    // Your Ghoul
                    continue;
                }

                if (HelperFunctions.checkState(p, "Dematerialized") && p.color == piece.color)
                {
                    // Your Dematerialized
                    continue;
                }

                if (isGhost && p.color != piece.color)
                {
                    // Your piece is a ghost, your piece
                    continue;
                }

                //Debug.Log("MOVE FROM " + piece.position[0] + "," + piece.position[1] + " to " + x + "," + y + " is a JUMP");
                return true;
            }
        }
        return false;
    }

    public static bool isolatedIsJumpBouncing(Piece piece, int[] from, int toX, int toY, BoardState bs)
    {
        int fromX = from[0];
        int fromY = from[1];

        foreach (var (dx, dy) in globalDiagionalDirectionsNoZero)
        {
            int x = fromX;
            int y = fromY;

            for (int i = 0; i < 14; i++)
            {
                x += dx;
                y += dy;

                int[] newCoords = HelperFunctions.adjustCoordsForBouncing(piece, x, y);
                int newX = newCoords[0];
                int newY = newCoords[1];

                if (newX == toX && newY == toY) {
                    return false;
                }

                List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(newX, newY, bs.boardGrid, false);

                if (piecesOnCoords.Count > 0) {
                    break;
                }
            }
        }

        return true;
    }

    /*
    public static bool isolatedIsJumpBouncing(Piece piece, int[] from, int[] to, BoardState bs)
    {
        int fromX = from[0];
        int fromY = from[1];
        int toX = to[0];
        int toY = to[1];

        int[] coords = { fromX, fromY };

        int[,] directions = new int[,]
        {
            { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }
        };

        for (int j = 0; j < 4; j++)
        {
            coords[0] = fromX;
            coords[1] = fromY;
            for (int i = 0; i < 14; i++)
            {
                coords[0] = coords[0] + directions[j, 0];
                coords[1] = coords[1] + directions[j, 1];
                int[] newCoords = HelperFunctions.adjustCoordsForBouncing(piece, coords[0], coords[1]);

                List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(newCoords[0], newCoords[1], bs.boardGrid, false);

                if (newCoords[0] == toX && newCoords[1] == toY)
                {
                    return false;
                }

                if (piecesOnCoords.Count > 0)
                {
                    break;
                }
            }
        }

        return true;
    }
    */

    public static bool isolatedIsJumpPortal(Piece piece, int[] from, int toX, int toY, BoardState bs) {
        int fromX = from[0];
        int fromY = from[1];

        /*
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },  // right
            new int[] {-1, 0 },  // left
            new int[] { 0, 1 },  // up
            new int[] { 0, -1 }, // down
            new int[] { 1, 1 },  // up-right
            new int[] {-1, 1 },  // up-left
            new int[] { 1, -1 }, // down-right
            new int[] {-1, -1 }  // down-left
        };
        */

        //bool anyPathFound = false;
        foreach (var (dx, dy) in globalDirectionsNoZero)
        {
            int x = fromX;
            int y = fromY;
            //bool crossedBackRank = false;
            //bool jumpedPiece = false;

            for (int step = 0; step < 8; step++)
            {
                //x += dir[0];
                //y += dir[1];
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
                        return true;
                    //}
                }

                //List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid, false);
                if (bs.boardGrid[x - 1][y - 1].Count > 0)
                {
                    //jumpedPiece = true;
                    break;
                }
            }
        }

        return true;
    }

    private static bool isolatedCheckSquareCrowdingEligible(Piece piece, List<Piece> piecesOnCoords) {
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
        if (piecesOnCoords.Count > 1 && HelperFunctions.checkState(piece, "Crowding"))
        {
            foreach (Piece _piece in piecesOnCoords)
            {
                if (!HelperFunctions.checkState(_piece, "Crowding"))
                {
                    return false;
                }
            }

            // If they are all crowding
            return true;
        }

        int piecesOnCoordsCount = piecesOnCoords.Count;
        bool sameColorOnCoords = isolatedIsColorOnCoords(piecesOnCoords, true, piece.color)

        //There is one piece on the square, piece is crowding
        if (HelperFunctions.checkState(piece, "Crowding") && piecesOnCoordsCount == 1 && sameColorOnCoords) {
            return true;
        }

        //There is one piece on the square, piece is piggyback
        if (piecesOnCoordsCount == 1 && HelperFunctions.checkState(piecesOnCoords[0], "Piggyback") && sameColorOnCoords) {
            return true;
        }

        //There is one piece on square, piece is jockey
        if (piecesOnCoordsCount == 1 && HelperFunctions.checkState(piece, "Jockey") && sameColorOnCoords) {
            return true;
        }

        if (!HelperFunctions.checkState(piece, "Crowding"))
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
            if (/*piece.disabled || piece.alive == 0 || */(ignoreDematerialized && HelperFunctions.checkState(piece, "Dematerialized")) || HelperFunctions.checkState(piece, "Jailed"))
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
            if (/*piece.disabled || piece.alive == 0 || */(ignoreDematerialized && HelperFunctions.checkState(piece, "Dematerialized")) || HelperFunctions.checkState(piece, "Jailed"))
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
        List<List<List<Piece>>> boardGrid = bs.boardGrid;

        List<Piece> pieces = isolatedGetPiecesOnBoardGrid(boardGrid);
        foreach (Piece p in pieces)
        {
            if (HelperFunctions.checkState(p, "Oppressive") && p.color != color)
            {
                return true;
            }
        }

        return false;
    }

    private static List<Piece> isolatedGetPiecesOnBoardGrid(List<List<List<Piece>>> boardGrid)
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

    public static List<Piece> isolatedGetPiecesOnCoordsBoardGrid(int x, int y, List<List<List<Piece>>> boardGrid, bool debug)
    {
        //if (debug) Debug.Log("Getting Pieces on Coords: " + (x + 1) + "," + (y + 1));
        if (x > 7 || y > 7 || x < 0 || y < 0)
        {
            //return new List<Piece>();
            return null;
        }

        List<Piece> pieces;
        
        pieces = boardGrid[x][y];

        return pieces;
    }

    public static (Piece piece, int[] coords) getRandomBotMove(BotTemplate bot)
    {
        var botMoves = getAllPossibleBotMoves(bot, bot.currentBoardState, bot.color);

        List<Dictionary<Piece, List<int[]>>> allMoves = botMoves.pieceMoveList;
        //TODO add abilities
        Dictionary<Piece, List<string>> allAbilities = botMoves.piecesAbilities;

        System.Random rand = new System.Random();

        int dictIndex = rand.Next(allMoves.Count);

        Dictionary<Piece, List<int[]>> pieceMovesDict = allMoves[dictIndex];
        KeyValuePair<Piece, List<int[]>> pieceMovesKeyVal = pieceMovesDict.First();

        Piece randMovePiece = pieceMovesKeyVal.Key;
        List<int[]> randMoveCoordsList = pieceMovesKeyVal.Value;

        int coordIndex = rand.Next(randMoveCoordsList.Count);
        int[] randMoveCoords = randMoveCoordsList[coordIndex];

        return (randMovePiece, randMoveCoords);
    }

    //TODO run movePiece on boardstate without actual move
    public static void movePieceBoardState(Piece piece, int[] coords, BoardState boardState)
    {
        if (coords[0] < 0 || coords[1] < 0)
        {
            return;
        }

        // int[] position = boardState.getPiecePosition(piece);
        // if (position == null) return;

        int[] position = new int[] { piece.position[0] - 1, piece.position[1] - 1 };

        updateBoardState(position, piece, "r", boardState);
        updateBoardState(coords, piece, "a", boardState);
        piece.position = coords;
        piece.hasMoved = true;
    }

    public static void updateBoardState(int[] coords, Piece piece, String action, BoardState boardState)
    {
        if (coords[0] < 0 || coords[1] < 0)
        {
            return;
        }

        //Debug.Log("Accessing: " + coords[0] + "," + coords[1]);
        var square = boardState.boardGrid[coords[0]][coords[1]];

        if (action.ToLower() == "a" || action.ToLower() == "add")
        {
            bool alreadyExists = square.Any(p => p.name == piece.name);
            if (!alreadyExists)
            {
                square.Add(piece);
                //Debug.LogWarning("Added " + piece.name + " to " + (coords[0] - 1) + "," + (coords[1] - 1));
            }
        }

        if (action.ToLower() == "r" || action.ToLower() == "remove")
        {
            int ok = square.RemoveAll(p => p.name == piece.name);
            //Debug.LogWarning("Removed " + ok + " " + piece.name);
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
        copy.boardGrid = HelperFunctions.initBoardGrid();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach(Piece piece in bs.boardGrid[x][y])
                {
                    copy.boardGrid[x][y].Add(HelperFunctions.clonePiece(piece));
                }
                
            }
        }

        copy.whitePointsOnBoard = bs.whitePointsOnBoard;
        copy.blackPointsOnBoard = bs.blackPointsOnBoard;

        copy.inCheck = new int[] { bs.inCheck[0], bs.inCheck[1] };

        return copy;
    }

    public static List<float> getPointsOnBoardState(BoardState bs) {
        List<List<List<Piece>>> board = bs.boardGrid;
        float wCount = 0;
        float bCount = 0;

        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++) {
                foreach (Piece piece in board[x][y]) {
                    if (piece.color == 1) {
                        wCount += piece.points;
                        //Debug.Log(piece.name + " found worth " + piece.points + ". Total is now " + wCount);
                    }
                    else {
                        bCount += piece.points;
                    }
                }
            }
        }

        List<float> l = new List<float>();
        l.Add(wCount);
        l.Add(bCount);

        return l;
    }

    public static bool isolatedIsDeath(List<Piece> piecesOnCoords, Piece piece)
    {
        bool death = false;

        //Debug.Log("Pre Checking for Death");

        if (piecesOnCoords.Count != 0)
        {
            death = true;

            //Debug.Log("Checking for Death");

            if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color * -1)*/ !isolatedIsColorOnCoords(piecesOnCoords, true, piece.color * -1) && !HelperFunctions.checkState(piece, "Murderous"))
            {
                death = false;
                //Debug.Log("NO death. Piece is same colour. Colour: " + piece.color);
            }
            else if (HelperFunctions.checkStateAllOnSquare(piecesOnCoords, "Dematerialized"))
            {
                death = false;
                //Debug.Log("NO death. Pieces are dematerialized");
            }
            else if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords))
            {
                death = false;
                //Debug.Log("NO death. Piece is crowding");
            }
            else if (HelperFunctions.checkState(piece, "Dematerialized"))
            {
                death = false;
                //Debug.Log("NO death. Piece is dematerialized");
            }
        }

        return death;
    }

    public static void simulatePieceMove(BotTemplate bot, BoardState bs, Piece piece, int[] coords) {

        ///////TODO PIECE PRE MOVE

        coords = new int[] { coords[0] - 1, coords[1] - 1 };
        //Debug.Log("Pre-Accessing: " + coords[0] + "," + coords[1]);

        if (coords[0] < 0 || coords[0] >= 8 || coords[1] < 0 || coords[1] >= 8)
        {
            return;
        }

        //List<Piece> piecesOnCoordsPreDeath = isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, true);
        List<Piece> piecesOnCoordsPreDeath = isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, false);
        bool death = isolatedIsDeath(piecesOnCoordsPreDeath, piece);

        if (death)
        {
            Piece destroyer = piece;
            isolatedOnDeaths(destroyer, coords, bs);
        }

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

        if (HelperFunctions.checkState(piece, "Delayed"))
        {
            PieceMove delayedMove = new PieceMove(piece, coords, 2);
            bs.delayedQueue.Enqueue(delayedMove);

            return;
        }

        List<Piece> piecesOnSquare = isolatedGetPiecesOnCoordsBoardGrid(piece.position[0], piece.position[1], bs.boardGrid, false);
        foreach (Piece pieceOnSquare in piecesOnSquare)
        {
            if (HelperFunctions.checkState(pieceOnSquare, "Crook"))
            {
                if (piecesOnSquare.Count == 2)
                {
                    HelperFunctions.removeState(pieceOnSquare, "Jailed");
                }
            }

            if (HelperFunctions.checkState(piece, "Jailer"))
            {
                HelperFunctions.removeState(pieceOnSquare, "Jailed");
            }
        }

        int[] originalCoords = { piece.position[0], piece.position[1] };
        movePieceBoardState(piece, coords, bs);

        List<Piece> piecesOnSquare2 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords[0], originalCoords[1], bs.boardGrid, false);
        if (HelperFunctions.checkState(piece, "Piggyback"))
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

        List<Piece> piecesOnSquare3 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords[0], originalCoords[1], bs.boardGrid, false);
        foreach (Piece pieceOnSquare in new List<Piece> (piecesOnSquare3))
        {
            if (HelperFunctions.checkState(pieceOnSquare, "Jockey"))
            {
                movePieceBoardState(pieceOnSquare, coords, bs);
                pieceOnSquare.hasMoved = true;
            }
        }

        if (piece.promotesInto != "")
        {
            if (piece.position[1] == piece.promotingRow)
            {
                string pname = piece.promotesInto;
                Piece p = HelperFunctions.Spawnables.create(pname, piece.color);
                Destroy(p.go);
                updateBoardState(new int[] { piece.position[0] - 1, piece.position[1] - 1 }, piece, "r", bs);
                updateBoardState(coords, p, "a", bs);
            }
        }

        Piece botBlackKing = filterPieces("King", bot.opponentPieces)[0];
        Piece botWhiteKing = bot.king;

        int[] botWhiteKingPos = new int[] { botWhiteKing.position[0] - 1, botWhiteKing.position[1] - 1 };
        int[] botBlackKingPos = new int[] { botBlackKing.position[0] - 1, botBlackKing.position[1] - 1 };

        if (bot.color == -1)
        {
            botWhiteKing = filterPieces("King", bot.opponentPieces)[0];
            botBlackKing = bot.king;
        }

        if (HelperFunctions.checkState(botWhiteKing, "Heartbroken"))
        {
            if (!isolatedIsPieceTypeOnBoard("q", 1, bs))
            {
                Piece tempKing = HelperFunctions.Spawnables.create("DepressedKing", 1);
                Destroy(tempKing.go);

                updateBoardState(botWhiteKingPos, tempKing, "a", bs);
                updateBoardState(botWhiteKingPos, bot.king, "r", bs);
                if (bot.color == 1) bot.king = tempKing;
            }
        }

        if (HelperFunctions.checkState(botBlackKing, "Heartbroken"))
        {
            if (!isolatedIsPieceTypeOnBoard("q", -1, bs))
            {
                Piece tempKing = HelperFunctions.Spawnables.create("DepressedKing", -1);
                Destroy(tempKing.go);

                updateBoardState(botBlackKingPos, tempKing, "a", bs);
                updateBoardState(botBlackKingPos, bot.king, "r", bs);
                if (bot.color == -1) bot.king = tempKing;
            }
        }
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
        Piece piece = pMove.piece;
        int[] coords = pMove.coords;

        List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, false);

        if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color)*/!isolatedIsColorOnCoords(piecesOnCoords, true, piece.color)) {
            bool death = false;

            if (piecesOnCoords.Count != 0) {
                death = true;

                //Debug.LogWarning("Checking for death");

                if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color * -1)*/!isolatedIsColorOnCoords(piecesOnCoords, true, piece.color * -1) && !HelperFunctions.checkState(piece, "Murderous")) {
                    death = false;
                }
                else if (HelperFunctions.checkStateAllOnSquare(piecesOnCoords, "Dematerialized")) {
                    death = false;
                }
                else if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords)) {
                    death = false;
                }
                else if (HelperFunctions.checkState(piece, "Dematerialized")) {
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

    public static void isolatedOnDeaths(Piece attacker, int[] deadCoords, BoardState bs) {
        List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords[0], deadCoords[1], bs.boardGrid, false));

        foreach (Piece piece in pieces) {
            //Debug.Log(piece.name + " died on (" + piece.position[0] + "," + piece.position[1] + ") during a simulated move");
            isolatedOnDeath(piece, attacker, bs);
        }
    }

    public static void isolatedOnDeath(Piece deadPiece, Piece attackerPiece, BoardState bs)
    {
        int[] attackerCoords = attackerPiece.position;
        int[] deadPieceCoords = deadPiece.position;

        bool skipCollateral = false;

        //Infinite/Multi Lives
        if (deadPiece.lives != 0)
        {
            isolatedHandleMultipleLivesDeath(deadPiece, bs);

            return;
        }

        //Hungry
        if (HelperFunctions.checkState(deadPiece, "Electric"))
        {
            System.Random rand = new System.Random();
            int randNumber = rand.Next(1, 3);

            if (randNumber == 1)
            {
                isolatedRemovePiece(attackerPiece, bs);
            }
        }

        //Electric
        if (HelperFunctions.checkState(attackerPiece, "Hungry"))
        {
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            attackerPiece.storage.Add(deadPiece);
            skipCollateral = true;
            isolatedRemovePiece(deadPiece, bs);

            return;
        }

        //Spitting

        if (HelperFunctions.checkState(attackerPiece, "Spitting"))
        {
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

        if (HelperFunctions.checkState(attackerPiece, "Stacking"))
        {
            string state = deadPiece.state;
            string[] parts = state.Split('-');

            foreach (string statePart in parts)
            {
                if (!attackerPiece.state.Contains(statePart))
                {
                    HelperFunctions.addState(attackerPiece, statePart);
                }
            }

            string state2 = deadPiece.secondaryState;
            string[] parts2 = state2.Split('-');

            foreach (string statePart in parts2)
            {
                if (!attackerPiece.state.Contains(statePart))
                {
                    HelperFunctions.addState(attackerPiece, statePart);
                }
            }

            string ability = deadPiece.ability;
            string[] abilityParts = ability.Split('-');

            foreach (string abilityPart in abilityParts)
            {
                if (!attackerPiece.ability.Contains(abilityPart))
                {
                    HelperFunctions.addAbility(attackerPiece, abilityPart);
                }
            }

            //Moves
            int[,] moves = HelperFunctions.combineMoveSets(attackerPiece.moves, deadPiece.moves);
            int[,] oneTimeMoves = HelperFunctions.combineMoveSets(attackerPiece.oneTimeMoves, deadPiece.oneTimeMoves);
            int[,] moveAndAttacks = HelperFunctions.combineMoveSets(attackerPiece.moveAndAttacks, deadPiece.moveAndAttacks);
            int[,] oneTimeMoveAndAttacks = HelperFunctions.combineMoveSets(attackerPiece.oneTimeMoveAndAttacks, deadPiece.oneTimeMoveAndAttacks);
            int[,] murderousAttacks = HelperFunctions.combineMoveSets(attackerPiece.murderousAttacks, deadPiece.murderousAttacks);
            int[,] conditionalAttacks = HelperFunctions.combineMoveSets(attackerPiece.conditionalAttacks, deadPiece.conditionalAttacks);
            int[,] attacks = HelperFunctions.combineMoveSets(attackerPiece.attacks, deadPiece.attacks);
            int[,] jumpAttacks = HelperFunctions.combineMoveSets(attackerPiece.jumpAttacks, deadPiece.jumpAttacks);
            int[,] dependentAttacks = HelperFunctions.combineMoveSets(attackerPiece.dependentAttacks, deadPiece.dependentAttacks);
            int[,] interactiveAttacks = HelperFunctions.combineMoveSets(attackerPiece.interactiveAttacks, deadPiece.interactiveAttacks);
            int[,] positionIndependentMoves = HelperFunctions.combineMoveSets(attackerPiece.positionIndependentMoves, deadPiece.positionIndependentMoves);
            int[,] forceStayTurnMoves = HelperFunctions.combineMoveSets(attackerPiece.forceStayTurnMoves, deadPiece.forceStayTurnMoves);
            int[,] flagMove1 = HelperFunctions.combineMoveSets(attackerPiece.flagMove1, deadPiece.flagMove1);
            int[,] flagMove2 = HelperFunctions.combineMoveSets(attackerPiece.flagMove2, deadPiece.flagMove2);
            int[,] pushMoves = HelperFunctions.combineMoveSets(attackerPiece.pushMoves, deadPiece.pushMoves);
            int[,] enPassantMoves = HelperFunctions.combineMoveSets(attackerPiece.enPassantMoves, deadPiece.enPassantMoves);

            attackerPiece.moves = moves;
            attackerPiece.oneTimeMoves = oneTimeMoves;
            attackerPiece.moveAndAttacks = moveAndAttacks;
            attackerPiece.oneTimeMoveAndAttacks = oneTimeMoveAndAttacks;
            attackerPiece.murderousAttacks = murderousAttacks;
            attackerPiece.conditionalAttacks = conditionalAttacks;
            attackerPiece.attacks = attacks;
            attackerPiece.jumpAttacks = jumpAttacks;
            attackerPiece.dependentAttacks = dependentAttacks;
            attackerPiece.interactiveAttacks = interactiveAttacks;
            attackerPiece.positionIndependentMoves = positionIndependentMoves;
            attackerPiece.forceStayTurnMoves = forceStayTurnMoves;
            attackerPiece.flagMove1 = flagMove1;
            attackerPiece.flagMove2 = flagMove2;
            attackerPiece.pushMoves = pushMoves;
            attackerPiece.enPassantMoves = enPassantMoves;

            //maybe add promotion row and storage
        }

        if (HelperFunctions.checkState(attackerPiece, "Jailer"))
        {
            HelperFunctions.addState(deadPiece, "Jailed");

            return;
        }

        if (HelperFunctions.checkState(deadPiece, "Crook") && deadPiece.color != attackerPiece.color)
        {
            HelperFunctions.addState(deadPiece, "Jailed");

            return;
        }

        if (HelperFunctions.checkState(attackerPiece, "Medusa"))
        {
            if (attackerPiece.numSpawns != 0)
            {
                attackerPiece.numSpawns--;

                int[] pos = deadPiece.position;
                updateBoardState(pos, deadPiece, "r", bs);


                Piece shieldPawn = HelperFunctions.Spawnables.create("ShieldPawn", attackerPiece.color * -1);
                Destroy(shieldPawn.go);
                updateBoardState(pos, deadPiece, "a", bs);
            }
        }

        if (!skipCollateral)
        {
            if (attackerPiece.collateralType == 0)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, "Defuser", bs))
                {
                    isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs);
                    isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs);
                    return;
                }

                for (int i = 0; i < attackerPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { attackerCoords[0] + attackerPiece.collateral[i, 0], attackerCoords[1] + attackerPiece.collateral[i, 1] };

                    if (attackerPiece.collateral[i, 0] == 0 && attackerPiece.collateral[i, 1] == 0)
                    {
                        isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs);
                    }

                    List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, false));
                    isolatedCollateralDeath(pieces, bs);
                }
            }

            if (deadPiece.collateralType == 1)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, "Defuser", bs))
                {
                    isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs);
                    isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs);
                    return;
                }

                for (int i = 0; i < deadPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { deadPieceCoords[0] + deadPiece.collateral[i, 0], deadPieceCoords[1] + deadPiece.collateral[i, 1] };

                    if (deadPiece.collateral[i, 0] == 0 && deadPiece.collateral[i, 1] == 0)
                    {
                        isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs);
                        isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs);
                    }

                    List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, false));
                    isolatedCollateralDeath(pieces, bs);
                }
            }
        }

        deadPiece.alive = 0;
        //Debug.Log("DEAD DEAD DEAD");
        updateBoardState(new int[] { deadPieceCoords[0] - 1, deadPieceCoords[1] - 1 }, deadPiece, "r", bs);
    }

    public static bool isolatedIsPieceSurroundingState(Piece piece, string state, BoardState bs)
    {
        int[][] directions = new int[][]
        {
            new int[] { 1, 0 },  // right
            new int[] {-1, 0 },  // left
            new int[] { 0, 1 },  // up
            new int[] { 0, -1 }, // down
            new int[] { 1, 1 },  // up-right
            new int[] {-1, 1 },  // up-left
            new int[] { 1, -1 }, // down-right
            new int[] {-1, -1 },  // down-left
            new int[] { 0, 0 }  // on
        };

        foreach (var dir in directions)
        {
            int x = piece.position[0] + dir[0];
            int y = piece.position[1] + dir[1];

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

    public static void isolatedCollateralDeath(List<Piece> deadPieces, BoardState bs) {
        foreach (Piece deadPiece in new List<Piece>(deadPieces))
        {
            if (HelperFunctions.checkState(deadPiece, "Shield") || HelperFunctions.checkCaptureTheFlag(deadPiece))
            {
                continue;
            }

            //Debug.Log(deadPiece.name + " died to collateral on (" + deadPiece.position[0] + "," + deadPiece.position[1] + ") during simulated move");
            if (deadPiece.lives != 0)
            {
                isolatedHandleMultipleLivesDeath(deadPiece, bs);

                continue;
            }
            else
            {
                updateBoardState(new int[] { deadPiece.position[0] - 1, deadPiece.position[1] - 1 }, deadPiece, "r", bs);
                deadPiece.alive = 0;
            }
        }
    }

    public static void isolatedRemovePiece(Piece p, BoardState bs) {
        updateBoardState(new int[] { p.position[0] - 1, p.position[1] - 1 }, p,"r", bs);
    }

    public static void isolatedHandleMultipleLivesDeath(Piece deadPiece, BoardState bs) {
        deadPiece.lives--;

        if (!HelperFunctions.isOnStartSquare(deadPiece) && !isolatedIsPieceOnStartSquare(deadPiece, bs)) {
            movePieceBoardState(deadPiece, new int[] { deadPiece.startSquare[0] - 1, deadPiece.startSquare[1] - 1 }, bs);
        }
        else {
            updateBoardState(new int[] { deadPiece.position[0] - 1, deadPiece.position[1] - 1 }, deadPiece, "r", bs);
        }
    }

    //Probably dont need
    public static bool isolatedIsOnStartSquare(Piece p, BoardState bs) {
        return false;
    }

    public static bool isolatedIsPieceOnStartSquare(Piece p, BoardState bs) {
        return isolatedGetPiecesOnCoordsBoardGrid(p.startSquare[0], p.startSquare[1], bs.boardGrid, false).Count != 0;
    }

    public static void resetPiecePositions(BoardState bs, List<List<List<Piece>>> boardGrid)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                List<Piece> sourceSquare = boardGrid[x][y];

                foreach (Piece piece in sourceSquare)
                {
                    //Debug.LogWarning(piece.name + " reset to pos " + (x + 1) + "," + (y + 1));
                    piece.position = new int[] { x + 1, y + 1 };
                }
            }
        }
    }

    public static void debug_printBoardState(BoardState bs)
    {
        Debug.LogWarning("Pieces on Board State");
        List<List<List<Piece>>> boardGrid = bs.boardGrid;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach(Piece p in boardGrid[x][y])
                {
                    Debug.LogWarning(p.name + " found on " + (x + 1) + "," + (y + 1));
                }
            }
        }

        Debug.LogWarning("Pieces on Board State END");
    }

    public static Piece findPieceOnOtherBoardState(BoardState bs, string pName)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach(Piece p in bs.boardGrid[x][y])
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

        return null;
    }
}
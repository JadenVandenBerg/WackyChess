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

    public static List<Piece> filterPieces(string type, List<Piece> pieces) {
    	List<Piece> filteredPieces = new List<Piece>();

    	foreach (var piece in pieces) {
            if (piece.baseType == type) {
    			filteredPieces.Add(piece);
    		}
    	}

        return filteredPieces;
    }

    //List so its easier to randomize. Each Dict has only one entry
    public static (List<Dictionary<Piece, List<int[]>>> pieceMoveList, Dictionary<Piece, List<string>> piecesAbilities) getAllPossibleBotMoves(BotTemplate bot, int color) {
    	List<Dictionary<Piece, List<int[]>>> totalMoves = new List<Dictionary<Piece, List<int[]>>>();
        //urgent fix this
    	foreach (Piece piece in bot.pieces) {
            int[] oldPiecePosition = { piece.position[0], piece.position[1] };
            List<List<List<Piece>>> oldBoardGrid = gameData.boardGrid;
            gameData.boardGrid = bot.currentBoardState.boardGrid;
            //Debug.Log(piece.name + " OLD POS" + piece.position[0] + "," + piece.position[1]);
            piece.position = bot.currentBoardState.getPiecePosition(piece);

            if (piece.position == null)
            {
                piece.position = oldPiecePosition;
                gameData.boardGrid = oldBoardGrid;

                continue;
            }

            //Debug.Log(piece.name + " NEW POS" + piece.position[0] + "," + piece.position[1]);

    		List<int[]> moves = HelperFunctions.addMovesToCurrentMoveableCoords(piece);

    		if (moves.Count > 0) {
    			Dictionary<Piece, List<int[]>> pMoveDict = new Dictionary<Piece, List<int[]>>();

	    		pMoveDict.Add(piece, moves);

	    		totalMoves.Add(pMoveDict);
    		}

            piece.position = oldPiecePosition;
            gameData.boardGrid = oldBoardGrid;
    	}

        return (totalMoves, HelperFunctions.getAllEligibleAbilities(color));
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



        return allMoves;
    }

    private static void isolatedIterateThroughPieceMoves(Func<Piece, bool, bool, bool, List<Piece>, bool> comparator, Piece piece, BoardState bs, int[,] moveType, bool check, List<int[]> allMoves)
    {
        int color = piece.color;

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

        for (int i = 0; i < moveType.GetLength(0); i++)
        {
            //Portal
            int[] oldCoords = new int[] { moveType[i, 0] + piece.position[0], moveType[i, 1] + piece.position[1] };
            int[] coordsP = HelperFunctions.adjustCoordsForPortal(piece, oldCoords[0], oldCoords[1]);
            int[] coordsB = HelperFunctions.adjustCoordsForBouncing(piece, oldCoords[0], oldCoords[1]);

            int[] newPos = new int[] { oldCoords[0], oldCoords[1] };

            if (HelperFunctions.checkState(piece, "Portal"))
            {
                newPos[0] = coordsP[0];
                newPos[1] = coordsP[1];
            }
            else if (HelperFunctions.checkState(piece, "Bouncing"))
            {
                newPos[0] = coordsB[0];
                newPos[1] = coordsB[1];
            }

            List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(newPos[0], newPos[1], bs.boardGrid);
            bool pieceIsNull = piecesOnCoords == null || piecesOnCoords.Count == 0;
            bool pieceIsDiffColour = false;

            if (!pieceIsNull)
            {
                pieceIsDiffColour = !isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color);

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

            //TODO jump
        }
    }

    private static bool isolatedCheckSquareCrowdingEligible(Piece piece, List<Piece> piecesOnCoords) {
        // No pieces
        if (piecesOnCoords == null || piecesOnCoords.Count == 0) {
            return true;
        }

        int[] colorsOnCoords = isolatedGetColorsOnCoords(piecesOnCoords, true);

        //Pieces different color
        if (colorsOnCoords.Contains(piece.color * -1)) {
            return false;
        }

        // Square contains more than one other piece (not crowding)
        if (piecesOnSquare.Count > 1 && HelperFunctions.checkState(piece, "Crowding"))
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

        //There is one piece on the square, piece is crowding
        if (HelperFunctions.checkState(piece, "Crowding") && piecesOnCoords.Count == 1 && colorsOnCoords.Contains(piece.color)) {
            return true;
        }

        //There is one piece on the square, piece is piggyback
        if (piecesOnCoords.Count == 1 && HelperFunctions.checkState(piecesOnCoords[0], "Piggyback") && colorsOnCoords.Contains(piece.color)) {
            return true;
        }

        //There is one piece on square, piece is jockey
        if (piecesOnCoords.Count == 1 && HelperFunctions.checkState(piece, "Jockey") && colorsOnCoords.Contains(piece.color)) {
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
            if (piece.disabled || piece.alive == 0 || (ignoreDematerialized && HelperFunctions.checkState(piece, "Dematerialized")) || HelperFunctions.checkState(piece, "Jailed"))
            {
                continue;
            }

            colors.Add(piece.color);
        }

        return colors;
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
                List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(i, j, boardGrid);
                allPieces.AddRange(pieces);
            }
        }

        return allPieces;
    }

    public static List<Piece> isolatedGetPiecesOnCoordsBoardGrid(int x, int y, List<List<List<Piece>>> boardGrid)
    {
        List<Piece> pieces = new List<Piece>();
        
        pieces = boardGrid[x][y];

        return pieces;
    }

    public static (Piece piece, int[] coords) getRandomBotMove(BotTemplate bot)
    {
        var botMoves = getAllPossibleBotMoves(bot, bot.color);

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
        if (coords[0] < 1 || coords[1] < 1)
        {
            return;
        }

        int[] position = boardState.getPiecePosition(piece);
        if (position == null) return;

        updateBoardState(position, piece, "r", boardState);
        gameData.boardGrid[coords[0] - 1][coords[1] - 1].Add(piece);
    }

    public static void updateBoardState(int[] coords, Piece piece, String action, BoardState boardState)
    {
        if (coords[0] < 1 || coords[1] < 1)
        {
            return;
        }

        var square = boardState.boardGrid[coords[0] - 1][coords[1] - 1];

        if (action.ToLower() == "a" || action.ToLower() == "add")
        {
            bool alreadyExists = square.Any(p => p.name == piece.name);
            if (!alreadyExists)
            {
                square.Add(piece);
            }
        }

        if (action.ToLower() == "r" || action.ToLower() == "remove")
        {
            square.RemoveAll(p => p.name == piece.name);
        }
    }

    public static BoardState copyBoardState(BoardState bs) {
        BoardState copy = new BoardState();

        copy.boardGrid = bs.boardGrid.Select(x =>
            x.Select(y =>
                new List<Piece>(y)
            ).ToList()
        ).ToList();

        copy.whitePointsOnBoard = bs.whitePointsOnBoard;
        copy.blackPointsOnBoard = bs.blackPointsOnBoard;

        copy.inCheck = new int[] { bs.inCheck[0], bs.inCheck[1] };

        return copy;
    }

    public static List<float> getPointsOnBoardState(BoardState bs) {
        List<List<List<Piece>>> board = bs.boardGrid;
        float wCount = 0;
        float bCount = 0;

        foreach (var x in board) {
            foreach (var y in x) {
                foreach (Piece piece in y) {
                    if (piece.color == 1) {
                        wCount += piece.points;
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

}
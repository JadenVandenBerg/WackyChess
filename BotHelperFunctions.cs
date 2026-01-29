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
		List<string> pieces = getAllTypePieces(type);

		List<string> selected = new List<string>();

		int count = 2;

		if (type == "Pawn") {
			count = 8;
		}
		else if (type == "Queen" || type == "King") {
			count = 1;
		}

		for (int i = 0; i < count; i++)
		{
		    int index = Random.Shared.Next(pieces.Count);

		    Type type = Type.GetType("" + pieces[index] + ", Assembly-CSharp");
		    selected.Add((Piece) Activator.CreateInstance(type, color, false));
		    pieces.RemoveAt(index);
		}

		return selected;
	}

	private List<string> getAllTypePieces(string type) {

    	List<Piece> allPieces = Lootbox.GetAllPieces();
    	List<string> eligiblePieces = new List<string>();

    	foreach (var piece in allPieces) {
    		if (piece.baseType = type) {
    			eligiblePieces.Add(piece.name);
    		}
    	}

    	return eligiblePieces;
    }

    public List<Piece> filterPieces(string type, List<Piece> pieces) {
    	List<Piece> filteredPieces = new List<Piece>();

    	foreach (var piece in pieces) {
    		if (piece.baseType == type) {
    			filteredPieces.Add(piece);
    		}
    	}
    }

    //List so its easier to randomize. Each Dict has only one entry
    public (List<Dictionary<Piece, List<int[]>>> pieceMoveList, Dictionary<Piece, string> piecesAbilities) getAllPossibleBotMoves(BotTemplate bot, int color) {
    	// REMAKE of the original algorithm from helperfunctions
    	// TODO this function assumes gameData vars are set

    	List<List<List<Piece>>> oldBoardGrid = gameData.boardGrid;
    	List<Dictionary<Piece, List<int[]>>> totalMoves = new List<Dictionary<Piece, List<int[]>>>();

    	foreach (Piece piece in bot.pieces) {
    		List<int[]> moves = HelperFunctions.addMovesToCurrentMoveableCoords(piece);

    		if (moves.Count > 0) {
    			Dictionary<Piece, List<int[]>> pMoveDict = new Dictionary<Piece, List<int[]>>();

	    		pMoveDict.Add(piece, moves);

	    		totalMoves.Add(pMoveDict);
    		}
    	}

    }

    public (Piece piece, int[] coords) getRandomBotMove(BotTemplate bot) {
        var botMoves = BotHelperFunctions.getAllPossibleBotMoves(bot, bot.color);
        List<Dictionary<Piece, List<int[]>>> allMoves = botMoves.pieceMoveList;
        Dictionary<Piece, string> allAbilities = botMoves.piecesAbilities;

        Random rand = new Random();
        int r = rand.Next(allMoves.Count);

        Dictionary<Piece, List<int[]>> pieceMovesDict = allMoves[r];
        KeyValuePair<Piece, List<int[]>> = pieceMovesKeyVal.First();
        Piece _randMovePiece = pieceMovesKeyVal.Key;
        int[] _randMoveCoordsList = pieceMovesKeyVal.Value;

        rand = new Random();
        int r = rand.Next(_randMoveCoordsList.Count);

        int[] randMoveCoords = _randMoveCoordsList[r];

        return (_randMovePiece, moveCoords)
    }
}
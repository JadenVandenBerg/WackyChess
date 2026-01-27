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

    public (List<Dictionary<Piece, int[]>>, List<Dictionary<Piece, string>>) getAllPossibleBotMoves(BotTemplate bot, int color) {
    	// REMAKE of the original algorithm from helperfunctions
    }
}
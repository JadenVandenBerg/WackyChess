using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class Lootbox {
	private int rarityLevel;
	private string item;

	public Lootbox() {
		setRarityLevel(getRarity());
		getUnboxedPiece(getRarityLevel());
	}

	private int() getRarity {
		Random rand = new Random();

		int randVal = rand.Next(0, 100);
		int rarityLevel = 0;

		if (randVal >= 0 && randVal < 35) {
			rarityLevel = 1; //Pawn
		}
		else if (randVal >= 35 && randVal < 70) {
			rarityLevel = 2; //Common Item
		}
		else if (randVal >= 70 && randVal < 90) {
			rarityLevel = 3; //Uncommon Item
		}
		else if (randVal >= 90 && randVal < 98) {
			rarityLevel = 4; //Rare Item
		}
		else if (randVal >= 98 && randVal < 100) {
			rarityLevel = 5; //Legendary Item
		}

		return rarityLevel;
	}

	public static List<Type> GetAllPieces()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                typeof(Piece).IsAssignableFrom(t) &&
                t.IsClass &&
                !t.IsAbstract)
            .ToList();
    }

    private List<string> getAllElegiblePieces(int rarityLevel) {

    	List<Piece> allPieces = GetAllPieces();
    	List<string> eligiblePieces = new List<string>();

    	foreach (var piece in allPieces) {
    		if (piece.rarityLevel = rarityLevel) {
    			eligiblePieces.Add(piece.name);
    		}
    	}

    	return eligiblePieces;
    }

    private string getUnboxedPiece(int rarityLevel) {
    	Random rand = new Random();
    	List<string> pieces = getAllElegiblePieces(rarityLevel);

    	string unboxed = pieces[rand.Next(pieces.length)];
    	setPiece(unboxed);
    	return unboxed;
    }

	private void setRarityLevel(int rarity) {
		rarityLevel = rarity;
	}

	private int getRarityLevel() {
		return rarityLevel;
	}

	public string getUnboxed() {
		return item;
	}

	public string setUnboxed(string unboxed) {
		item = unboxed;
	}

}
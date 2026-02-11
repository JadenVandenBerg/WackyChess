using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotTemplate {
	//TODO make sure these are set appropriately
	public BoardState currentBoardState { get; set; } = new BoardState();
	public List<Piece> pieces { get; set; } = new List<Piece>();
	public List<Piece> opponentPieces { get; set; } = new List<Piece>();
	public Piece king { get; set; } = null;
	public bool penalty { get; set; } = false;
	public string name { get; set; } = "Template";

	//1: white, -1: black
	public int color { get; set; } = 0;

	public BotTemplate() {
	}

	/* 
	* Called from bot master script
	*/
	public void choosePieces() {
		List<Piece> pawns = BotHelperFunctions.getPiecesTypeRandom("Pawn", color);
		pieces.AddRange(pawns);

		List<Piece> rooks = BotHelperFunctions.getPiecesTypeRandom("Rook", color);
		pieces.AddRange(rooks);

		List<Piece> knights = BotHelperFunctions.getPiecesTypeRandom("Knight", color);
		pieces.AddRange(knights);

		List<Piece> bishops = BotHelperFunctions.getPiecesTypeRandom("Bishop", color);
		pieces.AddRange(bishops);

		List<Piece> kings = BotHelperFunctions.getPiecesTypeRandom("King", color);
		pieces.AddRange(kings);

		List<Piece> queens = BotHelperFunctions.getPiecesTypeRandom("Queen", color);
		pieces.AddRange(queens);
	}

	public List<Piece> getPieces() {
		return HelperFunctions.getPiecesOnBoardColor(color);
	}

	public List<Piece> getOpponentPieces() {
		return HelperFunctions.getPiecesOnBoardColor(color * -1);
	}

	// This function will be called to determine your pieces next move in the game
	//Piece: The piece to be moved
	//int[]: 1-indexed coords to move the piece ([1,1]:[8,8])
	abstract
	public Dictionary<Piece, int[]> nextMove();
}

//TODO make sure these are updated
public class BoardState {

	// Outer List: x-Axis
	// Middle List: y-Axis
	// Inner List: List of Pieces
	// ie. List<Piece> a2 = boardGrid[0][1]
	public List<List<List<Piece>>> boardGrid { get; set; } = new List<List<List<Piece>>>();
	public List<Piece> allPieces = new List<Piece>();
	public List<Piece> whitePieces = new List<Piece>();
	public List<Piece> blackPieces = new List<Piece>();
	public Piece whiteKing;
	public Piece blackKing;

	public DelayedQueue delayedQueue { get; set; } = new DelayedQueue();

	public float whitePointsOnBoard { get; set; } = 0;
	public float blackPointsOnBoard { get; set; } = 0;

	// List of colours in check
	public int[] inCheck { get; set; } = new int[2];

	public BoardState() {
		refresh();
	}

	// Resets the board state based on the real value of the board
	public void refresh(List<List<List<Piece>>> passedBoardGrid) {

		boardGrid = passedBoardGrid;

		float wp = 0;
		float bp = 0;

		foreach (List<List<Piece>> llp in boardGrid) {
			foreach (List<Piece> lp in llp) {
				foreach (Piece piece in lp) {
					allPieces.Add(piece);
					if (piece.color == 1) {
						wp += piece.points;
						whitePieces.Add(piece);
					}
					else if (piece.color == -1) {
						bp += piece.points;
						blackPieces.Add(piece);
					}
				}
			}
		}

		whiteKing = BotHelperFunctions.filterPieces("King", whitePieces)[0];
		blackKing = BotHelperFunctions.filterPieces("King", blackPieces)[0];

		whitePointsOnBoard = wp;
		blackPointsOnBoard = bp;

		inCheck = gameData.isInCheck;

		delayedQueue = tempInfo.delayedQueue;
	}

	public int[] getPiecePosition(Piece piece) {
		if (boardGrid == null || boardGrid.Count == 0) return null;

		for (int x = 0; x < 8; x++) {
			if (boardGrid[x] == null) continue;
			for (int y = 0; y < 8; y++) {
				if (boardGrid[x][y] == null) continue;

				foreach(Piece p in boardGrid[x][y]) {
					if (piece.name == p.name) {
						return new int[] { x + 1, y + 1 };
					}
				}
			}
		}

		return null;
	}
}
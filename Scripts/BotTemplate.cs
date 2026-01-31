using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotTemplate {
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

	// This function will be called to determine your pieces next move in the game
	//Piece: The piece to be moved
	//int[]: 1-indexed coords to move the piece ([1,1]:[8,8])
	abstract
	public Dictionary<Piece, int[]> nextMove();
}

public class BoardState {

	// Outer List: x-Axis
	// Middle List: y-Axis
	// Inner List: List of Pieces
	// ie. List<Piece> a2 = boardGrid[0][1]
	public List<List<List<Piece>>> boardGrid { get; set; } = new List<List<List<Piece>>>();

	public float whitePointsOnBoard { get; set; } = 0;
	public float blackPointsOnBoard { get; set; } = 0;

	// List of colours in check
	public int[] inCheck { get; set; } = new int[2];

	public BoardState() {
		refresh();
	}

	// Resets the board state based on the real value of the board
	// TODO
	public void refresh() {

		boardGrid = gameData.boardGrid;

		float wp = 0;
		float bp = 0;

		foreach (List<List<Piece>> llp in boardGrid) {
			foreach (List<Piece> lp in llp) {
				foreach (Piece piece in lp) {
					if (piece.color == 1) {
						wp += piece.points;
					}
					else if (piece.color == -1) {
						bp += piece.points;
					}
				}
			}
		}

		whitePointsOnBoard = wp;
		blackPointsOnBoard = bp;
	}
}
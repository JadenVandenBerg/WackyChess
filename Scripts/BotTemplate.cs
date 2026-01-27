using System;
using System.Collections.Generic;
using UnityEngine;
using HelperFunctions;

public abstract class BotTemplate {
	public BoardState currentBoardState { get; set; } = new BoardState();
	public List<Piece> pieces { get; set; } = new List<Piece>();
	public List<Piece> opponentPieces { get; set; } = new List<Piece>();
	public Piece king { get; set; } = null;

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
	public Dictionary<Piece, int[]> nextMove() {
		Dictionary<Piece, int[]> finalMove = new Dictionary<Piece, int[]>();

		return finalMove;
	}
}

public class BoardState {

	// Outer List: x-Axis
	// Middle List: y-Axis
	// Inner List: List of Pieces
	public List<List<List<Piece>>> boardGrid { get; set; } = new List<List<List<Piece>>>();

	public int whitePointsOnBoard { get; set; } = 0;
	public int blackPointsOnBoard { get; set; } = 0;

	// How many unique squares colour can attack
	public int whiteControl { get; set; } = 0;
	public int whiteControl { get; set; } = 0;

	public float whiteRating { get; set; } = 0.0f;
	public float blackRating { get; set; } = 0.0f;

	// List of colours in check
	public int[] inCheck { get; set; } = [];

	public BoardState() {
		//
	}

	// Resets the board state based on the real value of the board
	public BoardState refresh(List<List<List<Piece>>> newBoardGrid) {
		boardGrid = new List<List<List<Piece>>>(newBoardGrid);
	}
}
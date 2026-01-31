using System;
using System.Collections.Generic;
using UnityEngine;
using HelperFunctions;

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
	public Dictionary<Piece, int[]> nextMove() {
		Dictionary<Piece, int[]> finalMove = new Dictionary<Piece, int[]>();

		return finalMove;
	}
}

public class BoardState {

	// Outer List: x-Axis
	// Middle List: y-Axis
	// Inner List: List of Pieces
	// ie. List<Piece> a2 = boardGrid[0][1]
	public List<List<List<Piece>>> boardGrid { get; set; } = new List<List<List<Piece>>>();

	public int whitePointsOnBoard { get; set; } = 0;
	public int blackPointsOnBoard { get; set; } = 0;

	// List of colours in check
	public int[] inCheck { get; set; } = [];

	public BoardState() {
		refresh();
	}

	// Resets the board state based on the real value of the board
	// TODO
	public BoardState refresh(List<List<List<Piece>>> newBoardGrid) {
		boardGrid = new List<List<List<Piece>>>(newBoardGrid);

		boardGrid = gameData.boardGrid;

		int wp = 0;
		int bp = 0;

		foreach (List<List<Piece>> llp in boardGrid) {
			foreach (List<Piece> lp in llp) {
				foreach (Piece piece in lp) {
					if (piece.color == 1) {
						wp += piece.points
					}
					else if (piece.color == -1) {
						bp += piece.points;
					}
				}
			}
		}
	}
}
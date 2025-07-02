using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class gameData
{
    public static GameObject selected { get; set; } = null;
    public static Piece selectedPiece { get; set; } = null;
    public static Piece selectedToMovePiece { get; set; } = null;
    public static Dictionary<GameObject, Piece> piecesDict { get; set; } = new Dictionary<GameObject, Piece>();
    public static GameObject board { get; set; }
    public static bool isSelected { get; set; } = false;
    public static bool selectedFromPanel { get; set; } = false;
    public static bool refreshedSinceClick { get; set; } = false;
    public static int turn { get; set; } = 1;
    public static bool readyToMove { get; set; } = false;
    public static GameObject selectedToMove { get; set; }
    public static List<int[]> currentMoveableCoords { get; set; } = new List<int[]>();
    public static List<int[]> currentMoveableCoordsAllPieces { get; set; } = new List<int[]>();
    public static int[] isInCheck { get; set; } = { 0, 0 }; //0 white, 1 black
    public static Piece whiteKing { get; set; }
    public static Piece blackKing { get; set; }
    public static List<Piece> whiteRooks { get; set; } = new List<Piece>();
    public static List<Piece> blackRooks { get; set; } = new List<Piece>();
    public static float[] pointsOnBoard { get; set; } = { 0, 0 };
    public static String winner { get; set; } = "";
    public static bool isPaused { get; set; } = false;
    public static bool check { get; set; } = false;
    public static bool checkMate { get; set; } = false;
    public static bool staleMate { get; set; } = false;
    public static bool botMove { get; set; } = false;
    public static Dictionary<Piece, List<int[]>> botMoves { get; set; } = new Dictionary<Piece, List<int[]>>();
    public static String playMode { get; set; } = "";
    public static Piece bestMovePiece { get; set; } = null;
    public static int[] bestMoveCoords { get; set; } = new int[] { 0, 0 };
    public static int forceStayTurn { get; set; } = 0;
    public static List<List<List<Piece>>> boardGrid { get; set; } = new List<List<List<Piece>>>();
}

public static class tempInfo
{
    public static float botMoveOpponentBestPoints { get; set; } = 0;
}
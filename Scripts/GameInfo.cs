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
    public static Dictionary<GameObject, Piece> allPiecesDict { get; set; } = new Dictionary<GameObject, Piece>();
    public static GameObject board { get; set; }
    public static bool isSelected { get; set; } = false;
    public static bool selectedFromPanel { get; set; } = false;
    public static bool abilityAdvanceNext { get; set; } = false;
    public static bool refreshedSinceClick { get; set; } = false;
    public static int turn { get; set; } = 1;
    public static bool readyToMove { get; set; } = false;
    public static string abilitySelected { get; set; } = "";
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
    public static List<String> panelCodes { get; set; } = new List<String>();
    public static BotTemplate botWhite { get; set; } = null;
    public static BotTemplate botBlack { get; set; } = null;
    public static bool isBotMatch { get; set; } = false;
}

public static class tempInfo
{
    public static float botMoveOpponentBestPoints { get; set; } = 0;
    public static List<int[]> tempCoordSet { get; set; } = null;
    public static GameObject tempSquare { get; set; } = null;
    public static Piece tempPiece { get; set; } = null;
    public static bool selectedFromPanel { get; set; } = false;
    public static bool passed { get; set; } = false;
    public static DelayedQueue delayedQueue { get; set; } = new DelayedQueue();
}

public static readonly (int dx, int dy)[] globalDirectionsNoZero =
{
    (1, 0), (-1, 0),
    (0, 1), (0, -1),
    (1, 1), (-1, 1),
    (1, -1), (-1, -1)
};

public static readonly (int dx, int dy)[] globalDiagionalDirectionsNoZero =
{
    (1, 1), (-1, -1),
    (-1, 1), (1, -1)
};

public static class botTournament {
    public static List<BotTemplate> competingBots = new List<BotTemplate>();
}

/*
Round 1
1 v 8
2 v 7
3 v 6
4 v 5

Round 2
1 v 5
2 v 6
3 v 7
4 v 8

Round 3
1 v 2
3 v 4
5 v 6
7 v 8

Round 4
1 v 7
2 v 4
3 v 5
6 v 8

Round 5
1 v 3
2 v 8
4 v 6
5 v 7

Round 6
1 v 4
2 v 3
5 v 8
6 v 7

Round 7
1 v 6
2 v 5
3 v 8
4 v 7
*/
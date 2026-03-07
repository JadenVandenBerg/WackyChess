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
    public static bool attackerDied { get; set; } = false;
}

public static class globalDefs
{
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

    public static readonly (int dx, int dy)[] globalDirections =
    {
        (1, 0), (-1, 0),
        (0, 1), (0, -1),
        (1, 1), (-1, 1),
        (1, -1), (-1, -1),
        (0, 0)
    };
}

public class BotGameStatus
{
    public string white;
    public string black;

    public List<string> whitePieces = new List<string>();
    public List<string> blackPieces = new List<string>();

    public string result;
    public string winner;

    public float whitePoints;
    public float blackPoints;

    public int whitePenalties = 0;
    public int blackPenalties = 0;

    public int numTurns = 0;
}

public class BotTournament {
    public List<string> competingBots = new List<string>();
    public int round = 1;
    public int match = 1;

    public BotTournament(string botOne, string botTwo, string botThree, string botFour, string botFive, string botSix, string botSeven, string botEight) {
        competingBots.Add(botOne);
        competingBots.Add(botTwo);
        competingBots.Add(botThree);
        competingBots.Add(botFour);
        competingBots.Add(botFive);
        competingBots.Add(botSix);
        competingBots.Add(botSeven);
        competingBots.Add(botEight);
    }

    public (string botWhite, string botBlack) nextGame() {
        var (botOne_, botTwo_) = botTournamentMatches[round - 1][match - 1];
        string botOne = competingBots[botOne_ - 1];
        string botTwo = competingBots[botTwo_ - 1];

        match++;
        if (match > 8) {
            match = 1;
            round++;

            if (round > 8) {
                return ("", "");
            }
        }

        System.Random rand = new System.Random();
        int randNumber = rand.Next(1, 3);

        if (randNumber == 1) {
            return (botOne, botTwo);
        }
        else {
            return (botTwo, botOne);
        }
    }

    public static readonly List<(int botOne, int botTwo)[]> botTournamentMatches = new List<(int botOne, int botTwo)[]>
    {
        new (int botOne, int botTwo)[]
        {
            (1, 8),
            (2, 7),
            (3, 6),
            (4, 5)
        },
        new (int botOne, int botTwo)[]
        {
            (1, 5),
            (2, 6),
            (3, 7),
            (4, 8)
        },
        new (int botOne, int botTwo)[]
        {
            (1, 6),
            (2, 5),
            (3, 8),
            (4, 7)
        },
        new (int botOne, int botTwo)[]
        {
            (1, 2),
            (3, 4),
            (5, 6),
            (7, 8)
        },
        new (int botOne, int botTwo)[]
        {
            (1, 7),
            (2, 4),
            (3, 5),
            (6, 8)
        },
        new (int botOne, int botTwo)[]
        {
            (1, 4),
            (2, 3),
            (5, 8),
            (6, 7)
        },
        new (int botOne, int botTwo)[]
        {
            (1, 3),
            (2, 8),
            (4, 6),
            (5, 7)
        },
    };
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
1 v 6
2 v 5
3 v 8
4 v 7

Round 4
1 v 7
2 v 4
3 v 5
6 v 8

Round 5
1 v 4
2 v 3
5 v 8
6 v 7

Round 6
1 v 3
2 v 8
4 v 6
5 v 7

Round 7
1 v 2
3 v 4
5 v 6
7 v 8
*/
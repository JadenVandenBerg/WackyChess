using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static PieceAbilities abilitySelected { get; set; } = PieceAbilities.None;
    public static GameObject selectedToMove { get; set; }
    public static List<coords> currentMoveableCoords { get; set; } = new List<coords>();
    public static List<coords> currentMoveableCoordsAllPieces { get; set; } = new List<coords>();
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
    public static List<List<List<Piece>>> boardGrid { get; set; } = new List<List<List<Piece>>>();
    public static List<String> panelCodes { get; set; } = new List<String>();
    public static BotTemplate botWhite { get; set; } = null;
    public static BotTemplate botBlack { get; set; } = null;
    public static bool isBotMatch { get; set; } = false;
    public static HelperFunctions helper { get; set; } = null;
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
    public static PieceState stackingStates { get; set; } = PieceState.None;
}

public static class lastMoveCache
{
    public static Piece lastPiece = null;
    public static int[] lastDirection = null;
    public static int lastDistance = 0;
    public static bool lastWasJump = false;

    public static void reset()
    {
        lastPiece = null;
        lastDirection = null;
        lastDistance = 0;
        lastWasJump = false;
    }

    public static int[] normalizeDirection(int dx, int dy)
    {
        if (dx != 0) dx /= Math.Abs(dx);
        if (dy != 0) dy /= Math.Abs(dy);
        return new int[] { dx, dy };
    }

    public static bool isDirectional(int dx, int dy)
    {
        return dx == 0 || dy == 0 || Math.Abs(dx) == Math.Abs(dy);
    }

    public static int distance(int[] position, int[] coords)
    {
        return Math.Max(Math.Abs(position[0] - coords[0]), Math.Abs(position[1] - coords[1]));
    }
}

public static class nonResettables
{
    public static BotTournamentSmall botTournamentSmall { get; set; } = null;
    public static BotTournament botTournament { get; set; } = null;
    public static bool isBotTournament { get; set; } = false;
    public static bool logMatch { get; set; } = false;
    public static string ruleset { get; set; } = "Normal";

    public static void calculateElo(Bot botA, Bot botB, string winner)
    {
        double expectedScoreA =
            1.0 / (1.0 + Math.Pow(10, (botB.Elo - botA.Elo) / 400.0));

        double scoreA;

        if (string.IsNullOrEmpty(winner))
        {
            scoreA = 0.5;
        }
        else if (winner == botA.Name)
        {
            scoreA = 1;
        }
        else
        {
            scoreA = 0;
        }

        int delta = (int)Math.Round(64 * (scoreA - expectedScoreA));

        botA.Elo += delta;
        botB.Elo -= delta;

        botA.PeakElo = Math.Max(botA.PeakElo, botA.Elo);
        botB.PeakElo = Math.Max(botB.PeakElo, botB.Elo);

        botA.MinElo = Math.Min(botA.MinElo, botA.Elo);
        botB.MinElo = Math.Min(botB.MinElo, botB.Elo);
    }

    public static string fixBotName(string botName)
    {
        if (botName == "Idiot")
        {
            return "Idiot Bot";
        }
        else if (botName == "RandomBot")
        {
            return "Random Bot";
        }
        else if (botName == "SavageBeastBot" || botName == "Savage Beastbot")
        {
            return "Savage Beastbot";
        }
        else if (botName == "G2Ebot" || botName == "G2EBot")
        {
            return "G2 E-Bot";
        }

        return botName;
    }

    public static List<string> get8RandomBots(List<string> forceNames)
    {
        List<string> options = new List<string>
        {
            "BottusMaximus",
            "Abilibot",
            "Bloodbot",
            "SavageBeastBot",
            "FiveXRandomBot",
            "RandomBot",
            "OneMoveBot",
            "IdiotBot",
            "G2EBot",
            "KamikazeBot",
            "BotRoss",
            "TwoMoveBot",
            "ThinkingBot",
            "Lobotomy",
            "Bot618",
            "BotsUtd",
            "ThinkingBotII"
        };

        List<string> result = new List<string>();
        result.AddRange(forceNames);

        foreach(string name in forceNames)
        {
            options.Remove(name);
        }

        System.Random rng = new System.Random();

        for (int i = 0; i < 8 - forceNames.Count && options.Count > 0; i++)
        {
            int index = rng.Next(options.Count);
            result.Add(options[index]);
            options.RemoveAt(index);
        }

        return result;
    }

    public static void postBotMatch(string botAName, string botBName, string winner)
    {
        string path = @"C:\Users\Jay\Projects\WC_React\wc_react\public\data.json";

        List<Bot> bots = JsonConvert.DeserializeObject<List<Bot>>(
            File.ReadAllText(path)
        );

        Debug.Log(botAName + " : " + botBName);

        botAName = fixBotName(botAName);
        botBName = fixBotName(botBName);
        winner = fixBotName(winner);

        Bot botA = bots.First(b => b.Name == botAName);
        Bot botB = bots.First(b => b.Name == botBName);

        calculateElo(botA, botB, winner);

        if (string.IsNullOrEmpty(winner))
        {
            botA.DrawsTotal++;
            botB.DrawsTotal++;
        }
        else if (winner == botA.Name)
        {
            botA.WinsTotal++;
            botB.LossesTotal++;
        }
        else
        {
            botB.WinsTotal++;
            botA.LossesTotal++;
        }

        File.WriteAllText(
            path,
            JsonConvert.SerializeObject(bots, Formatting.Indented)
        );
    }
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

    public static readonly System.Random globalRand = new System.Random();
}

public class BotGameStatus
{
    public string white;
    public string black;

    public List<string> whitePieces = new List<string>();
    public List<string> blackPieces = new List<string>();

    public string result;
    public string winner;
    public string winnerName;

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

    public BotTournament(string botOne, string botTwo, string botThree, string botFour, string botFive, string botSix, string botSeven, string botEight, bool randomize)
    {
        List<string> bots = new List<string>()
        {
            botOne, botTwo, botThree, botFour,
            botFive, botSix, botSeven, botEight
        };

        if (randomize)
        {
            System.Random rand = new System.Random();

            for (int i = bots.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (bots[i], bots[j]) = (bots[j], bots[i]);
            }
        }

        competingBots.AddRange(bots);
    }

    public (string botWhite, string botBlack) nextGame() {
        if (round > 7)
        {
            return ("", "");
        }
        var (botOne_, botTwo_) = botTournamentMatches[round - 1][match - 1];
        string botOne = competingBots[botOne_ - 1];
        string botTwo = competingBots[botTwo_ - 1];

        match++;
        if (match > 4) {
            match = 1;
            round++;
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

public class BotTournamentSmall {
    public List<string> competingBots = new List<string>();
    public int round = 1;
    public int match = 1;

    public BotTournamentSmall(string botOne, string botTwo, string botThree, string botFour)
    {
        competingBots.Add(botOne);
        competingBots.Add(botTwo);
        competingBots.Add(botThree);
        competingBots.Add(botFour);
    }

    public (string botWhite, string botBlack) nextGame()
    {
        if (round > 3)
        {
            return ("", "");
        }

        var (botOne_, botTwo_) = botTournamentMatches[round - 1][match - 1];
        string botOne = competingBots[botOne_ - 1];
        string botTwo = competingBots[botTwo_ - 1];

        match++;
        if (match > 2)
        {
            match = 1;
            round++;
        }

        System.Random rand = new System.Random();
        int randNumber = rand.Next(1, 3);

        if (randNumber == 1)
        {
            return (botOne, botTwo);
        }
        else
        {
            return (botTwo, botOne);
        }
    }

    public static readonly List<(int botOne, int botTwo)[]> botTournamentMatches = new List<(int botOne, int botTwo)[]>
    {
        new (int botOne, int botTwo)[]
        {
            (1, 4),
            (2, 3)
        },
        new (int botOne, int botTwo)[]
        {
            (1, 3),
            (2, 4)
        },
        new (int botOne, int botTwo)[]
        {
            (1, 2),
            (3, 4)
        }
    };
}

[Flags]
public enum PieceState : long
{
    None = 0,

    Shield = 1L << 0,
    Dematerialized = 1L << 1,
    Frozen = 1L << 2,
    Ghost = 1L << 3,
    Ghoul = 1L << 4,
    Feminist = 1L << 5,
    Oppressive = 1L << 6,
    Combustable = 1L << 7,
    Fragile = 1L << 8,
    Jailed = 1L << 9,
    Uncastle = 1L << 10,
    Rulebreaker = 1L << 11,
    Electric = 1L << 12,
    Crook = 1L << 13,
    Wall = 1L << 14,
    Medusa = 1L << 15,
    Hungry = 1L << 16,
    Piggyback = 1L << 17,
    Jockey = 1L << 18,
    Delayed = 1L << 19,
    Depressed = 1L << 20,
    Heartbroken = 1L << 21,
    Portal = 1L << 22,
    Bouncing = 1L << 23,
    CaptureTheFlag = 1L << 24,
    Defuser = 1L << 25,
    Switch = 1L << 26,
    Pawn = 1L << 27,
    Double = 1L << 28,
    Protective = 1L << 29,
    Scaredy = 1L << 30,
    Murderous = 1L << 31,
    Crowding = 1L << 32,
    Spitting = 1L << 33,
    Stacking = 1L << 34,
    Jailer = 1L << 35,
}

public enum PieceAbilities : long
{
    None = 0,

    Freeze = 1L << 0,
    Unfreeze = 1L << 1,
    Dematerialize = 1L << 2,
    Materialize = 1L << 3,
    Vomit = 1L << 4,
    CastleLeft = 1L << 5,
    CastleRight = 1L << 6,
    Spawn = 1L << 7,
    Spit = 1L << 8,
    Split = 1L << 9,
}

public class BotSeason
{
    public int Season { get; set; }
    public int Division { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }
    public int StartingElo { get; set; }
    public int? EndingElo { get; set; }
}

public class Bot
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Elo { get; set; }
    public int Division { get; set; }
    public string Profile { get; set; }
    public int Position { get; set; }

    public int WinsTotal { get; set; }
    public int LossesTotal { get; set; }
    public int DrawsTotal { get; set; }

    public string Creator { get; set; }

    public List<BotSeason> Seasons { get; set; }

    public int PeakElo { get; set; }
    public int MinElo { get; set; }

    public List<string> Competing { get; set; }
}

public class PieceMove
{
    public Piece piece { get; set; }
    public coords coords { get; set; }
    public int turnsToRemove { get; set; }

    public PieceMove(Piece piece, coords coords, int turnsToRemove)
    {
        this.piece = piece;
        this.coords = coords;
        this.turnsToRemove = turnsToRemove;
    }
}

public class DelayedQueue
{
    public List<PieceMove> _items = new List<PieceMove>();

    public int Count => _items.Count;

    public void Enqueue(PieceMove item)
    {
        _items.Add(item);
    }

    public PieceMove Dequeue()
    {
        if (_items.Count == 0)
        {
            return null;
        }

        PieceMove item = _items[0];
        _items.RemoveAt(0);
        return item;
    }

    public PieceMove Peek()
    {
        if (_items.Count == 0)
        {
            return null;
        }

        return _items[0];
    }

    public void Clear()
    {
        _items.Clear();
    }

    public void deIncrement()
    {
        foreach (PieceMove item in _items)
        {
            item.turnsToRemove--;
        }
    }
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

public class Move
{
    public Piece p;
    public coords coords;

    public Move(Piece piece, coords coords)
    {
        this.p = piece;
        this.coords = coords;
    }
}
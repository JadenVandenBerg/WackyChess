using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;
public class Queen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 9;
    public static readonly int rarityLevel = 0;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wQueen";
    public static readonly string bImage = "Images/Queens/bQueen";
    public string name = "Queen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Queen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class MurderousQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 9.5f;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "Moves like a normal queen but can also kill your own pieces.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Murderous;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = moveDefs.queenMoves;
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wMurderousQueen";
    public static readonly string bImage = "Images/Queens/bMurderousQueen";
    public string name = "Murderous Queen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public MurderousQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }
        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class GhostQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10.5f;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "Moves like a normal queen but can also go through your own pieces.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Ghost;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wGhostQueen";
    public static readonly string bImage = "Images/Queens/bGhostQueen";
    public string name = "Ghost Queen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public GhostQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }
        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class GhoulQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10f;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "Moves like a normal queen but your pieces can go through it.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Ghoul;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = {
    };
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wGhoulQueen";
    public static readonly string bImage = "Images/Queens/bGhoulQueen";
    public string name = "Ghoul Queen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public GhoulQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }
        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class OneTimeQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1f;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "Moves like a normal queen but can only move once.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] murderousAttacks = {
    };
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wOneTimeQueen";
    public static readonly string bImage = "Images/Queens/bOneTimeQueen";
    public string name = "One Time Queen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public OneTimeQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }
        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class ElectricQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 12.5f;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "This piece moves like a queen. On capture, there is a 50% chance the capturing piece will die.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Electric;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wElectricQueen";
    public static readonly string bImage = "Images/Queens/bElectricQueen";
    public string name = "Electric Queen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ElectricQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class InfiniteQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 17;
    public static readonly int rarityLevel = 5;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = -1;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wInfiniteQueen";
    public static readonly string bImage = "Images/Queens/bInfiniteQueen";
    public string name = "InfiniteQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public InfiniteQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class PortalQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 14;
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Portal;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wPortalQueen";
    public static readonly string bImage = "Images/Queens/bPortalQueen";
    public string name = "PortalQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PortalQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class AtomicQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 9;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = 0;
    public static readonly coords[] collateral = moveDefs.standardCollateral;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wAtomicQueen";
    public static readonly string bImage = "Images/Queens/bAtomicQueen";
    public string name = "AtomicQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public AtomicQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class LandmineQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = 1;
    public static readonly coords[] collateral = moveDefs.standardCollateral;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wLandmineQueen";
    public static readonly string bImage = "Images/Queens/bLandmineQueen";
    public string name = "LandmineQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LandmineQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class LiteQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wLiteQueen";
    public static readonly string bImage = "Images/Queens/bLiteQueen";
    public string name = "LiteQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LiteQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class SuperGhostQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 11.5f;
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "Moves like a normal queen but your pieces can go through it.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Ghost | PieceState.Ghoul;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = {
    };
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wSuperGhostQueen";
    public static readonly string bImage = "Images/Queens/bSuperGhostQueen";
    public string name = "SuperGhostQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SuperGhostQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }
        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class Minister : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wMinister";
    public static readonly string bImage = "Images/Queens/bMinister";
    public string name = "MinisterQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Minister(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class FragileQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Fragile;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wFragileQueen";
    public static readonly string bImage = "Images/Queens/bFragileQueen";
    public string name = "FragileQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FragileQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class Amazon : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 12;
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = moveDefs.knightMoves;
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wAmazon";
    public static readonly string bImage = "Images/Queens/bAmazon";
    public string name = "AmazonQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Amazon(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class ReverseMinister : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 3;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(1, 0), new coords(-1, 0), new coords(0, -1), new coords(0, 1) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wReverseMinister";
    public static readonly string bImage = "Images/Queens/bReverseMinister";
    public string name = "ReverseMinisterQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ReverseMinister(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class SinisterMinisterQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = moveDefs.knightMoves;
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wSinisterMinisterQueen";
    public static readonly string bImage = "Images/Queens/bSinisterMinisterQueen";
    public string name = "SinisterMinisterQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SinisterMinisterQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class MonochromeQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4.5f;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 2), new coords(0, 4), new coords(0, 6), new coords(0, 8), new coords(2, 0), new coords(4, 0), new coords(6, 0), new coords(8, 0), new coords(0, -2), new coords(0, -4), new coords(0, -6), new coords(0, -8), new coords(-2, 0), new coords(-4, 0), new coords(-6, 0), new coords(-8, 0) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wMonochromeQueen";
    public static readonly string bImage = "Images/Queens/bMonochromeQueen";
    public string name = "MonochromeQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public MonochromeQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class BouncingQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 11;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Bouncing;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wBouncingQueen";
    public static readonly string bImage = "Images/Queens/bBouncingQueen";
    public string name = "BouncingQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public BouncingQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class CrowdingQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Crowding;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wCrowdingQueen";
    public static readonly string bImage = "Images/Queens/bCrowdingQueen";
    public string name = "CrowdingQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public CrowdingQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class HungryQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Vomit;
    public PieceState states = PieceState.Hungry;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wHungryQueen";
    public static readonly string bImage = "Images/Queens/bHungryQueen";
    public string name = "HungryQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public HungryQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class CaptureTheFlagQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.CaptureTheFlag;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wCaptureTheFlagQueen";
    public static readonly string bImage = "Images/Queens/bCaptureTheFlagQueen";
    public string name = "CaptureTheFlagQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public CaptureTheFlagQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class FreezingQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10.5f;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Freeze;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wFreezingQueen";
    public static readonly string bImage = "Images/Queens/bFreezingQueen";
    public string name = "FreezingQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FreezingQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class CloningQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 14;
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Spawn;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wCloningQueen";
    public static readonly string bImage = "Images/Queens/bCloningQueen";
    public string name = "CloningQueen";

    public int flag = 0;
    public static readonly string spawnable = "Queen";
    public static readonly int numSpawns = 2;

    public CloningQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class UndeadQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10.5f;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Spawn;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wUndeadQueen";
    public static readonly string bImage = "Images/Queens/bUndeadQueen";
    public string name = "UndeadQueen";

    public int flag = 0;
    public static readonly string spawnable = "ZombiePawn";
    public static readonly int numSpawns = 3;

    public UndeadQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class DefuserQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Defuser;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wDefuserQueen";
    public static readonly string bImage = "Images/Queens/bDefuserQueen";
    public string name = "DefuserQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DefuserQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class SpittingQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 9;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Spit;
    public PieceState states = PieceState.Spitting;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = 1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wSpittingQueen";
    public static readonly string bImage = "Images/Queens/bSpittingQueen";
    public string name = "SpittingQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SpittingQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}
/*
public class PhantomQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 12;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Dematerialize;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wPhantomQueen";
    public static readonly string bImage = "Images/Queens/bPhantomQueen";
    public string name = "PhantomQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PhantomQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}
*/
public class StackingQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 14.5f;
    public static readonly int rarityLevel = 5;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Stacking;
    public int collateralType = -1;
    public coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public coords[] moves = Array.Empty<coords>();
    public coords[] oneTimeMoves = Array.Empty<coords>();
    public coords[] moveAndAttacks = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public coords[] conditionalAttacks = Array.Empty<coords>();
    public coords[] flagMove1 = Array.Empty<coords>();
    public coords[] flagMove2 = Array.Empty<coords>();
    public coords[] pushMoves = Array.Empty<coords>();
    public coords[] enPassantMoves = Array.Empty<coords>();
    public coords[] jumpAttacks = Array.Empty<coords>();
    public coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wStackingQueen";
    public static readonly string bImage = "Images/Queens/bStackingQueen";
    public string name = "StackingQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public StackingQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class JailQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 3;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Jailer;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wJailQueen";
    public static readonly string bImage = "Images/Queens/bJailQueen";
    public string name = "JailQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public JailQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class PiggybackQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Piggyback;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wPiggybackQueen";
    public static readonly string bImage = "Images/Queens/bPiggybackQueen";
    public string name = "PiggybackQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PiggybackQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class JockeyQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Queen";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Jockey;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wJockeyQueen";
    public static readonly string bImage = "Images/Queens/bJockeyQueen";
    public string name = "JockeyQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public JockeyQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class Feminist : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 3;
    public static readonly int rarityLevel = 2;
    public static readonly string baseType = "Queen";
    public coords startSquare = new coords(-1, -1);
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Feminist;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wFeminist";
    public static readonly string bImage = "Images/Queens/bFeminist";
    public string name = "Feminist";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Feminist(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class Medusa : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 15;
    public static readonly int rarityLevel = 5;
    public static readonly string baseType = "Queen";
    public coords startSquare = new coords(-1, -1);
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Medusa;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wMedusa";
    public static readonly string bImage = "Images/Queens/bMedusa";
    public string name = "Medusa";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 3; //Use numspawns for shield pawn gen

    public Medusa(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}

public class DelayedQueen : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 2;
    public static readonly string baseType = "Queen";
    public coords startSquare = new coords(-1, -1);
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Delayed;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.queenMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Queens/wDelayedQueen";
    public static readonly string bImage = "Images/Queens/bDelayedQueen";
    public string name = "DelayedQueen";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DelayedQueen(int color, bool online, bool simulated)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
            go.name = name;
        }
        else if (simulated)
        {
            go = null;
        }
        else
        {
            if (go == null) go = new GameObject();
            go.name = name;
        }

        this.color = color;

        if (!simulated)
        {
            go.name = name;

            HelperFunctions.UpdateMovesForColor(this);

            Image s = go.AddComponent<Image>();
            Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

            s.sprite = sp;
            s.preserveAspect = true;
        }
    }
}


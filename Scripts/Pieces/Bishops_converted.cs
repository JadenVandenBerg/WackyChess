using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;

public class Bishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 3;
    public static readonly int rarityLevel = 0;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wBishop";
    public static readonly string bImage = "Images/Bishops/bBishop";
    public string name = "Bishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Bishop(int color, bool online, bool simulated)
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

public class MurderousBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4f;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
    public static readonly string description = "Moves like a bishop but you can kill your own pieces.";
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
    public static readonly coords[] murderousAttacks = moveDefs.bishopMoves;
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

    public static readonly string wImage = "Images/Bishops/wMurderousBishop";
    public static readonly string bImage = "Images/Bishops/bMurderousBishop";
    public string name = "MurderousBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public MurderousBishop(int color, bool online, bool simulated)
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

public class GhostBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
    public static readonly string description = "Moves like a bishop but you can go through your own pieces.";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
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

    public static readonly string wImage = "Images/Bishops/wGhostBishop";
    public static readonly string bImage = "Images/Bishops/bGhostBishop";
    public string name = "Ghost Bishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public GhostBishop(int color, bool online, bool simulated)
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

public class GhoulBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4f;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
    public static readonly string description = "Moves like a bishop but your pieces can g=o through it.";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
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

    public static readonly string wImage = "Images/Bishops/wGhoulBishop";
    public static readonly string bImage = "Images/Bishops/bGhoulBishop";
    public string name = "Ghoul Bishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public GhoulBishop(int color, bool online, bool simulated)
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

public class OneTimeBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1f;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
    public static readonly string description = "Moves like a bishop but can only move once.";
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
    public static readonly coords[] oneTimeMoveAndAttacks = moveDefs.bishopMoves;
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

    public static readonly string wImage = "Images/Bishops/wOneTimeBishop";
    public static readonly string bImage = "Images/Bishops/bOneTimeBishop";
    public string name = "One Time Bishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public OneTimeBishop(int color, bool online, bool simulated)
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

public class ElectricBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4.5f;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
    public static readonly string description = "This piece moves like a bishop. On capture, the capturing piece has a 50% chance of dying.";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wElectricBishop";
    public static readonly string bImage = "Images/Bishops/bElectricBishop";
    public string name = "Electric Bishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ElectricBishop(int color, bool online, bool simulated)
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

public class InfiniteBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 9f;
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wInfiniteBishop";
    public static readonly string bImage = "Images/Bishops/bInfiniteBishop";
    public string name = "InfiniteBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public InfiniteBishop(int color, bool online, bool simulated)
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

public class PortalBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wPortalBishop";
    public static readonly string bImage = "Images/Bishops/bPortalBishop";
    public string name = "PortalBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PortalBishop(int color, bool online, bool simulated)
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

public class AtomicBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 7;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wAtomicBishop";
    public static readonly string bImage = "Images/Bishops/bAtomicBishop";
    public string name = "AtomicBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public AtomicBishop(int color, bool online, bool simulated)
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

public class LandmineBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wLandmineBishop";
    public static readonly string bImage = "Images/Bishops/bLandmineBishop";
    public string name = "LandmineBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LandmineBishop(int color, bool online, bool simulated)
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

public class ColorChangingBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moves = { new coords(1, 0), new coords(-1, 0) };
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wColorChangingBishop";
    public static readonly string bImage = "Images/Bishops/bColorChangingBishop";
    public string name = "ColorChangingBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ColorChangingBishop(int color, bool online, bool simulated)
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

public class LiteBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wLiteBishop";
    public static readonly string bImage = "Images/Bishops/bLiteBishop";
    public string name = "LiteBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LiteBishop(int color, bool online, bool simulated)
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

public class SuperGhostBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 5f;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
    public static readonly string description = "Moves like a bishop but your pieces can go through it.";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
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

    public static readonly string wImage = "Images/Bishops/wSuperGhostBishop";
    public static readonly string bImage = "Images/Bishops/bSuperGhostBishop";
    public string name = "SuperGhostBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SuperGhostBishop(int color, bool online, bool simulated)
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

public class Princess : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = moveDefs.knightMoves;
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wPrincess";
    public static readonly string bImage = "Images/Bishops/bPrincess";
    public string name = "BishopPrincess";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Princess(int color, bool online, bool simulated)
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

public class FragileBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wFragileBishop";
    public static readonly string bImage = "Images/Bishops/bFragileBishop";
    public string name = "FragileBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FragileBishop(int color, bool online, bool simulated)
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

public class RoyalBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4.5f;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wRoyalBishop";
    public static readonly string bImage = "Images/Bishops/bRoyalBishop";
    public string name = "RoyalBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public RoyalBishop(int color, bool online, bool simulated)
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

public class BouncingBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wBouncingBishop";
    public static readonly string bImage = "Images/Bishops/bBouncingBishop";
    public string name = "BouncingBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public BouncingBishop(int color, bool online, bool simulated)
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

public class CrowdingBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wCrowdingBishop";
    public static readonly string bImage = "Images/Bishops/bCrowdingBishop";
    public string name = "CrowdingBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public CrowdingBishop(int color, bool online, bool simulated)
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

public class HungryBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wHungryBishop";
    public static readonly string bImage = "Images/Bishops/bHungryBishop";
    public string name = "HungryBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public HungryBishop(int color, bool online, bool simulated)
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

public class CaptureTheFlagBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wCaptureTheFlagBishop";
    public static readonly string bImage = "Images/Bishops/bCaptureTheFlagBishop";
    public string name = "CaptureTheFlagBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public CaptureTheFlagBishop(int color, bool online, bool simulated)
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

public class FreezingBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wFreezingBishop";
    public static readonly string bImage = "Images/Bishops/bFreezingBishop";
    public string name = "FreezingBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FreezingBishop(int color, bool online, bool simulated)
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

public class CloningBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wCloningBishop";
    public static readonly string bImage = "Images/Bishops/bCloningBishop";
    public string name = "CloningBishop";

    public int flag = 0;
    public static readonly string spawnable = "Bishop";
    public static readonly int numSpawns = 2;

    public CloningBishop(int color, bool online, bool simulated)
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

public class UndeadBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wUndeadBishop";
    public static readonly string bImage = "Images/Bishops/bUndeadBishop";
    public string name = "UndeadBishop";

    public int flag = 0;
    public static readonly string spawnable = "ZombiePawn";
    public static readonly int numSpawns = 3;

    public UndeadBishop(int color, bool online, bool simulated)
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

public class PromotionBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "Rook";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wPromotionBishop";
    public static readonly string bImage = "Images/Bishops/bPromotionBishop";
    public string name = "PromotionBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PromotionBishop(int color, bool online, bool simulated)
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

public class DefuserBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wDefuserBishop";
    public static readonly string bImage = "Images/Bishops/bDefuserBishop";
    public string name = "DefuserBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DefuserBishop(int color, bool online, bool simulated)
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

public class SpittingBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 3;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wSpittingBishop";
    public static readonly string bImage = "Images/Bishops/bSpittingBishop";
    public string name = "SpittingBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SpittingBishop(int color, bool online, bool simulated)
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
public class PhantomBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wPhantomBishop";
    public static readonly string bImage = "Images/Bishops/bPhantomBishop";
    public string name = "PhantomBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PhantomBishop(int color, bool online, bool simulated)
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
public class StackingBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 7;
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public coords[] moveAndAttacks = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public coords[] conditionalAttacks = Array.Empty<coords>();
    public coords[] jumpAttacks = Array.Empty<coords>();
    public coords[] attacks = Array.Empty<coords>();
    public coords[] flagMove1 = Array.Empty<coords>();
    public coords[] flagMove2 = Array.Empty<coords>();
    public coords[] pushMoves = Array.Empty<coords>();
    public coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wStackingBishop";
    public static readonly string bImage = "Images/Bishops/bStackingBishop";
    public string name = "StackingBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public StackingBishop(int color, bool online, bool simulated)
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

public class JailBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wJailBishop";
    public static readonly string bImage = "Images/Bishops/bJailBishop";
    public string name = "JailBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public JailBishop(int color, bool online, bool simulated)
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

public class PiggybackBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wPiggybackBishop";
    public static readonly string bImage = "Images/Bishops/bPiggybackBishop";
    public string name = "PiggybackBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PiggybackBishop(int color, bool online, bool simulated)
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

public class JockeyBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wJockeyBishop";
    public static readonly string bImage = "Images/Bishops/bJockeyBishop";
    public string name = "JockeyBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public JockeyBishop(int color, bool online, bool simulated)
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

public class DelayedBishop : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly int rarityLevel = 2;
    public static readonly string baseType = "Bishop";
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
    public static readonly coords[] moveAndAttacks = moveDefs.bishopMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Bishops/wDelayedBishop";
    public static readonly string bImage = "Images/Bishops/bDelayedBishop";
    public string name = "DelayedBishop";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DelayedBishop(int color, bool online, bool simulated)
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
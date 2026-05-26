using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;

public class Rook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 0;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wRook";
    public static readonly string bImage = "Images/Rooks/bRook";
    public string name  = "Rook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Rook(int color, bool online, bool simulated)
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

public class MurderousRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "Moves like a rook but you can kill your own pieces.";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Murderous;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = moveDefs.rookMoves;
    public bool condition  = false;
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wMurderousRook";
    public static readonly string bImage = "Images/Rooks/bMurderousRook";
    public string name  = "MurderousRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public MurderousRook(int color, bool online, bool simulated)
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

public class GhostRook : Piece
{
    public bool disabled = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 6.5f;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "Moves like a rook but you can go through your own pieces.";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Ghost;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public bool condition  = false;
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wGhostRook";
    public static readonly string bImage = "Images/Rooks/bGhostRook";
    public string name  = "Ghost Rook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public GhostRook(int color, bool online, bool simulated)
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

public class GhoulRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 5.5f;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "Moves like a rook but your pieces can go through it.";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Ghoul;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public bool condition  = false;
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wGhoulRook";
    public static readonly string bImage = "Images/Rooks/bGhoulRook";
    public string name  = "Ghoul Rook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public GhoulRook(int color, bool online, bool simulated)
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

public class OneTimeRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 1f;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "Moves like a rook but can only move once.";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public bool condition  = false;
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wOneTimeRook";
    public static readonly string bImage = "Images/Rooks/bOneTimeRook";
    public string name  = "One Time Rook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public OneTimeRook(int color, bool online, bool simulated)
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

public class ElectricRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 7;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "This piece moves like a rook. On capture, the capturing piece has a 50% chance of dying.";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Electric;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wElectricRook";
    public static readonly string bImage = "Images/Rooks/bElectricRook";
    public string name  = "Electric Rook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ElectricRook(int color, bool online, bool simulated)
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

public class InfiniteRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 4;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = -1;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wInfiniteRook";
    public static readonly string bImage = "Images/Rooks/bInfiniteRook";
    public string name  = "InfiniteRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public InfiniteRook(int color, bool online, bool simulated)
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

public class PortalRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 7.5f;
    public static readonly int rarityLevel = 4;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Portal;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wPortalRook";
    public static readonly string bImage = "Images/Rooks/bPortalRook";
    public string name  = "PortalRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PortalRook(int color, bool online, bool simulated)
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

public class AtomicRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 7;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = 0;
    public static readonly coords[] collateral  = moveDefs.standardCollateral;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wAtomicRook";
    public static readonly string bImage = "Images/Rooks/bAtomicRook";
    public string name  = "AtomicRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public AtomicRook(int color, bool online, bool simulated)
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

public class LandmineRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 8;
    public static readonly int rarityLevel = 4;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = 1;
    public static readonly coords[] collateral  = moveDefs.standardCollateral;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wLandmineRook";
    public static readonly string bImage = "Images/Rooks/bLandmineRook";
    public string name  = "LandmineRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LandmineRook(int color, bool online, bool simulated)
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

public class LiteRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 0;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wLiteRook";
    public static readonly string bImage = "Images/Rooks/bLiteRook";
    public string name  = "LiteRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LiteRook(int color, bool online, bool simulated)
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

public class SuperGhostRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 7f;
    public static readonly int rarityLevel = 4;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "Moves like a rook but your pieces can go through it.";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Ghost | PieceState.Ghoul;
    public string secondaryState  = "Ghost";
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public bool condition  = false;
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wSuperGhostRook";
    public static readonly string bImage = "Images/Rooks/bSuperGhostRook";
    public string name  = "SuperGhostRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SuperGhostRook(int color, bool online, bool simulated)
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

public class Empress : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 7;
    public static readonly int rarityLevel = 4;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = moveDefs.knightMoves;
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wEmpress";
    public static readonly string bImage = "Images/Rooks/bEmpress";
    public string name  = "RookEmpress";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Empress(int color, bool online, bool simulated)
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

public class FragileRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 3;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Fragile;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wFragileRook";
    public static readonly string bImage = "Images/Rooks/bFragileRook";
    public string name  = "FragileRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FragileRook(int color, bool online, bool simulated)
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

public class RoyalRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 7;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = { new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0), new coords(1, -1), new coords(1, 1), new coords(-1, 1), new coords(-1, -1) };
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wRoyalRook";
    public static readonly string bImage = "Images/Rooks/bRoyalRook";
    public string name  = "RoyalRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public RoyalRook(int color, bool online, bool simulated)
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

public class MonochromeRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 2;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = { new coords(0, 2), new coords(0, 4), new coords(0, 6), new coords(0, 8), new coords(2, 0), new coords(4, 0), new coords(6, 0), new coords(8, 0), new coords(0, -2), new coords(0, -4), new coords(0, -6), new coords(0, -8), new coords(-2, 0), new coords(-4, 0), new coords(-6, 0), new coords(-8, 0) };
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wMonochromeRook";
    public static readonly string bImage = "Images/Rooks/bMonochromeRook";
    public string name  = "MonochromeRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public MonochromeRook(int color, bool online, bool simulated)
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

public class CrowdingRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Crowding;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wCrowdingRook";
    public static readonly string bImage = "Images/Rooks/bCrowdingRook";
    public string name  = "CrowdingRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public CrowdingRook(int color, bool online, bool simulated)
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

public class HungryRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.Vomit;
    public PieceState states  = PieceState.Hungry;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wHungryRook";
    public static readonly string bImage = "Images/Rooks/bHungryRook";
    public string name  = "HungryRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public HungryRook(int color, bool online, bool simulated)
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

public class CaptureTheFlagRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 7;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.CaptureTheFlag;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wCaptureTheFlagRook";
    public static readonly string bImage = "Images/Rooks/bCaptureTheFlagRook";
    public string name  = "CaptureTheFlagRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public CaptureTheFlagRook(int color, bool online, bool simulated)
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

public class FreezingRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 7;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.Freeze;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wFreezingRook";
    public static readonly string bImage = "Images/Rooks/bFreezingRook";
    public string name  = "FreezingRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FreezingRook(int color, bool online, bool simulated)
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

public class CloningRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 10;
    public static readonly int rarityLevel = 4;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.Spawn;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wCloningRook";
    public static readonly string bImage = "Images/Rooks/bCloningRook";
    public string name  = "CloningRook";

    public int flag  = 0;
    public static readonly string spawnable = "Rook";
    public static readonly int numSpawns = 2;

    public CloningRook(int color, bool online, bool simulated)
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

public class UndeadRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.Spawn;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wUndeadRook";
    public static readonly string bImage = "Images/Rooks/bUndeadRook";
    public string name  = "UndeadRook";

    public int flag  = 0;
    public static readonly string spawnable = "ZombiePawn";
    public static readonly int numSpawns = 3;

    public UndeadRook(int color, bool online, bool simulated)
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

public class PromotionRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "Queen";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wPromotionRook";
    public static readonly string bImage = "Images/Rooks/bPromotionRook";
    public string name  = "PromotionRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PromotionRook(int color, bool online, bool simulated)
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

public class DefuserRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Defuser;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wDefuserRook";
    public static readonly string bImage = "Images/Rooks/bDefuserRook";
    public string name  = "DefuserRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DefuserRook(int color, bool online, bool simulated)
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

public class SpittingRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.Spit;
    public PieceState states  = PieceState.Spitting;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = 1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wSpittingRook";
    public static readonly string bImage = "Images/Rooks/bSpittingRook";
    public string name  = "SpittingRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SpittingRook(int color, bool online, bool simulated)
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
public class PhantomRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 9;
    public static readonly int rarityLevel = 4;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.Dematerialize;
    public PieceState states  = PieceState.None;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wPhantomRook";
    public static readonly string bImage = "Images/Rooks/bPhantomRook";
    public string name  = "PhantomRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PhantomRook(int color, bool online, bool simulated)
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

public class StackingRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 9;
    public static readonly int rarityLevel = 5;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Stacking;
    public int collateralType  = -1;
    public coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public coords[] moves  = Array.Empty<coords>();
    public coords[] oneTimeMoves  = Array.Empty<coords>();
    public coords[] moveAndAttacks  = moveDefs.rookMoves;
    public coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public coords[] murderousAttacks  = Array.Empty<coords>();
    public coords[] flagMove1  = Array.Empty<coords>();
    public coords[] flagMove2  = Array.Empty<coords>();
    public coords[] pushMoves  = Array.Empty<coords>();
    public coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public coords[] conditionalAttacks  = Array.Empty<coords>();
    public coords[] jumpAttacks  = Array.Empty<coords>();
    public coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wStackingRook";
    public static readonly string bImage = "Images/Rooks/bStackingRook";
    public string name  = "StackingRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public StackingRook(int color, bool online, bool simulated)
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

public class JailRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly int rarityLevel = 2;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Jailer;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wJailRook";
    public static readonly string bImage = "Images/Rooks/bJailRook";
    public string name  = "JailRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public JailRook(int color, bool online, bool simulated)
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

public class Crook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 12;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Crook;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wCrook";
    public static readonly string bImage = "Images/Rooks/bCrook";
    public string name  = "Crook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Crook(int color, bool online, bool simulated)
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

public class PiggybackRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 3;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Piggyback;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wPiggybackRook";
    public static readonly string bImage = "Images/Rooks/bPiggybackRook";
    public string name  = "PiggybackRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PiggybackRook(int color, bool online, bool simulated)
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

public class JockeyRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly int rarityLevel = 0;
    public coords startSquare  = new coords(-1, -1);
    public static readonly string baseType = "Rook";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Jockey;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wJockeyRook";
    public static readonly string bImage = "Images/Rooks/bJockeyRook";
    public string name  = "JockeyRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public JockeyRook(int color, bool online, bool simulated)
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

public class DelayedRook : Piece
{
    public bool disabled  = false;
    public int color  = 1; //1 White, -1 Black
    public static readonly float points = 3;
    public static readonly int rarityLevel = 2;
    public static readonly string baseType = "Rook";
    public coords startSquare  = new coords(-1, -1);
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive  = 1;
    public int lives  = 0;
    public PieceAbilities abilities  = PieceAbilities.None;
    public PieceState states  = PieceState.Delayed;
    public int collateralType  = -1;
    public static readonly coords[] collateral  = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage  = null;
    public static readonly coords[] moves  = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves  = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks  = moveDefs.rookMoves;
    public static readonly coords[] oneTimeMoveAndAttacks  = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks  = Array.Empty<coords>();
    public static readonly coords[] flagMove1  = Array.Empty<coords>();
    public static readonly coords[] flagMove2  = Array.Empty<coords>();
    public static readonly coords[] pushMoves  = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves  = Array.Empty<coords>();
    public bool condition  = false; //Condition: Castle
    public static readonly coords[] conditionalAttacks  = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks  = Array.Empty<coords>();
    public static readonly coords[] attacks  = Array.Empty<coords>();

    public coords position  = new coords(0, 0);
    public GameObject go  = null;
    public bool hasMoved  = false;

    public static readonly string wImage = "Images/Rooks/wDelayedRook";
    public static readonly string bImage = "Images/Rooks/bDelayedRook";
    public string name  = "DelayedRook";

    public int flag  = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DelayedRook(int color, bool online, bool simulated)
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

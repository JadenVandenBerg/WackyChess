using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;

public class King : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wKing";
    public static readonly string bImage = "Images/Kings/bKing";
    public string name = "King";
    public static readonly int rarityLevel = 0;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public King(int color, bool online, bool simulated)
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

public class MurderousKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "Like a normal king, but you can kill your own pieces to get out of a pinch!";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
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
    public static readonly coords[] murderousAttacks = moveDefs.kingMoves;
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Kings/wMurderousKing";
    public static readonly string bImage = "Images/Kings/bMurderousKing";
    public string name = "Murderous King";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public MurderousKing(int color, bool online, bool simulated)
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

public class GhoulKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "Like a normal king, but other pieces can go through it.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
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
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Kings/wGhoulKing";
    public static readonly string bImage = "Images/Kings/bGhoulKing";
    public string name = "Ghoul King";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public GhoulKing(int color, bool online, bool simulated)
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

public class OneTimeKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -6;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "Like a normal king, but can only move once.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
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
    public static readonly coords[] oneTimeMoveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Kings/wOneTimeKing";
    public static readonly string bImage = "Images/Kings/bOneTimeKing";
    public string name = "One Time King";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public OneTimeKing(int color, bool online, bool simulated)
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

public class ElectricKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wElectricKing";
    public static readonly string bImage = "Images/Kings/bElectricKing";
    public string name = "Electric King";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "Protect this piece! It cannot move very fast, but if this piece dies or gets checkmated you lose the game.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Electric;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ElectricKing(int color, bool online, bool simulated)
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

public class PortalKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 5;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wPortalKing";
    public static readonly string bImage = "Images/Kings/bPortalKing";
    public string name = "PortalKing";
    public static readonly int rarityLevel = 5;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Portal;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PortalKing(int color, bool online, bool simulated)
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

public class AtomicKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wAtomicKing";
    public static readonly string bImage = "Images/Kings/bAtomicKing";
    public string name = "AtomicKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.None;
    public int collateralType = 0;
    public static readonly coords[] collateral = moveDefs.standardCollateral;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public AtomicKing(int color, bool online, bool simulated)
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

public class LandmineKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wLandmineKing";
    public static readonly string bImage = "Images/Kings/bLandmineKing";
    public string name = "LandmineKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.None;
    public int collateralType = 1;
    public static readonly coords[] collateral = moveDefs.standardCollateral;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LandmineKing(int color, bool online, bool simulated)
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

public class LiteKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -8;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false; 
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wLiteKing";
    public static readonly string bImage = "Images/Kings/bLiteKing";
    public string name = "LiteKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LiteKing(int color, bool online, bool simulated)
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

public class HyperFastKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -5;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(3, 3), new coords(-3, 3), new coords(3, -3), new coords(-3, -3), new coords(0, 3), new coords(3, 0), new coords(0, -3), new coords(-3, 0) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false; 
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wHyperFastKing";
    public static readonly string bImage = "Images/Kings/bHyperFastKing";
    public string name = "HyperFastKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public HyperFastKing(int color, bool online, bool simulated)
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

public class FastKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -2;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(2, 2), new coords(-2, 2), new coords(2, -2), new coords(-2, -2), new coords(0, 2), new coords(2, 0), new coords(0, -2), new coords(-2, 0) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wFastKing";
    public static readonly string bImage = "Images/Kings/bFastKing";
    public string name = "FastKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FastKing(int color, bool online, bool simulated)
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

public class FragileKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -4;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wFragileKing";
    public static readonly string bImage = "Images/Kings/bFragileKing";
    public string name = "FragileKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Fragile;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FragileKing(int color, bool online, bool simulated)
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

public class SlidingKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(1, 0), new coords(-1, 0), new coords(2, 0), new coords(-2, 0), new coords(3, 0), new coords(-3, 0), new coords(4, 0), new coords(-4, 0), new coords(5, 0), new coords(-5, 0), new coords(6, 0), new coords(-6, 0), new coords(7, 0), new coords(-7, 0), new coords(8, 0), new coords(-8, 0) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wSlidingKing";
    public static readonly string bImage = "Images/Kings/bSlidingKing";
    public string name = "SlidingKing";
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SlidingKing(int color, bool online, bool simulated)
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

public class CrowdingKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wCrowdingKing";
    public static readonly string bImage = "Images/Kings/bCrowdingKing";
    public string name = "CrowdingKing";
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Crowding;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public CrowdingKing(int color, bool online, bool simulated)
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

public class HungryKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;  
    public static readonly coords[] conditionalAttacks = {  };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wHungryKing";
    public static readonly string bImage = "Images/Kings/bHungryKing";
    public string name = "HungryKing";
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight | PieceAbilities.Vomit;
    public PieceState states = PieceState.Hungry;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public HungryKing(int color, bool online, bool simulated)
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

public class FreezingKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wFreezingKing";
    public static readonly string bImage = "Images/Kings/bFreezingKing";
    public string name = "FreezingKing";
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight | PieceAbilities.Freeze;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FreezingKing(int color, bool online, bool simulated)
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

public class UndeadKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wUndeadKing";
    public static readonly string bImage = "Images/Kings/bUndeadKing";
    public string name = "UndeadKing";
    public static readonly int rarityLevel = 3;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight | PieceAbilities.Spawn;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "ZombiePawn";
    public static readonly int numSpawns = 3;

    public UndeadKing(int color, bool online, bool simulated)
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

public class DefuserKing : Piece
{
    public bool disabled = false;
    public int color = 2; //1 White, -1 Black
    public static readonly float points = 0;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wDefuserKing";
    public static readonly string bImage = "Images/Kings/bDefuserKing";
    public string name = "DefuserKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Defuser;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DefuserKing(int color, bool online, bool simulated)
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

public class Overlord : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 8;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0), new coords(2, 2), new coords(-2, 2), new coords(2, -2), new coords(-2, -2), new coords(0, 2), new coords(2, 0), new coords(0, -2), new coords(-2, 0) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wOverlord";
    public static readonly string bImage = "Images/Kings/bOverlord";
    public string name = "Overlord";
    public static readonly int rarityLevel = 5;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public Overlord(int color, bool online, bool simulated)
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

public class BadKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -1;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wBadKing";
    public static readonly string bImage = "Images/Kings/bBadKing";
    public string name = "BadKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Uncastle;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public BadKing(int color, bool online, bool simulated)
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

public class SpittingKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wSpittingKing";
    public static readonly string bImage = "Images/Kings/bSpittingKing";
    public string name = "SpittingKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight | PieceAbilities.Spit;
    public PieceState states = PieceState.Spitting;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = 1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SpittingKing(int color, bool online, bool simulated)
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

public class SwitchingKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wSwitchingKing";
    public static readonly string bImage = "Images/Kings/bSwitchingKing";
    public string name = "SwitchingKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Switch;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SwitchingKing(int color, bool online, bool simulated)
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

public class StackingKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 9;
    public coords[] moves = Array.Empty<coords>();
    public coords[] oneTimeMoves = Array.Empty<coords>();
    public coords[] moveAndAttacks = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public coords[] conditionalAttacks = Array.Empty<coords>();
    public coords[] attacks = Array.Empty<coords>();
    public coords[] jumpAttacks = Array.Empty<coords>();
    public coords[] flagMove1 = Array.Empty<coords>();
    public coords[] flagMove2 = Array.Empty<coords>();
    public coords[] pushMoves = Array.Empty<coords>();
    public coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wStackingKing";
    public static readonly string bImage = "Images/Kings/bStackingKing";
    public string name = "StackingKing";
    public static readonly int rarityLevel = 5;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Stacking;
    public int collateralType = -1;
    public coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public StackingKing(int color, bool online, bool simulated)
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

public class PiggybackKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>(); 
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wPiggybackKing";
    public static readonly string bImage = "Images/Kings/bPiggybackKing";
    public string name = "PiggybackKing";
    public PieceState states = PieceState.Piggyback;
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public PiggybackKing(int color, bool online, bool simulated)
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

public class ScaredyKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 6;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>(); //Flag: Not in check
    public static readonly coords[] flagMove2 = { new coords(2, 2), new coords(-2, 2), new coords(2, -2), new coords(-2, -2), new coords(0, 2), new coords(2, 0), new coords(0, -2), new coords(-2, 0) }; //Flag: In check
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wScaredyKing";
    public static readonly string bImage = "Images/Kings/bScaredyKing";
    public string name = "ScaredyKing";
    public static readonly int rarityLevel = 4;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Scaredy;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ScaredyKing(int color, bool online, bool simulated)
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

public class DepressedKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -3;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = moveDefs.kingMoves; //Flag: Not in check
    public static readonly coords[] flagMove2 = Array.Empty<coords>(); //Flag: In check
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wDepressedKing";
    public static readonly string bImage = "Images/Kings/bDepressedKing";
    public string name = "DepressedKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Depressed;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DepressedKing(int color, bool online, bool simulated)
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

public class HeartbrokenKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -2;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wHeartbrokenKing";
    public static readonly string bImage = "Images/Kings/bHeartbrokenKing";
    public string name = "HeartbrokenKing";
    public static readonly int rarityLevel = 2;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "King";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Heartbroken;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public HeartbrokenKing(int color, bool online, bool simulated)
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

public class RulebreakerKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly int rarityLevel = 3;
    public static readonly string baseType = "King";
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wRulebreakerKing";
    public static readonly string bImage = "Images/Kings/bRulebreakerKing";
    public string name = "RulebreakerKing";
    public coords startSquare = new coords(-1, -1);
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Rulebreaker;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public RulebreakerKing(int color, bool online, bool simulated)
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

public class DelayedKing : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -5;
    public static readonly int rarityLevel = 2;
    public static readonly string baseType = "King";
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Kings/wDelayedKing";
    public static readonly string bImage = "Images/Kings/bDelayedKing";
    public string name = "DelayedKing";
    public coords startSquare = new coords(-1, -1);
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states = PieceState.Delayed;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DelayedKing(int color, bool online, bool simulated)
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

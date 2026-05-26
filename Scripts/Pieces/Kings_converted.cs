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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wKing";
    public String bImage { get; set; } = "Images/Kings/bKing";
    public String name { get; set; } = "King";
    public int rarityLevel { get; set; } = 0;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "Like a normal king, but you can kill your own pieces to get out of a pinch!";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Murderous;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Kings/wMurderousKing";
    public String bImage { get; set; } = "Images/Kings/bMurderousKing";
    public String name { get; set; } = "Murderous King";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "Like a normal king, but other pieces can go through it.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Ghoul;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Kings/wGhoulKing";
    public String bImage { get; set; } = "Images/Kings/bGhoulKing";
    public String name { get; set; } = "Ghoul King";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -6;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "Like a normal king, but can only move once.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Kings/wOneTimeKing";
    public String bImage { get; set; } = "Images/Kings/bOneTimeKing";
    public String name { get; set; } = "One Time King";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wElectricKing";
    public String bImage { get; set; } = "Images/Kings/bElectricKing";
    public String name { get; set; } = "Electric King";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "Protect this piece! It cannot move very fast, but if this piece dies or gets checkmated you lose the game.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Electric;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wPortalKing";
    public String bImage { get; set; } = "Images/Kings/bPortalKing";
    public String name { get; set; } = "PortalKing";
    public int rarityLevel { get; set; } = 5;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Portal;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wAtomicKing";
    public String bImage { get; set; } = "Images/Kings/bAtomicKing";
    public String name { get; set; } = "AtomicKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = 0;
    public coords[] collateral { get; set; } = { new coords(0, 0), new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wLandmineKing";
    public String bImage { get; set; } = "Images/Kings/bLandmineKing";
    public String name { get; set; } = "LandmineKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = 1;
    public coords[] collateral { get; set; } = { new coords(0, 0), new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -8;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false; 
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wLiteKing";
    public String bImage { get; set; } = "Images/Kings/bLiteKing";
    public String name { get; set; } = "LiteKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -5;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(3, 3), new coords(-3, 3), new coords(3, -3), new coords(-3, -3), new coords(0, 3), new coords(3, 0), new coords(0, -3), new coords(-3, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false; 
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wHyperFastKing";
    public String bImage { get; set; } = "Images/Kings/bHyperFastKing";
    public String name { get; set; } = "HyperFastKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -2;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(2, 2), new coords(-2, 2), new coords(2, -2), new coords(-2, -2), new coords(0, 2), new coords(2, 0), new coords(0, -2), new coords(-2, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wFastKing";
    public String bImage { get; set; } = "Images/Kings/bFastKing";
    public String name { get; set; } = "FastKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -4;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wFragileKing";
    public String bImage { get; set; } = "Images/Kings/bFragileKing";
    public String name { get; set; } = "FragileKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Fragile;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 0), new coords(-1, 0), new coords(2, 0), new coords(-2, 0), new coords(3, 0), new coords(-3, 0), new coords(4, 0), new coords(-4, 0), new coords(5, 0), new coords(-5, 0), new coords(6, 0), new coords(-6, 0), new coords(7, 0), new coords(-7, 0), new coords(8, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wSlidingKing";
    public String bImage { get; set; } = "Images/Kings/bSlidingKing";
    public String name { get; set; } = "SlidingKing";
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wCrowdingKing";
    public String bImage { get; set; } = "Images/Kings/bCrowdingKing";
    public String name { get; set; } = "CrowdingKing";
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Crowding;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public coords[] conditionalAttacks { get; set; } = {  };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wHungryKing";
    public String bImage { get; set; } = "Images/Kings/bHungryKing";
    public String name { get; set; } = "HungryKing";
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight | PieceAbilities.Vomit;
    public PieceState states { get; set; } = PieceState.Hungry;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wFreezingKing";
    public String bImage { get; set; } = "Images/Kings/bFreezingKing";
    public String name { get; set; } = "FreezingKing";
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight | PieceAbilities.Freeze;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wUndeadKing";
    public String bImage { get; set; } = "Images/Kings/bUndeadKing";
    public String name { get; set; } = "UndeadKing";
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight | PieceAbilities.Spawn;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "ZombiePawn";
    public int numSpawns { get; set; } = 3;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 2; //1 White, -1 Black
    public float points { get; set; } = 0;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wDefuserKing";
    public String bImage { get; set; } = "Images/Kings/bDefuserKing";
    public String name { get; set; } = "DefuserKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Defuser;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 8;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0), new coords(2, 2), new coords(-2, 2), new coords(2, -2), new coords(-2, -2), new coords(0, 2), new coords(2, 0), new coords(0, -2), new coords(-2, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wOverlord";
    public String bImage { get; set; } = "Images/Kings/bOverlord";
    public String name { get; set; } = "Overlord";
    public int rarityLevel { get; set; } = 5;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -1;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wBadKing";
    public String bImage { get; set; } = "Images/Kings/bBadKing";
    public String name { get; set; } = "BadKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Uncastle;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wSpittingKing";
    public String bImage { get; set; } = "Images/Kings/bSpittingKing";
    public String name { get; set; } = "SpittingKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight | PieceAbilities.Spit;
    public PieceState states { get; set; } = PieceState.Spitting;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = 1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wSwitchingKing";
    public String bImage { get; set; } = "Images/Kings/bSwitchingKing";
    public String name { get; set; } = "SwitchingKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Switch;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wStackingKing";
    public String bImage { get; set; } = "Images/Kings/bStackingKing";
    public String name { get; set; } = "StackingKing";
    public int rarityLevel { get; set; } = 5;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Stacking;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { }; 
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wPiggybackKing";
    public String bImage { get; set; } = "Images/Kings/bPiggybackKing";
    public String name { get; set; } = "PiggybackKing";
    public PieceState states { get; set; } = PieceState.Piggyback;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 6;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { }; //Flag: Not in check
    public coords[] flagMove2 { get; set; } = { new coords(2, 2), new coords(-2, 2), new coords(2, -2), new coords(-2, -2), new coords(0, 2), new coords(2, 0), new coords(0, -2), new coords(-2, 0) }; //Flag: In check
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wScaredyKing";
    public String bImage { get; set; } = "Images/Kings/bScaredyKing";
    public String name { get; set; } = "ScaredyKing";
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Scaredy;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -3;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) }; //Flag: Not in check
    public coords[] flagMove2 { get; set; } = { }; //Flag: In check
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wDepressedKing";
    public String bImage { get; set; } = "Images/Kings/bDepressedKing";
    public String name { get; set; } = "DepressedKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Depressed;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -2;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wHeartbrokenKing";
    public String bImage { get; set; } = "Images/Kings/bHeartbrokenKing";
    public String name { get; set; } = "HeartbrokenKing";
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "King";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Heartbroken;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public int rarityLevel { get; set; } = 3;
    public string baseType { get; set; } = "King";
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wRulebreakerKing";
    public String bImage { get; set; } = "Images/Kings/bRulebreakerKing";
    public String name { get; set; } = "RulebreakerKing";
    public coords startSquare { get; set; } = new coords(-1, -1);
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Rulebreaker;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -5;
    public int rarityLevel { get; set; } = 2;
    public string baseType { get; set; } = "King";
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Kings/wDelayedKing";
    public String bImage { get; set; } = "Images/Kings/bDelayedKing";
    public String name { get; set; } = "DelayedKing";
    public coords startSquare { get; set; } = new coords(-1, -1);
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.CastleLeft | PieceAbilities.CastleRight;
    public PieceState states { get; set; } = PieceState.Delayed;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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

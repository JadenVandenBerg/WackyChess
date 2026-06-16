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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int rarityLevel { get; set; } = 0;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wQueen";
    public String bImage { get; set; } = "Images/Queens/bQueen";
    public String name { get; set; } = "Queen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9.5f;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "Moves like a normal queen but can also kill your own pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
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
    public coords[] murderousAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wMurderousQueen";
    public String bImage { get; set; } = "Images/Queens/bMurderousQueen";
    public String name { get; set; } = "Murderous Queen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10.5f;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "Moves like a normal queen but can also go through your own pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Ghost;
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = {
    };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wGhostQueen";
    public String bImage { get; set; } = "Images/Queens/bGhostQueen";
    public String name { get; set; } = "Ghost Queen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10f;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "Moves like a normal queen but your pieces can go through it.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = {
    };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wGhoulQueen";
    public String bImage { get; set; } = "Images/Queens/bGhoulQueen";
    public String name { get; set; } = "Ghoul Queen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1f;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "Moves like a normal queen but can only move once.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
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
    public coords[] oneTimeMoveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] murderousAttacks { get; set; } = {
    };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wOneTimeQueen";
    public String bImage { get; set; } = "Images/Queens/bOneTimeQueen";
    public String name { get; set; } = "One Time Queen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 12.5f;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "This piece moves like a queen. On capture, there is a 50% chance the capturing piece will die.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Electric;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wElectricQueen";
    public String bImage { get; set; } = "Images/Queens/bElectricQueen";
    public String name { get; set; } = "Electric Queen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 17;
    public int rarityLevel { get; set; } = 5;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = -1;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wInfiniteQueen";
    public String bImage { get; set; } = "Images/Queens/bInfiniteQueen";
    public String name { get; set; } = "InfiniteQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 14;
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Portal;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wPortalQueen";
    public String bImage { get; set; } = "Images/Queens/bPortalQueen";
    public String name { get; set; } = "PortalQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = 0;
    public coords[] collateral { get; set; } = { new coords(0, 0), new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wAtomicQueen";
    public String bImage { get; set; } = "Images/Queens/bAtomicQueen";
    public String name { get; set; } = "AtomicQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = 1;
    public coords[] collateral { get; set; } = { new coords(0, 0), new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wLandmineQueen";
    public String bImage { get; set; } = "Images/Queens/bLandmineQueen";
    public String name { get; set; } = "LandmineQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wLiteQueen";
    public String bImage { get; set; } = "Images/Queens/bLiteQueen";
    public String name { get; set; } = "LiteQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 11.5f;
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "Moves like a normal queen but your pieces can go through it.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Ghost | PieceState.Ghoul;
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = {
    };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wSuperGhostQueen";
    public String bImage { get; set; } = "Images/Queens/bSuperGhostQueen";
    public String name { get; set; } = "SuperGhostQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wMinister";
    public String bImage { get; set; } = "Images/Queens/bMinister";
    public String name { get; set; } = "MinisterQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Fragile;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wFragileQueen";
    public String bImage { get; set; } = "Images/Queens/bFragileQueen";
    public String name { get; set; } = "FragileQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 12;
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { new coords(1, 2), new coords(-1, 2), new coords(2, 1), new coords(-2, 1), new coords(1, -2), new coords(-1, -2), new coords(2, -1), new coords(-2, -1) };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wAmazon";
    public String bImage { get; set; } = "Images/Queens/bAmazon";
    public String name { get; set; } = "AmazonQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 0), new coords(-1, 0), new coords(0, -1), new coords(0, 1) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wReverseMinister";
    public String bImage { get; set; } = "Images/Queens/bReverseMinister";
    public String name { get; set; } = "ReverseMinisterQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 6;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { new coords(1, 2), new coords(-1, 2), new coords(2, 1), new coords(-2, 1), new coords(1, -2), new coords(-1, -2), new coords(2, -1), new coords(-2, -1) };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wSinisterMinisterQueen";
    public String bImage { get; set; } = "Images/Queens/bSinisterMinisterQueen";
    public String name { get; set; } = "SinisterMinisterQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4.5f;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 2), new coords(0, 4), new coords(0, 6), new coords(0, 8), new coords(2, 0), new coords(4, 0), new coords(6, 0), new coords(8, 0), new coords(0, -2), new coords(0, -4), new coords(0, -6), new coords(0, -8), new coords(-2, 0), new coords(-4, 0), new coords(-6, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wMonochromeQueen";
    public String bImage { get; set; } = "Images/Queens/bMonochromeQueen";
    public String name { get; set; } = "MonochromeQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 11;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Bouncing;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wBouncingQueen";
    public String bImage { get; set; } = "Images/Queens/bBouncingQueen";
    public String name { get; set; } = "BouncingQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Crowding;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wCrowdingQueen";
    public String bImage { get; set; } = "Images/Queens/bCrowdingQueen";
    public String name { get; set; } = "CrowdingQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Vomit;
    public PieceState states { get; set; } = PieceState.Hungry;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wHungryQueen";
    public String bImage { get; set; } = "Images/Queens/bHungryQueen";
    public String name { get; set; } = "HungryQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.CaptureTheFlag;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wCaptureTheFlagQueen";
    public String bImage { get; set; } = "Images/Queens/bCaptureTheFlagQueen";
    public String name { get; set; } = "CaptureTheFlagQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10.5f;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Freeze;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wFreezingQueen";
    public String bImage { get; set; } = "Images/Queens/bFreezingQueen";
    public String name { get; set; } = "FreezingQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 14;
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Spawn;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wCloningQueen";
    public String bImage { get; set; } = "Images/Queens/bCloningQueen";
    public String name { get; set; } = "CloningQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "Queen";
    public int numSpawns { get; set; } = 2;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10.5f;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Spawn;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wUndeadQueen";
    public String bImage { get; set; } = "Images/Queens/bUndeadQueen";
    public String name { get; set; } = "UndeadQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "ZombiePawn";
    public int numSpawns { get; set; } = 3;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Defuser;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wDefuserQueen";
    public String bImage { get; set; } = "Images/Queens/bDefuserQueen";
    public String name { get; set; } = "DefuserQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Spit;
    public PieceState states { get; set; } = PieceState.Spitting;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = 1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wSpittingQueen";
    public String bImage { get; set; } = "Images/Queens/bSpittingQueen";
    public String name { get; set; } = "SpittingQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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

public class PhantomQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 12;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Dematerialize;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wPhantomQueen";
    public String bImage { get; set; } = "Images/Queens/bPhantomQueen";
    public String name { get; set; } = "PhantomQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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

public class StackingQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 14.5f;
    public int rarityLevel { get; set; } = 5;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Stacking;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wStackingQueen";
    public String bImage { get; set; } = "Images/Queens/bStackingQueen";
    public String name { get; set; } = "StackingQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Jailer;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wJailQueen";
    public String bImage { get; set; } = "Images/Queens/bJailQueen";
    public String name { get; set; } = "JailQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Piggyback;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wPiggybackQueen";
    public String bImage { get; set; } = "Images/Queens/bPiggybackQueen";
    public String name { get; set; } = "PiggybackQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Jockey;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wJockeyQueen";
    public String bImage { get; set; } = "Images/Queens/bJockeyQueen";
    public String name { get; set; } = "JockeyQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public string baseType { get; set; } = "Queen";
    public coords startSquare { get; set; } = new coords(-1, -1);
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Feminist;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wFeminist";
    public String bImage { get; set; } = "Images/Queens/bFeminist";
    public String name { get; set; } = "Feminist";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 15;
    public int rarityLevel { get; set; } = 5;
    public string baseType { get; set; } = "Queen";
    public coords startSquare { get; set; } = new coords(-1, -1);
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Medusa;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wMedusa";
    public String bImage { get; set; } = "Images/Queens/bMedusa";
    public String name { get; set; } = "Medusa";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 3; //Use numspawns for shield pawn gen

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 6;
    public int rarityLevel { get; set; } = 2;
    public string baseType { get; set; } = "Queen";
    public coords startSquare { get; set; } = new coords(-1, -1);
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Delayed;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wDelayedQueen";
    public String bImage { get; set; } = "Images/Queens/bDelayedQueen";
    public String name { get; set; } = "DelayedQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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

public class FreezeBombQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 11;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = 2;
    public coords[] collateral { get; set; } = { new coords(0, 0), new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wFreezeBombQueen";
    public String bImage { get; set; } = "Images/Queens/bFreezeBombQueen";
    public String name { get; set; } = "FreezeBombQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public FreezeBombQueen(int color, bool online, bool simulated)
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

public class FusionQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 14;
    public int rarityLevel { get; set; } = 0;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Queen";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Fusion;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Queens/wFusionQueen";
    public String bImage { get; set; } = "Images/Queens/bFusionQueen";
    public String name { get; set; } = "FusionQueen";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public FusionQueen(int color, bool online, bool simulated)
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
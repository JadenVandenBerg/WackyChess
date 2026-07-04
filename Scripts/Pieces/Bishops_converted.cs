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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 0;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wBishop";
    public String bImage { get; set; } = "Images/Bishops/bBishop";
    public String name { get; set; } = "Bishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4f;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
    public String description { get; set; } = "Moves like a bishop but you can kill your own pieces.";
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
    public coords[] murderousAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
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

    public String wImage { get; set; } = "Images/Bishops/wMurderousBishop";
    public String bImage { get; set; } = "Images/Bishops/bMurderousBishop";
    public String name { get; set; } = "MurderousBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
    public String description { get; set; } = "Moves like a bishop but you can go through your own pieces.";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
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

    public String wImage { get; set; } = "Images/Bishops/wGhostBishop";
    public String bImage { get; set; } = "Images/Bishops/bGhostBishop";
    public String name { get; set; } = "Ghost Bishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4f;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
    public String description { get; set; } = "Moves like a bishop but your pieces can g=o through it.";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
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

    public String wImage { get; set; } = "Images/Bishops/wGhoulBishop";
    public String bImage { get; set; } = "Images/Bishops/bGhoulBishop";
    public String name { get; set; } = "Ghoul Bishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1f;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
    public String description { get; set; } = "Moves like a bishop but can only move once.";
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
    public coords[] oneTimeMoveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] murderousAttacks { get; set; } = { };
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

    public String wImage { get; set; } = "Images/Bishops/wOneTimeBishop";
    public String bImage { get; set; } = "Images/Bishops/bOneTimeBishop";
    public String name { get; set; } = "One Time Bishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4.5f;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
    public String description { get; set; } = "This piece moves like a bishop. On capture, the capturing piece has a 50% chance of dying.";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wElectricBishop";
    public String bImage { get; set; } = "Images/Bishops/bElectricBishop";
    public String name { get; set; } = "Electric Bishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9f;
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wInfiniteBishop";
    public String bImage { get; set; } = "Images/Bishops/bInfiniteBishop";
    public String name { get; set; } = "InfiniteBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 6;
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wPortalBishop";
    public String bImage { get; set; } = "Images/Bishops/bPortalBishop";
    public String name { get; set; } = "PortalBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 7;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wAtomicBishop";
    public String bImage { get; set; } = "Images/Bishops/bAtomicBishop";
    public String name { get; set; } = "AtomicBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 6;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wLandmineBishop";
    public String bImage { get; set; } = "Images/Bishops/bLandmineBishop";
    public String name { get; set; } = "LandmineBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moves { get; set; } = { new coords(1, 0), new coords(-1, 0) };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wColorChangingBishop";
    public String bImage { get; set; } = "Images/Bishops/bColorChangingBishop";
    public String name { get; set; } = "ColorChangingBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wLiteBishop";
    public String bImage { get; set; } = "Images/Bishops/bLiteBishop";
    public String name { get; set; } = "LiteBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5f;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
    public String description { get; set; } = "Moves like a bishop but your pieces can go through it.";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
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

    public String wImage { get; set; } = "Images/Bishops/wSuperGhostBishop";
    public String bImage { get; set; } = "Images/Bishops/bSuperGhostBishop";
    public String name { get; set; } = "SuperGhostBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { new coords(1, 2), new coords(-1, 2), new coords(2, 1), new coords(-2, 1), new coords(1, -2), new coords(-1, -2), new coords(2, -1), new coords(-2, -1) };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wPrincess";
    public String bImage { get; set; } = "Images/Bishops/bPrincess";
    public String name { get; set; } = "BishopPrincess";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wFragileBishop";
    public String bImage { get; set; } = "Images/Bishops/bFragileBishop";
    public String name { get; set; } = "FragileBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4.5f;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wRoyalBishop";
    public String bImage { get; set; } = "Images/Bishops/bRoyalBishop";
    public String name { get; set; } = "RoyalBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wBouncingBishop";
    public String bImage { get; set; } = "Images/Bishops/bBouncingBishop";
    public String name { get; set; } = "BouncingBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wCrowdingBishop";
    public String bImage { get; set; } = "Images/Bishops/bCrowdingBishop";
    public String name { get; set; } = "CrowdingBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wHungryBishop";
    public String bImage { get; set; } = "Images/Bishops/bHungryBishop";
    public String name { get; set; } = "HungryBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wCaptureTheFlagBishop";
    public String bImage { get; set; } = "Images/Bishops/bCaptureTheFlagBishop";
    public String name { get; set; } = "CaptureTheFlagBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wFreezingBishop";
    public String bImage { get; set; } = "Images/Bishops/bFreezingBishop";
    public String name { get; set; } = "FreezingBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 6;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wCloningBishop";
    public String bImage { get; set; } = "Images/Bishops/bCloningBishop";
    public String name { get; set; } = "CloningBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "Bishop";
    public int numSpawns { get; set; } = 2;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wUndeadBishop";
    public String bImage { get; set; } = "Images/Bishops/bUndeadBishop";
    public String name { get; set; } = "UndeadBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "ZombiePawn";
    public int numSpawns { get; set; } = 3;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "Rook";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wPromotionBishop";
    public String bImage { get; set; } = "Images/Bishops/bPromotionBishop";
    public String name { get; set; } = "PromotionBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wDefuserBishop";
    public String bImage { get; set; } = "Images/Bishops/bDefuserBishop";
    public String name { get; set; } = "DefuserBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wSpittingBishop";
    public String bImage { get; set; } = "Images/Bishops/bSpittingBishop";
    public String name { get; set; } = "SpittingBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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

public class PhantomBishop : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wPhantomBishop";
    public String bImage { get; set; } = "Images/Bishops/bPhantomBishop";
    public String name { get; set; } = "PhantomBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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

public class StackingBishop : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 7;
    public int rarityLevel { get; set; } = 4;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wStackingBishop";
    public String bImage { get; set; } = "Images/Bishops/bStackingBishop";
    public String name { get; set; } = "StackingBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wJailBishop";
    public String bImage { get; set; } = "Images/Bishops/bJailBishop";
    public String name { get; set; } = "JailBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wPiggybackBishop";
    public String bImage { get; set; } = "Images/Bishops/bPiggybackBishop";
    public String name { get; set; } = "PiggybackBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 2;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wJockeyBishop";
    public String bImage { get; set; } = "Images/Bishops/bJockeyBishop";
    public String name { get; set; } = "JockeyBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public int rarityLevel { get; set; } = 2;
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wDelayedBishop";
    public String bImage { get; set; } = "Images/Bishops/bDelayedBishop";
    public String name { get; set; } = "DelayedBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

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

public class FreezeBombBishop : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
    public int rarityLevel { get; set; } = 3;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wFreezeBombBishop";
    public String bImage { get; set; } = "Images/Bishops/bFreezeBombBishop";
    public String name { get; set; } = "FreezeBombBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public FreezeBombBishop(int color, bool online, bool simulated)
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

public class FusionBishop : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 6;
    public int rarityLevel { get; set; } = 0;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Bishop";
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
    public coords[] moveAndAttacks { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Bishops/wFusionBishop";
    public String bImage { get; set; } = "Images/Bishops/bFusionBishop";
    public String name { get; set; } = "FusionBishop";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public FusionBishop(int color, bool online, bool simulated)
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
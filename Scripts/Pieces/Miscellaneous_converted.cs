using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;

/*
public class Template : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int rarityLevel { get; set; } = 0;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Misc";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] moves { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
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

    public String wImage { get; set; } = "Images/";
    public String bImage { get; set; } = "Images/";
    public String name { get; set; } = "";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public Template(int color, bool online, bool simulated)
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

public class SuperPawn : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 12;
    public int rarityLevel { get; set; } = 0;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Misc";
    public String description { get; set; } = "This piece can move like a queen and a knight";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
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
    public coords[] jumpAttacks { get; set; } = { new coords(1, 2), new coords(-1, 2), new coords(2, 1), new coords(-2, 1), new coords(1, -2), new coords(-1, -2), new coords(2, -1), new coords(-2, -1) };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Extras/wSuperPawn";
    public String bImage { get; set; } = "Images/Extras/bSuperPawn";
    public String name { get; set; } = "Super Pawn";

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public SuperPawn(int color, bool online, bool simulated)
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
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
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0;
    public static readonly int rarityLevel = 0;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Misc";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
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

    public static readonly string wImage = "Images/";
    public static readonly string bImage = "Images/";
    public string name = "";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 12;
    public static readonly int rarityLevel = 0;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Misc";
    public static readonly string description = "This piece can move like a queen and a knight";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
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
    public static readonly coords[] jumpAttacks = moveDefs.knightMoves;
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Extras/wSuperPawn";
    public static readonly string bImage = "Images/Extras/bSuperPawn";
    public string name = "Super Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
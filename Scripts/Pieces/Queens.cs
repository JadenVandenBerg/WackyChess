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
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bQueen.png";
    public String name { get; set; } = "Queen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public Queen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class MurderousQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Moves like a normal queen but can also kill your own pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wMurderousQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bMurderousQueen.png";
    public String name { get; set; } = "Murderous Queen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { }; ;
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { }; ;
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public MurderousQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }
        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class GhostQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 11;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Moves like a normal queen but can also go through your own pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Ghost";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = {
    };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wGhostQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bGhostQueen.png";
    public String name { get; set; } = "Ghost Queen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public GhostQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }
        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class GhoulQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9.5f;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Moves like a normal queen but your pieces can go through it.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Ghoul";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = {
    };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wGhoulQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bGhoulQueen.png";
    public String name { get; set; } = "Ghoul Queen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public GhoulQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }
        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class OneTimeQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Moves like a normal queen but can only move once.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] oneTimeMoveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] murderousAttacks { get; set; } = {
    };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wOneTimeQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bOneTimeQueen.png";
    public String name { get; set; } = "One Time Queen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public OneTimeQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }
        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class ElectricQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 10.5f;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "This piece moves like a queen. On capture, there is a 50% chance the capturing piece will die.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Electric";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wElectricQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bElectricQueen.png";
    public String name { get; set; } = "Electric Queen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public ElectricQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = "Queen";

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class InfiniteQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 18;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = -1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wInfiniteQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bInfiniteQueen.png";
    public String name { get; set; } = "InfiniteQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public InfiniteQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = "InfiniteQueen";

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class PortalQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 15;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Portal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wPortalQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bPortalQueen.png";
    public String name { get; set; } = "PortalQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public PortalQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = "PortalQueen";

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class AtomicQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = { { 0, 0 }, { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wAtomicQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bAtomicQueen.png";
    public String name { get; set; } = "AtomicQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public AtomicQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = "AtomicQueen";

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class LandmineQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = 1;
    public int[,] collateral { get; set; } = { { 0, 0 }, { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wLandmineQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bLandmineQueen.png";
    public String name { get; set; } = "LandmineQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public LandmineQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = "LandmineQueen";

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class LiteQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wLiteQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bLiteQueen.png";
    public String name { get; set; } = "LiteQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public LiteQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class SuperGhostQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9.5f;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Moves like a normal queen but your pieces can go through it.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Ghoul";
    public String secondaryState { get; set; } = "Ghost";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = {
    };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wSuperGhostQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bSuperGhostQueen.png";
    public String name { get; set; } = "SuperGhostQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public SuperGhostQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }
        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class Minister : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wMinister.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bMinister.png";
    public String name { get; set; } = "MinisterQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public Minister(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class FragileQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Fragile";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wFragileQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bFragileQueen.png";
    public String name { get; set; } = "FragileQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public FragileQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class Amazon : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wAmazon.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bAmazon.png";
    public String name { get; set; } = "AmazonQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public Amazon(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class ReverseMinister : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 0 }, { -1, 0 }, { 0, -1 }, { 0, 1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wReverseMinister.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bReverseMinister.png";
    public String name { get; set; } = "ReverseMinisterQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public ReverseMinister(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class SinisterMinisterQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wSinisterMinisterQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bSinisterMinisterQueen.png";
    public String name { get; set; } = "SinisterMinisterQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public SinisterMinisterQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class MonochromeQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 2 }, { 0, 4 }, { 0, 6 }, { 0, 8 },
        { 2, 0 }, { 4, 0 }, { 6, 0 }, { 8, 0 },
        { 0, -2 }, { 0, -4 }, { 0, -6 }, { 0, -8 },
        { -2, 0 }, { -4, 0 }, { -6, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wMonochromeQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bMonochromeQueen.png";
    public String name { get; set; } = "MonochromeQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public MonochromeQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class BouncingQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Bouncing";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wBouncingQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bBouncingQueen.png";
    public String name { get; set; } = "BouncingQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public BouncingQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class CrowdingQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Crowding";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wCrowdingQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bCrowdingQueen.png";
    public String name { get; set; } = "CrowdingQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public CrowdingQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class HungryQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Vomit";
    public String state { get; set; } = "Hungry";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wHungryQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bHungryQueen.png";
    public String name { get; set; } = "HungryQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public HungryQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class CaptureTheFlagQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "CaptureTheFlag";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wCaptureTheFlagQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bCaptureTheFlagQueen.png";
    public String name { get; set; } = "CaptureTheFlagQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public CaptureTheFlagQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class FreezingQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Freeze";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wFreezingQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bFreezingQueen.png";
    public String name { get; set; } = "FreezingQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public FreezingQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class CloningQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Spawn";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wCloningQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bCloningQueen.png";
    public String name { get; set; } = "CloningQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "Queen";
    public int numSpawns { get; set; } = 2;

    public CloningQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class UndeadQueen : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Spawn";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public GameObject go { get; set; } = null;
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wUndeadQueen.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bUndeadQueen.png";
    public String name { get; set; } = "UndeadQueen";
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
    {
        return new int[,] { };
    }

    public bool stayTurn()
    {
        canMoveTwice = 0;
        return false;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "ZombiePawn";
    public int numSpawns { get; set; } = 3;

    public UndeadQueen(int color, bool online)
    {
        if (online)
        {
            if (go == null) go = PhotonNetwork.Instantiate("Empty", new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            if (go == null) go = new GameObject();
        }

        this.color = color;

        go.name = name;

        HelperFunctions.UpdateMovesForColor(this);
        Image s = go.AddComponent<Image>();
        byte[] fileData;

        if (color == 1)
        {
            fileData = File.ReadAllBytes(wImage);
        }
        else
        {
            fileData = File.ReadAllBytes(bImage);
        }

        Texture2D texture2D = new Texture2D(2, 2);
        texture2D.LoadImage(fileData);

        Sprite sp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        s.sprite = sp;
    }
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}
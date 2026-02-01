using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;
public class Knight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 0;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bKnight.png";
    public String name { get; set; } = "Knight";

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

    public Knight(int color, bool online)
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
}

public class MurderousKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "Similar to a regular knight, but this piece can capture your own pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { { 2, 1 }, { 2, -1 }, { 1, 2 }, { 1, -2 }, { -2, 1 }, { -2, -1 }, { -1, 2 }, { -1, -2 } };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wMurderousKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bMurderousKnight.png";
    public String name { get; set; } = "Murderous Knight";
    public int[,] dependentMovesSet()
    {
        dependentAttacks = new int[,] { };
        int[,] moveOptions = new int[,] { { 2, 1 }, { 2, -1 }, { 1, 2 }, { 1, -2 }, { -2, 1 }, { -2, -1 }, { -1, 2 }, { -1, -2 } };

        for (int i = 0; i < moveOptions.GetLength(0); i++)
        {
            GameObject newSquare = HelperFunctions.findSquare(this.position[0] + moveOptions[i,0], this.position[1] + moveOptions[i,1]);
            if (newSquare != null)
            {
                HelperFunctions.addTo2DArray(dependentAttacks, new int[] { moveOptions[i, 0], moveOptions[i, 1] });
            }
        }
        return dependentAttacks;
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

    public MurderousKnight(int color, bool online)
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
}

public class GhoulKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3.5f;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "Similar to a regular knight, but your pieces can move through this piece.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Ghoul";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { { 2, 1 }, { 2, -1 }, { 1, 2 }, { 1, -2 }, { -2, 1 }, { -2, -1 }, { -1, 2 }, { -1, -2 } };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wGhoulKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bGhoulKnight.png";
    public String name { get; set; } = "Ghoul Knight";
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

    public GhoulKnight(int color, bool online)
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
}

public class OneTimeKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "Similar to a regular knight, but can only move once.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { { 2, 1 }, { 2, -1 }, { 1, 2 }, { 1, -2 }, { -2, 1 }, { -2, -1 }, { -1, 2 }, { -1, -2 } };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wOneTimeKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bOneTimeKnight.png";
    public String name { get; set; } = "One Time Knight";
    public int[,] dependentMovesSet()
    {
        dependentAttacks = new int[,] { };
        int[,] moveOptions = new int[,] { { 2, 1 }, { 2, -1 }, { 1, 2 }, { 1, -2 }, { -2, 1 }, { -2, -1 }, { -1, 2 }, { -1, -2 } };

        for (int i = 0; i < moveOptions.GetLength(0); i++)
        {
            GameObject newSquare = HelperFunctions.findSquare(this.position[0] + moveOptions[i, 0], this.position[1] + moveOptions[i, 1]);
            if (newSquare != null && !this.hasMoved)
            {
                if (HelperFunctions.isPieceOnSquare(newSquare) || HelperFunctions.isColorNotOnSquare(newSquare, this.color))
                {
                    HelperFunctions.addTo2DArray(dependentAttacks, new int[] { moveOptions[i,0], moveOptions[i,1] });
                }
            }
        }
        return dependentAttacks;
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

    public OneTimeKnight(int color, bool online)
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
}

public class ElectricKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int rarityLevel { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "This piece moves like a knight. On capture, there is a 50% chance the capturing piece will die.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Electric";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wElectricKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bElectricKnight.png";
    public String name { get; set; } = "Electric Knight";

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

    public ElectricKnight(int color, bool online)
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

        go.name = "Electric Knight";

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
}

public class InfiniteKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 12;
    public int rarityLevel { get; set; } = 4;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = -1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wInfiniteKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bInfiniteKnight.png";
    public String name { get; set; } = "InfiniteKnight";

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

    public InfiniteKnight(int color, bool online)
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

        go.name = "InfiniteKnight";

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
}

public class PortalKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
    public int rarityLevel { get; set; } = 4;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Portal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wPortalKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bPortalKnight.png";
    public String name { get; set; } = "PortalKnight";

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

    public PortalKnight(int color, bool online)
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

        go.name = "PortalKnight";

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
}

public class AtomicKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 5;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wAtomicKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bAtomicKnight.png";
    public String name { get; set; } = "AtomicKnight";

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

    public AtomicKnight(int color, bool online)
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

        go.name = "AtomicKnight";

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
}

public class LandmineKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 4;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wLandmineKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bLandmineKnight.png";
    public String name { get; set; } = "LandmineKnight";

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

    public LandmineKnight(int color, bool online)
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

        go.name = "LandmineKnight";

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
}

public class LiteKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wLiteKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bLiteKnight.png";
    public String name { get; set; } = "LiteKnight";

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

    public LiteKnight(int color, bool online)
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
}

public class SpiderKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 },
        { 2, 0 }, { -2, 0 }, { 0, 2 }, { 0, -2 },
        { 2, 2 }, { -2, 2 }, { -2, -2 }, { 2, -2 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wSpiderKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bSpiderKnight.png";
    public String name { get; set; } = "SpiderKnight";

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

    public SpiderKnight(int color, bool online)
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
}

public class DisabledKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 0, 2 }, { 0, -2 }, { 2, 0 },{ -2, 0 },
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wDisabledKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bDisabledKnight.png";
    public String name { get; set; } = "DisabledKnight";

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

    public DisabledKnight(int color, bool online)
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
}

public class Elephant : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 2, -2 }, { -2, -2 }, { 2, 2 },{ -2, 2 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wElephant.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bElephant.png";
    public String name { get; set; } = "KnightElephant";

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

    public Elephant(int color, bool online)
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
}

public class SniperElephant : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 3, -3 }, { -3, -3 }, { 3, 3 },{ -3, 3 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wSniperElephant.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bSniperElephant.png";
    public String name { get; set; } = "KnightSniperElephant";

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

    public SniperElephant(int color, bool online)
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
}

public class Camel : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 3 }, { -1, 3 }, { 3, 1 },{ -3, 1 },
        { 1, -3 }, { -1, -3 }, { 3, -1 },{ -3, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wCamel.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bCamel.png";
    public String name { get; set; } = "KnightCamel";

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

    public Camel(int color, bool online)
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
}

public class FragileKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Fragile";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wFragileKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bFragileKnight.png";
    public String name { get; set; } = "FragileKnight";

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

    public FragileKnight(int color, bool online)
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
}

public class SinisterMinisterKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 9;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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

    public String wImage { get; set; } = "Assets/Images/Knights/wSinisterMinisterKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bSinisterMinisterKnight.png";
    public String name { get; set; } = "SinisterMinisterKnight";
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

    public SinisterMinisterKnight(int color, bool online)
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
}

public class RoyalKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wRoyalKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bRoyalKnight.png";
    public String name { get; set; } = "RoyalKnight";

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

    public RoyalKnight(int color, bool online)
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
}

public class CrowdingKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Crowding";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wCrowdingKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bCrowdingKnight.png";
    public String name { get; set; } = "CrowdingKnight";

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

    public CrowdingKnight(int color, bool online)
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
}

public class HungryKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Vomit";
    public String state { get; set; } = "Hungry";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wHungryKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bHungryKnight.png";
    public String name { get; set; } = "HungryKnight";

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

    public HungryKnight(int color, bool online)
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
}

public class CaptureTheFlagKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "CaptureTheFlag";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wCaptureTheFlagKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bCaptureTheFlagKnight.png";
    public String name { get; set; } = "CaptureTheFlagKnight";

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

    public CaptureTheFlagKnight(int color, bool online)
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
}

public class FreezingKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Freeze";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wFreezingKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bFreezingKnight.png";
    public String name { get; set; } = "FreezingKnight";

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

    public FreezingKnight(int color, bool online)
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
}

public class CloningKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Spawn";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wCloningKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bCloningKnight.png";
    public String name { get; set; } = "CloningKnight";

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
    public string spawnable { get; set; } = "Knight";
    public int numSpawns { get; set; } = 2;

    public CloningKnight(int color, bool online)
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
}

public class UndeadKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Spawn";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wUndeadKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bUndeadKnight.png";
    public String name { get; set; } = "UndeadKnight";

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

    public UndeadKnight(int color, bool online)
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
}

public class PromotionKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "Rook";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wPromotionKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bPromotionKnight.png";
    public String name { get; set; } = "PromotionKnight";

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

    public PromotionKnight(int color, bool online)
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
}

public class LongKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 7 }, { -1, 7 }, { 7, 1 },{ -7, 1 },
        { 1, -7 }, { -1, -7 }, { 7, -1 },{ -7, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wLongKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bLongKnight.png";
    public String name { get; set; } = "LongKnight";

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

    public LongKnight(int color, bool online)
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
}

public class DefuserKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Defuser";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wDefuserKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bDefuserKnight.png";
    public String name { get; set; } = "DefuserKnight";

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

    public DefuserKnight(int color, bool online)
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
}

public class Hippocamelus : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 4;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 },
        { 1, 3 }, { -1, 3 }, { 3, 1 },{ -3, 1 },
        { 1, -3 }, { -1, -3 }, { 3, -1 },{ -3, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wHippocamelus.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bHippocamelus.png";
    public String name { get; set; } = "Hippocamelus";

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

    public Hippocamelus(int color, bool online)
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
}

public class SpittingKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Spit";
    public String state { get; set; } = "Spitting";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = 1;
    public List<Piece> storage { get; set; } = null;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wSpittingKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bSpittingKnight.png";
    public String name { get; set; } = "SpittingKnight";

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

    public SpittingKnight(int color, bool online)
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
}

public class PhantomKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 4;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Dematerialize";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wPhantomKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bPhantomKnight.png";
    public String name { get; set; } = "PhantomKnight";

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

    public PhantomKnight(int color, bool online)
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
}

public class StackingKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 5;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Stacking";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wStackingKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bStackingKnight.png";
    public String name { get; set; } = "StackingKnight";

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

    public StackingKnight(int color, bool online)
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
}

public class JailKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Jailer";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wJailKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bJailKnight.png";
    public String name { get; set; } = "JailKnight";

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

    public JailKnight(int color, bool online)
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
}

public class PiggybackKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Piggyback";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wPiggybackKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bPiggybackKnight.png";
    public String name { get; set; } = "PiggybackKnight";

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

    public PiggybackKnight(int color, bool online)
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

public class JockeyKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Knight";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Jockey";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wJockeyKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bJockeyKnight.png";
    public String name { get; set; } = "JockeyKnight";

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

    public JockeyKnight(int color, bool online)
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

public class DelayedKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int rarityLevel { get; set; } = 2;
    public string baseType { get; set; } = "Knight";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Delayed";
    public String secondaryState { get; set; } = "Normal";
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
    public int[,] jumpAttacks { get; set; } = {
        { 1, 2 }, { -1, 2 }, { 2, 1 },{ -2, 1 },
        { 1, -2 }, { -1, -2 }, { 2, -1 },{ -2, -1 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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

    public String wImage { get; set; } = "Assets/Images/Knights/wDelayedKnight.png";
    public String bImage { get; set; } = "Assets/Images/Knights/bDelayedKnight.png";
    public String name { get; set; } = "DelayedKnight";

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

    public DelayedKnight(int color, bool online)
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
}


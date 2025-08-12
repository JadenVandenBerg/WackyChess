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
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bKing.png";
    public String name { get; set; } = "King";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public King(int color, bool online)
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

public class MurderousKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Like a normal king, but you can kill your own pieces to get out of a pinch!";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Kings/wMurderousKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bMurderousKing.png";
    public String name { get; set; } = "Murderous King";
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

    public MurderousKing(int color, bool online)
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

public class GhoulKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Like a normal king, but other pieces can go through it.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Kings/wGhoulKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bGhoulKing.png";
    public String name { get; set; } = "Ghoul King";
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

    public GhoulKing(int color, bool online)
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

public class OneTimeKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Like a normal king, but can only move once.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Kings/wOneTimeKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bOneTimeKing.png";
    public String name { get; set; } = "One Time King";
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

    public OneTimeKing(int color, bool online)
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

public class ElectricKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wElectricKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bElectricKing.png";
    public String name { get; set; } = "Electric King";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Protect this piece! It cannot move very fast, but if this piece dies or gets checkmated you lose the game.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public ElectricKing(int color, bool online)
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

        go.name = "ElectricKing";

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

public class PortalKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wPortalKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bPortalKing.png";
    public String name { get; set; } = "PortalKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public PortalKing(int color, bool online)
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

        go.name = "PortalKing";

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

public class AtomicKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wAtomicKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bAtomicKing.png";
    public String name { get; set; } = "AtomicKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public AtomicKing(int color, bool online)
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

        go.name = "AtomicKing";

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

public class LandmineKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wLandmineKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bLandmineKing.png";
    public String name { get; set; } = "LandmineKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public LandmineKing(int color, bool online)
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

        go.name = "LandmineKing";

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

public class LiteKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false; 
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wLiteKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bLiteKing.png";
    public String name { get; set; } = "LiteKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public LiteKing(int color, bool online)
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

public class HyperFastKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 3, 3 }, { -3, 3 }, { 3, -3 }, { -3, -3 }, { 0, 3 }, { 3, 0 }, { 0, -3 }, { -3, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false; 
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wHyperFastKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bHyperFastKing.png";
    public String name { get; set; } = "HyperFastKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public HyperFastKing(int color, bool online)
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

public class FastKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 2, 2 }, { -2, 2 }, { 2, -2 }, { -2, -2 }, { 0, 2 }, { 2, 0 }, { 0, -2 }, { -2, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wFastKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bFastKing.png";
    public String name { get; set; } = "FastKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public FastKing(int color, bool online)
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

public class FragileKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wFragileKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bFragileKing.png";
    public String name { get; set; } = "FragileKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public FragileKing(int color, bool online)
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

public class SlidingKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 0 }, { -1, 0 }, { 2, 0 }, { -2, 0 }, { 3, 0 }, { -3, 0 }, { 4, 0 }, { -4, 0 },
        { 5, 0 }, { -5, 0 }, { 6, 0 }, { -6, 0 }, { 7, 0 }, { -7, 0 }, { 8, 0 }, { -8, 0 },
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wSlidingKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bSlidingKing.png";
    public String name { get; set; } = "SlidingKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public SlidingKing(int color, bool online)
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

public class CrowdingKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wCrowdingKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bCrowdingKing.png";
    public String name { get; set; } = "CrowdingKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public CrowdingKing(int color, bool online)
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

public class HungryKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wHungryKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bHungryKing.png";
    public String name { get; set; } = "HungryKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Vomit-CastleLeft-CastleRight";
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

    public HungryKing(int color, bool online)
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

public class CaptureTheFlagKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;  
    public int[,] conditionalAttacks { get; set; } = {  };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wCaptureTheFlagKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bCaptureTheFlagKing.png";
    public String name { get; set; } = "CaptureTheFlagKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "CastleLeft-CastleRight";
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

    public CaptureTheFlagKing(int color, bool online)
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

public class FreezingKing : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = {
        { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 }
    };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Assets/Images/Kings/wFreezingKing.png";
    public String bImage { get; set; } = "Assets/Images/Kings/bFreezingKing.png";
    public String name { get; set; } = "FreezingKing";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Freeze-CastleLeft-CastleRight";
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

    public FreezingKing(int color, bool online)
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


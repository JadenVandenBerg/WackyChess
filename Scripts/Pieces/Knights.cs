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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class MurderousKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Similar to a regular knight, but this piece can capture your own pieces.";
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class GhoulKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3.5f;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Similar to a regular knight, but your pieces can move through this piece.";
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class OneTimeKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "Similar to a regular knight, but can only move once.";
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
    public GameObject go { get; set; } = new GameObject();
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
                Piece piece = HelperFunctions.getPieceOnSquare(newSquare);
                if (piece == null || piece.color != this.color)
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class ElectricKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "This piece moves like a knight. On capture, there is a 50% chance the capturing piece will die.";
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class InfiniteKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 12;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class PortalKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 5;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class AtomicKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class LandmineKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class LiteKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class SpiderKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class DisabledKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class Elephant : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class SniperElephant : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Knights/wSniperElephant.png";
    public String bImage { get; set; } = "Assets/Images/Knights/vSniperElephant.png";
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class Camel : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class FragileKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class SinisterMinisterKnight : Piece
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
    public GameObject go { get; set; } = new GameObject();
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Assets/Images/Queens/wSinisterMinisterKnight.png";
    public String bImage { get; set; } = "Assets/Images/Queens/bSinisterMinisterKnight.png";
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class RoyalKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

public class CrowdingKnight : Piece
{
    public bool disabled { get; set; } = false;
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3;
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
    public GameObject go { get; set; } = new GameObject();
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
    public Piece spawnable { get; set; } = null;

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
    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}
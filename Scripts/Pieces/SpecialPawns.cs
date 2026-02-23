using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;

public class MurderousPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "MurderousPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wMurderousPawn";
    public String bImage { get; set; } = "Images/Pawns/bMurderousPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. Or Killing them, since you can do that too.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Murderous";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        //Simulate Murderous
        dependentAttacks = new int[,] { };

        GameObject newSquare = HelperFunctions.findSquare(this.position[0], this.position[1] + 1);
        if (newSquare != null)
        {
            if (HelperFunctions.isPieceOnSquare(newSquare) && HelperFunctions.getColorsOnSquare(newSquare, true).Contains(this.color))
            {
                newSquare = HelperFunctions.findSquare(this.position[0], this.position[1] + 2);
                if (HelperFunctions.isPieceOnSquare(newSquare) && this.hasMoved == false)
                {
                    HelperFunctions.addTo2DArray(dependentAttacks, new int[] { 0, 2 });
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

    public MurderousPawn(int color, bool online)
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

        go.name = "MurderousPawn";

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class GhostPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
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
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Ghost Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wGhostPawn";
    public String bImage { get; set; } = "Images/Pawns/bGhostPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. This piece can move through your other pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Ghost";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        dependentAttacks = new int[,] { };

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

    public GhostPawn(int color, bool online)
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

        go.name = this.name;

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class GhoulPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] attacks { get; set; } = { { 1, 1 }, { -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { { 0, 2 } };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Ghoul Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wGhoulPawn";
    public String bImage { get; set; } = "Images/Pawns/bGhoulPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "Normal pawn, but your pieces can move through this piece.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Ghoul";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
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

    public GhoulPawn(int color, bool online)
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

        go.name = this.name;

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class OneTimePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -0.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { };
    public int[,] attacks { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 }, { 0, 1 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] dependentAttacks { get; set; } = { { 1, 1 }, { -1, 1 } };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "One Time Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wOneTimePawn";
    public String bImage { get; set; } = "Images/Pawns/bOneTimePawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "Normal pawn, but can only move once.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        dependentAttacks = new int[,] { };

        GameObject newSquare = HelperFunctions.findSquare(this.position[0] + 1, this.position[1] + 1);
        if (newSquare != null && !HelperFunctions.isJump(this, this.position, HelperFunctions.findCoords(newSquare)))
        {
            if (HelperFunctions.isPieceOnSquare(newSquare) || HelperFunctions.isColorNotOnSquare(newSquare, this.color))
            {
                HelperFunctions.addTo2DArray(dependentAttacks, new int[] { 1, 1 });
            }
        }

        newSquare = HelperFunctions.findSquare(this.position[0] + -1, this.position[1] + 1);
        if (newSquare != null && !HelperFunctions.isJump(this, this.position, HelperFunctions.findCoords(newSquare)))
        {
            if (HelperFunctions.isPieceOnSquare(newSquare) || HelperFunctions.isColorNotOnSquare(newSquare, this.color))
            {
                HelperFunctions.addTo2DArray(dependentAttacks, new int[] { -1, 1 });
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

    public OneTimePawn(int color, bool online)
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

        go.name = this.name;

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class ElectricPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Electric Pawn";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wElectricPawn";
    public String bImage { get; set; } = "Images/Pawns/bElectricPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "This piece moves like a pawn. On its capture, there is a 50% chance the capturing piece will die.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Electric";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
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

    public ElectricPawn(int color, bool online)
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

        go.name = "Electric Pawn";

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class ShieldPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
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
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Shield Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wShieldPawn";
    public String bImage { get; set; } = "Images/Pawns/bShieldPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Shield";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
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

    public ShieldPawn(int color, bool online)
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

        go.name = "Shield Pawn";

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class InfinitePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Infinite Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wInfinitePawn";
    public String bImage { get; set; } = "Images/Pawns/bInfinitePawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = -1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "None";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
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

    public InfinitePawn(int color, bool online)
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

        go.name = "InfinitePawn";

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class PortalPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PortalPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPortalPawn";
    public String bImage { get; set; } = "Images/Pawns/bPortalPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        /*int[,] portalMoves = new int[,] { };
        if (position[0] == 1)
        {
            int posY_ = position[1] + 1 * color;
            GameObject square = HelperFunctions.findSquare(8, posY_);
            Piece piece = HelperFunctions.getPieceOnSquareDebug(square);
            Debug.Log(HelperFunctions.findCoords(square)[0] + " " + HelperFunctions.findCoords(square)[1]);
            if (piece != null && piece.color != color)
            {
                Debug.Log("HERE");
                portalMoves = HelperFunctions.addTo2DArray(portalMoves, new int[] { 7, 1 });
            }
        }
        else if (position[0] == 8)
        {
            int posY_ = position[1] + 1;
            GameObject square = HelperFunctions.findSquare(1, posY_);
            Piece piece = HelperFunctions.getPieceOnSquareDebug(square);
            if (piece != null && piece.color != color)
            {
                portalMoves = HelperFunctions.addTo2DArray(portalMoves, new int[] { -7, 1 });
            }
        }

        return portalMoves;*/
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

    public PortalPawn(int color, bool online)
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

        go.name = "PortalPawn";

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class AtomicPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "AtomicPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wAtomicPawn";
    public String bImage { get; set; } = "Images/Pawns/bAtomicPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public AtomicPawn(int color, bool online)
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

        go.name = "AtomicPawn";

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class LandminePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LandminePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wLandminePawn";
    public String bImage { get; set; } = "Images/Pawns/bLandminePawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public LandminePawn(int color, bool online)
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

        go.name = "LandminePawn";

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class SpontaneouslyCombustingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -2f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SpontaneouslyCombustingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wSpontaneouslyCombustingPawn";
    public String bImage { get; set; } = "Images/Pawns/bSpontaneouslyCombustingPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Combustable";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
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

    public SpontaneouslyCombustingPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class SuperGhostPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 }, { -1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SuperGhostPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wSuperGhostPawn";
    public String bImage { get; set; } = "Images/Pawns/bSuperGhostPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. This piece can move through your other pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Ghost";
    public String secondaryState { get; set; } = "Ghoul";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
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

    public SuperGhostPawn(int color, bool online)
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

        go.name = this.name;

        HelperFunctions.UpdateMovesForColor(this);

        Image s = go.AddComponent<Image>();
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class FragilePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FragilePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wFragilePawn";
    public String bImage { get; set; } = "Images/Pawns/bFragilePawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public FragilePawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class CrowdingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "CrowdingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wCrowdingPawn";
    public String bImage { get; set; } = "Images/Pawns/bCrowdingPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public CrowdingPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class HungryPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "HungryPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wHungryPawn";
    public String bImage { get; set; } = "Images/Pawns/bHungryPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public HungryPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class CaptureTheFlagPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "CaptureTheFlagPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wCaptureTheFlagPawn";
    public String bImage { get; set; } = "Images/Pawns/bCaptureTheFlagPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public CaptureTheFlagPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class FreezingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FreezingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wFreezingPawn";
    public String bImage { get; set; } = "Images/Pawns/bFreezingPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public FreezingPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class CloningPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "CloningPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wCloningPawn";
    public String bImage { get; set; } = "Images/Pawns/bCloningPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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
    public string spawnable { get; set; } = "Pawn";
    public int numSpawns { get; set; } = 2;

    public CloningPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class ZombiePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { };
    public int[,] oneTimeMoves { get; set; } = { };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
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
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { { 0, 1 } };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ZombiePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wZombiePawn";
    public String bImage { get; set; } = "Images/Pawns/bZombiePawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public ZombiePawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class UndeadPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "UndeadPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wUndeadPawn";
    public String bImage { get; set; } = "Images/Pawns/bUndeadPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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
    public string spawnable { get; set; } = "ZombiePawn";
    public int numSpawns { get; set; } = 3;

    public UndeadPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class PromotionPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PromotionPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPromotionPawn";
    public String bImage { get; set; } = "Images/Pawns/bPromotionPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "Knight";
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

    public PromotionPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class DefuserPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DefuserPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wDefuserPawn";
    public String bImage { get; set; } = "Images/Pawns/bDefuserPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public DefuserPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class SpittingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SpittingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wSpittingPawn";
    public String bImage { get; set; } = "Images/Pawns/bSpittingPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = 1;
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

    public SpittingPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class PhantomPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PhantomPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPhantomPawn";
    public String bImage { get; set; } = "Images/Pawns/bPhantomPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public PhantomPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class SplittingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SplittingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wSplittingPawn";
    public String bImage { get; set; } = "Images/Pawns/bSplittingPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "Split";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
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

    public SplittingPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class PromotingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PromotingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPromotingPawn";
    public String bImage { get; set; } = "Images/Pawns/bPromotingPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 7;
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

    public PromotingPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class StackingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "StackingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wStackingPawn";
    public String bImage { get; set; } = "Images/Pawns/bStackingPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public StackingPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class JailPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "JailPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wJailPawn";
    public String bImage { get; set; } = "Images/Pawns/bJailPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public JailPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class PiggybackPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PiggybackPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPiggybackPawn";
    public String bImage { get; set; } = "Images/Pawns/bPiggybackPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public PiggybackPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
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

public class JockeyPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "JockeyPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wJockeyPawn";
    public String bImage { get; set; } = "Images/Pawns/bJockeyPawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public JockeyPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
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

public class ProtectivePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { }; //Flag: Not in check
    public int[,] flagMove2 { get; set; } = {
        { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 7 }, { 8, 8 },
        { -1, 1 }, { -2, 2 }, { -3, 3 }, { -4, 4 }, { -5, 5 }, { -6, 6 }, { -7, 7 }, { -8, 8 },
        { 1, -1 }, { 2, -2 }, { 3, -3 }, { 4, -4 }, { 5, -5 }, { 6, -6 }, { 7, -7 }, { 8, -8 },
        { -1, -1 }, { -2, -2 }, { -3, -3 }, { -4, -4 }, { -5, -5 }, { -6, -6 }, { -7, -7 }, { -8, -8 },
        { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 0, 6 }, { 0, 7 }, { 0, 8 },
        { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { 0, -1 }, { 0, -2 }, { 0, -3 }, { 0, -4 }, { 0, -5 }, { 0, -6 }, { 0, -7 }, { 0, -8 },
        { -1, 0 }, { -2, 0 }, { -3, 0 }, { -4, 0 }, { -5, 0 }, { -6, 0 }, { -7, 0 }, { -8, 0 }
    }; //Flag: In check
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ProtectivePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wProtectivePawn";
    public String bImage { get; set; } = "Images/Pawns/bProtectivePawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Protective";
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

    public ProtectivePawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
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

public class PAWN : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PAWN";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPAWN!";
    public String bImage { get; set; } = "Images/Pawns/bPAWN!";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "PAWN";
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

    public PAWN(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
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

public class DoublePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DoublePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wDoublePawn";
    public String bImage { get; set; } = "Images/Pawns/bDoublePawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Double";
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

    public DoublePawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
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

public class OppressivePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3f;
    public bool disabled { get; set; } = false;
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OppressivePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wOppressivePawn";
    public String bImage { get; set; } = "Images/Pawns/bOppressivePawn";
    public int rarityLevel { get; set; } = 1;
    public int[] startSquare { get; set; } = null;
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Oppressive";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = -1;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
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

    public OppressivePawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}

public class DelayedPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public bool disabled { get; set; } = false;
    public int rarityLevel { get; set; } = 1;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 }, { 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DelayedPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wDelayedPawn";
    public String bImage { get; set; } = "Images/Pawns/bDelayedPawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
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

    public DelayedPawn(int color, bool online)
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
        Sprite sp = Resources.Load<Sprite>(color == 1 ? wImage : bImage);

        s.sprite = sp;
        s.preserveAspect = true;
    }
}
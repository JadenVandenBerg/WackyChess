using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;

/*
public class MurderousPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] dependentAttacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "MurderousPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wMurderousPawn";
    public String bImage { get; set; } = "Images/Pawns/bMurderousPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. Or Killing them, since you can do that too.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Murderous;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public coords[] dependentMovesSet()
    {
        //Simulate Murderous
        dependentAttacks = new coords[] { };

        GameObject newSquare = HelperFunctions.findSquare(this.position[0], this.position[1] + 1);
        if (newSquare != null)
        {
            if (HelperFunctions.isPieceOnSquare(newSquare) && HelperFunctions.getColorsOnSquare(newSquare, true).Contains(this.color))
            {
                newSquare = HelperFunctions.findSquare(this.position[0], this.position[1] + 2);
                if (HelperFunctions.isPieceOnSquare(newSquare) && this.hasMoved == false)
                {
                    HelperFunctions.addTo2DArray(dependentAttacks, new coords new coords(0, 2));
                }
            }
        }

        return dependentAttacks;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public MurderousPawn(int color, bool online, bool simulated)
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

public class GhostPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(1, 1), new coords(-1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Ghost Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wGhostPawn";
    public String bImage { get; set; } = "Images/Pawns/bGhostPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. This piece can move through your other pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Ghost;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public GhostPawn(int color, bool online, bool simulated)
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

public class GhoulPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] attacks { get; set; } = { new coords(1, 1), new coords(-1, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Ghoul Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wGhoulPawn";
    public String bImage { get; set; } = "Images/Pawns/bGhoulPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "Normal pawn, but your pieces can move through this piece.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Ghoul;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public GhoulPawn(int color, bool online, bool simulated)
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

/*
public class OneTimePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -0.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2), new coords(0, 1) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] dependentAttacks { get; set; } = { new coords(1, 1), new coords(-1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "One Time Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wOneTimePawn";
    public String bImage { get; set; } = "Images/Pawns/bOneTimePawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "Normal pawn, but can only move once.";
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
    public coords[] dependentMovesSet()
    {
        dependentAttacks = new coords[] { };

        GameObject newSquare = HelperFunctions.findSquare(this.position[0] + 1, this.position[1] + 1);
        if (newSquare != null && !HelperFunctions.isJump(this, this.position, HelperFunctions.findCoords(newSquare)))
        {
            if (HelperFunctions.isPieceOnSquare(newSquare) || HelperFunctions.isColorNotOnSquare(newSquare, this.color))
            {
                HelperFunctions.addTo2DArray(dependentAttacks, new coords new coords(1, 1));
            }
        }

        newSquare = HelperFunctions.findSquare(this.position[0] + -1, this.position[1] + 1);
        if (newSquare != null && !HelperFunctions.isJump(this, this.position, HelperFunctions.findCoords(newSquare)))
        {
            if (HelperFunctions.isPieceOnSquare(newSquare) || HelperFunctions.isColorNotOnSquare(newSquare, this.color))
            {
                HelperFunctions.addTo2DArray(dependentAttacks, new coords new coords(-1, 1));
            }
        }

        return dependentAttacks;
    }

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public OneTimePawn(int color, bool online, bool simulated)
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

public class ElectricPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Electric Pawn";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wElectricPawn";
    public String bImage { get; set; } = "Images/Pawns/bElectricPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "This piece moves like a pawn. On its capture, there is a 50% chance the capturing piece will die.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Electric;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public ElectricPawn(int color, bool online, bool simulated)
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

public class ShieldPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Shield Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wShieldPawn";
    public String bImage { get; set; } = "Images/Pawns/bShieldPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Shield;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public ShieldPawn(int color, bool online, bool simulated)
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

public class InfinitePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "Infinite Pawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wInfinitePawn";
    public String bImage { get; set; } = "Images/Pawns/bInfinitePawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = -1;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public InfinitePawn(int color, bool online, bool simulated)
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

public class PortalPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PortalPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPortalPawn";
    public String bImage { get; set; } = "Images/Pawns/bPortalPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Portal;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public PortalPawn(int color, bool online, bool simulated)
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

public class AtomicPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "AtomicPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wAtomicPawn";
    public String bImage { get; set; } = "Images/Pawns/bAtomicPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = 0;
    public coords[] collateral { get; set; } = { new coords(0, 0), new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public AtomicPawn(int color, bool online, bool simulated)
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

public class LandminePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 4;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LandminePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wLandminePawn";
    public String bImage { get; set; } = "Images/Pawns/bLandminePawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = 1;
    public coords[] collateral { get; set; } = { new coords(0, 0), new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, 1), new coords(1, 0), new coords(0, -1), new coords(-1, 0) };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public LandminePawn(int color, bool online, bool simulated)
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

public class SpontaneouslyCombustingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = -2f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SpontaneouslyCombustingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wSpontaneouslyCombustingPawn";
    public String bImage { get; set; } = "Images/Pawns/bSpontaneouslyCombustingPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Combustable;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public SpontaneouslyCombustingPawn(int color, bool online, bool simulated)
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

public class SuperGhostPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(1, 1), new coords(-1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SuperGhostPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wSuperGhostPawn";
    public String bImage { get; set; } = "Images/Pawns/bSuperGhostPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. This piece can move through your other pieces.";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Ghoul | PieceState.Ghost;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public SuperGhostPawn(int color, bool online, bool simulated)
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

public class FragilePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FragilePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wFragilePawn";
    public String bImage { get; set; } = "Images/Pawns/bFragilePawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Fragile;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public FragilePawn(int color, bool online, bool simulated)
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

public class CrowdingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "CrowdingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wCrowdingPawn";
    public String bImage { get; set; } = "Images/Pawns/bCrowdingPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Crowding;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public CrowdingPawn(int color, bool online, bool simulated)
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

public class HungryPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "HungryPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wHungryPawn";
    public String bImage { get; set; } = "Images/Pawns/bHungryPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Vomit;
    public PieceState states { get; set; } = PieceState.Hungry;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public HungryPawn(int color, bool online, bool simulated)
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

public class CaptureTheFlagPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "CaptureTheFlagPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wCaptureTheFlagPawn";
    public String bImage { get; set; } = "Images/Pawns/bCaptureTheFlagPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.CaptureTheFlag;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public CaptureTheFlagPawn(int color, bool online, bool simulated)
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

public class FreezingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FreezingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wFreezingPawn";
    public String bImage { get; set; } = "Images/Pawns/bFreezingPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Freeze;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public FreezingPawn(int color, bool online, bool simulated)
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

public class CloningPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "CloningPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wCloningPawn";
    public String bImage { get; set; } = "Images/Pawns/bCloningPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Spawn;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "Pawn";
    public int numSpawns { get; set; } = 2;

    public CloningPawn(int color, bool online, bool simulated)
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

public class ZombiePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { };
    public coords[] oneTimeMoves { get; set; } = { };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { new coords(0, 1) };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ZombiePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wZombiePawn";
    public String bImage { get; set; } = "Images/Pawns/bZombiePawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
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

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public ZombiePawn(int color, bool online, bool simulated)
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

public class UndeadPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "UndeadPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wUndeadPawn";
    public String bImage { get; set; } = "Images/Pawns/bUndeadPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Spawn;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "ZombiePawn";
    public int numSpawns { get; set; } = 3;

    public UndeadPawn(int color, bool online, bool simulated)
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

public class PromotionPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PromotionPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPromotionPawn";
    public String bImage { get; set; } = "Images/Pawns/bPromotionPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "Knight";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public PromotionPawn(int color, bool online, bool simulated)
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

public class DefuserPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DefuserPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wDefuserPawn";
    public String bImage { get; set; } = "Images/Pawns/bDefuserPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Defuser;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public DefuserPawn(int color, bool online, bool simulated)
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

public class SpittingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SpittingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wSpittingPawn";
    public String bImage { get; set; } = "Images/Pawns/bSpittingPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Spit;
    public PieceState states { get; set; } = PieceState.Spitting;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = 1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public SpittingPawn(int color, bool online, bool simulated)
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
/*
public class PhantomPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PhantomPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPhantomPawn";
    public String bImage { get; set; } = "Images/Pawns/bPhantomPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Dematerialize;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public PhantomPawn(int color, bool online, bool simulated)
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
public class SplittingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SplittingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wSplittingPawn";
    public String bImage { get; set; } = "Images/Pawns/bSplittingPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.Split;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public SplittingPawn(int color, bool online, bool simulated)
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

public class PromotingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PromotingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPromotingPawn";
    public String bImage { get; set; } = "Images/Pawns/bPromotingPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.None;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 7;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public PromotingPawn(int color, bool online, bool simulated)
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

public class StackingPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "StackingPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wStackingPawn";
    public String bImage { get; set; } = "Images/Pawns/bStackingPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Stacking;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public StackingPawn(int color, bool online, bool simulated)
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

public class JailPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "JailPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wJailPawn";
    public String bImage { get; set; } = "Images/Pawns/bJailPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Jailer;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public JailPawn(int color, bool online, bool simulated)
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

public class PiggybackPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PiggybackPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPiggybackPawn";
    public String bImage { get; set; } = "Images/Pawns/bPiggybackPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public PiggybackPawn(int color, bool online, bool simulated)
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

public class JockeyPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "JockeyPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wJockeyPawn";
    public String bImage { get; set; } = "Images/Pawns/bJockeyPawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
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
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public JockeyPawn(int color, bool online, bool simulated)
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

public class ProtectivePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { }; //Flag: Not in check
    public coords[] flagMove2 { get; set; } = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) }; //Flag: In check
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ProtectivePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wProtectivePawn";
    public String bImage { get; set; } = "Images/Pawns/bProtectivePawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Protective;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public ProtectivePawn(int color, bool online, bool simulated)
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

public class PAWN : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 1.5f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "PAWN";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wPAWN!";
    public String bImage { get; set; } = "Images/Pawns/bPAWN!";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Pawn;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public PAWN(int color, bool online, bool simulated)
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

public class DoublePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 2f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DoublePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wDoublePawn";
    public String bImage { get; set; } = "Images/Pawns/bDoublePawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Double;
    public int stackable { get; set; } = 0;
    public int reverseStackable { get; set; } = 0;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public DoublePawn(int color, bool online, bool simulated)
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

public class OppressivePawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 3f;
    public bool disabled { get; set; } = false;
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OppressivePawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wOppressivePawn";
    public String bImage { get; set; } = "Images/Pawns/bOppressivePawn";
    public int rarityLevel { get; set; } = 1;
    public coords startSquare { get; set; } = new coords(-1, -1);
    public string baseType { get; set; } = "Pawn";
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Oppressive;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public OppressivePawn(int color, bool online, bool simulated)
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

public class DelayedPawn : Piece
{
    public int color { get; set; } = 1; //1 White, -1 Black
    public float points { get; set; } = 0f;
    public bool disabled { get; set; } = false;
    public int rarityLevel { get; set; } = 1;
    public string baseType { get; set; } = "Pawn";
    public coords[] moves { get; set; } = { new coords(0, 1) };
    public coords[] oneTimeMoves { get; set; } = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks { get; set; } = { };
    public coords[] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public coords[] conditionalAttacks { get; set; } = { };
    public coords[] attacks { get; set; } = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 { get; set; } = { };
    public coords[] flagMove2 { get; set; } = { };
    public coords[] pushMoves { get; set; } = { };
    public coords[] enPassantMoves { get; set; } = { };
    public coords position { get; set; } = new coords(0, 0);
    public coords[] jumpAttacks { get; set; } = { };
    public coords[] moveAndAttacks { get; set; } = { };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DelayedPawn";
    public bool hasMoved { get; set; } = false;

    public String wImage { get; set; } = "Images/Pawns/wDelayedPawn";
    public String bImage { get; set; } = "Images/Pawns/bDelayedPawn";
    public coords startSquare { get; set; } = new coords(-1, -1);
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 0;
    public PieceAbilities abilities { get; set; } = PieceAbilities.None;
    public PieceState states { get; set; } = PieceState.Delayed;
    public int collateralType { get; set; } = -1;
    public coords[] collateral { get; set; } = null;
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;

    public int flag { get; set; } = 0;
    public string spawnable { get; set; } = "";
    public int numSpawns { get; set; } = 0;

    public DelayedPawn(int color, bool online, bool simulated)
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
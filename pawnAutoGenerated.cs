using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;

/*public class ThreePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnLite(int color, bool online)
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

}*/
            


/*public class ThreePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnU";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnU";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnU(int color, bool online)
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

}*/
            


/*public class ThreePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnD";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnD";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnD(int color, bool online)
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

}*/
            


/*public class ThreePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnUD(int color, bool online)
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

}*/
            


/*public class ThreePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnLR(int color, bool online)
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

}*/
            


/*public class ThreePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnULUR(int color, bool online)
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

}*/
            


/*public class ThreePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnDLDR(int color, bool online)
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

}*/
            


/*public class ThreePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnULUUR(int color, bool online)
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

}*/
            


/*public class ThreePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnULURD(int color, bool online)
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

}*/
            


/*public class ThreePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnUDLDR(int color, bool online)
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

}*/
            


/*public class ThreePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnDLDDR(int color, bool online)
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

}*/
            


/*public class ThreePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnUDLR(int color, bool online)
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

}*/
            


/*public class ThreePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnULURLR(int color, bool online)
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

}*/
            


/*public class ThreePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class ThreePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "ThreePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wThreePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bThreePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public ThreePawnULUURLR(int color, bool online)
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

}*/
            


/*public class FourPawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnLite";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnLite";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnLite(int color, bool online)
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

}*/
            


/*public class FourPawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnU";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnU";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnU(int color, bool online)
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

}*/
            


/*public class FourPawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnD";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnD";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnD(int color, bool online)
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

}*/
            


/*public class FourPawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnUD";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnUD";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnUD(int color, bool online)
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

}*/
            


/*public class FourPawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnLR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnLR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnLR(int color, bool online)
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

}*/
            


/*public class FourPawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnULUR(int color, bool online)
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

}*/
            


/*public class FourPawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnDLDR(int color, bool online)
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

}*/
            


/*public class FourPawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnULUUR(int color, bool online)
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

}*/
            


/*public class FourPawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnULURD(int color, bool online)
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

}*/
            


/*public class FourPawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnUDLDR(int color, bool online)
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

}*/
            


/*public class FourPawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnDLDDR(int color, bool online)
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

}*/
            


/*public class FourPawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnUDLR(int color, bool online)
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

}*/
            


/*public class FourPawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnULURLR(int color, bool online)
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

}*/
            


/*public class FourPawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnULURDLDR(int color, bool online)
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

}*/
            


/*public class FourPawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FourPawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFourPawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bFourPawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public FourPawnULUURLR(int color, bool online)
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

}*/
            


/*public class FivePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnLite(int color, bool online)
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

}*/
            


/*public class FivePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnU";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnU";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnU(int color, bool online)
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

}*/
            


/*public class FivePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnD";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnD";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnD(int color, bool online)
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

}*/
            


/*public class FivePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnUD(int color, bool online)
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

}*/
            


/*public class FivePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnLR(int color, bool online)
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

}*/
            


/*public class FivePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnULUR(int color, bool online)
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

}*/
            


/*public class FivePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnDLDR(int color, bool online)
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

}*/
            


/*public class FivePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnULUUR(int color, bool online)
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

}*/
            


/*public class FivePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnULURD(int color, bool online)
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

}*/
            


/*public class FivePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnUDLDR(int color, bool online)
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

}*/
            


/*public class FivePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnDLDDR(int color, bool online)
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

}*/
            


/*public class FivePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnUDLR(int color, bool online)
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

}*/
            


/*public class FivePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnULURLR(int color, bool online)
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

}*/
            


/*public class FivePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class FivePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 5 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "FivePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wFivePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bFivePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public FivePawnULUURLR(int color, bool online)
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

}*/
            


/*public class SixPawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnLite";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnLite";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnLite(int color, bool online)
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

}*/
            


/*public class SixPawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnU";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnU";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnU(int color, bool online)
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

}*/
            


/*public class SixPawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnD";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnD";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnD(int color, bool online)
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

}*/
            


/*public class SixPawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnUD";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnUD";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnUD(int color, bool online)
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

}*/
            


/*public class SixPawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnLR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnLR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnLR(int color, bool online)
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

}*/
            


/*public class SixPawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnULUR(int color, bool online)
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

}*/
            


/*public class SixPawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnDLDR(int color, bool online)
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

}*/
            


/*public class SixPawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnULUUR(int color, bool online)
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

}*/
            


/*public class SixPawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnULURD(int color, bool online)
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

}*/
            


/*public class SixPawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnUDLDR(int color, bool online)
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

}*/
            


/*public class SixPawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnDLDDR(int color, bool online)
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

}*/
            


/*public class SixPawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnUDLR(int color, bool online)
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

}*/
            


/*public class SixPawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnULURLR(int color, bool online)
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

}*/
            


/*public class SixPawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnULURDLDR(int color, bool online)
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

}*/
            


/*public class SixPawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 6 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SixPawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSixPawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bSixPawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public SixPawnULUURLR(int color, bool online)
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

}*/
            


/*public class SevenPawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnLite";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnLite";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnLite(int color, bool online)
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

}*/
            


/*public class SevenPawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnU";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnU";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnU(int color, bool online)
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

}*/
            


/*public class SevenPawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnD";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnD";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnD(int color, bool online)
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

}*/
            


/*public class SevenPawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnUD";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnUD";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnUD(int color, bool online)
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

}*/
            


/*public class SevenPawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnLR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnLR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnLR(int color, bool online)
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

}*/
            


/*public class SevenPawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnULUR(int color, bool online)
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

}*/
            


/*public class SevenPawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnDLDR(int color, bool online)
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

}*/
            


/*public class SevenPawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnULUUR(int color, bool online)
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

}*/
            


/*public class SevenPawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnULURD(int color, bool online)
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

}*/
            


/*public class SevenPawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnUDLDR(int color, bool online)
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

}*/
            


/*public class SevenPawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnDLDDR(int color, bool online)
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

}*/
            


/*public class SevenPawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnUDLR(int color, bool online)
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

}*/
            


/*public class SevenPawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnULURLR(int color, bool online)
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

}*/
            


/*public class SevenPawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnULURDLDR(int color, bool online)
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

}*/
            


/*public class SevenPawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 7 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SevenPawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSevenPawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bSevenPawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public SevenPawnULUURLR(int color, bool online)
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

}*/
            


/**/
            


/**/
            


/**/
            


/**/
            


/**/
            


/**/
            


/**/
            


/**/
            


/*public class OneTwoPawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoPawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoPawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoPawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoPawnULURD(int color, bool online)
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

}*/
            


/*public class OneTwoPawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoPawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoPawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoPawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoPawnUDLDR(int color, bool online)
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

}*/
            


/*public class OneTwoPawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoPawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoPawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoPawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoPawnDLDDR(int color, bool online)
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

}*/
            


/*public class OneTwoPawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoPawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoPawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoPawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoPawnUDLR(int color, bool online)
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

}*/
            


/*public class OneTwoPawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoPawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoPawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoPawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoPawnULURLR(int color, bool online)
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

}*/
            


/*public class OneTwoPawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoPawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoPawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoPawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoPawnULURDLDR(int color, bool online)
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

}*/
            


/*public class OneTwoPawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoPawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoPawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoPawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoPawnULUURLR(int color, bool online)
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

}*/
            


/*public class OneThreePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnLite(int color, bool online)
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

}*/
            


/*public class OneThreePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnU";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnU";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnU(int color, bool online)
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

}*/
            


/*public class OneThreePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnD";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnD";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnD(int color, bool online)
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

}*/
            


/*public class OneThreePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnUD(int color, bool online)
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

}*/
            


/*public class OneThreePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnLR(int color, bool online)
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

}*/
            


/*public class OneThreePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnULUR(int color, bool online)
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

}*/
            


/*public class OneThreePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnDLDR(int color, bool online)
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

}*/
            


/*public class OneThreePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnULUUR(int color, bool online)
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

}*/
            


/*public class OneThreePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnULURD(int color, bool online)
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

}*/
            


/*public class OneThreePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnUDLDR(int color, bool online)
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

}*/
            


/*public class OneThreePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnDLDDR(int color, bool online)
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

}*/
            


/*public class OneThreePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnUDLR(int color, bool online)
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

}*/
            


/*public class OneThreePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnULURLR(int color, bool online)
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

}*/
            


/*public class OneThreePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class OneThreePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneThreePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneThreePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneThreePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneThreePawnULUURLR(int color, bool online)
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

}*/
            


/*public class OneFourPawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnLite";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnLite";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnLite(int color, bool online)
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

}*/
            


/*public class OneFourPawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnU";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnU";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnU(int color, bool online)
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

}*/
            


/*public class OneFourPawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnD";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnD";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnD(int color, bool online)
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

}*/
            


/*public class OneFourPawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnUD";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnUD";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnUD(int color, bool online)
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

}*/
            


/*public class OneFourPawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnLR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnLR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnLR(int color, bool online)
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

}*/
            


/*public class OneFourPawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnULUR(int color, bool online)
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

}*/
            


/*public class OneFourPawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnDLDR(int color, bool online)
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

}*/
            


/*public class OneFourPawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnULUUR(int color, bool online)
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

}*/
            


/*public class OneFourPawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnULURD(int color, bool online)
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

}*/
            


/*public class OneFourPawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnUDLDR(int color, bool online)
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

}*/
            


/*public class OneFourPawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnDLDDR(int color, bool online)
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

}*/
            


/*public class OneFourPawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnUDLR(int color, bool online)
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

}*/
            


/*public class OneFourPawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnULURLR(int color, bool online)
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

}*/
            


/*public class OneFourPawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnULURDLDR(int color, bool online)
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

}*/
            


/*public class OneFourPawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 4 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneFourPawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneFourPawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneFourPawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneFourPawnULUURLR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnLite(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnU";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnU";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnU(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnD";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnD";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnD(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnUD(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnLR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnULUR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnDLDR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnULUUR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnULURD(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnUDLDR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnDLDDR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnUDLR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnULURLR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class OneTwoThreePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, 2 },{ 0, 3 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneTwoThreePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneTwoThreePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneTwoThreePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneTwoThreePawnULUURLR(int color, bool online)
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

}*/
            


/*public class BackOnePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnLite(int color, bool online)
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

}*/
            


/*public class BackOnePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnU";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnU";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnU(int color, bool online)
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

}*/
            


/*public class BackOnePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnD";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnD";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnD(int color, bool online)
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

}*/
            


/*public class BackOnePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnUD(int color, bool online)
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

}*/
            


/*public class BackOnePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnLR(int color, bool online)
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

}*/
            


/*public class BackOnePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnULUR(int color, bool online)
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

}*/
            


/*public class BackOnePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnDLDR(int color, bool online)
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

}*/
            


/*public class BackOnePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnULUUR(int color, bool online)
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

}*/
            


/*public class BackOnePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnULURD(int color, bool online)
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

}*/
            


/*public class BackOnePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnUDLDR(int color, bool online)
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

}*/
            


/*public class BackOnePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnDLDDR(int color, bool online)
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

}*/
            


/*public class BackOnePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnUDLR(int color, bool online)
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

}*/
            


/*public class BackOnePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnULURLR(int color, bool online)
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

}*/
            


/*public class BackOnePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class BackOnePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "BackOnePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wBackOnePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bBackOnePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public BackOnePawnULUURLR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnLite(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnU";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnU";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnU(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnD";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnD";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnD(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnUD(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnLR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnULUR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnDLDR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnULUUR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnULURD(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnUDLDR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnDLDDR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnUDLR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnULURLR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class OneBackOnePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackOnePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackOnePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackOnePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackOnePawnULUURLR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnLite(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnU";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnU";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnU(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnD";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnD";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnD(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnUD(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnLR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnULUR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnDLDR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnULUUR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnULURD(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnUDLDR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnDLDDR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnUDLR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnULURLR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class DiagonalOnePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalOnePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalOnePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalOnePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalOnePawnULUURLR(int color, bool online)
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

}*/
            


/*public class SquarePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnLite(int color, bool online)
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

}*/
            


/*public class SquarePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnU";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnU";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnU(int color, bool online)
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

}*/
            


/*public class SquarePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnD";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnD";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnD(int color, bool online)
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

}*/
            


/*public class SquarePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnUD(int color, bool online)
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

}*/
            


/*public class SquarePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnLR(int color, bool online)
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

}*/
            


/*public class SquarePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnULUR(int color, bool online)
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

}*/
            


/*public class SquarePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnDLDR(int color, bool online)
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

}*/
            


/*public class SquarePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnULUUR(int color, bool online)
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

}*/
            


/*public class SquarePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnULURD(int color, bool online)
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

}*/
            


/*public class SquarePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnUDLDR(int color, bool online)
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

}*/
            


/*public class SquarePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnDLDDR(int color, bool online)
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

}*/
            


/*public class SquarePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnUDLR(int color, bool online)
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

}*/
            


/*public class SquarePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnULURLR(int color, bool online)
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

}*/
            


/*public class SquarePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class SquarePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "SquarePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wSquarePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bSquarePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public SquarePawnULUURLR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnLite(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnU";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnU";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnU(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnD";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnD";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnD(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnUD(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnLR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnULUR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnDLDR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnULUUR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnULURD(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnUDLDR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnDLDDR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnUDLR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnULURLR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class DiagonalSquarePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "DiagonalSquarePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wDiagonalSquarePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bDiagonalSquarePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public DiagonalSquarePawnULUURLR(int color, bool online)
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

}*/
            


/*public class LitePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = -1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnLite(int color, bool online)
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

}*/
            


/*public class LitePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnU";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnU";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnU(int color, bool online)
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

}*/
            


/*public class LitePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnD";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnD";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnD(int color, bool online)
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

}*/
            


/*public class LitePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnUD(int color, bool online)
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

}*/
            


/*public class LitePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnLR(int color, bool online)
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

}*/
            


/*public class LitePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnULUR(int color, bool online)
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

}*/
            


/*public class LitePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnDLDR(int color, bool online)
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

}*/
            


/*public class LitePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnULUUR(int color, bool online)
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

}*/
            


/*public class LitePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnULURD(int color, bool online)
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

}*/
            


/*public class LitePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnUDLDR(int color, bool online)
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

}*/
            


/*public class LitePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnDLDDR(int color, bool online)
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

}*/
            


/*public class LitePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnUDLR(int color, bool online)
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

}*/
            


/*public class LitePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnULURLR(int color, bool online)
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

}*/
            


/*public class LitePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class LitePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = {  };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "LitePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wLitePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bLitePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public LitePawnULUURLR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnLite(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnU";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnU";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnU(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnD";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnD";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnD(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnUD(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnLR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnULUR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnDLDR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnULUUR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnULURD(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnUDLDR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnDLDDR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnUDLR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnULURLR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class OneDiagonalOnePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, 1 },{ -1, 1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneDiagonalOnePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneDiagonalOnePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneDiagonalOnePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneDiagonalOnePawnULUURLR(int color, bool online)
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

}*/
            


/*public class OctaPawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnLite";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnLite";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnLite(int color, bool online)
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

}*/
            


/*public class OctaPawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnU";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnU";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnU(int color, bool online)
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

}*/
            


/*public class OctaPawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnD";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnD";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnD(int color, bool online)
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

}*/
            


/*public class OctaPawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnUD";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnUD";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnUD(int color, bool online)
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

}*/
            


/*public class OctaPawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnLR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnLR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnLR(int color, bool online)
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

}*/
            


/*public class OctaPawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnULUR(int color, bool online)
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

}*/
            


/*public class OctaPawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnDLDR(int color, bool online)
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

}*/
            


/*public class OctaPawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnULUUR(int color, bool online)
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

}*/
            


/*public class OctaPawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnULURD(int color, bool online)
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

}*/
            


/*public class OctaPawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnUDLDR(int color, bool online)
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

}*/
            


/*public class OctaPawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnDLDDR(int color, bool online)
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

}*/
            


/*public class OctaPawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnUDLR(int color, bool online)
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

}*/
            


/*public class OctaPawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnULURLR(int color, bool online)
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

}*/
            


/*public class OctaPawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 3;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnULURDLDR(int color, bool online)
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

}*/
            


/*public class OctaPawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 4;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 },{ 1, 0 },{ -1, 0 },{ 0, 1 },{ 0, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OctaPawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOctaPawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bOctaPawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public OctaPawnULUURLR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnLite : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 0;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = {  };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnLite";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnLite";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnLite";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnLite(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnU : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnU";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnU";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnU";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnU(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnD";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnD";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnD(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnUD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnUD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnUD";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnUD";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnUD(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnLR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnLR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnLR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnULUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnULUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnULUR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnULUR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnULUR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnDLDR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnULUUR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnULUUR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnULUUR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnULUUR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnULUUR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnULURD : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ -1, 1 },{ 0, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnULURD";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnULURD";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnULURD";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnULURD(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnUDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnUDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnUDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnUDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnUDLDR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnDLDDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 1;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, -1 },{ -1, -1 },{ 1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnDLDDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnDLDDR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnDLDDR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnDLDDR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnUDLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 0, 1 },{ 0, -1 },{ -1, 0 },{ 1, 0 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnUDLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnUDLR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnUDLR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnUDLR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnULURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnULURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnULURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnULURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnULURLR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnULURDLDR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { 1, 1 },{ 1, -1 },{ -1, 1 },{ -1, -1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnULURDLDR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnULURDLDR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnULURDLDR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnULURDLDR(int color, bool online)
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

}*/
            


/*public class OneBackDiagonalOnePawnULUURLR : Piece
{
    public int color { get; set; } = 1;
    public float points { get; set; } = 2;
    public bool disabled { get; set; } = false;
    public string baseType { get; set; } = "Pawn";
    public int[,] moves { get; set; } = { { 0, 1 },{ 1, -1 },{ -1, -1 } };
    public int[,] oneTimeMoves { get; set; } = { { 0, 2 } };
    public int numSpawns { get; set; } = 0;
    public int rarityLevel { get; set; } = 1;
    public int[,] oneTimeMoveAndAttacks { get; set; } = { };
    public int[,] murderousAttacks { get; set; } = { };
    public bool condition { get; set; } = false;
    public int[,] conditionalAttacks { get; set; } = { };
    public int[,] attacks { get; set; } = { { -1, 0 },{ 1, 0 },{ 0, 1 },{ -1, 1 },{ 1, 1 } };
    public int[,] dependentAttacks { get; set; } = { };
    public int[,] interactiveAttacks { get; set; } = { };
    public int[,] positionIndependentMoves { get; set; } = { };
    public int[,] forceStayTurnMoves { get; set; } = { };
    public int[,] flagMove1 { get; set; } = { };
    public int[,] flagMove2 { get; set; } = { };
    public int[,] pushMoves { get; set; } = { };
    public int[,] enPassantMoves { get; set; } = { };
    public int[,] jumpAttacks { get; set; } = { };
    public int[,] moveAndAttacks { get; set; } = { };
    public int[] position { get; set; } = { 0, 0 };
    public PhotonView photonView { get; set; } = null;
    public GameObject go { get; set; } = null;
    public String name { get; set; } = "OneBackDiagonalOnePawnULUURLR";
    public bool hasMoved { get; set; } = false;
    public String wImage { get; set; } = "Images/Pawns/wOneBackDiagonalOnePawnULUURLR";
    public String bImage { get; set; } = "Images/Pawns/bOneBackDiagonalOnePawnULUURLR";
    public int[] startSquare { get; set; } = null;
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
    public OneBackDiagonalOnePawnULUURLR(int color, bool online)
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

}*/
            
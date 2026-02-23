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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wThreePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bThreePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFourPawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFourPawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wFivePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bFivePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSixPawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSixPawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSevenPawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSevenPawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoPawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoPawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoPawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoPawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoPawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoPawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoPawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoPawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoPawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoPawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoPawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoPawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoPawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoPawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneThreePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneThreePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneFourPawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneFourPawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneTwoThreePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneTwoThreePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wBackOnePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bBackOnePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackOnePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackOnePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalOnePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalOnePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wSquarePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bSquarePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wDiagonalSquarePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bDiagonalSquarePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wLitePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bLitePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneDiagonalOnePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneDiagonalOnePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOctaPawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOctaPawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnLite.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnLite.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnU.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnU.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnUD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnUD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnULUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnULUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnULUUR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnULUUR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnULURD.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnULURD.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnUDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnUDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnDLDDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnDLDDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnUDLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnUDLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnULURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnULURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnULURDLDR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnULURDLDR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
    public int[,] oneTimeMovesAndAttacks { get; set; } = { };
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
    public String wImage { get; set; } = "Assets/Images/Pawns/wOneBackDiagonalOnePawnULUURLR.png";
    public String bImage { get; set; } = "Assets/Images/Pawns/bOneBackDiagonalOnePawnULUURLR.png";
    public int[] startSquare { get; set; } = null;
    public String description { get; set; } = "";
    public String longDescription { get; set; } = "";
    public int alive { get; set; } = 1;
    public int lives { get; set; } = 1;
    public String ability { get; set; } = "None";
    public String state { get; set; } = "Normal";
    public String secondaryState { get; set; } = "Normal";
    public int collateralType { get; set; } = 0;
    public int[,] collateral { get; set; } = null;
    public int[] size { get; set; } = new int[] { 1, 1 };
    public String promotesInto { get; set; } = "SuperPawn";
    public int promotingRow { get; set; } = 8;
    public int canMoveTwice { get; set; } = 0;
    public int storageLimit { get; set; } = -1;
    public List<Piece> storage { get; set; } = null;
    public int[,] dependentMovesSet()
    {
        return new int[,] { };
    }

    public int[,] interactiveMovesSet()
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
            
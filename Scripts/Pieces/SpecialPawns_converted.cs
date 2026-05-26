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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] dependentAttacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public GameObject go = null;
    public string name = "MurderousPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wMurderousPawn";
    public static readonly string bImage = "Images/Pawns/bMurderousPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. Or Killing them, since you can do that too.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Murderous;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;
    public static readonly coords[] dependentMovesSet()
    {
        //Simulate Murderous
        dependentAttacks = new coords[] Array.Empty<coords>();

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

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public GameObject go = null;
    public string name = "Ghost Pawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wGhostPawn";
    public static readonly string bImage = "Images/Pawns/bGhostPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. This piece can move through your other pieces.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Ghost;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public GameObject go = null;
    public string name = "Ghoul Pawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wGhoulPawn";
    public static readonly string bImage = "Images/Pawns/bGhoulPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "Normal pawn, but your pieces can move through this piece.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Ghoul;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -0.5f;
    public bool disabled = false;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = { new coords(0, 2), new coords(0, 1) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] dependentAttacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public GameObject go = null;
    public string name = "One Time Pawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wOneTimePawn";
    public static readonly string bImage = "Images/Pawns/bOneTimePawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "Normal pawn, but can only move once.";
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
    public static readonly coords[] dependentMovesSet()
    {
        dependentAttacks = new coords[] Array.Empty<coords>();

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

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "Electric Pawn";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wElectricPawn";
    public static readonly string bImage = "Images/Pawns/bElectricPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This piece moves like a pawn. On its capture, there is a 50% chance the capturing piece will die.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Electric;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2f;
    public bool disabled = false;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "Shield Pawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wShieldPawn";
    public static readonly string bImage = "Images/Pawns/bShieldPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Shield;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "Infinite Pawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wInfinitePawn";
    public static readonly string bImage = "Images/Pawns/bInfinitePawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = -1;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "PortalPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wPortalPawn";
    public static readonly string bImage = "Images/Pawns/bPortalPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Portal;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "AtomicPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wAtomicPawn";
    public static readonly string bImage = "Images/Pawns/bAtomicPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = 0;
    public static readonly coords[] collateral = moveDefs.standardCollateral;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 4;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "LandminePawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wLandminePawn";
    public static readonly string bImage = "Images/Pawns/bLandminePawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = 1;
    public static readonly coords[] collateral = moveDefs.standardCollateral;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -2f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "SpontaneouslyCombustingPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wSpontaneouslyCombustingPawn";
    public static readonly string bImage = "Images/Pawns/bSpontaneouslyCombustingPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Combustable;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public GameObject go = null;
    public string name = "SuperGhostPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wSuperGhostPawn";
    public static readonly string bImage = "Images/Pawns/bSuperGhostPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This piece can move up one square, and attack diagonally up one square. These pieces are effective at protecting your more important pieces. This piece can move through your other pieces.";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Ghoul | PieceState.Ghost;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "FragilePawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wFragilePawn";
    public static readonly string bImage = "Images/Pawns/bFragilePawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Fragile;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "CrowdingPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wCrowdingPawn";
    public static readonly string bImage = "Images/Pawns/bCrowdingPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Crowding;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "HungryPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wHungryPawn";
    public static readonly string bImage = "Images/Pawns/bHungryPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Vomit;
    public PieceState states = PieceState.Hungry;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "CaptureTheFlagPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wCaptureTheFlagPawn";
    public static readonly string bImage = "Images/Pawns/bCaptureTheFlagPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.CaptureTheFlag;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "FreezingPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wFreezingPawn";
    public static readonly string bImage = "Images/Pawns/bFreezingPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Freeze;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "CloningPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wCloningPawn";
    public static readonly string bImage = "Images/Pawns/bCloningPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Spawn;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "Pawn";
    public static readonly int numSpawns = 2;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0.5f;
    public bool disabled = false;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(0, 1) };
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "ZombiePawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wZombiePawn";
    public static readonly string bImage = "Images/Pawns/bZombiePawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
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

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "UndeadPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wUndeadPawn";
    public static readonly string bImage = "Images/Pawns/bUndeadPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Spawn;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "ZombiePawn";
    public static readonly int numSpawns = 3;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "PromotionPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wPromotionPawn";
    public static readonly string bImage = "Images/Pawns/bPromotionPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "Knight";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "DefuserPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wDefuserPawn";
    public static readonly string bImage = "Images/Pawns/bDefuserPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Defuser;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "SpittingPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wSpittingPawn";
    public static readonly string bImage = "Images/Pawns/bSpittingPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Spit;
    public PieceState states = PieceState.Spitting;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = 1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "PhantomPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wPhantomPawn";
    public static readonly string bImage = "Images/Pawns/bPhantomPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Dematerialize;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "SplittingPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wSplittingPawn";
    public static readonly string bImage = "Images/Pawns/bSplittingPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.Split;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "PromotingPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wPromotingPawn";
    public static readonly string bImage = "Images/Pawns/bPromotingPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.None;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 7;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 3f;
    public bool disabled = false;
    public coords[] moves = { new coords(0, 1) };
    public coords[] oneTimeMoves = { new coords(0, 2) };
    public coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public coords[] conditionalAttacks = Array.Empty<coords>();
    public coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public coords[] flagMove1 = Array.Empty<coords>();
    public coords[] flagMove2 = Array.Empty<coords>();
    public coords[] pushMoves = Array.Empty<coords>();
    public coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public coords[] jumpAttacks = Array.Empty<coords>();
    public coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "StackingPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wStackingPawn";
    public static readonly string bImage = "Images/Pawns/bStackingPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Stacking;
    public int collateralType = -1;
    public coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "JailPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wJailPawn";
    public static readonly string bImage = "Images/Pawns/bJailPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Jailer;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "PiggybackPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wPiggybackPawn";
    public static readonly string bImage = "Images/Pawns/bPiggybackPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Piggyback;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "JockeyPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wJockeyPawn";
    public static readonly string bImage = "Images/Pawns/bJockeyPawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Jockey;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>(); //Flag: Not in check
    public static readonly coords[] flagMove2 = { new coords(1, 1), new coords(2, 2), new coords(3, 3), new coords(4, 4), new coords(5, 5), new coords(6, 6), new coords(7, 7), new coords(8, 8), new coords(-1, 1), new coords(-2, 2), new coords(-3, 3), new coords(-4, 4), new coords(-5, 5), new coords(-6, 6), new coords(-7, 7), new coords(-8, 8), new coords(1, -1), new coords(2, -2), new coords(3, -3), new coords(4, -4), new coords(5, -5), new coords(6, -6), new coords(7, -7), new coords(8, -8), new coords(-1, -1), new coords(-2, -2), new coords(-3, -3), new coords(-4, -4), new coords(-5, -5), new coords(-6, -6), new coords(-7, -7), new coords(-8, -8), new coords(0, 1), new coords(0, 2), new coords(0, 3), new coords(0, 4), new coords(0, 5), new coords(0, 6), new coords(0, 7), new coords(0, 8), new coords(1, 0), new coords(2, 0), new coords(3, 0), new coords(4, 0), new coords(5, 0), new coords(6, 0), new coords(7, 0), new coords(8, 0), new coords(0, -1), new coords(0, -2), new coords(0, -3), new coords(0, -4), new coords(0, -5), new coords(0, -6), new coords(0, -7), new coords(0, -8), new coords(-1, 0), new coords(-2, 0), new coords(-3, 0), new coords(-4, 0), new coords(-5, 0), new coords(-6, 0), new coords(-7, 0), new coords(-8, 0) }; //Flag: In check
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "ProtectivePawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wProtectivePawn";
    public static readonly string bImage = "Images/Pawns/bProtectivePawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Protective;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "PAWN";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wPAWN!";
    public static readonly string bImage = "Images/Pawns/bPAWN!";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Pawn;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "DoublePawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wDoublePawn";
    public static readonly string bImage = "Images/Pawns/bDoublePawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Double;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 3f;
    public bool disabled = false;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OppressivePawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wOppressivePawn";
    public static readonly string bImage = "Images/Pawns/bOppressivePawn";
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Oppressive;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0f;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "DelayedPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wDelayedPawn";
    public static readonly string bImage = "Images/Pawns/bDelayedPawn";
    public coords startSquare = new coords(-1, -1);
    public static readonly string description = "";
    public static readonly string longDescription = "";
    public int alive = 1;
    public int lives = 0;
    public PieceAbilities abilities = PieceAbilities.None;
    public PieceState states = PieceState.Delayed;
    public int collateralType = -1;
    public static readonly coords[] collateral = null;
    public static readonly string promotesInto = "SuperPawn";
    public static readonly int promotingRow = 8;
    public static readonly int storageLimit = -1;
    public List<Piece> storage = null;

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

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
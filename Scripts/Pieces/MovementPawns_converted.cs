using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using Photon.Pun;

public class Pawn : Piece
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
    public string name = "Pawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wPawn";
    public static readonly string bImage = "Images/Pawns/bPawn";
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

    public Pawn(int color, bool online, bool simulated)
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
public class TwoPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "Similar to a normal pawn, but this piece is capable of moving up two squares. What it lacks in defense it makes up for with its speedy promotions";
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
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wTwoPawn";
    public static readonly string bImage = "Images/Pawns/bTwoPawn";
    public string name = "Two Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public TwoPawn(int color, bool online, bool simulated)
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

public class ThreePawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "Similar to a normal pawn, but this piece is capable of moving up three squares. What it lacks in defense it makes up for with its speedy promotions";
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
    public static readonly coords[] moves = { new coords(0, 3) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wThreePawn";
    public static readonly string bImage = "Images/Pawns/bThreePawn";
    public string name = "Three Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ThreePawn(int color, bool online, bool simulated)
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

public class FourPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "Similar to a normal pawn, but this piece is capable of moving up four squares. What it lacks in defense it makes up for with its speedy promotions";
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
    public static readonly coords[] moves = { new coords(0, 4) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wFourPawn";
    public static readonly string bImage = "Images/Pawns/bFourPawn";
    public string name = "Four Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public FourPawn(int color, bool online, bool simulated)
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

public class OneTwoPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "A strong pawn that can move one or two squares forward.";
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
    public static readonly coords[] moves = { new coords(0, 1), new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wOneTwoPawn";
    public static readonly string bImage = "Images/Pawns/bOneTwoPawn";
    public string name = "One-Two Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public OneTwoPawn(int color, bool online, bool simulated)
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

public class ForwardPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This pawn both attacks and moves forwards.";
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
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] moveAndAttacks = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public GameObject go = null;
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wForwardPawn";
    public static readonly string bImage = "Images/Pawns/bForwardPawn";
    public string name = "Forward Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ForwardPawn(int color, bool online, bool simulated)
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

public class TwoForwardPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This pawn both attacks and moves forwards. Can move two squares at a time";
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
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] attacks = { new coords(0, 1) };
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

    public static readonly string wImage = "Images/Pawns/wTwoForwardPawn";
    public static readonly string bImage = "Images/Pawns/bTwoForwardPawn";
    public string name = "Two-Forward Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public TwoForwardPawn(int color, bool online, bool simulated)
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

public class OneTwoForwardPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This pawn both attacks and moves forwards. Can move one or two squares at a time";
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
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] attacks = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(0, 1) };
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

    public static readonly string wImage = "Images/Pawns/wOneTwoForwardPawn";
    public static readonly string bImage = "Images/Pawns/bOneTwoForwardPawn";
    public string name = "One-Two-Forward Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public OneTwoForwardPawn(int color, bool online, bool simulated)
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

public class UpperPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "A strong, defensive pawn. Can attack in all upwards directions, this pawn has no weak points when in formation.";
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
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] moveAndAttacks = { new coords(0, 1) };
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

    public static readonly string wImage = "Images/Pawns/wUpperPawn";
    public static readonly string bImage = "Images/Pawns/bUpperPawn";
    public string name = "Upper Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public UpperPawn(int color, bool online, bool simulated)
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

public class DiagonalSquarePawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "A strong, defensive pawn that protects the pawns in front and below it.";
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
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
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

    public static readonly string wImage = "Images/Pawns/wDiagonalSquarePawn";
    public static readonly string bImage = "Images/Pawns/bDiagonalSquarePawn";
    public string name = "Diagonal Square Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public DiagonalSquarePawn(int color, bool online, bool simulated)
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

public class OctaPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "The powerhouse of pawns, this piece can attack anywhere it pleases.";
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
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, -1), new coords(1, 0), new coords(-1, 0) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] moveAndAttacks = { new coords(0, 1) };
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

    public static readonly string wImage = "Images/Pawns/wOctaPawn";
    public static readonly string bImage = "Images/Pawns/bOctaPawn";
    public string name = "Octapawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public OctaPawn(int color, bool online, bool simulated)
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

public class OctaPawnLite : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "The powerhouse of pawns, this piece can attack anywhere it pleases. However, this piece cannot move";
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
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1), new coords(1, -1), new coords(-1, -1), new coords(0, -1), new coords(1, 0), new coords(-1, 0), new coords(0, 1) };
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

    public static readonly string wImage = "Images/Pawns/wOctaPawnLite";
    public static readonly string bImage = "Images/Pawns/bOctaPawnLite";
    public string name = "Octapawn Lite";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public OctaPawnLite(int color, bool online, bool simulated)
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

public class ForwardSidePawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 1.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This pawn attacks forward, left, and right.";
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
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] attacks = { new coords(1, 0), new coords(-1, 0), new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
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

    public static readonly string wImage = "Images/Pawns/wForwardSidePawn";
    public static readonly string bImage = "Images/Pawns/bForwardSidePawn";
    public string name = "Forward-Side Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public ForwardSidePawn(int color, bool online, bool simulated)
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

public class SquarePawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 2f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This pawn attacks forward, backward, left, and right.";
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
    public static readonly coords[] attacks = { new coords(1, 0), new coords(-1, 0), new coords(0, 1), new coords(-1, 0) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] moveAndAttacks = { new coords(0, 1) };
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

    public static readonly string wImage = "Images/Pawns/wSquarePawn";
    public static readonly string bImage = "Images/Pawns/bSquarePawn";
    public string name = "Square Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public SquarePawn(int color, bool online, bool simulated)
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

public class LitePawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -0.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This pawn cannot move.";
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

    public static readonly string wImage = "Images/Pawns/wLitePawn";
    public static readonly string bImage = "Images/Pawns/bLitePawn";
    public string name = "Pawn Lite";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public LitePawn(int color, bool online, bool simulated)
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

public class BackwardPawn : Piece
{
    public bool disabled = false;
    public int color = 1; //1 White, -1 Black
    public static readonly float points = -0.5f;
    public static readonly int rarityLevel = 1;
    public coords startSquare = new coords(-1, -1);
    public static readonly string baseType = "Pawn";
    public static readonly string description = "This pawn moves forwards and attacks backwards";
    public static readonly string longDescription = "There once was a backward pawn who walked a backward mile. He lived in a backward house and had a backward smile.";
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
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] attacks = { new coords(0, -1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
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

    public static readonly string wImage = "Images/Pawns/wBackwardsPawn";
    public static readonly string bImage = "Images/Pawns/bBackwardsPawn";
    public string name = "Backward Pawn";

    public int flag = 0;
    public static readonly string spawnable = "";
    public static readonly int numSpawns = 0;

    public BackwardPawn(int color, bool online, bool simulated)
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

public class Man : Piece
{
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 3;
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
    public static readonly coords[] moveAndAttacks = moveDefs.kingMoves;
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "ManPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wMan";
    public static readonly string bImage = "Images/Pawns/bMan";
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

    public Man(int color, bool online, bool simulated)
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

public class LeftPawn : Piece
{
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0.5f;
    public bool disabled = false;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 1), new coords(-1, 0) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(0, 1) };
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "LeftPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wLeftPawn";
    public static readonly string bImage = "Images/Pawns/bLeftPawn";
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

    public LeftPawn(int color, bool online, bool simulated)
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

public class RightPawn : Piece
{
    public int color = 1; //1 White, -1 Black
    public static readonly float points = 0.5f;
    public bool disabled = false;
    public static readonly coords[] moves = Array.Empty<coords>();
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(1, 0) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = { new coords(0, 1) };
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "RightPawn";
    public bool hasMoved = false;

    public static readonly string wImage = "Images/Pawns/wRightPawn";
    public static readonly string bImage = "Images/Pawns/bRightPawn";
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

    public RightPawn(int color, bool online, bool simulated)
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

public class OnePawnLite : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly int rarityLevel = 1;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = {  };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnLite";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnLite";
    public static readonly string bImage = "Images/Pawns/bOnePawnLite";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnLite(int color, bool online, bool simulated)
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

public class OnePawnU : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnU";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnU";
    public static readonly string bImage = "Images/Pawns/bOnePawnU";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnU(int color, bool online, bool simulated)
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

public class OnePawnUD : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(0, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnUD";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnUD";
    public static readonly string bImage = "Images/Pawns/bOnePawnUD";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnUD(int color, bool online, bool simulated)
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

public class OnePawnD : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnD";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnD";
    public static readonly string bImage = "Images/Pawns/bOnePawnD";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnD(int color, bool online, bool simulated)
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

public class OnePawnLR : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 0), new coords(1, 0) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnLR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnLR";
    public static readonly string bImage = "Images/Pawns/bOnePawnLR";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnLR(int color, bool online, bool simulated)
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

public class OnePawnDLDR : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, -1), new coords(1, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnDLDR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnDLDR";
    public static readonly string bImage = "Images/Pawns/bOnePawnDLDR";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnDLDR(int color, bool online, bool simulated)
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

public class OnePawnULUUR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnULUUR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnULUUR";
    public static readonly string bImage = "Images/Pawns/bOnePawnULUUR";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnULUUR(int color, bool online, bool simulated)
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

public class OnePawnULURD : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly int rarityLevel = 1;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1), new coords(0, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnULURD";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnULURD";
    public static readonly string bImage = "Images/Pawns/bOnePawnULURD";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnULURD(int color, bool online, bool simulated)
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

public class OnePawnUDLDR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly int rarityLevel = 1;
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(-1, -1), new coords(1, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnUDLDR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnUDLDR";
    public static readonly string bImage = "Images/Pawns/bOnePawnUDLDR";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnUDLDR(int color, bool online, bool simulated)
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

public class OnePawnDLDDR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, -1), new coords(-1, -1), new coords(1, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnDLDDR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnDLDDR";
    public static readonly string bImage = "Images/Pawns/bOnePawnDLDDR";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnDLDDR(int color, bool online, bool simulated)
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

public class OnePawnULURLR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 0), new coords(1, 0), new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnULURLR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnULURLR";
    public static readonly string bImage = "Images/Pawns/bOnePawnULURLR";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnULURLR(int color, bool online, bool simulated)
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

public class OnePawnULUURLR : Piece
{
    public int color = 1;
    public static readonly int rarityLevel = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 0), new coords(1, 0), new coords(0, 1), new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OnePawnULUURLR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOnePawnULUURLR";
    public static readonly string bImage = "Images/Pawns/bOnePawnULUURLR";
    public coords startSquare = new coords(-1, -1);
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
    public OnePawnULUURLR(int color, bool online, bool simulated)
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

public class TwoPawnLite : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = {  };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnLite";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnLite";
    public static readonly string bImage = "Images/Pawns/bTwoPawnLite";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnLite(int color, bool online, bool simulated)
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

public class TwoPawnU : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnU";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnU";
    public static readonly string bImage = "Images/Pawns/bTwoPawnU";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnU(int color, bool online, bool simulated)
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

public class TwoPawnD : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnD";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnD";
    public static readonly string bImage = "Images/Pawns/bTwoPawnD";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnD(int color, bool online, bool simulated)
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

public class TwoPawnUD : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly int rarityLevel = 1;
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(0, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnUD";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnUD";
    public static readonly string bImage = "Images/Pawns/bTwoPawnUD";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnUD(int color, bool online, bool simulated)
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

public class TwoPawnLR : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 0), new coords(1, 0) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnLR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnLR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnLR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnLR(int color, bool online, bool simulated)
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

public class TwoPawnDLDR : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, -1), new coords(1, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnDLDR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnDLDR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnDLDR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnDLDR(int color, bool online, bool simulated)
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

public class TwoPawnULUUR : Piece
{
    public int color = 1;
    public static readonly int rarityLevel = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnULUUR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnULUUR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnULUUR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnULUUR(int color, bool online, bool simulated)
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

public class TwoPawnUDLDR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly int rarityLevel = 1;
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(-1, -1), new coords(1, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnUDLDR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnUDLDR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnUDLDR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnUDLDR(int color, bool online, bool simulated)
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

public class TwoPawnDLDDR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly int rarityLevel = 1;
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, -1), new coords(-1, -1), new coords(1, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnDLDDR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnDLDDR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnDLDDR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnDLDDR(int color, bool online, bool simulated)
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

public class TwoPawnULURD : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly int rarityLevel = 1;
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(-1, 1), new coords(0, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnULURD";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnULURD";
    public static readonly string bImage = "Images/Pawns/bTwoPawnULURD";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnULURD(int color, bool online, bool simulated)
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

public class TwoPawnUDLR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(0, -1), new coords(-1, 0), new coords(1, 0) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnUDLR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnUDLR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnUDLR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnUDLR(int color, bool online, bool simulated)
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

public class TwoPawnULURLR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 0), new coords(1, 0), new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnULURLR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnULURLR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnULURLR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnULURLR(int color, bool online, bool simulated)
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

public class TwoPawnULURDLDR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(1, 1), new coords(1, -1), new coords(-1, 1), new coords(-1, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnULURDLDR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnULURDLDR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnULURDLDR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnULURDLDR(int color, bool online, bool simulated)
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

public class TwoPawnULUURLR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 0), new coords(1, 0), new coords(0, 1), new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "TwoPawnULUURLR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wTwoPawnULUURLR";
    public static readonly string bImage = "Images/Pawns/bTwoPawnULUURLR";
    public coords startSquare = new coords(-1, -1);
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
    public TwoPawnULUURLR(int color, bool online, bool simulated)
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

public class OneTwoPawnLite : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1), new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = {  };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OneTwoPawnLite";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOneTwoPawnLite";
    public static readonly string bImage = "Images/Pawns/bOneTwoPawnLite";
    public coords startSquare = new coords(-1, -1);
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
    public OneTwoPawnLite(int color, bool online, bool simulated)
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

public class OneTwoPawnU : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1), new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OneTwoPawnU";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOneTwoPawnU";
    public static readonly string bImage = "Images/Pawns/bOneTwoPawnU";
    public coords startSquare = new coords(-1, -1);
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
    public OneTwoPawnU(int color, bool online, bool simulated)
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

public class OneTwoPawnD : Piece
{
    public int color = 1;
    public static readonly float points = 0;
    public static readonly int rarityLevel = 1;
    public bool disabled = false;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1), new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OneTwoPawnD";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOneTwoPawnD";
    public static readonly string bImage = "Images/Pawns/bOneTwoPawnD";
    public coords startSquare = new coords(-1, -1);
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
    public OneTwoPawnD(int color, bool online, bool simulated)
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

public class OneTwoPawnUD : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1), new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(0, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OneTwoPawnUD";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOneTwoPawnUD";
    public static readonly string bImage = "Images/Pawns/bOneTwoPawnUD";
    public coords startSquare = new coords(-1, -1);
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
    public OneTwoPawnUD(int color, bool online, bool simulated)
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

public class OneTwoPawnLR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1), new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, 0), new coords(1, 0) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OneTwoPawnLR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOneTwoPawnLR";
    public static readonly string bImage = "Images/Pawns/bOneTwoPawnLR";
    public coords startSquare = new coords(-1, -1);
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
    public OneTwoPawnLR(int color, bool online, bool simulated)
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

public class OneTwoPawnDLDR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1), new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(-1, -1), new coords(1, -1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OneTwoPawnDLDR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOneTwoPawnDLDR";
    public static readonly string bImage = "Images/Pawns/bOneTwoPawnDLDR";
    public coords startSquare = new coords(-1, -1);
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
    public OneTwoPawnDLDR(int color, bool online, bool simulated)
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

public class OneTwoPawnULUUR : Piece
{
    public int color = 1;
    public static readonly float points = 1;
    public bool disabled = false;
    public static readonly int rarityLevel = 1;
    public static readonly string baseType = "Pawn";
    public static readonly coords[] moves = { new coords(0, 1), new coords(0, 2) };
    public static readonly coords[] oneTimeMoves = { new coords(0, 2) };
    public static readonly int numSpawns = 0;
    public static readonly coords[] oneTimeMoveAndAttacks = Array.Empty<coords>();
    public static readonly coords[] murderousAttacks = Array.Empty<coords>();
    public bool condition = false;
    public static readonly coords[] conditionalAttacks = Array.Empty<coords>();
    public static readonly coords[] attacks = { new coords(0, 1), new coords(-1, 1), new coords(1, 1) };
    public static readonly coords[] flagMove1 = Array.Empty<coords>();
    public static readonly coords[] flagMove2 = Array.Empty<coords>();
    public static readonly coords[] pushMoves = Array.Empty<coords>();
    public static readonly coords[] enPassantMoves = Array.Empty<coords>();
    public static readonly coords[] jumpAttacks = Array.Empty<coords>();
    public static readonly coords[] moveAndAttacks = Array.Empty<coords>();
    public coords position = new coords(0, 0);
    public PhotonView photonView = null;
    public GameObject go = null;
    public string name = "OneTwoPawnULUUR";
    public bool hasMoved = false;
    public static readonly string wImage = "Images/Pawns/wOneTwoPawnULUUR";
    public static readonly string bImage = "Images/Pawns/bOneTwoPawnULUUR";
    public coords startSquare = new coords(-1, -1);
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
    public OneTwoPawnULUUR(int color, bool online, bool simulated)
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
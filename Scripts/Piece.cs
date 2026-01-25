using System;
using System.Collections.Generic;
using UnityEngine;

public interface Piece
{
    /* --------------------- INFO --------------------- */

    /*
     * Color
     * 1: White Piece
     * -1: Black Piece
     * 0: Both
     */
    public int color { get; set; }


    /*
     * Points
     * Used to determine how much the piece is worth.
     * Higher points for better pieces
     */
    public float points { get; set; }

    /*
     * Rarity Level
     * 0 - Start With
     * 1 - Pawn             35%
     * 2 - Common Item      35%
     * 3 - Uncommon Item    20%
     * 4 - Rare Item        8%
     * 5 - Legendary Item   2%
     */
    public int rarityLevel { get; set; }


    /*
     * Position
     * The position of a piece on the board
     * using 1 base indexing
     */
    public int[] position { get; set; }


    /*
     * Start Square
     * Where the piece starts
     */
    public int[] startSquare { get; set; }


    /*
     * Go
     * The gameobject associated with the piece
     */
    public GameObject go { get; set; }


    /*
     * wImage/bImage
     * The links to a pieces image for both colours
     */
    public String wImage { get; set; }
    public String bImage { get; set; }


    /*
     * Disabled
     * Used to ignore pieces in calculations of possible moves
     */
    public bool disabled { get; set; }


    /*
     * Name
     * The name of a piece
     */
    public String name { get; set; }


    /*
     * Description
     * A short description of the piece
     * Used in piece library
     */
    public String description { get; set; }


    /*
     * Long Description (not implemented yet)
     * A longer description of the piece, if necessary for aditional rules
     * Used in piece library
     */
    public String longDescription { get; set; }


    /* Alive
     * 1: On the board
     * 0: Off the board
     */
    public int alive { get; set; }

    /*
     * Lives
     * The amount of times a piece can respawn
     * 0: None
     * -1: Infinite
     */
    public int lives { get; set; }


    /* Ability
     * Different abilities are moves pieces can do instead of doing a regular move
     * 
     * None: No ability
     * Freeze: Can freeze pieces within a 1 block radius
     * Dematerialize: Can enter/exit dematerialized state (not implemented yet)
     * Vomit: Spit out all pieces in storage onto squares that have no pieces
     * Castle: Can castle
     * Rotate: Pieces can rotate 90 degrees and their moves rotate with them (not implemented yet)
     * Spawn: Can spawn other pieces depending on spawnable flag (not implemented yet)
     * Wizard: Dematerializes pieces that put it in check (not implemented yet)
     */
    public String ability { get; set; }


    /* State
     * Different states effect how the piece interacts with the pieces around it
     * States checked when calculating moves
     * Some pieces may start in a state, and some pieces may not be able to leave a state
     * 
     * Normal: Piece functions as normal
     * Shield: Cannot be captured
     * Dematerialized: Cannot be captured until manually rematerialized
     * Frozen: Cannot move until unfrozen
     * Ghost: Can go through your own pieces
     * Ghoul: Your pieces can go through this piece
     * Feminist: Cannot capture female opponent pieces
     * Oppressive: Cannot capture male opponent pieces
     * Combustable: May explode after every turn (1/6)
     * Fragile: May die after each move (1/6)
     * Jailed: Piece temporarily cant move
     * Uncastle: Can't castle (not implemented yet)
     * Rulebreaker: Can castle in check or if rook has moved (as long as it's in start)
     * Electric: Has a change of killing pieces that capture it (1/2)
     * Crook: Can't be captured, only jailed
     * Wall: Can't be jumped over (not implemented yet)
     * Medusa: Converts pieces it captures into shield pawns or jail states
     * Hungry: Adds pieces to storage, has the option of spitting out pieces
     * Piggyback: Carries pieces on top of it with it
     * Jockey: Other pieces carry it around with them
     * Delayed: Move executed the turn after (if possible) (not implemented yet)
     * Depressed: Cannot move if in check
     * Heartbroken: Becomes depressed if your queen dies
     * Portal: Can teleport through the board
     * Bouncing: Can bounce off the walls
     * CaptureTheFlag: Cannot be captured on your own side of the board (back 2 ranks)
     * Defuser: Bombs do not explode near this piece
     * Switch: When you castle, king and rook switch instead of normal castle
     * PAWN: Start an extra square forward
     * Double: Starts as two pawns
     */
    public String state { get; set; }
    public String secondaryState { get; set; }

    /*
     * CollateralType
     * What happens to the pieces in range of collateral
     * Collateral is not considered check/checkmate
     * -1: None
     * 0: Kill (After Capture)
     * 1: Kill (After Captured)
     * 2: Push (not implemented yet)
     * 3: Magnet (not implemented yet)
     * 4: Prevent Explosion (not implemented yet)
     * 5: Pawn Shop (After Capture) (not implemented yet)
     * 6: Thief (Converts enemy pieces into your pieces) (not implemented yet)
     * 7: Lift (Basically Magnet but for the square you're on) (not implemented yet)
     */
    public int collateralType { get; set; }


    /*
     * Collateral
     * Range of collateral
     * Collateral is not considered check/checkmate
     */
    public int[,] collateral { get; set; }


    /*
     * Size (not implemented yet)
     * How big this piece is
     * [0]: Width
     * [1]: Height
     */
    public int[] size { get; set; }


    /*
     * Promotes Into
     * "": Can't promote
     * Anything else promotes into that piece
     */
    public String promotesInto { get; set; }


    /*
     * Promoting Row
     * Where pieces promote (row)
     */
    public int promotingRow { get; set; }


    /* 
     * CanMoveTwice (not implemented yet)
     * 0: Can't
     * 1: Has a possibility of being able to move twice in one turn
     */
    public int canMoveTwice { get; set; }


    /*
     * Storage Limit
     * The max number of pieces that can be stored
     * -1: Can't store
     * Pos. Num: Can store x pieces
     */
    public int storageLimit { get; set; }


    /*
     * Storage
     * Pieces the current piece is storing
     */
    public List<Piece> storage { get; set; }


    /* ---------------- MOVES / ATTACKS ---------------- */



    /*
     * Moves
     * If there are no pieces on the square, move
     */
    public int[,] moves { get; set; }


    /*
     * One Time Moves
     * If there are no pieces on the square, move
     * Can only do these moves once
     */
    public int[,] oneTimeMoves { get; set; }


    /*
     * One Time Moves And Attacks
     * If there are no pieces on the square, move
     * If the piece on the square is the opposing colour, move
     * Can only do these moves once
     */
    public int[,] oneTimeMoveAndAttacks { get; set; }


    /* 
     * Attacks
     * If the piece on the square is the opposing colour, move
     */
    public int[,] attacks { get; set; }


    /*
     * Jump Attacks
     * Ignore all pieces in between the piece and the square when calculating moves
     * Serves as moveAndAttacks
     */
    public int[,] jumpAttacks { get; set; }


    /*
     * Move And Attacks
     * If there are no pieces on the square, move
     * If the piece on the square is the opposing colour, move
     */
    public int[,] moveAndAttacks { get; set; }

    /*
     * Murderous Attacks
     * If there are no pieces on the square, move
     * If there is a piece on the square, move
     */
    public int[,] murderousAttacks { get; set; }


    /* 
     * Conditional Attacks
     * If the condition marked by the condition flag is met, move
     */
    public int[,] conditionalAttacks { get; set; }


    /*
     * Dependent Attacks
     * Attacks set by a method for each individual piece.
     * Method is called before moves need to be calculated for each piece
     * Condition is different for every piece
     */
    public int[,] dependentAttacks { get; set; }


    /*
     * Interactive Attacks
     * Attacks that impact the pieces around them
     * Method is called to determine what attacks can be done (within piece),
     * secondary method is called to determine how the various pieces move (in game)
     */
    public int[,] interactiveAttacks { get; set; }


    /*
     * Position Indepent Moves (not implemented yet)
     * Attacks that go to set squares (also attacks)
     */
    public int[,] positionIndependentMoves { get; set; }


    /*
     * Force Stay Turn Moves
     * Moves (not attacks) that happen after its been your turn for more than 1 round
     */
    public int[,] forceStayTurnMoves { get; set; }


    /*
     * Flag Moves
     * Can be one move set or another, depending on the flag "flagMove"
     */
    public int[,] flagMove1 { get; set; }
    public int[,] flagMove2 { get; set; }


    /*
     * Push Moves (not implemented yet)
     * Moves the furthest it can in direction noted by the array
     * These are NOT attacks
     */
    public int[,] pushMoves { get; set; }


    /*
     * En Passant Moves (not implemented yet)
     * Captures as an en passant
     */
    public int[,] enPassantMoves { get; set; }


    /* ------------------- DEPENDANTS ------------------- */



    /*
     * Has Moved
     * false: If the piece has not moved
     * true: If the piece has moved
     */
    public bool hasMoved { get; set; }

    /*
     * Condition
     * false: Condition not met
     * true: Condition met
     * Conditions different for all pieces
     */
    public bool condition { get; set; }

    /*
     * SetDependentMoves (not implemented yet)
     * Populates the dependent moves
     * Moves determined by the method each piece has
     */
    public int[,] dependentMovesSet();

    /*
     * SetInteractiveMoves (not implemented yet)
     * Populates the interactive moves
     * Moves can interact with pieces without landing on that square
     */
    public int[,] interactiveMovesSet();


    /*
     * StayTurn
     * Determines if it should stay your turn after a move is made
     */
    public bool stayTurn();


    /*
     * FlagMove (not implemented yet)
     * Determined which set of moves is used
     * 0: Not used
     * 1: Set 1
     * 2: Set 2
     */
    public int flag { get; set; }


    /*
     * Spawnable
     * What piece this piece can spawn
     * null: Nothing
     */
    public string spawnable { get; set; }
    public int numSpawns { get; set; }



    /* --------------------- METHODS --------------------- */

    /*
     * Getters/Setters not unique to pieces
     */

    public int[] getPosition()
    {
        return position;
    }

    public void setPosition(int[] pos)
    {
        this.position = pos;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public interface Bot
{
    /*
     * Color
     * 1: White Piece
     * -1: Black Piece
     */
    public int color { get; set; }

    /* Check
    * True = In Check
    * False = Not In Check
    */
    public bool check { get; set; }

    /*
    * Pieces
    * The pieces your bot has control over
    */
    public List<Piece> pieces { get; set; }

    /*
    * Board Grid
    * The state of the board
    * This will be modified 
    */
    public List<List<List<Piece>>> boardGrid { get; set; }

    /* This method is called every time it's your bots turn */
    public (Piece piece, int[] coords, string ability) move();

    /*
    *
    *
    * PRIVATE METHODS
    *
    *
    */

    /*
    * Get All Moves
    * Gets the coords of every square you can move to
    */
    private List<int[]> getAllMoves()
    {
        return HelperFunctions.addToCurrentMoveableCoordsTotal(color, false, false, null, true, true);
    }

    /*
    * Get All Moves Piece
    * Gets the coords of every square you can move to with a specific piece
    */
    private List<int[]> getAllMovesPiece(Piece piece)
    {
        return HelperFunctions.addMovesToCurrentMoveableCoords(piece);
    }

    /*
    * Get Possible Moves
    * Gets all the possible moves and the pieces that can do them
    */
    private Dictionary<Piece, List<int[]>> getPossibleMoves()
    {
        HelperFunctions.updateBotMoves();

        return gameData.botMoves;
    }

    /*
    * Get All Abilities
    * Gets all the abilities you can use and the pieces that can use them
    */
    private Dictionary<Piece, List<string>> getAllAbilities()
    {
        return HelperFunctions.getAllEligibleAbilities(color);
    }
}
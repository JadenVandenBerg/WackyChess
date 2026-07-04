//TODO

//Bug
//When selecting from side panel, cannot properly deselect without clicking a new square
// update piece check flags in simulated piece moves (costly)

//Untested Bugfixes

//Dependant Attacks Need Fixing
// OneTimeKnight
// MurderousKnight

//Feature
//Side panel only show selectable pieces
//opponent can unfreeze but it is still considered stalemate
//add draw by repetition
// Game Saves && Replayer

//Remove
//Dependent Attacks
//Interactive Attacks

//Refactor

//Optimize
//Use structs instead of int[] and int[,]

//Pieces IMG
/*

//Abilities Img
*/

/*
 * Projected Piece Points
 * 
 ***** Rooks
 *
 * SharedRook                   -1
 * SweepingRook                 1
 * AutoRook                     4
 * HistoricallyAccurateCastle   4
 * MusketRook                   4
 * CastlerRook                  5
 * JumpingRook                  6
 * SuperRook                    7
 * CrusherRook                  7
 * DuplicateRook                8
 * 
 ***** Queens
 *
 * SharedQueen                  -1
 * SweepingQueen                3
 * AutoQueen                    5
 * MusketQueen                  6
 * CastlerQueen                 9
 * PromotionQueen               10
 * SuperQueen                   11
 * 
 ***** Bishops
 *
 * SharedBishop                 -1
 * AutoBishop                   1
 * SweepingBishop               2
 * CastlerBishop                3
 * MusketBishop                 4
 * SuperBishop                  5
 * DuplicateBishop              6
 * 
 ***** Knights
 *
 * SharedKnight                 -1
 * NoJumpKnight                 1
 * AutoKnight                   2
 * CastlerKnight                3
 * MusketKnight                 4
 * DuplicateKnight              6
 * WorkingTitleKnight           6
 * HistoricallyAccurateKnight   6
 * CheckersKnight (jump kill)   6
 * 
 ***** Kings
 * FatKing                      -5
 * SlowKing                     -5
 * TerritorialKing              -3
 * JailKing                     -3
 * SweepingKing                 -3
 * HistoricallyAccurateKing     -1
 * AutoKing                     0
 * MusketKing                   1
 * CastlerKing                  1
 * KingPin                      2
 * ExtremeCastlerKing           2
 * ExtraLifeKing                4
 * ForceFieldKing               4
 * TakeHimKing                  5
 * Wizard                       8
 */
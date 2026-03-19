//TODO

//Bug
//When selecting from side panel, cannot properly deselect without clicking a new square
//When a stacking piece captures a delayed piece, it becomes delayed too early, jailer too
//bottus maximus is counting moves as attacks when calculating trades
//ability still not calculated correctly for checkmate
//stalemate counted when a bishop was on the board
//ghoul king can stack on lite pawn
//delayed king moves too early

//Dependant Attacks Need Fixing
// OneTimeKnight
// MurderousKnight

//Feature
//Side panel only show selectable pieces

//Remove
//Dependent Attacks
//Interactive Attacks

//Refactor

//Optimize
//Use structs instead of int[]

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
 * FusionRook                   8
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
 * FusionQueen                  14
 * 
 ***** Bishops
 *
 * SharedBishop                 -1
 * AutoBishop                   1
 * SweepingBishop               2
 * CastlerBishop                3
 * MusketBishop                 4
 * SuperBishop                  5
 * FusionBishop                 6
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
 * FusionKnight                 7
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
 * FusionKing                   7
 * Wizard                       8
 */
//TODO

//Bug
//When selecting from side panel, cannot properly deselect without clicking a new square
//Need to rotate black pieces
//There is a problem with piecesOnSquareBoardGrid after collateral
//Take account for abilities when checking for check and checkmate
//When spawning new pieces, add them to BotPieces
//Portal and Bouncing does not account for Ghoul/Ghost/Dematerialized states
//When a stacking piece captures a delayed piece, it becomes delayed too early
//BUG refreshing boardState not working properly

//Feature
//Side panel only show selectable pieces
//Add abilities to RandomMove functions

//Remove
//Dependent Attacks
//Interactive Attacks

//Refactor

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
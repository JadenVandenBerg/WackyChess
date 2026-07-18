using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using System;
public class SpeedRunnerBot : BotTemplate
{
    bool hasMainPiece; 
    bool hasSoloPiece;   
    Piece mainPiece;
    Piece secondPiece;
    int turn = 0;      
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public SpeedRunnerBot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Speedrunner Bot";

        //This function populates the pieces variable
        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        //Initialize for later
        double closestDistanceToKing = 1000;
        double closestYToKing = 1000;
        float furthestDistance = 0;
        List<NextMove> validMoves = new List<NextMove>();

        //Get all the possible moves. Note that some of these moves may result in check, which will not be allowed
        //Other than that these moves are all legal.
        //NextMove is a class with 3 vars inside, NextMove.moveType == "move" | "ability"
        //if move, NextMove.move will be populated
        //Move contains Move.p, Move.coords
        //if ability, NextMove.ability will be populated
        //public Piece piece; //The piece with the ability
        //public string ability; //Ability name
        //public int[] coords; //Coords for abilities with one action (ie. Spawning, Freezing)
        //public List<Piece> placePieces; //Pieces for abilities with multiple actions. Only hungry for now
        //public List<int[]> placeCoords; //Coords for abilities with multiple actions. Only hungry for now
        //public Piece secondPiece; //The second piece used in abilities. Used for castling/spawning
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);
        //List<Piece> oppPieces = new List<Piece>();
        //List<Piece> oppPieces = getPieces(this, this.currentBoardState, this.color); Not acctual code I made this up
        turn += 1;
        int bestPiece = 0;
        int secondBestPiece = 0;
        bool hasSoloPiece = false;
        //Get enemy king's position
        coords oppKingPos = filterPieces("King", this.opponentPieces)[0].position;
        //Loop through all pieces and get main piece
        foreach (Piece piece in pieces)
        {
            int currentPieceValue = 0;
            if (piece.baseType != "King" & ! HelperFunctions.checkState(piece, PieceState.Jailed))
            {
                if (piece.hasMoved == true & piece.oneTimeMoveAndAttacks != null)
                {
                    currentPieceValue -= 10;
                }
                if (piece.position.x == oppKingPos.x & piece.position.y == oppKingPos.y){}
                else
                {
                hasMainPiece = true;
                if (piece.lives == -1)
                {                
                    currentPieceValue += 8;
                }
                else if (HelperFunctions.checkState(piece, PieceState.Combustable))
                {
                    currentPieceValue += 15;
                    hasSoloPiece = true;   
                }
                else if (HelperFunctions.checkState(piece, PieceState.Medusa))
                {
                    currentPieceValue += 16;
                    hasSoloPiece = true;      
                }
                else if (HelperFunctions.checkState(piece, PieceState.Delayed))
                {
                    currentPieceValue -= 8;   
                }
                else if (piece.collateralType == 0)
                {
                    currentPieceValue += 10;
                    hasSoloPiece = true; 

                } else if (piece.collateralType == 1)
                {
                    currentPieceValue += 9;   
                    hasSoloPiece = true;   
                } else if (piece.collateralType == 2)
                {
                    currentPieceValue += 7;   
                }
                if (piece.baseType == "Misc")
                {
                    currentPieceValue += 5;
                }
                if (piece.baseType == "Queen")
                {
                    currentPieceValue += 4;   
                } 
                else if (piece.baseType == "Bishop")
                {
                    currentPieceValue += 3;   
                } 
                else if (piece.baseType == "Rook")
                {
                    currentPieceValue += 2;   
                }
                else if (piece.baseType == "Knight")
                {
                    currentPieceValue += 1;   
  
                }
                else if (piece.baseType == "Pawn")
                    {
                        currentPieceValue -= 6;
                    }

                if (currentPieceValue > bestPiece)
                {
                    secondBestPiece = bestPiece;
                    bestPiece = currentPieceValue;
                    secondPiece = mainPiece;
                    mainPiece = piece;
                }
                }
            }
        }
        if (bestPiece < 1)
            {
                hasMainPiece = false;
            }

        foreach (NextMove nextMove in allMoves)
        {
            //Find out what the moveType is and set vars accordingly
            Piece piece;
            coords coords;
            string moveType = nextMove.moveType;

                if (moveType == "move")
                {
                    Move mv = nextMove.move;
                    piece = mv.p;
                    coords = mv.coords;
                    //Checks if the main piece can solo
                    if (hasMainPiece == true)
                    {
                    if (hasSoloPiece == true)
                    {
                        if (piece.name == mainPiece.name)
                        {
                            //Get the closest possible to the king
                            double pieceDistanceToKing = Math.Sqrt(Math.Pow(coords.y - oppKingPos.y, 2) + Math.Pow(coords.x - oppKingPos.x, 2)); 
                            double curretPieceDistanceToKing = Math.Sqrt(Math.Pow(piece.position.y - oppKingPos.y, 2) + Math.Pow(piece.position.x - oppKingPos.x, 2));
                            
                            if (pieceDistanceToKing < 6){// | (piece.position.y == piece.startSquare.y & piece.position.x == piece.startSquare.x)) {
                                if (pieceDistanceToKing < closestDistanceToKing) {
                                    validMoves.Clear();
                                    validMoves.Add(nextMove);
                                    closestDistanceToKing = pieceDistanceToKing;
                                }
                            }
                            else
                            {
                                if (pieceDistanceToKing > curretPieceDistanceToKing)
                                {
                                    if (pieceDistanceToKing < closestDistanceToKing) {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        closestDistanceToKing = pieceDistanceToKing;
                                    }
                                }
                        }
                        }                 
                        else
                        {
                            //If the main piece cannot move, move the pieces blocking it
                            if (validMoves.Count == 0)
                            {
                                if (mainPiece.baseType == "Rook")
                                {
                                    if (piece.position.x == mainPiece.position.x)
                                    {
                                        if (piece.color == -1) {
                                            if (coords.y - 8 > furthestDistance) {
                                                validMoves.Clear();
                                                validMoves.Add(nextMove);
                                                furthestDistance = coords.y - 8;
                                            }
                                            else if (coords.y == furthestDistance) {
                                                validMoves.Add(nextMove);
                                            }
                                        } else if (piece.color == 1) {
                                            if (coords.y > furthestDistance) {
                                                validMoves.Clear();
                                                validMoves.Add(nextMove);
                                                furthestDistance = coords.y;
                                            } 
                                            else if (coords.y == furthestDistance) {
                                                validMoves.Add(nextMove);
                                            }
                                        }              
                                    }
                                }
                                else if (mainPiece.baseType == "Bishop" | mainPiece.baseType == "Queen")
                                {
                                    if (piece.color == 1 & piece.position.y - 1 == mainPiece.position.y) 
                                    {
                                        if (piece.position.x + 1 == mainPiece.position.x | piece.position.x - 1 == mainPiece.position.x)
                                        {
                                            if (piece.color == -1) {
                                                if (coords.y - 8 > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y - 8;
                                                }
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            } else if (piece.color == 1) {
                                                if (coords.y > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y;
                                                } 
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            }              
                                        }
                                    }
                                    else if (piece.color == -1 & piece.position.y + 1 == mainPiece.position.y)
                                    {
                                        if (piece.position.x + 1 == mainPiece.position.x | piece.position.x - 1 == mainPiece.position.x)
                                        {
                                            if (piece.color == -1) {
                                                if (coords.y - 8 > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y - 8;
                                                }
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            } else if (piece.color == 1) {
                                                if (coords.y > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y;
                                                } 
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            }              
                                        }
                                    }
                                }
                            }
                        }   
                    }
                    else
                    {
                        //If the main piece cannot solo
                        double curretMainPieceDistanceToKing = Math.Sqrt(Math.Pow(mainPiece.position.y - oppKingPos.y, 2) + Math.Pow(mainPiece.position.x - oppKingPos.x, 2));
                        if ((mainPiece.color == 1 & (mainPiece.position.y > 3 & secondPiece.position.y <= 3)) | (mainPiece.color == -1 & mainPiece.position.y < 6 & secondPiece.position.y >= 6)  | curretMainPieceDistanceToKing < 2.5)
                        {
                        if (piece.name == secondPiece.name)
                        {
                            double pieceDistanceToKing = Math.Sqrt(Math.Pow(coords.y - oppKingPos.y, 2) + Math.Pow(coords.x - oppKingPos.x, 2)); 
                            double curretPieceDistanceToKing = Math.Sqrt(Math.Pow(piece.position.y - oppKingPos.y, 2) + Math.Pow(piece.position.x - oppKingPos.x, 2));
                            double yDistanceToKing = Math.Sqrt(Math.Pow(coords.y - oppKingPos.y, 2));
                            double currentYDistanceToKing = Math.Sqrt(Math.Pow(piece.position.y - oppKingPos.y, 2));

                            if (piece.baseType == "Rook")
                            {
                                if (yDistanceToKing < 6){// | (piece.position.y == piece.startSquare.y & piece.position.x == piece.startSquare.x)) {
                                    if (yDistanceToKing < closestYToKing) {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        closestYToKing = yDistanceToKing;
                                    }
                                }
                                else
                                {
                                    if (yDistanceToKing > currentYDistanceToKing)
                                    {
                                        if (yDistanceToKing < closestYToKing) {
                                            validMoves.Clear();
                                            validMoves.Add(nextMove);
                                            closestYToKing = yDistanceToKing;
                                        }
                                    }
                                }                                    
                            }
                            else
                            {
                                if (pieceDistanceToKing < 6){// | (piece.position.y == piece.startSquare.y & piece.position.x == piece.startSquare.x)) {
                                    if (pieceDistanceToKing < closestDistanceToKing) {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        closestDistanceToKing = pieceDistanceToKing;
                                    }
                                }
                                else
                                {
                                    if (pieceDistanceToKing > curretPieceDistanceToKing)
                                    {
                                        if (pieceDistanceToKing < closestDistanceToKing) {
                                            validMoves.Clear();
                                            validMoves.Add(nextMove);
                                            closestDistanceToKing = pieceDistanceToKing;
                                        }
                                    }
                                }
                            }
                        }                 
                        else
                        {
                            if (validMoves.Count == 0)
                            {
                                if (secondPiece.baseType == "Rook")
                                {
                                    if (piece.position.x == secondPiece.position.x)
                                    {
                                        if (piece.color == -1) {
                                            if (coords.y - 8 > furthestDistance) {
                                                validMoves.Clear();
                                                validMoves.Add(nextMove);
                                                furthestDistance = coords.y - 8;
                                            }
                                            else if (coords.y == furthestDistance) {
                                                validMoves.Add(nextMove);
                                            }
                                        } else if (piece.color == 1) {
                                            if (coords.y > furthestDistance) {
                                                validMoves.Clear();
                                                validMoves.Add(nextMove);
                                                furthestDistance = coords.y;
                                            } 
                                            else if (coords.y == furthestDistance) {
                                                validMoves.Add(nextMove);
                                            }
                                        }              
                                    }
                                }
                                else if (secondPiece.baseType == "Bishop" | secondPiece.baseType == "Queen")
                                {
                                    if (piece.color == 1 & piece.position.y - 1 == secondPiece.position.y) 
                                    {
                                        if (piece.position.x + 1 == secondPiece.position.x | piece.position.x - 1 == secondPiece.position.x)
                                        {
                                            if (piece.color == -1) {
                                                if (coords.y - 8 > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y - 8;
                                                }
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            } else if (piece.color == 1) {
                                                if (coords.y > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y;
                                                } 
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            }              
                                        }
                                    }
                                    else if (piece.color == -1 & piece.position.y + 1 == secondPiece.position.y)
                                    {
                                        if (piece.position.x + 1 == secondPiece.position.x | piece.position.x - 1 == secondPiece.position.x)
                                        {
                                            if (piece.color == -1) {
                                                if (coords.y - 8 > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y - 8;
                                                }
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            } else if (piece.color == 1) {
                                                if (coords.y > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y;
                                                } 
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            }              
                                        }
                                    }
                                }
                            }
                        }      
                    } else
                        {
                            if (piece.name == mainPiece.name)
                        {
                            //Get the closest possible to the king
                            double pieceDistanceToKing = Math.Sqrt(Math.Pow(coords.y - oppKingPos.y, 2) + Math.Pow(coords.x - oppKingPos.x, 2)); 
                            double curretPieceDistanceToKing = Math.Sqrt(Math.Pow(piece.position.y - oppKingPos.y, 2) + Math.Pow(piece.position.x - oppKingPos.x, 2));
                            double yDistanceToKing = Math.Sqrt(Math.Pow(coords.y - oppKingPos.y, 2));
                            double currentYDistanceToKing = Math.Sqrt(Math.Pow(piece.position.y - oppKingPos.y, 2));

                            if (piece.baseType == "Rook")
                            {
                                if (yDistanceToKing < 6){// | (piece.position.y == piece.startSquare.y & piece.position.x == piece.startSquare.x)) {
                                    if (yDistanceToKing < closestYToKing) {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        closestYToKing = yDistanceToKing;
                                    }
                                }
                                else
                                {
                                    if (yDistanceToKing > currentYDistanceToKing)
                                    {
                                        if (yDistanceToKing < closestYToKing) {
                                            validMoves.Clear();
                                            validMoves.Add(nextMove);
                                            closestYToKing = yDistanceToKing;
                                        }
                                    }
                                }                                    
                            }
                            else
                            {
                                if (pieceDistanceToKing < 6){// | (piece.position.y == piece.startSquare.y & piece.position.x == piece.startSquare.x)) {
                                    if (pieceDistanceToKing < closestDistanceToKing) {
                                        validMoves.Clear();
                                        validMoves.Add(nextMove);
                                        closestDistanceToKing = pieceDistanceToKing;
                                    }
                                }
                                else
                                {
                                    if (pieceDistanceToKing > curretPieceDistanceToKing)
                                    {
                                        if (pieceDistanceToKing < closestDistanceToKing) {
                                            validMoves.Clear();
                                            validMoves.Add(nextMove);
                                            closestDistanceToKing = pieceDistanceToKing;
                                        }
                                    }
                                }
                            }
                        }                 
                        else
                        {
                            //If the main piece cannot move, move the pieces blocking it
                            if (validMoves.Count == 0)
                            {
                                if (mainPiece.baseType == "Rook")
                                {
                                    if (piece.position.x == mainPiece.position.x)
                                    {
                                        if (piece.color == -1) {
                                            if (coords.y - 8 > furthestDistance) {
                                                validMoves.Clear();
                                                validMoves.Add(nextMove);
                                                furthestDistance = coords.y - 8;
                                            }
                                            else if (coords.y == furthestDistance) {
                                                validMoves.Add(nextMove);
                                            }
                                        } else if (piece.color == 1) {
                                            if (coords.y > furthestDistance) {
                                                validMoves.Clear();
                                                validMoves.Add(nextMove);
                                                furthestDistance = coords.y;
                                            } 
                                            else if (coords.y == furthestDistance) {
                                                validMoves.Add(nextMove);
                                            }
                                        }              
                                    }
                                }
                                else if (mainPiece.baseType == "Bishop" | mainPiece.baseType == "Queen")
                                {
                                    if (piece.color == 1 & piece.position.y - 1 == mainPiece.position.y) 
                                    {
                                        if (piece.position.x + 1 == mainPiece.position.x | piece.position.x - 1 == mainPiece.position.x)
                                        {
                                            if (piece.color == -1) {
                                                if (coords.y - 8 > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y - 8;
                                                }
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            } else if (piece.color == 1) {
                                                if (coords.y > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y;
                                                } 
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            }              
                                        }
                                    }
                                    else if (piece.color == -1 & piece.position.y + 1 == mainPiece.position.y)
                                    {
                                        if (piece.position.x + 1 == mainPiece.position.x | piece.position.x - 1 == mainPiece.position.x)
                                        {
                                            if (piece.color == -1) {
                                                if (coords.y - 8 > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y - 8;
                                                }
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            } else if (piece.color == 1) {
                                                if (coords.y > furthestDistance) {
                                                    validMoves.Clear();
                                                    validMoves.Add(nextMove);
                                                    furthestDistance = coords.y;
                                                } 
                                                else if (coords.y == furthestDistance) {
                                                    validMoves.Add(nextMove);
                                                }
                                            }              
                                        }
                                    }
                                }
                            }
                        }
                        }
                    }
                    }    
                    else
                    {
                            if (piece.color == -1) {
                                if (coords.y - 8 < closestDistanceToKing) {
                                    validMoves.Clear();
                                    validMoves.Add(nextMove);
                                    closestDistanceToKing = coords.y - 8;
                                }
                                else if (coords.y - 8 == closestDistanceToKing) {
                                    validMoves.Add(nextMove);
                                }
                            } else if (piece.color == 1) {
                                if (coords.y > closestDistanceToKing) {
                                    validMoves.Clear();
                                    validMoves.Add(nextMove);
                                    closestDistanceToKing = coords.y;
                                } 
                                else if (coords.y == closestDistanceToKing) {
                                    validMoves.Add(nextMove);
                                }
                            }                 
                    }    
                } 
                else
                {
                    PieceAbility pa = nextMove.ability;
                    piece = pa.piece;
                    coords = pa.coords;

                    //The main peice can use materialize, unfreeze and spawn
                    if (piece.name == mainPiece.name)
                    {
                        if (pa.ability == PieceAbilities.Spawn | pa.ability == PieceAbilities.Dematerialize | HelperFunctions.checkState(piece, PieceState.Frozen))
                        {
                            validMoves.Add(nextMove);
                        }
                    }
                }
        }
        //Just in case validMoves is empty and its gunna crash do default code instead
        if (validMoves.Count == 0)
        {
            float bestMoveDiff = -1000;
           foreach (NextMove nextMove in allMoves)
        {
            //Find out what the moveType is and set vars accordingly
            Piece piece;
            coords coords;
            string moveType = nextMove.moveType;

            if (moveType == "move")
            {
                Move mv = nextMove.move;

                piece = mv.p;
                coords = mv.coords;
            }
            else // moveType == "ability" guarenteed
            {
                PieceAbility pa = nextMove.ability;

                piece = pa.piece;
                coords = pa.coords;
            }

            //this.currentBoardState at the start of nextMove is a BoardState containing info of all the pieces. Save this. After we loop through all opponent moves, we set
            //this.currentBoardState = originalBoardstate
            BoardState originalBoardState = this.currentBoardState;

            //Simulate the piece move
            BoardState cloneState;
            if (moveType == "move")
            {
                cloneState = simulatePieceMove(this, this.currentBoardState, piece, coords);
            }
            else
            {
                cloneState = simulatePieceAbility(this, this.currentBoardState, nextMove.ability);
            }
            this.currentBoardState = cloneState;

            //Now that we have simulated our move, we do the same with opponent moves
            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

            NextMove bestOppNextMove;
            float bestOppMoveDiff = +1000;

            //Loop through all opponent moves
            foreach (NextMove nextMoveOpp in allMovesOpp)
            {
                Piece pieceOpp;
                coords coordsOpp;

                string moveTypeOpp = nextMoveOpp.moveType;

                if (moveTypeOpp == "move")
                {
                    Move mv = nextMoveOpp.move;

                    pieceOpp = mv.p;
                    coordsOpp = mv.coords;
                }
                else // moveType == "ability" guarenteed
                {
                    PieceAbility pa = nextMoveOpp.ability;

                    pieceOpp = pa.piece;
                    coordsOpp = pa.coords;
                }

                //Save the boardstate again, then simulate opponent move
                //After move is simulated, revert back to the boardstate after our move
                BoardState originalBoardState_ = this.currentBoardState;
                BoardState cloneState_;
                if (moveTypeOpp == "move")
                {
                    cloneState_ = simulatePieceMove(this, this.currentBoardState, pieceOpp, coordsOpp);
                }
                else
                {
                    cloneState_ = simulatePieceAbility(this, this.currentBoardState, nextMoveOpp.ability);
                }
                this.currentBoardState = originalBoardState_;

                //Using the boardstate after opponents boardstate, get the points on board
                // this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1] means if bot is white, use [0] else [1]
                List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                //Debug.Log("Testing a move by " + piece.name + " and opp " + pieceOpp.name + " results in " + (botPoints - 100) + " : " + (oppPoints - 100));
                //if (this.color == 1) Debug.LogWarning("Points on board after " + moveType + "," + moveTypeOpp + " " + pieceOpp.name + " moved to " + (coordsOpp[0]) + "," + (coordsOpp[1]) + " - White: " + (botPoints - 100) + ". Black: " + (oppPoints - 100));

                //debug_printBoardState(cloneState_);

                //Compare the difference of points. If the diff is a new best (in the sense of black made a good move), mark it as best
                //In this algorithm, this is considered to be the best move the opponent can make
                float diff = botPoints - oppPoints;
                if (diff < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff;
                    bestOppNextMove = nextMoveOpp;
                }
            }

            //Now back to the outer loop, if the move we checked, assuming the opponent makes the best move, is better than the current best, save it
            //If it is tied also save it
            if (bestOppMoveDiff >= bestMoveDiff)
            {
                if (bestOppMoveDiff > bestMoveDiff)
                {
                    //If it is better, clear all saved moves
                    validMoves.Clear();
                }

                bestMoveDiff = bestOppMoveDiff;

                //If it is a tie or better, save the move
                validMoves.Add(nextMove);
            }


            //Reset the currentBoardState and go to the next move
            this.currentBoardState = originalBoardState;
        }
        }
        //Pick a random move from our list of tied moves
        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

        //Get the original piece, you can just copy paste this part (ill probably add this to botMaster.cs later
        if (move.moveType == "move")
        {
            move.move.p = getOriginalPieceFromClone(move.move.p);
        }
        else
        {
            move.ability.piece = getOriginalPieceFromClone(move.ability.piece);
        }
        return move;
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static HelperFunctions;

public class HitmanBot : BotTemplate
{
    //The constructor, this function gets called when a new OneMoveBot is initialized
    //Ie. BotTemplate botWhite = new OneMoveBot(1);
    //1 is white, -1 is black
    public HitmanBot(int botColor)
    {
        //Initialize variables, do not change anything here but name
        color = botColor;
        pieces = new List<Piece>();
        name = "Hitman Bot";

        //This function populates the pieces variable
        choosePieces();
    }

    Piece target = null;

    override
    public NextMove nextMove()
    {
        //Initialize for later
        float bestMoveDiff = -1000;
        List<NextMove> validMoves = new List<NextMove>();
        List<NextMove> allMoves = getAllPossibleBotMovesAndAbilities(this, this.currentBoardState, this.color);

        target = refreshTarget();

        highlightSquare(findSquare(target.position.x, target.position.y), Color.orange);

        //Loop through all moves
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

            List<NextMove> allMovesOpp = getAllPossibleBotMovesAndAbilities(this, cloneState, this.color * -1);

            NextMove bestOppNextMove;
            float bestOppMoveDiff = +1000;
            BoardState bestOppMoveBS = null;

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

                List<float> pointsOnBoard = getPointsOnBoardState(cloneState_, true);
                float botPoints = this.color == 1 ? pointsOnBoard[0] : pointsOnBoard[1];
                float oppPoints = this.color == -1 ? pointsOnBoard[0] : pointsOnBoard[1];

                float diff = botPoints - oppPoints;
                if (diff < bestOppMoveDiff)
                {
                    bestOppMoveDiff = diff;
                    bestOppNextMove = nextMoveOpp;
                    bestOppMoveBS = cloneState_;
                }
            }

            float addOns = 0f;

            coords distToTarget = new coords(coords.x - target.position.x, coords.y - target.position.y);
            distToTarget = new coords(distToTarget.x * distToTarget.x, distToTarget.y * distToTarget.y);

            float distanceToTarget = Mathf.Sqrt(distToTarget.x + distToTarget.y);

            addOns += 0.3f * (11.5f - distanceToTarget);

            // Target is DEAD
            if (targetIsDead(bestOppMoveBS))
            {
                addOns += 100f;
            }

            if (canAttackTarget(cloneState))
            {
                addOns += 2f;
            }

            bestOppMoveDiff += addOns;

            if (bestOppMoveDiff >= bestMoveDiff)
            {
                if (bestOppMoveDiff > bestMoveDiff)
                {
                    validMoves.Clear();
                }

                bestMoveDiff = bestOppMoveDiff;

                validMoves.Add(nextMove);
            }

            this.currentBoardState = originalBoardState;

        }

        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];

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

    public Piece refreshTarget()
    {
        List<Piece> pieces = getPiecesOnBoardState(this.currentBoardState, this.color * -1);

        if (target == null)
        {
            int indx = globalDefs.globalRand.Next(pieces.Count);

            return pieces[indx];
        }
        else
        {
            foreach(Piece p in pieces)
            {
                if (p.name == target.name)
                {
                    return p;
                }
            }
        }

        int idx = globalDefs.globalRand.Next(pieces.Count);
        return pieces[idx];
    }

    public bool targetIsDead(BoardState bs)
    {
        List<Piece> pieces = getPiecesOnBoardState(bs, this.color * -1);

        foreach (Piece p in pieces)
        {
            if (p.name == target.name)
            {
                return false;
            }
        }

        return true;
    }

    public bool canAttackTarget(BoardState bs)
    {
        List<NextMove> nextMoves = getAllPossibleBotAttacksAndAbilities(this, bs, this.color);

        foreach(NextMove nm in nextMoves)
        {
            if (nm.moveType == "move")
            {
                if (nm.move.coords.x == target.position.x && nm.move.coords.y == target.position.y)
                {
                    return true;
                } 
            }
        }

        return false;
    }
}
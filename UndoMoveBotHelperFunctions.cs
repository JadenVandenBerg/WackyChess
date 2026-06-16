using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;
using static BotHelperFunctions;
using static HelperFunctions;

//TODO send a clone of the boardState to bots not the original

/* Workflow

* Get all bot moves
* Loop through
* 	Simulate Move
*   If want to save, clone, else just evaluate
*   Undo move on initial BoardState
* Return best
*/

[Flags]
public enum PieceAction : long
{
    None = 0,

    IncrementSpawn = 1L << 0,
    SetAlive = 1L << 1,
    IncrementLives = 1L << 2,
    IncrementTurnsTillDelay = 1L << 3,
    AddToQueueFront = 1L << 4,
    RemoveFromQueue = 1L << 5,
    ResetKing = 1L << 6,
}

public struct coords
{
    public int x;
    public int y;

    public coords(int x_, int y_)
    {
        x = x_;
        y = y_;
    }
}

public struct UndoStorage
{
    public Piece piece;
    public Piece storedPiece;
    public bool undo; //true == add to board, false == add to storage (during undo)
    public coords coords;

    public UndoStorage(Piece piece_, Piece storedPiece_, bool undo_, coords coords_)
    {
        piece = piece_;
        storedPiece = storedPiece_;
        undo = undo_;
        coords = coords_;
    }
}

public struct UndoDelayedQueueAction
{
    public PieceMove pieceMove;
    public PieceAction delayedAction;

    public UndoDelayedQueueAction(PieceMove pm, PieceAction pa)
    {
        pieceMove = pm;
        delayedAction = pa;
    }
}

public struct UndoState
{
    public Piece piece;
    public PieceState state;

    public UndoState(Piece piece_, PieceState pieceState_)
    {
        piece = piece_;
        state = pieceState_;
    }
}

public struct UndoAbility
{
    public Piece piece;
    public PieceAbilities ability;

    public UndoAbility(Piece piece_, PieceAbilities pieceAbility_)
    {
        piece = piece_;
        ability = pieceAbility_;
    }
}

public struct UndoPieceAction
{
    public Piece piece;
    public PieceAction action;

    public UndoPieceAction(Piece piece_, PieceAction action_)
    {
        piece = piece_;
        action = action_;
    }
}

public struct UndoBotAction
{
    public BotTemplate bot;
    public PieceAction action;

    public UndoBotAction(BotTemplate bot_, PieceAction action_)
    {
        bot = bot_;
        action = action_;
    }
}

public class UndoMovedPiece
{
    public Piece p;
    public coords initialPosition;
    public coords newPosition;
    public bool dead;
    public bool spawned;
    public bool revertHasMoved;

    public UndoMovedPiece(Piece p_, coords initialPosition_, coords newPosition_, bool dead_, bool spawned_, bool revertHasMoved_)
    {
        p = p_;
        initialPosition = initialPosition_;
        newPosition = newPosition_;
        dead = dead_;
        spawned = spawned_;
        revertHasMoved = revertHasMoved_;
    }

    public UndoMovedPiece(Piece p_)
    {
        p = p_;
        initialPosition = new coords(-1,-1);
        newPosition = new coords(-1, -1);
        dead = false;
        spawned = false;
        revertHasMoved = false;
    }

    public UndoMovedPiece()
    {
        p = null;
        initialPosition = new coords(-1, -1);
        newPosition = new coords(-1, -1);
        dead = false;
        spawned = false;
        revertHasMoved = false;
    }
}

public struct UndoEntry
{
    public string undoMoveType;

    public UndoMovedPiece movedPiece;
    public UndoStorage storage;
    public UndoAbility ability;
    public UndoState state;
    public UndoPieceAction action;
    public UndoBotAction botAction;
    public UndoDelayedQueueAction delayedQueueAction;

    public UndoEntry(string type)
    {
        undoMoveType = type;

        movedPiece = default;
        storage = default;
        ability = default;
        state = default;
        action = default;
        botAction = default;
        delayedQueueAction = default;
    }
}

public class UndoMove
{
    public List<UndoEntry> entries;

    public UndoMove()
    {
        entries = new List<UndoEntry>();
    }

    public void addMove(UndoMovedPiece undoMovedPiece)
    {
        UndoEntry entry = new UndoEntry("move");
        entry.movedPiece = undoMovedPiece;
        entries.Add(entry);
    }

    public void addStorage(UndoStorage undoStorage)
    {
        UndoEntry entry = new UndoEntry("storage");
        entry.storage = undoStorage;
        entries.Add(entry);
    }

    public void addState(UndoState undoState)
    {
        UndoEntry entry = new UndoEntry("state");
        entry.state = undoState;
        entries.Add(entry);
    }

    public void addAbility(UndoAbility undoAbility)
    {
        UndoEntry entry = new UndoEntry("ability");
        entry.ability = undoAbility;
        entries.Add(entry);
    }

    public void addAction(UndoPieceAction undoAction)
    {
        UndoEntry entry = new UndoEntry("action");
        entry.action = undoAction;
        entries.Add(entry);
    }

    public void addBotAction(UndoBotAction undoAction)
    {
        UndoEntry entry = new UndoEntry("botAction");
        entry.botAction = undoAction;
        entries.Add(entry);
    }

    public void addDelayedQueueAction(UndoDelayedQueueAction undoDelayedQueueAction_)
    {
        UndoEntry entry = new UndoEntry("delayedQueue");
        entry.delayedQueueAction = undoDelayedQueueAction_;
        entries.Add(entry);
    }
}

public class UndoMoveBotHelperFunctions : MonoBehaviour
{
    public static void undoMove(UndoMove undo, BoardState bs)
    {
        for (int i = undo.entries.Count - 1; i >= 0; i--)
        {
            UndoEntry entry = undo.entries[i];

            if (entry.undoMoveType == "move")
            {
                UndoMovedPiece ump = entry.movedPiece;

                if (ump == null)
                {
                    //Debug.Break();
                    continue;
                }

                Piece p = ump.p;
                coords initialPosition = ump.initialPosition;
                coords newPosition = ump.newPosition;
                bool dead = ump.dead;
                bool spawned = ump.spawned;
                bool revertHasMoved = ump.revertHasMoved;

                if (spawned)
                {
                    updateBoardState(new coords( newPosition.x - 1, newPosition.y - 1 ),p, "r", bs);
                }
                else if (dead)
                {
                    updateBoardState(new coords( initialPosition.x - 1, initialPosition.y - 1 ),p, "a", bs);
                }
                else
                {
                    updateBoardState(new coords( newPosition.x - 1, newPosition.y - 1 ),p, "r", bs);

                    updateBoardState(new coords( initialPosition.x - 1, initialPosition.y - 1 ),p, "a", bs);
                }

                if (revertHasMoved)
                {
                    p.hasMoved = false;
                }
            }

            if (entry.undoMoveType == "storage")
            {
                UndoStorage us = entry.storage;

                Piece piece = us.piece;
                Piece storedPiece = us.storedPiece;
                bool undo_ = us.undo;
                coords coords = us.coords;

                if (undo_)
                {
                    piece.storage.RemoveAll(p => p.name == storedPiece.name);

                    updateBoardState(new coords( coords.x - 1, coords.y - 1 ), storedPiece, "a", bs);
                }
                else
                {
                    piece.storage.Add(storedPiece);

                    updateBoardState(new coords( coords.x - 1, coords.y - 1 ), storedPiece, "r", bs);
                }
            }

            if (entry.undoMoveType == "action")
            {
                UndoPieceAction a = entry.action;

                Piece piece = a.piece;
                PieceAction action = a.action;

                if ((action & PieceAction.IncrementSpawn) != 0)
                {
                    piece.numSpawns++;
                }

                if ((action & PieceAction.SetAlive) != 0)
                {
                    piece.alive = 1;
                }

                if ((action & PieceAction.IncrementLives) != 0)
                {
                    piece.lives++;
                }
            }

            if (entry.undoMoveType == "state")
            {
                UndoState s = entry.state;

                Piece piece = s.piece;
                PieceState state = s.state;

                piece.states = state;
            }

            if (entry.undoMoveType == "ability")
            {
                UndoAbility a = entry.ability;

                Piece piece = a.piece;
                PieceAbilities ability = a.ability;

                piece.abilities = ability;
            }

            if (entry.undoMoveType == "delayedQueue")
            {
                UndoDelayedQueueAction a = entry.delayedQueueAction;

                PieceMove pieceMove = a.pieceMove;
                PieceAction delayedAction = a.delayedAction;

                if ((delayedAction & PieceAction.AddToQueueFront) != 0)
                {
                    bs.delayedQueue._items = bs.delayedQueue._items.Prepend(pieceMove).ToList();
                }

                if ((delayedAction & PieceAction.RemoveFromQueue) != 0)
                {
                    bs.delayedQueue._items.Remove(pieceMove);
                }

                if ((delayedAction & PieceAction.IncrementTurnsTillDelay) != 0)
                {
                    pieceMove.turnsToRemove++;
                }
            }

            if (entry.undoMoveType == "botAction")
            {
                UndoBotAction a = entry.botAction;

                BotTemplate bot = a.bot;
                PieceAction action = a.action;

                if ((action & PieceAction.ResetKing) != 0)
                {
                    bot.king = isolatedGetKing(bot.currentBoardState, bot.color);
                }
            }
        }
    }

    public static UndoMovedPiece undo_movePieceBoardState(Piece piece, coords coords, BoardState bs)
    {
        if (coords.x < 0 || coords.y < 0)
        {
            return null;
        }

        UndoMovedPiece undo = new UndoMovedPiece(piece);
        undo.initialPosition = new coords(piece.position.x, piece.position.y);

        coords position = new coords( piece.position.x - 1, piece.position.y - 1 );

        updateBoardState(position, piece, "r", bs);
        updateBoardState(new coords(coords.x, coords.y ), piece, "a", bs);
        piece.position = new coords(coords.x + 1, coords.y + 1 );
        undo.newPosition = new coords(coords.x + 1, coords.y + 1);

        if (!piece.hasMoved)
        {
            piece.hasMoved = true;
            undo.revertHasMoved = true;
        }

        return undo;
    }

    public static UndoMove undo_simulatePieceAbility(BoardState bs_, PieceAbility pieceAbility)
    {
        // Init undo move
        UndoMove undo = new UndoMove();

        Piece piece = pieceAbility.piece;
        PieceAbilities ability = pieceAbility.ability;

        coords coords = new coords(pieceAbility.coords.x, pieceAbility.coords.y );
        coords adjustedCoords = new coords(coords.x - 1, coords.y - 1 );
        coords adjustedPiecePosition = new coords(piece.position.x - 1, piece.position.y - 1 );

        List<Piece> placePieces = pieceAbility.placePieces;
        List<coords> placeCoords = pieceAbility.placeCoords;
        Piece secondPiece = pieceAbility.secondPiece;

        System.Random rand = globalDefs.globalRand;

        if (ability == PieceAbilities.Vomit)
        {
            int numPieces = placePieces.Count;
            int numCoords = placeCoords.Count;

            if (numPieces >= numCoords)
            {
                foreach (coords coords_ in placeCoords)
                {
                    int idx = rand.Next(numPieces);
                    numPieces--;

                    Piece p_ = placePieces[idx];
                    placePieces.Remove(p_);

                    coords coords__ = new coords( coords_.x - 1, coords_.y - 1 );

                    //Track this update with an UndoStorage
                    UndoStorage barf = new UndoStorage(piece, p_, false, new coords(coords_.x, coords_.y));
                    undo.addStorage(barf);

                    updateBoardState(coords__, p_, "a", bs_);

                    UndoState pieceJailedFix = new UndoState(p_, p_.states);
                    undo.addState(pieceJailedFix);
                    removeState(p_, PieceState.Jailed);
                    

                    piece.storage.Remove(p_);
                }
            }
            else
            {
                foreach (Piece p_ in placePieces)
                {
                    int idx = rand.Next(numCoords);
                    numCoords--;

                    coords c_ = placeCoords[idx];
                    placeCoords.RemoveAt(idx);

                    c_ = new coords( c_.x - 1, c_.y - 1 );

                    //Track this update with an UndoStorage
                    UndoStorage barf = new UndoStorage(piece, p_, false, new coords(c_.x + 1, c_.y + 1));
                    undo.addStorage(barf);

                    updateBoardState(c_, p_, "a", bs_);

                    UndoState pieceJailedFix = new UndoState(p_, p_.states);
                    undo.addState(pieceJailedFix);
                    removeState(p_, PieceState.Jailed);

                    piece.storage.Remove(p_);
                }
            }
        }
        else if (ability == PieceAbilities.CastleLeft)
        {
            coords kingCoords = coords;
            coords rookCoords = new coords( kingCoords.x + 1, kingCoords.y );

            coords adjustedKingCoords = new coords ( kingCoords.x - 1, kingCoords.y - 1 );
            coords adjustedRookCoords = new coords( rookCoords.x - 1, rookCoords.y - 1 );

            //King
            UndoMovedPiece kingMove = undo_movePieceBoardState(piece, adjustedKingCoords, bs_);
            undo.addMove(kingMove);
            //Rook
            UndoMovedPiece rookMove = undo_movePieceBoardState(secondPiece, adjustedRookCoords, bs_);
            undo.addMove(rookMove);

            if (checkAbility(piece, PieceAbilities.CastleLeft))
            {
                UndoAbility castleLeft = new UndoAbility(piece, piece.abilities);
                undo.addAbility(castleLeft);
                removeAbility(piece, PieceAbilities.CastleLeft);
            }

            if (checkAbility(piece, PieceAbilities.CastleRight))
            {
                UndoAbility castleRight = new UndoAbility(piece, piece.abilities);
                undo.addAbility(castleRight);
                removeAbility(piece, PieceAbilities.CastleRight);
            }
        }
        else if (ability == PieceAbilities.CastleRight)
        {
            coords kingCoords = coords;
            coords rookCoords = new coords( kingCoords.x - 1, kingCoords.y );

            coords adjustedKingCoords = new coords(kingCoords.x - 1, kingCoords.y - 1);
            coords adjustedRookCoords = new coords(rookCoords.x - 1, rookCoords.y - 1);

            //King
            UndoMovedPiece kingMove = undo_movePieceBoardState(piece, adjustedKingCoords, bs_);
            undo.addMove(kingMove);
            //Rook
            UndoMovedPiece rookMove = undo_movePieceBoardState(secondPiece, adjustedRookCoords, bs_);
            undo.addMove(rookMove);

            if (checkAbility(piece, PieceAbilities.CastleLeft))
            {
                UndoAbility castleLeft = new UndoAbility(piece, piece.abilities);
                undo.addAbility(castleLeft);
                removeAbility(piece, PieceAbilities.CastleLeft);
            }

            if (checkAbility(piece, PieceAbilities.CastleRight))
            {
                UndoAbility castleRight = new UndoAbility(piece, piece.abilities);
                undo.addAbility(castleRight);
                removeAbility(piece, PieceAbilities.CastleRight);
            }
        }
        else if (ability == PieceAbilities.Freeze)
        {
            UndoState pieceFreeze = new UndoState(secondPiece, secondPiece.states);
            undo.addState(pieceFreeze);
            addState(secondPiece, PieceState.Frozen);

            UndoAbility pieceUnfreeze = new UndoAbility(secondPiece, secondPiece.abilities);
            addAbility(secondPiece, PieceAbilities.Unfreeze);
            undo.addAbility(pieceUnfreeze);
        }
        else if (ability == PieceAbilities.Unfreeze)
        {
            UndoAbility pieceUnfreeze = new UndoAbility(piece, piece.abilities);
            removeAbility(piece, PieceAbilities.Unfreeze);
            undo.addAbility(pieceUnfreeze);

            UndoState pieceFreeze = new UndoState(piece, piece.states);
            removeState(piece, PieceState.Frozen);
            undo.addState(pieceFreeze);
        }
        else if (ability == PieceAbilities.Spawn)
        {
            Piece spawned = Spawnables.create(piece.spawnable, piece.color, true);
            piece.numSpawns--;
            UndoPieceAction incSpawn = new UndoPieceAction(piece, PieceAction.IncrementSpawn);
            undo.addAction(incSpawn);
            if (piece.numSpawns <= 0)
            {
                UndoAbility pieceSpawn = new UndoAbility(piece, piece.abilities);
                undo.addAbility(pieceSpawn);
                removeAbility(piece, PieceAbilities.Spawn);
            }

            UndoMovedPiece mp = new UndoMovedPiece(spawned, new coords(-1, -1), new coords(coords.x, coords.y), false, true, false);
            undo.addMove(mp);
            updateBoardState(adjustedCoords, spawned, "a", bs_);
        }
        else if (ability == PieceAbilities.Spit)
        {
            undo_isolatedCollateralDeath(isolatedGetPiecesOnCoordsBoardGrid(adjustedCoords.x, adjustedCoords.y, bs_.boardGrid, false), bs_, undo);

            UndoStorage spit = new UndoStorage(piece, secondPiece, false, new coords(coords.x, coords.y));
            undo.addStorage(spit);

            UndoState pieceJailedFix = new UndoState(secondPiece, secondPiece.states);
            undo.addState(pieceJailedFix);
            removeState(secondPiece, PieceState.Jailed);

            updateBoardState(adjustedCoords, secondPiece, "a", bs_);
            piece.storage.RemoveAll(p => p.name == secondPiece.name);
        }
        else if (ability == PieceAbilities.Dematerialize)
        {
            UndoState pieceState = new UndoState(piece, piece.states);
            undo.addState(pieceState);
            addState(piece, PieceState.Dematerialized);

            UndoAbility pieceDematerialize = new UndoAbility(piece, piece.abilities);
            undo.addAbility(pieceDematerialize);
            removeAbility(piece, PieceAbilities.Dematerialize);

            UndoAbility pieceMaterialize = new UndoAbility(piece, piece.abilities);
            undo.addAbility(pieceMaterialize);
            addAbility(piece, PieceAbilities.Materialize);
        }
        else if (ability == PieceAbilities.Materialize)
        {
            UndoState pieceState = new UndoState(piece, piece.states);
            undo.addState(pieceState);
            removeState(piece, PieceState.Dematerialized);

            UndoAbility pieceDematerialize = new UndoAbility(piece, piece.abilities);
            undo.addAbility(pieceDematerialize);
            addAbility(piece, PieceAbilities.Dematerialize);

            UndoAbility pieceMaterialize = new UndoAbility(piece, piece.abilities);
            undo.addAbility(pieceMaterialize);
            removeAbility(piece, PieceAbilities.Materialize);

            undo_isolatedOnDeathsDontIncludeAttacker(piece, adjustedCoords, bs_, undo);

            undo_isolatedCheckPromote(piece, bs_, undo);
        }
        else if (ability == PieceAbilities.Split)
        {
            removeAbility(piece, PieceAbilities.Split);

            undo_isolatedRemovePiece(piece, bs_, undo);

            Piece leftPawn = Spawnables.create("LeftPawn", piece.color, true);
            leftPawn.position = new coords( piece.position.x, piece.position.y );
            undo_isolatedAddPiece(leftPawn, bs_, undo);

            Piece rightPawn = Spawnables.create("RightPawn", piece.color, true);
            rightPawn.position = new coords( piece.position.x, piece.position.y );
            undo_isolatedAddPiece(rightPawn, bs_, undo);
        }

        return undo;
    }

    public static void undo_isolatedCheckPromote(Piece piece, BoardState bs, UndoMove undo)
    {
        if (piece.promotesInto != "" && !checkState(piece, PieceState.Dematerialized))
        {
            if (piece.position.y == piece.promotingRow)
            {
                string pname = piece.promotesInto;

                if (nonResettables.ruleset == "Normal")
                {
                    pname = "Queen";
                }

                Piece p = Spawnables.create(pname, piece.color, true);

                undo_isolatedCollateralDeath(isolatedGetPiecesOnCoordsBoardGrid(piece.position.x - 1, piece.position.y - 1, bs.boardGrid, false), bs, undo);

                undo_isolatedRemovePiece(piece, bs, undo);
                p.position = new coords( piece.position.x, piece.position.y );
                undo_isolatedAddPiece(p, bs, undo);
            }
        }
    }
    /*
    public static void undo_isolatedCollateralDeath(List<Piece> deadPieces, BoardState bs, UndoMove undo)
    {
        foreach (Piece deadPiece in new List<Piece>(deadPieces))
        {
            if (checkState(deadPiece, PieceState.Shield) || checkCaptureTheFlag(deadPiece))
            {
                continue;
            }

            if (deadPiece.lives != 0)
            {
                undo_isolatedHandleMultipleLivesDeath(deadPiece, bs, undo);

                continue;
            }
            else
            {
                UndoMovedPiece deadPiece_ = new UndoMovedPiece(deadPiece, new coords(deadPiece.position.x, deadPiece.position.y), new coords(-1, -1), true, false, false);
                undo.addMove(deadPiece_);
                updateBoardState(new coords( deadPiece.position.x - 1, deadPiece.position.y - 1 ), deadPiece, "r", bs);

                UndoPieceAction undoAction = new UndoPieceAction(deadPiece, PieceAction.SetAlive);
                undo.addAction(undoAction);
                deadPiece.alive = 0;
            }
        }
    }
    */
    public static void undo_isolatedCollateralDeath(List<Piece> deadPieces, BoardState bs, UndoMove undo)
    {
        //foreach (Piece deadPiece in new List<Piece>(deadPieces))
        for (int i = 0; i < deadPieces.Count; i++)
        {
            Piece deadPiece = deadPieces[i];
            if (checkState(deadPiece, PieceState.Shield) || checkCaptureTheFlag(deadPiece))
            {
                continue;
            }

            if (deadPiece.lives != 0)
            {
                undo_isolatedHandleMultipleLivesDeath(deadPiece, bs, undo);

                continue;
            }
            else
            {
                UndoMovedPiece deadPiece_ = new UndoMovedPiece(deadPiece, new coords(deadPiece.position.x, deadPiece.position.y), new coords(-1, -1), true, false, false);
                undo.addMove(deadPiece_);
                updateBoardState(new coords(deadPiece.position.x - 1, deadPiece.position.y - 1), deadPiece, "r", bs);

                UndoPieceAction undoAction = new UndoPieceAction(deadPiece, PieceAction.SetAlive);
                undo.addAction(undoAction);
                deadPiece.alive = 0;
            }
        }
    }

    public static void undo_isolatedHandleMultipleLivesDeath(Piece deadPiece, BoardState bs, UndoMove undo)
    {
        UndoPieceAction incLives = new UndoPieceAction(deadPiece, PieceAction.IncrementLives);
        undo.addAction(incLives);
        deadPiece.lives--;

        if (!isOnStartSquare(deadPiece) && !isolatedIsPieceOnStartSquare(deadPiece, bs))
        {
            UndoMovedPiece undoMove = undo_movePieceBoardState(deadPiece, new coords( deadPiece.startSquare.x - 1, deadPiece.startSquare.y - 1 ), bs);
            undo.addMove(undoMove);
        }
        else
        {
            UndoMovedPiece deadPiece_ = new UndoMovedPiece(deadPiece, new coords(deadPiece.position.x, deadPiece.position.y), new coords(-1, -1), true, false, false);
            undo.addMove(deadPiece_);
            updateBoardState(new coords( deadPiece.position.x - 1, deadPiece.position.y - 1 ), deadPiece, "r", bs);
        }
    }

    /*
    public static PieceState undo_isolatedOnDeathsDontIncludeAttacker(Piece attacker, coords deadCoords, BoardState bs, UndoMove undo)
    {
        List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false));
        pieces.RemoveAll(p => p.name == attacker.name);

        PieceState ps = PieceState.None;

        foreach (Piece piece in pieces)
        {
            if (!checkState(piece, PieceState.Dematerialized))
            {
                var onDeathVars = undo_isolatedOnDeath(piece, attacker, bs, undo);
                ps |= onDeathVars.stackingStates;
            }
        }

        return ps;
    }
    */

    public static PieceState undo_isolatedOnDeathsDontIncludeAttacker(Piece attacker, coords deadCoords, BoardState bs, UndoMove undo)
    {
        //List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false));
        //pieces.RemoveAll(p => p.name == attacker.name);
        List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false);
        pieces.Remove(attacker);

        PieceState ps = PieceState.None;

        //foreach (Piece piece in pieces)
        for (int i = 0; i < pieces.Count; i++)
        {
            Piece piece = pieces[i];
            if (!checkState(piece, PieceState.Dematerialized))
            {
                var onDeathVars = undo_isolatedOnDeath(piece, attacker, bs, undo);
                ps |= onDeathVars.stackingStates;
            }
        }

        return ps;
    }

    public static (PieceState stackingStates, bool attackerDied) undo_isolatedOnDeath(Piece deadPiece, Piece attackerPiece, BoardState bs, UndoMove undo)
    {
        coords attackerCoords = attackerPiece.position;
        coords deadPieceCoords = deadPiece.position;

        coords adjustedAttackerCoords = new coords( attackerPiece.position.x - 1, attackerPiece.position.y - 1 );
        coords adjustedDeadPieceCoords = new coords(deadPiece.position.x - 1, deadPiece.position.y - 1 );

        bool skipCollateral = false;
        bool attackerDied = false;

        System.Random rand = new System.Random();

        //Infinite/Multi Lives
        if (deadPiece.lives != 0)
        {
            undo_isolatedHandleMultipleLivesDeath(deadPiece, bs, undo);

            return (PieceState.None, false);
        }

        //Electric (Skip as it depends on random values)

        //Hungry
        if (checkState(attackerPiece, PieceState.Hungry))
        {
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            attackerPiece.storage.Add(deadPiece);
            UndoStorage store = new UndoStorage(attackerPiece, deadPiece, true, new coords(deadPieceCoords.x, deadPieceCoords.y));
            undo.addStorage(store);

            skipCollateral = true;
            undo_isolatedRemovePiece(deadPiece, bs, undo);

            return (PieceState.None, false);
        }

        if (checkState(deadPiece, PieceState.Hungry))
        {
            if (deadPiece.storage != null && deadPiece.storage.Count > 0)
            {
                List<coords> placeCoords_ = isolatedGetCollateralSquares(deadPiece, bs);
                List<Piece> placePieces_ = deadPiece.storage;

                PieceAbility pa = new PieceAbility(deadPiece, PieceAbilities.Vomit, deadPiece.position, placePieces_, placeCoords_, null);
                //Simulate Ability

                //List<Piece> placePieces = new List<Piece>(pa.placePieces);
                List<Piece> placePieces = pa.placePieces;
                List<coords> placeCoords = pa.placeCoords;

                int numPieces = placePieces.Count;
                int numCoords = placeCoords.Count;

                if (numPieces >= numCoords)
                {
                    foreach (coords coords_ in placeCoords)
                    {
                        int idx = rand.Next(numPieces);
                        numPieces--;

                        Piece p_ = placePieces[idx];
                        placePieces.RemoveAll(p => p.name == p_.name);

                        coords coords__ = new coords( coords_.x - 1, coords_.y - 1 );

                        //Track this update with an UndoStorage
                        UndoStorage barf = new UndoStorage(deadPiece, p_, false, new coords(coords_.x, coords_.y));
                        undo.addStorage(barf);

                        updateBoardState(coords__, p_, "a", bs);

                        UndoState pieceJailedFix = new UndoState(p_, p_.states);
                        undo.addState(pieceJailedFix);
                        removeState(p_, PieceState.Jailed);

                        deadPiece.storage.RemoveAll(p => p.name == p_.name);
                    }
                }
                else
                {
                    //foreach (Piece p_ in new List<Piece>(placePieces))
                    for (int i = 0; i < placePieces.Count; i++)
                    {
                        Piece p_ = placePieces[i];
                        int idx = rand.Next(numCoords);
                        numCoords--;

                        coords c = placeCoords[idx];
                        placeCoords.Remove(c);

                        coords gridCoords = new coords( c.x - 1, c.y - 1 );

                        UndoStorage barf = new UndoStorage(deadPiece, p_, false, new coords(c.x, c.y));
                        undo.addStorage(barf);

                        updateBoardState(gridCoords, p_, "a", bs);

                        UndoState pieceJailedFix = new UndoState(p_, p_.states);
                        undo.addState(pieceJailedFix);
                        removeState(p_, PieceState.Jailed);

                        deadPiece.storage.RemoveAll(p => p.name == p_.name); ;
                    }
                }
            }
        }

        //Spitting
        if (checkState(attackerPiece, PieceState.Spitting))
        {
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            if (attackerPiece.storage.Count < attackerPiece.storageLimit)
            {
                undo_isolatedRemovePiece(deadPiece, bs, undo);

                UndoStorage us = new UndoStorage(attackerPiece, deadPiece, true, new coords(deadPieceCoords.x, deadPieceCoords.y));
                undo.addStorage(us);

                attackerPiece.storage.Add(deadPiece);
                skipCollateral = true;
            }
            else
            {
                List<Piece> piece = pieceToList(deadPiece);
                undo_isolatedCollateralDeath(piece, bs, undo);
            }

            return (PieceState.None, false);
        }

        PieceState stackingStates = PieceState.None;
        //Stacking
        if (checkState(attackerPiece, PieceState.Stacking) && deadPiece.lives == 0)
        {
            Piece original = attackerPiece;
            Piece clone = clonePiece(original);

            stackingStates |= deadPiece.states;

            addAbility(attackerPiece, deadPiece.abilities);

            //Moves
            coords[] moves = combineMoveSets(original.moves, deadPiece.moves);
            coords[] oneTimeMoves = combineMoveSets(original.oneTimeMoves, deadPiece.oneTimeMoves);
            coords[] moveAndAttacks = combineMoveSets(original.moveAndAttacks, deadPiece.moveAndAttacks);
            coords[] oneTimeMoveAndAttacks = combineMoveSets(original.oneTimeMoveAndAttacks, deadPiece.oneTimeMoveAndAttacks);
            coords[] murderousAttacks = combineMoveSets(original.murderousAttacks, deadPiece.murderousAttacks);
            coords[] conditionalAttacks = combineMoveSets(original.conditionalAttacks, deadPiece.conditionalAttacks);
            coords[] attacks = combineMoveSets(original.attacks, deadPiece.attacks);
            coords[] jumpAttacks = combineMoveSets(original.jumpAttacks, deadPiece.jumpAttacks);
            coords[] flagMove1 = combineMoveSets(original.flagMove1, deadPiece.flagMove1);
            coords[] flagMove2 = combineMoveSets(original.flagMove2, deadPiece.flagMove2);
            coords[] pushMoves = combineMoveSets(original.pushMoves, deadPiece.pushMoves);
            coords[] enPassantMoves = combineMoveSets(original.enPassantMoves, deadPiece.enPassantMoves);

            clone.moves = moves;
            clone.oneTimeMoves = oneTimeMoves;
            clone.moveAndAttacks = moveAndAttacks;
            clone.oneTimeMoveAndAttacks = oneTimeMoveAndAttacks;
            clone.murderousAttacks = murderousAttacks;
            clone.conditionalAttacks = conditionalAttacks;
            clone.attacks = attacks;
            clone.jumpAttacks = jumpAttacks;
            clone.flagMove1 = flagMove1;
            clone.flagMove2 = flagMove2;
            clone.pushMoves = pushMoves;
            clone.enPassantMoves = enPassantMoves;

            undo_isolatedRemovePiece(original, bs, undo);
            undo_isolatedAddPiece(clone, bs, undo); 
        }

        //Jailer
        if (checkState(attackerPiece, PieceState.Jailer))
        {
            UndoState us = new UndoState(deadPiece, deadPiece.states);
            undo.addState(us);

            addState(deadPiece, PieceState.Jailed);

            return (PieceState.None, false);
        }

        //Crook
        if (checkState(deadPiece, PieceState.Crook) && deadPiece.color != attackerPiece.color)
        {
            UndoState us = new UndoState(deadPiece, deadPiece.states);
            undo.addState(us);

            addState(deadPiece, PieceState.Jailed);

            return (PieceState.None, false);
        }

        //Medusa
        if (checkState(attackerPiece, PieceState.Medusa))
        {
            if (attackerPiece.numSpawns != 0)
            {
                attackerPiece.numSpawns--;
                UndoPieceAction upa_ = new UndoPieceAction(attackerPiece, PieceAction.IncrementSpawn);
                undo.addAction(upa_);

                undo_isolatedRemovePiece(deadPiece, bs, undo);

                Piece shieldPawn = Spawnables.create("ShieldPawn", attackerPiece.color * -1, true);
                shieldPawn.position = deadPiece.position;

                undo_isolatedAddPiece(shieldPawn, bs, undo);
            }
        }

        if (!skipCollateral)
        {
            if (attackerPiece.collateralType == 0)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, PieceState.Defuser, bs))
                {
                    //undo_isolatedCollateralDeath(pieceToList(attackerPiece), bs, undo);
                    undo_isolatedCollateralDeath(pieceToList(deadPiece), bs, undo);
                    //attackerDied = true;
                    return (stackingStates, attackerDied);
                }

                for (int i = 0; i < attackerPiece.collateral.GetLength(0); i++)
                {
                    coords coords = new coords(adjustedDeadPieceCoords.x + attackerPiece.collateral[i].x, adjustedDeadPieceCoords.y + attackerPiece.collateral[i].y );

                    if (attackerPiece.collateral[i].x == 0 && attackerPiece.collateral[i].y == 0)
                    {
                        undo_isolatedCollateralDeath(pieceToList(attackerPiece), bs, undo);
                        attackerDied = true;
                    }

                    //List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false));
                    List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);
                    undo_isolatedCollateralDeath(pieces, bs, undo);
                }
            }

            if (attackerPiece.collateralType == 2)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, PieceState.Defuser, bs))
                {
                    return (stackingStates, attackerDied);
                }

                for (int i = 0; i < attackerPiece.collateral.GetLength(0); i++)
                {
                    coords coords = new coords(adjustedDeadPieceCoords.x + attackerPiece.collateral[i].x, adjustedDeadPieceCoords.y + attackerPiece.collateral[i].y);

                    if (attackerPiece.collateral[i].x == 0 && attackerPiece.collateral[i].y == 0)
                    {
                        UndoState us = new UndoState(attackerPiece, attackerPiece.states);
                        undo.addState(us);

                        addState(attackerPiece, PieceState.Frozen);

                        UndoAbility ua = new UndoAbility(attackerPiece, attackerPiece.abilities);
                        undo.addAbility(ua);
                        addAbility(attackerPiece, PieceAbilities.Unfreeze);
                    }

                    //List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false));
                    List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);
                    foreach (Piece p in pieces)
                    {
                        UndoState us = new UndoState(p, p.states);
                        undo.addState(us);
                        addState(p, PieceState.Frozen);

                        UndoAbility ua = new UndoAbility(p, p.abilities);
                        undo.addAbility(ua);
                        addAbility(p, PieceAbilities.Unfreeze);
                    }
                }
            }

            if (deadPiece.collateralType == 1)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, PieceState.Defuser, bs))
                {
                    //undo_isolatedCollateralDeath(pieceToList(attackerPiece), bs, undo);
                    undo_isolatedCollateralDeath(pieceToList(deadPiece), bs, undo);
                    //attackerDied = true;
                    return (stackingStates, attackerDied);
                }

                for (int i = 0; i < deadPiece.collateral.GetLength(0); i++)
                {
                    coords coords = new coords(adjustedDeadPieceCoords.x + deadPiece.collateral[i].x, adjustedDeadPieceCoords.y + deadPiece.collateral[i].y);

                    if (deadPiece.collateral[i].x == 0 && deadPiece.collateral[i].y == 0)
                    {
                        undo_isolatedCollateralDeath(pieceToList(deadPiece), bs, undo);
                        undo_isolatedCollateralDeath(pieceToList(attackerPiece), bs, undo);
                        attackerDied = true;
                    }

                    //List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false));
                    List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);
                    undo_isolatedCollateralDeath(pieces, bs, undo);
                }
            }
        }

        deadPiece.alive = 0;
        UndoPieceAction upa = new UndoPieceAction(deadPiece, PieceAction.SetAlive);
        undo.addAction(upa);

        undo_isolatedRemovePiece(deadPiece, bs, undo);

        return (stackingStates, attackerDied);

    }

    public static void undo_isolatedRemovePiece(Piece p, BoardState bs, UndoMove undo)
    {
        UndoMovedPiece ump = new UndoMovedPiece(p, new coords(p.position.x, p.position.y), new coords(-1, -1), true, false, false);
        undo.addMove(ump);
        updateBoardState(new coords( p.position.x - 1, p.position.y - 1 ), p, "r", bs);
    }

    public static void undo_isolatedAddPiece(Piece p, BoardState bs, UndoMove undo)
    {
        UndoMovedPiece ump = new UndoMovedPiece(p, new coords(-1, -1), new coords(p.position.x, p.position.y), false, true, false);
        undo.addMove(ump);
        updateBoardState(new coords( p.position.x - 1, p.position.y - 1 ), p, "a", bs);
    }

    public static UndoMove undo_simulatePieceMove(BoardState bs, Piece piece, coords coords)
    {
        // Init undo move
        UndoMove undo = new UndoMove();

        Piece botKing = isolatedGetKing(bs, piece.color);
        Piece oppKing = isolatedGetKing(bs, piece.color * -1);

        coords unadjusted = coords;
        coords = new coords(coords.x - 1, coords.y - 1);

        List<Piece> piecesOnCoordsPreDeath = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);
        bool death = isolatedIsDeath(piecesOnCoordsPreDeath, piece);

        bool attackerDied = false;
        PieceState stackingStates = PieceState.None;

        if (death && !checkState(piece, PieceState.Delayed))
        {
            Piece destroyer = piece;
            var deathVars = undo_isolatedOnDeaths(destroyer, coords, bs, undo);
            attackerDied = deathVars.attackerDied;
            stackingStates |= deathVars.stackingStates;
        }


        if (bs.delayedQueue == null)
        {
            bs.delayedQueue = new DelayedQueue();
        }

        //Deincrement
        foreach (PieceMove item in bs.delayedQueue._items)
        {
            item.turnsToRemove--;
            UndoDelayedQueueAction undoDQ = new UndoDelayedQueueAction(item, PieceAction.IncrementTurnsTillDelay);
            undo.addDelayedQueueAction(undoDQ);
        }

        bool delayedMoves = true;
        while (delayedMoves)
        {
            PieceMove moveToCheck = bs.delayedQueue.Peek();

            if (moveToCheck != null && moveToCheck.turnsToRemove <= 0)
            {
                moveToCheck = bs.delayedQueue.Dequeue();
                UndoDelayedQueueAction undoDQ = new UndoDelayedQueueAction(moveToCheck, PieceAction.AddToQueueFront);
                undo.addDelayedQueueAction(undoDQ);

                undo_isolatedDelayedMove(moveToCheck, bs, undo);
            }
            else
            {
                delayedMoves = false;
            }
        }

        if (checkState(piece, PieceState.Delayed))
        {
            PieceMove delayedMove = new PieceMove(piece, new coords( coords.x, coords.y ), 2);
            bs.delayedQueue.Enqueue(delayedMove);

            UndoDelayedQueueAction undoDQ = new UndoDelayedQueueAction(delayedMove, PieceAction.RemoveFromQueue);
            undo.addDelayedQueueAction(undoDQ);

            return undo;
        }

        List<Piece> piecesOnSquare = isolatedGetPiecesOnCoordsBoardGrid(piece.position.x - 1, piece.position.y - 1, bs.boardGrid, false);
        foreach (Piece pieceOnSquare in piecesOnSquare)
        {
            if (checkState(pieceOnSquare, PieceState.Crook))
            {
                if (piecesOnSquare.Count == 2)
                {
                    UndoState us_ = new UndoState(pieceOnSquare, pieceOnSquare.states);
                    undo.addState(us_);
                    removeState(pieceOnSquare, PieceState.Jailed);
                }
            }

            if (checkState(piece, PieceState.Jailer))
            {
                UndoState us_ = new UndoState(pieceOnSquare, pieceOnSquare.states);
                undo.addState(us_);
                removeState(pieceOnSquare, PieceState.Jailed);
            }
        }

        coords originalCoords = new coords( piece.position.x, piece.position.y );
        if (!attackerDied)
        {
            UndoMovedPiece mp = undo_movePieceBoardState(piece, coords, bs);
            undo.addMove(mp);
        }
        else
        {
            undo_isolatedRemovePiece(piece, bs, undo);
        }

        if (checkState(piece, PieceState.Piggyback))
        {
            List<Piece> piecesOnSquare2 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords.x - 1, originalCoords.y - 1, bs.boardGrid, false);
            //foreach (Piece pieceOnSquare in new List<Piece>(piecesOnSquare2))
            for (int i = 0; i < piecesOnSquare2.Count; i++)
            {
                Piece pieceOnSquare = piecesOnSquare2[i];
                if (pieceOnSquare.color == piece.color)
                {
                    UndoMovedPiece pm = undo_movePieceBoardState(pieceOnSquare, coords, bs);
                    undo.addMove(pm);
                    pieceOnSquare.hasMoved = true;
                }
            }
        }

        List<Piece> piecesOnSquare3 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords.x - 1, originalCoords.y - 1, bs.boardGrid, false);
        //foreach (Piece pieceOnSquare in new List<Piece>(piecesOnSquare3))
        for (int i = 0; i < piecesOnSquare3.Count; i++)
        {
            Piece pieceOnSquare = piecesOnSquare3[i];
            if (checkState(pieceOnSquare, PieceState.Jockey))
            {
                UndoMovedPiece pm = undo_movePieceBoardState(pieceOnSquare, coords, bs);
                undo.addMove(pm);
                pieceOnSquare.hasMoved = true;
            }
        }

        if (piece.promotesInto != "")
        {
            if (piece.position.y == piece.promotingRow)
            {
                string pname = piece.promotesInto;

                if (nonResettables.ruleset == "Normal")
                {
                    pname = "Queen";
                }

                Piece p = Spawnables.create(pname, piece.color, true);
                updateBoardState(new coords( piece.position.x - 1, piece.position.y - 1 ), piece, "r", bs);
                updateBoardState(new coords(coords.x, coords.y ), p, "a", bs);

                undo_isolatedRemovePiece(piece, bs, undo);
                undo_isolatedAddPiece(p, bs, undo);
            }
        }

        UndoState us = new UndoState(piece, piece.states);
        piece.states |= stackingStates;
        undo.addState(us);

        Piece botWhiteKing = piece.color == 1 ? botKing : oppKing;
        Piece botBlackKing = piece.color == -1 ? botKing : oppKing;

        if (botWhiteKing == null || botBlackKing == null)
        {
            //Debug.Log("King is null during simulated move.");
            return undo;
        }

        coords botWhiteKingPos = new coords( botWhiteKing.position.x - 1, botWhiteKing.position.y - 1 );
        coords botBlackKingPos = new coords( botBlackKing.position.x - 1, botBlackKing.position.y - 1 );

        if (checkState(botWhiteKing, PieceState.Heartbroken))
        {
            if (!isolatedIsPieceTypeOnBoard("q", 1, bs))
            {
                Piece tempKing = Spawnables.create("DepressedKing", 1, true);

                updateBoardState(botWhiteKingPos, tempKing, "a", bs);
                updateBoardState(botWhiteKingPos, botWhiteKing, "r", bs);

                undo_isolatedRemovePiece(botWhiteKing, bs, undo);
                undo_isolatedAddPiece(tempKing, bs, undo);
            }
        }

        if (checkState(botBlackKing, PieceState.Heartbroken))
        {
            if (!isolatedIsPieceTypeOnBoard("q", -1, bs))
            {
                Piece tempKing = Spawnables.create("DepressedKing", -1, true);

                updateBoardState(botBlackKingPos, tempKing, "a", bs);
                updateBoardState(botBlackKingPos, botBlackKing, "r", bs);

                undo_isolatedRemovePiece(botBlackKing, bs, undo);
                undo_isolatedAddPiece(tempKing, bs, undo);
            }
        }

        return undo;
    }

    public static void undo_isolatedDelayedMove(PieceMove pMove, BoardState bs, UndoMove undo)
    {
        Piece piece = pMove.piece;
        coords coords = pMove.coords;

        coords = new coords( coords.x - 1, coords.y - 1 );

        List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);

        if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color)*/!isolatedIsColorOnCoords(piecesOnCoords, true, piece.color))
        {
            bool death = false;

            if (piecesOnCoords.Count != 0)
            {
                death = true;

                //Debug.LogWarning("Checking for death");

                if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color * -1)*/!isolatedIsColorOnCoords(piecesOnCoords, true, piece.color * -1) && !checkState(piece, PieceState.Murderous))
                {
                    death = false;
                }
                else if (checkStateAllOnSquare(piecesOnCoords, PieceState.Dematerialized))
                {
                    death = false;
                }
                else if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords))
                {
                    death = false;
                }
                else if (checkState(piece, PieceState.Dematerialized))
                {
                    death = false;
                }
            }

            if (death)
            {
                undo_isolatedOnDeaths(piece, new coords(coords.x, coords.y), bs, undo);
            }

            piece.hasMoved = true;
            //Debug.Log("Delayed Coords: " + coords.x + "," + coords.y);
            UndoMovedPiece undoMovedPiece = undo_movePieceBoardState(piece, new coords(coords.x, coords.y), bs);

            if (undoMovedPiece != null) { undo.addMove(undoMovedPiece); }
        }
    }
    /*
    public static (PieceState stackingStates, bool attackerDied) undo_isolatedOnDeaths(Piece attacker, coords deadCoords, BoardState bs, UndoMove undo)
    {
        List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false));

        PieceState stacks = PieceState.None;
        bool attackerDied = false;
        foreach (Piece piece in pieces)
        {
            //Debug.Log(piece.name + " died on (" + piece.position.x + "," + piece.position.y + ") during a simulated move");
            if (!checkState(piece, PieceState.Dematerialized))
            {
                var onDeathVars = undo_isolatedOnDeath(piece, attacker, bs, undo);
                stacks = onDeathVars.stackingStates;
                attackerDied = onDeathVars.attackerDied;
            }
        }

        return (stacks, attackerDied);
    }
    */
    public static (PieceState stackingStates, bool attackerDied) undo_isolatedOnDeaths(Piece attacker, coords deadCoords, BoardState bs, UndoMove undo)
    {
        //List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false));
        List<Piece> pieces = isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false);

        PieceState stacks = PieceState.None;
        bool attackerDied = false;
        //foreach(Piece piece in pieces)
        for (int i = 0; i < pieces.Count; i++)
        {
            Piece piece = pieces[i];
            //Debug.Log(piece.name + " died on (" + piece.position.x + "," + piece.position.y + ") during a simulated move");
            if (!checkState(piece, PieceState.Dematerialized))
            {
                var onDeathVars = undo_isolatedOnDeath(piece, attacker, bs, undo);
                stacks = onDeathVars.stackingStates;
                attackerDied = onDeathVars.attackerDied;
            }
        }

        return (stacks, attackerDied);
    }
}
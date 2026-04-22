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
    public bool undo; //true = undo, false = redo (ie. for simulated Vomit)
    public coords coords;

    public UndoStorage(Piece piece_, Piece storedPiece_, bool undo_, coords coords_)
    {
        piece = piece_;
        storedPiece = storedPiece_;
        undo = undo_;
        coords = coords_;
    }
}

public struct UndoRevertFull
{
    public Piece reference;
    public Piece clone;

    public UndoRevertFull(Piece piece_, Piece clone_)
    {
        reference = piece_;
        clone = clone_;
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
    public bool add; //True if you should add WHILE undoing the move;

    public UndoState(Piece piece_, PieceState pieceState_, bool add_)
    {
        piece = piece_;
        state = pieceState_;
        add = add_;
    }
}

public struct UndoAbility
{
    public Piece piece;
    public string ability;
    public bool add; //True if you should add WHILE undoing the move;

    public UndoAbility(Piece piece_, string pieceAbility_, bool add_)
    {
        piece = piece_;
        ability = pieceAbility_;
        add = add_;
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

public class UndoMove
{
    public List<UndoMovedPiece> changed;
    public List<UndoStorage> changedStorage;
    public List<UndoAbility> changedAbilities;
    public List<UndoState> changedStates;
    public List<UndoPieceAction> actions;
    public List<UndoRevertFull> undoRevertFull;
    public List<UndoDelayedQueueAction> undoDelayedQueueAction;

    public UndoMove(List<UndoMovedPiece> undoMoves)
    {
        changed = undoMoves;
    }

    public UndoMove(List<UndoStorage> undoStorage)
    {
        changedStorage = undoStorage;
    }

    public UndoMove()
    {
        changed = new List<UndoMovedPiece>();
        changedStorage = new List<UndoStorage>();
        changedAbilities = new List<UndoAbility>();
        changedStates = new List<UndoState>();
        actions = new List<UndoPieceAction>();
        undoRevertFull = new List<UndoRevertFull>();
        undoDelayedQueueAction = new List<UndoDelayedQueueAction>();
    }

    public void addMove(UndoMovedPiece undoMovedPiece)
    {
        changed.Add(undoMovedPiece);
    }

    public void addStorage(UndoStorage undoStorage)
    {
        changedStorage.Add(undoStorage);
    }

    public void addState(UndoState undoState)
    {
        changedStates.Add(undoState);
    }

    public void addAbility(UndoAbility undoAbility)
    {
        changedAbilities.Add(undoAbility);
    }

    public void addAction(UndoPieceAction undoAction)
    {
        actions.Add(undoAction);
    }

    public void addRevertFull(UndoRevertFull undoRevert)
    {
        undoRevertFull.Add(undoRevert);
    }

    public void addDelayedQueueAction(UndoDelayedQueueAction undoDelayedQueueAction_)
    {
        undoDelayedQueueAction.Add(undoDelayedQueueAction_);
    }
}

public class UndoMoveBotHelperFunctions : MonoBehaviour
{
    public static void undoMove(UndoMove undo, BoardState bs)
    {

        //UndoMovedPiece
        List<UndoMovedPiece> undoMovedPieces = undo.changed;

        foreach (UndoMovedPiece ump in undoMovedPieces)
        {
            Piece p = ump.p;
            coords initialPosition = ump.initialPosition;
            coords newPosition = ump.newPosition;
            bool dead = ump.dead;
            bool spawned = ump.spawned;
            bool revertHasMoved = ump.revertHasMoved;

            if (dead)
            {
                updateBoardState(new int[] { initialPosition.x - 1, initialPosition.y - 1 }, p, "a", bs);
            }
            else
            {
                updateBoardState(new int[] { newPosition.x - 1, newPosition.y - 1 }, p, "r", bs);
                updateBoardState(new int[] { initialPosition.x - 1, initialPosition.y - 1 }, p, "a", bs);
            }

            if (spawned)
            {
                updateBoardState(new int[] { newPosition.x - 1, newPosition.y - 1 }, p, "r", bs);

                if (p.go != null)
                {
                    Destroy(p.go);
                }
            }

            if (revertHasMoved)
            {
                p.hasMoved = false;
            }
        }

        //UndoStorage
        List<UndoStorage> undoStorages = undo.changedStorage;
        foreach (UndoStorage us in undoStorages)
        {
            Piece piece = us.piece;
            Piece storedPiece = us.storedPiece;
            bool undo_ = us.undo; //true = undo, false = redo (ie. for simulated Vomit)
            coords coords = us.coords;

            if (undo_)
            {
                piece.storage.Remove(storedPiece);

                updateBoardState(new int[] { coords.x - 1, coords.y - 1 }, storedPiece, "a", bs);
            }
            else
            {
                piece.storage.Add(storedPiece);

                updateBoardState(new int[] { coords.x - 1, coords.y - 1 }, storedPiece, "r", bs);
            }
        }

        //UndoPieceAction
        List<UndoPieceAction> pieceActions = undo.actions;
        foreach (UndoPieceAction a in pieceActions)
        {
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

        //UndoState
        List<UndoState> pieceStates = undo.changedStates;
        foreach (UndoState s in pieceStates)
        {
            Piece piece = s.piece;
            PieceState state = s.state;
            bool add = s.add; //True if you should add WHILE undoing the move;

            if (add)
            {
                addState(piece, state);
            }
            else
            {
                removeState(piece, state);
            }
        }

        //UndoAbility
        List<UndoAbility> pieceAbilities = undo.changedAbilities;
        foreach (UndoAbility a in pieceAbilities)
        {
            Piece piece = a.piece;
            string ability = a.ability;
            bool add = a.add; //True if you should add WHILE undoing the move;

            if (add)
            {
                addAbility(piece, ability);
            }
            else
            {
                removeAbility(piece, ability);
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
        undo.initialPosition = new coords(piece.position[0], piece.position[1]);

        int[] position = new int[] { piece.position[0] - 1, piece.position[1] - 1 };

        updateBoardState(position, piece, "r", bs);
        updateBoardState(new int[] { coords.x, coords.y }, piece, "a", bs);
        piece.position = new int[] { coords.x + 1, coords.y + 1 };
        undo.newPosition = new coords(coords.x + 1, coords.y + 1);

        if (!piece.hasMoved)
        {
            piece.hasMoved = true;
            undo.revertHasMoved = true;
        }

        return undo;
    }

    public static UndoMove undo_simulatePieceAbility(BotTemplate bot, BoardState bs_, PieceAbility pieceAbility)
    {
        // Init undo move
        UndoMove undo = new UndoMove();

        Piece piece = pieceAbility.piece;
        string ability = pieceAbility.ability;

        int[] coords = new int[] { pieceAbility.coords[0], pieceAbility.coords[1] };
        int[] adjustedCoords = new int[] { coords[0] - 1, coords[1] - 1 };
        int[] adjustedPiecePosition = new int[] { piece.position[0] - 1, piece.position[1] - 1 };

        List<Piece> placePieces = pieceAbility.placePieces;
        List<int[]> placeCoords = pieceAbility.placeCoords;
        Piece secondPiece = pieceAbility.secondPiece;

        System.Random rand = new System.Random();

        if (ability == "Vomit")
        {
            int numPieces = placePieces.Count;
            int numCoords = placeCoords.Count;

            if (numPieces >= numCoords)
            {
                foreach (int[] coords_ in placeCoords)
                {
                    int idx = rand.Next(numPieces);
                    numPieces--;

                    Piece p_ = placePieces[idx];
                    placePieces.Remove(p_);

                    int[] coords__ = new int[] { coords_[0] - 1, coords_[1] - 1 };

                    //Track this update with an UndoStorage
                    UndoStorage barf = new UndoStorage(piece, p_, true, new coords(coords_[0], coords_[1]));
                    undo.addStorage(barf);

                    updateBoardState(coords__, p_, "a", bs_);

                    piece.storage.Remove(p_);
                }
            }
            else
            {
                foreach (Piece p_ in placePieces)
                {
                    int idx = rand.Next(numCoords);
                    numCoords--;

                    int[] c_ = placeCoords[idx];
                    placeCoords.Remove(c_);

                    c_ = new int[] { c_[0] - 1, c_[1] - 1 };

                    //Track this update with an UndoStorage
                    UndoStorage barf = new UndoStorage(piece, p_, true, new coords(c_[0], c_[1]));
                    undo.addStorage(barf);

                    updateBoardState(c_, p_, "a", bs_);

                    piece.storage.Remove(p_);
                }
            }
        }
        else if (ability == "CastleLeft")
        {
            int[] kingCoords = coords;
            int[] rookCoords = new int[] { kingCoords[0] + 1, kingCoords[1] };

            coords adjustedKingCoords = new coords ( kingCoords[0] - 1, kingCoords[1] - 1 );
            coords adjustedRookCoords = new coords( rookCoords[0] - 1, rookCoords[1] - 1 );

            //King
            UndoMovedPiece kingMove = undo_movePieceBoardState(piece, adjustedKingCoords, bs_);
            undo.addMove(kingMove);
            //Rook
            UndoMovedPiece rookMove = undo_movePieceBoardState(secondPiece, adjustedRookCoords, bs_);
            undo.addMove(rookMove);

            if (piece.ability.Contains("CastleLeft"))
            {
                UndoAbility castleLeft = new UndoAbility(piece, "CastleLeft", true);
                undo.addAbility(castleLeft);
                HelperFunctions.removeAbility(piece, "CastleLeft");
            }

            if (piece.ability.Contains("CastleRight"))
            {
                UndoAbility castleRight = new UndoAbility(piece, "CastleRight", true);
                undo.addAbility(castleRight);
                HelperFunctions.removeAbility(piece, "CastleRight");
            }
        }
        else if (ability == "CastleRight")
        {
            int[] kingCoords = coords;
            int[] rookCoords = new int[] { kingCoords[0] - 1, kingCoords[1] };

            coords adjustedKingCoords = new coords(kingCoords[0] - 1, kingCoords[1] - 1);
            coords adjustedRookCoords = new coords(rookCoords[0] - 1, rookCoords[1] - 1);

            //King
            UndoMovedPiece kingMove = undo_movePieceBoardState(piece, adjustedKingCoords, bs_);
            undo.addMove(kingMove);
            //Rook
            UndoMovedPiece rookMove = undo_movePieceBoardState(secondPiece, adjustedRookCoords, bs_);
            undo.addMove(rookMove);

            if (piece.ability.Contains("CastleLeft"))
            {
                UndoAbility castleLeft = new UndoAbility(piece, "CastleLeft", true);
                undo.addAbility(castleLeft);
                HelperFunctions.removeAbility(piece, "CastleLeft");
            }

            if (piece.ability.Contains("CastleRight"))
            {
                UndoAbility castleRight = new UndoAbility(piece, "CastleRight", true);
                undo.addAbility(castleRight);
                HelperFunctions.removeAbility(piece, "CastleRight");
            }
        }
        else if (ability == "Freeze")
        {
            HelperFunctions.addState(secondPiece, PieceState.Frozen);
            UndoState pieceFreeze = new UndoState(secondPiece, PieceState.Frozen, false);
            undo.addState(pieceFreeze);

            HelperFunctions.addAbility(secondPiece, "Unfreeze");
            UndoAbility pieceUnfreeze = new UndoAbility(secondPiece, "Unfreeze", false);
            undo.addAbility(pieceUnfreeze);
        }
        else if (ability == "Unfreeze")
        {
            HelperFunctions.removeAbility(piece, "Unfreeze");
            UndoAbility pieceUnfreeze = new UndoAbility(piece, "Unfreeze", true);
            undo.addAbility(pieceUnfreeze);

            HelperFunctions.removeState(piece, PieceState.Frozen);
            UndoState pieceFreeze = new UndoState(piece, PieceState.Frozen, true);
            undo.addState(pieceFreeze);
        }
        else if (ability == "Spawn")
        {
            Piece spawned = HelperFunctions.Spawnables.create(piece.spawnable, piece.color);
            piece.numSpawns--;
            UndoPieceAction incSpawn = new UndoPieceAction(piece, PieceAction.IncrementSpawn);
            undo.addAction(incSpawn);
            if (piece.numSpawns <= 0)
            {
                UndoAbility pieceSpawn = new UndoAbility(piece, "Spawn", true);
                undo.addAbility(pieceSpawn);
                HelperFunctions.removeAbility(piece, "Spawn");
            }
            Destroy(spawned.go);

            UndoMovedPiece mp = new UndoMovedPiece(spawned, new coords(-1, -1), new coords(coords[0], coords[1]), false, true, false);
            updateBoardState(adjustedCoords, spawned, "a", bs_);
        }
        else if (ability == "Spit")
        {
            undo_isolatedCollateralDeath(isolatedGetPiecesOnCoordsBoardGrid(adjustedCoords[0], adjustedCoords[1], bs_.boardGrid, false), bs_, undo);

            UndoStorage spit = new UndoStorage(piece, secondPiece, true, new coords(coords[0], coords[1]));
            undo.addStorage(spit);

            updateBoardState(adjustedCoords, secondPiece, "a", bs_);
            piece.storage.Remove(secondPiece);
        }
        else if (ability == "Dematerialize")
        {
            UndoState pieceState = new UndoState(piece, PieceState.Dematerialized, false);
            undo.addState(pieceState);
            HelperFunctions.addState(piece, PieceState.Dematerialized);

            UndoAbility pieceDematerialize = new UndoAbility(piece, "Dematerialize", true);
            undo.addAbility(pieceDematerialize);
            HelperFunctions.removeAbility(piece, "Dematerialize");

            UndoAbility pieceMaterialize = new UndoAbility(piece, "Materialize", false);
            undo.addAbility(pieceMaterialize);
            HelperFunctions.addAbility(piece, "Materialize");
        }
        else if (ability == "Materialize")
        {
            UndoState pieceState = new UndoState(piece, PieceState.Dematerialized, true);
            undo.addState(pieceState);
            HelperFunctions.removeState(piece, PieceState.Dematerialized);

            UndoAbility pieceDematerialize = new UndoAbility(piece, "Dematerialize", false);
            undo.addAbility(pieceDematerialize);
            HelperFunctions.addAbility(piece, "Dematerialize");

            UndoAbility pieceMaterialize = new UndoAbility(piece, "Materialize", true);
            undo.addAbility(pieceMaterialize);
            HelperFunctions.removeAbility(piece, "Materialize");

            undo_isolatedOnDeathsDontIncludeAttacker(piece, coords, bs_, undo);

            undo_isolatedCheckPromote(piece, bs_, undo);
        }
        else if (ability == "Split")
        {
            HelperFunctions.removeAbility(piece, "Split");

            undo_isolatedRemovePiece(piece, bs_, undo);

            Piece leftPawn = HelperFunctions.Spawnables.create("LeftPawn", piece.color);
            Destroy(leftPawn.go);
            leftPawn.position = new int[] { piece.position[0], piece.position[1] };
            undo_isolatedAddPiece(leftPawn, bs_, undo);

            Piece rightPawn = HelperFunctions.Spawnables.create("RightPawn", piece.color);
            Destroy(rightPawn.go);
            rightPawn.position = new int[] { piece.position[0], piece.position[1] };
            undo_isolatedAddPiece(rightPawn, bs_, undo);
        }

        return undo;
    }

    public static void undo_isolatedCheckPromote(Piece piece, BoardState bs, UndoMove undo)
    {
        if (piece.promotesInto != "" && !HelperFunctions.checkState(piece, PieceState.Dematerialized))
        {
            if (piece.position[1] == piece.promotingRow)
            {
                string pname = piece.promotesInto;
                Piece p = HelperFunctions.Spawnables.create(pname, piece.color);
                Destroy(p.go);
                undo_isolatedCollateralDeath(isolatedGetPiecesOnCoordsBoardGrid(piece.position[0] - 1, piece.position[1] - 1, bs.boardGrid, false), bs, undo);

                undo_isolatedRemovePiece(piece, bs, undo);
                p.position = new int[] { piece.position[0], +piece.position[1] };
                undo_isolatedAddPiece(p, bs, undo);
            }
        }
    }

    public static void undo_isolatedCollateralDeath(List<Piece> deadPieces, BoardState bs, UndoMove undo)
    {
        foreach (Piece deadPiece in new List<Piece>(deadPieces))
        {
            if (HelperFunctions.checkState(deadPiece, PieceState.Shield) || HelperFunctions.checkCaptureTheFlag(deadPiece))
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
                UndoMovedPiece deadPiece_ = new UndoMovedPiece(deadPiece, new coords(deadPiece.position[0], deadPiece.position[1]), new coords(-1, -1), true, false, false);
                undo.addMove(deadPiece_);
                updateBoardState(new int[] { deadPiece.position[0] - 1, deadPiece.position[1] - 1 }, deadPiece, "r", bs);

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

        if (!HelperFunctions.isOnStartSquare(deadPiece) && !isolatedIsPieceOnStartSquare(deadPiece, bs))
        {
            UndoMovedPiece undoMove = undo_movePieceBoardState(deadPiece, new coords( deadPiece.startSquare[0] - 1, deadPiece.startSquare[1] - 1 ), bs);
            undo.addMove(undoMove);
        }
        else
        {
            UndoMovedPiece deadPiece_ = new UndoMovedPiece(deadPiece, new coords(deadPiece.position[0], deadPiece.position[1]), new coords(-1, -1), true, false, false);
            undo.addMove(deadPiece_);
            updateBoardState(new int[] { deadPiece.position[0] - 1, deadPiece.position[1] - 1 }, deadPiece, "r", bs);
        }
    }

    public static PieceState undo_isolatedOnDeathsDontIncludeAttacker(Piece attacker, int[] deadCoords, BoardState bs, UndoMove undo)
    {
        List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords[0], deadCoords[1], bs.boardGrid, false));
        pieces.Remove(attacker);

        PieceState ps = PieceState.None;

        foreach (Piece piece in pieces)
        {
            if (!HelperFunctions.checkState(piece, PieceState.Dematerialized))
            {
                var onDeathVars = undo_isolatedOnDeath(piece, attacker, bs, undo);
                ps |= onDeathVars.stackingStates;
            }
        }

        return ps;
    }

    public static (PieceState stackingStates, bool attackerDied) undo_isolatedOnDeath(Piece deadPiece, Piece attackerPiece, BoardState bs, UndoMove undo)
    {
        int[] attackerCoords = attackerPiece.position;
        int[] deadPieceCoords = deadPiece.position;

        int[] adjustedAttackerCoords = new int[] { attackerPiece.position[0] - 1, attackerPiece.position[1] - 1 };
        int[] adjustedDeadPieceCoords = new int[] { deadPiece.position[0] - 1, deadPiece.position[1] - 1 };

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
        if (HelperFunctions.checkState(attackerPiece, PieceState.Hungry))
        {
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            attackerPiece.storage.Add(deadPiece);
            UndoStorage store = new UndoStorage(attackerPiece, deadPiece, true, new coords(deadPieceCoords[0], deadPieceCoords[1]));
            undo.addStorage(store);

            skipCollateral = true;
            undo_isolatedRemovePiece(deadPiece, bs, undo);

            return (PieceState.None, false);
        }

        if (HelperFunctions.checkState(deadPiece, PieceState.Hungry))
        {
            if (deadPiece.storage != null && deadPiece.storage.Count > 0)
            {
                List<int[]> placeCoords_ = isolatedGetCollateralSquares(deadPiece, bs);
                List<Piece> placePieces_ = deadPiece.storage;

                PieceAbility pa = new PieceAbility(deadPiece, "Vomit", deadPiece.position, placePieces_, placeCoords_, null);
                //Simulate Ability

                List<Piece> placePieces = pa.placePieces;
                List<int[]> placeCoords = pa.placeCoords;

                int numPieces = placePieces.Count;
                int numCoords = placeCoords.Count;

                if (numPieces >= numCoords)
                {
                    foreach (int[] coords_ in placeCoords)
                    {
                        int idx = rand.Next(numPieces);
                        numPieces--;

                        Piece p_ = placePieces[idx];
                        placePieces.Remove(p_);

                        int[] coords__ = new int[] { coords_[0] - 1, coords_[1] - 1 };

                        //Track this update with an UndoStorage
                        UndoStorage barf = new UndoStorage(deadPiece, p_, true, new coords(coords_[0], coords_[1]));
                        undo.addStorage(barf);

                        updateBoardState(coords__, p_, "a", bs);

                        deadPiece.storage.Remove(p_);
                    }
                }
                else
                {
                    foreach (Piece p_ in placePieces)
                    {
                        int idx = rand.Next(numCoords);
                        numCoords--;

                        int[] c_ = placeCoords[idx];
                        placeCoords.Remove(c_);

                        c_ = new int[] { c_[0] - 1, c_[1] - 1 };

                        //Track this update with an UndoStorage
                        UndoStorage barf = new UndoStorage(deadPiece, p_, true, new coords(c_[0], c_[1]));
                        undo.addStorage(barf);

                        updateBoardState(c_, p_, "a", bs);

                        deadPiece.storage.Remove(p_);
                    }
                }
            }
        }

        //Spitting
        if (HelperFunctions.checkState(attackerPiece, PieceState.Spitting))
        {
            if (attackerPiece.storage == null)
            {
                attackerPiece.storage = new List<Piece>();
            }

            if (attackerPiece.storage.Count < attackerPiece.storageLimit)
            {
                UndoStorage us = new UndoStorage(attackerPiece, deadPiece, false, new coords(deadPieceCoords[0], deadPieceCoords[1]));
                undo.addStorage(us);

                attackerPiece.storage.Add(deadPiece);
                skipCollateral = true;
                undo_isolatedRemovePiece(deadPiece, bs, undo);

            }
            else
            {
                List<Piece> piece = HelperFunctions.pieceToList(deadPiece);
                undo_isolatedCollateralDeath(piece, bs, undo);
            }

            return (PieceState.None, false);
        }

        PieceState stackingStates = PieceState.None;
        //Stacking
        if (HelperFunctions.checkState(attackerPiece, PieceState.Stacking) && deadPiece.lives == 0)
        {
            Piece original = attackerPiece;
            Piece clone = HelperFunctions.clonePiece(original);

            stackingStates |= deadPiece.states;

            string ability = deadPiece.ability;
            string[] abilityParts = ability.Split('-');

            foreach (string abilityPart in abilityParts)
            {
                if (!original.ability.Contains(abilityPart))
                {
                    HelperFunctions.addAbility(clone, abilityPart);
                }
            }

            //Moves
            int[,] moves = HelperFunctions.combineMoveSets(original.moves, deadPiece.moves);
            int[,] oneTimeMoves = HelperFunctions.combineMoveSets(original.oneTimeMoves, deadPiece.oneTimeMoves);
            int[,] moveAndAttacks = HelperFunctions.combineMoveSets(original.moveAndAttacks, deadPiece.moveAndAttacks);
            int[,] oneTimeMoveAndAttacks = HelperFunctions.combineMoveSets(original.oneTimeMoveAndAttacks, deadPiece.oneTimeMoveAndAttacks);
            int[,] murderousAttacks = HelperFunctions.combineMoveSets(original.murderousAttacks, deadPiece.murderousAttacks);
            int[,] conditionalAttacks = HelperFunctions.combineMoveSets(original.conditionalAttacks, deadPiece.conditionalAttacks);
            int[,] attacks = HelperFunctions.combineMoveSets(original.attacks, deadPiece.attacks);
            int[,] jumpAttacks = HelperFunctions.combineMoveSets(original.jumpAttacks, deadPiece.jumpAttacks);
            int[,] flagMove1 = HelperFunctions.combineMoveSets(original.flagMove1, deadPiece.flagMove1);
            int[,] flagMove2 = HelperFunctions.combineMoveSets(original.flagMove2, deadPiece.flagMove2);
            int[,] pushMoves = HelperFunctions.combineMoveSets(original.pushMoves, deadPiece.pushMoves);
            int[,] enPassantMoves = HelperFunctions.combineMoveSets(original.enPassantMoves, deadPiece.enPassantMoves);

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

            //UndoRevertFull undoClone = new UndoRevertFull(original, clone);
            //undo.addRevertFull(undoClone);
            
        }

        //Jailer
        if (HelperFunctions.checkState(attackerPiece, PieceState.Jailer))
        {
            UndoState us = new UndoState(deadPiece, PieceState.Jailed, false);
            undo.addState(us);

            HelperFunctions.addState(deadPiece, PieceState.Jailed);

            return (PieceState.None, false);
        }

        //Crook
        if (HelperFunctions.checkState(deadPiece, PieceState.Crook) && deadPiece.color != attackerPiece.color)
        {
            UndoState us = new UndoState(deadPiece, PieceState.Jailed, false);
            undo.addState(us);

            HelperFunctions.addState(deadPiece, PieceState.Jailed);

            return (PieceState.None, false);
        }

        //Medusa
        if (HelperFunctions.checkState(attackerPiece, PieceState.Medusa))
        {
            if (attackerPiece.numSpawns != 0)
            {
                attackerPiece.numSpawns--;
                UndoPieceAction upa_ = new UndoPieceAction(attackerPiece, PieceAction.IncrementSpawn);
                undo.addAction(upa_);

                undo_isolatedRemovePiece(deadPiece, bs, undo);

                Piece shieldPawn = HelperFunctions.Spawnables.create("ShieldPawn", attackerPiece.color * -1);
                Destroy(shieldPawn.go);

                undo_isolatedAddPiece(shieldPawn, bs, undo);
            }
        }

        if (!skipCollateral)
        {
            if (attackerPiece.collateralType == 0)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, PieceState.Defuser, bs))
                {
                    undo_isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs, undo);
                    undo_isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs, undo);
                    attackerDied = true;
                    return (stackingStates, attackerDied);
                }

                for (int i = 0; i < attackerPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { adjustedDeadPieceCoords[0] + attackerPiece.collateral[i, 0], adjustedDeadPieceCoords[1] + attackerPiece.collateral[i, 1] };

                    if (attackerPiece.collateral[i, 0] == 0 && attackerPiece.collateral[i, 1] == 0)
                    {
                        undo_isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs, undo);
                        attackerDied = true;
                    }

                    List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, false));
                    undo_isolatedCollateralDeath(pieces, bs, undo);
                }
            }

            if (deadPiece.collateralType == 1)
            {
                if (isolatedIsPieceSurroundingState(deadPiece, PieceState.Defuser, bs))
                {
                    undo_isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs, undo);
                    attackerDied = true;
                    return (stackingStates, attackerDied);
                }

                for (int i = 0; i < deadPiece.collateral.GetLength(0); i++)
                {
                    int[] coords = new int[] { adjustedDeadPieceCoords[0] + deadPiece.collateral[i, 0], adjustedDeadPieceCoords[1] + deadPiece.collateral[i, 1] };

                    if (deadPiece.collateral[i, 0] == 0 && deadPiece.collateral[i, 1] == 0)
                    {
                        undo_isolatedCollateralDeath(HelperFunctions.pieceToList(deadPiece), bs, undo);
                        undo_isolatedCollateralDeath(HelperFunctions.pieceToList(attackerPiece), bs, undo);
                        attackerDied = true;
                    }

                    List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, false));
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
        UndoMovedPiece ump = new UndoMovedPiece(p, new coords(p.position[0], p.position[1]), new coords(-1, -1), true, false, false);
        undo.addMove(ump);
        updateBoardState(new int[] { p.position[0] - 1, p.position[1] - 1 }, p, "r", bs);
    }

    public static void undo_isolatedAddPiece(Piece p, BoardState bs, UndoMove undo)
    {
        UndoMovedPiece ump = new UndoMovedPiece(p, new coords(p.position[0], p.position[1]), new coords(-1, -1), false, true, false);
        undo.addMove(ump);
        updateBoardState(new int[] { p.position[0] - 1, p.position[1] - 1 }, p, "a", bs);
    }

    public static UndoMove undo_simulatePieceMove(BotTemplate bot, BoardState bs, Piece piece, coords coords)
    {
        // Init undo move
        UndoMove undo = new UndoMove();

        Piece botKing = isolatedGetKing(bs, bot.color);
        Piece oppKing = isolatedGetKing(bs, bot.color * -1);

        coords = new coords(coords.x - 1, coords.y - 1);

        List<Piece> piecesOnCoordsPreDeath = isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid, false);
        bool death = isolatedIsDeath(piecesOnCoordsPreDeath, piece);

        if (death)
        {
            Piece destroyer = piece;
            undo_isolatedOnDeaths(destroyer, coords, bs, undo);
        }

        //todo attackerDied


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

        if (HelperFunctions.checkState(piece, PieceState.Delayed))
        {
            PieceMove delayedMove = new PieceMove(piece, new int[] { coords.x, coords.y }, 2);
            bs.delayedQueue.Enqueue(delayedMove);

            UndoDelayedQueueAction undoDQ = new UndoDelayedQueueAction(delayedMove, PieceAction.RemoveFromQueue);
            undo.addDelayedQueueAction(undoDQ);

            return undo;
        }

        List<Piece> piecesOnSquare = isolatedGetPiecesOnCoordsBoardGrid(piece.position[0] - 1, piece.position[1] - 1, bs.boardGrid, false);
        foreach (Piece pieceOnSquare in piecesOnSquare)
        {
            if (HelperFunctions.checkState(pieceOnSquare, PieceState.Crook))
            {
                if (piecesOnSquare.Count == 2)
                {
                    HelperFunctions.removeState(pieceOnSquare, PieceState.Jailed);
                    UndoState us = new UndoState(pieceOnSquare, PieceState.Jailed, true);
                    undo.addState(us);
                }
            }

            if (HelperFunctions.checkState(piece, PieceState.Jailer))
            {
                HelperFunctions.removeState(pieceOnSquare, PieceState.Jailed);
                UndoState us = new UndoState(pieceOnSquare, PieceState.Jailed, true);
                undo.addState(us);
            }
        }

        int[] originalCoords = { piece.position[0], piece.position[1] };
        if (!tempInfo.attackerDied)
        {
            UndoMovedPiece mp = undo_movePieceBoardState(piece, coords, bs);
            undo.addMove(mp);
        }
        else
        {
            undo_isolatedRemovePiece(piece, bs, undo);
        }

        List<Piece> piecesOnSquare2 = isolatedGetPiecesOnCoordsBoardGrid(originalCoords[0] - 1, originalCoords[1] - 1, bs.boardGrid, false);
        if (HelperFunctions.checkState(piece, PieceState.Piggyback))
        {
            foreach (Piece pieceOnSquare in new List<Piece>(piecesOnSquare2))
            {
                if (pieceOnSquare.color == piece.color)
                {
                    UndoMovedPiece pm = undo_movePieceBoardState(pieceOnSquare, coords, bs);
                    undo.addMove(pm);
                    pieceOnSquare.hasMoved = true;
                }
            }
        }


        return undo;
    }

    public static void undo_isolatedDelayedMove(PieceMove pMove, BoardState bs, UndoMove undo)
    {
        Piece piece = pMove.piece;
        int[] coords = pMove.coords;

        List<Piece> piecesOnCoords = isolatedGetPiecesOnCoordsBoardGrid(coords[0], coords[1], bs.boardGrid, false);

        if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color)*/!isolatedIsColorOnCoords(piecesOnCoords, true, piece.color))
        {
            bool death = false;

            if (piecesOnCoords.Count != 0)
            {
                death = true;

                //Debug.LogWarning("Checking for death");

                if (/*!isolatedGetColorsOnCoords(piecesOnCoords, true).Contains(piece.color * -1)*/!isolatedIsColorOnCoords(piecesOnCoords, true, piece.color * -1) && !HelperFunctions.checkState(piece, PieceState.Murderous))
                {
                    death = false;
                }
                else if (HelperFunctions.checkStateAllOnSquare(piecesOnCoords, PieceState.Dematerialized))
                {
                    death = false;
                }
                else if (isolatedCheckSquareCrowdingEligible(piece, piecesOnCoords))
                {
                    death = false;
                }
                else if (HelperFunctions.checkState(piece, PieceState.Dematerialized))
                {
                    death = false;
                }
            }

            if (death)
            {
                undo_isolatedOnDeaths(piece, new coords(coords[0], coords[1]), bs, undo);
            }

            piece.hasMoved = true;
            UndoMovedPiece undoMovedPiece = undo_movePieceBoardState(piece, new coords(coords[0], coords[1]), bs);
            undo.addMove(undoMovedPiece);
        }
    }

    public static PieceState undo_isolatedOnDeaths(Piece attacker, coords deadCoords, BoardState bs, UndoMove undo)
    {
        List<Piece> pieces = new List<Piece>(isolatedGetPiecesOnCoordsBoardGrid(deadCoords.x, deadCoords.y, bs.boardGrid, false));

        PieceState stacks = PieceState.None;
        foreach (Piece piece in pieces)
        {
            //Debug.Log(piece.name + " died on (" + piece.position[0] + "," + piece.position[1] + ") during a simulated move");
            if (!HelperFunctions.checkState(piece, PieceState.Dematerialized))
            {
                var onDeathVars = undo_isolatedOnDeath(piece, attacker, bs, undo);
                stacks = onDeathVars.stackingStates;
            }
        }

        return stacks;
    }
}
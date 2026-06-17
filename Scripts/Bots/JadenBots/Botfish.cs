using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;

/*
public class Botfish : BotTemplate
{
    public Botfish(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Botfish";

        choosePieces();
    }

    override
    public NextMove nextMove()
    {
        Botfish_BoardState botfishBS = Botfish_FixBoardState(this.currentBoardState);
        List<NextMove> validMoves = new List<NextMove>();

        List<NextMove> allMoves = Botfish_getAllBotMoves(botfishBS, this.color);
        //Todo game logic

        //Todo if botfish returns no moves, choose random move
        System.Random rand = new System.Random();
        int rndIdx = rand.Next(validMoves.Count);

        NextMove move = validMoves[rndIdx];
        return move;
    }

    public static List<NextMove> Botfish_getAllBotMoves(Botfish_BoardState bs, int color) {
        List<NextMove> moves = new List<NextMove>();

        List<BotfishPiece> pieces = bs.allPieces;

        for (int i = 0; i < pieces.Count; i++) {
            BotfishPiece piece = pieces[i];

            if (piece.color != color) {
                continue;
            }

            coords[] moves_ = Botfish_getIsolatedStatePieceMoves(piece, bs);

            if (moves_ != null && moves_.Length > 0)
            {
                foreach (coords move in moves_) {
                    Move mv = new Move(piece, move);

                    moves.Add(new NextMove(mv));
                }
            }
        }

        return moves;
    }

    public static List<BotfishPiece> Botfish_isolatedGetPiecesOnCoordsBoardGrid(int x, int y, List<BotfishPiece>[,] boardGrid)
    {
        if (x > 7 || y > 7 || x < 0 || y < 0)
        {
            return new List<BotfishPiece>();
        }

        List<BotfishPiece> pieces;

        pieces = boardGrid[x, y];

        return pieces;
    }

    public static coords[] Botfish_getIsolatedStatePieceMoves(BotfishPiece p, Botfish_BoardState bs) {
        coords[] allMoves = new coords[32];
        int acceptedMoves = 0;

        int useDirections = 0;
        (int dx, int dy)[] directions;
        coords[] moves;

        directions = p.directions;
        moves = p.moves;

        if (p.baseType == "Queen")
        {
            useDirections = 1;
        }
        else if (p.baseType == "Rook")
        {
            useDirections = 1;
        }
        else if (p.baseType == "Bishop")
        {
            useDirections = 1;
        }
        else if (p.baseType == "Pawn") {  
            useDirections = 2;
        }

        int x = p.position.x - 1; //0-based
        int y = p.position.y - 1;

        if (useDirections == 1) {

            foreach (var direction in directions)
            {
                int dx = direction.dx;
                int dy = direction.dy;

                int newPosX = x + dx;
                int newPosY = y + dy;

                List<BotfishPiece> piecesOnCoords = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosX, newPosY, bs.boardGrid);

                bool pieceIsNull = true;
                bool pieceIsDiffColour = false;

                if (piecesOnCoords.Count > 0) {
                    pieceIsNull = false;

                    foreach(BotfishPiece pieceOnCoords in piecesOnCoords) {
                        if (pieceOnCoords.color != p.color) {
                            pieceIsDiffColour = true;
                            break;
                        }
                    }
                }

                if (pieceIsNull) {
                    allMoves[acceptedMoves] = new coords(newPosX + 1, newPosY + 1);
                    acceptedMoves++;
                }

                if (!pieceIsNull && pieceIsDiffColour) {
                    allMoves[acceptedMoves] = new coords(newPosX + 1, newPosY + 1);
                    acceptedMoves++;

                    break;
                }
                else if (!pieceIsNull) {
                    break;
                }
            }
        }
        else if (useDirections == 0) {
            foreach(coords move in moves) {
                int newPosX = x + move.x;
                int newPosY = y + move.y;

                List<BotfishPiece> piecesOnCoords = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosX, newPosY, bs.boardGrid);

                bool pieceIsNull = true;
                bool pieceIsDiffColour = false;

                if (piecesOnCoords.Count > 0) {
                    pieceIsNull = false;

                    foreach(BotfishPiece pieceOnCoords in piecesOnCoords) {
                        if (pieceOnCoords.color != p.color) {
                            pieceIsDiffColour = true;
                            break;
                        }
                    }
                }

                if (pieceIsNull || (!pieceIsNull && pieceIsDiffColour)) {
                    allMoves[acceptedMoves] = new coords(newPosX + 1, newPosY + 1);
                    acceptedMoves++;
                }
            }
        } else {
            //Pawn

            if (!p.hasMoved) {
                int newPosX = x + 0;
                int newPosY = y + 2 * p.color;
                int newPosYJump = y + 1 * p.color;

                List<BotfishPiece> piecesOnCoords = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosX, newPosY, bs.boardGrid);

                bool pieceIsNull = true;
                bool pieceIsDiffColour = false;

                if (piecesOnCoords.Count > 0) {
                    pieceIsNull = false;

                    foreach(BotfishPiece pieceOnCoords in piecesOnCoords) {
                        if (pieceOnCoords.color != p.color) {
                            pieceIsDiffColour = true;
                            break;
                        }
                    }
                }

                if (pieceIsNull || (!pieceIsNull && pieceIsDiffColour)) {
                    // Is Jump

                    List<BotfishPiece> piecesOnCoordsJump = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosX, newPosYJump, bs.boardGrid);
                    if (piecesOnCoordsJump.Count == 0) {
                        allMoves[acceptedMoves] = new coords(newPosX + 1, newPosYJump + 1);
                        acceptedMoves++;
                    }
                }
            }

            //Move
            int newPosXMove = x + 0;
            int newPosYMove = y + 1 * p.color;

            List<BotfishPiece> piecesOnCoordsMove = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosXMove, newPosYMove, bs.boardGrid);
            if (piecesOnCoordsMove.Count == 0) {
                allMoves[acceptedMoves] = new coords(newPosXMove + 1, newPosYMove + 1);
                acceptedMoves++;
            }

            int newPosXAttack;
            //Attacks
            int[] dirs = new int[] { -1, 1 };
            foreach(int dir in dirs) {
                newPosXAttack = x + dir;

                List<BotfishPiece> piecesOnCoordsAttack = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosXAttack, newPosYMove, bs.boardGrid);

                bool pieceIsNull = true;
                bool pieceIsDiffColour = false;

                if (piecesOnCoordsAttack.Count > 0) {
                    pieceIsNull = false;

                    foreach(BotfishPiece pieceOnCoordsAttack in piecesOnCoordsAttack) {
                        if (pieceOnCoordsAttack.color != p.color) {
                            pieceIsDiffColour = true;
                            break;
                        }
                    }
                }

                if (!pieceIsNull && pieceIsDiffColour) {
                    allMoves[acceptedMoves] = new coords(newPosXAttack + 1, newPosYMove + 1);
                    acceptedMoves++;
                }
            }
        }

        return allMoves;
    }

    public static List<BotfishPiece> getBotFishPieces(BoardState bs, int color) {
        List<Piece> pieces = getPiecesOnBoardState(bs, color);
        List<BotfishPiece> botFishPieces = new List<BotfishPiece>();

        foreach(Piece p in pieces) {
            botFishPieces.Add(fixBotfishPiece(p));
        }

        return botFishPieces;
    }

    public static BotfishPiece fixBotfishPiece(Piece p) {
        if (p.baseType == "Queen") {
            return new Botfish_Queen(p.color, p.hasMoved);
        }
        else if (p.baseType == "King") {
            return new Botfish_King(p.color, p.hasMoved);
        }
        else if (p.baseType == "Bishop") {
            return new Botfish_Bishop(p.color, p.hasMoved);
        }
        else if (p.baseType == "Knight") {
            return new Botfish_Knight(p.color, p.hasMoved);
        }
        else if (p.baseType == "Rook") {
            return new Botfish_Rook(p.color, p.hasMoved);
        }
        else if (p.baseType == "Pawn") {
            return new Botfish_Pawn(p.color, p.hasMoved);
        }
        else {
            return new Botfish_Queen(p.color, p.hasMoved);
        }
    }

    public class Botfish_BoardState {
        public List<BotfishPiece>[,] boardGrid = new List<BotfishPiece>[8, 8];
        public List<BotfishPiece> allPieces = new List<BotfishPiece>();

        public float whitePointsOnBoard { get; set; } = 0;
        public float blackPointsOnBoard { get; set; } = 0;

        public Botfish_BoardState() {
            //refresh();
        }
    }

    public Botfish_BoardState Botfish_FixBoardState(BoardState bs) {
        Botfish_BoardState botfishBS = new Botfish_BoardState();
        float wp = 0;
        float bp = 0;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece piece in bs.boardGrid[x, y])
                {
                    BotfishPiece bfp = fixBotfishPiece(piece);
                    if (bfp.color == 1) {
                        wp += bfp.points;
                    }
                    else if (bfp.color == -1) {
                        bp += bfp.points;
                    }

                    botfishBS.boardGrid[x, y].Add(bfp);
                }
            }
        }

        botfishBS.whitePointsOnBoard = wp;
        botfishBS.blackPointsOnBoard = bp;

        return botfishBS;
    }

    public interface BotfishPiece {
        public int color { get; set; }
        public string baseType { get; set; }
        public string name { get; set; }
        public bool hasMoved { get; set; }
        public coords[] moves { get; set; }
        public (int dx, int dy)[] directions { get; set; }
        public float points { get; set; }
        public coords position { get; set; }
    }

    public class Botfish_Queen : BotfishPiece {
        public int color { get; set; } = 1;
        public coords position { get; set; } = new coords(-1, -1);
        public bool hasMoved { get; set; } = false;
        public string baseType { get; set; } = "Queen";
        public string name { get; set; } = "";
        public float points { get; set; } = 9;
        public (int dx, int dy)[] directions { get; set; } = new (int, int)[]
        {
            (0, 1), (1, 1), (1, 0), (0, -1),
            (-1, -1), (-1, 0), (-1, 1), (1, -1)
        };
        public coords[] moves { get; set; } = { };

        public Botfish_Queen(int color, bool hasMoved) {
            this.color = color;
            this.hasMoved = hasMoved;

            this.name = this.baseType + "_" + globalDefs.globalRand.Next(1, 10001);
        }
    }

    public class Botfish_Rook : BotfishPiece {
        public int color { get; set; } = 1;
        public coords position { get; set; } = new coords(-1,-1);
        public bool hasMoved { get; set; } = false;
        public string baseType { get; set; } = "Rook";
        public string name { get; set; } = "";
        public float points { get; set; } = 5;
        public (int dx, int dy)[] directions { get; set; } = new (int, int)[]
        {
            (0, 1), (1, 0), (0, -1), (-1, 0)
        };
        public coords[] moves { get; set; } = {};

        public Botfish_Rook(int color, bool hasMoved) {
            this.color = color;
            this.hasMoved = hasMoved;

            this.name = this.baseType + "_" + globalDefs.globalRand.Next(1, 10001);
        }
    }

    public class Botfish_Bishop : BotfishPiece {
        public int color { get; set; } = 1;
        public coords position { get; set; } = new coords(-1, -1);
        public bool hasMoved { get; set; } = false;
        public string baseType { get; set; } = "Bishop";
        public string name { get; set; } = "";
        public float points { get; set; } = 3;
        public (int dx, int dy)[] directions { get; set; } = new (int, int)[]
        {
            (1, 1), (-1, -1), (-1, 1), (1, -1)
        };
        public coords[] moves { get; set; } = {};

        public Botfish_Bishop(int color, bool hasMoved) {
            this.color = color;
            this.hasMoved = hasMoved;

            this.name = this.baseType + "_" + globalDefs.globalRand.Next(1, 10001);
        }
    }

    public class Botfish_King : BotfishPiece {
        public int color { get; set; } = 1;
        public coords position { get; set; } = new coords(-1, -1);
        public bool hasMoved { get; set; } = false;
        public string baseType { get; set; } = "King";
        public string name { get; set; } = "";
        public float points { get; set; } = 100;
        public (int dx, int dy)[] directions { get; set; } = new (int, int)[] { };
        public coords[] moves { get; set; } = new coords[]
        {
            new coords(0, 1), new coords(1, 1), new coords(1, 0), new coords(0, -1),
            new coords(-1, -1), new coords(-1, 0), new coords(-1, 1), new coords(1, -1)
        };

        public Botfish_King(int color, bool hasMoved) {
            this.color = color;
            this.hasMoved = hasMoved;

            this.name = this.baseType + "_" + globalDefs.globalRand.Next(1, 10001);
        }
    }

    public class Botfish_Knight : BotfishPiece {
        public int color { get; set; } = 1;
        public coords position { get; set; } = new coords(-1, -1);
        public bool hasMoved { get; set; } = false;
        public string baseType { get; set; } = "Knight";
        public string name { get; set; } = "";
        public float points { get; set; } = 3;
        public (int dx, int dy)[] directions { get; set; } = new (int, int)[] { };
        public coords[] moves { get; set; } = new coords[] {
            new coords(2, 1), new coords(-2, 1), new coords(2, -1), new coords(-2, -1)
            , new coords(1, 2), new coords(-1, 2), new coords(1, -2), new coords(-1, -2)
        };

        public Botfish_Knight(int color, bool hasMoved) {
            this.color = color;
            this.hasMoved = hasMoved;

            this.name = this.baseType + "_" + globalDefs.globalRand.Next(1, 10001);
        }
    }

    public class Botfish_Pawn : BotfishPiece
    {
        public int color { get; set; } = 1;
        public bool hasMoved { get; set; } = false;
        public coords position { get; set; } = new coords(-1, -1);
        public string baseType { get; set; } = "Pawn";
        public string name { get; set; } = "";
        public float points { get; set; } = 1;
        public (int dx, int dy)[] directions { get; set; } = new (int, int)[] { };
        public coords[] moves { get; set; } = { };

        public Botfish_Pawn(int color, bool hasMoved) {
            this.color = color;
            this.hasMoved = hasMoved;

            this.name = this.baseType + "_" + globalDefs.globalRand.Next(1, 10001);
        }
    }

    public class Botfish_UndoMovedPiece
    {
        public BotfishPiece p;
        public coords initialPosition;
        public coords newPosition;
        public bool dead;
        public bool spawned;
        public bool revertHasMoved;

        public Botfish_UndoMovedPiece(BotfishPiece p_, coords initialPosition_, coords newPosition_, bool dead_, bool spawned_, bool revertHasMoved_)
        {
            p = p_;
            initialPosition = initialPosition_;
            newPosition = newPosition_;
            dead = dead_;
            spawned = spawned_;
            revertHasMoved = revertHasMoved_;
        }

        public Botfish_UndoMovedPiece(BotfishPiece p_)
        {
            p = p_;
            initialPosition = new coords(-1,-1);
            newPosition = new coords(-1, -1);
            dead = false;
            spawned = false;
            revertHasMoved = false;
        }

        public Botfish_UndoMovedPiece()
        {
            p = null;
            initialPosition = new coords(-1, -1);
            newPosition = new coords(-1, -1);
            dead = false;
            spawned = false;
            revertHasMoved = false;
        }
    }

    public class Botfish_UndoMove
    {
        public List<Botfish_UndoMovedPiece> entries;

        public void addMove(Botfish_UndoMovedPiece undoMovedPiece)
        {
            entries.Add(undoMovedPiece);
        }
    }

    public static void Botfish_updateBoardState(coords coords, BotfishPiece piece, string action, Botfish_BoardState boardState)
    {
        if (coords.x < 0 || coords.y < 0)
        {
            return;
        }
        var square = boardState.boardGrid[coords.x, coords.y];

        if (action.ToLower() == "a" || action.ToLower() == "add")
        {
            bool alreadyExists = square.Any(p => p.name == piece.name);
            if (!alreadyExists)
            {
                square.Add(piece);
            }

            piece.position = new coords( coords.x + 1, coords.y + 1 );
        }

        if (action.ToLower() == "r" || action.ToLower() == "remove")
        {
            int removed = square.RemoveAll(p => p.name == piece.name);
        }
    }

    public static void Botfish_kill(BotfishPiece p, Botfish_UndoMove undo, Botfish_BoardState bs) {
        Botfish_UndoMovedPiece ump = new Botfish_UndoMovedPiece(p, p.position, new coords(-1, -1), true, false, false);
        undo.addMove(ump);

        Botfish_updateBoardState(new coords( p.position.x - 1, p.position.y - 1 ), p, "r", bs);
    }

    public static void Botfish_move(BotfishPiece p, coords to, Botfish_UndoMove undo, Botfish_BoardState bs) {
        Botfish_UndoMovedPiece ump;
        if (p.hasMoved) {
            ump = new Botfish_UndoMovedPiece(p, p.position, to, false, false, false);
        }
        else {
            ump = new Botfish_UndoMovedPiece(p, p.position, to, false, false, true);
        }
        undo.addMove(ump);

        Botfish_updateBoardState(new coords( p.position.x - 1, p.position.y - 1 ), p, "r", bs);
        Botfish_updateBoardState(new coords( to.x, to.y ), p, "a", bs); //to is adjusted to 0 indexing
    }

    public static Botfish_UndoMove Botfish_simulatePieceMove(Botfish_BoardState bs, BotfishPiece piece, coords coords) {
        Botfish_UndoMove undo = new Botfish_UndoMove();

        coords unadjusted = coords;
        coords = new coords(coords.x - 1, coords.y - 1);

        List<BotfishPiece> piecesOnCoordsPreDeath = Botfish_isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid);

        bool death = false;
        if (piecesOnCoordsPreDeath.Count > 0) {
            foreach(BotfishPiece poc in piecesOnCoordsPreDeath) {
                if (poc.color != piece.color) {
                    death = true;
                    break;
                }
            }
        }

        if (death) {
            foreach(BotfishPiece p in piecesOnCoordsPreDeath) {
                Botfish_kill(p, undo, bs);
            }
        }

        Botfish_move(piece, coords, undo, bs);

        if (piece.baseType == "Pawn") {
            if (piece.color == 1 && piece.position.y == 8 || piece.color == -1 && piece.position.y == 1) {
                BotfishPiece queen = new Botfish_Queen(piece.color, false);
                queen.position = new coords(piece.position.x, piece.position.y);

                Botfish_UndoMovedPiece umpbf = new Botfish_UndoMovedPiece(queen, new coords(-1, -1), queen.position, false, true, false);
                undo.addMove(umpbf);
                Botfish_kill(piece, undo, bs);
            }
        }

        return undo;
    }

    public static void undoMove(Botfish_UndoMove undo, Botfish_BoardState bs)
    {
        for (int i = undo.entries.Count - 1; i >= 0; i--)
        {
            Botfish_UndoMovedPiece ump = undo.entries[i];

            if (ump == null)
            {
                continue;
            }

            BotfishPiece p = ump.p;
            coords initialPosition = ump.initialPosition;
            coords newPosition = ump.newPosition;
            bool dead = ump.dead;
            bool spawned = ump.spawned;
            bool revertHasMoved = ump.revertHasMoved;

            if (spawned)
            {
                Botfish_updateBoardState(new coords( newPosition.x - 1, newPosition.y - 1 ),p, "r", bs);
            }
            else if (dead)
            {
                Botfish_updateBoardState(new coords( initialPosition.x - 1, initialPosition.y - 1 ),p, "a", bs);
            }
            else
            {
                Botfish_updateBoardState(new coords( newPosition.x - 1, newPosition.y - 1 ),p, "r", bs);

                Botfish_updateBoardState(new coords( initialPosition.x - 1, initialPosition.y - 1 ),p, "a", bs);
            }

            if (revertHasMoved)
            {
                p.hasMoved = false;
            }
        }
    }
}
*/
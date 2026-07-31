using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static BotHelperFunctions;
using static UndoMoveBotHelperFunctions;
using System.Text;

public class Botfish : BotTemplate
{
    public Botfish(int botColor)
    {
        color = botColor;
        pieces = new List<Piece>();
        name = "Botfish";

        choosePieces();
    }

    public class Botfish_Move
    {
        public BotfishPiece p;
        public coords coords;

        public Botfish_Move(BotfishPiece piece, coords coords)
        {
            this.p = piece;
            this.coords = coords;
        }
    }

    public static string Botfish_debug_printBoardState(Botfish_BoardState bs)
    {
        StringBuilder sb = new StringBuilder();

        List<string> names = new List<string>();
        bool duplicateName = false;

        float w = 0;
        float b = 0;

        List<BotfishPiece>[,] boardGrid = bs.boardGrid;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (BotfishPiece p in boardGrid[x, y])
                {
                    if (names.Contains(p.name))
                    {
                        duplicateName = true;
                    }
                    else
                    {
                        names.Add(p.name);
                    }

                    sb.AppendLine(p.name + "(" + p.color + ")" + " found on " + (x + 1) + "," + (y + 1) + " worth " + p.points + ". Position: (" + p.position.x + "," + p.position.y + ")");
                    if (p.color == 1)
                    {
                        w += p.points;
                    }
                    else
                    {
                        b += p.points;
                    }
                }
            }
        }



        sb.AppendLine("White Total: " + w + "Black Total: " + b);

        //Debug.LogWarning(sb.ToString());

        if (duplicateName)
        {
            Debug.LogError("Duplicate Name Found");
            //Debug.Break();
        }

        return sb.ToString();
    }

    override
    public NextMove nextMove()
    {
        int movesAnalyzed_l1 = 0, movesAnalyzed_l2 = 0, movesAnalyzed_l3 = 0, movesAnalyzed_l4 = 0;
        Dictionary<BotfishPiece, Piece> pieceMap = new Dictionary<BotfishPiece, Piece>();

        Botfish_BoardState botfishBS = Botfish_FixBoardState(this.currentBoardState, pieceMap);
        Botfish_Move bestMove = null;

        List<Botfish_Move> allMoves = Botfish_getAllBotMoves(botfishBS, this.color, false);

        float bestScore_fold1 = Mathf.NegativeInfinity;
        float alpha1 = Mathf.NegativeInfinity;

        foreach (Botfish_Move botFishMove in allMoves)
        {
            movesAnalyzed_l1++;
            BotfishPiece piece_fold1 = botFishMove.p;
            coords coords_fold1 = botFishMove.coords;

            //Debug.LogWarning("Analyzing Move: " + piece_fold1 + " " + piece_fold1.name + " to " + coords_fold1.x + "," + coords_fold1.y);

            Botfish_UndoMove undo_fold1 = Botfish_simulatePieceMove(botfishBS, piece_fold1, coords_fold1);
            if (Botfish_IsKingAttacked(botfishBS, this.color))
            {
                Botfish_undoMove(undo_fold1, botfishBS);
                continue;
            }

            //Debug.Log(Botfish_debug_printBoardState(botfishBS));

            List<Botfish_Move> allMoves_fold1 = Botfish_getAllBotMoves(botfishBS, this.color * -1, false);

            float worstScore_fold2 = Mathf.Infinity;
            float beta2 = Mathf.Infinity;

            if (allMoves_fold1.Count == 0)
            {
                //Debug.Log("Setting Worst Score Fold 2");
                worstScore_fold2 = Botfish_Evaluate(botfishBS, this.color);
            }
            else
            {
                foreach (Botfish_Move botFishMove_fold2 in allMoves_fold1)
                {
                    movesAnalyzed_l2++;
                    BotfishPiece piece_fold2 = botFishMove_fold2.p;
                    coords coords_fold2 = botFishMove_fold2.coords;

                    Botfish_UndoMove undo_fold2 = Botfish_simulatePieceMove(botfishBS, piece_fold2, coords_fold2);
                    if (Botfish_IsKingAttacked(botfishBS, this.color * -1))
                    {
                        Botfish_undoMove(undo_fold2, botfishBS);
                        continue;
                    }

                    List<Botfish_Move> allMoves_fold2 = Botfish_getAllBotMoves(botfishBS, this.color * 1, false);

                    float alpha3 = alpha1;
                    float bestScore_fold3 = Mathf.NegativeInfinity;

                    if (allMoves_fold2.Count == 0)
                    {
                        bestScore_fold3 = Botfish_Evaluate(botfishBS, this.color);
                    }
                    else
                    {
                        foreach (Botfish_Move botFishMove_fold3 in allMoves_fold2)
                        {
                            movesAnalyzed_l3++;
                            BotfishPiece piece_fold3 = botFishMove_fold3.p;
                            coords coords_fold3 = botFishMove_fold3.coords;

                            Botfish_UndoMove undo_fold3 = Botfish_simulatePieceMove(botfishBS, piece_fold3, coords_fold3);
                            if (Botfish_IsKingAttacked(botfishBS, this.color))
                            {
                                Botfish_undoMove(undo_fold3, botfishBS);
                                continue;
                            }

                            List<Botfish_Move> allMoves_fold3 = Botfish_getAllBotMoves(botfishBS, this.color * -1, false);

                            float beta4 = beta2;
                            float worstScore_fold4 = Mathf.Infinity;

                            if (allMoves_fold3.Count == 0)
                            {
                                worstScore_fold4 = Botfish_Evaluate(botfishBS, this.color);
                            }
                            else
                            {
                                foreach (Botfish_Move botFishMove_fold4 in allMoves_fold3)
                                {
                                    movesAnalyzed_l4++;

                                    BotfishPiece piece_fold4 = botFishMove_fold4.p;
                                    coords coords_fold4 = botFishMove_fold4.coords;

                                    Botfish_UndoMove undo_fold4 = Botfish_simulatePieceMove(botfishBS, piece_fold4, coords_fold4);

                                    if (Botfish_IsKingAttacked(botfishBS, this.color * -1))
                                    {
                                        Botfish_undoMove(undo_fold4, botfishBS);
                                        continue;
                                    }

                                    float score = Botfish_Evaluate(botfishBS, this.color);

                                    worstScore_fold4 = Mathf.Min(worstScore_fold4, score);
                                    beta4 = Mathf.Min(beta4, worstScore_fold4);

                                    if (beta4 <= alpha3)
                                    {
                                        Botfish_undoMove(undo_fold4, botfishBS);
                                        break;
                                    }

                                    Botfish_undoMove(undo_fold4, botfishBS);
                                }
                            }

                            /*
                            if (worstScore_fold4 > bestScore_fold3)
                            {
                                Debug.Log("Setting Best Fold 3 Score to: " + worstScore_fold4 + " after " + piece_fold3.name + " to " + coords_fold3.x + "," + coords_fold3.y);
                                Debug.Log(Botfish_debug_printBoardState(botfishBS));
                            }
                            */

                            bestScore_fold3 = Mathf.Max(bestScore_fold3, worstScore_fold4);

                            alpha3 = Mathf.Max(alpha3, bestScore_fold3);

                            if (alpha3 >= beta2)
                            {
                                Botfish_undoMove(undo_fold3, botfishBS);
                                break;
                            }

                            Botfish_undoMove(undo_fold3, botfishBS);
                        }
                    }

                    /*
                    if (bestScore_fold3 < worstScore_fold2)
                    {
                        Debug.LogWarning("Setting Worst Fold 2 Score to: " + bestScore_fold3);
                        Debug.Log(Botfish_debug_printBoardState(botfishBS));
                    }
                    */

                    worstScore_fold2 = Mathf.Min(worstScore_fold2, bestScore_fold3);

                    beta2 = Mathf.Min(beta2, worstScore_fold2);

                    if (beta2 <= alpha1)
                    {
                        Botfish_undoMove(undo_fold2, botfishBS);
                        break;
                    }

                    Botfish_undoMove(undo_fold2, botfishBS);
                }
            }

            //float addOns = Botfish_Eval(botfishBS, this.color);
            //Debug.LogWarning("Analyzed Move. Points: " + worstScore_fold2 + " Addons: " + addOns + " Total: " + (worstScore_fold2 + addOns));

            //worstScore_fold2 += addOns;

            if (worstScore_fold2 > bestScore_fold1)
            {
                bestScore_fold1 = worstScore_fold2;
                bestMove = botFishMove;
            }

            alpha1 = bestScore_fold1;

            Botfish_undoMove(undo_fold1, botfishBS);
        }

        //Debug.LogWarning("Botfish Moves Analyzed: " + "L1: " + movesAnalyzed_l1 + " L2: " + movesAnalyzed_l2 + " L3: " + movesAnalyzed_l3 + " L4: " + movesAnalyzed_l4);

        if (bestMove == null)
        {
            return BotHelperFunctions.getRandomBotMove(this);
        }
        else
        {
            Botfish_Move move = bestMove;
            Piece movePiece = pieceMap[move.p];
            coords moveCoords = move.coords;

            return new NextMove(new Move(movePiece, moveCoords));
        }
    }

    public static bool Botfish_IsKingAttacked(Botfish_BoardState bs, int kingColor)
    {
        BotfishPiece king = null;
        foreach (BotfishPiece p in bs.allPieces)
        {
            if (p.color == kingColor && p.baseType == "King")
            {
                king = p;
                break;
            }
        }

        if (king == null) return true;

        int kx = king.position.x - 1; // convert to 0-indexed
        int ky = king.position.y - 1;

        // --- Sliding pieces: Rook/Queen (straight lines) ---
        int[][] straightDirs = new int[][] {
            new int[]{1,0}, new int[]{-1,0},
            new int[]{0,1}, new int[]{0,-1}
        };
        foreach (int[] dir in straightDirs)
        {
            int x = kx + dir[0];
            int y = ky + dir[1];
            while (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                List<BotfishPiece> pieces = Botfish_isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid);
                if (pieces.Count > 0)
                {
                    BotfishPiece p = pieces[0];
                    if (p.color != kingColor && (p.baseType == "Rook" || p.baseType == "Queen"))
                        return true;
                    break; // blocked by any piece
                }
                x += dir[0];
                y += dir[1];
            }
        }

        // --- Sliding pieces: Bishop/Queen (diagonals) ---
        int[][] diagDirs = new int[][] {
            new int[]{1,1}, new int[]{1,-1},
            new int[]{-1,1}, new int[]{-1,-1}
        };
        foreach (int[] dir in diagDirs)
        {
            int x = kx + dir[0];
            int y = ky + dir[1];
            while (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                List<BotfishPiece> pieces = Botfish_isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid);
                if (pieces.Count > 0)
                {
                    BotfishPiece p = pieces[0];
                    if (p.color != kingColor && (p.baseType == "Bishop" || p.baseType == "Queen"))
                        return true;
                    break;
                }
                x += dir[0];
                y += dir[1];
            }
        }

        // --- Knight attacks ---
        int[][] knightMoves = new int[][] {
            new int[]{2,1}, new int[]{2,-1}, new int[]{-2,1}, new int[]{-2,-1},
            new int[]{1,2}, new int[]{1,-2}, new int[]{-1,2}, new int[]{-1,-2}
        };
        foreach (int[] offset in knightMoves)
        {
            int x = kx + offset[0];
            int y = ky + offset[1];
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                List<BotfishPiece> pieces = Botfish_isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid);
                if (pieces.Count > 0 && pieces[0].color != kingColor && pieces[0].baseType == "Knight")
                    return true;
            }
        }

        // --- Pawn attacks ---
        int pawnDir = kingColor == 1 ? 1 : -1; // pawns attack toward the king
        int[][] pawnOffsets = new int[][] { new int[] { 1, pawnDir }, new int[] { -1, pawnDir } };
        foreach (int[] offset in pawnOffsets)
        {
            int x = kx + offset[0];
            int y = ky + offset[1];
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                List<BotfishPiece> pieces = Botfish_isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid);
                if (pieces.Count > 0 && pieces[0].color != kingColor && pieces[0].baseType == "Pawn")
                    return true;
            }
        }

        // --- King proximity (to avoid illegal king adjacency) ---
        int[][] kingOffsets = new int[][] {
            new int[]{1,0}, new int[]{-1,0}, new int[]{0,1}, new int[]{0,-1},
            new int[]{1,1}, new int[]{1,-1}, new int[]{-1,1}, new int[]{-1,-1}
        };
        foreach (int[] offset in kingOffsets)
        {
            int x = kx + offset[0];
            int y = ky + offset[1];
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                List<BotfishPiece> pieces = Botfish_isolatedGetPiecesOnCoordsBoardGrid(x, y, bs.boardGrid);
                if (pieces.Count > 0 && pieces[0].color != kingColor && pieces[0].baseType == "King")
                    return true;
            }
        }

        return false;
    }

    public static List<Botfish_Move> Botfish_getAllBotMoves(Botfish_BoardState bs, int color, bool onlyKills) {
        List<Botfish_Move> moves = new List<Botfish_Move>();

        List<BotfishPiece> pieces = bs.allPieces;

        for (int i = 0; i < pieces.Count; i++) {
            BotfishPiece piece = pieces[i];

            if (piece.color != color) {
                continue;
            }

            var moves__ = Botfish_getIsolatedStatePieceMoves(piece, bs, onlyKills);
            coords[] moves_ = moves__.moves;
            int numMoves = moves__.numMoves;

            if (moves_ != null && moves_.Length > 0)
            {
                for (int j = 0; j < numMoves; j++) {
                    coords move = moves_[j];
                    Botfish_Move mv = new Botfish_Move(piece, move);

                    moves.Add(mv);
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

    public static (coords[] moves, int numMoves) Botfish_getIsolatedStatePieceMoves(BotfishPiece p, Botfish_BoardState bs, bool onlyKills) {
        coords[] allMoves = new coords[32];
        int acceptedMoves = 0;

        //Debug.Log("Getting moves for " + p.name + " " + p.baseType + " on " + p.position.x + "," + p.position.y);

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

                int newPosX = x;
                int newPosY = y;

                while (true)
                {
                    newPosX += dx;
                    newPosY += dy;

                    if (newPosX < 0 || newPosX > 7 || newPosY < 0 || newPosY > 7) break;

                    List<BotfishPiece> piecesOnCoords = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosX, newPosY, bs.boardGrid);

                    bool pieceIsNull = piecesOnCoords.Count == 0;
                    bool pieceIsDiffColour = false;

                    if (!pieceIsNull)
                    {
                        foreach (BotfishPiece pieceOnCoords in piecesOnCoords)
                        {
                            if (pieceOnCoords.color != p.color)
                            {
                                pieceIsDiffColour = true;
                                break;
                            }
                        }
                    }

                    if (pieceIsNull)
                    {
                        if (!onlyKills)
                        {
                            allMoves[acceptedMoves] = new coords(newPosX + 1, newPosY + 1);
                            acceptedMoves++;

                            //Debug.Log("Accepted Move to " + (newPosX + 1) + "," + (newPosY + 1));
                        }

                        continue;
                    }

                    if (pieceIsDiffColour)
                    {
                        allMoves[acceptedMoves] = new coords(newPosX + 1, newPosY + 1);
                        acceptedMoves++;

                        //Debug.Log("Accepted Move to " + (newPosX + 1) + "," + (newPosY + 1));
                    }

                    break;
                }
            }
        }
        else if (useDirections == 0) {
            foreach(coords move in moves) {
                int newPosX = x + move.x;
                int newPosY = y + move.y;

                if (newPosX < 0 || newPosX > 7 || newPosY < 0 || newPosY > 7) continue;

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
                    if (!onlyKills)
                    {
                        allMoves[acceptedMoves] = new coords(newPosX + 1, newPosY + 1);
                        acceptedMoves++;
                    }
                }

                if (!pieceIsNull && pieceIsDiffColour)
                {
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

                bool boundsCheck_ = false;

                if (newPosX < 0 || newPosX > 7 || newPosY < 0 || newPosY > 7) boundsCheck_ = true;

                if (!boundsCheck_)
                {
                    List<BotfishPiece> piecesOnCoords = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosX, newPosY, bs.boardGrid);

                    bool pieceIsNull = true;

                    if (piecesOnCoords.Count > 0)
                    {
                        pieceIsNull = false;
                    }

                    if (pieceIsNull)
                    {
                        // Is Jump

                        List<BotfishPiece> piecesOnCoordsJump = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosX, newPosYJump, bs.boardGrid);
                        if (piecesOnCoordsJump.Count == 0 && !onlyKills)
                        {
                            allMoves[acceptedMoves] = new coords(newPosX + 1, newPosY + 1);
                            acceptedMoves++;
                        }
                    }
                }
            }

            //Move
            int newPosXMove = x + 0;
            int newPosYMove = y + 1 * p.color;


            bool boundsCheck = false;

            if (newPosXMove < 0 || newPosXMove > 7 || newPosYMove < 0 || newPosYMove > 7) boundsCheck = true;

            if (!boundsCheck)
            {
                List<BotfishPiece> piecesOnCoordsMove = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosXMove, newPosYMove, bs.boardGrid);
                if (piecesOnCoordsMove.Count == 0)
                {
                    if (!onlyKills)
                    {
                        allMoves[acceptedMoves] = new coords(newPosXMove + 1, newPosYMove + 1);
                        acceptedMoves++;
                    }
                }
            }

            int newPosXAttack;
            //Attacks
            int[] dirs = new int[] { -1, 1 };
            foreach(int dir in dirs) {
                newPosXAttack = x + dir;

                boundsCheck = false;

                if (newPosXAttack < 0 || newPosXAttack > 7 || newPosYMove < 0 || newPosYMove > 7) boundsCheck = true;

                if (!boundsCheck)
                {

                    List<BotfishPiece> piecesOnCoordsAttack = Botfish_isolatedGetPiecesOnCoordsBoardGrid(newPosXAttack, newPosYMove, bs.boardGrid);

                    bool pieceIsNull = true;
                    bool pieceIsDiffColour = false;

                    if (piecesOnCoordsAttack.Count > 0)
                    {
                        pieceIsNull = false;

                        foreach (BotfishPiece pieceOnCoordsAttack in piecesOnCoordsAttack)
                        {
                            if (pieceOnCoordsAttack.color != p.color)
                            {
                                pieceIsDiffColour = true;
                                break;
                            }
                        }
                    }

                    if (!pieceIsNull && pieceIsDiffColour)
                    {
                        allMoves[acceptedMoves] = new coords(newPosXAttack + 1, newPosYMove + 1);
                        acceptedMoves++;
                    }
                }
            }
        }

        return (allMoves, acceptedMoves);
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
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    boardGrid[x, y] = new List<BotfishPiece>();
                }
            }
        }
    }

    public Botfish_BoardState Botfish_FixBoardState(BoardState bs, Dictionary<BotfishPiece, Piece> pieceMap) {
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
                    bfp.name = piece.name;
                    bfp.position = new coords(x + 1, y + 1);

                    if (pieceMap != null) {
                        pieceMap[bfp] = piece;
                    }

                    if (bfp.color == 1) {
                        wp += bfp.points;
                    }
                    else if (bfp.color == -1) {
                        bp += bfp.points;
                    }

                    botfishBS.boardGrid[x, y].Add(bfp);
                    botfishBS.allPieces.Add(bfp);
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

            this.name = this.baseType + "_" + this.color.ToString() + "_" + globalDefs.globalRand.Next(1, 1000001);
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
        public List<Botfish_UndoMovedPiece> entries = new List<Botfish_UndoMovedPiece>();

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
            square.Add(piece);
        }

        if (action.ToLower() == "r" || action.ToLower() == "remove")
        {
            square.Remove(piece);
        }
    }

    public static void Botfish_kill(BotfishPiece p, Botfish_UndoMove undo, Botfish_BoardState bs) {
        Botfish_UndoMovedPiece ump = new Botfish_UndoMovedPiece(p, p.position, new coords(-1, -1), true, false, false);
        undo.addMove(ump);

        bs.allPieces.Remove(p);

        Botfish_updateBoardState(new coords( p.position.x - 1, p.position.y - 1 ), p, "r", bs);

        if (p.color == 1) {
            bs.whitePointsOnBoard -= p.points;
        }
        else {
            bs.blackPointsOnBoard -= p.points;
        }
    }

    public static void Botfish_move(BotfishPiece p, coords to, Botfish_UndoMove undo, Botfish_BoardState bs) {
        Botfish_UndoMovedPiece ump;
        if (p.hasMoved) {
            ump = new Botfish_UndoMovedPiece(p, p.position, new coords(to.x + 1, to.y + 1), false, false, false);
        }
        else {
            ump = new Botfish_UndoMovedPiece(p, p.position, new coords(to.x + 1, to.y + 1), false, false, true);
        }
        undo.addMove(ump);

        p.hasMoved = true;

        Botfish_updateBoardState(new coords( p.position.x - 1, p.position.y - 1 ), p, "r", bs);
        //Debug.Log(p + " from " + p.position.x + "," + p.position.y + " to " + to.x + "," + to.y);
        Botfish_updateBoardState(new coords( to.x, to.y ), p, "a", bs); //to is adjusted to 0 indexing

        p.position = new coords(to.x + 1, to.y + 1);
    }

    public static Botfish_UndoMove Botfish_simulatePieceMove(Botfish_BoardState bs, BotfishPiece piece, coords coords) {
        Botfish_UndoMove undo = new Botfish_UndoMove();

        coords unadjusted = coords;
        coords = new coords(coords.x - 1, coords.y - 1);

        List<BotfishPiece> piecesOnCoordsPreDeath = Botfish_isolatedGetPiecesOnCoordsBoardGrid(coords.x, coords.y, bs.boardGrid);

        if (piecesOnCoordsPreDeath.Count > 0) {
            foreach(BotfishPiece poc in new List<BotfishPiece>(piecesOnCoordsPreDeath)) {
                if (poc.color != piece.color) {
                    Botfish_kill(poc, undo, bs);
                }
            }
        }

        Botfish_move(piece, coords, undo, bs);

        if (piece.baseType == "Pawn") {
            if (piece.color == 1 && piece.position.y == 8 || piece.color == -1 && piece.position.y == 1) {
                BotfishPiece queen = new Botfish_Queen(piece.color, false);
                bs.allPieces.Add(queen);
                queen.position = new coords(piece.position.x, piece.position.y);

                if (queen.color == 1) {
                    bs.whitePointsOnBoard += queen.points;
                }
                else {
                    bs.blackPointsOnBoard += queen.points;
                }

                Botfish_UndoMovedPiece umpbf = new Botfish_UndoMovedPiece(queen, new coords(-1, -1), queen.position, false, true, false);
                undo.addMove(umpbf);
                Botfish_kill(piece, undo, bs);
            }
        }

        return undo;
    }

    public static void Botfish_undoMove(Botfish_UndoMove undo, Botfish_BoardState bs)
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

                bs.allPieces.Remove(p);

                if (p.color == 1) {
                    bs.whitePointsOnBoard -= p.points;
                }
                else {
                    bs.blackPointsOnBoard -= p.points;
                }
            }
            else if (dead)
            {
                Botfish_updateBoardState(new coords( initialPosition.x - 1, initialPosition.y - 1 ),p, "a", bs);

                bs.allPieces.Add(p);
                p.position = initialPosition;

                if (p.color == 1) {
                    bs.whitePointsOnBoard += p.points;
                }
                else {
                    bs.blackPointsOnBoard += p.points;
                }
            }
            else
            {
                Botfish_updateBoardState(new coords( newPosition.x - 1, newPosition.y - 1 ),p, "r", bs);

                Botfish_updateBoardState(new coords( initialPosition.x - 1, initialPosition.y - 1 ),p, "a", bs);

                p.position = initialPosition;
            }

            if (revertHasMoved)
            {
                p.hasMoved = false;
            }
        }
    }

    private float Botfish_Points(Botfish_BoardState bs, int color)
    {
        float botPoints = color == 1 ? bs.whitePointsOnBoard : bs.blackPointsOnBoard;
        float oppPoints = color == -1 ? bs.whitePointsOnBoard : bs.blackPointsOnBoard;

        return botPoints - oppPoints;
    }

    private float Botfish_Eval(Botfish_BoardState bs, int color)
    {
        var boardControlEVAL = Botfish_getBoardControlOnBoardState(bs, color);

        int botBoardControl = boardControlEVAL.boardControl;
        int botCenterControl = boardControlEVAL.centerControl;
        int botKingAttacking = boardControlEVAL.kingAttacking;

        return (float) ((0.01 * botBoardControl) + (0.01 * botCenterControl) + (0.001 * botKingAttacking));
    }

    private float Botfish_Evaluate(Botfish_BoardState bs, int color)
    {
        float botPoints = color == 1 ? bs.whitePointsOnBoard : bs.blackPointsOnBoard;
        float oppPoints = color == -1 ? bs.whitePointsOnBoard : bs.blackPointsOnBoard;

        var boardControlEVAL = Botfish_getBoardControlOnBoardState(bs, color);

        int botBoardControl = boardControlEVAL.boardControl;
        int botCenterControl = boardControlEVAL.centerControl;
        int botKingAttacking = boardControlEVAL.kingAttacking;

        return (float)(botPoints - oppPoints + (0.01 * botBoardControl) + (0.01 * botCenterControl) + (0.001 * botKingAttacking));
    }

    private (int boardControl, int centerControl, int kingAttacking) Botfish_getBoardControlOnBoardState(Botfish_BoardState bs, int color)
    {
        coords whiteKingPos = new coords(-1, -1);
        coords blackKingPos = new coords(-1, -1);

        foreach (BotfishPiece p in bs.allPieces)
        {
            if (p.baseType == "King")
            {
                if (p.color == 1) whiteKingPos = p.position;
                else blackKingPos = p.position;
            }
        }

        int boardControl = 0;
        int centerControl = 0;
        int kingAttacking = 0;

        coords kingPos = color == 1 ? blackKingPos : whiteKingPos;

        var moves = Botfish_getAllBotMoves(bs, 1, false);

        foreach (Botfish_Move mv in moves)
        {
            coords c = mv.coords;

            boardControl++;

            if ((c.x == 4 || c.x == 5) && (c.y == 4 || c.y == 5))
                centerControl++;

            if (kingPos.x != -1)
            {
                int dx = Mathf.Abs(c.x - kingPos.x);
                int dy = Mathf.Abs(c.y - kingPos.y);

                if (dx < 3 && dy < 3)
                {
                    kingAttacking += (3 - dx);
                    kingAttacking += (3 - dy);
                }

                if (c.x == kingPos.x && c.y == kingPos.y)
                    kingAttacking += 16;
            }
        }

        return (boardControl, centerControl, kingAttacking);
    }
}
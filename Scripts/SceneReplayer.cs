using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using Photon.Pun;
using System.Linq;
using System.IO;
using System.Text;
using static BotHelperFunctions;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public static class WindowsFileDialog
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OpenFileName
    {
        public int structSize = Marshal.SizeOf(typeof(OpenFileName));
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public string filter = "All Files\0*.*\0Text Files\0*.txt\0";
        public string customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 1;
        public StringBuilder file = new StringBuilder(260);
        public int maxFile = 260;
        public StringBuilder fileTitle = new StringBuilder(260);
        public int maxFileTitle = 260;
        public string initialDir = "";
        public string title = "Open File";
        public int flags = 0x00000008 | 0x00001000;
        public short fileOffset;
        public short fileExtension;
        public string defExt = "";
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public string templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }

    [DllImport("Comdlg32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

    public static string OpenFile()
    {
        OpenFileName ofn = new OpenFileName();

        if (GetOpenFileName(ofn))
        {
            return ofn.file.ToString();
        }

        return null;
    }
}

public class FilePicker : MonoBehaviour
{
    [SerializeField] private matchReplayer replayer;

    public void OpenFile()
    {
        string path = WindowsFileDialog.OpenFile();

        if (string.IsNullOrEmpty(path))
            return;

        Debug.Log("Selected: " + path);

        byte[] bytes = File.ReadAllBytes(path);
        replayer.setFile(bytes);
    }
}

public class matchReplayer : MonoBehaviour
{
    byte[] file;
    string[] lines;

    List<List<Piece>[,]> turnBoardGrids = new List<List<Piece>[,]>();
    int turnIdx = 0;

    public GameObject board2;
    public HelperFunctions helper;

    public void setFile(byte[] file)
    {
        this.file = file;

        string text = Encoding.UTF8.GetString(file);
        string[] lines = text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

        this.lines = lines;
    }

    void Start()
    {
        helper.panel.Initialize();
        gameData.helper = helper;

        gameData.isBotMatch = true;
    }

    public void setTurnIdx(int idx)
    {
        this.turnIdx = idx;

        if (idx < 0)
        {
            this.turnIdx = 0;
        }
        else if (idx >= turnBoardGrids.Count)
        {
            this.turnIdx = turnBoardGrids.Count - 1;
        }
    }

    void clearBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject square = HelperFunctions.findSquare(x + 1, y + 1);

                foreach (Transform child in square.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    void setBoard(List<Piece>[,] boardGrid)
    {
        clearBoard();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                foreach (Piece piece in boardGrid[x, y])
                {
                    GameObject square = HelperFunctions.findSquare(x + 1, y + 1);

                    HelperFunctions.movePiece(piece, square);
                }
            }
        }

        helper.updatePointsOnBoard();
    }

    void parseFile(string[] lines)
    {
        bool processingTurn = false;
        bool createdObject = false;
        bool sendMoveObject = false;

        bool setBots = false;

        foreach (string line in lines)
        {
            if (line.Contains("Turn"))
            {
                processingTurn = true;
                createdObject = false;
            }
            else if (line.Contains("Pieces on Board Grid"))
            {
                continue;
            }
            else if (line.Contains("White") && line.Contains("Black") && setBots == false)
            {
                char[] charSeparators = new char[] { '(', ')' };

                string[] result = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                string botWhite = result[0];
                string botBlack = result[4];

                Type botWhiteType = Type.GetType(botWhite + ", Assembly-CSharp");
                Type botBlackType = Type.GetType(botBlack + ", Assembly-CSharp");

                gameData.botWhite = (BotTemplate)Activator.CreateInstance(botWhiteType, 1);
                gameData.botBlack = (BotTemplate)Activator.CreateInstance(botBlackType, -1);

                helper.panel.Initialize();

                setBots = true;
            }

            List<Piece>[,] boardGrid = null;
            if (processingTurn)
            {
                if (!createdObject)
                {
                    boardGrid = HelperFunctions.initBoardGridNew();
                    createdObject = true;
                }

                if (line.Contains("Turn"))
                {
                    sendMoveObject = true;
                }
                else if (line == "")
                {
                    sendMoveObject = true;
                }
                else
                {
                    char[] charSeparators = new char[] { '(', ')', ' ', ',' };
                    string[] result;

                    result = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                    string pieceName = result[0];
                    int pieceColor = int.Parse(result[1]);

                    Piece piece = getPieceTypeInstance(pieceName, pieceColor);

                    int x = int.Parse(result[4]);
                    int y = int.Parse(result[5]);

                    coords pieceCoords = new coords(x, y);

                    boardGrid[pieceCoords.x, pieceCoords.y].Add(piece);
                }
            }


            if (sendMoveObject)
            {
                turnBoardGrids.Add(boardGrid);

                sendMoveObject = false;
                processingTurn = false;
            }
        }
    }
}
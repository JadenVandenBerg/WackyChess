using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LoadNewGame : MonoBehaviour
{
    private void Start()
    {
        //PhotonNetwork.Disconnect();
    }

    public void loadGame()
    {
        resetGame();
        SceneManager.LoadScene(1);
    }

    public void resetGame()
    {
        gameData.selected = null;
        gameData.piecesDict = new Dictionary<GameObject, Piece>();
        gameData.board = null;
        gameData.selectedToMove = null;
        gameData.whiteKing = null;
        gameData.blackKing = null;
        gameData.isSelected = false;
        gameData.turn = 1;
        gameData.readyToMove = false;
        gameData.currentMoveableCoords = new List<int[]>();
        gameData.currentMoveableCoordsAllPieces = new List<int[]>();
        gameData.isInCheck = new int[] { 0, 0 }; //0 white, 1 black
        gameData.whiteRooks = new List<Piece>();
        gameData.blackRooks = new List<Piece>();
        gameData.pointsOnBoard = new float[] { 0, 0 };
        gameData.winner = "";
        gameData.botMoves = new Dictionary<Piece, List<int[]>>();
        gameData.playMode = "";
    }

    public void loadGameBot1()
    {
        resetGame();
        SceneManager.LoadScene(2);
    }

    public void loadGameBot2()
    {
        resetGame();
        SceneManager.LoadScene(3);
    }

    public void loadLobby()
    {
        resetGame();
        SceneManager.LoadScene("Loading");
    }

    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();
    }
}
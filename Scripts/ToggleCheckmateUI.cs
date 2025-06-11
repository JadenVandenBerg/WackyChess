using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ToggleCheckmateUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject checkmateUI;
    public GameObject text;

    public void Update()
    {
        text.GetComponent<TextMeshProUGUI>().text = gameData.winner;
    }
    public void ToggleOffCheckmateUI()
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

        if (gameData.playMode == "2Player")
        {
            SceneManager.LoadScene(1);
        } else if (gameData.playMode == "Bot1")
        {
            SceneManager.LoadScene(2);
        }
        checkmateUI.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {

        Room currentRoom = PhotonNetwork.CurrentRoom;

        Debug.Log("Room name: " + currentRoom.Name);
        Debug.Log("Player count: " + currentRoom.PlayerCount);
        Debug.Log("Max players: " + currentRoom.MaxPlayers);
        Debug.Log("Is visible: " + currentRoom.IsVisible);
        Debug.Log("Is open: " + currentRoom.IsOpen);
        Debug.Log("Custom properties: " + currentRoom.CustomProperties.ToString());

        PhotonNetwork.LoadLevel("OnlineBoard");
    }
}

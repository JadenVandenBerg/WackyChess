using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        /*
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting to Server");
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.Disconnect();
        }
        */

        PhotonNetwork.ConnectUsingSettings();
    }

    /*

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    */
    public override void OnConnectedToMaster()
    {

        Debug.Log("Connected to Master Server");
        Debug.Log("Joining Lobby");
        PhotonNetwork.JoinLobby();
        //PhotonNetwork.JoinRandomOrCreateRoom();

    }
    /*
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Joining Lobby Failed. Making Lobby");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Lobby Success");
        PhotonNetwork.LoadLevel("OnlineBoard");
    }
    */

    public override void OnJoinedLobby()
    {
        Debug.Log("Joining Lobby");

        SceneManager.LoadScene("Lobby");


    }

    /*
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from Server");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Updated List of Rooms");
        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log(roomInfo.Name);
        }
        base.OnRoomListUpdate(roomList);
    }
    */
}

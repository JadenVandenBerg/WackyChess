using UnityEngine;
using Photon.Pun;
using System;

public static class PieceFactory
{
    public static T SpawnPiece<T>(int color, bool online) where T : Piece
    {
        GameObject go;

        if (online)
        {
            go = PhotonNetwork.Instantiate("Empty", Vector3.zero, Quaternion.identity);
        }
        else
        {
            GameObject prefab = Resources.Load<GameObject>("Empty");
            go = GameObject.Instantiate(prefab);
        }

        return (T)Activator.CreateInstance(typeof(T), go, color);
    }
}

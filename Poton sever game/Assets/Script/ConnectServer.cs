using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectServer : MonoBehaviour
{
    private string serverName;

    pbulic void SecelcServer(string text)
    {
        serverName = text;

        PhotonNetwork.ConnectUsingSettings();


    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }
}

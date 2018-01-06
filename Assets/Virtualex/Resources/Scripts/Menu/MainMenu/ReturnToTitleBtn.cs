using UnityEngine;
using System.Collections;
using Photon;

public class ReturnToTitleBtn : Photon.MonoBehaviour {

    void Activate() // leave room
    {
        if (!PhotonNetwork.inRoom)
        {
            Application.LoadLevel("LoginScene");
        }
        else
        {
            GameObject.FindGameObjectWithTag("NetworkController").GetComponent<ConnectToNetwork>().ReturnToLobby();
        }
    }
}

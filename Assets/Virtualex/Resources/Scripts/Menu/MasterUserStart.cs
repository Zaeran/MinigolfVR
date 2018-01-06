using UnityEngine;
using System.Collections;
using Photon;

public class MasterUserStart : Photon.MonoBehaviour {

    public void Activate(string courseToLoad)
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.open = false;
            photonView.RPC("MoveToNextLevel", PhotonTargets.All, courseToLoad);
        }
    }

    [PunRPC]
    public void MoveToNextLevel(string courseToLoad)
    {
        PhotonNetwork.LoadLevel(courseToLoad);
    }

}

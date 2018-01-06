using UnityEngine;
using System.Collections;
using Photon;

public class CreateScoreTracker : Photon.MonoBehaviour {

    void OnLevelWasLoaded()
    {
        if(PhotonNetwork.inRoom && Application.loadedLevelName != "LoginScene")
        {
            PhotonNetwork.Instantiate("ScoreObj", Vector3.zero, Quaternion.identity, 0);
        }
    }
}

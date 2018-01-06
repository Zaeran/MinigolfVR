using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Logs when a player has finished the course
/// </summary>
public class PlayerFinishedCourse : Photon.MonoBehaviour {

    int noOfPlayersFinished;

    void Start()
    {
        noOfPlayersFinished = 0;
    }

    public bool allPlayersDone()
    {
        return noOfPlayersFinished == PhotonNetwork.room.playerCount;
    }

    public void CourseComplete()
    {
        photonView.RPC("PlayerFinished", PhotonTargets.All);
    }

    [PunRPC]
    void PlayerFinished()
    {
        noOfPlayersFinished++;
    }
}

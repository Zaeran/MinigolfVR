using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Increments user's score across the network
/// </summary>
public class IncrementScore : Photon.MonoBehaviour {

    public bool isMine;
    public int currentScore;

	// Use this for initialization
	void Start () {
        isMine = photonView.isMine;
	}

    public void ScoreUp()
    {
        Debug.Log("SCORE UP REQUEST");
        photonView.RPC("ScoreIncrement", PhotonTargets.All, VarsTracker.playerID, VarsTracker.currentHole);
    }

    [PunRPC]
    public void ScoreIncrement(int ID, int hole)
    {
        Debug.Log("SCORE REQUEST SUCCEEDED");
        VarsTracker.playerScores[ID, hole - 1]++;
    }
}

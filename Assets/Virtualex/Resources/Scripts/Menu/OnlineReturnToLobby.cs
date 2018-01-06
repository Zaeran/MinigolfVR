using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class OnlineReturnToLobby : Photon.MonoBehaviour {

    int noOfVotes;
    public Text myText;

	// Use this for initialization
	void Start () {
        noOfVotes = 0;
	}
	
	// Update is called once per frame
	void Update () {
        myText.text = "FINISH COURSE\n" + noOfVotes + "/" + PhotonNetwork.room.playerCount;
	}

    public void Activate()
    {
        photonView.RPC("AddVote", PhotonTargets.All);
    }

    [PunRPC]
    public void AddVote()
    {
        noOfVotes++;
        if (noOfVotes == PhotonNetwork.room.playerCount)
        {
            if (PhotonNetwork.isMasterClient)
            {
                GameObject.FindGameObjectWithTag("NetworkController").GetComponent<MasterUserStart>().Activate("LoginScene");
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Used to set the local player ID (1,2,3,4)
/// Stores this ID as part of the room properties
/// </summary>
public class HandlePlayerID : Photon.MonoBehaviour {

    void Start()
    {
        if (photonView.isMine)
        {
            DontDestroyOnLoad(this);
        }
    }

    public void SetPlayerID()
    {
        if (photonView.isMine)
        {
            if (PhotonNetwork.room.customProperties["Player1"].Equals(-1))
            {
                ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
                h.Add("Player1", photonView.ownerId);
                PhotonNetwork.room.SetCustomProperties(h);
                VarsTracker.playerID = 0;
                Debug.Log("OWNED BY: " + photonView.ownerId);
            }
            else if (PhotonNetwork.room.customProperties["Player2"].Equals(-1))
            {
                ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
                h.Add("Player2", photonView.ownerId);
                PhotonNetwork.room.SetCustomProperties(h);
                VarsTracker.playerID = 1;
            }
            else if (PhotonNetwork.room.customProperties["Player3"].Equals(-1))
            {
                ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
                h.Add("Player3", photonView.ownerId);
                PhotonNetwork.room.SetCustomProperties(h);
                VarsTracker.playerID = 2;
            }
            else if (PhotonNetwork.room.customProperties["Player4"].Equals(-1))
            {
                ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
                h.Add("Player4", photonView.ownerId);
                PhotonNetwork.room.SetCustomProperties(h);
                VarsTracker.playerID = 3;
            }
        }
    }

    public void OnLeftRoom()
    {
        Destroy(this);
    }
}

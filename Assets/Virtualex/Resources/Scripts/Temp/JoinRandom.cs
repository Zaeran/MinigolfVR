using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Join a random game
/// </summary>
public class JoinRandom : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.autoJoinLobby = false;
            //PhotonNetwork.playerName = userName;
            PhotonNetwork.ConnectUsingSettings("0.9");
        }
	}

    /// <summary>
    /// Called when connected to master lobby
    /// </summary>
    public void OnConnectedToMaster()
    {
        RoomOptions ro = new RoomOptions();
        ro.maxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("LOL", ro, TypedLobby.Default);
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() + "\n" + (PhotonNetwork.room == null ? "" : PhotonNetwork.room.name));
    }

    /// <summary>
    /// Once room joined, set player properties
    /// </summary>
    public void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add("Player1", -1);
            properties.Add("Player2", -1);
            properties.Add("Player3", -1);
            properties.Add("Player4", -1);
            PhotonNetwork.room.SetCustomProperties(properties);
        }

        GameObject g = PhotonNetwork.Instantiate("PlayerIDHandler", Vector3.zero, Quaternion.identity, 0);
        g.GetComponent<HandlePlayerID>().SetPlayerID();
    }
}

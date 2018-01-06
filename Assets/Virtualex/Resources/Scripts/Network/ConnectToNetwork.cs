using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.UI;

public class ConnectToNetwork : PunBehaviour {

    //public Text errorMsg;
    string roomToJoin;

    #region CONNECTION HANDLING

    void Start()
    {
        roomToJoin = "";
    }
    /// <summary>
    /// Returns player to the network lobby
    /// </summary>
    public void ReturnToLobby()
    {
        roomToJoin = "";
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Ensures login is refreshed between levels
    /// </summary>
    void OnLevelWasLoaded()
    {
        if (PhotonNetwork.connected)
        {
            StartCoroutine(RefreshLogin());
        }
    }
    /// <summary>
    /// Joins a new room
    /// </summary>
    /// <param name="roomName">Room to join</param>
    public void JoinRoom(string roomName)
    {
        roomToJoin = roomName;
        if (PhotonNetwork.room != null)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            HandleRoomJoining();
        }
    }

    /// <summary>
    /// Connects a player to the network
    /// </summary>
    /// <param name="userName">username of player</param>
    public void JoinNetwork(string userName)
    {
        roomToJoin = "A";
        PhotonNetwork.automaticallySyncScene = true;
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.playerName = userName;
            PhotonNetwork.ConnectUsingSettings("0.9");
        }
    }

    /// <summary>
    /// Disconnect from network
    /// </summary>
    public void DisconnectFromNetwork()
    {
        PhotonNetwork.Disconnect();
        StopAllCoroutines();
    }



    // This is one of the callback/event methods called by PUN (read more in PhotonNetworkingMessage enumeration)
    public void OnConnectedToMaster()
    {
        HandleRoomJoining();       
    }

    /// <summary>
    /// Join room if room to join, otherwise start login refresh
    /// </summary>
    public void HandleRoomJoining()
    {
        if (roomToJoin == "A")
        {
            StartCoroutine(RefreshLogin());
            //do nothing. This will occur when we log in
        }
        else
        {
            if (roomToJoin != "")
            {
                PhotonNetwork.JoinRoom(roomToJoin);
            }
            else
            {
                Application.LoadLevel("LoginScene");
            }
        }
        roomToJoin = "";
    }

    public void OnPhotonJoinRoomFailed()
    {
       
    }

    // This is one of the callback/event methods called by PUN (read more in PhotonNetworkingMessage enumeration)
    public void OnPhotonRandomJoinFailed()
    {
        string RoomName = CreateRandomRoomName();
        PhotonNetwork.CreateRoom(RoomName, new RoomOptions() { maxPlayers = 4 }, null);
    }

    // This is one of the callback/event methods called by PUN (read more in PhotonNetworkingMessage enumeration)
    public void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient && !PhotonNetwork.room.customProperties.ContainsKey("Player1"))
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


        //join course
        Debug.Log("Room Created");
        AddJoinedRoom();
        //PhotonNetwork.Instantiate("[CameraRig]", Vector3.zero, Quaternion.identity, 0);
    }

    public void OnLeftRoom()
    {
        AddLeftRoom();
    }

    /// <summary>
    /// Lets the database know that a room has been joined
    /// </summary>
    void AddJoinedRoom()
    {
        WWWForm joinRoomForm = new WWWForm();
        joinRoomForm.AddField("room", PhotonNetwork.room.name);
        joinRoomForm.AddField("user", VarsTracker.PlayerName);
        WWW joinRoom = new WWW(VarsTracker.MinigolfNetworkBasePath + "JoinRoom.php", joinRoomForm);
        VarsTracker.CurrentRoom = PhotonNetwork.room.name;
    }

    /// <summary>
    /// Lets the database know that a room has been left
    /// </summary>
    void AddLeftRoom()
    {
        WWWForm leaveRoomForm = new WWWForm();
        leaveRoomForm.AddField("room", VarsTracker.CurrentRoom);
        leaveRoomForm.AddField("user", VarsTracker.PlayerName);
        WWW joinRoom = new WWW(VarsTracker.MinigolfNetworkBasePath + "LeaveRoom.php", leaveRoomForm);
        VarsTracker.CurrentRoom = "";
    }

    // This is one of the callback/event methods called by PUN (read more in PhotonNetworkingMessage enumeration)
    public void OnCreatedRoom()
    {
        //Application.LoadLevel(Application.loadedLevel);
        Debug.Log(PhotonNetwork.room.name);
    }

    /// <summary>
    /// Called when photon lobby is joined
    /// </summary>
    public void OnJoinedLobby()
    {
        foreach (RoomInfo s in PhotonNetwork.GetRoomList())
        {
            Debug.Log(s.name);
        }
    }

    //when a player disconnects, update the room properties
    public void OnPhotonPlayerDisconnected(PhotonPlayer other)
     {
         if (PhotonNetwork.room.customProperties["Player1"].Equals(other.ID))
         {
             ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
             h.Add("Player1", -1);
             PhotonNetwork.room.SetCustomProperties(h);
         }
         else if (PhotonNetwork.room.customProperties["Player2"].Equals(other.ID))
         {
             ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
             h.Add("Player2", -1);
             PhotonNetwork.room.SetCustomProperties(h);
         }
         else if (PhotonNetwork.room.customProperties["Player3"].Equals(other.ID))
         {
             ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
             h.Add("Player3", -1);
             PhotonNetwork.room.SetCustomProperties(h);
         }
         else if (PhotonNetwork.room.customProperties["Player4"].Equals(other.ID))
         {
             ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
             h.Add("Player4", -1);
             PhotonNetwork.room.SetCustomProperties(h);
         }
     }

    /*
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() + "\n" + (PhotonNetwork.room == null ? "" : PhotonNetwork.room.name));
    }
     */


    /// <summary>
    /// Hits the server with a login refresh every 5 seconds
    /// This is used to keep a player's online status up to date
    /// </summary>
    /// <returns></returns>
    IEnumerator RefreshLogin()
    {
        WWWForm f = new WWWForm();
        f.AddField("user", VarsTracker.PlayerName);

        WWW UpdateOnline = new WWW(VarsTracker.MinigolfNetworkBasePath + "RefreshLogin.php", f);
        Debug.Log("Login Refreshed");
        yield return new WaitForSeconds(10f);
        StartCoroutine(RefreshLogin());
    }

    /// <summary>
    /// Creates a random room name
    /// </summary>
    /// <returns>random room name</returns>
    private string CreateRandomRoomName()
    {
        string name = "";
        int roomNameLength = Random.Range(5, 12);
        for (int i = 5; i < roomNameLength; i++)
        {
            name += (char)Random.Range(65, 123);
        }
        while (!VerifyRoomName(name))
        {
            name += (char)Random.Range(65, 123);
        }
        return name;
    }

    /// <summary>
    /// Tests if a room exists or not
    /// </summary>
    /// <param name="roomName"></param>
    /// <returns></returns>
    private bool VerifyRoomName(string roomName)
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].name == roomName)
            {
                return false;
            }
        }
        return true;
    }
    

    #endregion
}

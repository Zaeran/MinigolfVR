using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Photon;

public class FriendUIItemCode : Photon.MonoBehaviour {

    public int ListNumber; //compare list number against scroll position to determine collider activation
    public Text friendName;
    public Text friendRoom;
    public Text friendRoomPlayerCount;
    public string actualFriendRoomName;
    FriendMenu menu;

    public void SetParameters(string name, string room, string roomCount, int listNo, FriendMenu baseMenu, bool startCoroutine = true)
    {
        friendName.text = name;
        ListNumber = listNo;
        actualFriendRoomName = room;
        StartCoroutine(SetRoomText(room, roomCount));

        menu = baseMenu;

        if (startCoroutine)
        {
            StartCoroutine(RefreshStatus());
        }
    }

    void Update()
    {
        /*
        if (transform.parent.localPosition.y > (100 * ListNumber) + 1 || transform.parent.localPosition.y < -405 + (100 * ListNumber))
        {
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
        }
         */
    }

    public void Activate()
    {
        GameObject panel;
        if(!friendRoom.text.Contains("LOBBY")){
            panel = Instantiate(Resources.Load(@"Prefabs/Menu/FriendActionMenuOffline"), transform.position, transform.rotation) as GameObject;
        }
        else{
            panel = Instantiate(Resources.Load(@"Prefabs/Menu/FriendActionMenuOnline"), transform.position, transform.rotation) as GameObject;
        }        
        panel.transform.localPosition += new Vector3(0, 0, -1);

        FriendPanel fp = panel.GetComponent<FriendPanel>();
        fp.SetFriend(this);
        
    }

    IEnumerator SetRoomText(string roomName, string roomCount)
    {
        if (roomName == "-OFFLINE")
        {
            friendRoom.text = "OFFLINE";
        }
        else if (roomName == "-")
        {
            friendRoom.text = "ONLINE";
        }
        else
        {
            WWWForm wf = new WWWForm();
            wf.AddField("room", roomName);

            WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "IsRoomLobby.php", wf);
            yield return query;

            Debug.Log("RESULT: " + query.text);

            if (query.text == "YES")
            {
                friendRoom.text = "IN LOBBY";
            }
            else if(query.text == "ERROR1")
            {
                friendRoom.text = "UNKNOWN";
            }
            else
            {
                friendRoom.text = "IN GAME";
            }
        }

        if (roomName != "ONLINE")
        {
            friendRoomPlayerCount.text = roomCount;
        }
    }

    IEnumerator RefreshStatus()
    {
        yield return new WaitForSeconds(5);
        WWWForm wf = new WWWForm();
        wf.AddField("user", friendName.text);
        WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "GetUserStatus.php", wf);
        yield return query;

        if (friendName.text == "ZAERAN")
        {
            Debug.Log("UPDATING: " + query.text);
        }

        SetParameters(friendName.text, query.text, "0", ListNumber, menu, false);
        StartCoroutine(RefreshStatus());
    }
}

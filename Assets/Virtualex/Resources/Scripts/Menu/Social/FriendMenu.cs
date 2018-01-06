using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FriendMenu : Photon.MonoBehaviour {

    public GameObject KeyboardPanel;
    public GameObject FriendListHolder;
    public int noOfFriendsOnline;
    public GameObject selectedFriendUI;

    public Dictionary<int, GameObject> friendUIItems;
    int scrollStartNo;

    public int noOfFriendsInCircle;
    public float friendCircleDistance;

    void OnEnable()
    {
        noOfFriendsOnline = 0;
        friendUIItems = new Dictionary<int, GameObject>();
        StartCoroutine(GetFriendsOnline());
        scrollStartNo = 0;
    }

    void OnDisable()
    {
        selectedFriendUI = null;
        for(int i = 0; i < noOfFriendsOnline; i++){
            Destroy(friendUIItems[i]);
        }
        noOfFriendsOnline = 0;
    }

    void OrderFriendList()
    {
        /*
        GameObject[] friends = friendUIItems.ToArray();
        int nextListNo = 0;
        while (nextListNo < friends.Length - 1)
        {
            for (int i = 0; i < friends.Length; i++)
            {
                if (friends[i].GetComponent<FriendUIItemCode>().ListNumber == nextListNo)
                {
                    friendUIItems[i] = friends[i];
                    nextListNo++;
                }
            }
        }
         */
        //SetSelectedFriendItem(friendUIItems[0]);
    }

    public void ResetFriendMenu()
    {
        OnDisable();
        OnEnable();
    }

    IEnumerator GetFriendsOnline()
    {
        //yield return new WaitForSeconds(1);
        //get friends list
        WWWForm wf = new WWWForm();
        wf.AddField("user", VarsTracker.PlayerName);
        //wf.AddField("user", "ZAE");
        WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "GetFriends.php", wf);

        yield return query;
        Debug.Log(query.text);

        //create UI Item for each friend
        string[] allFriends = query.text.Split(',');
        for (int i = 0; i < allFriends.Length; i++)
        {
            StartCoroutine(GetFriendStatus(allFriends[i], i));
        }
        noOfFriendsOnline = allFriends.Length - 1;

        if (noOfFriendsOnline > 0)
        {
            while (friendUIItems.Count != noOfFriendsOnline)
            {
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    IEnumerator GetFriendStatus(string friendName, int currentListNo)
    {
        WWWForm wf = new WWWForm();
        wf.AddField("user", friendName);
        WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "GetUserStatus.php", wf);
        yield return query;
        if (friendName.Length > 1) //ignore annoying terminating characters that always show up
        {
            string room = query.text;
            string noOfPeopleInRoom = "0"; //PHP -> GET FROM DATABASE
            CreateFriendIDBtn(friendName, room, noOfPeopleInRoom, currentListNo);
            currentListNo++;
        }
    }

    void CreateFriendIDBtn(string name, string room, string noOfusers, int listNo)
    {
        GameObject FriendUI = Instantiate(Resources.Load(@"Prefabs/Menu/FriendUIItemPanel"), Vector3.zero, Quaternion.identity) as GameObject;
        FriendUI.transform.SetParent(FriendListHolder.transform, true);
        FriendUI.transform.localPosition = new Vector3(Mathf.Sin((listNo / 5) * Mathf.Deg2Rad * (360 / noOfFriendsInCircle)) * friendCircleDistance, 15 - (3 * (listNo % 5)), Mathf.Cos((listNo / 5) * Mathf.Deg2Rad * (360 / noOfFriendsInCircle)) * friendCircleDistance);
        FriendUI.transform.LookAt(new Vector3(FriendListHolder.transform.position.x, FriendUI.transform.position.y, FriendListHolder.transform.position.z));
        FriendUI.transform.eulerAngles += new Vector3(0, 180, 0);
        FriendUI.transform.localScale = Vector3.one * 0.025f;
        FriendUI.transform.FindChild("FriendUIItem").GetComponent<Image>().color = new Color(128f / 255f, 205 / 255f, 88 / 255f, 230 / 255f);
        FriendUI.transform.FindChild("FriendUIItem").GetComponent<FriendUIItemCode>().SetParameters(name, room, noOfusers, listNo, this);
        friendUIItems.Add(listNo, FriendUI);
    }

    /*
    public void SetSelectedFriendItem(GameObject friendUI)
    {
        if (selectedFriendUI != null)
        {
            selectedFriendUI.GetComponent<Image>().color = MenuColours.GetColour(MenuColours.ColourType.DarkGreen);
        }
        selectedFriendUI = friendUI;
        if (friendUI != null)
        {
            friendUI.GetComponent<Image>().color = MenuColours.GetColour(MenuColours.ColourType.LightGreen);
        }
    }

     */
 
    public string GetRoomFromSelectedFriend()
    {
        if (selectedFriendUI == null)
        {
            return "";
        }
        return selectedFriendUI.GetComponent<FriendUIItemCode>().actualFriendRoomName;
    }

    public void NavigateFriendUI(bool goUp)
    {
        /*
        FriendUIItemCode selected = selectedFriendUI.GetComponent<FriendUIItemCode>();
        Debug.Log(selected.ListNumber);
        if (goUp && selected.ListNumber != 0)
        {
            SetSelectedFriendItem(friendUIItems[selected.ListNumber - 1]);
        }
        if (!goUp && selected.ListNumber < noOfFriendsOnline - 1)
        {
            SetSelectedFriendItem(friendUIItems[selected.ListNumber + 1]);
        }

        selected = selectedFriendUI.GetComponent<FriendUIItemCode>();
        if (goUp && selected.ListNumber == scrollStartNo && scrollStartNo != 0)
        {
            ScrollPanel.transform.localPosition += new Vector3(0, -100, 0);
            scrollStartNo--;
        }
        else if (!goUp && selected.ListNumber == scrollStartNo + 4 && scrollStartNo != friendUIItems.Count - 1)
        {
            ScrollPanel.transform.localPosition += new Vector3(0, 100, 0);
            scrollStartNo++;
        }
        Debug.Log(selected.ListNumber);
         */
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FriendPanel : MonoBehaviour {

    public FriendUIItemCode friend;
    public Text friendNameText;
    string friendName;

    GameObject friendHolder;
    GameObject friendBaseMenu;

    public void SetFriend(FriendUIItemCode f)
    {
        friend = f;
        friendName = friend.friendName.text;
        friendNameText.text = friendName;
        //not the best way to do this...
        friendHolder = GameObject.Find("FRIENDLISTHOLDER");
        friendBaseMenu = GameObject.Find("FriendBaseMenu");

        friendHolder.SetActive(false);
        friendBaseMenu.SetActive(false);
    }

    public void Back()
    {
        friendHolder.SetActive(true);
        friendBaseMenu.SetActive(true);
        Destroy(gameObject);
    }

    public void JoinFriend()
    {
        GameObject.FindGameObjectWithTag("NetworkController").GetComponent<ConnectToNetwork>().JoinRoom(friend.actualFriendRoomName);
        friendHolder.SetActive(true);
        friendBaseMenu.SetActive(true);
        Destroy(gameObject);
    }

    public void RemoveFriend()
    {
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        WWWForm wf = new WWWForm();
        wf.AddField("friend", friendName);
        wf.AddField("user", VarsTracker.PlayerName);
        WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "RemoveFriend.php", wf);

        yield return query;

        friendHolder.SetActive(true);
        friendBaseMenu.SetActive(true);
        Destroy(gameObject);
    }
}

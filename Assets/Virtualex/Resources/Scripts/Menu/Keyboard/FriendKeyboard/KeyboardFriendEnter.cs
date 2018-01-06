using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyboardFriendEnter : MonoBehaviour {

    public GameObject panel;
    public Text ERRORTEXT;
    public Text FriendName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (FriendName.text.Length > 0)
        {
            //add friend
            StartCoroutine(AddFriend());
        }
        else
        {
            //set focus to friend text
            ERRORTEXT.text = "Input the name of your friend";
        }
    }

    IEnumerator AddFriend()
    {
        WWWForm wf = new WWWForm();
        wf.AddField("user", FriendName.text);
        WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "TestExistingUser.php", wf);

        yield return query;

        if (query.text == "0")
        {
            ERRORTEXT.text = "That user doesn't exist";
        }
        else if (query.text != "EXISTS")
        {
            ERRORTEXT.text = "An error occurred";
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("user", VarsTracker.PlayerName);
            form.AddField("friend", FriendName.text);
            query = new WWW(VarsTracker.MinigolfNetworkBasePath + "AddFriend.php", form);
            yield return query;
            if (query.text == "EXISTS")
            {
                ERRORTEXT.text = "Friend already exists";
            }
            else if (query.text == "FAIL")
            {
                ERRORTEXT.text = "An error occurred";
            }
            else
            {
                ERRORTEXT.text = "Friend added";
                panel.SendMessage("Activate");
            }
        }
    }
}

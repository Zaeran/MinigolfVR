using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoveFriendButton : MonoBehaviour {

    public FriendPanel panel;

    public void Activate()
    {
        panel.RemoveFriend();
    }
}

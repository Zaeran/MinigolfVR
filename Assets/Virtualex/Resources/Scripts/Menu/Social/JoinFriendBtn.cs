using UnityEngine;
using System.Collections;

public class JoinFriendBtn : MonoBehaviour
{

    public FriendPanel panel;

    public void Activate()
    {
        panel.JoinFriend();
    }
}
using UnityEngine;
using System.Collections;

public class FriendBackButton : MonoBehaviour {

    public FriendPanel panel;

    public void Activate()
    {
        panel.Back();
    }
}

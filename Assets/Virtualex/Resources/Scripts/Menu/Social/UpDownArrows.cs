using UnityEngine;
using System.Collections;

public class UpDownArrows : MonoBehaviour {

    public bool isUpArrow;
    public Transform scroll;
    public FriendMenu menu;

    public void Activate()
    {
        menu.NavigateFriendUI(isUpArrow);
    }
}

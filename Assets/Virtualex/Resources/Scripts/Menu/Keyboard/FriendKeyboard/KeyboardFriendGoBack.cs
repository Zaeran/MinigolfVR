using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyboardFriendGoBack : MonoBehaviour {

    public GameObject thisPanel;
    public GameObject prevPanel1;
    public GameObject prevPanel2;
    public Text friendText;
    public Text ERRORTEXT;

    public void Activate()
    {
        friendText.text = "";
        ERRORTEXT.text = "";
        prevPanel1.SetActive(true);
        prevPanel2.SetActive(true);
        thisPanel.SetActive(false);
    }
}

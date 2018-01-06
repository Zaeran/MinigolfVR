using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardUserGoBack : MonoBehaviour {

    public GameObject thisPanel;
    public GameObject prevMenu;
    public Text usernameText;
    public Text ERRORTEXT;

    public void Activate()
    {
        usernameText.text = "";
        ERRORTEXT.text = "";
        prevMenu.SetActive(true);
        thisPanel.SetActive(false);
    }
}

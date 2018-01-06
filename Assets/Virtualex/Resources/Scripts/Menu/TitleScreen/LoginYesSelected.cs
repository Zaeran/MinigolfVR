using UnityEngine;
using System.Collections;

public class LoginYesSelected : MonoBehaviour {

    public LoginHandler handler;
    public GameObject thisPanel;

    public void Activate()
    {
        handler.Activate();
        thisPanel.SetActive(false);
    }
}

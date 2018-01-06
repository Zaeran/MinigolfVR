using UnityEngine;
using System.Collections;
using Photon;

public class SocialOfflineMenu : Photon.MonoBehaviour {

    public GameObject KeyboardPanel;
    public GameObject SocialOnlinePanel;

    public void LoginSuccessful()
    {
        SocialOnlinePanel.SetActive(true);
        gameObject.SetActive(false);
        

        KeyboardPanel.GetComponent<KeyboardPanelCode>().SetToInactive();
    }
}

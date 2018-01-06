using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class SetCorrectMainMenuText : Photon.MonoBehaviour {

    Text menuText;

    void Start()
    {
        menuText = GetComponent<Text>();
    }
	
	void Update () {
        if (PhotonNetwork.inRoom)
        {
            menuText.text = "MULTIPLAYER LOBBY";
        }
        else
        {
            //menuText.text = "PRESS THE RED BUTTON TO START";
            menuText.text = "";
        }
	}
}

using UnityEngine;
using System.Collections;
using Photon;

public class AutoMultiplayerPanel : Photon.MonoBehaviour {

    public GameObject courseSelection;
    public GameObject menu;

    void Update()
    {
        if (PhotonNetwork.inRoom)
        {
            courseSelection.SetActive(true);
            menu.SetActive(false);
        }
    }
}

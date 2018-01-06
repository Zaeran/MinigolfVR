using UnityEngine;
using System.Collections;
using Photon;

public class NewGameBtn : Photon.MonoBehaviour {

    public GameObject courseSelection;
    public GameObject menu;

    public void Activate()
    {
        courseSelection.SetActive(true);
        menu.SetActive(false);
    }
}

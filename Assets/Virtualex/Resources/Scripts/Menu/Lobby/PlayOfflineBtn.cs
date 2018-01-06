using UnityEngine;
using System.Collections;

public class PlayOfflineBtn : MonoBehaviour {

    public GameObject otherPanel;
    public GameObject thisPanel;

    public void Activate()
    {
        otherPanel.SetActive(true);
        thisPanel.SetActive(false);
    }
}

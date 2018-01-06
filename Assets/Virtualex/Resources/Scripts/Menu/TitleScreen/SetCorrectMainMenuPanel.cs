using UnityEngine;
using System.Collections;

public class SetCorrectMainMenuPanel : MonoBehaviour {

    public GameObject onlineMenu;
    public GameObject regularMenu;
    public GameObject courseMenu;


	// Use this for initialization
	void OnEnable () {
        if (PhotonNetwork.connected)
        {
            if (PhotonNetwork.inRoom)
            {
                courseMenu.SetActive(true);
            }
            else
            {
                onlineMenu.SetActive(true);
            }
        }
        else
        {
            regularMenu.SetActive(true);
        }
	}
}

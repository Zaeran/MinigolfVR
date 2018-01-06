using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class GreyedOutWhenOffline : Photon.MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.connected)
        {
            GetComponent<BoxCollider>().enabled = true;
            if (GetComponent<Image>().color == MenuColours.GetColour(MenuColours.ColourType.DarkGreen))
            {
                GetComponent<Image>().color = MenuColours.GetColour(MenuColours.ColourType.LightGreen);
            }
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Image>().color = MenuColours.GetColour(MenuColours.ColourType.DarkGreen);
        }
	}

    public bool isOnline()
    {
        return PhotonNetwork.connected;
    }
}

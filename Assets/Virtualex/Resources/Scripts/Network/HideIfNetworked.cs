using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Attach to an object to hide it if online
/// </summary>
public class HideIfNetworked : Photon.MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.inRoom)
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }
	}
}

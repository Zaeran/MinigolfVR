using UnityEngine;
using System.Collections;

/// <summary>
/// Test script to create a handheld fan deathmatch object
/// </summary>
public class CreateNetworkDeathmatchObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Application.loadedLevelName != "Lobby" && PhotonNetwork.inRoom && PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.InstantiateSceneObject("HandheldFan", transform.position + new Vector3(10, 10, 0), Quaternion.identity, 0, null);
        }
	}
}

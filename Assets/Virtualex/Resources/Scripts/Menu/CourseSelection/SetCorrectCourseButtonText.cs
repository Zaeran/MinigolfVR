using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class SetCorrectCourseButtonText : Photon.MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        if (PhotonNetwork.inRoom)
        {
            GetComponent<Text>().text = "VOTE";
        }
        else
        {
            GetComponent<Text>().text = "SELECT";
        }
	}
}

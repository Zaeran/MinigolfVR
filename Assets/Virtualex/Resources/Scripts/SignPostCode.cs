using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignPostCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GolfHoleParameters ghp = transform.parent.GetComponent<GolfHoleParameters>();
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "HOLE: " + ghp.HoleNumber + "\nPAR: " + ghp.Par;
	}
}

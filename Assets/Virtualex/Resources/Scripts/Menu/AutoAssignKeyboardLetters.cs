using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoAssignKeyboardLetters : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = transform.parent.name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

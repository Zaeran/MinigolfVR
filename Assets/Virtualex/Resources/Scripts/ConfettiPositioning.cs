using UnityEngine;
using System.Collections;

public class ConfettiPositioning : MonoBehaviour {

    public Transform head;

	// Update is called once per frame
	void Update () {
        transform.position = head.position + (Vector3.up * 5.0f);
        transform.eulerAngles = Vector3.zero;
	}
}

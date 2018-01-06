using UnityEngine;
using System.Collections;

public class GentlyFloat : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        transform.position += (Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad)) / 1000;
	}
}

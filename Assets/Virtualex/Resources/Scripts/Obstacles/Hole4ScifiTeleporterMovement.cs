using UnityEngine;
using System.Collections;

/// <summary>
/// Specific script for movement of one teleporter
/// </summary>
public class Hole4ScifiTeleporterMovement : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
        transform.localPosition = new Vector3(Mathf.Sin(Time.fixedTime / 1.5f) * 20, -4.45f, 40.0f);
	}
}

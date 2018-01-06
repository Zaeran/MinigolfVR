using UnityEngine;
using System.Collections;

/// <summary>
/// Rotates the windmill blades
/// </summary>
public class WindmillRotate : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
        transform.RotateAround(GetComponent<MeshCollider>().bounds.center, Vector3.forward, -90 * Time.fixedDeltaTime);
	}
}




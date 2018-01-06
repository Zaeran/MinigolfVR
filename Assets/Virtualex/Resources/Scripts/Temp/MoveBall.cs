using UnityEngine;
using System.Collections;

/// <summary>
/// Attach to the ball to simulate hits
/// Useful for testing without requiring hitting the ball perfectly
/// </summary>
public class MoveBall : MonoBehaviour {

    public Vector3 force;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(force);
        }
	}
}

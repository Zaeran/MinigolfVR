using UnityEngine;
using System.Collections;

public class TrackerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Rigidbody>().velocity.sqrMagnitude > 0)
        {
            Instantiate(Resources.Load("Prefabs/Logger"), transform.position, Quaternion.identity);
        }
	}
}

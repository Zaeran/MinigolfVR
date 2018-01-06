using UnityEngine;
using System.Collections;

/// <summary>
/// Shoots the ball directly away from it on collision
/// </summary>
public class BumperScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision c)
    {
        //only knock the ball back
        if (c.collider.tag == "MyBall")
        {
            c.collider.GetComponent<Rigidbody>().AddForce((c.transform.position - transform.position).normalized * 2500);
        }
    }
}

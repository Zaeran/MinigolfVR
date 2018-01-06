using UnityEngine;
using System.Collections;
/// <summary>
/// Turns off lights when player is far away from them
/// </summary>
public class TurnOffLightsWhenFarAway : MonoBehaviour {

    Transform playerPos;
    public float threshold;

	// Use this for initialization
	void Start () {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, playerPos.position) > threshold)
        {
            GetComponent<Light>().enabled = false;
        }
        else
        {
            GetComponent<Light>().enabled = true;
        }
	}
}

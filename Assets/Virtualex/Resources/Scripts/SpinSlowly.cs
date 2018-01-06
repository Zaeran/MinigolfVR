using UnityEngine;
using System.Collections;

public class SpinSlowly : MonoBehaviour {

    public Vector3 angleToSpin;
    public float degPerSecond;
    public bool selfSpace;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(angleToSpin, degPerSecond * Time.deltaTime, selfSpace ? Space.Self : Space.World);
	}
}

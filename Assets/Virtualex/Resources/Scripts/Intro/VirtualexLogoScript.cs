using UnityEngine;
using System.Collections;

//Handles logo
public class VirtualexLogoScript : MonoBehaviour {

    Light thisLight;
    float waitTimer;

	// Use this for initialization
	void Start () {
        waitTimer = 3;
        thisLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
        else
        {
            thisLight.intensity += Time.deltaTime * 1.5f;
            if (thisLight.intensity >= 7)
            {
                transform.parent.position += Vector3.down * Time.deltaTime / 2f;
            }
        }
	}
}

using UnityEngine;
using System.Collections;

public class AffectedByGravityFields : MonoBehaviour {

    public Vector3 gravityDirection;
    public bool isInGravityField;
    public int currentGravLevel;
    int currentFrame;

    void Start()
    {
        isInGravityField = false;
        currentFrame = 0;
    }

    public void SetGravity(bool inGravityField, Vector3 direction, int gravLevel)
    {
        isInGravityField = inGravityField;
        gravityDirection = direction;
        currentGravLevel = gravLevel;
        Rigidbody r = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void FixedUpdate () {
        currentFrame++;
        if (isInGravityField)// && !Physics.Raycast(transform.position, gravityDirection, 0.51f))
        {
            if (gravityDirection != Vector3.zero)
            {
                GetComponent<Rigidbody>().AddForce(gravityDirection * 98.1f);
            }
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
	}
}

using UnityEngine;
using System.Collections;

/// <summary>
/// Used for XBox Controller input
/// Shows the direction the ball will be shit
/// </summary>
public class GolfBallMarker : MonoBehaviour {

    Transform player;
    Transform image;
    public Transform ball;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        image = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        //orient the marker in the correct direction
        AffectedByGravityFields ballGrav = ball.GetComponent<AffectedByGravityFields>();
        Vector3 markerDirection = -ballGrav.gravityDirection;
        if (markerDirection == Vector3.zero && !ballGrav.isInGravityField)
        {
            markerDirection = Vector3.up;
        }

        transform.position = ball.transform.position + (markerDirection * 1.8f);
        transform.up = markerDirection;
        Vector3 oldEuler = image.localEulerAngles;
        image.LookAt(player);
        Vector3 newEuler = image.localEulerAngles;
        image.localEulerAngles = new Vector3(oldEuler.x, newEuler.y, oldEuler.z);
	}
}

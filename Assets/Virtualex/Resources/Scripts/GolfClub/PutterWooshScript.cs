using UnityEngine;
using System.Collections;

/// <summary>
/// Makes a swoosh sound when using the club
/// </summary>
public class PutterWooshScript : MonoBehaviour {

    VelocityCalculator vel;
    AudioSource a;
    bool hasWooshed;
    public float wooshSpeed;
    public ClubReturnToHand hand;

	// Use this for initialization
	void Start () {
        vel = GetComponent<VelocityCalculator>();
        a = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (vel.estimatedVelocity.magnitude > wooshSpeed && hand.isInHand)
        {
            if (!hasWooshed)
            {
                a.pitch = 0.5f + ((0.25f / 200) * vel.estimatedVelocity.magnitude); //faster the club, higher the woosh
                a.Play();
                hasWooshed = true;
            }
        }
        else if(vel.estimatedVelocity.magnitude < 50) //no woosh if too slow
        {
            hasWooshed = false;
        }
	}
}

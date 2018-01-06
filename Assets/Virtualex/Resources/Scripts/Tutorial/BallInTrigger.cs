using UnityEngine;
using System.Collections;

/// <summary>
/// Detects that the ball has gone in the hole in tutorial
/// </summary>
public class BallInTrigger : MonoBehaviour {

    public TutorialScript tut;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "MyBall")
        {
            tut.BallIn();
        }
    }
}

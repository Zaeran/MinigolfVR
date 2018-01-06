using UnityEngine;
using System.Collections;

/// <summary>
/// Detects that the ball has left the tutorial area
/// </summary>
public class BallOutTrigger : MonoBehaviour {

    public TutorialScript tut;

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.tag == "MyBall")
        {
            tut.BallOut();
        }
    }

}

using UnityEngine;
using System.Collections;

/// <summary>
/// Detects when ball is in area
/// </summary>
public class BallTriggered : MonoBehaviour {

    public TutorialScript tut;
    public bool failBox;
    bool hasTriggered;

    void Start()
    {
        hasTriggered = false;
    }

    void OnTriggerEnter(Collider c)
    {
        if (!hasTriggered)
        {
            if (c.tag == "MyBall")
            {
                if (failBox)
                {
                    if (c.GetComponent<Rigidbody>().velocity.z < 0)
                    {
                        tut.RampFail();
                        hasTriggered = true;
                    }
                }
                else
                {
                    hasTriggered = true;
                    tut.RampDone();
                }
            }
        }
    }
}

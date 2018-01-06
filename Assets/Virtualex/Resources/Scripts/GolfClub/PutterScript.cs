using UnityEngine;
using System.Collections;

public class PutterScript : MonoBehaviour {

    GameObject Ball;
    GameObject BallSafety;  //This is a sphere collider on the ball. When active, it disables ball collisions with the putter
    float timeSinceEnabled;
    Vector3 lastPos;
    ClubReturnToHand golfClub;

    void Start()
    {
        lastPos = transform.position;
        timeSinceEnabled = 0;
        golfClub = transform.parent.gameObject.GetComponent<ClubReturnToHand>();
    }

    public void Teleported()
    {
        //Deactivate putter during teleport, so that putter teleporting inside ball doesn't cause ball to fly off
        //Also enable ball safety
        if (BallSafety != null)
        {
            BallSafety.GetComponent<SphereCollider>().enabled = true;
        }
        lastPos = transform.position;
        timeSinceEnabled = 0;
    }

    /// <summary>
    /// Resets the putter
    /// </summary>
    /// <param name="duration"></param>
    public void ResetPutter(float duration)
    {
        GetComponent<BoxCollider>().enabled = false;
        timeSinceEnabled = 0;
    }

    void FixedUpdate()
    {
        if (Ball == null)
        {
            Ball = GameObject.FindGameObjectWithTag("MyBall");
        }
        if (BallSafety == null && Ball != null)
        {
            BallSafety = Ball.transform.FindChild("BallSafety").gameObject;
        }
        RaycastHit hit;
        if (BallSafety != null) //if ball safety exists
        {
            if (!BallSafety.GetComponent<SphereCollider>().enabled) //if ball safety has a sphere collider
            {
                if (Physics.BoxCast(lastPos, new Vector3(0.01f, 0.01f, 1f), transform.position - lastPos, out hit, transform.rotation, (lastPos - transform.position).magnitude, 1 << LayerMask.NameToLayer("Ball")))
                {
                    if (hit.collider.tag == "MyBall" && !BallSafety.GetComponent<SphereCollider>().enabled)
                    //if we hit the ball, and the ball is not moving, and it's ok to hit the ball
                    {
                        if (Ball == null) //set the ball object
                        {
                            Ball = hit.collider.gameObject;
                        }
                        GetComponent<VelocityCalculator>().Apply(hit.collider.GetComponent<Rigidbody>());
                        BallHit();
                    }
                }
            }
        }

        //disable collider when not holding club
        if (!golfClub.isInHand)
        {
            if (BallSafety != null)
            {
                BallSafety.GetComponent<SphereCollider>().enabled = true;
                timeSinceEnabled = 0;
            }
        }
        //test if ball safety is on
        else if (BallSafety != null)
        {
            if (BallSafety.GetComponent<SphereCollider>().enabled)
            {
                if (GetComponent<BoxCollider>().bounds.Intersects(BallSafety.GetComponent<Renderer>().bounds) && timeSinceEnabled < 0.1f)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    timeSinceEnabled = 0;
                }
                else
                {
                    BallSafety.GetComponent<SphereCollider>().enabled = false;
                    GetComponent<BoxCollider>().enabled = true;
                }
                timeSinceEnabled += Time.fixedDeltaTime;
            }
        }

        lastPos = transform.position;
    }

    void BallHit()
    {
        //remove colliders, make sure ball can't be hit
        BallSafety.GetComponent<SphereCollider>().enabled = true;
        Ball.GetComponent<AudioSource>().volume = (1.0f / 150) * GetComponent<VelocityCalculator>().estimatedVelocity.magnitude;
        Ball.GetComponent<AudioSource>().Play();
        //Ball.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().isTrigger = true;
        //increment score if ball hit above certain speed
        if (GetComponent<VelocityCalculator>().MovementVelocity.magnitude > 3)
        {
            if (!VarsTracker.holesComplete[VarsTracker.currentHole - 1])
            {
                if (PhotonNetwork.inRoom)
                {
                    GameObject.FindGameObjectWithTag("ScoreTracker").GetComponent<IncrementScore>().ScoreUp();
                }
                else
                {
                    VarsTracker.playerScores[0, VarsTracker.currentHole - 1]++;
                }
            }
        }
        Ball.GetComponent<GolfBallScript>().hasBeenInfinite = false;

        StartCoroutine(TriggerHapticPulse(GetComponent<VelocityCalculator>().MovementVelocity.magnitude / 25));
    }

    //resets the putter's collider
    IEnumerator ResetCollider(float duration)
    {
        yield return new WaitForSeconds(duration);
        GetComponent<BoxCollider>().enabled = true;
    }
    //disables the ball safety
    IEnumerator DisableBallSafety()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.FindGameObjectWithTag("BallSafety").GetComponent<SphereCollider>().enabled = false;
    }
    //Fires a haptic pulse on the vive controller
    IEnumerator TriggerHapticPulse(float duration)
    {
        Debug.Log(duration);
        float myDur = duration / 0.02f;
        while (myDur <= duration)
        {
            SteamVR_Controller.Input((int)transform.parent.parent.parent.GetComponent<SteamVR_TrackedObject>().index).TriggerHapticPulse(1000);
            yield return new WaitForSeconds(0.02f);
            myDur++;
        }
    }
}

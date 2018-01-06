using UnityEngine;
using System.Collections;

/// <summary>
/// Handles ball movement
/// </summary>
public class GolfBallScript : MonoBehaviour {

    RaycastHit hit;
    Vector3 ballResetPosition = new Vector3(0, 3f, 0f);
    Vector3[] ballLastPosition = new Vector3[9]{Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};
    GameObject lastTunnel;
    Rigidbody rigid;
    AffectedByGravityFields gravityField;
    public bool hasBeenInfinite;
    float zeroGTimer;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        rigid.maxAngularVelocity = float.PositiveInfinity;
        Physics.solverIterationCount = 100;
        gravityField = GetComponent<AffectedByGravityFields>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //zero g timer
        if (gravityField.isInGravityField && gravityField.gravityDirection == Vector3.zero)
        {
            zeroGTimer -= Time.deltaTime;
        }
        else
        {
            zeroGTimer = 10;
        }
        //if in zero g too long, ball is 'out'
        if (zeroGTimer <= 0)
        {
            ResetBallPosition();
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/ErrorNoise") as AudioClip);
        }
        //custom drag based on ball velocity
        if (rigid.useGravity || (gravityField.isInGravityField && gravityField.gravityDirection != Vector3.zero))
        {
            if ( rigid.angularDrag != Mathf.Infinity || hasBeenInfinite)
            {
                rigid.angularDrag = 6f / rigid.velocity.magnitude;
                rigid.drag = 0.1f;
            }
        }
        else
        {
            rigid.angularDrag = 0;
            rigid.drag = 0;
        }
        if (rigid.angularDrag > 400 && !hasBeenInfinite)
        {
            rigid.angularDrag = Mathf.Infinity;
            rigid.drag = Mathf.Infinity;
            hasBeenInfinite = true;
        }
        //ball out if below -50y
        if (transform.position.y < -50)
        {
            ResetBallPosition();
        }
        //set position ball resets to
        if (rigid.angularDrag == Mathf.Infinity)
        {
            if (GetComponent<AffectedByGravityFields>().isInGravityField)
            {
                ballLastPosition[VarsTracker.currentHole - 1] = transform.position - gravityField.gravityDirection * 0.5f;
            }
            else
            {
                ballLastPosition[VarsTracker.currentHole - 1] = transform.position + Vector3.up * 0.5f;
            }
            VarsTracker.ballPositions[VarsTracker.currentHole - 1] = ballLastPosition[VarsTracker.currentHole - 1];
        }
        //make tunnels see-through
        if (Physics.Raycast(transform.position, gravityField.isInGravityField ? -gravityField.gravityDirection : Vector3.up, out hit, 5.0f))
        {
            if (hit.collider.tag == "Obstacle")
            {
                lastTunnel = hit.collider.gameObject;
                hit.collider.GetComponent<Renderer>().material = Resources.Load("Materials/BrownTrans") as Material;
            }
        }
        else if(lastTunnel != null)
        {
            lastTunnel.GetComponent<Renderer>().material = Resources.Load("Materials/Brown") as Material;
        }
	}

    /// <summary>
    /// Moves the ball to the correct position on a course
    /// Keeps velocity and position
    /// </summary>
    public void MoveBallToCourse()
    {
        GetComponent<DontGoThroughThings>().MissFiveSteps();
        transform.position = VarsTracker.ballPositions[VarsTracker.currentHole - 1];
        rigid.velocity = VarsTracker.ballVelocities[VarsTracker.currentHole - 1];
        rigid.angularVelocity = VarsTracker.ballAngulars[VarsTracker.currentHole - 1];
        rigid.useGravity = VarsTracker.ballGravity[VarsTracker.currentHole - 1];
        rigid.drag = 0.1f;
        rigid.useGravity = true;
        gravityField.currentGravLevel = 0;
        Transform safety = transform.FindChild("BallSafety");
        if (safety != null)
        {
            safety.GetComponent<SphereCollider>().enabled = true;
        }
    }

    /// <summary>
    /// Moves ball to a given position on a course
    /// </summary>
    /// <param name="markerposition">Position on the course, usually the starting marker</param>
    public void MoveBallToCourse(Vector3 markerposition)
    {
        GetComponent<DontGoThroughThings>().MissFiveSteps();
        transform.position = markerposition + Vector3.up;
        if (rigid != null)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            rigid.drag = 0.1f;
            rigid.useGravity = VarsTracker.ballGravity[VarsTracker.currentHole - 1];
        }
        if (gravityField != null)
        {
            gravityField.currentGravLevel = 0;
        }
    }

    //collisions
    void OnCollisionEnter(Collision c)
    {
        if (c.collider.tag == "OutZone")
        {
            ResetBallPosition();
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/ErrorNoise") as AudioClip);
        }
        else if (c.other.tag != "Course")
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().volume = (c.relativeVelocity.magnitude / 50);
            Debug.Log(c.relativeVelocity.magnitude);
            GetComponent<AudioSource>().Play();
        }
        rigid.drag = 0.1f;
    }

    /// <summary>
    /// Puts the ball where it was before the last shot
    /// </summary>
    void ResetBallPosition()
    {
        GetComponent<DontGoThroughThings>().MissFiveSteps();
        if (ballLastPosition[VarsTracker.currentHole - 1] == Vector3.zero)
        {
            transform.position = GameObject.Find("Hole" + VarsTracker.currentHole).transform.FindChild("Course").transform.position + (Vector3.up * 5);
        }
        else
        {
            transform.position = ballLastPosition[VarsTracker.currentHole - 1];
        }
        
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        rigid.useGravity = true;
    }

    //Triggers
    void OnTriggerEnter(Collider c)
    {
        switch (c.tag)
        {
            case "Hole":
                c.enabled = false;
                VarsTracker.holesComplete[VarsTracker.currentHole - 1] = true;
                GetComponent<ConfettiFeedback>().BallIn(VarsTracker.playerID);
                GetComponent<AudioSource>().volume = 1;
                GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/HoleComplete") as AudioClip);
                TestAllBallsIn();
                break;
            case "Booster":
                if (rigid.velocity.magnitude < 100)
                {
                    rigid.velocity = c.transform.forward * 100;
                }
                break;
            case "Water":
                Instantiate(Resources.Load("Prefabs/WaterSplash"), transform.position, Quaternion.Euler(-90,0,0));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Check that all holes are completed to display the scoreboard
    /// </summary>
    void TestAllBallsIn()
    {
        int noOfCompleteHoles = 0;
        for (int i = 0; i < VarsTracker.holesComplete.Length; i++)
        {
            noOfCompleteHoles += VarsTracker.holesComplete[i] ? 1 : 0;
        }

        if (noOfCompleteHoles == VarsTracker.holesComplete.Length)
        {
            Transform scoreboardLocation = GameObject.Find("ScoreboardLocation").transform;
            if (PhotonNetwork.inRoom)
            {
                Instantiate(Resources.Load(@"Prefabs/ScoreBoardOnline"), scoreboardLocation.position, scoreboardLocation.rotation);
                GameObject.FindGameObjectWithTag("NetworkController").GetComponent<PlayerFinishedCourse>().CourseComplete();
            }
            else
            {
                Instantiate(Resources.Load(@"Prefabs/ScoreBoardOffline"), scoreboardLocation.position, scoreboardLocation.rotation);
            } 
        }
    }
}

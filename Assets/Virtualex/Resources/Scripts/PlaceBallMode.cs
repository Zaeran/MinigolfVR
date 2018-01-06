using UnityEngine;
using System.Collections;
using Photon;

public class PlaceBallMode : Photon.MonoBehaviour {

    GameObject myBall;
    Transform ballMarker;

    public bool PlaceBall;

    void Start()
    {
        if (PlaceBall)
        {
            Activate(null);
        }
    }

    public void Activate(GameObject hole) //add in the correct course to get the marker for
    {
        if (hole != null)
        {
            ballMarker = hole.transform.FindChild("BallPlacementMarker");
        }
        else
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("BallPlacement"))
            {
                if (Vector3.Distance(g.transform.position, transform.position) < 100)
                {
                    ballMarker = g.transform;
                    break;
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("MyBall") == null && ballMarker != null)
        {
            if (PhotonNetwork.connected && PhotonNetwork.inRoom) //create networked ball
            {
                myBall = PhotonNetwork.Instantiate("Ball", Vector3.up * 5, Quaternion.identity, 0);
                myBall.tag = "MyBall";

                myBall.AddComponent<Rigidbody>();
                myBall.GetComponent<Rigidbody>().angularDrag = 20;
                myBall.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
                myBall.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
                
                myBall.AddComponent<SphereCollider>();
                myBall.GetComponent<SphereCollider>().material = Resources.Load(@"PhysicMaterials/GolfBallPhysics") as PhysicMaterial;

                myBall.AddComponent<GolfBallScript>();

                myBall.AddComponent<DontGoThroughThings>();
                myBall.GetComponent<DontGoThroughThings>().layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Course"));
                myBall.GetComponent<DontGoThroughThings>().skinWidth = 0.001f;

                myBall.AddComponent<AffectedByGravityFields>();

                GameObject ballSafety = Instantiate(Resources.Load(@"Prefabs/BallSafety"), Vector3.zero, Quaternion.identity) as GameObject;
                ballSafety.transform.parent = myBall.transform;
                ballSafety.transform.localPosition = Vector3.zero;
                ballSafety.name = "BallSafety";

                GameObject golfballMarker = Instantiate(Resources.Load(@"Prefabs/BallMarker"), Vector3.zero, Quaternion.identity) as GameObject;
                golfballMarker.transform.parent = myBall.transform;
                golfballMarker.GetComponent<GolfBallMarker>().ball = myBall.transform;
            }
            else //create non-networked ball
            {
                myBall = Instantiate(Resources.Load(@"Prefabs/Ball"), Vector3.up * 5, Quaternion.identity) as GameObject;
            }
        }
        else
        {
            myBall = GameObject.FindGameObjectWithTag("MyBall");
        }
        if (ballMarker != null)
        {
            myBall.GetComponent<GolfBallScript>().MoveBallToCourse(ballMarker.transform.position);
            Destroy(ballMarker.gameObject);
            ballMarker = null;
        }
        else
        {
            myBall.GetComponent<GolfBallScript>().MoveBallToCourse();
        }
    }
}

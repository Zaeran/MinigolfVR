using UnityEngine;
using System.Collections;

public class SetNewHole : MonoBehaviour {

    public void SetHoleFromTeleport(GameObject closestHole)
    {
        GameObject btn = closestHole;
        if (btn != null)
        {
            //move to hole
            int prevHole = VarsTracker.currentHole - 1;
            //set current hole
            VarsTracker.currentHole = btn.GetComponent<GolfHoleParameters>().HoleNumber;
            //place ball if not yet placed
            GameObject ball = GameObject.FindGameObjectWithTag("MyBall");
            if (VarsTracker.playerScores[VarsTracker.playerID, VarsTracker.currentHole - 1] == 0 && VarsTracker.ballPositions[VarsTracker.currentHole - 1] == Vector3.zero)
            {
                VarsTracker.ballPositions[prevHole] = ball.transform.position;
                VarsTracker.ballVelocities[prevHole] = ball.GetComponent<Rigidbody>().velocity;
                VarsTracker.ballAngulars[prevHole] = ball.GetComponent<Rigidbody>().angularVelocity;
                VarsTracker.ballGravity[prevHole] = ball.GetComponent<Rigidbody>().useGravity;
                GetComponent<PlaceBallMode>().Activate(closestHole);
            }
            else
            {
                VarsTracker.ballPositions[prevHole] = ball.transform.position;
                VarsTracker.ballVelocities[prevHole] = ball.GetComponent<Rigidbody>().velocity;
                VarsTracker.ballAngulars[prevHole] = ball.GetComponent<Rigidbody>().angularVelocity;
                VarsTracker.ballGravity[prevHole] = ball.GetComponent<Rigidbody>().useGravity;
                ball.GetComponent<GolfBallScript>().MoveBallToCourse();
            }
        }
    }
}

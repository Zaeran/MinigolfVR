using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGameBtn : MonoBehaviour {

    public SceneVoter voter;
    public Text ERRORTEXT;

    void OnEnable()
    {
        if (!PhotonNetwork.inRoom)
        {
            ERRORTEXT.text = "SELECT A COURSE, THEN PRESS START";
        }
        else
        {
            if (!PhotonNetwork.isMasterClient)
            {
                ERRORTEXT.text = "CAST YOUR VOTE";
            }
            else
            {
                ERRORTEXT.text = "";
            }
        }
    }

    void Update()
    {
        if (PhotonNetwork.connected)
        {
            if (PhotonNetwork.inRoom && PhotonNetwork.isMasterClient)
            {
                if (voter.noOfVotes == PhotonNetwork.room.playerCount)
                {
                    ERRORTEXT.text = "PRESS START TO BEGIN";
                }
                else
                {
                    ERRORTEXT.text = "NOT ALL PLAYERS HAVE VOTED";
                }
            }
        }
    }

    void Activate()
    {
        string courseToLoad = voter.EvaluateVote();
        if (PhotonNetwork.inRoom && courseToLoad != "")
        {
            if (voter.noOfVotes == PhotonNetwork.room.playerCount) //everyone must vote before game can be started
            {
                GameObject.FindGameObjectWithTag("NetworkController").GetComponent<MasterUserStart>().Activate(courseToLoad);
            }
        }
        else if (courseToLoad != "")
        {
            VarsTracker.CurrentCourseName = courseToLoad;
            Application.LoadLevel(courseToLoad);
        }
        
    }
}

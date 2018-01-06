using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Photon;

public class SceneVoter : PunBehaviour
{

    Dictionary<string, int> votes;
    public CourseSelectionInfo[] coursePanels;
    string currentVote;
    public int noOfVotes;
    public Text ERRORTEXT;

	// Use this for initialization
	void OnEnable () {
        ResetVote();
	}

    public string EvaluateVote()
    {
        string courseToLoad = "";
        if (PhotonNetwork.inRoom)
        {
            int voteTotal = 0;
            foreach (string course in votes.Keys)
            {
                if (votes[course] > voteTotal)
                {
                    courseToLoad = course;
                    voteTotal = votes[course];
                }
                else if (votes[course] == voteTotal)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        courseToLoad = course;
                    }
                }
            }
        }
        else
        {
            courseToLoad = currentVote;
        }

        return courseToLoad;
    }

    public void ResetVote()
    {
        noOfVotes = 0;
        votes = new Dictionary<string, int>();
        currentVote = "";
        foreach (CourseSelectionInfo csi in coursePanels)
        {
            votes.Add(csi.courseName, 0);
        }
        UpdateVoteNumber();
    }

    public void UpdateVoteNumber()
    {
        foreach (CourseSelectionInfo csi in coursePanels)
        {
            if (PhotonNetwork.connected && PhotonNetwork.inRoom)
            {
                csi.voteText.text = votes[csi.courseName].ToString();
            }
            else
            {
                csi.voteText.text = "";
            }
        }
    }

    public void AddVote(string course)
    {
        if (currentVote != "")
        {
            if (PhotonNetwork.inRoom)
            {
                photonView.RPC("RemoveVoteNetwork", PhotonTargets.All, currentVote);
            }
        }
        if (PhotonNetwork.inRoom)
        {
            photonView.RPC("AddVoteNetwork", PhotonTargets.All, course);
        }
        else
        {
            ERRORTEXT.text = course;
        }
        currentVote = course;
    }

    public void RemoveVote(string course)
    {
        photonView.RPC("RemoveVoteNetwork", PhotonTargets.All, course);
    }

    //send new players te current vote count
    void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("ReceiveVotes", player, SerializeVotes());
        }
    }

    [PunRPC]
    void ReceiveVotes(string voteString)
    {
        StartCoroutine(DeSerializeVotes(voteString));
    }

    [PunRPC]
    void AddVoteNetwork(string course)
    {
        votes[course]++;
        noOfVotes++;
        UpdateVoteNumber();
    }

    [PunRPC]
    void RemoveVoteNetwork(string course)
    {
        votes[course]--;
        noOfVotes--;
        UpdateVoteNumber();
    }

    private string SerializeVotes()
    {
        string voteString = "";
        foreach (string v in votes.Keys)
        {
            voteString += v + "," + votes[v].ToString() + "\n";
        }
        return voteString;
    }

    IEnumerator DeSerializeVotes(string voteString)
    {
        Debug.Log("VOTES:\n" + voteString);
        yield return new WaitForSeconds(0.5f);
        string[] allVotes = voteString.Split('\n');
        foreach (string s in allVotes)
        {
            if (s.Length > 1)
            {
                string[] split = s.Split(',');
                votes[split[0]] = int.Parse(split[1]);
            }
        }
        UpdateVoteNumber();
    }
}

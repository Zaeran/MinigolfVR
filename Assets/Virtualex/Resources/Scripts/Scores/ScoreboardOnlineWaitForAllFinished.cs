using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Scoreboard not visible until everyone is finished
/// </summary>
public class ScoreboardOnlineWaitForAllFinished : Photon.MonoBehaviour {

    PlayerFinishedCourse pfc;
    bool swapped;
    public GameObject thisText;
    public GameObject scoreboard;

    void Start()
    {
        swapped = false;
        pfc = GameObject.FindGameObjectWithTag("NetworkController").GetComponent<PlayerFinishedCourse>();
    }

    void Update()
    {
        if (!swapped)
        {
            if (pfc.allPlayersDone())
            {
                scoreboard.SetActive(true);
                thisText.SetActive(false);
                swapped = true;
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Set the UI text for current score on golf club
/// </summary>
public class ScoreUI : MonoBehaviour {

    public int holeNumber;
    Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
    }

    void Update()
    {
        if (PhotonNetwork.inRoom)
        {
            myText.text = "Hole " + holeNumber + ": " + VarsTracker.playerScores[VarsTracker.playerID, holeNumber - 1];
        }
        else
        {
            myText.text = "Hole " + holeNumber + ": " + VarsTracker.playerScores[0, holeNumber - 1];
        }
    }
}

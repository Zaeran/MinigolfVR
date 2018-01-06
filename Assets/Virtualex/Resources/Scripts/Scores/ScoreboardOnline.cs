using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Show scores for all online players
/// </summary>
public class ScoreboardOnline : MonoBehaviour {

    public GameObject[] players;
    string[] playerNames;
    List<ScoreTotal> scores;
    ScoreTotal[] orderedScores;

	// Use this for initialization
	void OnEnable () {
        playerNames = new string[4];
        scores = new List<ScoreTotal>();
        orderedScores = new ScoreTotal[]{null, null, null, null};

        SetPlayerNames();
        CalculateTotalScores();
        //OrderScores();
        DisplayScores();
	}

    /// <summary>
    /// Gets names of each player in room
    /// </summary>
    void SetPlayerNames()
    {
        if (PhotonNetwork.room.customProperties.ContainsKey("Player1"))
        {
            for (int i = 1; i <= 4; i++)
            {
                if ((int)PhotonNetwork.room.customProperties["Player" + i.ToString()] != -1)
                {
                    playerNames[i - 1] = PhotonPlayer.Find((int)PhotonNetwork.room.customProperties["Player" + i.ToString()]).name;
                }
            }
        }
    }

    /// <summary>
    /// Calculates the score for each player
    /// </summary>
    void CalculateTotalScores()
    {
        for (int playerID = 0; playerID < VarsTracker.playerScores.GetLength(0); playerID++)
        {
            int totalScore = 0;
            for (int hole = 0; hole < VarsTracker.playerScores.GetLength(1); hole++)
            {
                totalScore += VarsTracker.playerScores[playerID, hole];
            }
            if (totalScore >= 9)
            {
                scores.Add(new ScoreTotal(playerNames[playerID], playerID, totalScore));
            }
        }
    }

    /// <summary>
    /// Puts the scores in numerical order
    /// </summary>
    void OrderScores()
    {
        int highScore = int.MaxValue;
        for(int i = 0; i < 4; i++){
            ScoreTotal highScorer = null;
            foreach (ScoreTotal st in scores)
            {
                if (st.totalScore < highScore)
                {
                    highScore = st.totalScore;
                    highScorer = st;
                }
            }
            orderedScores[i] = highScorer;
            scores.Remove(highScorer);
        }
    }

    /// <summary>
    /// Shows the scores
    /// </summary>
    void DisplayScores()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (i < scores.Count)
            {
                players[i].SetActive(true);
                players[i].GetComponent<ScoreboardMultiplayerUI>().ShowScore(scores[i].playerName, scores[i].playerID, scores[i].totalScore);
            }
        }
    }

    /// <summary>
    /// Class to hold the total score
    /// </summary>
    public class ScoreTotal
    {
        public string playerName;
        public int playerID;
        public int totalScore;

        public ScoreTotal(string name, int ID, int total)
        {
            playerName = name;
            playerID = ID;
            totalScore = total;
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Multiplayer offline scoreboard
/// </summary>
public class ScoreboardOffline : MonoBehaviour {

    OnlineScore[] highScores;
    OnlineScore myScore;
    public GameObject[] highScorePlayer;
    public GameObject thisPlayer;
    public Text thisPlayerLeaderboardText;
    int thisPlayerPosition;

	// Use this for initialization
	void OnEnable () {
        //if connected, show online leaderboard
        if (PhotonNetwork.connected)
        {
            highScores = new OnlineScore[10];
            CalculateTotalScore();
            StartCoroutine(UpdateThenPopulate());
        }
        //otherwise only show player score
        else
        {
            CalculateTotalScore();
            SetPlayerScoreText();
        }
	}

    /// <summary>
    /// Calculate player's score
    /// </summary>
    void CalculateTotalScore()
    {
        int totalScore = 0;
        for (int hole = 0; hole < VarsTracker.playerScores.GetLength(1); hole++)
        {
            totalScore += VarsTracker.playerScores[0, hole];
        }
        myScore = new OnlineScore(PhotonNetwork.connected ? VarsTracker.PlayerName : "YOUR SCORE", totalScore);
    }

    //Send score to database, then show table
    IEnumerator UpdateThenPopulate()
    {
        UpdateHighScore();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(GetPlayerLeaderboardPosition());
        StartCoroutine(PopulateScoreTable());
    }

    //Get scores from database
    IEnumerator PopulateScoreTable()
    {
        WWWForm f = new WWWForm();
        f.AddField("Course", Application.loadedLevelName);

        WWW GetScores = new WWW(VarsTracker.MinigolfNetworkBasePath + "GetHighScores.php", f);
        yield return GetScores;

        string s = GetScores.text;
        string[] names = s.Split('\n');
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Length > 1)
            {
                string[] split = names[i].Split(',');
                highScores[i] = new OnlineScore(split[0], int.Parse(split[1]));
            }
        }
        SetHighScoreText();
    }

    //Get player's position on leaderboard
    IEnumerator GetPlayerLeaderboardPosition()
    {
        WWWForm f = new WWWForm();
        f.AddField("user", VarsTracker.PlayerName);
        f.AddField("course", Application.loadedLevelName);

        WWW pos = new WWW(VarsTracker.MinigolfNetworkBasePath + "GetPositionInHighScoreTable.php", f);
        yield return pos;
        Debug.Log(pos.text);
        string[] split = pos.text.Split(',');
        thisPlayerPosition = int.Parse(split[0]);
        SetPlayerScoreText();
    }

    /// <summary>
    /// Sets the text for a high score
    /// </summary>
    void SetHighScoreText()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (highScores[i] != null)
            {
                highScorePlayer[i].GetComponent<ScoreboardSinglePlayerUI>().ShowScore(highScores[i].playerName, highScores[i].totalScore);
            }
        }
    }

    /// <summary>
    /// Sets the text for the player's score
    /// </summary>
    void SetPlayerScoreText()
    {
        thisPlayer.GetComponent<ScoreboardSinglePlayerUI>().ShowScore(myScore.playerName, myScore.totalScore);
        thisPlayerLeaderboardText.text = thisPlayerPosition.ToString();
    }

    /// <summary>
    /// Sets a high score for the player
    /// </summary>
    void UpdateHighScore()
    {
        Debug.Log("SETTING HIGH SCORE: " + myScore.playerName + ", " + myScore.totalScore);
        WWWForm f = new WWWForm();
        f.AddField("user", myScore.playerName);
        f.AddField("score", myScore.totalScore);
        f.AddField("course", Application.loadedLevelName);
        f.AddField("vfc", "QWERTYTREWQ");
        WWW UpdateOnline = new WWW(VarsTracker.MinigolfNetworkBasePath + "UpdateHighScore.php", f);
    }
    
    public class OnlineScore
    {
        public string playerName;
        public int totalScore;

        public OnlineScore(string name, int score)
        {
            playerName = name;
            totalScore = score;
        }
    }
}

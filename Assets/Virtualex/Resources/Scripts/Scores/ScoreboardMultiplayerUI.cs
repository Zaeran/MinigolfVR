using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The golf club score UI for multiplayer games
/// </summary>
public class ScoreboardMultiplayerUI : MonoBehaviour
{

    public Text[] scoreText;
    public Text playerNameText;
    public Text totalScoreText;

    public void ShowScore(string playerName, int playerID, int totalScore)
    {
        playerNameText.text = playerName;
        if (PhotonNetwork.inRoom)
        {
            for (int i = 0; i < scoreText.Length; i++)
            {
                scoreText[i].text = VarsTracker.playerScores[playerID, i].ToString();
            }
        }
        totalScoreText.text = totalScore.ToString();
    }
}

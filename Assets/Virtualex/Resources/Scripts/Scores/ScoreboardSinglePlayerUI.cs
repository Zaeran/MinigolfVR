using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Single player scoreboard text
/// </summary>
public class ScoreboardSinglePlayerUI : MonoBehaviour {

    public Text playerNameText;
    public Text totalScoreText;

    public void ShowScore(string playerName, int totalScore)
    {
        playerNameText.text = playerName;
        totalScoreText.text = totalScore.ToString();
    }
}

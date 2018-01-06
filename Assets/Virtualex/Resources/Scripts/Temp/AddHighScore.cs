using UnityEngine;
using System.Collections;

/// <summary>
/// Test script to add a high score to the table
/// </summary>
public class AddHighScore : MonoBehaviour {

    public string user;
    public string course;
    public int score;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(HighScore());
        }
	}

    IEnumerator HighScore()
    {
        Debug.Log("SETTING HIGH SCORE: "+ user + ", " + score);
        WWWForm f = new WWWForm();
        f.AddField("user", user);
        f.AddField("score", score);
        f.AddField("course", course);
        f.AddField("vfc", "QWERTYTREWQ");
        WWW UpdateOnline = new WWW(VarsTracker.MinigolfNetworkBasePath + "UpdateHighScore.php", f);
        yield return UpdateOnline;
        Debug.Log(UpdateOnline.text);
    }
}

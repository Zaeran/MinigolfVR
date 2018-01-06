using UnityEngine;
using System.Collections;

/// <summary>
/// Test script to view user's high score position
/// </summary>
public class GethighScorepos : MonoBehaviour {

    public string user;
    public string course;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(HighScore());
        }
    }

    IEnumerator HighScore()
    {
        WWWForm f = new WWWForm();
        f.AddField("user", user);
        f.AddField("course", course);

        WWW pos = new WWW(VarsTracker.MinigolfNetworkBasePath + "GetPositionInHighScoreTable.php", f);
        yield return pos;
        Debug.Log(pos.text);
    }
}

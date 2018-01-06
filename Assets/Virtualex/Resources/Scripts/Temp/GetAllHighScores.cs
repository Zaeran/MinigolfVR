using UnityEngine;
using System.Collections;

/// <summary>
/// Test script to view all high scores
/// </summary>
public class GetAllHighScores : MonoBehaviour {

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
        f.AddField("course", course);
        WWW UpdateOnline = new WWW(VarsTracker.MinigolfNetworkBasePath + "GetHighScores.php", f);
        yield return UpdateOnline;
        Debug.Log(UpdateOnline.text);
    }
}

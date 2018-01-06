using UnityEngine;
using System.Collections;

/// <summary>
/// Auto-load tutorial if first time playing
/// </summary>
public class ChangeSceneOnComplete : MonoBehaviour {

    public string levelToLoad;

	// Update is called once per frame
	void Update () {
        if (transform.position.y < -7.5f)
        {
            if (PlayerPrefs.HasKey("PlayedBefore"))
            {
                Application.LoadLevel(levelToLoad);
            }
            else
            {
                PlayerPrefs.SetInt("PlayedBefore", 1);
                PlayerPrefs.Save();
                Application.LoadLevel("TutorialScene");
            }
        }
	}
}

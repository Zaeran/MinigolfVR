using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour {

    public Text thisText;

    public void Activate()
    {
        VarsTracker.PlayMusic = !VarsTracker.PlayMusic;
        thisText.text = "MUSIC: " + (VarsTracker.PlayMusic ? "ON" : "OFF");
    }
}

using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {

    AudioSource src;
    public bool playRandom;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    void Update()
    {
        //don't play music in tutorial
        if (VarsTracker.PlayMusic && Application.loadedLevelName == "TutorialScene")
        {
            if (src.isPlaying)
            {
                src.Stop();
            }
        }

        if (VarsTracker.PlayMusic && Application.loadedLevelName != "IntroScene" && Application.loadedLevelName != "TutorialScene")
        {
            if (!src.isPlaying)
            {
                if (playRandom)
                {
                    src.clip = GetComponent<MusicRepository>().PickRandomClip();
                }
                src.Play();
            }
        }
        else
        {
            if (src.isPlaying)
            {
                src.Stop();
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class MusicRepository : MonoBehaviour {

    public AudioClip[] clips;

    public AudioClip PickRandomClip()
    {
        if (clips.Length > 0)
        {
            return clips[Random.Range(0, clips.Length)];
        }
        return null;
    }
}

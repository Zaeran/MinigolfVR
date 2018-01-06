using UnityEngine;
using System.Collections;

public class MoveToLevel : MonoBehaviour {

    public string LevelName;

	// Use this for initialization
	void Start () {
        Application.LoadLevel(LevelName);
        Destroy(this);
	}
}

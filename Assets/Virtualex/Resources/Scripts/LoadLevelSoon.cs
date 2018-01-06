using UnityEngine;
using System.Collections;

public class LoadLevelSoon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(swapLevel());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator swapLevel()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel("Level1");
    }
}

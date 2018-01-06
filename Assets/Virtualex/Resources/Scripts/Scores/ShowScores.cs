using UnityEngine;
using System.Collections;

/// <summary>
/// Activate the club score UI
/// </summary>
public class ShowScores : MonoBehaviour {

    public ClubReturnToHand hand;
    public GameObject UI;
	
	// Update is called once per frame
	void Update () {
        UI.SetActive((Application.loadedLevelName != "LoginScene" && Application.loadedLevelName != "TutorialScene") && hand.isInHand);
	}
}

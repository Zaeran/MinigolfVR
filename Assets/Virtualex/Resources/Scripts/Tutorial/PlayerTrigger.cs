using UnityEngine;
using System.Collections;

/// <summary>
/// Detects when player is inside area
/// </summary>
public class PlayerTrigger : MonoBehaviour {

    public TutorialScript tut;
    public Transform player;

	// Update is called once per frame
	void Update () {
        if (GetComponent<MeshRenderer>().bounds.Contains(player.position))
        {
            tut.TeleportDone();
            Destroy(gameObject);
        }
	}
}

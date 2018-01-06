using UnityEngine;
using System.Collections;

/// <summary>
/// Keeps the head of the golf club on the ground for shorter players
/// </summary>
public class ClubDontGoThroughCourse : MonoBehaviour {

	// Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<ClubReturnToHand>().isInHand)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.parent.parent.position, transform.parent.parent.forward, out hit, 9.1f, (1 << LayerMask.NameToLayer("Course")) + (1 << LayerMask.NameToLayer("ClubRaycast"))))
            {
                transform.localPosition = Vector3.zero + new Vector3((9.1f - hit.distance) / 10, 0, 0);
            }
        }
    }
}

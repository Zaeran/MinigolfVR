using UnityEngine;
using System.Collections;

/// <summary>
/// causes a teleporter to flicker between on and off
/// </summary>
public class ShortOutTeleporter : MonoBehaviour {

    Teleporter tp;
    public float activateTime;
    public float deActivateTime;


    void Start()
    {
        tp = GetComponent<Teleporter>();
        StartCoroutine(DeActivateTeleport());
    }

    IEnumerator ActivateTeleport()
    {
        yield return new WaitForSeconds(activateTime);
        tp.Activate();
        StartCoroutine(DeActivateTeleport());
    }
    IEnumerator DeActivateTeleport()
    {
        yield return new WaitForSeconds(deActivateTime);
        tp.DeActivate();
        StartCoroutine(ActivateTeleport());
    }
}

using UnityEngine;
using System.Collections;

/// <summary>
/// Teleports player
/// </summary>
public class TeleportScript : MonoBehaviour {

    public Transform headPos;

    public void Teleport(Vector3 pos)
    {
        //transform.RotateAround(headPos.position, Vector3.up, angle);
        Vector3 offset = new Vector3(headPos.position.x - transform.position.x, 0, headPos.position.z - transform.position.z);
        transform.position = pos - offset;
    }
}

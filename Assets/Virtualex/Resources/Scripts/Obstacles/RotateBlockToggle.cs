using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// A Rotates an object 90 degrees on activation
/// </summary>
public class RotateBlockToggle : Photon.MonoBehaviour
{

    public Transform block;
    bool canEnable = true;

    void OnTriggerEnter()
    {
        if (canEnable)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("ToggleObjects", PhotonTargets.All);
            }
            else
            {
                ToggleObjects();
            }
        }
    }

    IEnumerator TriggerCD()
    {
        yield return new WaitForSeconds(2.5f);
        canEnable = true;
    }

    [PunRPC]
    public void ToggleObjects()
    {
        canEnable = false;
        StartCoroutine(TriggerCD());
        block.Rotate(transform.up, 90, Space.Self);
    }

}

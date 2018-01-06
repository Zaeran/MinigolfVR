using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

/// <summary>
/// Shows the player's name over the network
/// </summary>
public class HeadShowNameScript : Photon.MonoBehaviour
{

    public Text t;

    void Start()
    {
        photonView.RPC("InitialiseNameCanvas", PhotonTargets.All);
    }

    [PunRPC]
    public void InitialiseNameCanvas()
    {
        t.text = photonView.owner.name;
        if (transform.childCount != 0)
        {
            transform.GetChild(0).parent = null;
        }
    }
}

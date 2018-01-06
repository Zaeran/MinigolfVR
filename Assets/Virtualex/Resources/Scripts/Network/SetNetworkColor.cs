using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Sets the colour of a player based on their ID
/// </summary>
public class SetNetworkColor : Photon.MonoBehaviour {

    void Start()
    {
        photonView.RPC("RequestNewColour", PhotonPlayer.Find(photonView.ownerId));
    }



    public void SetNewColor(Color c)
    {
        photonView.RPC("SetMyColor", PhotonTargets.All, c.r, c.g, c.b);
    }

    [PunRPC]
    public void RequestNewColour()
    {
        SetNewColor(GetColour());
    }

    public Color GetColour()
    {
        switch (VarsTracker.playerID)
        {
            case 0:
                return Color.yellow;
            case 1:
                return Color.cyan;
            case 2:
                return Color.red;
            case 3:
                return Color.green;
            default:
                return Color.white;
        }
    }

    [PunRPC]
    public void SetMyColor(float r, float g, float b)
    {
        GetComponent<Renderer>().material.color = new Color(r,g,b);
    }
}

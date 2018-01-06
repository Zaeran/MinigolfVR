using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Fire confetti if the ball goes in
/// </summary>
public class ConfettiFeedback : Photon.PunBehaviour {

    //fires only one colour of confetti
    [PunRPC]
    public void FireConfetti(int playerID)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ConfettiHolder>().FireConfetti(playerID);
    }

    //Ball went in the hole
    public void BallIn(int playerID)
    {
        if (PhotonNetwork.inRoom)
        {
            photonView.RPC("FireConfetti", PhotonTargets.All, playerID);
        }
        else
        {
            FireConfetti(0);
            FireConfetti(1);
            FireConfetti(2);
            FireConfetti(3);
        }
    }
    
}

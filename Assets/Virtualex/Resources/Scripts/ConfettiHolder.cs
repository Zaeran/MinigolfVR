using UnityEngine;
using System.Collections;

public class ConfettiHolder : MonoBehaviour {

    public GameObject[] confetti;

    public void FireConfetti(int playerID)
    {
        if (PhotonNetwork.inRoom)
        {
            confetti[playerID].GetComponent<ParticleSystem>().Emit(100);
            confetti[playerID].GetComponent<AudioSource>().Play();
        }
        else
        {
            confetti[playerID].GetComponent<ParticleSystem>().Emit(25);
            if (playerID == 0)
            {
                confetti[playerID].GetComponent<AudioSource>().Play();
            }
        }
    }
}

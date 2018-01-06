using UnityEngine;
using System.Collections;

public class BackBtnCourse : MonoBehaviour {

    public GameObject courseSelectionMenu;
    public GameObject mainMenu;
    public SceneVoter voter;

    void Activate()
    {
        mainMenu.SetActive(true);
        courseSelectionMenu.SetActive(false);

        voter.ResetVote();

        if (PhotonNetwork.inRoom)
        {
            GameObject.FindGameObjectWithTag("NetworkController").GetComponent<ConnectToNetwork>().ReturnToLobby();
        }
    }
}

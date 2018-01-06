using UnityEngine;
using System.Collections;
using Photon;

public class JoinRandomBtn : Photon.MonoBehaviour {

    public GameObject thisMenu;
    public GameObject courseSelection;
    public Canvas baseCanvas;

    void Activate()
    {
        if (PhotonNetwork.JoinRandomRoom())
        {
            StartCoroutine(CourseSelectOnCreated());
        }
    }


    IEnumerator CourseSelectOnCreated()
    {
        baseCanvas.enabled = false;
        baseCanvas.GetComponent<BoxCollider>().enabled = false;
        while (!PhotonNetwork.inRoom)
        {
            yield return new WaitForSeconds(0.25f);
        }
        baseCanvas.enabled = true;
        baseCanvas.GetComponent<BoxCollider>().enabled = true;
        courseSelection.SetActive(true);
        thisMenu.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetCorrectBackButtonText : MonoBehaviour {

    void OnEnable()
    {
        if(PhotonNetwork.connected){
            if (PhotonNetwork.inRoom)
            {
                GetComponent<Text>().text = "LEAVE LOBBY";
            }
            else
            {
                GetComponent<Text>().text = "BACK";
            }
        }
        else
        {
            GetComponent<Text>().text = "BACK";
        }
    }
}

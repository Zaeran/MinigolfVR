using UnityEngine;
using System.Collections;
using Photon;

public class CreateNetworkController : Photon.MonoBehaviour {

    void Start()
    {
        if (PhotonNetwork.inRoom)
        {
            CreateNetworkObj();
        }
    }

    public void OnJoinedRoom()
    {
        CreateNetworkObj();
    }

    void CreateNetworkObj()
    {
        GameObject Obj = PhotonNetwork.Instantiate("Sphere", Vector3.zero, Quaternion.identity, 0);
        Obj.GetComponent<SetNetworkColor>().SetNewColor(VarsTracker.playerColor);
        Obj.transform.parent = transform;
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localEulerAngles = Vector3.zero;
        //Obj.GetComponent<Renderer>().enabled = false;
    }
}

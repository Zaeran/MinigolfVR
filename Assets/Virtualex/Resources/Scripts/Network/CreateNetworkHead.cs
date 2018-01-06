using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class CreateNetworkHead : Photon.MonoBehaviour
{
    GameObject Obj;
    void Start()
    {
        if (PhotonNetwork.inRoom && Obj == null)
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
        Obj = PhotonNetwork.Instantiate("HeadSphere", Vector3.zero, Quaternion.identity, 0);
        //Obj.GetComponent<SetNetworkColor>().SetNewColor(VarsTracker.playerColor);
        Obj.transform.parent = transform;
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localEulerAngles = Vector3.zero;
        Obj.GetComponent<Renderer>().enabled = false;
    }
}

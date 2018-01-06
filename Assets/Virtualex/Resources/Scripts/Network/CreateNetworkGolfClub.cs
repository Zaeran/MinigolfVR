using UnityEngine;
using System.Collections;
using Photon;

public class CreateNetworkGolfClub : Photon.MonoBehaviour
{

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
        GameObject Obj = PhotonNetwork.Instantiate("ClubModel", Vector3.zero, Quaternion.identity, 0);
        Obj.transform.parent = transform;
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localEulerAngles = Vector3.zero;
        Obj.GetComponent<Renderer>().enabled = false;
    }
}

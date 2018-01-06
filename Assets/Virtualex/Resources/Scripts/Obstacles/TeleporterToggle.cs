using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Toggles a teleporter on or off
/// </summary>
public class TeleporterToggle : Photon.MonoBehaviour {

    public GameObject OutTele1;
    public GameObject OutTele2;
    public GameObject InTele;
    bool obj1Enabled;
    bool canEnable = true;

    void OnTriggerEnter()
    {
        if (canEnable)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("ToggleObjects", PhotonTargets.All);
            }
            else
            {
                ToggleObjects();
            }
        }
    }

    IEnumerator TriggerCD()
    {
        yield return new WaitForSeconds(2.5f);
        canEnable = true;
    }

    [PunRPC]
    public void ToggleObjects()
    {
        canEnable = false;
        StartCoroutine(TriggerCD());
        //when disabling, change the teleporter links
        if (obj1Enabled)
        {
            OutTele1.SendMessage("Activate");
            OutTele2.SendMessage("DeActivate");
            InTele.GetComponent<Teleporter>().ChangeLink(OutTele1);
            obj1Enabled = false;
        }
        else
        {
            OutTele2.SendMessage("Activate");
            OutTele1.SendMessage("DeActivate");
            InTele.GetComponent<Teleporter>().ChangeLink(OutTele2);
            obj1Enabled = true;
        }
    }
}

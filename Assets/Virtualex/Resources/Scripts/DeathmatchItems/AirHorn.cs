using UnityEngine;
using System.Collections;
using Photon;

[RequireComponent(typeof(DeathmatchItemCMDHolder))]
public class AirHorn : Photon.MonoBehaviour, IDeathmatchItemCommand {

    bool canEnable = true;
    bool isActive = false;
    float charge = 2.0f;
    AudioSource a;

    void Start()
    {
        a = GetComponent<AudioSource>();
        GetComponent<DeathmatchItemCMDHolder>().cmd = this;
    }

    public void ActivateStart()
    {
        if (charge > 0)
        {
            isActive = true;
            if (PhotonNetwork.connected)
            {
                photonView.RPC("ActivateObject", PhotonTargets.All);
            }
            else
            {
                ActivateObject();
            }
        }
    }

    public void ActivateEnd()
    {
        if (PhotonNetwork.connected)
        {
            photonView.RPC("StopObject", PhotonTargets.All);
        }
        else
        {
            StopObject();
        }
    }

    public void SetHoldingController(ViveControls v)
    {
        
    }

    void Update()
    {
        if (charge <= 0)
        {
            isActive = false;
        }
        if (isActive)
        {
            if (!a.isPlaying)
            {
                a.Play();
            }
            charge -= Time.deltaTime;
        }
        else
        {
            if (a.isPlaying)
            {
                a.Stop();
            }
        }
    }

    [PunRPC]
    public void ActivateObject()
    {
        isActive = true;
    }

    [PunRPC]
    public void StopObject()
    {
        isActive = false;
    }
}

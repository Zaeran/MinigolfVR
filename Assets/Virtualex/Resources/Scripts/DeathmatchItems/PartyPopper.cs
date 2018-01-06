using UnityEngine;
using System.Collections;
using Photon;

public class PartyPopper : Photon.MonoBehaviour, IDeathmatchItemCommand {

    bool canEnable = true;
    bool isActive = false;
    AudioSource a;
    public ParticleSystem particles;
    public TutorialScript tut;

    void Start()
    {
        a = GetComponent<AudioSource>();
        GetComponent<DeathmatchItemCMDHolder>().cmd = this;
    }

    public void ActivateStart()
    {
        if (PhotonNetwork.connected)
        {
            photonView.RPC("ActivateObject", PhotonTargets.All);
        }
        else
        {
            ActivateObject();
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
        if (isActive && canEnable)
        {
            canEnable = false;
            a.Play();
            particles.Play();
            if (tut != null)
            {
                tut.PopperPopped();
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

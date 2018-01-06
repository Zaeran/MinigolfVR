using UnityEngine;
using System.Collections;
using Photon;

[RequireComponent(typeof(DeathmatchItemCMDHolder))]
public class SmokeBomb : Photon.MonoBehaviour, IDeathmatchItemCommand
{
    bool isActive;
    public ParticleSystem particles;

    ViveControls grabbingController;

    void OnCollisionEnter(Collision c)
    {
        if (isActive)
        {
            if (PhotonNetwork.connected)
            {
                photonView.RPC("Explode", PhotonTargets.All);
            }
            else
            {
                Explode();
            }
        }
    }

    void Start()
    {
        GetComponent<DeathmatchItemCMDHolder>().cmd = this;
    }

    public void ActivateStart()
    {
        //pull pin
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
        grabbingController.ReleaseHeldObject();
    }

    public void SetHoldingController(ViveControls v)
    {
        grabbingController = v;
    }

    [PunRPC]
    public void ActivateObject()
    {
        particles.emissionRate = 100;
        isActive = true;
    }

    [PunRPC]
    public void Explode()
    {
        Instantiate(Resources.Load(@"Prefabs/Deathmatch/SmokeField"), transform.position, Quaternion.Euler(270,0,0));
        Destroy(gameObject);
    }
}

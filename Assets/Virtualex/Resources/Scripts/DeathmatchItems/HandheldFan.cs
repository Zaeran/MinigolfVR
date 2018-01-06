using UnityEngine;
using System.Collections;
using Photon;

[RequireComponent(typeof(DeathmatchItemCMDHolder))]
public class HandheldFan : Photon.MonoBehaviour, IDeathmatchItemCommand
{

    bool canEnable = true;
    bool isActive = false;
    float charge = 3.0f;
    public Transform fanBlades;

    void Start()
    {
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
            fanBlades.Rotate(Vector3.forward, 1440 * Time.deltaTime, Space.Self);
            //charge -= Time.deltaTime;

            Collider[] nearbyObjects = Physics.OverlapSphere(transform.position + transform.up * 4, 2.0f);
            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                if (nearbyObjects[i].tag == "MyBall")
                {
                    Rigidbody r = nearbyObjects[i].GetComponent<Rigidbody>();
                    r.drag = 0;
                    r.AddForce(transform.up * 10);
                    Debug.Log("BALLHERE");
                }
                else if (nearbyObjects[i].gameObject.tag == "Blowable")
                {
                    ParticleSystem p = nearbyObjects[i].GetComponent<ParticleSystem>();
                    p.maxParticles -= 5;
                    p.emissionRate -= 1;
                    if (p.maxParticles <= 100)
                    {
                        Destroy(nearbyObjects[i].gameObject);
                    }
                }
            }
        }
    }

    [PunRPC]
    public void ActivateObject()
    {
        Debug.Log("ISACTIVE");
        isActive = true;
    }

    [PunRPC]
    public void StopObject()
    {
        isActive = false;
    }
}

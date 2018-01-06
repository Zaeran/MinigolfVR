using UnityEngine;
using System.Collections;
using Photon;

[RequireComponent(typeof(DeathmatchItemCMDHolder))]
public class Grenade : Photon.MonoBehaviour, IDeathmatchItemCommand {

    public float radius;
    public float explosionForce;
    public GameObject pin;
    public GameObject pinPhysics;
    int ballOnlyLayer;
    bool isActive;

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
        ballOnlyLayer = 1 << LayerMask.NameToLayer("Ball");
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
        pin.SetActive(false);
        pinPhysics.SetActive(true);
        pinPhysics.GetComponent<Rigidbody>().AddExplosionForce(3000, transform.position, 10);
        pinPhysics.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)));
        isActive = true;
    }

    [PunRPC]
    public void Explode()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("MyBall");
        ball.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, radius);
        Instantiate(Resources.Load(@"Prefabs/Deathmatch/Explosion"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

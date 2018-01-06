using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Synchronises an object across the network
/// </summary>
public class SyncObject : Photon.MonoBehaviour {

    void Awake()
    {
        PhotonNetwork.sendRate = 33;
        PhotonNetwork.sendRateOnSerialize = 33;
    }

    private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.isMine && correctPlayerPos != Vector3.zero)
        {
            transform.position = correctPlayerPos;
            transform.rotation = correctPlayerRot;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }

    void OnLevelWasLoaded(int level)
    {
        Destroy(gameObject);
    }
}

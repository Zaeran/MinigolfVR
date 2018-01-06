using UnityEngine;
using System.Collections;

/// <summary>
/// Force-pulls the golf club back to your hand
/// </summary>
public class ClubReturnToHand : MonoBehaviour {

    public bool returnToHand = false;
    public bool isInHand = false;
    public GameObject putter;
    BoxCollider col;

    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

	// Update is called once per frame
	void Update () {
        //move towards hand
        if (returnToHand)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, Vector3.zero, Time.deltaTime * 9);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * 9);
            if (transform.localPosition.magnitude < 0.1f && transform.localRotation.eulerAngles.magnitude < 0.1f)
            {
                isInHand = true;
                returnToHand = false;
                putter.GetComponent<PutterScript>().Teleported();
            }
        }
        if (!col.enabled)
        {
            col.enabled = true;
        }
	}

    /// <summary>
    /// Deactivate whole club collider, activate putter collider
    /// </summary>
    public void ReturnToHand()
    {
        GetComponent<MeshCollider>().enabled = false;
        //Destroy(GetComponent<Rigidbody>());
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;

        returnToHand = true;
    }

    /// <summary>
    /// Activate whole club collider, deactivate putter collider
    /// </summary>
    public void Throw()
    {
        returnToHand = false;
        transform.parent = null;
        //gameObject.AddComponent<Rigidbody>();
        //GetComponent<Rigidbody>().mass = 0.001f;
       // GetComponent<Rigidbody>().useGravity = false;
        GetComponent<MeshCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        isInHand = false;
        
    }
}

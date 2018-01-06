using UnityEngine;
using System.Collections;

/// <summary>
/// Handles ball / teleporter interactions
/// </summary>
public class Teleporter : MonoBehaviour {

    public Transform linkedTeleporter;
    public GameObject teleportEffect;
    public bool leadsToZeroGrav;
    public bool isActive = true;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "MyBall")
        {
            linkedTeleporter.GetComponent<Teleporter>().DisableLinkedTrigger();
            linkedTeleporter.GetComponent<AudioSource>().Play();
            SetGravity(c);
            SetPosition(c);
            SetVelocity(c);
            SetAngularVelocity(c);
            c.GetComponent<DontGoThroughThings>().MissFiveSteps();
            GetComponent<AudioSource>().Play();
            //offset frame
            c.GetComponent<Rigidbody>().AddForce(-c.GetComponent<AffectedByGravityFields>().gravityDirection * 98.1f);
        }
    }
    /// <summary>
    /// Sets the gravity of the object at the linked teleporter
    /// </summary>
    /// <param name="c"></param>
    private void SetGravity(Collider c)
    {
        if (leadsToZeroGrav)
        {
            c.GetComponent<Rigidbody>().useGravity = false;
            c.GetComponent<Rigidbody>().drag = 0;
        }
        else
        {
            //c.GetComponent<Rigidbody>().useGravity = true;
            c.GetComponent<Rigidbody>().useGravity = false;
            c.GetComponent<Rigidbody>().drag = 0.1f;
        }
    }

    /// <summary>
    /// Sets the velocity of the object at the linked teleporter
    /// </summary>
    /// <param name="c"></param>
    private void SetVelocity(Collider c)
    {
        Vector3 velocity = c.GetComponent<Rigidbody>().velocity;
        velocity = Vector3.Reflect(velocity, transform.forward);
        velocity = transform.InverseTransformDirection(velocity);
        velocity = new Vector3(velocity.x, velocity.y, 0);
        velocity = linkedTeleporter.TransformDirection(velocity);
        c.GetComponent<Rigidbody>().velocity = -velocity;
    }

    /// <summary>
    /// Sets the angular velocity of the object at the linked teleporter
    /// </summary>
    /// <param name="c"></param>
    private void SetAngularVelocity(Collider c)
    {
        Vector3 angularVelocity = c.GetComponent<Rigidbody>().velocity;
        angularVelocity = Vector3.Reflect(angularVelocity, transform.forward);
        angularVelocity = transform.InverseTransformDirection(angularVelocity);
        angularVelocity = linkedTeleporter.TransformDirection(angularVelocity);
        c.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
    }
    /// <summary>
    /// Sets the position of the object at the linked teleporter
    /// </summary>
    /// <param name="c"></param>
    private void SetPosition(Collider c)
    {
        Vector3 position = c.transform.position - transform.position;
        position = transform.InverseTransformDirection(position);
        position = linkedTeleporter.transform.position + linkedTeleporter.TransformDirection(position);
        c.transform.position = position;
    }

    /// <summary>
    /// Disables a teleporter for a brief duration
    /// used to stop the ball instantly re-entering a teleporter once teleported
    /// </summary>
    public void DisableLinkedTrigger()
    {
        StartCoroutine(TriggerDisable());
    }

    /// <summary>
    /// Disables a teleporter for a brief duration
    /// used to stop the ball instantly re-entering a teleporter once teleported
    /// </summary>
    IEnumerator TriggerDisable()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<BoxCollider>().enabled = true;
    }

    /// <summary>
    /// Activates a teleporter
    /// </summary>
    public void Activate()
    {
        GetComponent<BoxCollider>().enabled = true;
        teleportEffect.SetActive(true);
    }

    /// <summary>
    /// Deactivates a teleporter
    /// </summary>
    public void DeActivate()
    {
        GetComponent<BoxCollider>().enabled = false;
        teleportEffect.SetActive(false);
    }

    /// <summary>
    /// Changes which teleporter is linked to this one
    /// </summary>
    /// <param name="Teleporter"></param>
    public void ChangeLink(GameObject Teleporter)
    {
        linkedTeleporter = Teleporter.transform;
    }
}

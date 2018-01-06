using UnityEngine;
using System.Collections;

/// <summary>
/// Sets the interactions of a gravity field
/// Field is in the transform.up direction of the object this is attached to
/// </summary>
public class GravField : MonoBehaviour {

    public bool isZeroGrav;
    public int gravLevel; //used for intersecting gravity fields. Higher grav levels take priority

    void OnTriggerEnter(Collider c)
    {
        AffectedByGravityFields f = c.GetComponent<AffectedByGravityFields>();
        if (f != null)
        {
            if (f.currentGravLevel < gravLevel)
            {
                c.GetComponent<Rigidbody>().useGravity = false;
                f.SetGravity(true, isZeroGrav ? Vector3.zero : transform.up, gravLevel);
            }
        }
    }
    void OnTriggerStay(Collider c)
    {
        AffectedByGravityFields f = c.GetComponent<AffectedByGravityFields>();
        if (f != null)
        {
            if (f.currentGravLevel < gravLevel)
            {
                c.GetComponent<Rigidbody>().useGravity = false;
                f.SetGravity(true, isZeroGrav ? Vector3.zero : transform.up, gravLevel);
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        AffectedByGravityFields f = c.GetComponent<AffectedByGravityFields>();
        if (f != null)
        {
            c.GetComponent<Rigidbody>().useGravity = true;
            f.SetGravity(false, Vector3.zero, 0);
        }
    }
}

using UnityEngine;
using System.Collections;

public class CollisionInfoScript : MonoBehaviour {

    public Vector3 vel;
    public Vector3 rel;
    public Vector3 norm;

    public void GiveData(Vector3 d1, Vector3 d2, Vector3 d3)
    {
        vel = d1;
        rel = d2;
        norm = d3;
    }
}

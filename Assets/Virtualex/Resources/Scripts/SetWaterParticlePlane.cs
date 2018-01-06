using UnityEngine;
using System.Collections;

public class SetWaterParticlePlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().collision.SetPlane(0, GameObject.FindGameObjectWithTag("Water").transform);
	}
}

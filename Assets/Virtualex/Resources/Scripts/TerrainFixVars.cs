using UnityEngine;
using System.Collections;

public class TerrainFixVars : MonoBehaviour {

    Terrain t;
    public float TreeDistance;
	// Use this for initialization
	void Start () {
        t = GetComponent<Terrain>();
	}
	
	// Update is called once per frame
	void Update () {
        t.treeDistance = TreeDistance;
	}
}

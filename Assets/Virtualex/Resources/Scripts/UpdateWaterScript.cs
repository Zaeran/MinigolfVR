using UnityEngine;
using System.Collections;

public class UpdateWaterScript : MonoBehaviour {

    Material myMat;
    public float waterSpeed;
    float currentOffset;

	// Use this for initialization
	void Start () {
        myMat = GetComponent<MeshRenderer>().material;
        currentOffset = 0;
	}
	
	// Update is called once per frame
	void Update () {
        currentOffset += waterSpeed * Time.deltaTime;
        myMat.SetTextureOffset("_MainTex", new Vector2(0, currentOffset));
        myMat.SetTextureOffset("_Detail", new Vector2(0, currentOffset));
	}
}

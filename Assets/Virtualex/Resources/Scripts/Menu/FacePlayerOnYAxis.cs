using UnityEngine;
using System.Collections;

public class FacePlayerOnYAxis : MonoBehaviour {

    Transform Head;

	// Use this for initialization
	void Start () {
        Head = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = Head.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
        transform.eulerAngles += new Vector3(0, 180, 0);
	}
}

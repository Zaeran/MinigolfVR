using UnityEngine;
using System.Collections;

public class MenuFacePlayerOnOpen : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Vector3 targetPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
    }
}

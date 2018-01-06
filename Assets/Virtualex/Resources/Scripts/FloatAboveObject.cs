using UnityEngine;
using System.Collections;

public class FloatAboveObject : MonoBehaviour {

    public Transform obj;

    public void SetTranform(Transform t)
    {
        obj = t;
    }
	
	// Update is called once per frame
	void Update () {
        if (obj != null)
        {
            transform.position = obj.transform.position + (Vector3.up * 1.5f);
            transform.eulerAngles = new Vector3(0, obj.eulerAngles.y + 180,0);
        }
	}
}

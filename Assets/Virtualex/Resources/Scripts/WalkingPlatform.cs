using UnityEngine;
using System.Collections;

public class WalkingPlatform : MonoBehaviour {

    public Transform head;
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

	// Update is called once per frame
	void Update () {
        renderer.enabled = false;
        RaycastHit hit;
        if (Physics.Raycast(head.transform.position, Vector3.down, out hit, 1000))
        {
            if (hit.distance > (head.transform.localPosition.y + 0.5f) * 10)
            {
                renderer.enabled = true;
                transform.position = head.transform.position - new Vector3(0, (head.transform.localPosition.y + 0.2f) * 10, 0);
            }
        }
	}
}

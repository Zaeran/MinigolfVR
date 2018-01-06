using UnityEngine;
using System.Collections;

/// <summary>
/// Highlights attached object
/// Used for vive controller buttons
/// </summary>
public class HighlightButton : MonoBehaviour {

    Color c;
    Material m;
    public GameObject particles;

	// Use this for initialization
	void Start () {
        m = null;
        particles.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (m == null && GetComponent<MeshRenderer>() != null)
        {
            m = GetComponent<MeshRenderer>().material;
            c = m.color;
        }
        /*
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateColor();
        }
        else
        {
            DeActivateColor();
        }
        */
	}

    public void ActivateColor()
    {
        if (m != null)
        {
            m.color = Color.yellow;
            particles.SetActive(true);
        }
    }

    public void DeActivateColor()
    {
        if (m != null)
        {
            m.color = c;
            particles.SetActive(false);
        }
    }
}

using UnityEngine;
using System.Collections;

public class LobbyLightReset : MonoBehaviour {

    public bool enabled;
    Light thisLight;

    void Start()
    {
        thisLight = GetComponent<Light>();
    }

		// Update is called once per frame
	void Update () {
        if (enabled)
        {
            if (thisLight.range < 1000)
            {
                thisLight.range += 200 * Time.deltaTime;
            }
        }
	}

    public void ActivateLight()
    {
        enabled = true;
    }
}

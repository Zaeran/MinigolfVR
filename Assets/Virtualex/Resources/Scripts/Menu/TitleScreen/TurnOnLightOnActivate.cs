using UnityEngine;
using System.Collections;

public class TurnOnLightOnActivate : MonoBehaviour {

    public LobbyLightReset light;

    void OnEnable()
    {
        light.ActivateLight();
    }
}

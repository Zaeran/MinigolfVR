using UnityEngine;
using System.Collections;

public class CloseMenuButton : MonoBehaviour {

    public GameObject baseMenu;

    void Activate()
    {
        Destroy(baseMenu);
        VarsTracker.Menu = null;
    }
}

using UnityEngine;
using System.Collections;

public class SwapPanelOnActivate : MonoBehaviour {

    public GameObject[] thesePanels;
    public GameObject[] otherPanels;

    public void Activate()
    {

        for (int i = 0; i < otherPanels.Length; i++)
        {
            otherPanels[i].SetActive(true);
        }
        for (int i = 0; i < thesePanels.Length; i++)
        {
            thesePanels[i].SetActive(false);
        }
    }
}

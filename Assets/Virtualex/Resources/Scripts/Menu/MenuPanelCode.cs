using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuPanelCode : MonoBehaviour {

    public GameObject selector;
    bool isSelected;

    public void Selected()
    {
        selector.SetActive(true);
        //transform.parent.gameObject.GetComponent<ActivationManager>().currentPanel = gameObject;
        //transform.FindChild("Panel").GetComponent<Image>().color = new Color(220f / 255f, 100f / 255f, 100f / 255f, 230f / 255f);
        isSelected = true;
    }

    public void DeSelected()
    {
        selector.SetActive(false);
        //transform.FindChild("Panel").GetComponent<Image>().color = new Color(183f / 255f, 82f / 255f, 81f / 255f, 230f / 255f);
        isSelected = false;
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void SetSelectorStart(GameObject btn)
    {
        selector.GetComponent<SelectorCode>().thisButton = btn;
    }

    public void MoveSelector(SelectorCode.NavigationDirection dir)
    {
        selector.GetComponent<SelectorCode>().Navigate(dir);
    }

    public void ActivateSelector()
    {
        selector.GetComponent<SelectorCode>().Activate();
    }
}

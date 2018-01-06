using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginNoSelected : MonoBehaviour {

    public GameObject thisPanel;
    public GameObject prevKeyboard;
    public MoveFieldToPosition prevField;
    public Text prevERRORTEXT;

    public void Activate()
    {
        prevField.MoveToInput();
        prevERRORTEXT.text = "";
        prevKeyboard.SetActive(true);
        thisPanel.SetActive(false);
    }
}

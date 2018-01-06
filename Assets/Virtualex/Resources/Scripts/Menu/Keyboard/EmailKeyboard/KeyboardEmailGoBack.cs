using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardEmailGoBack : MonoBehaviour {

    public GameObject thisKeyboard;
    public GameObject prevKeyboard;
    public MoveFieldToPosition prevField;
    public GameObject thisField;
    public Text emailText;
    public Text ERRORTEXT;
    public Text prevERRORTEXT;

    public void Activate()
    {
        prevField.MoveToInput();
        emailText.text = "";
        ERRORTEXT.text = "";
        prevERRORTEXT.text = "";
        prevKeyboard.SetActive(true);
        thisField.SetActive(false);
        thisKeyboard.SetActive(false);
    }
}

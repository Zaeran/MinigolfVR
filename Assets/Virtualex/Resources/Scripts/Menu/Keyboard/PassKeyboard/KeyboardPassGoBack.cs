using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardPassGoBack : MonoBehaviour {
    
    public GameObject thisKeyboard;
    public GameObject prevKeyboard;
    public MoveFieldToPosition prevField;
    public GameObject thisField;
    public Text passwordText;
    public Text ERRORTEXT;
    public Text prevERRORTEXT;

    public void Activate()
    {
        prevField.MoveToInput();
        passwordText.text = "";
        ERRORTEXT.text = "";
        prevERRORTEXT.text = "";
        thisField.SetActive(false);
        prevKeyboard.SetActive(true);
        thisKeyboard.SetActive(false);
    }
}

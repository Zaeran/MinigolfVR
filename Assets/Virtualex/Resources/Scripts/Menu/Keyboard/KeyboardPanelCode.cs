using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardPanelCode : MonoBehaviour {

    public Text targetText;
    public GameObject EnterBtn;
    public GameObject GoBackBtn;

    public void AddLetter(string s)
    {
        if (s == "ENTER")
        {
            //text entry complete
//            SetToInactive();
            //DoActionOnEnter();
            EnterBtn.SendMessage("Activate");
        }
        else if (s == "GO BACK")
        {
            //
            GoBackBtn.SendMessage("Activate");
        }
        else if (s != "<")
        {
            targetText.text += s;
        }
        else if(targetText.text.Length > 0)
        {
            targetText.text = targetText.text.Substring(0, targetText.text.Length - 1);
        }
    }

    public void SetTextBox(Text text)
    {
        targetText = text;
    }

    public void SetToInactive()
    {
        targetText = null;
        gameObject.SetActive(false);
    }    
}

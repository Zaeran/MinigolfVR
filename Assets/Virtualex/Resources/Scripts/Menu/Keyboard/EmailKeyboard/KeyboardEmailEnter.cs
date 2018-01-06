using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardEmailEnter : MonoBehaviour {

    public MoveFieldToPosition field;
    public GameObject nextPanel;
    public GameObject thisKeyboard;
    public Text ERRORTEXT;
    public Text Email;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (Email.text.Length > 0)
        {
            ERRORTEXT.text = "";
            field.MoveToFinal();
            StartCoroutine(DelayPanelSwap());
        }
        else
        {
            ERRORTEXT.text = "NO EMAIL ENTERED";
        }
    }

    IEnumerator DelayPanelSwap()
    {
        yield return new WaitForSeconds(0.5f);
        nextPanel.SetActive(true);
        thisKeyboard.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardPassEnterExisting : MonoBehaviour {

    public MoveFieldToPosition field;
    public GameObject nextPanel;
    public GameObject thisKeyboard;
    public Text ERRORTEXT;
    public Text Password;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (Password.text.Length > 0)
        {
            ERRORTEXT.text = "";
            field.MoveToFinal();
            StartCoroutine(DelayPanelSwap());
        }
        else
        {
            ERRORTEXT.text = "NO PASSWORD ENTERED";
        }
    }

    IEnumerator DelayPanelSwap()
    {
        yield return new WaitForSeconds(0.5f);
        nextPanel.SetActive(true);
        thisKeyboard.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardPassEnter : MonoBehaviour {

    public MoveFieldToPosition field;
    public GameObject nextKeyboard;
    public GameObject thisKeyboard;
    public GameObject emailField;
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
        yield return new WaitForSeconds(1.0f);
        nextKeyboard.SetActive(true);
        emailField.SetActive(true);
        thisKeyboard.SetActive(false);
    }
}

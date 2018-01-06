using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardUserEnter : MonoBehaviour {

    public MoveFieldToPosition field;
    public GameObject nextKeyboard;
    public GameObject thisKeyboard;
    public GameObject passwordField;
    public Text ERRORTEXT;
    public Text UserName;

    public bool testUsername;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (UserName.text.Length > 0)
        {
            ERRORTEXT.text = "";
            field.MoveToFinal();
            StartCoroutine(DelayPanelSwap());
        }
        else
        {
            ERRORTEXT.text = "NO USERNAME ENTERED";
        }
    }

    IEnumerator DelayPanelSwap()
    {
        yield return new WaitForSeconds(0.5f);
        if (testUsername)
        {
            StartCoroutine(UpdateText());
        }
        else
        {
            nextKeyboard.SetActive(true);
            passwordField.SetActive(true);
            thisKeyboard.SetActive(false);
        }
    }

    IEnumerator UpdateText()
    {
        WWWForm wf = new WWWForm();
        wf.AddField("user", UserName.text);
        WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "TestExistingUser.php", wf);
        yield return query;
        if (query.text == "0")
        {
            ERRORTEXT.text = "USERNAME IS AVAILABLE";
        }
        else
        {
            ERRORTEXT.text = "USERNAME ALREADY EXISTS";
        }
        thisKeyboard.SetActive(false);
        nextKeyboard.SetActive(true);
        passwordField.SetActive(true);
    }
}

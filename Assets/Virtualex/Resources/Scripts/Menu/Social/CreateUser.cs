using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateUser : MonoBehaviour {

    public Text UserName;
    public Text Password;
    public Text Email;
    public Text ERRORTEXT;

	// Use this for initialization
    public void Activate()
    {
        StartCoroutine(SendRequest());
    }

    IEnumerator SendRequest()
    {
        WWWForm wf = new WWWForm();
        wf.AddField("user", UserName.text);
        WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "TestExistingUser.php", wf);

        yield return query;

        if (query.text == "0")
        {
            WWWForm creationForm = new WWWForm();
            creationForm.AddField("user", UserName.text);
            creationForm.AddField("pass", Password.text);
            creationForm.AddField("email", Email.text);
            query = new WWW(VarsTracker.MinigolfNetworkBasePath + "CreateUser.php", creationForm);
            yield return query;
            if (query.text == "FAIL")
            {
                ERRORTEXT.text = "Error creating account";
            }
            else
            {
                ERRORTEXT.text = "Account created";
            }
        }
        else
        {
            ERRORTEXT.text = "USERNAME ALREADY EXISTS";
        }
    }
}

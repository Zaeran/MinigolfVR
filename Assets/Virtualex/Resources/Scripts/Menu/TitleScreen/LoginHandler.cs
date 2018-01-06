using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour {

    public Text UserName;
    public Text Password;
    public Text Email;

    public Text ERRORTEXT;

    public bool createUser;

    public GameObject thisPanel;
    public GameObject succeedPanel;
    public GameObject failPanel;

    // Use this for initialization
    public void Activate()
    {
        if (createUser)
        {
            StartCoroutine(CreateUser());
        }
        else
        {
            StartCoroutine(LoginAttempt());
        }
    }

    IEnumerator LoginAttempt()
    {
        ERRORTEXT.text = "ATTEMPTING LOGIN...";
        WWWForm wf = new WWWForm();
        wf.AddField("user", UserName.text);
        wf.AddField("pass", Password.text);
        WWW query = new WWW(VarsTracker.MinigolfNetworkBasePath + "Login.php", wf);

        yield return query;

        bool successfulLogin = false;
        switch (query.text)
        {
            case "0": //username doesn't exist
                ERRORTEXT.text = "Username doesn't exist";
                StartCoroutine(Fail());
                break;
            case "1": //wrong password
                ERRORTEXT.text = "Incorrect password";
                StartCoroutine(Fail());
                break;
            case "2": //already logged in
                ERRORTEXT.text = "This account is already logged in elsewhere";
                StartCoroutine(Fail());
                break;
            default: //success
                ERRORTEXT.text = "Login successful!";
                VarsTracker.PlayerName = UserName.text;
                successfulLogin = true;
                break;
        }

        if (successfulLogin)
        {
            GameObject.FindGameObjectWithTag("NetworkController").GetComponent<ConnectToNetwork>().JoinNetwork(VarsTracker.PlayerName);
            int cycles = 0;
            while (!PhotonNetwork.connected && cycles < 50) //wait 5 seconds
            {
                cycles++;
                yield return new WaitForSeconds(0.1f);
            }
            if (!PhotonNetwork.connected)
            {
                ERRORTEXT.text = "Network Error";
                StartCoroutine(Fail());
                Logout();
            }
            else
            {
                PlayerPrefs.SetString("LastUsername", UserName.text);
                PlayerPrefs.SetString("LastPassword", Password.text);
                StartCoroutine(Success());
            }
        }
    }

    void Logout()
    {
        //call logout PHP script
        WWWForm wf = new WWWForm();
        wf.AddField("user", VarsTracker.PlayerName);

        WWW logout = new WWW(VarsTracker.MinigolfNetworkBasePath + "Logout.php", wf);

        //reset in-game variables
        VarsTracker.PlayerName = "";
        GameObject.FindGameObjectWithTag("NetworkController").GetComponent<ConnectToNetwork>().DisconnectFromNetwork();
    }

    IEnumerator CreateUser()
    {
        ERRORTEXT.text = "CREATING ACCOUNT";
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
                StartCoroutine(Fail());
            }
            else
            {
                ERRORTEXT.text = "Account created";
                StartCoroutine(LoginAttempt());
            }
        }
        else
        {
            ERRORTEXT.text = "USERNAME ALREADY EXISTS";
            StartCoroutine(Fail());
        }
    }

    IEnumerator Success()
    {
        yield return new WaitForSeconds(1.5f);
        succeedPanel.SetActive(true);
        thisPanel.SetActive(false);
    }

    IEnumerator Fail()
    {
        yield return new WaitForSeconds(1.5f);
        failPanel.SetActive(true);
        ERRORTEXT.text = "";
        //thisPanel.SetActive(false);
    }

    /*
    void OnDisable()
    {
        UserName.text = "";
        Password.text = "";
    }
     */
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginPreviousUser : MonoBehaviour {

    public Text ERRORTEXT;

    public GameObject thisPanel;
    public GameObject succeedPanel;
    public GameObject failPanel;

    // Use this for initialization
    public void Activate()
    {
        StartCoroutine(LoginAttempt());
    }

    IEnumerator LoginAttempt()
    {
        ERRORTEXT.text = "ATTEMPTING LOGIN...";
        WWWForm wf = new WWWForm();
        wf.AddField("user", PlayerPrefs.GetString("LastUsername"));
        wf.AddField("pass", PlayerPrefs.GetString("LastPassword"));
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
                VarsTracker.PlayerName = PlayerPrefs.GetString("LastUsername");
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
                StartCoroutine(Success());
            }
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

}

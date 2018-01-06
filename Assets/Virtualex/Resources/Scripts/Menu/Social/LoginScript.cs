using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;

public class LoginScript : Photon.MonoBehaviour {

    public Text UserName;
    public Text Password;

    public Text ERRORTEXT;
    public SocialOfflineMenu menu;

	// Use this for initialization
    public void Activate()
    {
        StartCoroutine(LoginAttempt());
    }

    IEnumerator LoginAttempt()
    {
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
                break;
            case "1": //wrong password
                ERRORTEXT.text = "Incorrect password";
                break;
            case "2": //already logged in
                ERRORTEXT.text = "This account is already logged in elsewhere";
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
                Logout();
            }
            else
            {
                menu.LoginSuccessful();
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

    void OnDisable()
    {
        ERRORTEXT.text = "";
        UserName.text = "";
        Password.text = "";
    }
}

using UnityEngine;
using System.Collections;

public class LogoutScript : MonoBehaviour {

    public GameObject thisPanel;
    public GameObject mainMenuPanel;

    public void Activate()
    {
        Logout();
    }

    void Logout()
    {
        //call logout PHP script
        WWWForm wf = new WWWForm();
        wf.AddField("user", VarsTracker.PlayerName);

        WWW logout = new WWW(VarsTracker.MinigolfNetworkBasePath + "Logout.php", wf);

        //reset in-game variables
        VarsTracker.PlayerName = "";
        mainMenuPanel.SetActive(true);
        thisPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("NetworkController").GetComponent<ConnectToNetwork>().DisconnectFromNetwork();
    }
}

using UnityEngine;
using System.Collections;
using Photon;

/// <summary>
/// Attempts to alert the database that a user has logged out if they quit the application
/// </summary>
public class EmergencyLogout : Photon.MonoBehaviour {

    void OnApplicationQuit() //log out officially if currently online
    {
        if (VarsTracker.PlayerName != "")
        {
            WWWForm wf = new WWWForm();
            wf.AddField("user", VarsTracker.PlayerName);

            WWW logout = new WWW(VarsTracker.MinigolfNetworkBasePath + "Logout.php", wf);
        }
        if (VarsTracker.CurrentRoom != "")
        {
            WWWForm wf = new WWWForm();
            wf.AddField("room", VarsTracker.CurrentRoom);

            WWW logout = new WWW(VarsTracker.MinigolfNetworkBasePath + "LeaveRoom.php", wf);
        }
    }
}

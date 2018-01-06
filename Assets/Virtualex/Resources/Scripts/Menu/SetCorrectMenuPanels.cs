using UnityEngine;
using Photon;
using System.Collections;

public class SetCorrectMenuPanels : Photon.MonoBehaviour {

    public GameObject SocialOnlinePanel;
    public GameObject SocialOfflinePanel;
    public GameObject CoursePanel;
    public GameObject HelpPanel;
    public GameObject MultiScorePanel;
    public GameObject TutorialMenu;
    public GameObject MainMenuPanel;

	// Use this for initialization
	void Start () {
        if (VarsTracker.PlayerName != "")
        {
            SocialOnlinePanel.SetActive(true);
            SocialOfflinePanel.SetActive(false);
        }

        if (Application.loadedLevelName == "Lobby")
        {
            HelpPanel.SetActive(true);
            CoursePanel.SetActive(false);
        }

        if (Application.loadedLevelName == "TutorialScene")
        {
            TutorialMenu.SetActive(true);
            MainMenuPanel.SetActive(false);
        }
	}

    void Update()
    {
        if (PhotonNetwork.room != null && Application.loadedLevelName != "Lobby")
        {
            MultiScorePanel.SetActive(true);
        }
        else
        {
            MultiScorePanel.SetActive(false);
        }
    }
}
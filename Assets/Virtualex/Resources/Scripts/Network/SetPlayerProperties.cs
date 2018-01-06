using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Sets the colour and name of a player
/// Used for testing (if I remember correctly)
/// </summary>
public class SetPlayerProperties : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        playerColor = Color.blue;
        playerName = "";
	}

    public Color playerColor;
    public string playerName;
    public Image playerColorRepresentation;

    public void SetPlayerColor(Object o)
    {
        playerColor = ((GameObject)o).GetComponent<Image>().color;
        playerColorRepresentation.color = playerColor;
    }

    void OnGUI()
    {
        if (Application.loadedLevelName == "SetupLevel")
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 20), "YOUR NAME");
            playerName = GUI.TextField(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 20), playerName);

            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 200, 100, 30), "Start"))
            {
                Application.LoadLevel("Lobby");
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class ScoreboardScript : Photon.MonoBehaviour {

    Text myText;

    string[] playerNames;

    void OnEnable()
    {
        playerNames = new string[4]{"","","",""};
        if (PhotonNetwork.inRoom)
        {
            if (PhotonNetwork.room.customProperties.ContainsKey("Player1"))
            {
                for (int i = 1; i <= 4; i++)
                {
                    if ((int)PhotonNetwork.room.customProperties["Player" + i.ToString()] != -1)
                    {
                        playerNames[i - 1] = PhotonPlayer.Find((int)PhotonNetwork.room.customProperties["Player" + i.ToString()]).name;
                    }
                }
            }
        }
    }

    void Update()
    {
        myText = GetComponent<Text>();
        myText.text = "";
        for (int player = 0; player < 4; player++)
        {
            if (playerNames[player] != "")
            {
                myText.text += playerNames[player] + "\n";
                for (int hole = 0; hole < 9; hole++)
                {
                    myText.text += VarsTracker.playerScores[player, hole].ToString() + " ";
                }
                myText.text += "\n";
            }
        }
    }
}

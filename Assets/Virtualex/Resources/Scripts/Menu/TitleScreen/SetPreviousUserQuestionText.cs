using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetPreviousUserQuestionText : MonoBehaviour {

    void OnEnable()
    {
        GetComponent<Text>().text = "ARE YOU\n" + PlayerPrefs.GetString("LastUsername") + "?";
    }
}

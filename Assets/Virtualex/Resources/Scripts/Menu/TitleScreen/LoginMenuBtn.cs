using UnityEngine;
using System.Collections;

public class LoginMenuBtn : MonoBehaviour
{

    public GameObject thisMenu;
    public GameObject loginMenu;
    public GameObject keyboard;
    public GameObject existingUserMenu;
    public GameObject creditsMenu;

    public void Activate()
    {
        if (PlayerPrefs.HasKey("LastUsername") && PlayerPrefs.HasKey("LastPassword"))
        {
            existingUserMenu.SetActive(true);
        }
        else
        {
            loginMenu.SetActive(true);
            keyboard.SetActive(true);
        } 
        thisMenu.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectorCode : MonoBehaviour {

    public enum SelectorType {Menu, Keyboard };
    public enum NavigationDirection { Up, Down, Left, Right };

    public GameObject thisButton;
    public SelectorType thisSelectorType;

    public GameObject currentPanel;

	// Use this for initialization
	void Start () {
        currentPanel = transform.parent.gameObject;
        if (thisButton != null)
        {
            SetButtonColor(thisButton.GetComponent<Image>(), true);
        }
	}

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10, 1 << LayerMask.NameToLayer("MenuButtonLayer")))
        {
            if (hit.collider.gameObject != thisButton)
            {
                if (thisButton != null)
                {
                    SetButtonColor(thisButton.GetComponent<Image>(), false);
                }
                GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/UI/Navigate") as AudioClip);
            }
            thisButton = hit.collider.gameObject;
            SetButtonColor(thisButton.GetComponent<Image>(), true);
        }
        else
        {
            if (thisButton != null)
            {
                SetButtonColor(thisButton.GetComponent<Image>(), false);
            }
            thisButton = null;
        }
    }

    public GameObject GetHoveredButton()
    {
        return thisButton;
    }

    public void Navigate(NavigationDirection dir)
    {
        if (thisButton != null)
        {
            SetButtonColor(thisButton.GetComponent<Image>(), false);
            switch (dir)
            {
                case NavigationDirection.Up:
                    if (thisButton.GetComponent<MenuButtonNavigation>().UpButton != null)
                    {
                        GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/UI/Navigate") as AudioClip);
                        thisButton = thisButton.GetComponent<MenuButtonNavigation>().UpButton;
                        while (thisButton.GetComponent<GreyedOutWhenOffline>() != null)
                        {
                            if (!thisButton.GetComponent<GreyedOutWhenOffline>().isOnline())
                            {
                                thisButton = thisButton.GetComponent<MenuButtonNavigation>().UpButton;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    break;
                case NavigationDirection.Down:
                    if (thisButton.GetComponent<MenuButtonNavigation>().DownButton != null)
                    {
                        GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/UI/Navigate") as AudioClip);
                        thisButton = thisButton.GetComponent<MenuButtonNavigation>().DownButton;
                        while (thisButton.GetComponent<GreyedOutWhenOffline>() != null)
                        {
                            if (!thisButton.GetComponent<GreyedOutWhenOffline>().isOnline())
                            {
                                thisButton = thisButton.GetComponent<MenuButtonNavigation>().DownButton;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    break;
                case NavigationDirection.Left:
                    if (thisButton.GetComponent<MenuButtonNavigation>().LeftButton != null)
                    {
                        GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/UI/Navigate") as AudioClip);
                        thisButton = thisButton.GetComponent<MenuButtonNavigation>().LeftButton;
                    }
                    break;
                case NavigationDirection.Right:
                    if (thisButton.GetComponent<MenuButtonNavigation>().RightButton != null)
                    {
                        GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/UI/Navigate") as AudioClip);
                        thisButton = thisButton.GetComponent<MenuButtonNavigation>().RightButton;
                    }
                    break;
                default:
                    break;
            }
            SetButtonColor(thisButton.GetComponent<Image>(), true);
        }
    }

    public void Activate()
    {
        if (thisButton != null)
        {
            SelectorActivatedCode();
        }
    }

    void OnEnable()
    {
        if (thisButton != null)
        {
            SetButtonColor(thisButton.GetComponent<Image>(), true);
        }
    }

    void OnDisable()
    {
        if (thisButton != null)
        {
            SetButtonColor(thisButton.GetComponent<Image>(), false);
        }
    }

    void SetButtonColor(Image button, bool selected)
    {
        if (!selected)
        {
            button.color = new Color(128f / 255f, 205 / 255f, 88 / 255f, 230 / 255f);
        }
        else
        {
            button.color = new Color(128 / 255, 255 / 255f, 88 / 255f, 230 / 255f);
        }
    }


    void SelectorActivatedCode()
    {
        switch (thisSelectorType)
        {
            case SelectorCode.SelectorType.Menu:
                SelectorMainMenuCode();
                break;
            case SelectorCode.SelectorType.Keyboard:
                SelectorKeyboardCode();
                break;
            default:
                break;
        }
    }

    void SelectorMainMenuCode()
    {
        GameObject btn = thisButton;
        if (btn != null)
        {
            //GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/UI/Select") as AudioClip);
            btn.SendMessage("Activate");
        }
    }

    void SelectorKeyboardCode()
    {
        GameObject btn = thisButton;
        if (btn != null)
        {
            //if selector type is keyboard, current menu MUST be keyboard panel
            //GetComponent<AudioSource>().PlayOneShot(Resources.Load(@"Sounds/UI/Select") as AudioClip);
            currentPanel.GetComponent<KeyboardPanelCode>().AddLetter(btn.name);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// All things controlled by Vive controller input
/// </summary>
public class ViveControls : MonoBehaviour
{
    public GameObject club;
    public GameObject clubRigid;
    public PutterScript putter;
    public GameObject teleporter;
    public GameObject baseBox;
    public Transform playerHead;
    GameObject currentMenu;
    bool isHoldingClub;

    public HighlightButton LGrip;
    public HighlightButton RGrip;
    public HighlightButton Trackpad;
    public HighlightButton MenuBtn;

    public GameObject menuLaser;

    GameObject currentHeldObject;
    GameObject[] holes;

    void Start()
    {
        isHoldingClub = false;
        currentHeldObject = null;
        holes = GameObject.FindGameObjectsWithTag("WholeHole");
    }

    protected void Update()
    {
        //test for objects that can be picked up
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 1, 1 << LayerMask.NameToLayer("Holdable"));
        //highlight contorller button when in range to pick up something
        if (nearbyObjects.Length > 0 && currentHeldObject == null)
        {
            LGrip.ActivateColor();
            RGrip.ActivateColor();
        }
        else
        {
            LGrip.DeActivateColor();
            RGrip.DeActivateColor();
        }
        //grab golf club / activate item
        if (SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(currentHeldObject == null)
            {
                PickupGolfClub();
            }
            else if (currentHeldObject != clubRigid)
            {
                currentHeldObject.GetComponent<DeathmatchItemCMDHolder>().cmd.ActivateStart();
            }
        }
        //deactivate item
        if (SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (currentHeldObject != null && currentHeldObject != clubRigid)
            {
                currentHeldObject.GetComponent<DeathmatchItemCMDHolder>().cmd.ActivateEnd();
            }
        }
        //release golf club
        if (!SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetPress(SteamVR_Controller.ButtonMask.Trigger) && !SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            if (club != null && isHoldingClub)
            {
                clubRigid.GetComponent<ClubReturnToHand>().Throw();
                GetComponent<VelocityCalculatorNoAngles>().Apply(clubRigid.GetComponent<Rigidbody>());
                isHoldingClub = false;
                currentHeldObject = null;
            }
        }

        //pick up/drop item
        if (SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (currentHeldObject == null) //pickup
            {
                for (int i = 0; i < nearbyObjects.Length; i++)
                {
                    currentHeldObject = nearbyObjects[i].gameObject;
                    Rigidbody r = currentHeldObject.GetComponent<Rigidbody>();
                    r.useGravity = false;
                    r.isKinematic = true;
                    currentHeldObject.GetComponent<Collider>().enabled = false;
                    currentHeldObject.transform.parent = transform;
                    currentHeldObject.transform.localEulerAngles = new Vector3(90, 0, 0);
                    currentHeldObject.transform.localPosition = new Vector3(0, 0, 0.075f);
                    if (nearbyObjects[i].tag == "DeathmatchItem")
                    {
                        currentHeldObject.GetComponent<DeathmatchItemCMDHolder>().cmd.SetHoldingController(this);
                        if (PhotonNetwork.inRoom)
                        {
                            currentHeldObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player);
                        }
                    }
                    break;
                }
                if (currentHeldObject == null)
                {
                    PickupGolfClub();
                }
            }
            else if (currentHeldObject != clubRigid) //let go
            {
                currentHeldObject.GetComponent<Collider>().enabled = true;
                ReleaseHeldObject();
            }
        }

        //teleport
        if (SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            TeleportCode();
        }
        else
        {
            teleporter.SetActive(false);
        }
        //menu
        if (SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (currentMenu != null)
            {
                currentMenu.GetComponent<MenuPanelCode>().ActivateSelector();
            }
        }
        
        //Open Menu
        if (SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && Application.loadedLevelName != "LoginScene")
        {
            if (VarsTracker.Menu == null)
            {
                if (Application.loadedLevelName == "TutorialScene")
                {
                    VarsTracker.Menu = Instantiate(Resources.Load(@"Prefabs/Menu/MenuSystemTutorial"), transform.position + transform.forward * 15, Quaternion.identity) as GameObject;
                }
                else
                {
                    VarsTracker.Menu = Instantiate(Resources.Load(@"Prefabs/Menu/MenuSystem"), transform.position + transform.forward * 15, Quaternion.identity) as GameObject;
                }
            }
            else
            {
                Destroy(VarsTracker.Menu);
                VarsTracker.Menu = null;
            }
        }
        //menu selection
        /*
         * CHANGE THIS TO ONLY ONE MENU PANEL
         * AT LEAST UNTIL I CAN FIND A BETTER IMPLEMENTATION OF UI
         * 
         */
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, 1 << LayerMask.NameToLayer("MenuLayer")))
        {
            menuLaser.SetActive(true);
            Trackpad.ActivateColor();
            if (currentMenu != hit.collider.gameObject)
            {
                if (currentMenu != null)
                {
                    currentMenu.GetComponent<MenuPanelCode>().DeSelected();
                }
                currentMenu = hit.collider.gameObject;
                currentMenu.GetComponent<MenuPanelCode>().Selected();
            }
            currentMenu.GetComponent<MenuPanelCode>().selector.transform.position = hit.point;
            currentMenu.GetComponent<MenuPanelCode>().selector.transform.localPosition -= Vector3.forward;
            //MenuCode();
        }
        else
        {
            menuLaser.SetActive(false);
            Trackpad.DeActivateColor();
            if (currentMenu != null)
            {
                currentMenu.GetComponent<MenuPanelCode>().DeSelected();
            }
            currentMenu = null;
        }
    }

    void PickupGolfClub()
    {
        if (clubRigid.activeSelf)
        {
            currentHeldObject = clubRigid;
            clubRigid.transform.parent = club.transform;
            clubRigid.GetComponent<ClubReturnToHand>().ReturnToHand();
            isHoldingClub = true;
        }
    }

    //release held object
    public void ReleaseHeldObject()
    {
        Rigidbody r = currentHeldObject.GetComponent<Rigidbody>();
        currentHeldObject.transform.SetParent(null, true);
        GetComponent<VelocityCalculatorNoAngles>().Apply(r);
        r.isKinematic = false;
        r.useGravity = true;
        currentHeldObject = null;
    }

    void MenuCode()
    {
        Vector2 scroll = new Vector2(GetComponent<ViveTrackpadDataManager>().GetDataForInputType(ViveTrackpadDataManager.InputType.HorizontalScroll), GetComponent<ViveTrackpadDataManager>().GetDataForInputType(ViveTrackpadDataManager.InputType.VerticalScroll));
        if (scroll.x < 0)
        {
            currentMenu.GetComponent<MenuPanelCode>().MoveSelector(SelectorCode.NavigationDirection.Left);
        }
        else if (scroll.x > 0)
        {
            currentMenu.GetComponent<MenuPanelCode>().MoveSelector(SelectorCode.NavigationDirection.Right);
        }
        if (scroll.y > 0)
        {
            currentMenu.GetComponent<MenuPanelCode>().MoveSelector(SelectorCode.NavigationDirection.Up);
        }
        else if (scroll.y < 0)
        {
            currentMenu.GetComponent<MenuPanelCode>().MoveSelector(SelectorCode.NavigationDirection.Down);
        }
    }

    void EmergencyTeleport()
    {
        if (GameObject.FindGameObjectWithTag("MyBall") != null)
        {
            putter.Teleported();
            baseBox.transform.position = GameObject.FindGameObjectWithTag("MyBall").transform.position;
            VarsTracker.atBall = true;
        }
    }

    void TeleportCode()
    {
        RaycastHit hit;
        int onlyDefaultAndCourseLayer = (1 << LayerMask.NameToLayer("Default")) + (1 << LayerMask.NameToLayer("MenuLayer")) + (1 << LayerMask.NameToLayer("Course")) + (1 << LayerMask.NameToLayer("NotTeleportable"));
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000, onlyDefaultAndCourseLayer))
        {
            if (hit.normal.y > 0.95f && hit.collider.gameObject.layer != LayerMask.NameToLayer("NotTeleportable")) //only teleport to relatively flat surfaces, and surfaces that aren't 'not teleportable'
            {
                //show teleport particle effect
                if (!teleporter.activeSelf)
                {
                    teleporter.SetActive(true);
                }

                //trackpad integration for teleport facing.
                teleporter.transform.position = hit.point;
                teleporter.transform.rotation = Quaternion.LookRotation(new Vector3(transform.forward.x, 0, transform.forward.z));
                teleporter.transform.eulerAngles = new Vector3(270, teleporter.transform.eulerAngles.y, 0);
                
                //button pressed -> teleport
                if (SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    putter.Teleported();
                    baseBox.GetComponent<TeleportScript>().Teleport(hit.point);
                    baseBox.GetComponent<SetNewHole>().SetHoleFromTeleport(FindClosestHole());
                }
            }
            else
            {
                teleporter.SetActive(false);
            }
        }
        else
        {
            teleporter.SetActive(false);
        }
    }
    
    private GameObject FindClosestHole()
    {
        GameObject closestHole = null;
        if (holes.Length > 0)
        {
            float distanceToHole = Mathf.Infinity;
            for (int i = 0; i < holes.Length; i++)
            {
                float distance = Vector3.Distance(baseBox.transform.position, holes[i].transform.position);
                if (distance < distanceToHole)
                {
                    distanceToHole = distance;
                    closestHole = holes[i];
                }
            }
        }

        return closestHole;
    }
}
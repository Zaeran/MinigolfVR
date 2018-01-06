using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;

public class VarsTracker : Photon.MonoBehaviour {

    public enum DeviceType { Vive, Rift, Touch };

    void Awake()
    {
        deviceType = DeviceType.Vive;
        DontDestroyOnLoad(gameObject);
        ResetVars();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnLevelWasLoaded()
    {
        ResetVars();
    }

    void ResetVars()
    {
        atBall = false;
        LevelComplete = false;
        playerScores = new int[4, 9];
        isPlacingBall = false;
        holesComplete = new bool[9] { false, false, false, false, false, false, false, false, false };
        ballPositions = new Vector3[9] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
        ballVelocities = new Vector3[9] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
        ballAngulars = new Vector3[9] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
        ballGravity = new bool[9] { true, true, true, true, true, true, true, true, true };
        playerColor = Color.white;
        PushtoTalkPressed = true;
        currentHole = 1;
    }

    /*
    void OnGUI()
    {
        GUILayout.Label((1.0f / Time.deltaTime).ToString());
    }

     */

    public static bool atBall;
    public static bool LevelComplete;
    public static bool isPlacingBall;
    public static bool ValuesCreated;
    public static bool PushtoTalkPressed;
    public static bool PlayMusic = true;
    public static bool[] holesComplete;
    public static bool[] ballGravity;
    public static int currentHole = 1;
    public static int playerID = 0;
    public static int[,] playerScores;
    public static float clubHitModifier;
    public static string PlayerName = "";
    public static string CurrentRoom = "";
    public static string CurrentCourseName = "";
    public static PhotonPlayer[] players;
    public static Vector3[] ballPositions;
    public static Vector3[] ballVelocities;
    public static Vector3[] ballAngulars;
    public static Color playerColor;    
    public static GameObject Menu;
    public static DeviceType deviceType;

    public static string MinigolfNetworkBasePath = @"http://virtualex.com.au/uploads/";
}

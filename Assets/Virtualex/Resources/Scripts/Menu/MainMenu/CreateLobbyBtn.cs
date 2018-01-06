using UnityEngine;
using System.Collections;
using Photon;

public class CreateLobbyBtn : Photon.PunBehaviour {

    public GameObject thisMenu;
    public GameObject courseSelection;
    public Canvas baseCanvas;

    public void Activate()
    {
        CreateRoom();
    }

    void CreateRoom()
    {
        string randomRoomName = CreateRandomRoomName();
        RoomOptions ro = new RoomOptions();
        ro.isVisible = true;
        ro.maxPlayers = 4;
        if (PhotonNetwork.CreateRoom(randomRoomName, ro, TypedLobby.Default))
        {
            StartCoroutine(CourseSelectOnCreated());
        }        
    }

    IEnumerator CourseSelectOnCreated()
    {
        baseCanvas.enabled = false;
        baseCanvas.GetComponent<BoxCollider>().enabled = false;
        while (!PhotonNetwork.inRoom)
        {
            yield return new WaitForSeconds(0.25f);
        }
        baseCanvas.enabled = true;
        baseCanvas.GetComponent<BoxCollider>().enabled = true;
        courseSelection.SetActive(true);
        thisMenu.SetActive(false);
    }

    private string CreateRandomRoomName()
    {
        string name = "";
        int roomNameLength = Random.Range(5, 12);
        for (int i = 0; i < roomNameLength; i++)
        {
            name += (char)Random.Range(65, 123);
        }
        while (!VerifyRoomName(name))
        {
            name += (char)Random.Range(65, 123);
        }
        return name;
    }

    private bool VerifyRoomName(string roomName)
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].name == roomName)
            {
                return false;
            }
        }
        return true;
    }
}

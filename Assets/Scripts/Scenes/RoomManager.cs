using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private GameObject[] allRooms;
    public static RoomManager Instance;

    [SerializeField]
    private GameObject currentRoom;

    private void Awake()
    {
        Instance = this;

         allRooms = GameObject.FindGameObjectsWithTag(
        "Room"
    );

    foreach (GameObject room in allRooms)
{
    room.SetActive(false);
}

if (currentRoom != null)
{
    currentRoom.SetActive(true);
}
    }

public void ChangeRoom(GameObject nextRoom, Transform[] spawnPoints)
{
    Debug.Log("Mudando para sala: " + nextRoom.name);

    foreach (GameObject room in allRooms)
    {
        room.SetActive(false);
    }

    nextRoom.SetActive(true);

    CharacterManager.Instance.TeleportAllPlayers(
        spawnPoints
    );

    currentRoom = nextRoom;
}

public GameObject GetRoomByName(string roomName)
{
    foreach (GameObject room in allRooms)
    {
        if (room.name == roomName)
        {
            return room;
        }
    }

    return null;
}
}
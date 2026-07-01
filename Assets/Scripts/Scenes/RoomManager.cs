using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    [SerializeField]
    private GameObject currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeRoom(GameObject nextRoom, Transform[] spawnPoints)
    {
        if (currentRoom != null)
        {
            currentRoom.SetActive(false);
        }

        nextRoom.SetActive(true);

        CharacterManager.Instance.TeleportAllPlayers(spawnPoints);

        currentRoom = nextRoom;
    }
}
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
    Debug.Log("Mudando para sala: " + nextRoom.name);

    if (currentRoom != null)
    {
        Debug.Log("Desativando: " + currentRoom.name);
        currentRoom.SetActive(false);
    }

    Debug.Log("Ativando: " + nextRoom.name);
    nextRoom.SetActive(true);

    CharacterManager.Instance.TeleportAllPlayers(spawnPoints);

    currentRoom = nextRoom;
}
}
using UnityEngine;

public class DoorTransition : MonoBehaviour
{
    [SerializeField]
    private GameObject destinationRoom;

    [SerializeField]
    private Transform[] spawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        RoomManager.Instance.ChangeRoom(destinationRoom, spawnPoints);

        GameManager.Instance.SaveCheckpoint(
            destinationRoom,
            spawnPoints
        );
    }
}
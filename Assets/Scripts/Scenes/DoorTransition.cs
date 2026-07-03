using UnityEngine;

public class DoorTransition : MonoBehaviour
{
    [SerializeField]
    private GameObject destinationRoom;

    [SerializeField]
    private Transform[] spawnPoints;

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isTransitioning)
            return;

        isTransitioning = true;

        GameManager.Instance.ChangeRoomWithFade(
            destinationRoom,
            spawnPoints
        );
    }

    private void OnDisable()
    {
        isTransitioning = false;
    }
}
using UnityEngine;

public class DoorTransition : MonoBehaviour
{
    [SerializeField]
    private GameObject destinationRoom;

    [SerializeField]
    private Transform[] spawnPoints;

    private bool isTransitioning = false;

   
[SerializeField]
private AudioSource portalAudio;



    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isTransitioning)
            return;

   isTransitioning = true;

    if (portalAudio != null)
    {
        portalAudio.Play();
        
    }
    

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
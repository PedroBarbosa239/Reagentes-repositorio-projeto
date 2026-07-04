using UnityEngine;

public class VoidKillZone : MonoBehaviour
{

    [SerializeField]
private AudioSource deathAudio;
   private void OnTriggerEnter(Collider other)
{
    Debug.Log("Colidiu com: " + other.name);

    var player = other.GetComponent<MovimentPlayer>();

    if (player != null)
    {
        Debug.Log("Player detectado");

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance é NULL");
            return;
        }

        if (deathAudio != null)
        {
            deathAudio.Play();
        }

        GameManager.Instance.PlayerDied();
    }
}
}
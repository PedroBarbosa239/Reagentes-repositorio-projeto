using UnityEngine;

public class VoidKillZone : MonoBehaviour
{
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

            GameManager.Instance.PlayerDied();
        }
    }
}
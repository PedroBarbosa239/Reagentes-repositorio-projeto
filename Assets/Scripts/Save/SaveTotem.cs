using UnityEngine;

public class SaveTotem : MonoBehaviour
{
    [SerializeField]
    private GameObject room;

    [SerializeField]
    private Transform[] spawnPoints;

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;

        SaveMessageUI.Instance.ShowMessage(
    "Pressione [I] para salvar"
);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
    }

    private void Update()
    {
        if (!playerInside)
            return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.Instance.SaveCheckpoint(
                room,
                spawnPoints
            );

            GameManager.Instance.SaveGame();

            SaveMessageUI.Instance.ShowMessage(
    "Jogo salvo!"
);
        }
    }
}
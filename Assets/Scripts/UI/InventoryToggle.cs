using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryUI;

    private void Update()
    {
        // Aguarda o CharacterManager ser inicializado
        if (CharacterManager.Instance == null)
            return;

        // Apenas o Pedro (Personagem A) pode abrir o inventário
        if (CharacterManager.Instance.CurrentPlayerIndex != 0)
        {
            // Se o inventário estiver aberto ao trocar para a Sarah, fecha
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
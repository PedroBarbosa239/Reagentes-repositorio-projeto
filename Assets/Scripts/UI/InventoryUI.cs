using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject slotPrefab;
    public Transform slotsParent;

    void Start()
    {
        UpdateUI();
    }

   public void UpdateUI()
{
    Debug.Log("Atualizando UI");

    foreach (Transform child in slotsParent)
    {
        Destroy(child.gameObject);
    }

    foreach (Item item in inventory.items)
    {
        Debug.Log("Criando slot para: " + item.itemName);

        GameObject slotObj =
            Instantiate(slotPrefab, slotsParent);

        Debug.Log("Slot criado");

        InventorySlotUI slot =
            slotObj.GetComponent<InventorySlotUI>();

        Debug.Log("Componente encontrado: " + slot);

        slot.inventory = inventory;
        slot.inventoryUI = this;
        slot.SetItem(item);

        Debug.Log("Ícone definido");
    }
}
}
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public Inventory inventory;
    public GameObject slotPrefab;
    public Transform slotsParent;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
        Debug.Log(Application.persistentDataPath);
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

            InventorySlotUI slot =
                slotObj.GetComponent<InventorySlotUI>();

            slot.inventory = inventory;
            slot.inventoryUI = this;
            slot.SetItem(item);

            Debug.Log("Ícone definido");
        }
    }

    public void ShowUseButtons(bool show)
    {
        InventorySlotUI[] slots =
            GetComponentsInChildren<InventorySlotUI>();

        foreach (InventorySlotUI slot in slots)
        {
            if (slot.useButton != null)
                slot.useButton.gameObject.SetActive(show);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;

    public Item currentItem;

    public Inventory inventory;
    public InventoryUI inventoryUI;

    public void SetItem(Item item)
    {
        currentItem = item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ConsumeItem()
    {
        if (currentItem == null)
            return;

        inventory.items.Remove(currentItem);

        inventoryUI.UpdateUI();
    }
}
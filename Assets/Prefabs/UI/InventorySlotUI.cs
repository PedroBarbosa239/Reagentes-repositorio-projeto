using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Button useButton;

    public Item currentItem;

    public Inventory inventory;
    public InventoryUI inventoryUI;

    private void Awake()
    {
        if (icon == null)
            icon = transform.Find("Icon").GetComponent<Image>();

        if (useButton == null)
            useButton = transform.Find("UseButton").GetComponent<Button>();
    }

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
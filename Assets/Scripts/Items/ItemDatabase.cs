using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    [SerializeField]
    private Item[] allItems;

    private void Awake()
    {
        Instance = this;
    }

    public Item GetItemByID(string id)
    {
        foreach (Item item in allItems)
        {
            if (item.itemID == id)
            {
                return item;
            }
        }

        return null;
    }
}
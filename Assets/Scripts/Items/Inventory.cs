using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Lista de itens no inventário
    public List<Item> items = new List<Item>();

    // Adiciona um item ao inventário
    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log(item.itemName + " adicionado ao inventário.");
    }

    // Remove um item pelo índice
    public void RemoveItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Debug.Log(items[index].itemName + " removido do inventário.");
            items.RemoveAt(index);
        }
    }

    // Exibe todos os itens no inventário (por exemplo, no console)
    public void ListItems()
    {
        Debug.Log("Itens no inventário:");
        foreach (var item in items)
        {
            Debug.Log(item.itemName);
        }
    }

    //save
        public List<Item> GetItemsCopy()
    {
        return new List<Item>(items);
    }

    public void LoadItems(List<Item> savedItems)
    {
        items = new List<Item>(savedItems);

    Debug.Log("Inventário restaurado:");
    foreach (var item in items)
    {
        Debug.Log(item.itemName);
    }
    }
}
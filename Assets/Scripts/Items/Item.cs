using UnityEngine;

[CreateAssetMenu(fileName = "NovoItem", menuName = "Itens/Item")]
public class Item : ScriptableObject
{
    public string itemID;

    public string itemName;

    public Sprite icon;
}
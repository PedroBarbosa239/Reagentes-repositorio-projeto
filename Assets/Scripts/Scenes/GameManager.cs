using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private InventoryUI inventoryUI;

    [SerializeField]
    private Inventory inventory;

        [SerializeField]
    private GameObject initialRoom;

    [SerializeField]
    private Transform[] initialSpawnPoints;
    private WorldItem[] allWorldItems;
    private GameObject savedRoom;
    private Transform[] savedSpawnPoints;
    private HashSet<string> collectedItems = new();
    private HashSet<string> savedCollectedItems = new();
    private List<Item> savedInventory = new();

private void Start()
{
    SaveCheckpoint(
        initialRoom,
        initialSpawnPoints
    );

       allWorldItems = FindObjectsByType<WorldItem>(
        FindObjectsSortMode.None
    );
}

public void RegisterCollectedItem(string itemID)
{
    collectedItems.Add(itemID);
}
    public void SaveCheckpoint(
        GameObject room,
        Transform[] spawnPoints)
    {
        savedRoom = room;
        savedSpawnPoints = (Transform[])spawnPoints.Clone();

        savedInventory = inventory.GetItemsCopy();

    savedCollectedItems =
        new HashSet<string>(collectedItems);

        Debug.Log("Checkpoint salvo.");
    }

public void PlayerDied()
{
    StartCoroutine(RespawnRoutine());
}

private IEnumerator RespawnRoutine()
{
    yield return StartCoroutine(
        DeathFade.Instance.FadeOut(0.5f)
    );

    if(savedRoom == null || savedSpawnPoints == null)
    {
        Debug.LogWarning("Nenhum checkpoint salvo!");
        yield break;
    }

    inventory.LoadItems(savedInventory);

    if (inventoryUI != null)
    {
        inventoryUI.UpdateUI();
    }

    RestoreWorldItems();

    RoomManager.Instance.ChangeRoom(
        savedRoom,
        savedSpawnPoints
    );

    yield return StartCoroutine(
        DeathFade.Instance.FadeIn(0.5f)
    );
}

private void Awake()
{
    Instance = this;
    Debug.Log("GameManager iniciado!");
}

private void RestoreWorldItems()
{
    foreach (WorldItem item in allWorldItems)
    {
        if(item == null)
            continue;

        bool shouldExist =
            !savedCollectedItems.Contains(
                item.worldItemID
            );

        item.gameObject.SetActive(shouldExist);
    }
}
}
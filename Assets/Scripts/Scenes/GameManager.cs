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
    allWorldItems = FindObjectsByType<WorldItem>(
        FindObjectsSortMode.None
    );

    if (GameLoader.LoadSaveOnStart)
    {
        GameLoader.LoadSaveOnStart = false;

        LoadGame();
    }
    else
    {
        SaveCheckpoint(
            initialRoom,
            initialSpawnPoints
        );
    }
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

        
        SaveGame();

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
public void ChangeRoomWithFade(
    GameObject destinationRoom,
    Transform[] spawnPoints)
{
    StartCoroutine(
        ChangeRoomRoutine(
            destinationRoom,
            spawnPoints
        )
    );
}

private IEnumerator ChangeRoomRoutine(
    GameObject destinationRoom,
    Transform[] spawnPoints)
{
    yield return StartCoroutine(
        DeathFade.Instance.FadeOut(0.5f)
    );

    RoomManager.Instance.ChangeRoom(
        destinationRoom,
        spawnPoints
    );

    SaveCheckpoint(
        destinationRoom,
        spawnPoints
    );

    yield return StartCoroutine(
        DeathFade.Instance.FadeIn(0.5f)
    );
}

public void SaveGame()
{
    SaveData data = new SaveData();

    data.roomName = savedRoom.name;

    data.spawnIndex = 0;

    foreach(Item item in savedInventory)
    {
        data.inventoryItems.Add(
            item.itemID
        );
    }

    foreach(string itemID in savedCollectedItems)
    {
        data.collectedItems.Add(
            itemID
        );
    }

    SaveSystem.Save(data);
}

public void LoadGame()
{
    SaveData data = SaveSystem.Load();

    if (data == null)
    {
        Debug.Log("Nenhum save encontrado.");
        return;
    }

    Debug.Log("Save encontrado!");

    GameObject room =
        GameObject.Find(data.roomName);

    if (room == null)
    {
        Debug.LogError("Sala não encontrada: " + data.roomName);
        return;
    }

    Transform spawnA =
        room.transform.Find("SpawnA");

    Transform spawnB =
        room.transform.Find("SpawnB");

    if (spawnA == null || spawnB == null)
    {
        Debug.LogError(
            "SpawnA ou SpawnB não encontrados em " +
            room.name
        );
        return;
    }

    Transform[] spawnPoints =
    {
        spawnA,
        spawnB
    };

    SaveCheckpoint(
        room,
        spawnPoints
    );

    RoomManager.Instance.ChangeRoom(
        room,
        spawnPoints
    );
}

}
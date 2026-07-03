using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private HashSet<string> savedTriggeredDialogues;

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
    private HashSet<string> triggeredDialogues =
    new HashSet<string>();
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
    if (GameLoader.ShowTutorial)
{
    GameLoader.ShowTutorial = false;
    
    TutorialManager.Instance.StartTutorial();
}else
{
   StartCoroutine(
        DeathFade.Instance.FadeIn(0.8f)
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
Debug.Log("Room: " + room);
Debug.Log("SpawnPoints: " + spawnPoints);
Debug.Log("Inventory: " + inventory);
Debug.Log("CollectedItems: " + collectedItems);
        savedInventory = inventory.GetItemsCopy();

    savedCollectedItems =
        new HashSet<string>(collectedItems);
    savedTriggeredDialogues =
        new HashSet<string>(triggeredDialogues);
        


        Debug.Log("Checkpoint salvo.");

   
    }

public bool HasPlayedDialogue(string dialogueID)
{
    bool played = triggeredDialogues.Contains(dialogueID);

    Debug.Log("HasPlayedDialogue(" + dialogueID + ") = " + played);

    return played;
}

public void RegisterDialogue(string dialogueID)
{
     Debug.Log("Registrando diálogo: " + dialogueID);
    triggeredDialogues.Add(dialogueID);
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
    yield return new WaitForSeconds(0.2f);

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
    triggeredDialogues =
    new HashSet<string>(savedTriggeredDialogues);

    RoomManager.Instance.ChangeRoom(
        savedRoom,
        savedSpawnPoints
    );
    yield return new WaitForSeconds(0.15f);
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
        DeathFade.Instance.FadeOut(0.8f)
    );

    // Pequena pausa com a tela totalmente preta
    yield return new WaitForSeconds(0.2f);

    RoomManager.Instance.ChangeRoom(
        destinationRoom,
        spawnPoints
    );

    // Mantém a tela preta por um instante após a troca
    yield return new WaitForSeconds(0.15f);

    yield return StartCoroutine(
        DeathFade.Instance.FadeIn(0.8f)
    );
}

public void SaveGame()
{
    if (savedRoom == null)
    {
        Debug.LogWarning("Nenhum checkpoint salvo. Jogo não foi salvo.");
        return;
    }

    SaveData data = new SaveData();

    data.roomName = savedRoom.name;

    data.spawnIndex = 0;

    foreach (Item item in savedInventory)
    {
        data.inventoryItems.Add(item.itemID);
    }

    foreach (string itemID in savedCollectedItems)
    {
        data.collectedItems.Add(itemID);
    }

    foreach (string dialogueID in savedTriggeredDialogues)
{
    data.triggeredDialogues.Add(dialogueID);
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
    RoomManager.Instance.GetRoomByName(
        data.roomName
    );

    if (room == null)
    {
        Debug.LogError(
            "Sala não encontrada: " +
            data.roomName
        );
        return;
    }

    Transform spawnA =
        room.transform.Find("SpawnA");

    Transform spawnB =
        room.transform.Find("SpawnB");

    Transform[] spawnPoints =
    {
        spawnA,
        spawnB
    };

    // INVENTÁRIO
    List<Item> loadedItems =
        new List<Item>();

    foreach (string itemID in data.inventoryItems)
    {
        Item item =
            ItemDatabase.Instance.GetItemByID(
                itemID
            );

        if (item != null)
        {
            loadedItems.Add(item);
        }
    }

    inventory.LoadItems(loadedItems);

    // ITENS COLETADOS
    collectedItems =
        new HashSet<string>(
            data.collectedItems
        );

    savedCollectedItems =
        new HashSet<string>(
            data.collectedItems
        );

    // DIÁLOGOS JÁ EXECUTADOS
    triggeredDialogues =
        new HashSet<string>(
            data.triggeredDialogues
        );

    savedTriggeredDialogues =
        new HashSet<string>(
            data.triggeredDialogues
        );

    RestoreWorldItems();

    if (inventoryUI != null)
    {
        inventoryUI.UpdateUI();
    }

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
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string roomName;

    public int spawnIndex;

    public List<string> inventoryItems =
        new List<string>();

    public List<string> collectedItems =
        new List<string>();

    public List<string> triggeredDialogues =
    new List<string>();
}
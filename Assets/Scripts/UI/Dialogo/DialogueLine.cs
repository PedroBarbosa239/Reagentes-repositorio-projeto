using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    public string characterName;

    [TextArea(2,4)]
    public string text;
}
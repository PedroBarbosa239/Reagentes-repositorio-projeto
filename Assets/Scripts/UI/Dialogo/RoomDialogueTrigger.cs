using System.Collections;
using UnityEngine;

public class RoomDialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private string dialogueID;

    [SerializeField]
    private Dialogue dialogue;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            yield break;

        if (GameManager.Instance.HasPlayedDialogue(dialogueID))
            yield break;
Debug.Log(GameManager.Instance);
        GameManager.Instance.RegisterDialogue(dialogueID);

        yield return StartCoroutine(
            DialogueManager.Instance.PlayDialogue(dialogue)
        );
    }
}
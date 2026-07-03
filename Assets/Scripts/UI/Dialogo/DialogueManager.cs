using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance;

    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TMP_Text characterName;

    [SerializeField]
    private TMP_Text dialogueText;

    private Dialogue currentDialogue;
    private int currentLine;

    private bool isTyping;
  

    private void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

   public IEnumerator PlayDialogue(Dialogue dialogue)
{
    currentDialogue = dialogue;
    currentLine = 0;

    dialoguePanel.SetActive(true);

    CharacterManager.Instance.CanSwitchCharacters = false;
    CharacterManager.Instance.CurrentPlayer.SetControlled(false);

    while (currentLine < currentDialogue.lines.Count)
    {
        DialogueLine line = currentDialogue.lines[currentLine];

        characterName.text = line.characterName;

    
       

        yield return StartCoroutine(TypeText(line.text));

    
        // Espera SOLTAR o espaço
        yield return new WaitUntil(() => !Input.GetKey(KeyCode.Space));

        // Espera um NOVO espaço
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        currentLine++;
    }

    dialoguePanel.SetActive(false);

    CharacterManager.Instance.CanSwitchCharacters = true;
    CharacterManager.Instance.CurrentPlayer.SetControlled(true);
}
    private IEnumerator TypeText(string text)
{
    dialogueText.text = "";

    foreach (char c in text)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueText.text = text;
            yield break;
        }

        dialogueText.text += c;

        float timer = 0f;

        while (timer < 0.03f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.text = text;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
}
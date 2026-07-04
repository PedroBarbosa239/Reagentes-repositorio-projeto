using TMPro;
using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
[SerializeField]
private Dialogue introDialogue;
    [SerializeField]
private TMP_Text pageText;

[SerializeField]
private TMP_Text continueText;


    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private TMP_Text descriptionText;

    private int currentPage;

    private readonly string[] titles =
    {
        "Movimentação",
        "Pedro",
        "Sarah"
    };

    private readonly string[] descriptions =
    {
        "WASD - Movimentar\n\nE - Trocar personagem\n\nSpace - Pular\n",
        
        "Q - Consumir coisas\n",
        
        "Q - Poder de troca\nA/D - Trocar NPC\nSegure Q(1s) - Possuir ou retornar\n"
    };

    private bool showingTutorial = false;

    public bool IsShowingTutorial => showingTutorial;

    private void Awake()
    {
          Debug.Log("TutorialManager Awake");
        Instance = this;

        tutorialPanel.SetActive(false);
    }

    public void StartTutorial()
    {

        
        showingTutorial = true;

        currentPage = 0;

        tutorialPanel.SetActive(true);

        CharacterManager.Instance.CanSwitchCharacters = false;
        CharacterManager.Instance.CurrentPlayer.SetControlled(false);

        ShowPage();
    }

    private void Update()
    {
        if (!showingTutorial)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPage++;

            if (currentPage >= titles.Length)
            {
                FinishTutorial();
                return;
            }

            ShowPage();
        }
    }

  void ShowPage()
{
    titleText.text = titles[currentPage];

    descriptionText.text = descriptions[currentPage];

    pageText.text =
        (currentPage + 1) +
        " / " +
        titles.Length;

    if (currentPage == titles.Length - 1)
    {
        continueText.text =
            "Pressione ESPAÇO para começar";
    }
    else
    {
        continueText.text =
            "Pressione ESPAÇO para continuar";
    }
}

    void FinishTutorial()
{
    showingTutorial = false;
    StartCoroutine(FinishTutorialRoutine());
}

private IEnumerator FinishTutorialRoutine()
{
    // Esconde o tutorial
    tutorialPanel.SetActive(false);

    // Pequena pausa
    yield return new WaitForSeconds(0.1f);

    // Intro
    yield return StartCoroutine(
        IntroSequence.Instance.PlayIntro()
    );

    // Revela o jogo lentamente
    yield return StartCoroutine(
        DeathFade.Instance.FadeIn(1.8f)
    );

    // Diálogo inicial
    yield return StartCoroutine(
        DialogueManager.Instance.PlayDialogue(
            introDialogue
        )
    );

    
}
}
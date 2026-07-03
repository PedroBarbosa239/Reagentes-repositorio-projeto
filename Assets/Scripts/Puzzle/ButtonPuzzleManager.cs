using UnityEngine;

public class ButtonPuzzleManager : MonoBehaviour
{
    public static ButtonPuzzleManager Instance;

    [SerializeField] private int totalButtons = 9;
    [SerializeField] private GameObject endDoor;
    [SerializeField] private PuzzleButton[] buttons;

    private int pressedButtons = 0;

    private void Awake()
    {
        Instance = this;

        if (endDoor != null)
            endDoor.SetActive(false);
    }

    public void ButtonPressed()
    {
        pressedButtons++;

        PuzzleUI.Instance.UpdateCounter(pressedButtons, totalButtons);

        if (pressedButtons >= totalButtons)
            PuzzleCompleted();
    }

    private void PuzzleCompleted()
    {
        Debug.Log("Puzzle Completo!");

        if (endDoor != null)
            endDoor.SetActive(true);
    }

   public bool[] GetButtonsState()
{
    Debug.Log("Quantidade de botões: " + buttons.Length);

    bool[] state = new bool[buttons.Length];

    for (int i = 0; i < buttons.Length; i++)
    {
        state[i] = buttons[i].IsActivated;

        Debug.Log($"Botão {i}: {state[i]}");
    }

    return state;
}

    // ===== ESTE MÉTODO ESTAVA FALTANDO =====
    public void LoadButtons(bool[] state)
    {
            Debug.Log("LoadButtons chamado!");

        if (state == null)
        {
            Debug.Log("State é NULL");
            return;
        }

    Debug.Log("Quantidade de estados: " + state.Length);

        pressedButtons = 0;

        for (int i = 0; i < buttons.Length && i < state.Length; i++)
        {
            if (state[i])
            {
                buttons[i].Activate();
                pressedButtons++;
            }
        }

        if (pressedButtons > 0)
            PuzzleUI.Instance.UpdateCounter(pressedButtons, totalButtons);

        if (pressedButtons >= totalButtons)
            PuzzleCompleted();
    }
}
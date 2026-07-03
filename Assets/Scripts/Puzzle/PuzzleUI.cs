using TMPro;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    public static PuzzleUI Instance;

    [SerializeField] private TextMeshProUGUI counterText;

    private void Awake()
    {
        Instance = this;
        counterText.gameObject.SetActive(false);
    }
public void UpdateCounter(int current, int total)
{
    Debug.Log("UpdateCounter foi chamado!");

    counterText.gameObject.SetActive(true);
    counterText.text = $"{current}/{total}";
}

    public void HideCounter()
    {
        counterText.gameObject.SetActive(false);
    }
}
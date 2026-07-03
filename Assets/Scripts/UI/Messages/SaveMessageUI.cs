using System.Collections;
using TMPro;
using UnityEngine;

public class SaveMessageUI : MonoBehaviour
{
    public static SaveMessageUI Instance;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TMP_Text messageText;

    [Header("Configuração")]
    [SerializeField]
    private float fadeDuration = 0.35f;

    [SerializeField]
    private float visibleTime = 2f;

    private void Awake()
    {
        Instance = this;
        canvasGroup.alpha = 0;
    }

    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(message));
    }

    private IEnumerator ShowRoutine(string message)
    {
        messageText.text = message;

        // Fade In
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;

            canvasGroup.alpha = Mathf.Lerp(
                0,
                1,
                t / fadeDuration
            );

            yield return null;
        }

        canvasGroup.alpha = 1;

        // Tempo visível
        yield return new WaitForSecondsRealtime(
            visibleTime
        );

        // Fade Out
        t = 0;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;

            canvasGroup.alpha = Mathf.Lerp(
                1,
                0,
                t / fadeDuration
            );

            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
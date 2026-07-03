using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathFade : MonoBehaviour
{
    public static DeathFade Instance;

    [SerializeField]
    private Image fadeImage;

    private void Awake()
    {
        Instance = this;
          SetBlack();
    }

    // Deixa a tela preta imediatamente
    public void SetBlack()
    {
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;
    }

    // Deixa a tela transparente imediatamente
    public void SetClear()
    {
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
    }

    public IEnumerator FadeOut(float duration)
    {
        float time = 0;

        Color color = fadeImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(0, 1, time / duration);

            fadeImage.color = color;

            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn(float duration)
    {
        float time = 0;

        Color color = fadeImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(1, 0, time / duration);

            fadeImage.color = color;

            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;
    }
}
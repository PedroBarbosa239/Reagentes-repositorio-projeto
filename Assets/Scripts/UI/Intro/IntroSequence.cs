using System.Collections;
using TMPro;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    public static IntroSequence Instance;
[SerializeField]
private AudioSource gameMusic;
    [SerializeField]
private LetterJitter jitter;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private TMP_Text introText;
    private bool isPlaying = false;

   private void Awake()
{
    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }

    Instance = this;

    panel.SetActive(false);
}

public IEnumerator PlayIntro()
{
    Debug.Log(">>> PlayIntro INICIO");
    if (isPlaying)
        yield break;

    isPlaying = true;

    panel.SetActive(true);

    yield return ShowMessage("EU AVISEI", 2f, 0.04f);

    yield return new WaitForSeconds(0.5f);

    yield return ShowMessage("CRIANÇAS... ILUDIDAS", 2f, 0.05f);

    yield return new WaitForSeconds(0.5f);

    yield return ShowMessage("AGORA BRINQUEM NO MEU MUNDO", 2.5f, 0.05f);

    yield return new WaitForSeconds(0.5f);

    yield return ShowMessage("NÃO ME PROCUREM MAIS", 3.5f, 0.08f);

    yield return new WaitForSeconds(1.2f);

    panel.SetActive(false);

    isPlaying = false; 
    if (gameMusic != null)
    {
        gameMusic.Play();
    }
    Debug.Log("<<< PlayIntro FIM");
}



   private IEnumerator ShowMessage(
    string text,
    float stayTime,
    float typingSpeed)
{
    introText.text = "";

    Vector3 originalPos =
        introText.rectTransform.localPosition;

    // Digita letra por letra
    foreach (char letter in text)
    {
        introText.text += letter;

        yield return new WaitForSeconds(typingSpeed);
    }

    // Texto completo fica na tela tremendo
    float timer = 0f;

    while (timer < stayTime)
    {
        timer += Time.deltaTime;

        introText.rectTransform.localPosition =
            originalPos +
            (Vector3)Random.insideUnitCircle * 0.8f;

        yield return null;
    }

    introText.rectTransform.localPosition =
        originalPos;

    // Apaga a frase
    introText.text = "";
}
}
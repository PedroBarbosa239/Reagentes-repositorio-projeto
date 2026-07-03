using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDemoDoor : MonoBehaviour
{
    private bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (finished)
            return;

        finished = true;

        StartCoroutine(FinishRoutine());
    }

    private IEnumerator FinishRoutine()
    {
        yield return StartCoroutine(
            DeathFade.Instance.FadeOut(0.5f)
        );

        SceneManager.LoadScene("EndDemo");
    }
}
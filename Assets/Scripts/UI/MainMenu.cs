using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SaveSystem.DeleteSave();

        SceneManager.LoadScene("SampleScene");
    }

  public void ContinueGame()
{
    GameLoader.LoadSaveOnStart = true;

    SceneManager.LoadScene(
        "SampleScene"
    );
}

    public void QuitGame()
    {
        Application.Quit();
    }
}
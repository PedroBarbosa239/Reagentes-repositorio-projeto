using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SaveSystem.DeleteSave();
        GameLoader.ShowTutorial = true;
        SceneManager.LoadScene("SampleScene");
    }

  public void ContinueGame()
{
    GameLoader.LoadSaveOnStart = true;
GameLoader.ShowTutorial = false;
    SceneManager.LoadScene(
        "SampleScene"
    );
}

    public void QuitGame()
    {
        Application.Quit();
    }
}
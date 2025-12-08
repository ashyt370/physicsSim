using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuiyGame()
    {
        Application.Quit();
    }
}

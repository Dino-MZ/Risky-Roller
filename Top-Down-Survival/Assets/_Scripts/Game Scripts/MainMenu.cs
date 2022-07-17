using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void toMenu()
    {
        if (Pause.isPaused)
        {
            Pause.isPaused = false;
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(0);
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

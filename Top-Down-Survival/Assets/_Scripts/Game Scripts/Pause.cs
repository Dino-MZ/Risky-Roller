using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    #region Variables

    public GameObject pauseMenu;

    public static bool isPaused = false;

    public bool inMenu;

    #endregion


    #region MonoBehaviors

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) && !inMenu)
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
        if (!isPaused && !inMenu)
        {
            resumeGame();
        }

    }

    #endregion


    #region Method

    public void pauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        if (Pause.isPaused)
        {
            Pause.isPaused = false;
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(0);
    }


    #endregion
}

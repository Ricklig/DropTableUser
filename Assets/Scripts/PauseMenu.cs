using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject PauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        else if (Time.timeScale == 1f)
            Resume();
    }

    public void Resume()
    {
        isPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void Options()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

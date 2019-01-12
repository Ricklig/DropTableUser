using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject PauseMenuUI;
    public GUISkin guiSkin;

    int screenWidth, screenHeight;
    int btnWidth, btnHeight;
    float posX, posY;

    void Start()
    {
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
                Resume();
            else
                Pause();
        }
    }

    private void OnGUI()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        int btnWidth = screenWidth / 3;
        int btnHeight = screenHeight / 10;

        int posX = screenWidth / 2 - btnWidth / 2;
        float posY = (3f / 10f) * screenHeight;

        GUI.skin = guiSkin;
      
        if (PauseMenuUI.activeSelf)
        {
            if (GUI.Button(new Rect(posX, posY + btnHeight * 1, btnWidth, btnHeight), "CONTINUER"))
                Resume();
            if (GUI.Button(new Rect(posX, posY + btnHeight * 2, btnWidth, btnHeight), "MENU PRINCIPAL"))
                Options();
            if (GUI.Button(new Rect(posX, posY + btnHeight * 3, btnWidth, btnHeight), "QUITTER"))
                Quit();
        }
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

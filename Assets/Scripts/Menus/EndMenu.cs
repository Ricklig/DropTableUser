using UnityEngine;
using TMPro;
using System;

public class EndMenu : MonoBehaviour
{
    public GUISkin _GuiSkin;
    public GameManager gameManager;

    // Should put the proper type but I'm brain-dead
    public GameObject timeText;
    public GameObject quantityText;
    public GameObject valueText;    

    int _IndexResolution;
    int _ScreenWidth, _ScreenHeight;
    int _BtnWidth, _BtnHeight;
    float _PosX, _PosY;

    // Everything that happens every time the menu scene is created
    private void Start()
    {
        timeText.GetComponent<TextMeshPro>().text = "En " + ConvertTime(gameManager.timeElapsed);
        if (gameManager.NumberOfStolenArtefacts > 1)
            timeText.GetComponent<TextMeshPro>().text = "Vous avez ramassé " + gameManager.NumberOfStolenArtefacts + " objets";
        else
            timeText.GetComponent<TextMeshPro>().text = "Vous avez ramassé " + gameManager.NumberOfStolenArtefacts + " objet";
        timeText.GetComponent<TextMeshPro>().text = "Pour un total de €" + gameManager.stolen;
    }

    private void Update()
    {
        CalculateButtonMetrics();
    }

    private string ConvertTime(double time)
    {
        string timeString = "";
        int minutes = (int)Math.Floor(time / 60);
        timeString += minutes.ToString() + " minute";

        // Multiple or one minute, cuz we want perfect french!
        if (minutes > 1)
            timeString += "s ";
        else
            timeString += " ";

        int seconds = (int)(time % 60);
        timeString += seconds + " secondes";

        return timeString;
    }

    // Quits the game, need to actually build the game though, it does not work on the editor
    public void QuitGame()
    {
        Application.Quit();
    }

    // Every single button generated dynamicly on the main menu
    private void OnGUI()
    {
        GUI.skin = _GuiSkin;

        if (GUI.Button(new Rect(_PosX, _PosY + _BtnHeight * 4, _BtnWidth, _BtnHeight), "Quitter"))
            QuitGame();
    }

    // Not only calculating these on start since the user can resize the window
    void CalculateButtonMetrics()
    {
        _ScreenWidth = Screen.width;
        _ScreenHeight = Screen.height;
        _BtnWidth = _ScreenWidth / 3;
        _BtnHeight = _ScreenHeight / 10;

        _PosX = _ScreenWidth / 2 - _BtnWidth / 2;
        _PosY = (3f / 10f) * _ScreenHeight;
    }
}

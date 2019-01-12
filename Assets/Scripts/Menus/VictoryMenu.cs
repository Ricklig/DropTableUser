using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    public GUISkin _GuiSkin;

    int _IndexResolution;
    int _ScreenWidth, _ScreenHeight;
    int _BtnWidth, _BtnHeight;
    float _PosX, _PosY;

    // Everything that happens every time the menu scene is created
    private void Start()
    {

    }

    private void Update()
    {
        CalculateButtonMetrics();
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

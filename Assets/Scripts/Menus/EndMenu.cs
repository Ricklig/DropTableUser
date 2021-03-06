﻿using UnityEngine;
using TMPro;
using System;

public class EndMenu : MonoBehaviour
{
    const float END_DELAY = 2f;

    public GUISkin _GuiSkin;
    private GameManager gameManager;

    public GameObject title;
    public GameObject timeText;
    public GameObject quantityText;
    public GameObject valueText;
    public GameObject pressToReturn;

    public AudioSource victoryMusic;
    public AudioSource defeatMusic;

    int _IndexResolution;
    int _ScreenWidth, _ScreenHeight;
    int _BtnWidth, _BtnHeight;
    float _PosX, _PosY;
    float outlineValue;
    float endTimer;
    bool upstream;

    // Everything that happens every time the menu scene is created
    void Start()
    {
        // Retrieve the gameObject and read the results
        gameManager = FindObjectOfType<GameManager>();

        Debug.Log("Victory: " + gameManager.isVictory);

        if (gameManager.isVictory)
            PrintVictory();
        else
            PrintDefeat();
        endTimer = 0f;
    }

    private void PrintVictory()
    {
        victoryMusic.Play();

        title.GetComponent<TextMeshProUGUI>().text = "FELICITATIONS!";
        timeText.GetComponent<TextMeshProUGUI>().text = "En " + ConvertTime(gameManager.timeElapsed);
        if (gameManager.NumberOfStolenArtefacts > 1)
            quantityText.GetComponent<TextMeshProUGUI>().text = "Vous avez ramassé " + gameManager.NumberOfStolenArtefacts + " objets";
        else
            quantityText.GetComponent<TextMeshProUGUI>().text = "Vous avez ramassé " + gameManager.NumberOfStolenArtefacts + " objet";
        valueText.GetComponent<TextMeshProUGUI>().text = "Pour un total de €" + gameManager.stolen;
    }

    private void PrintDefeat()
    {
        defeatMusic.Play();

        title.GetComponent<TextMeshProUGUI>().text = "OH NON!";
        timeText.GetComponent<TextMeshProUGUI>().text = "Après " + ConvertTime(gameManager.timeElapsed) + " vous vous êtes\nfait attrapé!";
        if (gameManager.NumberOfStolenArtefacts > 1)
            quantityText.GetComponent<TextMeshProUGUI>().text = "Vous aviez avec vous " + gameManager.NumberOfStolenArtefacts + " objets";
        else
            quantityText.GetComponent<TextMeshProUGUI>().text = "Vous aviez avec vous " + gameManager.NumberOfStolenArtefacts + " objet";
        valueText.GetComponent<TextMeshProUGUI>().text = "Pour un total de €" + gameManager.stolen;
    }

    private void Update()
    {
        CalculateButtonMetrics();
        if (endTimer > END_DELAY && Input.anyKey)
			Application.Quit();
		UpdateGlow();
        endTimer += Time.deltaTime;
    }

    private void UpdateGlow()
    {
        if (upstream)
            outlineValue += 0.004f;
        else
            outlineValue -= 0.004f;

        if (outlineValue <= 0.00f)
            upstream = true;
        else if (outlineValue >= 0.32f)
            upstream = false;
        
        pressToReturn.GetComponent<TextMeshProUGUI>().outlineWidth = outlineValue;
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

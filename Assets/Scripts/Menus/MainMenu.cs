﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
    const float DEFAULT_VOL = 0f;
    const float MIN_VOL = -80f;
    const float MAX_VOL = 20f;

    public AudioMixer effectsMixer;
    public AudioMixer musicMixer;
    public AudioSource onClickSound;
    public Dropdown qualityDd, resolutionDd;
    public Toggle fullscreen;
    public GameObject videoOptions;
    public GameObject title;
    public GUISkin guiSkin;
    public Image controls;
    public float fXVolume;
    public float musicVolume;

    int indexResolution;
    int screenWidth, screenHeight;
    int btnWidth, btnHeight;
    float posX, posY;

    Resolution[] resolutions;

    enum Menu { main, play, options, controls, audio, video }
    [SerializeField] Menu currentMenu = Menu.main;

    // Use this for initialization
    void Start ()
    {
        fXVolume = DEFAULT_VOL;
        musicVolume = DEFAULT_VOL;

        resolutions = Screen.resolutions;
        resolutionDd.ClearOptions();
        List<string> choices = new List<string>();
        
        // Source : Tutorial at https://www.youtube.com/watch?v=YOaYQrN1oYQ
        for (int i = 0; i < resolutions.Length; i++)
        {
            string widthByHeight = resolutions[i].width + " x " + resolutions[i].height;
            choices.Add(widthByHeight);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                indexResolution = i;
        }

        CalculateButtonMetrics();
        resolutionDd.AddOptions(choices);
        resolutionDd.value = indexResolution;
        resolutionDd.RefreshShownValue();
    }

    private void Test()
    {
        Debug.Log("Esketit");
    }

    // Calculating the sizes at infinitum in case of resizes
    private void Update()
    {
        CalculateButtonMetrics();
    }

    // Starts a new game, the game scene must be the next one in the build settings
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quits the game, need to actually build the game though, it does not work on the editor
    public void QuitGame()
    {
        Application.Quit();
    }

    private void ClickSound()
    {
        onClickSound.Play();
    }

    // Every single button generated dynamicly on the main menu
    private void OnGUI()
    {
        GUI.skin = guiSkin;

        if (currentMenu == Menu.main)
            title.SetActive(true);
        else
            title.SetActive(false);

        if (currentMenu == Menu.main)
        {
            if (GUI.Button(new Rect(posX, posY + btnHeight * 2, btnWidth, btnHeight), "JOUER"))
            {
                
                StartGame();
                currentMenu = Menu.play;
                ClickSound();
            }
            if (GUI.Button(new Rect(posX, posY + btnHeight * 3, btnWidth, btnHeight), "OPTIONS"))
            {
                currentMenu = Menu.options;
                ClickSound();
            }
            if (GUI.Button(new Rect(posX, posY + btnHeight * 4, btnWidth, btnHeight), "QUITTER"))
            {
                QuitGame();
                ClickSound();
            }
        }
        else if (currentMenu == Menu.options)
        {
            if (GUI.Button(new Rect(posX, posY + btnHeight * 1, btnWidth, btnHeight), "CONTROLES"))
            {
                currentMenu = Menu.controls;
                ClickSound();
            }
            if (GUI.Button(new Rect(posX, posY + btnHeight * 2, btnWidth, btnHeight), "AUDIO"))
            { 
                currentMenu = Menu.audio;
                ClickSound();
            }
            if (GUI.Button(new Rect(posX, posY + btnHeight * 3, btnWidth, btnHeight), "VIDEO"))
            {
                currentMenu = Menu.video;
                ClickSound();
            }
            if (GUI.Button(new Rect(posX, posY + btnHeight * 5, btnWidth, btnHeight), "RETOUR"))
            {
                currentMenu = Menu.main;
                ClickSound();
            }
        }
        else if (currentMenu == Menu.controls)
        {
            controls.gameObject.SetActive(true);
            if (GUI.Button(new Rect(posX, posY + btnHeight * 6, btnWidth, btnHeight), "RETOUR"))
            {
                currentMenu = Menu.options;
                ClickSound();
            }
        }
        else if (currentMenu == Menu.audio)
        {
            if (GUI.Button(new Rect(posX, posY + btnHeight * 1, btnWidth, btnHeight), "VOLUME EFFETS"))
            { 
                fXVolume = DEFAULT_VOL;
                ClickSound();
            }    
                EffectsVolumeSlider();

            if (GUI.Button(new Rect(posX, posY + btnHeight * 3, btnWidth, btnHeight), "VOLUME MUSIQUE"))
            { 
               musicVolume = DEFAULT_VOL;
                ClickSound();
            }
            EffectsMusicSlider();

            if (GUI.Button(new Rect(posX, posY + btnHeight * 5, btnWidth, btnHeight), "RETOUR"))
            { 
                currentMenu = Menu.options;
                ClickSound();
            }
        }
        else if (currentMenu == Menu.video)
        {
            SetVideoOptions();
            if (GUI.Button(new Rect(posX, posY + btnHeight * 5, btnWidth, btnHeight), "RETOUR"))
            {
                currentMenu = Menu.options;
                ClickSound();
            }
        }

        if (currentMenu != Menu.video)
            videoOptions.gameObject.SetActive(false);
        if (currentMenu != Menu.controls)
            controls.gameObject.SetActive(false);
    }

    // The lower the index, the worst the quality is, e.g. no shadows rendered
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Toggles fullscreen and windowed mode, not feeling like adding windowed fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
    }

    // Sets the resolution depending on the index [0, 5], 0 being the worst
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Retrieving actual value to print the right slider, reading the new value at each frame to update the slider
    void EffectsVolumeSlider()
    {
        effectsMixer.GetFloat("Volume", out fXVolume);
        fXVolume = GUI.HorizontalSlider(new Rect(posX, posY + btnHeight * 2, btnWidth, btnHeight), fXVolume, MIN_VOL, MAX_VOL);
        effectsMixer.SetFloat("Volume", fXVolume);
    }

    // Retrieving actual value to print the right slider, reading the new value at each frame to update the slider
    void EffectsMusicSlider()
    {
        musicMixer.GetFloat("Volume", out musicVolume);
        musicVolume = GUI.HorizontalSlider(new Rect(posX, posY + btnHeight * 4, btnWidth, btnHeight), musicVolume, MIN_VOL, MAX_VOL);
        musicMixer.SetFloat("Volume", musicVolume);
    }

    // How big should the things be ( ͡° ͜ʖ ͡°)
    void CalculateButtonMetrics() 
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        btnWidth = screenWidth / 3;
        btnHeight = screenHeight / 10;

        posX = screenWidth / 2 - btnWidth / 2;
        posY = (3f / 10f) * screenHeight;
    }

    // All of the video options
    void SetVideoOptions()
    {
        videoOptions.gameObject.SetActive(true);
        qualityDd.value = QualitySettings.GetQualityLevel();
        fullscreen.isOn = (Screen.fullScreen) ? true : false;
    }
}

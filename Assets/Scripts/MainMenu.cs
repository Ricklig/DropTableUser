using UnityEngine;
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
    public Dropdown qualityDd, resolutionDd;
    public Toggle fullscreen;
    public GameObject videoOptions;
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
            string widthByHeight = resolutions[i].width + " par " + resolutions[i].height;
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

    // Every single button generated dynamicly on the main menu
    private void OnGUI()
    {
        GUI.skin = guiSkin;

        if (currentMenu == Menu.main)
        {
            if (GUI.Button(new Rect(posX, posY + btnHeight * 2, btnWidth, btnHeight), "Jouer"))
            {
                StartGame();
                currentMenu = Menu.play;
            }
            if (GUI.Button(new Rect(posX, posY + btnHeight * 3, btnWidth, btnHeight), "Options"))
                currentMenu = Menu.options;
            if (GUI.Button(new Rect(posX, posY + btnHeight * 4, btnWidth, btnHeight), "Quitter"))
                QuitGame();
        }
        else if (currentMenu == Menu.options)
        {
            if (GUI.Button(new Rect(posX, posY + btnHeight * 1, btnWidth, btnHeight), "Controles"))
                currentMenu = Menu.controls;
            if (GUI.Button(new Rect(posX, posY + btnHeight * 2, btnWidth, btnHeight), "Audio"))
                currentMenu = Menu.audio;
            if (GUI.Button(new Rect(posX, posY + btnHeight * 3, btnWidth, btnHeight), "Video"))
                currentMenu = Menu.video;
            if (GUI.Button(new Rect(posX, posY + btnHeight * 5, btnWidth, btnHeight), "Retour"))
                currentMenu = Menu.main;
        }
        else if (currentMenu == Menu.controls)
        {
            controls.gameObject.SetActive(true);
            if (GUI.Button(new Rect(posX, posY + btnHeight * 6, btnWidth, btnHeight), "Retour"))
                currentMenu = Menu.options;
        }
        else if (currentMenu == Menu.audio)
        {
            if (GUI.Button(new Rect(posX, posY + btnHeight * 1, btnWidth, btnHeight), "Volume effets"))
                fXVolume = DEFAULT_VOL;
            EffectsVolumeSlider();

            if (GUI.Button(new Rect(posX, posY + btnHeight * 3, btnWidth, btnHeight), "Volume musique"))
               musicVolume = DEFAULT_VOL;
            EffectsMusicSlider();

            if (GUI.Button(new Rect(posX, posY + btnHeight * 5, btnWidth, btnHeight), "Retour"))
                currentMenu = Menu.options;
        }
        else if (currentMenu == Menu.video)
        {
            SetVideoOptions();
            if (GUI.Button(new Rect(posX, posY + btnHeight * 5, btnWidth, btnHeight), "Retour"))
                currentMenu = Menu.options;
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

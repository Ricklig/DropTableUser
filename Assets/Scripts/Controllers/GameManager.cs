using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager> {
    public AlertManager AlertManager = new AlertManager();

    public MusicController mc;
    public bool isPlaying = false;

    public double stolen = 0;
    public double timeElapsed = 0;
    public int NumberOfStolenArtefacts = 0;

    public Text amountStolen;
    public Text valueStolen;
    public Text timer;

    private int seenStolenArtefacts = 0;

    public void IncrementSeenStolenItem()
    {
        seenStolenArtefacts++;
        AlertManager.UpdateAlertStatus(seenStolenArtefacts);
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        if (isPlaying)
        {
            timeElapsed += Time.deltaTime;
            timer.text = ConvertTime(timeElapsed);
        }
    }

    // To know wether we should increment the timer or not
    private void CheckStatus()
    {
        if ((SceneManager.GetActiveScene().name == "gym_map" && Time.timeScale == 1f) ||
            (SceneManager.GetActiveScene().name == "gym_UI" && Time.timeScale == 1f))
            isPlaying = true;
        else
            isPlaying = false;
    }

    private string ConvertTime(double time)
    {
        string timeString = "";
        int minutes = (int)Math.Floor(time / 60);
        timeString += minutes.ToString() + "m ";
        int seconds = (int)(time % 60);
        timeString += seconds + "s";

        return timeString;
    }

    public void addValue(double val)
    {
        mc.levelUp();
        stolen += val;
        valueStolen.text = stolen.ToString() + "€";
        NumberOfStolenArtefacts++;
        amountStolen.text = NumberOfStolenArtefacts.ToString();
    }

    public void escape()
    {
        SceneManager.LoadScene("Menu");
    }

    public double getValue()
    {
        return stolen;
    }

    public int getQuantity()
    {
        return NumberOfStolenArtefacts;
    }

    public void KillPlayer()
    {
        Debug.Log("Your Dead");
    }
}

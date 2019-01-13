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
    public bool isVictory;

    public double stolen = 0;
    public double timeElapsed = 0;
    public int NumberOfStolenArtefacts = 0;

    public Text amountStolen;
    public Text valueStolen;
    public Text timer;
    public Text alert;

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
        AlertManager.alertStatusChanged += UpgradeAlert;
    }

    private void UpgradeAlert(AlertStatus alertStatus)
    {
        if (alertStatus.Equals(AlertLevel.None))
        {
            alert.text = "Bas";
            alert.color = Color.white;
        }
        else if (alertStatus.Equals(AlertLevel.MinorAlert))
        {
            alert.text = "Moyen";
            alert.color = Color.green;
        }
        else if (alertStatus.Equals(AlertLevel.SevereAlert))
        {
            alert.text = "Haut";
            alert.color = Color.yellow;
        }
        else if (alertStatus.Equals(AlertLevel.SevereAlert))
        {
            alert.text = "MAXIMAL!";
            alert.color = Color.red;
        }
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
        
        stolen += val;
        valueStolen.text = "€" + stolen.ToString();
        NumberOfStolenArtefacts++;
        amountStolen.text = NumberOfStolenArtefacts.ToString();
    }

    public void escape()
    {
        isVictory = true;
        SceneManager.LoadScene("EndScreen");
    }

    public void getCaught()
    {
        SceneManager.LoadScene("EndScreen");
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
        isVictory = false;
        Debug.Log("Omae Wa Mou Shindeiru");
        getCaught();
    }
    public void levelUp()
    {
        mc.levelUp();
    }
}

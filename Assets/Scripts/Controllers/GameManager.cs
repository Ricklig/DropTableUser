﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : Singleton<GameManager> {
    public AlertManager AlertManager = new AlertManager();

    public MusicController mc;
    public bool isPlaying = false;
    public bool isVictory;

    public double stolen = 0;
    public double timeElapsed = 0;
    public int NumberOfStolenArtefacts = 0;

    public GameObject amountStolen;
    public GameObject valueStolen;
    public GameObject timer;
    public GameObject alert;

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
        TextMeshProUGUI tmp = null;
        try
        {
            tmp = alert.GetComponent<TextMeshProUGUI>();
        }
        catch (MissingReferenceException) { }

        if (tmp != null)
        { 
            if (alertStatus.Equals(AlertStatus.level1))
            {
                tmp.text = "Bas";
                tmp.color = Color.white;
            }
            else if (alertStatus.Equals(AlertStatus.level2))
            {
                tmp.text = "Moyen";
                tmp.color = Color.green;
            }
            else if (alertStatus.Equals(AlertStatus.level3))
            {
                tmp.text = "Haut";
                tmp.color = Color.yellow;
            }
            else if (alertStatus.Equals(AlertStatus.RedAlert))
            {
                tmp.text = "MAXIMAL!";
                tmp.color = Color.red;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI tmp = null;
        try
        {
            tmp = timer.GetComponent<TextMeshProUGUI>();
        }
        catch (MissingReferenceException) { }

        CheckStatus();
        if (isPlaying && tmp)
        {
            timeElapsed += Time.deltaTime;
            tmp.text = ConvertTime(timeElapsed);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AlertManager.TriggerRedAlert();
        }
    }

    // To know wether we should increment the timer or not
    private void CheckStatus()
    {
        if ((SceneManager.GetActiveScene().name == "gym_map" && Time.timeScale == 1f) ||
            (SceneManager.GetActiveScene().name == "gym_UI" && Time.timeScale == 1f)  ||
            (SceneManager.GetActiveScene().name == "gym_VF" && Time.timeScale == 1f))
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

        TextMeshProUGUI value = null;
        TextMeshProUGUI qty = null;
        try
        {
            value = valueStolen.GetComponent<TextMeshProUGUI>();
            qty = amountStolen.GetComponent<TextMeshProUGUI>();
			
        }
        catch (MissingReferenceException) { }

        if (qty != null && value != null)
        {
            stolen += val;
            value.text = "€" + stolen.ToString();
            NumberOfStolenArtefacts++;
            qty.text = NumberOfStolenArtefacts.ToString();
        }
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

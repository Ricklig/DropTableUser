using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AlertStatus { level1 = 1, level2 = 2, level3 = 3, RedAlert = 4 }
public class AlertManager{
    public int AlertLevel2Ceil = 1;
    public int AlertLevel3Ceil = 2;
    public int RedAlertCeil = 3;
    public AlertStatus currentAlertStatus = AlertStatus.level1;
    public delegate void AlertStatusChanged(AlertStatus alertStatus);
    public event AlertStatusChanged alertStatusChanged;
    private GameManager mc;

    void OnAwake()
    {
       mc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Start()
    {
    }

    public void UpdateAlertStatus(int seenStolenItem)
    {
        if (RedAlertCeil <= seenStolenItem && currentAlertStatus != AlertStatus.RedAlert)
        {
            alertStatusChanged(AlertStatus.RedAlert);
            mc.levelUp();
            currentAlertStatus = AlertStatus.RedAlert;
        }
        else if (AlertLevel3Ceil <= seenStolenItem && currentAlertStatus != AlertStatus.level3)
        {
            alertStatusChanged(AlertStatus.level3);
            mc.levelUp();
            currentAlertStatus = AlertStatus.level3;
        }
        else if (AlertLevel2Ceil <= seenStolenItem && currentAlertStatus != AlertStatus.level2)
        {
            currentAlertStatus = AlertStatus.level2;
            alertStatusChanged(AlertStatus.level2);
            mc.levelUp();
        }
    }
}

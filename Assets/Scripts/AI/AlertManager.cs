using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AlertStatus { level1 = 1, level2 = 2, level3 = 3, RedAlert = 4 }
public class AlertManager{
    public int AlertLevel2Ceil = 5;
    public int AlertLevel3Ceil = 10;
    public int RedAlertCeil = 15;
    public AlertStatus currentAlertStatus = AlertStatus.level1;
    public delegate void AlertStatusChanged(AlertStatus alertStatus);
    public event AlertStatusChanged alertStatusChanged;
    public void TriggerRedAlert()
    {
        alertStatusChanged(AlertStatus.RedAlert);
        currentAlertStatus = AlertStatus.RedAlert;
        GameManager.Instance.levelUp();
        GameManager.Instance.levelUp();
        GameManager.Instance.levelUp();
    }
    public void UpdateAlertStatus(int seenStolenItem)
    {
        if (RedAlertCeil <= seenStolenItem)
        {
            if (currentAlertStatus != AlertStatus.RedAlert)
            {
                alertStatusChanged(AlertStatus.RedAlert);
                currentAlertStatus = AlertStatus.RedAlert;
                GameManager.Instance.levelUp();
            }
        }
        else if (AlertLevel3Ceil <= seenStolenItem)
        {
            if (currentAlertStatus != AlertStatus.level3)
            {
                alertStatusChanged(AlertStatus.level3);
                currentAlertStatus = AlertStatus.level3;
                GameManager.Instance.levelUp();
            }

        }
        else if (AlertLevel2Ceil <= seenStolenItem)
        {
            if(currentAlertStatus != AlertStatus.level2)
            {
                currentAlertStatus = AlertStatus.level2;
                alertStatusChanged(AlertStatus.level2);
                GameManager.Instance.levelUp();
            }
        }
    }
}

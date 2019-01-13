﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AlertStatus { level1 = 1, level2 = 2, level3 = 3, RedAlert = 4 }
public class AlertManager{
    public int AlertLevel2Ceil = 2;
    public int AlertLevel3Ceil = 5;
    public int RedAlertCeil = 8;
    public AlertStatus currentAlertStatus = AlertStatus.level1;
    public delegate void AlertStatusChanged(AlertStatus alertStatus);
    public event AlertStatusChanged alertStatusChanged;
    public void UpdateAlertStatus(int seenStolenItem)
    {
        if (RedAlertCeil <= seenStolenItem && currentAlertStatus != AlertStatus.RedAlert)
        {
            alertStatusChanged(AlertStatus.RedAlert);
        }
        else if (AlertLevel3Ceil <= seenStolenItem && currentAlertStatus != AlertStatus.level3)
        {
            alertStatusChanged(AlertStatus.level3);
        }
        else if (AlertLevel2Ceil <= seenStolenItem && currentAlertStatus != AlertStatus.level2)
        {
            alertStatusChanged(AlertStatus.level2);
        }
    }
}

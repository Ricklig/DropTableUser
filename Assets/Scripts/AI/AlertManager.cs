using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AlertStatus { None, MinorAlert, Alert, SevereAlert }
public class AlertManager{
    public AlertStatus currentAlertStatus = AlertStatus.None;
    public void UpdateAlertStatus(int seenStolenItem)
    {

    }
}

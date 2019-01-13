using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class RedLightScript : MonoBehaviour
{
	private Light[] lights;

	private bool activeLights;
	// Use this for initialization
	void Start () {
		GameManager.Instance.AlertManager.alertStatusChanged += AlertManagerOnAlertStatusChanged;
		lights = gameObject.GetComponentsInChildren<Light>(true);
		activeLights = false;
		foreach (var light in lights)
		{
			light.enabled = false;
		}
	}

	private void AlertManagerOnAlertStatusChanged(AlertStatus alertstatus)
	{
		if (alertstatus == AlertStatus.RedAlert)
		{
			activeLights = true;
			foreach (var light in lights)
			{
				light.enabled = true;
			}
		}
	}



	// Update is called once per frame
	void Update () {
		if (activeLights)
		{
			foreach (var light in lights)
			{
				light.transform.Rotate(Vector3.right, 3);
			}
			
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSightConeColor : MonoBehaviour {
    public Material AlertMaterial;
    public Material SearchingMaterial;
    public Material PatrolMaterial;
    private MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
	}
	public void SetAlarmMaterial()
    {
        meshRenderer.material = AlertMaterial;
    }
    public void SetSearchingMaterial()
    {
        meshRenderer.material = SearchingMaterial;
    }

    public void SetPatrolMaterial()
    {
        meshRenderer.material = PatrolMaterial;
    }
	// Update is called once per frame
	void Update () {
		
	}
}

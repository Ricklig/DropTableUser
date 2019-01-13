using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour {

    int rotateRate = 20;

	// Use this for initialization
	void Start () {
        rotateRate = Random.Range(15, 40);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Time.deltaTime * rotateRate, 0);
	}
}

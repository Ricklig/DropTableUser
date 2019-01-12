using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private double stolen = 0;
    private int quantity = 0;

    public Text amountStolen;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addValue(double val)
    {
        stolen += val;
        amountStolen.text = stolen.ToString();
        quantity++;

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
        return quantity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public AlertManager AlertManager;
    private double stolen = 0;
    private int NumberOfStolenArtefacts = 0;

    public Text amountStolen;

    private int seenStolenArtefacts = 0;

    public void IncrementSeenStolenItem()
    {
        seenStolenArtefacts++;
        AlertManager.UpdateAlertStatus(seenStolenArtefacts);
    }
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
        NumberOfStolenArtefacts++;

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
        return NumberOfStolenArtefacts;
    }
}

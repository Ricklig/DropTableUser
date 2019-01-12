using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public AlertManager AlertManager;
    public bool isPlaying = false;

    public double stolen = 0;
    public double timeElapsed = 0;
    public int NumberOfStolenArtefacts = 0;

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
        timeElapsed = 0;
}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
            timeElapsed += Time.deltaTime;
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

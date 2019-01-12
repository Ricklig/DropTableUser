using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public AudioSource level1;
    public AudioSource level2;
    public AudioSource level3;

    private int currentLevel = 1;

    public void levelUp()
    {
        if (currentLevel == 1)
        {
            currentLevel++;
            level2.mute = false;
        }
        else if (currentLevel == 2)
        {
            currentLevel++;
            level3.mute = false;
        }
    }

    public void levelDown()
    {
        if (currentLevel == 2)
        {
            currentLevel--;
            level2.mute = true;
        }
        else if (currentLevel == 3)
        {
            currentLevel--;
            level3.mute = true;
        }
    }
}

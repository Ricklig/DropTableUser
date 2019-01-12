using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioSource level1;
    public AudioSource level2;
    public AudioSource level3;
    public AudioSource level4;

    private int currentLevel = 1;

    public void levelUp()
    {
        StartCoroutine(Level_up());
    }

    IEnumerator Level_up()
    {
        if (currentLevel == 3)
        {
            currentLevel++;
            float startVolume3 = 1;
            level4.Play();
            while (level3.volume > 0)
            {
                level3.volume -= startVolume3 * Time.deltaTime;
                level4.volume += startVolume3 * Time.deltaTime;
                yield return null;
            }
            level3.Stop();


        }
        else if (currentLevel == 2)
        {
            currentLevel++;
            float startVolume2 = level2.volume;
            while (level3.volume < 1)
            {
                level3.volume += startVolume2 * Time.deltaTime;
                level2.volume -= startVolume2 * Time.deltaTime;
                yield return null;
            }
            level2.Stop();
        }
        else if (currentLevel == 1)
        {
            currentLevel++;
            float startVolume1 = level1.volume;
            while (level2.volume < 1)
            {
                level2.volume += startVolume1 * Time.deltaTime;
                level1.volume -= startVolume1 * Time.deltaTime;
                yield return null;
            }
            level1.Stop();
        }
    }
}

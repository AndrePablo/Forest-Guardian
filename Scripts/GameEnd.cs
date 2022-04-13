using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public GameObject victoryScreen;
    public HealthScript bossHealth;
    private float waitTime = 1.0f;

    void Update()
    {
        //check end menu
        if (bossHealth.hp <= 0)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                victoryScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}

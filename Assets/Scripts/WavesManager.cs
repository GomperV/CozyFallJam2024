using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WavesManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text waveText, waveSkipTip;
    private EnemySpawner[] enemySpawners;
    public int waveNumber = 0;
    private float enemySpawnRate;
    private int activeSpawners, spawnersToKill, waitTime;
    private GameObject player;
    private PlayerExperience playerExp;
    // Start is called before the first frame update
    void Start()
    {
        activeSpawners = 0;
        waveNumber = 0;
        enemySpawnRate = 5f; //how often enemies spawn initially
        enemySpawners = FindObjectsOfType<EnemySpawner>();
        player = GameObject.Find("Player");
        playerExp = player.GetComponent<PlayerExperience>();
        print("EnemySpawners amount: " + enemySpawners.Length);
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) && waitTime > 3)
        {
            waitTime = 3;
        }
    }

    private void StartWave()
    {
        waveNumber++;
        
        waveText.text = "WAVE " + waveNumber;
        if(enemySpawnRate > 0.5f) enemySpawnRate -= ((float)waveNumber / 10); //enemies spawn faster witch each wave

        //some funny logic calculating how many spawners are active based on wave number (FOR NOW, might wanna set this manually)
        if (waveNumber > 2) activeSpawners = Mathf.RoundToInt(0.6f + waveNumber / 2f);
        else activeSpawners = waveNumber;
        spawnersToKill = activeSpawners;
        print(activeSpawners);

        print("Enemies will spawn every " + enemySpawnRate + " s.");
        foreach(EnemySpawner spawner in enemySpawners)
        {
            //might wanna make active spawners random
            if(activeSpawners > 0)
            {
                spawner.SpawnEnemies(enemySpawnRate);
                activeSpawners--;
            }
        }
    }

    //called by every nest when their hp < 1
    public void SpawnerDestroyed()
    {
        spawnersToKill--;
        if(spawnersToKill < 1)
        {
            if (waveNumber > 0) playerExp.GainExperience(30 + waveNumber * 10, true);
            StartCoroutine(DelayBetweenWaves());
        }
    }

    IEnumerator DelayBetweenWaves()
    {
        //delay slightly longer with each wave (for preparations)

        waitTime = 19 + waveNumber;
        while(waitTime > 0)
        {
            yield return new WaitForSeconds(1);
            waitTime--;
            waveText.text = "Wave " + (waveNumber + 1) + " in " + waitTime + "s";
            if (waitTime < 3)
            {
                waveSkipTip.text = "";
            } else
            {
                waveSkipTip.text = "Press 's' to skip";
            }
            
        }
        waveSkipTip.text = "Destroy all active Ice crystals!";
        StartWave();
        

    }
}

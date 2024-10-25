using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WavesManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text waveText;
    private EnemySpawner[] enemySpawners;
    public int waveNumber = 0;
    private float enemySpawnRate;
    private int activeSpawners;
    // Start is called before the first frame update
    void Start()
    {
        activeSpawners = 0;
        waveNumber = 0;
        enemySpawnRate = 2f; //how often enemies spawn initially
        enemySpawners = FindObjectsOfType<EnemySpawner>();
        print("EnemySpawners amount: " + enemySpawners.Length);
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartWave()
    {
        waveNumber++;
        waveText.text = "WAVE " + waveNumber;
        enemySpawnRate = enemySpawnRate - waveNumber / 15; //enemies spawn faster witch each wave

        //some funny logic calculating how many spawners are active based on wave number (FOR NOW, might wanna set this manually)
        if (waveNumber > 2) activeSpawners = Mathf.RoundToInt(0.5f + waveNumber / 2f);
        else activeSpawners = waveNumber;

        print(activeSpawners);

        print("Enemies will spawn every " + enemySpawnRate + " s.");
        foreach(EnemySpawner spawner in enemySpawners)
        {
            //might wanna make active spawners random
            while(activeSpawners > 0)
            {
                spawner.SpawnEnemies(enemySpawnRate);
                activeSpawners--;
            }
            
        }
    }
}

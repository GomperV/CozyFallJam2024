using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool isSpawning = false;
    public float timeBetweenSpawns = 1f;
    private int spawnedEnemies = 0;
    private bool spawnedDefender = false;
    [SerializeField]
    private GameObject enemyBeetle, enemyDefender, enemyGuardian, patrolPointA, patrolPointB;
    private int randomChance = 0;
    private WavesManager wavesManager;
    private float buffIncrease = 0;
    private void Start()
    {
        wavesManager = GameObject.Find("WavesManager").GetComponent<WavesManager>();
    }
    private void Update()
    {

    }
    public void SpawnEnemies(float spawnRate)
    {
        buffIncrease = 0;
        //nest will spawn a new defender after each wave
        spawnedDefender = false;
        GetComponent<EnemyNestHealth>().health = 300f;
        GetComponent<EnemyNestHealth>().originalHealth = GetComponent<EnemyNestHealth>().health;
        GetComponent<EnemyNestHealth>().nestDestroyed = false;
        isSpawning = true;
        timeBetweenSpawns = spawnRate;
        StartCoroutine(SpawnWithDelay());
    }

    IEnumerator SpawnWithDelay()
    {
        print("Started spawning");
        while(isSpawning)
        {
            if (!isSpawning) break;
            randomChance = Random.Range(1, 5);
            //spawn enemy guardian with attacker sometimes
            GameObject attacker;
            if (randomChance == 3 && wavesManager.waveNumber > 4)
            {
                print("spawning GUARDIAN");
                attacker = Instantiate(enemyBeetle, transform.position, Quaternion.identity);
                GameObject guardian = Instantiate(enemyGuardian, transform.position, Quaternion.identity);
                guardian.GetComponent<EnemyGuardianMovement>().PunktA = attacker.GetComponent<EnemyMovement>().patrolPointA;
                guardian.GetComponent<EnemyGuardianMovement>().PunktB = attacker.GetComponent<EnemyMovement>().patrolPointB;
            } else //spawn only attacker
            {
                attacker = Instantiate(enemyBeetle, transform.position, Quaternion.identity);
            }

            //set attacker enemies health
            if (wavesManager.buffEnemies) {
                buffIncrease++;
                //enemies get +50hp with each enemy spawned
                attacker.GetComponent<EnemyCombat>().health = 50f + buffIncrease * 50; 
            } else
            {
                //normal situation
                attacker.GetComponent<EnemyCombat>().health = 50f;
            }
            

            
            if(!spawnedDefender && wavesManager.waveNumber > 2)
            {
                yield return new WaitForSeconds(timeBetweenSpawns/2);
                GameObject newDefender = Instantiate(enemyDefender, patrolPointA.transform.position, Quaternion.identity);
                //each nest sends its patrol points to the newly spawned defender
                newDefender.GetComponent<EnemyDefenderMovement>().PunktA = patrolPointA;
                newDefender.GetComponent<EnemyDefenderMovement>().PunktB = patrolPointB;
                spawnedDefender = true;
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        
    }
}

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
    public void SpawnEnemies(float spawnRate)
    {
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
            if(randomChance == 3)
            {
                print("spawning GUARDIAN");
                GameObject attacker = Instantiate(enemyBeetle, transform.position, Quaternion.identity);
                GameObject guardian = Instantiate(enemyGuardian, transform.position, Quaternion.identity);
                guardian.GetComponent<EnemyGuardianMovement>().PunktA = attacker.GetComponent<EnemyMovement>().patrolPointA;
                guardian.GetComponent<EnemyGuardianMovement>().PunktB = attacker.GetComponent<EnemyMovement>().patrolPointB;
            } else //spawn only attacker
            {
                Instantiate(enemyBeetle, transform.position, Quaternion.identity);
            }
            
            if(!spawnedDefender)
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

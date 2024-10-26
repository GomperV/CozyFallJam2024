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
    private GameObject enemyBeetle, enemyDefender, enemy3, patrolPointA, patrolPointB;

    public void SpawnEnemies(float spawnRate)
    {
        //nest will spawn a new defender after each wave
        spawnedDefender = false;
        GetComponent<EnemyNestHealth>().health = 100f;
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
            yield return new WaitForSeconds(timeBetweenSpawns);
            if (!isSpawning) break;
            Instantiate(enemyBeetle, transform.position, Quaternion.identity);
            if(!spawnedDefender)
            {
                yield return new WaitForSeconds(timeBetweenSpawns/2);
                GameObject newDefender = Instantiate(enemyDefender, patrolPointA.transform.position, Quaternion.identity);
                //each nest sends its patrol points to the newly spawned defender
                newDefender.GetComponent<EnemyDefenderMovement>().PunktA = patrolPointA;
                newDefender.GetComponent<EnemyDefenderMovement>().PunktB = patrolPointB;
                spawnedDefender = true;
            }

        }
        
    }
}

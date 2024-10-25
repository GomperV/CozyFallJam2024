using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool isSpawning = false;
    public float timeBetweenSpawns = 1f;
    private int spawnedEnemies = 0;
    [SerializeField]
    private GameObject enemyBeetle, enemy2, enemy3;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnEnemies(float spawnRate)
    {
        GetComponent<EnemyNestHealth>().health = 100;
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
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

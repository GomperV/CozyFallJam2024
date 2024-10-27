using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    [SerializeField] private MinionBullet fireball;
    [SerializeField] private float timeBetweenFiring = 0.3f;
    public float maxDistance = 20f;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;   
    }
    private GameObject GetClosestEnemy()
    {
        EnemyCombat[] enemies = FindObjectsOfType<EnemyCombat>();
        GameObject closest = null;
        float closestSqDist = float.MaxValue;
        for (int i = 0; i < enemies.Length; i++)
        {
            float sqDist = Vector2.SqrMagnitude(transform.position - enemies[i].transform.position);
            if (Vector2.Distance(transform.position, enemies[i].transform.position) < 10f)
                {
                var hit = Physics2D.Raycast(transform.position, enemies[i].transform.position - transform.position, 999f, LayerMask.GetMask("Terrain", "Enemy"));
                if (hit.rigidbody.CompareTag("Enemy"))
                {
                    closestSqDist = sqDist;
                    closest = hit.rigidbody.gameObject;
                }
            }
        }

        return closest;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenFiring)
        {
            GameObject closest = GetClosestEnemy();
            if (closest)
            {
                //_sfxInstance.start();
                var bullet = Instantiate(fireball, transform.position, Quaternion.identity);
                bullet.AimTowards(closest);
                timer = 0;
            }
        }
    }
}

using System.Collections;

using Unity.VisualScripting;

using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float force = 10;
    public float damage = 20f;
    public ParticleSystem trailParticle;
    public GameObject explosionPrefab;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void AimTowards(GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 direction = targetPos - transform.position;
        Vector3 rotation = transform.position - targetPos;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        Vector3 scale = transform.localScale;
        float angle = Vector2.SignedAngle(Vector2.right, rotation);
        if(angle > 90f || angle < -90f)
        {
            scale.x = -scale.x;
            angle += 180f;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Terrain"))
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Terrain"))
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        trailParticle.transform.SetParent(null, true);
        //trailParticle.transform.localScale = transform.localScale;
        trailParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        var timer = trailParticle.AddComponent<DestroyAfterTime>();
        timer.lifetime = 2f;

        var explosion = Instantiate(explosionPrefab, null, false);
        explosion.transform.position = transform.position;

        SplashDamage();
        Destroy(gameObject);
    }

    private IEnumerator DestroyRoutine()
    {
        yield return null;
    }

    private void SplashDamage()
    {
        const float DAMAGE = 20f;
        const float RADIUS = 5f;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if(Vector2.Distance(transform.position, enemy.transform.position) <= RADIUS)
            {
                EnemyCombat health = enemy.GetComponent<EnemyCombat>();
                if (health)
                {
                    health.TakeDamage(DAMAGE);
                }
            }
        }

        GameObject[] nests = GameObject.FindGameObjectsWithTag("EnemyNest");
        foreach(GameObject nest in nests)
        {
            float distance = Vector2.Distance(transform.position, nest.transform.position);
            if(Vector2.Distance(transform.position, nest.transform.position) <= RADIUS)
            {
                EnemyNestHealth health = nest.GetComponent<EnemyNestHealth>();
                if(health)
                {
                    health.TakeDamage(DAMAGE);
                }

            }
        }
    }
}

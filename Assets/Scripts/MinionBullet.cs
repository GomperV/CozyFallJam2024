using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class MinionBullet : MonoBehaviour
{
    public float force = 10;
    public float damage = 20f;
    public ParticleSystem trailParticle;
    public GameObject explosionPrefab;
    private bool damageDone = false;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        damage = 10f;
    }

    public void AimTowards(GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 direction = targetPos - transform.position;
        Vector3 rotation = transform.position - targetPos;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        Vector3 scale = transform.localScale;
        float angle = Vector2.SignedAngle(Vector2.right, rotation);
        if (angle > 90f || angle < -90f)
        {
            scale.x = -scale.x;
            angle += 180f;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrain"))
        {
            DestroyBullet();
            //only damage enemies, not nests
        } else if (collision.CompareTag("Enemy"))
        {
            if (damageDone) return;
            try
            {
                collision.gameObject.GetComponentInParent<EnemyCombat>().TakeDamage(damage);
                if(collision.gameObject.GetComponentInParent<EnemyMovement>() != null)
                {
                    collision.gameObject.GetComponentInParent<EnemyMovement>().SlowDown();
                }
            }
            catch
            {

            }
           
            DestroyBullet();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        //trailParticle.transform.SetParent(null, true);
        //trailParticle.transform.localScale = transform.localScale;
        //trailParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //var timer = trailParticle.AddComponent<DestroyAfterTime>();
        //timer.lifetime = 2f;

        //var explosion = Instantiate(explosionPrefab, null, false);
        //explosion.transform.position = transform.position;

        Destroy(gameObject);
    }

    private IEnumerator DestroyRoutine()
    {
        yield return null;
    }

    
}

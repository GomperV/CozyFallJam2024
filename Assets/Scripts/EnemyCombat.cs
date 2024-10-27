using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float health = 50f;
    [SerializeField] public float damage = 25f;
    public SpriteRenderer sprite;
    public float flameDamageCooldown = 0.1f;
    public GameObject damageTakenParticle;

    private bool damageDone = false;
    private GameObject player;
    private PlayerExperience playerExp;

    private float _lastFlameHit = -999f;

    void Start()
    {
        player = GameObject.Find("Player");
        playerExp = player.GetComponent<PlayerExperience>();
        damage = 25f;
        //health = 50f;
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Instantiate(damageTakenParticle, transform.position, Quaternion.identity, null);
        if(health < 1 )
        {
            OnDie();
        }
    }

    public void TakeFlamethrowerDamage()
    {
        if(Time.time > _lastFlameHit + flameDamageCooldown)
        {
            health -= 10f;
            _lastFlameHit = Time.time;
            var particle = Instantiate(damageTakenParticle, null);
            particle.transform.position = transform.position;

            if(health < 1)
            {
                OnDie();
            }
        }
    }

    public void OnDie()
    {
        playerExp.GainExperience(10, false);
        Destroy(gameObject);
    }

    void Update()
    {
        if (health < 75f && health > 25f)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.7f);
        }
        else if (health < 25f)
        {
            //GetComponent<EnemySpawner>().isSpawning = false;
            sprite.color = new Color(1f, 1f, 1f, 0.4f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBase") && !damageDone)
        {
            damageDone = true;
            PlayerBaseHealth pb = collision.GetComponent<PlayerBaseHealth>();
            pb.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}

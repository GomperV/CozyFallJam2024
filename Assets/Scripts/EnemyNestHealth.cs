using TMPro;

using UnityEngine;
public class EnemyNestHealth : MonoBehaviour
{
    public float health;
    public GameObject deathParticle;
    public GameObject damageTakenParticle;
    public SpriteRenderer sprite;
    [SerializeField]
    private TMP_Text baseHealthText;
    private GameObject UI;
    private WavesManager wavesManager;
    public bool nestDestroyed = false;
    public SpriteRenderer nestSprite;
    public float flameDamageCooldown = 0.1f;
    public float originalHealth;
    private float _lastFlameHit = -999f;
    private Vector2 originalScale;

    private void Awake()
    {
        health = 0f;
        nestDestroyed = true;
    }

    void Start()
    {
        originalScale = transform.localScale;
        wavesManager = GameObject.Find("WavesManager").GetComponent<WavesManager>();
        UI = GameObject.Find("UI");
    }

    public void TakeDamage(float dmg)
    {
        if (nestDestroyed) return;
        health -= dmg;
        Instantiate(damageTakenParticle, transform.position, Quaternion.identity, null);
        if (health < 1f)
        {
            DestroyNest();
        }
    }

    private void DestroyNest()
    {
        health = 0f;
        nestDestroyed = true;
        wavesManager.SpawnerDestroyed();
        var particle = Instantiate(deathParticle);
        particle.transform.rotation = sprite.transform.rotation;
        particle.transform.position = sprite.transform.position /*+ particle.transform.up*-0.5f*/;
    }

    public void TakeFlamethrowerDamage()
    {
        if(Time.time <= _lastFlameHit + flameDamageCooldown || nestDestroyed)
        {
            return;
        }

        Instantiate(damageTakenParticle, transform.position, Quaternion.identity, null);
        health -= 10f;
        _lastFlameHit = Time.time;

        if(health < 1)
        {
            DestroyNest();
        }
    }

    void Update()
    {
        if(wavesManager.buffEnemies)
        {
            sprite.transform.localScale = Vector3.one*2f;
        } else
        {
            sprite.transform.localScale = Vector3.one;
        }
        if(originalHealth == 0)
        {
            baseHealthText.text = "Inactive"; 
            GetComponent<EnemySpawner>().isSpawning = false;
            nestSprite.color = new Color(1f, 1f, 1f, 0.25f);
            return;
        }
        float healthPercentage = Mathf.Round(health / originalHealth * 100);
        baseHealthText.text = "" + healthPercentage + "%";
        if (healthPercentage > 99f)
        {
            nestSprite.color = new Color(1f, 1f, 1f, 1f);
        }else if (healthPercentage < 50f && healthPercentage >= 1f)
        {
            nestSprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if (healthPercentage < 1f)
        {
            GetComponent<EnemySpawner>().isSpawning = false;
            nestSprite.color = new Color(1f, 1f, 1f, 0.25f);
        }

    }

}

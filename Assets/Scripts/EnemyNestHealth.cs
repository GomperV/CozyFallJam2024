using TMPro;

using UnityEngine;
public class EnemyNestHealth : MonoBehaviour
{
    public float health;
    public GameObject deathParticle;
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

    private void Awake()
    {
        health = 0f;
        nestDestroyed = true;
    }

    void Start()
    {
        wavesManager = GameObject.Find("WavesManager").GetComponent<WavesManager>();
        UI = GameObject.Find("UI");
    }

    public void TakeDamage(float dmg)
    {
        if (nestDestroyed) return;
        health -= dmg;
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
        particle.transform.position = sprite.transform.position + particle.transform.up*-0.5f;
    }

    public void TakeFlamethrowerDamage()
    {
        if(Time.time <= _lastFlameHit + flameDamageCooldown || nestDestroyed)
        {
            return;
        }

        health -= 10f;
        _lastFlameHit = Time.time;

        if(health < 1)
        {
            DestroyNest();
        }
    }

    void Update()
    {
        
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

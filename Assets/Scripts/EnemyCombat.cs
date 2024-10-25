using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyCombat : MonoBehaviour
{
    public float health;
    [SerializeField]
    //private TMP_Text healthText;
    // Start is called before the first frame update
    public float damage = 25f;
    private bool damageDone = false;
    private GameObject player;
    private PlayerExperience playerExp;
    void Start()
    {
        player = GameObject.Find("Player");
        playerExp = player.GetComponent<PlayerExperience>();
        damage = 25f;
        health = 50f;
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health < 1 )
        {
            OnDie();
        }
    }

    public void OnDie()
    {
        playerExp.GainExperience(10, false);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //healthText.text = "" + health + "%";
        if (health < 75f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);
        }
        else if (health < 25f)
        {
            GetComponent<EnemySpawner>().isSpawning = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
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

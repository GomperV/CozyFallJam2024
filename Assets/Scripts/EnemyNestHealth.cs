using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyNestHealth : MonoBehaviour
{
    public float health;
    [SerializeField]
    private TMP_Text baseHealthText;
    private GameObject UI;
    private WavesManager wavesManager;
    public bool nestDestroyed = false;
    public SpriteRenderer nestSprite;

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
            nestDestroyed = true;
            wavesManager.SpawnerDestroyed();
        }
    }

    // Update is called once per frame
    void Update()
    {
        baseHealthText.text = "" + health + "%";
        if (health > 99f)
        {
            nestSprite.color = new Color(1f, 1f, 1f, 1f);
        }else if (health < 50f && health >= 1f)
        {
            nestSprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if (health < 1f)
        {
            GetComponent<EnemySpawner>().isSpawning = false;
            nestSprite.color = new Color(1f, 1f, 1f, 0.25f);
        }

    }

}

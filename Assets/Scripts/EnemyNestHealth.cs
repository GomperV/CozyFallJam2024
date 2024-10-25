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
    // Start is called before the first frame update
    void Start()
    {
        nestDestroyed = true;
        wavesManager = GameObject.Find("WavesManager").GetComponent<WavesManager>();
        UI = GameObject.Find("UI");
        health = 0f;
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
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }else if (health < 50f && health >= 1f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if (health < 1f)
        {
            GetComponent<EnemySpawner>().isSpawning = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
        }

    }

}

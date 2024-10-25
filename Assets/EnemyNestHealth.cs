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
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UI");
        health = 100f;
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
    // Update is called once per frame
    void Update()
    {
        baseHealthText.text = "" + health + "%";
        if(health < 50f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);
        } else if (health < 1f)
        {
            GetComponent<EnemySpawner>().isSpawning = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

}

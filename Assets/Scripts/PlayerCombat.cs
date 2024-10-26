using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private GameObject fireball;
    private bool canFire = true;
    private float timer, health;
    private float timeBetweenFiring = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        // TODO: automatically fire at the nearest enemy when you have the pumpkin lantern ability
        if (Input.GetKey(KeyCode.LeftControl) && canFire)
        {
            Instantiate(fireball, transform.position, Quaternion.identity);
            canFire = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    public GameObject PunktB, PunktA;
    private Transform player;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float patrolSpeed;
    private bool facingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Head Root").transform;
        patrolSpeed = Random.Range(1f, 2f);
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PunktB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //move towards player if they get close
        if (Vector2.Distance(transform.position, player.position) > 3.5f)
        {
            rb.velocity = (player.position - transform.position).normalized * 2f;
        } else
        {
            rb.velocity = (player.position - transform.position).normalized * 0f;
        }
    }
 }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefenderMovement : MonoBehaviour
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
        if(Vector2.Distance(transform.position, player.position) < 3.5f)
        {
            print("leap to the player!");
            rb.velocity = (player.position - transform.position).normalized * 2.5f;
            return;
        }

        rb.velocity = (currentPoint.position - transform.position).normalized*patrolSpeed;

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PunktB.transform)
        {
            currentPoint = PunktA.transform;
            if (facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PunktA.transform)
        {
            currentPoint = PunktB.transform;
            if (!facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
    }
}

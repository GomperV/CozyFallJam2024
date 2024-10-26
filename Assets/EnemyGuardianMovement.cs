using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardianMovement : MonoBehaviour
{
    public GameObject PunktB, PunktA;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float patrolSpeed;
    private bool facingRight = false;
    private bool reachedTarget = false;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        patrolSpeed = Random.Range(2f, 4f);
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PunktB.transform;
    }
    void MoveTowardsTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        transform.Translate(direction * 0.5f * Time.deltaTime, Space.World);

        float distanceToTarget = Vector3.Distance(transform.position, player.position);
        if (distanceToTarget < 0.5f)
        {
            reachedTarget = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currentPoint == null)
        {
            rb.velocity = (player.position - transform.position).normalized * 2f;
            return;
        }

        rb.velocity = (currentPoint.position - transform.position).normalized * patrolSpeed;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerBase;
    public float movementSpeed = 5f;
    public float baseDamage;
    private bool reachedTarget = false;
    private bool isSlowedDown = false;
    public GameObject patrolPointA, patrolPointB;

    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.Find("PlayerBase").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedTarget)
        {
            MoveTowardsTarget();
        }
    }
    //movement towards base / player / other objectives (if needed)
    void MoveTowardsTarget()
    {
        Vector3 direction = (playerBase.position - transform.position).normalized;

        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        float distanceToTarget = Vector3.Distance(transform.position, playerBase.position);
        if (distanceToTarget < 0.5f)
        {
            reachedTarget = true;
        }
    }
}

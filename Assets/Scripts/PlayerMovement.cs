using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 targetPosition;
    public Vector3 originalPosition;
    public Quaternion targetRotation;
    private void Start()
    {
        originalPosition = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //this is ugly logic i used in Fire in Spring (PLACEHOLDER FOR TESTING!!)

        if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = originalPosition.z;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 direction = (targetPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        }
        else if (Input.GetButton("Fire2") && !Input.GetButton("Fire1"))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = originalPosition.z;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 direction = (targetPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            targetPosition = transform.position;
            return;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        transform.position = Vector3.MoveTowards(transform.position, (new Vector3(targetPosition.x, targetPosition.y, 0f)), Time.deltaTime * 5);
    }
}

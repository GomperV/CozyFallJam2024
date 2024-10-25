using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 1f;

    void Start()
    {
        StartCoroutine(DestroyRoutine());
    }


    IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

using Basics;

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Collider or trigger that deals out hits to Hurtboxes
/// </summary>
public class Hitbox : MonoBehaviour
{
    [TagSelector, SerializeField]
    private string _targetTag;
    public UnityEvent<GameObject> hit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(string.IsNullOrEmpty(_targetTag) || other.CompareTag(_targetTag))
        {
            hit.Invoke(other.gameObject);
            other.GetComponent<Hurtbox>().hit.Invoke(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(string.IsNullOrEmpty(_targetTag) || collision.gameObject.CompareTag(_targetTag))
        {
            hit.Invoke(collision.gameObject);
            collision.gameObject.GetComponent<Hurtbox>().hit.Invoke(gameObject);
        }
    }
}
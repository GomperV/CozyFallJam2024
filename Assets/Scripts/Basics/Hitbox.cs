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
    public bool canHitTerrain;
    public UnityEvent<GameObject> hit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(string.IsNullOrEmpty(_targetTag) || other.CompareTag(_targetTag))
        {
            hit.Invoke(other.gameObject);
            Hurtbox hurtbox = other.GetComponent<Hurtbox>();
            hurtbox.hit.Invoke(gameObject);
        }

        if(canHitTerrain && other.CompareTag("Terrain"))
        {
            hit?.Invoke(other.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(string.IsNullOrEmpty(_targetTag) || collision.gameObject.CompareTag(_targetTag))
        {
            hit.Invoke(collision.gameObject);
            Hurtbox hurtbox = collision.gameObject.GetComponent<Hurtbox>();
            hurtbox.hit.Invoke(gameObject);
        }

        if(canHitTerrain && collision.gameObject.CompareTag("Terrain"))
        {
            hit?.Invoke(collision.gameObject);
        }
    }
}

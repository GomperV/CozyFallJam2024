using Basics;

using UnityEngine;
using UnityEngine.Events;

public class ContinuousHitBox : MonoBehaviour
{
    [TagSelector, SerializeField]
    private string _targetTag;
    public UnityEvent<GameObject> continuousHit;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(string.IsNullOrEmpty(_targetTag) || other.CompareTag(_targetTag))
        {
            continuousHit?.Invoke(other.gameObject);
            other.GetComponent<Hurtbox>().continuousHit?.Invoke(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(string.IsNullOrEmpty(_targetTag) || collision.gameObject.CompareTag(_targetTag))
        {
            continuousHit?.Invoke(collision.gameObject);
            collision.gameObject.GetComponent<Hurtbox>().continuousHit?.Invoke(gameObject);
        }
    }
}

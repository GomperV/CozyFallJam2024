using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Collider or trigger that takes hits from Hitboxes
/// </summary>
public class Hurtbox : MonoBehaviour
{
    public UnityEvent<GameObject> hit;
    public UnityEvent<GameObject> continuousHit;
}

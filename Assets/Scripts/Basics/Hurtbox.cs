using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Collider or trigger that takes hits from Hitboxes
/// </summary>
public class Hurtbox : MonoBehaviour
{
    public UnityEvent<GameObject> hit;
    public UnityEvent<GameObject> continuousHit;

#if UNITY_EDITOR
    private void Awake()
    {
        if(gameObject.layer != LayerMask.NameToLayer("Hurtbox"))
        {
            Debug.LogError("Hurtbox is not in the correct layer", this);
        }
    }
#endif
}

using System.Collections;

using UnityEngine;

public class BugJitter : MonoBehaviour
{
    public float distance;
    public float delay;
    public float speed;
    public float smoothTime;

    private Vector2 _center;
    private Vector2 _currentVelocity;

    IEnumerator Start()
    {
        _center = transform.position;

        while(true)
        {
            Vector2 target = _center + Random.insideUnitCircle*distance;
            while(Vector2.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector2.SmoothDamp(transform.position, target, ref _currentVelocity, smoothTime);
                yield return null;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject head;
    public GameObject[] segments;
    public float moveSpeed = 5f;
    public float segmentDistance = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Only move and rotate the character when its far enough away to prevent jitter
        if(Vector3.Distance(mousePos, head.transform.position) > 0.2f)
        {
            float angle = Vector2.SignedAngle(Vector2.up, mousePos - head.transform.position);
            head.transform.position = Vector3.MoveTowards(head.transform.position, mousePos, moveSpeed*Time.deltaTime);
            head.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if(segments.Length > 0)
        {
            MoveSegmentTowards(segments[0].transform, head.transform);
        }

        for(int i = 1; i < segments.Length; i++)
        {
            MoveSegmentTowards(segments[i].transform, segments[i - 1].transform);
        }
    }

    private void MoveSegmentTowards(Transform segment, Transform target)
    {
        Vector3 delta = target.position - segment.position;
        float distance = delta.magnitude;
        Vector2 direction = delta.normalized;
        if(distance > segmentDistance)
        {
            Vector3 targetWithMargin = target.position - (Vector3)direction*segmentDistance;
            segment.position = Vector3.MoveTowards(segment.position, targetWithMargin, moveSpeed);
        }
    }
}

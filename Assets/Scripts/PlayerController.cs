using System.Collections.Generic;

using TriInspector;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject head;
    public List<GameObject> segments;
    public float moveSpeed = 5f;
    public float segmentDistance = 3f;
    public GameObject segmentPrefab;
    public Transform playerRoot;
    public Sprite normalSegmentSprite;
    public Sprite brokenSegmentSprite;

    [Header("Speed boost")]
    public float boostDuration = 1f;
    public float boostAmount = 2f;
    public float boostCooldown = 3f;

    private float _lastBoost = -999f;
    private List<SpriteRenderer> _segmentSprites;

    void Start()
    {
        _segmentSprites = new List<SpriteRenderer>();
        for(int i = 0; i < segments.Count; i++)
        {
            _segmentSprites.Add(segments[i].GetComponentInChildren<SpriteRenderer>());
        }
    }

    [Button]
    public void SetHealthDisplay(int missingHealth)
    {
        for(int i = 0; i < segments.Count; i++)
        {
            _segmentSprites[i].sprite = i < missingHealth ? brokenSegmentSprite : normalSegmentSprite;
        }
    }

    [Button]
    public void AddSegment()
    {
        GameObject segment = Instantiate(segmentPrefab, playerRoot);
        segments.Add(segment);
        _segmentSprites.Add(segment.GetComponentInChildren<SpriteRenderer>());
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            ActivateSpeedBoost();
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        float speed = GetMoveSpeed();

        // Only move and rotate the character when its far enough away to prevent jitter
        if(Vector3.Distance(mousePos, head.transform.position) > 0.2f)
        {
            float angle = Vector2.SignedAngle(Vector2.up, mousePos - head.transform.position);
            head.transform.position = Vector3.MoveTowards(head.transform.position, mousePos, speed*Time.deltaTime);
            head.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if(segments.Count > 0)
        {
            MoveSegmentTowards(segments[0].transform, head.transform, speed);
        }

        for(int i = 1; i < segments.Count; i++)
        {
            MoveSegmentTowards(segments[i].transform, segments[i - 1].transform, speed);
        }
    }

    public void ActivateSpeedBoost()
    {
        if(Time.time > _lastBoost + boostCooldown)
        {
            _lastBoost = Time.time;
        }
    }

    public float GetMoveSpeed()
    {
        float speed = moveSpeed;
        if(Time.time < _lastBoost + boostDuration)
        {
            speed += boostAmount;
        }
        return speed;
    }

    private void MoveSegmentTowards(Transform segment, Transform target, float speed)
    {
        Vector3 delta = target.position - segment.position;
        float distance = delta.magnitude;
        Vector2 direction = delta.normalized;
        if(distance > segmentDistance)
        {
            Vector3 targetWithMargin = target.position - (Vector3)direction*segmentDistance;
            segment.position = Vector3.MoveTowards(segment.position, targetWithMargin, speed);
        }
    }
}

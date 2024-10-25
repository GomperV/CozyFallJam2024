using UnityEngine;

public class FriendWobble : MonoBehaviour
{
    public float magnitude = 1f;
    public float speed = 1f;

    private Vector3 _start;

    private void Start()
    {
        _start = transform.localPosition;
    }

    private void Update()
    {
        Vector3 pos = _start;
        pos.x += Mathf.Sin(Time.time*speed)*magnitude;
        transform.localPosition = pos;
    }
}
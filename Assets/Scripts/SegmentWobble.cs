using UnityEngine;

public class SegmentWobble : MonoBehaviour
{
    public float wobbleSize = 0.5f;
    public float wobbleSpeed = 5f;
    public float offset;

    void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Sin((Time.time + offset)*wobbleSpeed)*wobbleSize;
        transform.localPosition = pos;
    }
}

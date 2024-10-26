using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float left, top, right, bottom;
    public Transform followTarget;

    void Update()
    {
        Vector3 pos = followTarget.position;

        if(pos.x < left) pos.x = left;
        if(pos.x > right) pos.x = right;
        if(pos.y > top) pos.y = top;
        if(pos.y < bottom) pos.y = bottom;
        pos.z = -10f;

        transform.position = pos;
    }
}

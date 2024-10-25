using UnityEngine;

namespace Basics
{
    public class BoundedMovement : MonoBehaviour
    {
        public Vector2 boundarySize;
        private Vector2 _startPos;

        private void Start()
        {
            _startPos = transform.localPosition;
        }

        public void SetBoundedPosition(Vector2 pos)
        {
            pos.x = Mathf.Clamp(pos.x, -1f, 1f);
            pos.y = Mathf.Clamp(pos.y, -1f, 1f);
            
            Vector3 result = _startPos + pos*boundarySize*0.5f;
            result.z = transform.localPosition.z;
            transform.localPosition = result;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if(!UnityEditor.EditorApplication.isPlaying)
            {
                Gizmos.matrix = transform.parent.localToWorldMatrix;
                Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
                Gizmos.DrawWireCube(Vector3.zero, boundarySize);
            }
        }
#endif
    }
}

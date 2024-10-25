using UnityEngine;

namespace Basics
{

    public class HorizontalOscillator : MonoBehaviour
    {
        public float distance = 1f;
        public float duration = 1f;

        private float _startingX;

        void Start()
        {
            _startingX = transform.localPosition.x;
        }

        void Update()
        {
            Vector3 pos = transform.localPosition;
            pos.x = _startingX + Mathf.Sin(Time.time*2f*Mathf.PI*duration)*distance;
            transform.localPosition = pos;
        }
    }
}

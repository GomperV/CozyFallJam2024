using UnityEngine;

namespace Basics
{
    public class FramerateDebugger : MonoBehaviour
    {
#if UNITY_EDITOR
        public int frameRate = 60;

        void Update()
        {
            Application.targetFrameRate = frameRate;
        }
#endif
    }
}

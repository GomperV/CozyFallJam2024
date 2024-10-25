using UnityEngine;

namespace Basics
{
    public class TimeController : MonoBehaviour
    {
        public bool resetTimeOnStart;

        private void Start()
        {
            if(resetTimeOnStart)
            {
                StartTime();
            }
        }

        public void StartTime()
        {
            Time.timeScale = 1f;
        }

        public void PauseTime()
        {
            Time.timeScale = 0f;
        }
    }
}

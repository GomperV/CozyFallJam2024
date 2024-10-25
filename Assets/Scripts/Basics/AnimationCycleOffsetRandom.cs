using UnityEngine;

namespace Basics
{
    [RequireComponent(typeof(Animator))]
    public class AnimationCycleOffsetRandom : MonoBehaviour
    {
        public float minOffset = 0f;
        public float maxOffset = 1f;

        void Start()
        {
            GetComponent<Animator>().SetFloat("CycleOffset", Random.Range(minOffset, maxOffset));
        }
    }
}

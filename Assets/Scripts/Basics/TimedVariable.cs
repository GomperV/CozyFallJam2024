using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Basics
{
    public class TimedVariable : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [Tooltip("Value if the timer is inactive. For instance, you can set it to 1 when using this variable as a multiplier.")]
        [SerializeField] private float _inactiveValue = 1f;
        [Tooltip("Additional value to add to the final timed curve result.")]
        [SerializeField] private float _curveBaseValue = 0f;
        [SerializeField] private float _curveMultiplier = 1f;

        [Space]
        public UnityEvent timerStarted;
        public UnityEvent timerEnded;

        private float _startTime = -99999f;
        private bool _timerEndInvoked = true;

        /// <summary>
        /// The current position of the timer normalized to a 0-1 range.
        /// </summary>
        public float TimerProgress => Mathf.InverseLerp(_startTime, _startTime + _duration, Time.time);

        public bool IsActive()
        {
            return Time.time < _startTime + _duration;
        }

        public float GetValue()
        {
            if(!IsActive())
            {
                if(!_timerEndInvoked)
                {
                    _timerEndInvoked = true;
                    timerEnded?.Invoke();
                }

                return _inactiveValue;
            }

            return _curveBaseValue + _curve.Evaluate(TimerProgress)*_curveMultiplier;
        }

        public void Activate()
        {
            _startTime = Time.time;
            timerStarted?.Invoke();
            _timerEndInvoked = false;
        }
    }
}

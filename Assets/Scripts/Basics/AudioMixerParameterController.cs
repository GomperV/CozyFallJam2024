using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace Basics
{
    public class AudioMixerParameterController : MonoBehaviour
    {
        public AudioMixer mixer;
        public string parameter;
        public float rangeMin = -80f;
        public float rangeMax = 0f;
        public string saveToName;
        public AnimationCurve parameterCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public UnityEvent<float> onParameterChanged;

        private void Awake()
        {
            LoadValue();
        }

        public void SetValue(float value)
        {
            mixer.SetFloat(parameter, parameterCurve.Evaluate(value));
            onParameterChanged?.Invoke(value);
            SaveValue(value);
        }

        private void SaveValue(float value)
        {
            if(!string.IsNullOrWhiteSpace(saveToName))
            {
                PlayerPrefs.SetFloat(saveToName, value);
            }
        }

        public void LoadValue(float defaultValue = 1f)
        {
            if(!string.IsNullOrWhiteSpace(saveToName) && PlayerPrefs.HasKey(saveToName))
            {
                float value = PlayerPrefs.GetFloat(saveToName, defaultValue);
                mixer.SetFloat(parameter, parameterCurve.Evaluate(value));
                onParameterChanged?.Invoke(value);
            }
        }
    }
}

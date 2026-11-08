using lutilities.DataTypeSO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace lutilities.Audio
{
    public class AudioSlider : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("Settings")]
        [Tooltip("Name of the exposed parameter in the AudioMixer (e.g. MusicVolume).")]
        [SerializeField] private StringSO exposedParameter = null;

        [SerializeField, Range(0.0001f, 1f)] private float defaultValue = 0.75f;

        private Slider slider;
        private const float MinSliderValue = 0.0001f; // avoids Log10(0) = -infinity

        private void OnValidate()
        {
            slider = GetComponent<Slider>();
        }

        private void Start()
        {
            slider.minValue = MinSliderValue;
            slider.maxValue = 1f;

            float saved = PlayerPrefs.GetFloat(GetKey(), defaultValue);

            slider.SetValueWithoutNotify(saved);
            ApplyVolume(saved);

            slider.onValueChanged.AddListener(OnSliderChanged);
        }

        private void OnDestroy()
        {
            if (slider != null)
                slider.onValueChanged.RemoveListener(OnSliderChanged);

            PlayerPrefs.Save();
        }

        private void OnApplicationPause(bool paused)
        {
            // Important on mobile, where OnDestroy may never be called
            if (paused) PlayerPrefs.Save();
        }

        private void OnSliderChanged(float value)
        {
            ApplyVolume(value);
            PlayerPrefs.SetFloat(GetKey(), value);
        }

        private void ApplyVolume(float linearValue)
        {
            float dB = Mathf.Log10(Mathf.Max(linearValue, MinSliderValue)) * 20f;
            audioMixer.SetFloat(exposedParameter.Value, dB);
        }

        private string GetKey()
        {
            return $"Pref_{exposedParameter}";
        }
    }
}

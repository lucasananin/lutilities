using lutilities.DataTypeSO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace lutilities.Audio
{
    public class AudioToggle : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("Settings")]
        [Tooltip("Name of the exposed parameter in the AudioMixer (e.g. MusicVolume).")]
        [SerializeField] private StringSO exposedParameter = null;

        [Tooltip("Whether sound is enabled on first run.")]
        [SerializeField] private bool defaultEnabled = true;

        private Toggle toggle;
        private readonly float enabledDecibels = 0f;
        private readonly float mutedDecibels = -80f;

        private void OnValidate()
        {
            toggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            bool isEnabled = PlayerPrefs.GetInt(GetKey(), defaultEnabled ? 1 : 0) == 1;

            toggle.SetIsOnWithoutNotify(isEnabled);
            ApplyState(isEnabled);

            toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private void OnDestroy()
        {
            if (toggle != null)
                toggle.onValueChanged.RemoveListener(OnToggleChanged);

            PlayerPrefs.Save();
        }

        private void OnApplicationPause(bool paused)
        {
            // Important on mobile, where OnDestroy may never be called
            if (paused) PlayerPrefs.Save();
        }

        private void OnToggleChanged(bool isEnabled)
        {
            ApplyState(isEnabled);
            PlayerPrefs.SetInt(GetKey(), isEnabled ? 1 : 0);
            PlayerPrefs.Save(); // toggles change rarely, so saving immediately is fine
        }

        private void ApplyState(bool isEnabled)
        {
            audioMixer.SetFloat(exposedParameter.Value, isEnabled ? enabledDecibels : mutedDecibels);
        }

        private string GetKey()
        {
            return $"Pref_{exposedParameter.Value}";
        }
    }
}

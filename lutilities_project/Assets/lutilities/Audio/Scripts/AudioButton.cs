using UnityEngine.UI;

namespace lutilities.Audio
{
    public class AudioButton : AudioPlayer
    {
        private Button _button = null;

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Play);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Play);
        }

        private void Play()
        {
            Play(null);
        }
    }
}

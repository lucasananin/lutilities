using UnityEngine;
using UnityEngine.UI;

namespace lutilities.SceneManagement
{
    public class SceneLoaderButton : SceneLoader
    {
        [SerializeField] Button _button = null;

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Load);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Load);
        }
    }
}

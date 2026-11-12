using UnityEngine;
using UnityEngine.UI;

namespace lutilities.UI
{
    public class QuitButton : MonoBehaviour
    {
        private Button button;

        private void OnValidate()
        {
            button = GetComponent<Button>();
        }

        private void Awake()
        {
            if (button == null)
                button = GetComponent<Button>();

            if (button != null)
                button.onClick.AddListener(Quit);
        }

        private void OnDestroy()
        {
            if (button != null)
                button.onClick.RemoveListener(Quit);
        }

        private void Quit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}

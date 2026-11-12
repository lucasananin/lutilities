using TMPro;
using UnityEngine;

namespace lutilities.UI
{
    public class ProjectVersionDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI versionText;

        private void OnValidate()
        {
            versionText = GetComponent<TextMeshProUGUI>();
        }

        private void Awake()
        {
            UpdateVersionDisplay();
        }

        private void UpdateVersionDisplay()
        {
            versionText.text = $"Version {Application.version}";
        }
    }
}

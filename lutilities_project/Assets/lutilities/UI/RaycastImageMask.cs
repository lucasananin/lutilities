using UnityEngine;
using UnityEngine.UI;

namespace lutilities.UI
{
    public class RaycastImageMask : MonoBehaviour
    {
        // Basta colocar esse script no mesmo GameObject do componente "Button";
        private void Start()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace lutilities
{
    [RequireComponent(typeof(RawImage))]
    public class RawImageScroller : MonoBehaviour
    {
        [Header("Scroll Settings")]
        [SerializeField] private Vector2 scrollSpeed = new(0.1f, 0f);

        private RawImage rawImage;
        private Vector2 currentOffset;

        private void OnValidate()
        {
            rawImage = GetComponent<RawImage>();
        }

        private void Awake()
        {
            if (rawImage == null)
                rawImage = GetComponent<RawImage>();

            currentOffset = rawImage.uvRect.position;
        }

        private void Update()
        {
            currentOffset += scrollSpeed * Time.deltaTime;

            Rect uvRect = rawImage.uvRect;
            uvRect.position = currentOffset;
            rawImage.uvRect = uvRect;
        }
    }
}

using UnityEngine;

namespace lutilities.VFX
{
    public class RotateFX : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotationSpeed = new(0f, 90f, 0f);

        private void Update()
        {
            transform.Rotate(_rotationSpeed * Time.deltaTime);
        }
    }
}

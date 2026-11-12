using UnityEngine;

namespace lutilities.General
{
    public class FollowTarget : MonoBehaviour
    {
        public enum FollowMode
        {
            Snap,
            Smooth
        }

        [Header("Target")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;

        [Header("Settings")]
        [SerializeField] private FollowMode followMode = FollowMode.Smooth;
        [SerializeField, Min(0.001f)] private float smoothTime = 0.2f;

        private Vector3 velocity;

        private void LateUpdate()
        {
            if (target == null)
                return;

            Vector3 targetPosition = target.position + offset;

            switch (followMode)
            {
                case FollowMode.Snap:
                    transform.position = targetPosition;
                    break;

                case FollowMode.Smooth:
                    transform.position = Vector3.SmoothDamp(
                        transform.position,
                        targetPosition,
                        ref velocity,
                        smoothTime
                    );
                    break;
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            velocity = Vector3.zero;
        }

        public void SnapToTarget()
        {
            if (target == null)
                return;

            transform.position = target.position + offset;
            velocity = Vector3.zero;
        }
    }
}

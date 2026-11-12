using Unity.Cinemachine;
using UnityEngine;

namespace lutilities.CameraShake
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] bool _randomizeVelocity = true;

        private CinemachineImpulseSource _impulseSource;

        private void OnValidate()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void Awake()
        {
            if (_impulseSource == null)
                _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public virtual void Shake(float force = 1f)
        {
            if (_randomizeVelocity)
                _impulseSource.DefaultVelocity = Random.insideUnitCircle.normalized;

            _impulseSource.GenerateImpulseWithForce(force);
        }
    }
}

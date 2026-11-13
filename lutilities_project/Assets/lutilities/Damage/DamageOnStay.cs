using UnityEngine;

namespace lutilities.Damage
{
    [RequireComponent(typeof(Collider))]
    public class DamageOnStay : MonoBehaviour
    {
        [Header("Damage")]
        [SerializeField, Min(0f)] private float _damagePerTick = 5f;
        [SerializeField, Min(0.01f)] private float _damageInterval = 1f;

        [Header("Source")]
        [SerializeField] private GameObject _instigator;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;

            if (_instigator == null)
                _instigator = gameObject;
        }

        private void OnTriggerStay(Collider other)
        {
            Health health = other.GetComponentInParent<Health>();

            if (health == null || !health.IsAlive())
                return;

            // Get or create a damage timer for this target.
            if (!_damageTimers.TryGetValue(health, out float nextDamageTime))
            {
                _damageTimers.Add(health, Time.time + _damageInterval);
                return;
            }

            if (Time.time < nextDamageTime)
                return;

            ApplyDamage(health, other);

            _damageTimers[health] = Time.time + _damageInterval;
        }

        private void OnTriggerExit(Collider other)
        {
            Health health = other.GetComponentInParent<Health>();

            if (health != null)
                _damageTimers.Remove(health);
        }

        private void ApplyDamage(Health health, Collider other)
        {
            DamageInfo damageInfo = new DamageInfo(
                amount: _damagePerTick,
                source: gameObject,
                instigator: _instigator,
                hitPoint: other.ClosestPoint(transform.position),
                hitDirection: (other.transform.position - transform.position).normalized
            );

            health.TakeDamage(damageInfo);
        }

        private readonly System.Collections.Generic.Dictionary<Health, float> _damageTimers = new();
    }
}

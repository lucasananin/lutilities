using System.Collections.Generic;
using UnityEngine;

namespace lutilities.Damage
{
    [RequireComponent(typeof(Collider))]
    public class DamageOnTrigger : MonoBehaviour
    {
        [Header("Damage")]
        [SerializeField, Min(0f)] private float _damage = 10f;
        [SerializeField] private bool _damageOncePerTarget = false;

        [Header("Source")]
        [SerializeField] private GameObject _instigator;

        private readonly HashSet<Health> _hitTargets = new();
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;

            if (_instigator == null)
                _instigator = gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponentInParent<Health>();

            if (health == null || !health.IsAlive()) return;

            if (_damageOncePerTarget && !_hitTargets.Add(health)) return;

            DamageInfo damageInfo = new(
                amount: _damage,
                source: gameObject,
                instigator: _instigator,
                hitPoint: other.ClosestPoint(transform.position),
                hitDirection: (other.transform.position - transform.position).normalized
            );

            health.TakeDamage(damageInfo);
        }

        public void ResetHitTargets()
        {
            _hitTargets.Clear();
        }
    }
}
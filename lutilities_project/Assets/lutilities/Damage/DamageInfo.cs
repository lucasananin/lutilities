using System;
using UnityEngine;

namespace lutilities.Damage
{
    [Serializable]
    public sealed class DamageInfo
    {
        [SerializeField] private float _amount;
        [SerializeField] private GameObject _source;
        [SerializeField] private GameObject _instigator;
        [SerializeField] private Vector3 _hitPoint;
        [SerializeField] private Vector3 _hitDirection;

        public float Amount => _amount;
        public GameObject Source => _source;
        public GameObject Instigator => _instigator;
        public Vector3 HitPoint => _hitPoint;
        public Vector3 HitDirection => _hitDirection;

        public DamageInfo(
            float amount,
            GameObject source = null,
            GameObject instigator = null,
            Vector3 hitPoint = default,
            Vector3 hitDirection = default)
        {
            _amount = amount;
            _source = source;
            _instigator = instigator;
            _hitPoint = hitPoint;
            _hitDirection = hitDirection;
        }
    }
}
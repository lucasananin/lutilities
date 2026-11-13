using System;
using UnityEngine;
using UnityEngine.Events;

namespace lutilities.Damage
{
    public abstract class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField, Min(1f)] protected float _maxHealth = 100f;
        [SerializeField] protected bool _startAtFullHealth = true;

        [Header("Unity Events")]
        [SerializeField] private DamageTakenEvent _onDamageTaken = new();
        [SerializeField] private HealedEvent _onHealed = new();
        [SerializeField] private UnityEvent _onDied = new();
        [SerializeField] private HealthChangedEvent _onHealthChanged = new();

        protected float _currentHealth;
        protected bool _isDead;

        // C# events (code subscriptions)
        public event UnityAction<Health, DamageInfo> DamageTaken;
        public event UnityAction<Health, float> Healed;
        public event UnityAction<Health> Died;
        public event UnityAction<Health, float, float> HealthChanged;

        // Read-only properties
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;
        public bool IsDead => _isDead;

        protected virtual void Awake()
        {
            _maxHealth = Mathf.Max(1f, _maxHealth);
            _currentHealth = _startAtFullHealth ? _maxHealth : 0f;
        }

        public float GetNormalizedHealth()
        {
            return _currentHealth / _maxHealth;
        }

        public bool IsAlive()
        {
            return !_isDead;
        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            if (!IsAlive() || damageInfo == null) return;
            if (damageInfo.Amount <= 0f) return;

            float previousHealth = _currentHealth;
            _currentHealth = Mathf.Max(0f, _currentHealth - damageInfo.Amount);
            float actualDamage = previousHealth - _currentHealth;

            if (actualDamage <= 0f) return;

            DamageTaken?.Invoke(this, damageInfo);
            _onDamageTaken.Invoke(damageInfo);

            HealthChanged?.Invoke(this, previousHealth, _currentHealth);
            _onHealthChanged.Invoke(previousHealth, _currentHealth);

            OnDamageTaken(damageInfo);

            if (_currentHealth <= 0f) Die();
        }

        public void Heal(float amount)
        {
            if (!IsAlive() || amount <= 0f) return;

            float previousHealth = _currentHealth;
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
            float actualHealing = _currentHealth - previousHealth;

            if (actualHealing <= 0f) return;

            Healed?.Invoke(this, actualHealing);
            _onHealed.Invoke(actualHealing);

            HealthChanged?.Invoke(this, previousHealth, _currentHealth);
            _onHealthChanged.Invoke(previousHealth, _currentHealth);

            OnHealed(actualHealing);
        }

        public void Kill()
        {
            if (!IsAlive()) return;

            float previousHealth = _currentHealth;
            _currentHealth = 0f;

            if (previousHealth > 0f)
            {
                HealthChanged?.Invoke(this, previousHealth, _currentHealth);
                _onHealthChanged.Invoke(previousHealth, _currentHealth);
            }

            Die();
        }

        private void Die()
        {
            if (_isDead)
                return;

            _isDead = true;

            Died?.Invoke(this);
            _onDied.Invoke();

            OnDied();
        }

        // Extension points for derived classes
        protected virtual void OnDamageTaken(DamageInfo damageInfo) { }
        protected virtual void OnHealed(float amount) { }
        protected virtual void OnDied() { }
    }

    // UnityEvent types for Inspector serialization
    [Serializable]
    public class DamageTakenEvent : UnityEvent<DamageInfo> { }
    [Serializable]
    public class HealedEvent : UnityEvent<float> { }
    [Serializable]
    public class HealthChangedEvent : UnityEvent<float, float> { }
}

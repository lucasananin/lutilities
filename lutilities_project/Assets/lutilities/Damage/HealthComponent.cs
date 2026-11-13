using UnityEngine;

namespace lutilities.Damage
{
    public class HealthComponent : Health
    {
        protected override void OnDamageTaken(DamageInfo damageInfo)
        {
            base.OnDamageTaken(damageInfo);
            Debug.Log($"{name} took {damageInfo.Amount} damage!", this);
        }

        protected override void OnDied()
        {
            base.OnDied();
            Debug.Log($"{name} died!", this);
        }
    }
}

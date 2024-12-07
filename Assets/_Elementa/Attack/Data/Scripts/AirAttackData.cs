using UnityEngine;

namespace _Elementa.Attack.Data
{
    [CreateAssetMenu(menuName = "Attacks/AirAttack")]
    public class AirAttackData : ProjectileAttackData
    {
        public override void ApplyEffect(GameObject target)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(Damage);
            }
            Debug.Log($"Air attack hit {target.name} for {Damage} damage.");
        }
    }
}
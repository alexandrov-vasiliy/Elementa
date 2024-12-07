using UnityEngine;

namespace _Elementa.Attack.Data
{
    [CreateAssetMenu(menuName = "Attacks/FireAttack")]
    public class FireAttackData : ProjectileAttackData
    {
        public float burnDuration;

        public override void ApplyEffect(GameObject target)
        {
            
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(Damage);
            }
            
            // Логика эффекта FireAttack
            Debug.Log($"Fire attack hit {target.name} for {Damage} damage and burned for {burnDuration} seconds.");
            // Здесь можно добавить компонент BurnEffect к цели
        }
    }
}
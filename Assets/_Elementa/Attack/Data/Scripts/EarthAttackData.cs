using UnityEngine;

namespace _Elementa.Attack.Data
{
    [CreateAssetMenu(menuName = "Attacks/EarthAttackData")]

    public class EarthAttackData : AttackData
    {
        public override void ApplyEffect(GameObject target)
        {
            Debug.Log($"Earth attack to {target.name}");
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(Damage);
            }
        }
    }
}
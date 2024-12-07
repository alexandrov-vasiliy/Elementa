using UnityEngine;

namespace _Elementa.Attack.Data
{
    [CreateAssetMenu(menuName = "Attacks/Steam")]

    public class SteamAttackData: AttackData
    {
        public override void ApplyEffect(GameObject target)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(Damage);
            }
        }
    }
}
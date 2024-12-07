using UnityEngine;

namespace _Elementa.Attack.Data
{
    [CreateAssetMenu(menuName = "Attacks/Water")]
    public class WaterAttackData : ProjectileAttackData
    {
        public float Force = -20f;
        public override void ApplyEffect(GameObject target)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(Damage);
            }

            if (target.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(target.transform.forward * Force, ForceMode.Impulse);
            }
        }
    }
}
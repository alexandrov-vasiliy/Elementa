using UnityEngine;

namespace _Elementa.Attack.Data
{
    [CreateAssetMenu(menuName = "Attacks/AirAttack")]
    public class AirAttackData : ProjectileAttackData
    {
        public override void ApplyEffect(GameObject target)
        {
            Debug.Log($"Air attack hit {target.name} for {Damage} damage.");
        }
    }
}
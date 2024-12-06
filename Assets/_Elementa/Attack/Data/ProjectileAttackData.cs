using UnityEngine;

namespace _Elementa.Attack.Data
{
    public abstract class ProjectileAttackData : AttackData
    {
        public GameObject muzzlePrefab;
        public GameObject hitPrefab;
        public GameObject bodyPrefab;
        public float speed;
        public float searchRadius;
        
        public abstract override void ApplyEffect(GameObject target);

    }
}
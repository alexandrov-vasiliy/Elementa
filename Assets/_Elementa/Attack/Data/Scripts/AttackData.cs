using UnityEngine;

namespace _Elementa.Attack.Data
{
    public abstract class AttackData : ScriptableObject
    {
        public string AttackName;
        public float Damage;
        public float FireRate = 1f;
        public float LifeTime = 1f;
        public int AttackCount = 3;
        public abstract void ApplyEffect(GameObject target);
    }
}
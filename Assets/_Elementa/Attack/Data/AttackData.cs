using UnityEngine;

namespace _Elementa.Attack.Data
{
    public abstract class AttackData : ScriptableObject
    {
        public string AttackName;
        public float Damage;

        public abstract void ApplyEffect(GameObject target);
    }
}
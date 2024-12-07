using UnityEngine;

namespace _Elementa.Attack
{
    [CreateAssetMenu(fileName = "AttackConfig", menuName = "Attack/Config", order = 0)]
    public class AttackConfig : ScriptableObject
    {
        public LayerMask EnemyMask;
        public float EnemyFindRadius = 20f;
    }
}
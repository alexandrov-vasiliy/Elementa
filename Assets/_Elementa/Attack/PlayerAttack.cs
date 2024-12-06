using _Elementa.Attack.Data;
using _Elementa.Attack.Projectiles;
using _Elementa.ObjectPool;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        public ProjectileAttackData _airAttackData;
        public ProjectileAttackData _fireAttackData;
        
        [Inject(Id = nameof(PoolIds.Projectile))]
        private ObjectPool<Projectile> _pool;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IAttack airAttack = new AirAttack(_airAttackData, _pool);
                airAttack.ExecuteAttack(transform);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                IAttack fireAttack = new FireAttack(_fireAttackData, _pool);
                fireAttack.ExecuteAttack(transform);
            }
        }
    }
}
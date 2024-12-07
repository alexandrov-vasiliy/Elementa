using _Elementa.Attack.Data;
using _Elementa.Attack.Projectiles;
using _Elementa.ObjectPool;
using UnityEngine;

namespace _Elementa.Attack
{
    public class FireAttack: IAttack
    {
        private readonly ProjectileAttackData attackData;
    

        private ObjectPool<Projectile> _pool;

        public FireAttack(ProjectileAttackData data,ObjectPool<Projectile> pool )
        {
            this.attackData = data;
            _pool = pool;
        }

        public void ExecuteAttack(Transform owner)
        {
            var projectile = _pool.Get();
            projectile.Initialize(attackData, owner, _pool);
        }
    }
}
using _Elementa.Attack.Data;
using _Elementa.Attack.Projectiles;
using _Elementa.ObjectPool;

using UnityEngine;

namespace _Elementa.Attack
{
    public class WaterAttack: IAttack
    {
        private readonly ProjectileAttackData _attackData;
    

        private ObjectPool<Projectile> _pool;

        public WaterAttack(ProjectileAttackData data,ObjectPool<Projectile> pool )
        {
            _attackData = data;
            _pool = pool;
        }

        public void ExecuteAttack(Transform owner)
        {
            var projectile = _pool.Get();
            projectile.Initialize(_attackData, owner, _pool);
        }
    }
}
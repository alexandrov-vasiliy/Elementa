using _Elementa.Attack.DamageVariants;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack
{
    public class EarthAttack: IAttack
    {
        private readonly FindEnemy _findEnemy;
        private readonly AttackConfig _attackConfig;
        private readonly Rock _rock;
        private readonly DiContainer _container;

        public EarthAttack(FindEnemy findEnemy, AttackConfig attackConfig, Rock rock, DiContainer container)
        {
            _findEnemy = findEnemy;
            _attackConfig = attackConfig;
            _rock = rock;
            _container = container;
        }

        public void ExecuteAttack(Transform owner)
        {
           var enemy = _findEnemy.Nearest(owner.position, _attackConfig.EnemyFindRadius);
           if (enemy != null)
           {
               _container.InstantiatePrefab(_rock, enemy.transform.position + (Vector3.up * 10f), Quaternion.identity, null );
           }
           else
           {
               _container.InstantiatePrefab(_rock, owner.transform.position  + (Vector3.up * 10f), Quaternion.identity, null );

           }
        }
    }
}
using _Elementa.Attack.Data;
using _Elementa.Attack.Projectiles;
using _Elementa.Elements;
using _Elementa.ObjectPool;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        [Inject(Id = nameof(PoolIds.Projectile))]
        private ObjectPool<Projectile> _pool;
        [Inject] private ElementBar _elementBar;
        [Inject] private IAttackFactory _attackFactory;
        
         private float _lastAttackTime;
         private int _attackCount = 0;
        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                TryAttack();
            }
        }

        private void TryAttack()
        {
            var lastElement = _elementBar.GetLastElement();
            if (lastElement == null) return;

            var attackData = lastElement.AttackData;
            if (attackData == null) return;

            if (Time.time >= _lastAttackTime + attackData.FireRate)
            {
                var attack = _attackFactory.CreateAttack(lastElement, _pool);
                attack?.ExecuteAttack(transform);
                _attackCount++;
                if (_attackCount >= attackData.AttackCount)
                {
                    _elementBar.RemoveLastElement();
                    _attackCount = 0;
                }
                _lastAttackTime = Time.time;
            }
        }
    }
}
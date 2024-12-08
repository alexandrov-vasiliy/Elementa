using System;
using System.Collections;
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
        public event Action OnAttack;
        [SerializeField] private float _attackDelay = 0.3f;
        [SerializeField] private AttackAudioPlayer _attackAudioPlayer;

        
        [Inject(Id = nameof(PoolIds.Projectile))]
        private ObjectPool<Projectile> _pool;
        
        [Inject] private ElementBar _elementBar;
        [Inject] private IAttackFactory _attackFactory;
        
         private float _lastAttackTime;
         private int _attackCount = 0;
         

         private void OnEnable()
         {
             _elementBar.OnElementAdd += ClearAttackRate;
         }

         private void OnDisable()
         {
             _elementBar.OnElementAdd -= ClearAttackRate;
         }

         private void ClearAttackRate()
         {
             _lastAttackTime = 0f;
         }

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
                _attackAudioPlayer.PlaySpawnAudio(attackData);
                OnAttack?.Invoke(); 
                StartCoroutine(ExecuteAttackWithDelay(lastElement, attackData));
                _lastAttackTime = Time.time;
            }
        }
        
        private IEnumerator ExecuteAttackWithDelay(ElementData lastElement, AttackData attackData)
        {
            yield return new WaitForSeconds(_attackDelay);

            var attack = _attackFactory.CreateAttack(lastElement, _pool);
            attack?.ExecuteAttack(transform);
            _attackCount++;
            if (_attackCount >= attackData.AttackCount)
            {
                _elementBar.RemoveLastElement();
                _attackCount = 0;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using _Elementa.Attack.Data;
using _Elementa.ObjectPool;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack.Projectiles
{
    [RequireComponent(typeof(ProjectileEffects))]
    public class Projectile : MonoBehaviour
    {
        private ProjectileAttackData _attackData;
        [SerializeField] private Transform _target;
        private Vector3 _direction;
        [SerializeField] private bool _hasTarget;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _destroyTimeout = 20f;
        
        private ProjectileEffects _effects;
        [Inject] private AttackConfig _attackConfig;
        [Inject] private FindEnemy _findEnemy;
        
        private Collider[] hits = new Collider[10]; 


        public void Initialize(ProjectileAttackData projectileAttackData, Transform owner, ObjectPool<Projectile> pool)
        {
            var position = owner.position;
            transform.position = position;



            _attackData = projectileAttackData;

            _target = _findEnemy.Nearest(position, _attackConfig.EnemyFindRadius);
            if (_target != null)
            {
                _hasTarget = true;
            }
            else
            {
                _hasTarget = false;
                _direction = owner.forward; // Если врага нет, летим вперед
            }

            gameObject.SetActive(true);
            
            _effects = GetComponent<ProjectileEffects>();
            _effects.Initialize(projectileAttackData, this, pool, owner);
        }

        private void OnEnable()
        {
            StartCoroutine(LifeSpan());
        }

        private IEnumerator LifeSpan()
        {
            yield return new WaitForSeconds(_destroyTimeout);
            _effects.ReturnToPool();
        }

        private void Update()
        {
            if (_hasTarget && _target != null)
            {
                MoveTowardsTarget();
            }
            else
            {
                MoveForward();
            }
        }


        private void MoveTowardsTarget()
        {
            Vector3 directionToTarget = (_target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            transform.position =
                Vector3.MoveTowards(transform.position, _target.position, _attackData.speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            HitTarget(collision);
        }

        private void MoveForward()
        {
            if (_direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_direction);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }


            transform.position += _direction * (_attackData.speed * Time.deltaTime);
        }

        private void HitTarget(Collision collision)
        {
            if (_attackData == null) return;
            int hitCount = Physics.OverlapSphereNonAlloc(collision.transform.position, _attackData.Radius, hits, _attackConfig.EnemyMask);
            foreach (var hit in hits)
            {
                _attackData.ApplyEffect(hit.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            if(_attackData == null) return;
            Gizmos.DrawWireSphere(transform.position, _attackData.Radius);
            Gizmos.color = Color.yellow;
        }
    }
}
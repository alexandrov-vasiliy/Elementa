using _Elementa.Attack.Data;
using _Elementa.ObjectPool;
using UnityEngine;

namespace _Elementa.Attack.Projectiles
{
    [RequireComponent(typeof(ProjectileEffects))]
    public class Projectile : MonoBehaviour
    {
        private ProjectileAttackData _attackData;
        private Transform _target;
        private Vector3 _direction;
        private bool _hasTarget;
        
        private ProjectileEffects _effects;

        public void Initialize(ProjectileAttackData data, Transform owner, ObjectPool<Projectile> pool )
        {
            var position = owner.position;
            transform.position = position;

            
            _effects = GetComponent<ProjectileEffects>();
            _effects.Initialize(data.muzzlePrefab, data.hitPrefab, data.bodyPrefab, this, pool);
            
            _attackData = data;

            _target = FindNearestEnemy(position);
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
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _attackData.speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                HitTarget();
            }
        }

        private void MoveForward()
        {
            transform.position += _direction * (_attackData.speed * Time.deltaTime);

            // Уничтожаем снаряд, если он пролетел далеко
            if (Vector3.Distance(transform.position, _direction) > 20f)
            {
                _effects.ReturnToPool();
            }
        }

        private void HitTarget()
        {
            _attackData.ApplyEffect(_target.gameObject);
        }

        private Transform FindNearestEnemy(Vector3 position)
        {
            Collider[] hitColliders = Physics.OverlapSphere(position, _attackData.searchRadius);
            float closestDistance = float.MaxValue;
            Transform nearestEnemy = null;

            foreach (var collider in hitColliders)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    float distance = Vector3.Distance(position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestEnemy = enemy.transform;
                    }
                }
            }

            return nearestEnemy;
        }
        
    }
}
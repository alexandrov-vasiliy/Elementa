using System;
using _Elementa.ObjectPool;
using _Elementa.Player;
using UnityEngine;
using Zenject;

namespace _Elementa.Enemies.Scripts.SpawnStuff
{
    public class SphereApproachingCamera : MonoBehaviour
    {
    
        [SerializeField] private float _detectionDistance = 5f;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _enemyCount = 1;
    
        private bool _hasSpawned = false;

        [Inject(Id = nameof(PoolIds.Enemies))] private ObjectPool<Enemy> _enemyPool;
        private Camera _camera;

        void Start()
        {
            _camera = Camera.main;
           
        }

        void Update()
        {
            var hits = Physics.OverlapSphere(transform.position, _detectionDistance, layerMask: _playerLayer);
            foreach (var col in hits)
            {
                if (col.TryGetComponent(out PlayerBase playerBase))
                {
                    if (!_hasSpawned)
                    {
                        for (int i = 0; i < _enemyCount; i++)
                        {
                            SpawnEnemy();
                        }
            
                        _hasSpawned = true;
                        Destroy(gameObject);
                    }
                }
            }
            
        }
    
        private void SpawnEnemy()
        {
            if (_enemyPool != null)
            {
                Enemy enemy = _enemyPool.OnlyGet();
                enemy.transform.parent = null;
                Debug.Log($"Enemy spawn {enemy.transform.position} spawn position {transform.position}");
                enemy.transform.position = transform.position;
                enemy.gameObject.SetActive(true);

            }
            else
            {
                Debug.Log(_enemyPool);
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _detectionDistance);
            Gizmos.color = Color.magenta;
        }
    }
}


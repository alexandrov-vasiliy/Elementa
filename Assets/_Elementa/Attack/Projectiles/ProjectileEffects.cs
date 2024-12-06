using _Elementa.Attack.Projectiles;
using _Elementa.ObjectPool;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack
{
    public class ProjectileEffects : MonoBehaviour
    {
        private GameObject _muzzlePrefab;
        private GameObject _hitPrefab;
        private GameObject _bodyPrefab;
        private Projectile _projectile;

        private GameObject _body;

        private ObjectPool<Projectile> _pool;

        public void Initialize(GameObject muzzlePrefab, GameObject hitPrefab, GameObject bodyPrefab,
            Projectile projectile, ObjectPool<Projectile> pool)
        {
            _muzzlePrefab = muzzlePrefab;
            _hitPrefab = hitPrefab;
            _bodyPrefab = bodyPrefab;
            _projectile = projectile;

            SpawnMuzzle();
            _body = Instantiate(_bodyPrefab, transform);
            
            _pool = pool;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"on collision enter {collision.gameObject.name}");
            SpawnHit(collision);
        }

        private void SpawnMuzzle()
        {
            if (_muzzlePrefab != null)
            {
                var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, Quaternion.identity);
                muzzleVFX.transform.forward = gameObject.transform.forward;
                var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
                if (psMuzzle != null)
                {
                    Destroy(muzzleVFX, psMuzzle.main.duration);
                }
                else
                {
                    var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(muzzleVFX, psChild.main.duration);
                }
            }
        }


        public void ReturnToPool()
        {
            _pool.ReturnToPool(_projectile);
            Destroy(_body);
        }

        public void SpawnHit(Collision co)
        {
            ContactPoint contact = co.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            if (_hitPrefab != null)
            {
                var hitVFX = Instantiate(_hitPrefab, pos, rot);
                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            ReturnToPool();
        }
    }
}
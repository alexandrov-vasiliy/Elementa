using System.Collections;
using _Elementa.Attack.Data;
using _Elementa.Attack.Projectiles;
using _Elementa.ObjectPool;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack
{
    [RequireComponent(typeof(AudioSource))]
    public class ProjectileEffects : MonoBehaviour
    {
        private Projectile _projectile;

        private ProjectileAttackData _attackData;

        private GameObject _body;

        private ObjectPool<Projectile> _pool;
        private Transform _owner;

        private AudioSource _audioSource;

        public void Initialize(ProjectileAttackData attackData,
            Projectile projectile, ObjectPool<Projectile> pool, Transform owner)
        {
            _attackData = attackData;

            _projectile = projectile;
            _owner = owner;

            _audioSource = GetComponent<AudioSource>();
            SpawnMuzzle();
            _body = Instantiate(attackData.bodyPrefab, transform);
            if (_body.TryGetComponent(out ParticleSystem particleSystem))
            {
                particleSystem.Play();
            }
            _pool = pool;

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_attackData.DestroyAudio == null)
            {
                SpawnHit(collision);
                ReturnToPool();

                return;
            }
            StartCoroutine(PlayDestroyAudio(collision));
        }
        
        private IEnumerator PlayDestroyAudio(Collision collision)
        {
            PlayDestroy();
            SpawnHit(collision);
            Destroy(_body);

            yield return new WaitForSeconds(_attackData.DestroyAudio.length);

            _pool.ReturnToPool(_projectile);
        }

        

        private void PlayProcces()
        {
            if (_attackData.ProcessAudio == null) return;
            _audioSource.clip = _attackData.ProcessAudio;
            _audioSource.Play();
        }

        private void PlayDestroy()
        {
            if (_attackData.DestroyAudio == null) return;
            if(_audioSource.isPlaying) return;
            
            _audioSource.PlayOneShot(_attackData.DestroyAudio);
        }

        private void SpawnMuzzle()
        {
            if (_attackData.muzzlePrefab == null) return;

            Debug.Log($"SpawnMuzzle : {_owner.localRotation}");
            var muzzleVFX = Instantiate(_attackData.muzzlePrefab, _owner.position,
                Quaternion.LookRotation(_owner.forward));
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

            if (_attackData.hitPrefab != null)
            {
                var hitVFX = Instantiate(_attackData.hitPrefab, pos, rot);
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


            
        }
    }
}
using System;
using _Elementa.Attack.Data;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack.DamageVariants
{
    [RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
    public class Rock: MonoBehaviour
    {
        [SerializeField] private GameObject hitPrefab;
        [SerializeField] private float damageRadius;
        
         [SerializeField] private AttackData _attackData;

        [Inject] private AttackConfig _attackConfig;

        private AudioSource _audioSource;
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody>();
            
            _rigidbody.AddForce(Vector3.down * 130f, ForceMode.Force);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var colliders = Physics.OverlapSphere(collision.transform.position, damageRadius, _attackConfig.EnemyMask);
            
           var fx = Instantiate(hitPrefab);
            Destroy(fx, 1);
            
            foreach (var collider in colliders)
            {
                _attackData.ApplyEffect(collider.gameObject);
            }
            
            PlayDestroyAudio();
            
            gameObject.SetActive(false);
            Destroy(gameObject, 1);
            
        }

        private void PlayDestroyAudio()
        {
            if(_attackData.DestroyAudio == null) return;
            
            _audioSource.PlayOneShot(_attackData.DestroyAudio);

        }
        
    }
}
using System;
using _Elementa.Attack.Data;
using UnityEngine;
using Zenject;

namespace _Elementa.Attack.DamageVariants
{
    public class Rock: MonoBehaviour
    {
        [SerializeField] private GameObject hitPrefab;
        [SerializeField] private float damageRadius;
        
        private AttackData _attackData;

        [Inject] private AttackConfig _attackConfig;
        private void OnCollisionEnter(Collision collision)
        {
            var colliders = Physics.OverlapSphere(collision.transform.position, damageRadius, _attackConfig.EnemyMask);
            
           var fx = Instantiate(hitPrefab);
            Destroy(fx, 1);
            
            foreach (var collider in colliders)
            {
                _attackData.ApplyEffect(collider.gameObject);
            }
            Destroy(gameObject);

        }
    }
}
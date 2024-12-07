using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Elementa.Attack.DamageVariants
{
    public class SteamCloud : MonoBehaviour
    {
        private float _damage;
        private float _damageInterval = 0.5f; 
        private float _lifetime = 5.0f;
        private Dictionary<IDamageable, Coroutine> _activeDamageCoroutines = new Dictionary<IDamageable, Coroutine>();

        public void Initialize(float damage, float lifetime)
        {
            _damage = damage;
            _lifetime = lifetime;
            gameObject.SetActive(true);

            StartCoroutine(DeactivateAfterTimeout());
        }

        private void Awake()
        {
            Deactivate();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                if (!_activeDamageCoroutines.ContainsKey(damageable))
                {
                    Coroutine damageCoroutine = StartCoroutine(DealDamageWithInterval(damageable));
                    _activeDamageCoroutines.Add(damageable, damageCoroutine);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                if (_activeDamageCoroutines.TryGetValue(damageable, out Coroutine damageCoroutine))
                {
                    StopCoroutine(damageCoroutine);
                    _activeDamageCoroutines.Remove(damageable);
                }
            }
        }

        private IEnumerator DealDamageWithInterval(IDamageable damageable)
        {
            while (true)
            {
                damageable.Damage(_damage);
                yield return new WaitForSeconds(_damageInterval);
            }
        }

        private IEnumerator DeactivateAfterTimeout()
        {
            yield return new WaitForSeconds(_lifetime);
            Deactivate();
        }

        private void Deactivate()
        {
            foreach (var damageCoroutine in _activeDamageCoroutines.Values)
            {
                StopCoroutine(damageCoroutine);
            }

            _activeDamageCoroutines.Clear();
            gameObject.SetActive(false);
        }
    }
}

using System;
using UnityEngine;

namespace _Elementa.Player
{
    [RequireComponent(typeof(CharacterMovement), typeof(Health))]
    public class PlayerBase : MonoBehaviour
    {
        public CharacterMovement CharacterController;
        [SerializeField] private Health _health;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _hitParticles;
        private static readonly int Hit = Animator.StringToHash(nameof(Hit));

        private void Start()
        {
            CharacterController ??= GetComponent<CharacterMovement>();
        }

        private void OnValidate()
        {
            _health ??= GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnTakeDamage += HandleDamage;
        }

        private void OnDisable()
        {
            _health.OnTakeDamage -= HandleDamage;
        }

        private void HandleDamage()
        {
            _animator.SetTrigger(Hit);
            var fx = Instantiate(_hitParticles, transform);
            Destroy(fx, 2f);
        }
    }
}

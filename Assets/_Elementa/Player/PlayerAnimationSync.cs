using System;
using _Elementa.Attack;
using UnityEngine;

namespace _Elementa.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationSync : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerAttack _playerAttack;
        private static readonly int Speed = Animator.StringToHash(nameof(Speed));
        private static readonly int ZVel = Animator.StringToHash(nameof(ZVel));
        private static readonly int XVel = Animator.StringToHash(nameof(XVel));
        private static readonly int Attack = Animator.StringToHash(nameof(Attack));

        void Start()
        {
            _animator ??= GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _playerAttack.OnAttack += HandleAttack;
        }

        private void OnDisable()
        {
            _playerAttack.OnAttack -= HandleAttack;
        }

        void Update()
        {
            Vector3 velocity = _controller.velocity;

            _animator.SetFloat(Speed, velocity.magnitude);

            // Извлекаем значения для YVel и ZVel
            float xVel = velocity.normalized.x; // Горизонтальная скорость
            float zVel = velocity.normalized.z; // Движение вперед / назад

            // Обновляем параметры анимации
            _animator.SetFloat(ZVel, zVel);
            _animator.SetFloat(XVel, xVel);
        }

        private void HandleAttack()
        {
            _animator.SetTrigger(Attack);
        }
    }
}

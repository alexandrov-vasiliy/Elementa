using System;
using _Elementa.Attack;
using UnityEngine;
using Zenject;

namespace _Elementa.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationSync : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerAttack _playerAttack;

        private CharacterMovement _characterMovement;
        
        private static readonly int Speed = Animator.StringToHash(nameof(Speed));
        private static readonly int ZVel = Animator.StringToHash(nameof(ZVel));
        private static readonly int XVel = Animator.StringToHash(nameof(XVel));
        private static readonly int Attack = Animator.StringToHash(nameof(Attack));

        [Inject]
        public void Construct(CharacterMovement characterMovement)
        {
            _characterMovement = characterMovement;
        }
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

            // Устанавливаем общую скорость для BlendTree
            _animator.SetFloat(Speed, velocity.magnitude);

            var target = _characterMovement.Target;
            
            if (target != null)
            {
                // Направление на таргет
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                // Создаем Quaternion для вращения персонажа в сторону таргета
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

                // Преобразуем глобальную скорость в локальные координаты относительно персонажа
                Vector3 localVelocity = Quaternion.Inverse(targetRotation) * velocity;

                // Извлекаем значения для анимации
                float xVel = localVelocity.x; // Горизонтальная скорость (влево/вправо)
                float zVel = localVelocity.z; // Движение вперед/назад

                // Обновляем параметры анимации
                _animator.SetFloat(ZVel, zVel);
                _animator.SetFloat(XVel, xVel);
            }
            else
            {
                // Если таргета нет, используем текущие локальные координаты персонажа
                Vector3 localVelocity = transform.InverseTransformDirection(velocity);

                float xVel = localVelocity.x; // Горизонтальная скорость (влево/вправо)
                float zVel = localVelocity.z; // Движение вперед/назад

                _animator.SetFloat(ZVel, zVel);
                _animator.SetFloat(XVel, xVel);
            }
        }

        private void HandleAttack()
        {
            _animator.SetTrigger(Attack);
        }
    }
}

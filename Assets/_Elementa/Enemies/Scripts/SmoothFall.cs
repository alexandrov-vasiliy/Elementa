using UnityEngine;

namespace _Elementa.Enemies.Scripts
{
    public class SmoothFall : MonoBehaviour
    {
        [SerializeField] private float fallSpeed = 2.0f; // Скорость падения
        [SerializeField] private float fallDistance = 2.0f; // Расстояние, на которое опустится объект
        private Vector3 targetPosition;
        [SerializeField] private bool isFalling = false;

        

        private void Update()
        {
            if (isFalling)
            {
                if (targetPosition == Vector3.zero)
                {
                    targetPosition = transform.position - new Vector3(0, fallDistance, 0);
                }
                // Плавное перемещение объекта вниз
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallSpeed * Time.deltaTime);

                // Если объект достиг конечной позиции, останавливаем падение
                if (transform.position == targetPosition)
                {
                    isFalling = false;
                }
            }
        }

        // Вызывается для запуска анимации падения
        public void StartFalling()
        {
            targetPosition = transform.position - new Vector3(0, fallDistance, 0);
            isFalling = true;
            Debug.Log($"start falling {gameObject.name}");
        }
    }
}
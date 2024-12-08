using UnityEngine;
using UnityEngine.UI;

namespace _Elementa.HP
{
    [RequireComponent(typeof(Image))]
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;
        [SerializeField] private Image _image;
        
        
        private void OnEnable()
        {
            _playerHealth.OnHealthChange += UpdateBar;
        } 
        private void OnDisable()
        {
            _playerHealth.OnHealthChange -= UpdateBar;
        }

        private void UpdateBar()
        {
            float maxHealth = 100f; // Предположим, что максимальное здоровье фиксировано
            float currentHealth = Mathf.Clamp(_playerHealth.Value, 0f, maxHealth);

            // Рассчитываем процент оставшегося здоровья
            float fillAmount = currentHealth / maxHealth;

            // Обновляем шкалу
            _image.fillAmount  = fillAmount;
        }
    }
}

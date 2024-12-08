using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public float Value { private set; get; } = 100f;

    public event System.Action OnDeath;
    
    public event System.Action OnHealthChange;
    public event System.Action OnTakeDamage;

    public void Damage(float damage)
    {
        if (damage<0)
        {
            Debug.LogError("negative damage");
        }
        else
        {
            Value -= damage;
            OnTakeDamage?.Invoke();
            OnHealthChange?.Invoke();
        }

        if (Value <= 0f)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        Value += heal;
        OnHealthChange?.Invoke();
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}

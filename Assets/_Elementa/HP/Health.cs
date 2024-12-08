using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;

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
            health -= damage;
            OnTakeDamage?.Invoke();
            OnHealthChange?.Invoke();
        }

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        OnHealthChange?.Invoke();
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}

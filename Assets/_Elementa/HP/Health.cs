using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private float health = 100f;

    public event System.Action OnDeath;
    
    public event System.Action OnHealthChange;

    public void Damage(float damage)
    {
        if (damage<0)
        {
            Debug.LogError("negative damage");
        }
        else
        {
            health -= damage;
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

using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private UnityEvent<float> onHealthChange;

    private UnityEvent onDamageRecived;
    [SerializeField]
    private UnityEvent onDeath;

    private float currentHealth;
    public float CurrentHealth => currentHealth;

    public void InitializateHealth()
    {
        SetHealth(maxHealth);
    }
    private void SetHealth(float health)
    {
        currentHealth = health;
        onHealthChange?.Invoke(currentHealth / maxHealth);
    }
    public void TakeDamage(float damage)
    {
        SetHealth(currentHealth - damage);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            onDamageRecived?.Invoke();
        }
    }
    private void Die()
    {
        onDeath?.Invoke();
    }
}

using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    public int CurrentHealth { get; private set; }

    public event Action<int> HealthChanghed;
    public event Action Died;

    public void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        HealthChanghed?.Invoke(CurrentHealth);

        if (CurrentHealth == 0)
        {
            Died?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth += amount;
        CurrentHealth = Math.Min(CurrentHealth, _maxHealth);
        HealthChanghed?.Invoke(CurrentHealth);
    }
}
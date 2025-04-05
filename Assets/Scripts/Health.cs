using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _max = 100;

    public int Current { get; private set; }

    public event Action<int> Changhed;
    public event Action Died;

    public void Awake()
    {
        Current = _max;
    }

    public void TakeDamage(int damage)
    {
        if (Current <= 0) 
            return;

        Current -= damage;
        Current = Mathf.Max(Current, 0);
        Changhed?.Invoke(Current);

        if (Current == 0)
        {
            Died?.Invoke();
        }
    }

    public void TakeHeal(int amount)
    {
        if (Current <= 0) 
            return;

        Current += amount;
        Current = Math.Min(Current, _max);
        Changhed?.Invoke(Current);
    }
}
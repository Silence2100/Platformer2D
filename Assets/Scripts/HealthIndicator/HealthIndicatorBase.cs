using UnityEngine;

public abstract class HealthIndicatorBase : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected virtual void Start()
    {
        HandleHealthChanged(Health.Current, Health.Max);
    }

    protected virtual void Awake()
    {
        Health.ValueChanged += HandleHealthChanged;
    }

    private void OnDestroy()
    {
        Health.ValueChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(float current, float max)
    {
        OnHealthChanged(current, max);
    }

    protected abstract void OnHealthChanged(float current, float max);
}
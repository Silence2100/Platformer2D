using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public abstract class HealthBarBaseIndicator : HealthIndicatorBase
{
    protected Slider Slider;
    protected float TargetValue;

    protected override void Awake()
    {
        base.Awake();
        Slider = GetComponent<Slider>();
        Slider.minValue = 0f;
        Slider.maxValue = 1f;
    }

    protected override void OnHealthChanged(float current, float max)
    {
        float normalized = (max > 0f) ? current / max : 0f;
        TargetValue = Mathf.Clamp01(normalized);
        OnNormalizedChanged(TargetValue);
    }

    protected abstract void OnNormalizedChanged(float normalizedValue);
}
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBarIndicator : HealthBarBaseIndicator
{
    protected override void OnNormalizedChanged(float normalizedValue)
    {
        Slider.value = normalizedValue;
    }
}
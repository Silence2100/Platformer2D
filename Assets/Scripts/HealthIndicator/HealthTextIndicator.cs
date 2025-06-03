using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class HealthTextIndicator : HealthIndicatorBase
{
    private TMP_Text _text;

    protected override void Awake()
    {
        _text = GetComponent<TMP_Text>();
        base.Awake();
    }

    protected override void OnHealthChanged(float current, float max)
    {
        _text.text = $"{current}/{max}";
    }
}
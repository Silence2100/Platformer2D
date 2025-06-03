using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthSmoothBarIndicator : HealthBarBaseIndicator
{
    [SerializeField] private float _smoothSpeed = 1f;
    private float _displayedValue;

    protected override void Start()
    {
        base.Start();
        _displayedValue = TargetValue;
        Slider.value = _displayedValue;
    }

    protected override void OnNormalizedChanged(float normalizedValue)
    {
        TargetValue = normalizedValue;
    }

    private void Update()
    {
        if (_displayedValue != TargetValue)
        {
            _displayedValue = Mathf.MoveTowards(_displayedValue, TargetValue, _smoothSpeed * Time.deltaTime);
            Slider.value = _displayedValue;
        }
    }
}
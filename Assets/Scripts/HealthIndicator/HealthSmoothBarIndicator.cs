using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthSmoothBarIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _smoothSpeed = 1f;

    private Slider _slider;
    private float _targetValue;
    private float _displayedValue;
    private Coroutine _smoothRoutine;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = 0f;
        _slider.maxValue = 1f;

        if (_health != null)
        {
            _health.ValueChanged += OnHealthChanged;

            float normalized = (_health.Max > 0f) 
                ? _health.Current / (float)_health.Max 
                : 0f;
            _targetValue = normalized;
            _displayedValue = normalized;
            _slider.value = normalized;
        }
    }

    private void OnDestroy()
    {
        _health.ValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float current, float max)
    {
        if (max > 0f)
        {
            _targetValue = Mathf.Clamp01(current / max);
        }
        else
        {
            _targetValue = 0f;
        }

        if (_smoothRoutine != null)
        {
            StopCoroutine(_smoothRoutine);
        }

        _smoothRoutine = StartCoroutine(SmoothFill());
    }

    private IEnumerator SmoothFill()
    {
        while (!Mathf.Approximately(_displayedValue, _targetValue))
        {
            _displayedValue = Mathf.MoveTowards(_displayedValue, _targetValue, _smoothSpeed * Time.deltaTime);

            _slider.value = _displayedValue;
            yield return null;
        }

        _smoothRoutine = null;
    }
}
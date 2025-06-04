using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthSmoothBarIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _smoothSpeed = 1f;

    private Slider _slider;
    private Coroutine _smoothRoutine;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = 0f;
        _slider.maxValue = 1f;

        if (_health != null)
        {
            _health.ValueChanged += OnHealthChanged;

            float initialNormalized = (_health.Max > 0f) ? _health.Current / (float)_health.Max : 0f;
            _slider.value = initialNormalized;
        }
    }

    private void OnDestroy()
    {
        _health.ValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float current, float max)
    {
        float targetNormalized = (max > 0f) ? Mathf.Clamp01(current / max) : 0f;

        if (_smoothRoutine != null)
        {
            StopCoroutine(_smoothRoutine);
        }

        float startNormalized = _slider.value;
        float difference = Mathf.Abs(targetNormalized - startNormalized);

        if (difference == 0f)
        {
            _slider.value = targetNormalized;
            return;
        }

        float duration = difference / _smoothSpeed;
        _smoothRoutine = StartCoroutine(SmoothFill(startNormalized, targetNormalized, duration));
    }

    private IEnumerator SmoothFill(float startValue, float targetValue, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float normalizedTime = elapsed / duration;
            _slider.value = Mathf.Lerp(startValue, targetValue, normalizedTime);
            elapsed += Time.deltaTime;

            yield return null;
        }

        _slider.value = targetValue;
        _smoothRoutine = null;
    }
}
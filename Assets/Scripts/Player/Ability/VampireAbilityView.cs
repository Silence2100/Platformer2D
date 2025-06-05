using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(VampireAbilityLogic))]
public class VampireAbilityView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _radiusIndicator;
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private TMP_Text _cooldownText;

    private VampireAbilityLogic _logic;

    private void Awake()
    {
        _logic = GetComponent<VampireAbilityLogic>();
    }

    private void OnEnable()
    {
        _logic.OnStateChanged += HandleStateChanged;
        _logic.OnTimerChanged += HandleTimerChanged;
    }

    private void OnDisable()
    {
        _logic.OnStateChanged -= HandleStateChanged;
        _logic.OnTimerChanged -= HandleTimerChanged;
    }

    private void Start()
    {
        ApplyReadyState();
    }

    private void HandleStateChanged(VampireAbilityLogic.State newState)
    {
        switch (newState)
        {
            case VampireAbilityLogic.State.Ready:
                ApplyReadyState();
                break;

            case VampireAbilityLogic.State.Active:
                ApplyActiveState();
                    break;

            case VampireAbilityLogic.State.Cooldown:
                ApplyCooldownState();
                break;
        }
    }

    private void HandleTimerChanged(VampireAbilityLogic.State state, float remainingTime, float totalTime)
    {
        if (state == VampireAbilityLogic.State.Active)
        {
            float normalized = Mathf.Clamp01(remainingTime / totalTime);
            _cooldownSlider.value = normalized;

            if (_cooldownText != null)
            {
                _cooldownText.text = $"{remainingTime:F1}s / {totalTime:F1}s";
            }
        }
        else if (state == VampireAbilityLogic.State.Cooldown)
        {
            float elapsed = totalTime - remainingTime;
            float normalized = Mathf.Clamp01(elapsed / totalTime);
            _cooldownSlider.value = normalized;

            if (_cooldownText != null)
            {
                _cooldownText.text = $"{elapsed:F1}s / {totalTime:F1}s";
            }
        }
    }

    private void ApplyReadyState()
    {
        if (_radiusIndicator != null)
        {
            _radiusIndicator.enabled = false;
        }

        if (_cooldownSlider != null)
        {
            _cooldownSlider.value = 1f;
        }

        if (_cooldownText != null)
        {
            _cooldownText.text = "";
        }
    }

    private void ApplyActiveState()
    {
        if (_radiusIndicator != null)
        {
            _radiusIndicator.enabled = true;
        }

        if (_cooldownSlider != null)
        {
            _cooldownSlider.value = 1f;
        }
    }

    private void ApplyCooldownState()
    {
        if (_radiusIndicator != null)
        {
            _radiusIndicator.enabled = false;
        }

        if (_cooldownSlider != null)
        {
            _cooldownSlider.value = 0f;
        }
    }
}
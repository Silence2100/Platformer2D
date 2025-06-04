using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private TMP_Text _cooldownText;

    [SerializeField] private float _vampireRadius = 3f;
    [SerializeField] private int _damagePerSecond = 10;
    [SerializeField] private float _activeDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private GameObject _radiusIndicator;

    private float _activeTimer = 0f;
    private float _cooldownTimer = 0f;

    private bool _isActive = false;
    private bool _isOnCooldown = false;

    private Health _playerHealth;
    private float _damageAccumulator = 0f;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();

        if (_cooldownSlider != null)
        {
            _cooldownSlider.minValue = 0f;
            _cooldownSlider.maxValue = 1f;
            _cooldownSlider.value = 1f;
        }

        if (_cooldownText != null)
        {
            _cooldownText.text = "";
        }

        _isActive = false;
        _isOnCooldown = false;

        if (_radiusIndicator != null)
        {
            _radiusIndicator.SetActive(false);
        }
    }

    private void Update()
    {
        UpdateActive();
        UpdateCooldown();
        UpdateUI();
    }

    public void TryActivate()
    {
        if (_isActive == false && _isOnCooldown == false)
        {
            Activate();
        }
    }

    private void Activate()
    {
        _isActive = true;
        _activeTimer = _activeDuration;
        _damageAccumulator = 0f;

        if (_radiusIndicator != null)
        {
            _radiusIndicator.SetActive(true);
        }
    }

    private void UpdateActive()
    {
        if (_isActive)
        {
            _activeTimer -= Time.deltaTime;

            if (_activeTimer <= 0f)
            {
                Deactivate();
            }
            else
            {
                ProcessVampireDamage();
            }
        }
    }

    private void Deactivate()
    {
        _isActive = false;
        _isOnCooldown = true;
        _cooldownTimer = _cooldownDuration;

        if (_radiusIndicator != null)
        {
            _radiusIndicator.SetActive(false);
        }
    }

    private void UpdateCooldown()
    {
        if (_isOnCooldown)
        {
            _cooldownTimer -= Time.deltaTime;

            if (_cooldownTimer <= 0f)
            {
                _isOnCooldown = false;
                _cooldownTimer = 0f;
            }
        }
    }

    private void ProcessVampireDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _vampireRadius, _enemyLayer);

        if (hits.Length == 0)
        {
            return;
        }

        Collider2D nearest = null;
        float minDistSq = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            float distSq = ((Vector2)hit.transform.position - (Vector2)transform.position).sqrMagnitude;

            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                nearest = hit;
            }
        }

        if (nearest != null && nearest.TryGetComponent(out Health enemyHealth))
        {
            float rawDamageThisFrame = _damagePerSecond * Time.deltaTime;
            _damageAccumulator += rawDamageThisFrame;

            if (_damageAccumulator >= 1f)
            {
                int damageToApply = Mathf.FloorToInt(_damageAccumulator);

                enemyHealth.TakeDamage(damageToApply);

                if(_playerHealth != null)
                {
                    _playerHealth.TakeHeal(damageToApply);
                }

                _damageAccumulator -= damageToApply;
            }
        }
    }

    private void UpdateUI()
    {
        if (_cooldownSlider == null)
        {
            return;
        }

        if (_isActive)
        {
            float normalized = Mathf.Clamp01(_activeTimer / _activeDuration);
            _cooldownSlider.value = normalized;

            if (_cooldownText != null)
            {
                _cooldownText.text = $"{_activeTimer:F1}s / {_activeDuration:F1}s";
            }
        }
        else if (_isOnCooldown)
        {
            float normalized = Mathf.Clamp01(1f - (_cooldownTimer / _cooldownDuration));
            _cooldownSlider.value = normalized;

            if (_cooldownText != null)
            {
                float elapsed = _cooldownDuration - _cooldownTimer;
                _cooldownText.text = $"{elapsed:F1}s / {_cooldownDuration:F1}s";
            }
        }
        else
        {
            _cooldownSlider.value = 1f;

            if (_cooldownText != null)
            {
                _cooldownText.text = "";
            }
        }
    }
}
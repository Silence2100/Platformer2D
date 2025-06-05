using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class VampireAbilityLogic : MonoBehaviour
{
    private const int MaxOverlap = 16;
    private readonly Collider2D[] _overlapBuffer = new Collider2D[MaxOverlap];

    public enum State
    {
        Ready,
        Active,
        Cooldown
    }

    [SerializeField] private float _vampireRadius = 3f;
    [SerializeField] private int _damagePerSecond = 10;
    [SerializeField] private float _activeDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;

    [SerializeField] private LayerMask _enemyLayer;


    private float _damageAccumulator = 0f;
    private State _state = State.Ready;
    private Health _playerHealth;
    private Coroutine _vampireRoutine = null;

    public event Action<State> OnStateChanged;
    public event Action<State, float, float> OnTimerChanged;
    public event Action<int, Health> OnVampireHit;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();

        OnStateChanged?.Invoke(State.Ready);
    }

    public void TryActivate()
    {
        if (_state != State.Ready || _vampireRoutine != null)
        {
            return;
        }

        _vampireRoutine = StartCoroutine(VampireRoutine());
    }

    private IEnumerator VampireRoutine()
    {
        yield return RunActivePhase();
        yield return RunCooldownPhase();
        SwitchToReady();
        _vampireRoutine = null;
    }

    private IEnumerator RunActivePhase()
    {
        SwitchState(State.Active);

        float elapsed = 0f;
        _damageAccumulator = 0f;

        while (elapsed < _activeDuration)
        {
            float deltaTime = Time.deltaTime;
            elapsed += deltaTime;
            float remaining = Mathf.Max(_activeDuration - elapsed, 0f);

            OnTimerChanged?.Invoke(State.Active, remaining, _activeDuration);

            if (remaining > 0f)
            {
                ProcessVampireDamage(deltaTime);
            }

            yield return null;
        }
    }

    private IEnumerator RunCooldownPhase()
    {
        SwitchState(State.Cooldown);

        float elapsed = 0f;

        while (elapsed < _cooldownDuration)
        {
            float deltaTime = Time.deltaTime;
            elapsed += deltaTime;
            float remaining = Mathf.Max(_cooldownDuration - elapsed, 0f);

            OnTimerChanged?.Invoke(State.Cooldown, remaining, _cooldownDuration);

            yield return null;
        }
    }

    private void SwitchToReady()
    {
        _state = State.Ready;
        OnStateChanged?.Invoke(State.Ready);
    }

    private void SwitchState(State newState)
    {
        _state |= newState;
        OnStateChanged?.Invoke(newState);
    }

    private void ProcessVampireDamage(float deltaTime)
    {
        int count = Physics2D.OverlapCircleNonAlloc((Vector2)transform.position, _vampireRadius, _overlapBuffer, _enemyLayer);

        if (count == 0)
        {
            return;
        }

        Health closestEnemyHealth = FindNearestEnemy(count);

        if (closestEnemyHealth == null)
        {
            return;
        }

        _damageAccumulator += _damagePerSecond * deltaTime;

        if (_damageAccumulator >= 1f)
        {
            int damageToApply = Mathf.FloorToInt(_damageAccumulator);
            _damageAccumulator -= damageToApply;

            closestEnemyHealth.TakeDamage(damageToApply);
            _playerHealth.TakeHeal(damageToApply);

            OnVampireHit?.Invoke(damageToApply, closestEnemyHealth);
        }
    }

    private Health FindNearestEnemy(int count)
    {
        Collider2D nearestCollider = null;
        float minDistSq = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            Collider2D collider = _overlapBuffer[i];

            if (collider == null)
            {
                continue;
            }

            float distSq = ((Vector2)collider.transform.position - (Vector2)transform.position).sqrMagnitude;

            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                nearestCollider = collider;
            }
        }

        if (nearestCollider != null && nearestCollider.TryGetComponent(out Health enemyHealth))
        {
            return enemyHealth;
        }

        return null;
    }
}
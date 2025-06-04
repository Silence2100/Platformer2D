using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(EnemyDetection))]

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private PatrolBehavior _patrolBehavior;
    [SerializeField] private ChaseBehavior _chaseBehavior;

    private Health _health;
    private Attacker _attacker;
    private EnemyDetection _detection;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
        _detection = GetComponent<EnemyDetection>();

        _health.Died += OnEnemyDied;

        _detection.PlayerDetected += OnPlayerDetected;
        _detection.PlayerLost += OnPlayerLost;
    }

    private void Start()
    {
        if (_patrolBehavior != null)
        {
            _patrolBehavior.enabled = true;
            _patrolBehavior.AttackRequested += HandleAttackRequested;
        }

        if (_chaseBehavior != null)
        {
            _chaseBehavior.enabled = false;
            _chaseBehavior.AttackRequested += HandleAttackRequested;
        }
    }

    private void OnDestroy()
    {
        if (_health != null)
        {
            _health.Died -= OnEnemyDied;
        }

        if (_detection != null)
        {
            _detection.PlayerDetected -= OnPlayerDetected;
            _detection.PlayerLost -= OnPlayerLost;
        }

        if (_patrolBehavior != null)
        {
            _patrolBehavior.AttackRequested -= HandleAttackRequested;
        }

        if (_chaseBehavior != null)
        {
            _chaseBehavior.AttackRequested -= HandleAttackRequested;
        }
    }

    private void OnPlayerDetected(Transform player)
    {
        if (_patrolBehavior != null)
        {
            _patrolBehavior.enabled = false;
        }

        if (_chaseBehavior != null)
        {
            _chaseBehavior.SetTarget(player);
            _chaseBehavior.enabled = true;
        }
    }

    private void OnPlayerLost(Transform player)
    {
        if (_chaseBehavior != null)
        {
            _chaseBehavior.enabled = false;
        }

        if (_patrolBehavior != null)
        {
            _patrolBehavior.enabled = true;
        }
    }

    private void HandleAttackRequested()
    {
        _attacker.Attack();
    }

    private void OnEnemyDied()
    {
        Destroy(gameObject);
    }
}
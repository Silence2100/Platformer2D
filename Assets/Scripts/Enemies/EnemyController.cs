using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(EnemyDetection))]

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PatrolBehavior _patrolBehavior;
    [SerializeField] private ChaseBehavior _chaseBehavior;

    private Health _health;
    private EnemyDetection _detection;

    private void Awake()
    {
        _health = GetComponent<Health>();
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
        }

        if (_chaseBehavior != null)
        {
            _chaseBehavior.enabled = false;
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

    private void OnEnemyDied()
    {
        Destroy(gameObject);
    }
}

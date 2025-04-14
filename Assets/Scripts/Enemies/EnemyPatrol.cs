using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private EnemyDetection _detection;
    private Transform _player;
    private bool _isChasing = false;

    private float _targetThreshold = 0.1f;
    private int _currentTargetIndex = 0;
    private Health _health;
    private Attacker _attack;

    private Quaternion _rightRotation;
    private Quaternion _leftRotation;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _attack = GetComponent<Attacker>();

        _health.Changhed += OnHealthChanged;
        _health.Died += OnEnemyDied;

        _rightRotation = Quaternion.identity;
        _leftRotation = Quaternion.Euler(0, 180, 0);
    }

    private void OnEnable()
    {
        if (_detection != null)
        {
            _detection.PlayerDetected += OnPlayerDetected;
            _detection.PlayerLost += OnPlayerLost;
        }
    }

    private void OnDisable()
    {
        if (_detection != null)
        {
            _detection.PlayerDetected -= OnPlayerDetected;
            _detection.PlayerLost -= OnPlayerLost;
        }

        _health.Changhed -= OnHealthChanged;
        _health.Died -= OnEnemyDied;
    }

    private void Update()
    {
        if (_isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Transform target = _patrolPoints[_currentTargetIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, _patrolSpeed * Time.deltaTime);

        if (Vector2.SqrMagnitude(transform.position - target.position) < _targetThreshold)
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % _patrolPoints.Length;
            Flip();
        }

        _attack.Attack();
    }

    private void ChasePlayer()
    {
        if (_player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _chaseSpeed * Time.deltaTime);
        }

        _attack.Attack();
    }

    private void Flip()
    {
        transform.rotation = transform.rotation == _rightRotation ? _leftRotation : _rightRotation;
    }

    private void OnHealthChanged(int currentHealth) { }

    private void OnEnemyDied()
    {
        Destroy(gameObject);
    }

    private void OnPlayerDetected(Transform player)
    {
        _isChasing = true;
        _player = player;
    }

    private void OnPlayerLost(Transform player)
    {
        if (_player == player)
        {
            _isChasing = false;
            _player = null;
        }
    }
}
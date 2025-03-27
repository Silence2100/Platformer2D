using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 4f;
    private Transform _player;
    private bool _isChasing = false;

    private float _targetThreshold = 0.1f;
    private int _currentTargetIndex = 0;
    private Health _health;
    private Attack _attack;

    private Quaternion _rightRotation;
    private Quaternion _leftRotation;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();

        _health.HealthChanghed += OnHealthChanged;
        _health.Died += OnEnemyDied;

        _rightRotation = Quaternion.identity;
        _leftRotation = Quaternion.Euler(0, 180, 0);
    }

    private void OnDisable()
    {
        _health.HealthChanghed -= OnHealthChanged;
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

        _attack.AttackTarget();
    }

    private void ChasePlayer()
    {
        if (_player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _chaseSpeed * Time.deltaTime);
        }

        _attack.AttackTarget();
    }

    private void Flip()
    {
        transform.rotation = transform.rotation == _rightRotation ? _leftRotation : _rightRotation;
    }

    private void OnHealthChanged(int currentHealth)
    {
        Debug.Log("НР врага = " + _health.CurrentHealth);
    }

    private void OnEnemyDied()
    {
        Destroy(gameObject);
        Debug.Log("Enemy died");
    }

    public void SetChasing(bool chasing, Transform player)
    {
        _isChasing = chasing;
        _player = player;
    }
}
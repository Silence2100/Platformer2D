using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _speed = 2f;

    private float _targetThreshold = 0.1f;
    private int _currentTargetIndex = 0;
    private Quaternion _rightRotation;
    private Quaternion _leftRotation;

    private void Awake()
    {
        _rightRotation = Quaternion.identity;
        _leftRotation = Quaternion.Euler(0, 180, 0);
    }

    private void Update()
    {
        Transform target = _patrolPoints[_currentTargetIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (Vector2.SqrMagnitude(transform.position - target.position) < _targetThreshold)
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % _patrolPoints.Length;
            Flip();
        }
    }

    private void Flip()
    {
        transform.rotation = transform.rotation == _rightRotation ? _leftRotation : _rightRotation;
    }
}
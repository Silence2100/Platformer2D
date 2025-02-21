using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _speed = 2f;

    private int _currentTargetIndex = 0;
    private float _targetThreshold = 0.1f;

    private void Update()
    {
        if (_patrolPoints.Length == 0) return;

        Transform target = _patrolPoints[_currentTargetIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        float distanceSqr = (transform.position - target.position).sqrMagnitude;

        if (distanceSqr < _targetThreshold * _targetThreshold)
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % _patrolPoints.Length;
            Flip();
        }
    }

    private void Flip()
    {
        transform.rotation = Quaternion.Euler(0, _currentTargetIndex == 0 ? 0 : 180, 0);
    }
}
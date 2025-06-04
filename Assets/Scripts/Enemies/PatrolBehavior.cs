using System;
using UnityEngine;

public class PatrolBehavior : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private Transform _visuals;

    public event Action AttackRequested;

    private int _currentTargetIndex = 0;
    private float _targetThreshold = 0.1f;

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

        transform.position = Vector2.MoveTowards(transform.position, target.position, _patrolSpeed * Time.deltaTime);

        if (Vector2.SqrMagnitude(transform.position - target.position) < _targetThreshold)
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % _patrolPoints.Length;
            FlipVisuals();
        }

        AttackRequested?.Invoke();
    }

    private void FlipVisuals()
    {
        Quaternion current = _visuals.rotation;
        _visuals.rotation = (current == _rightRotation) ? _leftRotation : _rightRotation;
    }
}
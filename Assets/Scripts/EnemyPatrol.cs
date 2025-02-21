using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _pointA, _pointB;
    [SerializeField] private float _speed = 2f;

    private float _targetThreshold = 0.1f;

    private Transform _target;
    private int _directionMultiplier = 1;

    private void Start()
    {
        _target = _pointA;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _target.position) < _targetThreshold)
        {
            _target = _target == _pointA ? _pointB : _pointA;
            Flip();
        }
    }

    private void Flip()
    {
        _directionMultiplier *= -1;
        transform.localScale = new Vector3(_directionMultiplier * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
using System;
using UnityEngine;

public class ChaseBehavior : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private Transform _visuals;

    public event Action AttackRequested;

    private Transform _player;
    private Attacker _attacker;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
    }

    public void SetTarget(Transform playerTransform)
    {
        _player = playerTransform;
    }

    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _chaseSpeed * Time.deltaTime);

        FlipTowardsPlayer();

        float sqrDistance = (transform.position - _player.position).sqrMagnitude;
        float attackRange = (_attacker != null) ? _attacker.AttackRange : 0f;
        float attackRangeSq = attackRange * attackRange;

        if (sqrDistance <= attackRangeSq)
        {
            AttackRequested?.Invoke();
        }
    }

    private void FlipTowardsPlayer()
    {
        if (_player.position.x > transform.position.x)
        {
            _visuals.rotation = Quaternion.identity;
        }
        else
        {
            _visuals.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
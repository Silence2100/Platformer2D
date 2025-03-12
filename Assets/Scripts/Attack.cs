using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackRange = 3.0f;
    [SerializeField] private float _attackCooldown = 1.0f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _attackPoint;

    private float _lastAttackTime;

    private void TryAttack()
    {
        if (Time.time - _lastAttackTime < _attackCooldown) return;

        Collider2D target = Physics2D.OverlapCircle(_attackPoint.position, _attackRange,_targetLayer);

        if (target != null && target.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.TakeDamage(_damage);
            _lastAttackTime = Time.time;
        }
    }

    public void AttackTarget() => TryAttack();
}

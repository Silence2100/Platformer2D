using UnityEngine;

[RequireComponent(typeof(Attacker))]

public class ChaseBehavior : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private Transform _visuals;

    private Attacker _attacker;
    private Transform _player;

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

        _attacker.Attack();
        FlipTowardsPlayer();
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
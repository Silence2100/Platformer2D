using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(ItemDetector))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 32f;

    private Rigidbody2D _rigidbody;
    private PlayerAnimator _animator;
    private GroundChecker _ground;
    private InputReader _input;
    private Health _health;
    private Attacker _attack;

    private Quaternion _right = Quaternion.identity;
    private Quaternion _left = Quaternion.Euler(0, 180, 0);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
        _ground = GetComponent<GroundChecker>();
        _input = GetComponent<InputReader>();
        _health = GetComponent<Health>();
        _attack = GetComponent<Attacker>();

        _health.Changhed += OnHealthChanged;
        _health.Died += OnPlayerDied;
    }

    private void OnEnable()
    {
        _input.JumpPressed += Jump;
        _input.AttackPressed += Attack;
    }

    private void OnDisable()
    {
        _input.JumpPressed -= Jump;
        _input.AttackPressed -= Attack;

        _health.Changhed -= OnHealthChanged;
        _health.Died -= OnPlayerDied;
    }

    private void Update()
    {
        float moveInput = _input.MoveInput;
        _rigidbody.linearVelocity = new Vector2(moveInput * _speed, _rigidbody.linearVelocity.y);

        _animator.SetSpeed(Mathf.Abs(moveInput));

        transform.rotation = moveInput > 0 ? _right = Quaternion.identity :
                             moveInput < 0 ? _left :
                             transform.rotation;
    }

    private void Jump()
    {
        if (_ground.IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }
    }

    private void Attack()
    {
        _attack.Attack();
    }

    private void OnHealthChanged(int currentHealth)
    {
        Debug.Log("НР игрока = " + _health.Current);
    }

    private void OnPlayerDied()
    {
        Destroy(gameObject);
        Debug.Log("Player died");
    }

    public void Heal(int amount)
    {
        _health.TakeHeal(amount);
    }
}
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attack))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 32f;

    private Rigidbody2D _rigidbody;
    private PlayerAnimator _playerAnimator;
    private GroundChecker _groundChecker;
    private InputReader _inputReader;
    private Health _health;
    private Attack _attack;

    private Quaternion _rightRotation;
    private Quaternion _leftRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _groundChecker = GetComponent<GroundChecker>();
        _inputReader = GetComponent<InputReader>();
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();

        _rightRotation = Quaternion.identity;
        _leftRotation = Quaternion.Euler(0, 180, 0);

        _health.HealthChanghed += OnHealthChanged;
        _health.Died += OnPlayerDied;
    }

    private void OnEnable()
    {
        _inputReader.JumpPressed += Jump;
        _inputReader.AttackPressed += Attack;
    }

    private void OnDisable()
    {
        _inputReader.JumpPressed -= Jump;
        _inputReader.AttackPressed -= Attack;

        _health.HealthChanghed -= OnHealthChanged;
        _health.Died -= OnPlayerDied;
    }

    private void Update()
    {
        float moveInput = _inputReader.MoveInput;
        _rigidbody.linearVelocity = new Vector2(moveInput * _speed, _rigidbody.linearVelocity.y);

        _playerAnimator.SetSpeed(Mathf.Abs(moveInput));

        transform.rotation = moveInput > 0 ? _rightRotation :
                             moveInput < 0 ? _leftRotation :
                             transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            Destroy(coin.gameObject);
        }

        if (collision.TryGetComponent<HealthPack>(out HealthPack healthPack))
        {
            _health.Heal(healthPack.HealthAmount);
            Destroy(healthPack.gameObject); 
        }
    }

    private void Jump()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }
    }

    private void Attack()
    {
        _attack.AttackTarget();
    }

    private void OnHealthChanged(int currentHealth)
    {
        Debug.Log("НР игрока = " + _health.CurrentHealth);
    }

    private void OnPlayerDied()
    {
        Destroy(gameObject);
        Debug.Log("Player died");
    }
}
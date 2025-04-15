using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(ItemDetector))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(Jumper))]

public class Player : MonoBehaviour, IItemVisitor
{
    [SerializeField] private float _speed = 10f;

    private Rigidbody2D _rigidbody;
    private PlayerAnimator _animator;
    private InputReader _input;
    private Health _health;
    private Attacker _attack;
    private Flipper _flipper;
    private Jumper _jumper;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<PlayerAnimator>();
        _input = GetComponent<InputReader>();
        _health = GetComponent<Health>();
        _attack = GetComponent<Attacker>();
        _flipper = GetComponent<Flipper>();
        _jumper = GetComponent<Jumper>();
    }

    private void OnEnable()
    {
        _health.Changhed += OnHealthChanged;
        _health.Died += OnPlayerDied;

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
        _flipper.Flip(moveInput);
    }

    private void Jump()
    {
        _jumper.Jump();
    }

    private void Attack()
    {
        _attack.Attack();
    }

    private void OnHealthChanged(int currentHealth) { }

    private void OnPlayerDied()
    {
        Destroy(gameObject);
        Debug.Log("Player died");
    }

    public void Heal(int amount)
    {
        _health.TakeHeal(amount);
    }

    public void Visit(Coin coin)
    {
        coin.Collect(this);
    }

    public void Visit(HealthPack healthPack)
    {
        healthPack.Heal(this);
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 32f;

    private Rigidbody2D _rigidbody;
    private PlayerAnimator _playerAnimator;
    private GroundChecker _groundChecker;
    private InputReader _inputReader;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _groundChecker = GetComponent<GroundChecker>();
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        float moveInput = _inputReader.MoveInput;
        _rigidbody.linearVelocity = new Vector2(moveInput * _speed, _rigidbody.linearVelocity.y);

        _playerAnimator.SetSpeed(Mathf.Abs(moveInput));

        if (moveInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (_inputReader.JumpPressed && _groundChecker.IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            Destroy(coin.gameObject);
        }
    }
}
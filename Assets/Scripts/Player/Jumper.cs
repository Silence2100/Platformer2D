using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(GroundChecker))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 32f;

    private Rigidbody2D _rigidbody;
    private GroundChecker _ground;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ground = GetComponent<GroundChecker>();
    }

    public void Jump()
    {
        if (_ground.IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }
    }
}
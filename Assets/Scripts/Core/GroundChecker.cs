using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _groundChech;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkDistance = 0.1f;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        IsGrounded = Physics2D.Raycast(_groundChech.position, Vector2.down, _checkDistance, _groundLayer);
    }
}
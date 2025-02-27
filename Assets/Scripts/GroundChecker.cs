using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _groundChech;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkRadius = 0.1f;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(_groundChech.position, _checkRadius, _groundLayer);
    }
}
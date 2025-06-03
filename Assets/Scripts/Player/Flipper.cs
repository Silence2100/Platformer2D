using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private Transform _visuals;

    private Quaternion _right = Quaternion.identity;
    private Quaternion _left = Quaternion.Euler(0, 180, 0);

    public void Flip(float direction)
    {
        if (direction > 0)
        {
            _visuals.rotation = _right;
        }
        else if (direction < 0)
        {
            _visuals.rotation = _left;
        }
    }
}
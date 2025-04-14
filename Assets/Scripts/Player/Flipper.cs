using UnityEngine;

public class Flipper : MonoBehaviour
{
    private Quaternion _right = Quaternion.identity;
    private Quaternion _left = Quaternion.Euler(0, 180, 0);

    public void Flip(float direction)
    {
        if (direction > 0)
        {
            transform.rotation = _right;
        }
        else if (direction < 0)
        {
            transform.rotation = _left;
        }
    }
}
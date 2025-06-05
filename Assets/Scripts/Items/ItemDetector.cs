using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Item item) && TryGetComponent(out IItemVisitor visitor))
        {
            item.Accept(visitor);
        }
    }
}
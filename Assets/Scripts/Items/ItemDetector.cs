using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item>(out var item) &&
            TryGetComponent<IItemVisitor>(out var visitor))
        {
            item.Accept(visitor);
        }
    }
}
using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item>(out Item item))
        {
            if (item is Coin coin)
            {
                coin.Use(GetComponent<Player>());
                Destroy(coin.gameObject);
            }
            else if (item is HealthPack healthPack)
            {
                healthPack.Use(GetComponent<Player>());
                Destroy(healthPack.gameObject);
            }
        }
    }
}
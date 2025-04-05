using System;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public event Action<Transform> PlayerDetected;
    public event Action<Transform> PlayerLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            PlayerDetected?.Invoke(player.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            PlayerLost?.Invoke(player.transform);
        }
    }
}
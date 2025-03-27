using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private EnemyPatrol _enemyPatrol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerContoller))
        {
            Transform player = playerContoller.transform;
            _enemyPatrol.SetChasing(true, player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerContoller))
        {
            Transform player = playerContoller.transform;
            _enemyPatrol.SetChasing(false, player);
        }
    }
}
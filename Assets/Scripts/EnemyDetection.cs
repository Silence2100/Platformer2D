using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private EnemyPatrol _enemyPatrol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerContoller))
        {
            _enemyPatrol.SetChasing(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerContoller))
        {
            _enemyPatrol.SetChasing(false);
        }
    }
}

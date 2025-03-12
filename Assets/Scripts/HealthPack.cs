using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int _healthAmount = 20;

    public int HealthAmount => _healthAmount;
}

using UnityEngine;

public class HealthPack : Item
{
    [SerializeField] private int _healthAmount = 20;

    public override void Use(Player player)
    {
        Debug.Log($"�� ���������� �� {_healthAmount} HP.");
        player.Heal(_healthAmount);
    }
}
using UnityEngine;

public class HealthPack : Item
{
    [SerializeField] private int _healthAmount = 20;

    public override void Use(Player player)
    {
        player.Heal(_healthAmount);
    }
}
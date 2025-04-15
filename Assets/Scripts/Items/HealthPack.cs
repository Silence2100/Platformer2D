using UnityEngine;

public class HealthPack : Item
{
    [SerializeField] private int _healthAmount = 20;

    public override void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Heal(Player player)
    {
        player.Heal(_healthAmount);
        Destroy(gameObject);
    }
}
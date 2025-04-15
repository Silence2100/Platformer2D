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
        Health health = player.GetComponent<Health>();
        health.TakeHeal(_healthAmount);
        Destroy(gameObject);
    }
}
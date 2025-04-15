using UnityEngine;

public class Coin : Item
{
    public override void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Collect(Player player)
    {
        Destroy(gameObject);
    }
}
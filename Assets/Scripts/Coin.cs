using UnityEngine;

public class Coin : Item
{
    public override void Use(Player player)
    {
        Debug.Log("Подобрали монетку.");
    }
}
using UnityEngine;

public interface IItemVisitor
{
    void Visit(Coin coin);
    void Visit(HealthPack healthPack);
}
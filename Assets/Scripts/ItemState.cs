using UnityEngine;

public abstract class ItemState : IItemState
{
    public abstract void Update();

    public abstract void Start();

    public abstract void HandleCollision(Collider2D collider);
}
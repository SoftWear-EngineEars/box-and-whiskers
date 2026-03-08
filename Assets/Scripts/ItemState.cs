using UnityEngine;

public abstract class ItemState
{
    public ItemState(Item item)
    {
        Item = item;
    }
    
    protected Item Item;
    public abstract void Update();

    public abstract void Start();

    public abstract void HandleCollision(Collider2D collider);
}
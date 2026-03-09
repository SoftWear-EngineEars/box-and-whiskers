using UnityEngine;

public interface IItemState
{
    public void Update();

    public void Start();

    public void HandleCollision(Collider2D collider);
}
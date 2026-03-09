using System;
using UnityEngine;

public abstract class Item : MonoBehaviour 
{
    protected IItemState ItemState;
    public void Update()
    {
        ItemState.Update();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        ItemState.HandleCollision(other);
    }

    public void SetState(IItemState state)
    {
        ItemState = state;
        state.Start();
    }
}
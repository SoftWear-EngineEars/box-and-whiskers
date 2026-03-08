using System;
using UnityEngine;

public abstract class Item : MonoBehaviour 
{
    protected ItemState ItemState;
    private void Update()
    {
        ItemState.Update();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemState.HandleCollision(other);
    }

    public void SetState(ItemState state)
    {
        ItemState = state;
        state.Start();
    }
}
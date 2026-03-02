using UnityEngine;

public interface ICanMove : IState
{
    Rigidbody2D Rigidbody { get; }
    void IState.HandleHorizontal(float amount)
    {
        Rigidbody.linearVelocityX = amount * Player.GetMovementSpeed();
    }
}
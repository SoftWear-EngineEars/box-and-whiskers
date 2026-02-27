using System;
using UnityEngine;

public class NormalState : IState
{

    private readonly Rigidbody2D _rigidbody;
    protected Player Player;
    public NormalState(Player player)
    {
        Player = player;
        _rigidbody = Player.GetComponent<Rigidbody2D>();
    }
    
    public virtual void HandleUp()
    {
        _rigidbody.AddForce(new Vector2(0, Player.GetJumpStrength()), ForceMode2D.Impulse);
        Player.SetState(JumpState());
    }
    
    protected virtual JumpingState JumpState()
    {
        return new JumpingState(Player);
    }
    
    public void HandleHorizontal(float amount)
    {
        _rigidbody.linearVelocityX = amount * Player.GetMovementSpeed();
        Debug.Log(amount);
    }

    public virtual void AdvanceState()
    {
        // stay in Normal
    }
}
using UnityEditor;
using UnityEngine;

public abstract class PlayerState 
{
    public Player Player { get; }
    public Rigidbody2D Rigidbody { get; }

    public PlayerState(Player player)
    {
        Player = player;
        Rigidbody = player.GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        if (Player.UpAction.triggered && Player.CanJump())
        {
            Jump();
        }
        
        Move(Player.HorizontalAction.ReadValue<float>());
    }

    public abstract void Start();

    public virtual void Move(float amount)
    {
        Rigidbody.linearVelocityX = amount * Player.GetMovementSpeed();
    }

    public virtual void Jump()
    {
        Rigidbody.AddForce(new Vector2(0, Player.GetJumpStrength()), ForceMode2D.Impulse);
    }
}
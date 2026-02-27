using UnityEngine;

public class WhiskerNormalState : WhiskerState
{
    private const float JumpStrength = 8.0f;
    private const float MovementSpeed = 3.0f;

    private readonly Rigidbody2D _rigidbody;
    public WhiskerNormalState(Whiskers whiskers) : base(whiskers)
    {
        _rigidbody = Whiskers.GetComponent<Rigidbody2D>();
    }
    
    public override void HandleUp()
    {
        _rigidbody.AddForce(new Vector2(0, JumpStrength), ForceMode2D.Impulse);
        Whiskers.SetState(new WhiskerJumpingState(Whiskers));
    }

    public override void HandleHorizontal(float amount)
    {
        _rigidbody.linearVelocityX = amount * MovementSpeed;
    }

    public override void HandleShift()
    {
        // TODO: join with box
    }

    public override void AdvanceState()
    {
        // stay in Normal
    }
}
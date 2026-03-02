using UnityEngine;

public interface ICanJump : IState
{
    Rigidbody2D Rigidbody { get; }
    void IState.HandleUp()
    {
        Rigidbody.AddForce(new Vector2(0, Player.GetJumpStrength()), ForceMode2D.Impulse);
        Player.SetState(JumpState());
    }

    IState JumpState();
}
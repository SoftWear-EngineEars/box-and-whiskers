using UnityEngine;

public class JumpingState : PlayerState, ICanMove
{
    public JumpingState(Player player) : base(player) { }
    public void HandleUp()
    {
        // Do nothing. We are jumping.
    }
}
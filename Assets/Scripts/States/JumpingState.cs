using UnityEngine;

public class JumpingState : NormalState
{
    private BoxCollider2D collider;
    
    public JumpingState(Player player) : base(player)
    {
        collider = player.GetComponent<BoxCollider2D>();
    }

    public override void HandleUp()
    {
        // Do nothing. We are jumping.
    }

    public override void AdvanceState()
    {
        // TODO if on floor, return to normal state; perhaps use observer pattern?
    }
}
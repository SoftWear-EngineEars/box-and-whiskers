using UnityEngine;

public class WhiskerJumpingState : WhiskerNormalState
{
    private BoxCollider2D collider;
    
    public WhiskerJumpingState(Whiskers whiskers) : base(whiskers)
    {
        collider = whiskers.GetComponent<BoxCollider2D>();
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
public class WhiskerCombinedJumpingState : PlayerState, IWhiskerState
{
    public WhiskerCombinedJumpingState(Whiskers whiskers) : base(whiskers) { }

    public void HandleHorizontal(float amount)
    {
        // Cannot move while in box
    }

    public void HandleUp()
    {
        // Cannot jump
    }

    public void HandleShift()
    {
        var whiskers = (Whiskers)Player;
        
        whiskers.ExitBox();
        whiskers.SetState(new JumpingState(whiskers));
    }
}
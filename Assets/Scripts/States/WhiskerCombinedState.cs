public class WhiskerCombinedState : PlayerState, ICanJump, IWhiskerState
{
    public WhiskerCombinedState(Whiskers whiskers) : base(whiskers) { }
    public IState JumpState()
    {
        return new WhiskerCombinedJumpingState((Whiskers)Player);
    }

    public void HandleHorizontal(float amount)
    {
        // Cannot move while in box
    }

    public void HandleShift()
    {
        var whiskers = (Whiskers)Player;
        
        whiskers.ExitBox();
        whiskers.SetState(new JumpingState(whiskers));
    }
}
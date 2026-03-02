public class WhiskerJumpingState : PlayerState, ICanMove, IWhiskerState
{
    public WhiskerJumpingState(Whiskers whiskers) : base(whiskers) { }

    public void HandleUp()
    {
        // Do nothing
    }
}
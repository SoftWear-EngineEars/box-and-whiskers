public class WhiskerNormalState : NormalState, IWhiskerState
{
    public WhiskerNormalState(Whiskers whiskers) : base(whiskers) { }

    public void HandleShift()
    {
        // TODO: join with box
    }

    protected override JumpingState JumpState()
    {
        return new WhiskerJumpingState((Whiskers)Player);
    }
}
public abstract class WhiskerState : IState
{
    protected Whiskers Whiskers;

    public WhiskerState(Whiskers whiskers)
    {
        Whiskers = whiskers;
    }
    
    public abstract void HandleShift();
    public abstract void HandleHorizontal(float amount);
    public abstract void HandleUp();
    public abstract void AdvanceState();
}
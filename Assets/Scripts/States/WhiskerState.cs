using Unity.VisualScripting;

public abstract class WhiskerState : PlayerState
{
    protected Whiskers Whiskers;
    
    public WhiskerState(Whiskers whiskers) : base(whiskers)
    {
        Whiskers = whiskers;
    }

    public override void Update()
    {
        base.Update();

        if (Whiskers.ShiftAction.triggered)
        {
            HandleShift();
        }
    }
    
    public virtual void HandleShift() 
    {
        var box = Whiskers.EnterBox();
        Whiskers.SetState(new WhiskerCombinedState(Whiskers, box));
    }
}
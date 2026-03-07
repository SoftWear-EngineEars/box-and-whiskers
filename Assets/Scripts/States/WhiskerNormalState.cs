public class WhiskerNormalState : WhiskerState
{
    public WhiskerNormalState(Whiskers whiskers) : base(whiskers) { }

    public override void Start()
    {
        Player.UpAction.Enable();
        Player.HorizontalAction.Enable();
    }
}
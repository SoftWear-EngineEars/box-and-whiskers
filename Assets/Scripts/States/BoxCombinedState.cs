public class BoxCombinedState : PlayerState
{
    public BoxCombinedState(Box box) : base(box) { }

    public override void Start()
    {
        Player.UpAction.Disable();
        Player.HorizontalAction.Enable();
    }
}
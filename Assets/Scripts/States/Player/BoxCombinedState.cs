public class BoxCombinedState : BoxState
{
    public BoxCombinedState(Box box) : base(box) { }

    public override void Start()
    {
        Player.UpAction.Disable();
        Player.HorizontalAction.Enable();
    }

    public override void HandleCombinationEvent(CombinationEvent combinationEvent)
    {
        if (combinationEvent == CombinationEvent.Uncombine)
        {
            Box.SetState(new BoxNormalState(Box));
        }
    }

}
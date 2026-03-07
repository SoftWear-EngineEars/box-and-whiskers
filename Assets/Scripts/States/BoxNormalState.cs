public class BoxNormalState : BoxState
{
    public BoxNormalState(Box box) : base(box) { } 
    
    public override void Start()
    {
        Player.UpAction.Enable();
        Player.HorizontalAction.Enable();
    }

    public override void HandleCombinationEvent(CombinationEvent combinationEvent)
    {
        if (combinationEvent == CombinationEvent.Combine)
            Box.SetState(new BoxCombinedState(Box));
    }
}
public abstract class BoxState : PlayerState
{
    protected Box Box;

    public BoxState(Box box) : base(box)
    {
        Box = box;
    }

    public abstract void HandleCombinationEvent(CombinationEvent combinationEvent);
}
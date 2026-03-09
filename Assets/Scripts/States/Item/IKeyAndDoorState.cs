public interface IKeyAndDoorState : IItemState
{
    void HandleCombinationEvent(CombinationEvent combinationEvent);
}
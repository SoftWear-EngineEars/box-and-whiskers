public abstract class KeyAndDoorState : ItemState, IKeyAndDoorState
{
    protected KeyAndDoor KeyAndDoor;
    public KeyAndDoorState(KeyAndDoor keyAndDoor)
    {
        KeyAndDoor = keyAndDoor;
    }

    public abstract void HandleCombinationEvent(CombinationEvent combinationEvent);
}
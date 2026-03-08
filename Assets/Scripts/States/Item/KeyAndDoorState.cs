public abstract class KeyAndDoorState : ItemState
{
    protected KeyAndDoor KeyAndDoor;
    public KeyAndDoorState(KeyAndDoor keyAndDoor) : base(keyAndDoor)
    {
        KeyAndDoor = keyAndDoor;
    }

    public abstract void HandleCombinationEvent(CombinationEvent combinationEvent);
}
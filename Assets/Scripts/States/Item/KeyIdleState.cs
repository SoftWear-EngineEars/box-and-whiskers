using UnityEngine;

public class KeyIdleState : KeyAndDoorState
{
    public KeyIdleState(KeyAndDoor keyAndDoor) : base(keyAndDoor) { }

    public override void HandleCombinationEvent(CombinationEvent combinationEvent)
    {
        // ignore it! could do a little indicator though like a glow if there's time
    }

    public override void Update()
    {
        // Idle
    }

    public override void Start()
    {
        // Do nothing
    }

    public override void HandleCollision(Collider2D collider)
    {
        var whiskers = collider.GetComponent<Whiskers>();
        if (whiskers == null)
            return;
        
        KeyAndDoor.SetState(new KeyFollowState(KeyAndDoor, whiskers));
    }
}
using UnityEngine;

public class KeyIdleState : KeyAndDoorState, IDependency<INotifier<KeyCollectEvent>>
{
    public KeyIdleState(KeyAndDoor keyAndDoor) : base(keyAndDoor)
    {
        // see note to WhiskerCombinedState's constructor for why dependency is set here
        SetDependency(KeyCollectEventNotifier.Instance);
    }

    private INotifier<KeyCollectEvent> _keyCollectEventNotifier;

    public void SetDependency(INotifier<KeyCollectEvent> dependency)
    {
        _keyCollectEventNotifier = dependency;
    }

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
        
        _keyCollectEventNotifier.NotifySubscribers(new KeyCollectEvent());
        KeyAndDoor.SetState(new KeyFollowState(KeyAndDoor, whiskers));
    }
}
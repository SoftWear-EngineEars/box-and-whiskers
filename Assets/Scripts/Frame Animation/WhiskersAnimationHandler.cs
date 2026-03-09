using System.Diagnostics;
using System.Security;
using UnityEngine;

public class WhiskersAnimationHandler : PlayerAnimationHandler, ISubscriber<CombinationEvent>
{
    public INotifier<CombinationEvent> Notifier { get; set; }

    public void ReceiveEvent(CombinationEvent message)
    {
        UnityEngine.Debug.Log("Received combination event: " + message.GetType().Name);
        SpriteAnimation state = Player.GetAnimationState();

        ((WhiskersSpriteAnimation)state).HandleMerge();
        string spriteName = state.GetSprite();

        UnityEngine.Debug.Log("Handling merge animation frame");
        UnityEngine.Debug.Log("Next state: " + state.GetNextState().GetType().Name);
        UnityEngine.Debug.Log("Current state: " + state.GetType().Name);

        Player.SetAnimationState(state.GetNextState());
        SetSprite(spriteName);
    }

    void Start()
    {
        // Find a random combination event notifier!! it works!!
        SetDependency(FindObjectOfType<CombinationEventNotifier>());
    }

    public override void Update()
    {
        UnityEngine.Debug.Log("Animation state: " + Player.GetAnimationState().GetType().Name);
        if (Player.GetAnimationState() is WhiskersMergeAnimation)
        {
            float movementSpeed = ((Whiskers)Player).EnterBox().GetMovementSpeed();
            UnityEngine.Debug.Log("Whiskers is merging. Movement speed: " + movementSpeed);
            if (movementSpeed < 0)
            {
                HandleLeft();
            }
            else if (movementSpeed > 0)
            {
                HandleRight();
            }
        }
        else
        {
            base.Update();
        }
    }

    public void SetDependency(INotifier<CombinationEvent> notifier)
    {
        Notifier = notifier;
        Notifier.RegisterSubscriber(this);
    }
}

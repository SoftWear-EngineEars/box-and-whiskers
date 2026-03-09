using System.Diagnostics;
using System.Security;
using UnityEngine;

public class WhiskersAnimationHandler : PlayerAnimationHandler, ISubscriber<CombinationEvent>
{
    public INotifier<CombinationEvent> Notifier { get; set; }

    public void ReceiveEvent(CombinationEvent message)
    {
        SpriteAnimation state = Player.GetAnimationState();

        ((WhiskersSpriteAnimation)state).HandleMerge();
        string spriteName = state.GetSprite();

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
        if (Player.GetAnimationState() == null)
        {
            Player.SetAnimationState(new WhiskersIdleAnimation(Player, 0));
        }
        Player.GetAnimationState().UpdateAnimation();

        if (Player.GetAnimationState() is WhiskersMergeAnimation)
        {
            // Get velocity of whiskers's parent
            float movementSpeed = ((Whiskers)Player).EnterBox().GetComponent<Rigidbody2D>().linearVelocity.x;
            if (movementSpeed < 0)
            {
                HandleLeft();
            }
            else if (movementSpeed > 0)
            {
                HandleRight();
            }

            UpdateSprite();
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

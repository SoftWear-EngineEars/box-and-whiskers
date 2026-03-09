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

    public override void Update()
    {
        if (Player.GetAnimationState() is WhiskersMergeAnimation)
        {
            float movementSpeed = ((Whiskers)Player).EnterBox().GetMovementSpeed();
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
}

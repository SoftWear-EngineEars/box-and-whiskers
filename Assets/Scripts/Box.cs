using System;
using UnityEngine.InputSystem;

public class Box : Player, ISubscriber<CombinationEvent>
{
    protected override void Start()
    {
        base.Start();
        
        UpAction = InputSystem.actions.FindAction("BoxUp");
        HorizontalAction = InputSystem.actions.FindAction("BoxL/R");

        SetState(new BoxNormalState(this));

        var animationHandler = gameObject.AddComponent<BoxAnimationHandler>();
        animationHandler.Initialize(this);
        SetAnimationHandler(animationHandler);

        SetAnimationState(new BoxNormalAnimation(this, 0));
    }

    public void ReceiveEvent(CombinationEvent message)
    {
        ((BoxState)State).HandleCombinationEvent(message);
    }

    public void SetDependency(INotifier<CombinationEvent> dependency)
    {
        dependency.RegisterSubscriber(this);
    }
}
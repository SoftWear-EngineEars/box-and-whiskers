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
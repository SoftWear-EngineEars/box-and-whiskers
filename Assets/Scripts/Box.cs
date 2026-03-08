using System;
using UnityEngine.InputSystem;

public class Box : Player, ISubscriber<CombinationEvent>
{
    public INotifier<CombinationEvent> Notifier { get; set; } 

    protected override void Start()
    {
        base.Start();
        
        UpAction = InputSystem.actions.FindAction("BoxUp");
        HorizontalAction = InputSystem.actions.FindAction("BoxL/R");

        Notifier.RegisterSubscriber(this);
        
        SetState(new BoxNormalState(this));
    }

    public void ReceiveEvent(CombinationEvent message)
    {
        ((BoxState)State).HandleCombinationEvent(message);
    }
}
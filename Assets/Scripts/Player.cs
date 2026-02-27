using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player<TS> : MonoBehaviour where TS : IState
{
    protected TS State { get; private set; }

    protected InputAction UpAction;
    protected InputAction HorizontalAction;

    public virtual void Update()
    {
        if (UpAction.triggered)
        {
            State.HandleUp();
        }
        
        State.HandleHorizontal(HorizontalAction.ReadValue<float>());
        
        State.AdvanceState();
    }

    public void SetState(TS state)
    {
        State = state;
    }
}

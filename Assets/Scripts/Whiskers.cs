using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Whiskers : Player
{
    private InputAction _shiftAction;
    private void Start()
    {
        UpAction = InputSystem.actions.FindAction("WhiskerUp");
        HorizontalAction = InputSystem.actions.FindAction("WhiskerL/R");
        _shiftAction = InputSystem.actions.FindAction("WhiskerShift");

        SetState(new WhiskerNormalState(this));
    }

    public override void Update()
    {
        base.Update();

        if (_shiftAction.triggered)
        {
            ((IWhiskerState)State).HandleShift();
        }
    }

    public void EnterBox()
    {
        // TODO
    }

    public void ExitBox()
    {
        // TODO
    }
}
    
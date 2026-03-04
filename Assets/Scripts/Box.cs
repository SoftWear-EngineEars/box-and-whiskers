using System;
using UnityEngine.InputSystem;

public class Box : Player
{
    protected override void Start()
    {
        base.Start();
        
        UpAction = InputSystem.actions.FindAction("BoxUp");
        HorizontalAction = InputSystem.actions.FindAction("BoxL/R");
        
        SetState(new NormalState(this));
    }
}
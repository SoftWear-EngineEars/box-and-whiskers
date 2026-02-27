using System;
using UnityEngine.InputSystem;

public class Box : Player
{
    private void Start()
    {
        UpAction = InputSystem.actions.FindAction("BoxUp");
        HorizontalAction = InputSystem.actions.FindAction("BoxL/R");
        
        SetState(new NormalState(this));
    }
}
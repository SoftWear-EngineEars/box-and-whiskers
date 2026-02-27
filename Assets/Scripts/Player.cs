using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player : MonoBehaviour
{
    protected IState State { get; private set; }

    protected InputAction UpAction;
    protected InputAction HorizontalAction;

    [SerializeField] private float jumpStrength;
    [SerializeField] private float movementSpeed;

    public virtual void Update()
    {
        if (UpAction.triggered)
        {
            State.HandleUp();
        }
        
        State.HandleHorizontal(HorizontalAction.ReadValue<float>());
        
        State.AdvanceState();
    }

    public void SetState(IState state)
    {
        State = state;
    }

    public float GetJumpStrength()
    {
        return jumpStrength;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
}

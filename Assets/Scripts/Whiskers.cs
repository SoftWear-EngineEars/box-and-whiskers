using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Whiskers : Player
{
    public InputAction ShiftAction { get; private set; }

    [SerializeField] private Box box;
    protected override void Start()
    {
        base.Start();
        
        UpAction = InputSystem.actions.FindAction("WhiskerUp");
        HorizontalAction = InputSystem.actions.FindAction("WhiskerL/R");
        ShiftAction = InputSystem.actions.FindAction("WhiskerShift");
        
        SetState(new WhiskerNormalState(this));
        
        var animationHandler = gameObject.AddComponent<WhiskersAnimationHandler>();
        animationHandler.Initialize(this);
        SetAnimationHandler(animationHandler);

        SetAnimationState(new WhiskersIdleAnimation(this, 0));
    }
    public Box EnterBox()
    {
        const float maxDistance = 1.5f;
        if (Vector2.Distance(transform.position, box.transform.position) > maxDistance)
            return null;

        return box;
    }
}

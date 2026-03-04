using UnityEngine.InputSystem;

public class Whiskers : Player
{
    public InputAction ShiftAction { get; private set; }
    protected override void Start()
    {
        base.Start();
        
        UpAction = InputSystem.actions.FindAction("WhiskerUp");
        HorizontalAction = InputSystem.actions.FindAction("WhiskerL/R");
        ShiftAction = InputSystem.actions.FindAction("WhiskerShift");

        SetState(new WhiskerNormalState(this));
    }
    public Box EnterBox()
    {
        // TODO
        return null;
    }

    public void ExitBox()
    {
        // TODO
    }
}
    
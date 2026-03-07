using UnityEngine;

public class WhiskerCombinedState : PlayerState
{
    private readonly Box _box;

    public WhiskerCombinedState(Whiskers whiskers, Box box) : base(whiskers)
    {
        _box = box;
    }

    public void HandleShift()
    {
        var whiskers = (Whiskers)Player;
        
        whiskers.ExitBox();
        whiskers.SetState(new WhiskerNormalState(whiskers));
    }

    public override void Start()
    {
        Player.HorizontalAction.Enable();
        Player.UpAction.Disable();
    }

    public override void Jump()
    {
        Rigidbody.AddForce(new Vector2(0, _box.GetJumpStrength()), ForceMode2D.Impulse);
    }
}
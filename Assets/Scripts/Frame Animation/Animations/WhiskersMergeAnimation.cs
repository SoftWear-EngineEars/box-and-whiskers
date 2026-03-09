using UnityEngine;

public class WhiskersMergeAnimation : WhiskersSpriteAnimation
{
    public WhiskersMergeAnimation(Player player, int frameNumber) : base("whiskers_in_box", player, frameNumber)
    {
        nextState = this;
    }

    protected override void HandleFall()
    {
        return;
    }

    protected override void HandleRise()
    {
        return;
    }

    protected override void HandleWalk()
    {
        return;
    }

    protected override void HandleIdle()
    {
        return;
    }

    public override void HandleMerge()
    {
        nextState = new WhiskersIdleAnimation(player, currentFrame);
    }
}

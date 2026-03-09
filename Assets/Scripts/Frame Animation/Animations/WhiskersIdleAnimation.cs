using UnityEngine;

public class WhiskersIdleAnimation : WhiskersSpriteAnimation
{
    public WhiskersIdleAnimation(Player player, int frameNumber) : base("whiskers_idle", player, frameNumber)
    {
    }

    protected override void HandleFall()
    {
        nextState = new WhiskersFallAnimation(player, currentFrame);
    }

    protected override void HandleRise()
    {
        nextState = new WhiskersRiseAnimation(player, currentFrame);
    }

    protected override void HandleWalk()
    {
        nextState = new WhiskersWalkAnimation(player, currentFrame);
    }

    protected override void HandleIdle()
    {
        // Remain in idle animation
        nextState = this;
    }

    public override void HandleMerge()
    {
        nextState = new WhiskersMergeAnimation(player, currentFrame);
    }
}

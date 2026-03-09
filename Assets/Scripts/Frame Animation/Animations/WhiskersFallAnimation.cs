using UnityEngine;

public class WhiskersFallAnimation : WhiskersSpriteAnimation
{
    public WhiskersFallAnimation(Player player, int frameNumber) : base("whiskers_falling", player, frameNumber)
    {
    }

    protected override void HandleFall()
    {
        // Remain in fall animation
        nextState = this;
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
        nextState = new WhiskersIdleAnimation(player, currentFrame);
    }

    public override void HandleMerge()
    {
        nextState = new WhiskersMergeAnimation(player, currentFrame);
    }
}

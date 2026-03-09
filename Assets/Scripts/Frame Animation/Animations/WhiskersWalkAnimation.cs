using UnityEngine;

public class WhiskersWalkAnimation : WhiskersSpriteAnimation
{
    public WhiskersWalkAnimation(Player player, int frameNumber) : base(4, "whiskers_walking", player, frameNumber)
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
        // Remain in walk animation
        nextState = this;
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

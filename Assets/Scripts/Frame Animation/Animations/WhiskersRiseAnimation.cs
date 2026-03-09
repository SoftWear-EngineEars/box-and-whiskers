using UnityEngine;

public class WhiskersRiseAnimation : WhiskersSpriteAnimation
{
    public WhiskersRiseAnimation(Player player, int frameNumber) : base("whiskers_jumping", player, frameNumber)
    {
    }

    protected override void HandleFall()
    {
        nextState = new WhiskersFallAnimation(player, currentFrame);
    }

    protected override void HandleRise()
    {
        // Remain in rise animation
        nextState = this;
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
